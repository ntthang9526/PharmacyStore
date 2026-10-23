using System.Data;
using Microsoft.Data.SqlClient;
using Store.Shared;

namespace Store.DAL
{
    public class BanHangDAL
    {
        // Giữ kiểu DECIMAL(18,2) của DB; giá trị tiền được chuẩn hóa về đồng nguyên.
        private static SqlParameter TienParam(string ten, decimal giaTri)
        {
            return new SqlParameter(ten, SqlDbType.Decimal) { Precision = 18, Scale = 2, Value = giaTri };
        }

        // 1. Tìm thuốc để bán: theo tên, mã, barcode hoặc hoạt chất.
        //    Tồn kho chỉ tính các lô CÒN HẠN và còn hàng.
        public DataTable TimThuocBan(string tuKhoa)
        {
            const string sql = @"
                SELECT
                    m.Id AS MaThuoc,
                    m.Name AS TenThuoc,
                    m.Unit AS DonViTinh,
                    m.Price AS DonGia,
                    m.IsPrescription AS KeDon,
                    ISNULL(SUM(b.Stock), 0) AS TonKho
                FROM Medicines m
                LEFT JOIN Batches b
                       ON b.MedicineId = m.Id
                      AND b.Stock > 0
                      AND b.ExpiryDate >= CAST(GETDATE() AS DATE)
                WHERE m.IsActive = 1
                  AND ( m.Name LIKE @kw
                     OR m.BarCode LIKE @kw
                     OR m.ActiveIngredient LIKE @kw
                     OR CAST(m.Id AS NVARCHAR(20)) LIKE @kw )
                GROUP BY m.Id, m.Name, m.Unit, m.Price, m.IsPrescription
                ORDER BY m.Name";

            using var conn = new SqlConnection(DBConnection.connectionString);
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@kw", SqlDbType.NVarChar, 200).Value = "%" + tuKhoa + "%";

            DataTable dt = new DataTable();
            using var da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }

        // 2. Lấy tên + giá hiện hành của thuốc (thuốc còn bán)
        public DataTable LayThongTinThuoc(int maThuoc)
        {
            const string sql = "SELECT Name, Price FROM Medicines WHERE Id = @id AND IsActive = 1";

            using var conn = new SqlConnection(DBConnection.connectionString);
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = maThuoc;

            DataTable dt = new DataTable();
            using var da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }

        // 3. Tạo hóa đơn + chi tiết + TRỪ TỒN KHO (+ trừ lượt voucher) trong MỘT transaction.
        //    Trừ kho theo nguyên tắc FEFO: lô hết hạn sớm nhất được bán trước; một thuốc có thể tách ra nhiều lô.
        public bool TaoHoaDon(int employeeId, int? customerId, int? voucherId, decimal tongTien, decimal giamGia,
                              List<CartItem> items, out int orderId, out string error)
        {
            orderId = 0;
            error = "";

            using var conn = new SqlConnection(DBConnection.connectionString);
            try
            {
                conn.Open();
                using var tran = conn.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    decimal verifiedTotal = 0;
                    foreach (var item in items.OrderBy(x => x.MaThuoc))
                    {
                        using var priceCommand = new SqlCommand("SELECT Price FROM Medicines WITH (UPDLOCK, HOLDLOCK) WHERE Id=@id AND IsActive=1", conn, tran);
                        priceCommand.Parameters.Add("@id", SqlDbType.Int).Value = item.MaThuoc;
                        var value = priceCommand.ExecuteScalar();
                        if (value == null || value == DBNull.Value || TienTe.LamTron(Convert.ToDecimal(value)) != item.DonGia || item.SoLuong <= 0)
                            throw new InvalidOperationException("Giá hoặc trạng thái thuốc đã đổi. Vui lòng tải lại giỏ hàng.");
                        verifiedTotal += item.DonGia * item.SoLuong;
                    }
                    if (items.Count == 0 || verifiedTotal != tongTien || giamGia < 0 || giamGia > tongTien)
                        throw new InvalidOperationException("Tổng tiền không hợp lệ.");
                    decimal verifiedDiscount = 0;
                    if (voucherId.HasValue)
                    {
                        using var voucherCommand = new SqlCommand("SELECT DiscountType, DiscountValue FROM Vouchers WITH (UPDLOCK, HOLDLOCK) WHERE Id=@id AND IsActive=1 AND Quantity>0", conn, tran);
                        voucherCommand.Parameters.Add("@id", SqlDbType.Int).Value = voucherId.Value;
                        using var reader = voucherCommand.ExecuteReader();
                        if (!reader.Read()) throw new InvalidOperationException("Voucher đã hết lượt hoặc bị khóa.");
                        decimal value = reader.GetDecimal(1);
                        verifiedDiscount = reader.GetString(0).Trim().ToUpperInvariant() == "PERCENT"
                            ? TienTe.LamTron(tongTien * value / 100m) : TienTe.LamTron(value);
                        verifiedDiscount = Math.Clamp(verifiedDiscount, 0, tongTien);
                    }
                    if (verifiedDiscount != giamGia)
                        throw new InvalidOperationException("Giá trị voucher đã đổi. Vui lòng áp dụng lại.");

                    // 3.1 Trừ lượt voucher (điều kiện Quantity > 0 chống 2 máy dùng cùng lúc)
                    if (voucherId.HasValue)
                    {
                        using var cmdV = new SqlCommand(
                            "UPDATE Vouchers SET Quantity = Quantity - 1 WHERE Id = @id AND IsActive = 1 AND Quantity > 0",
                            conn, tran);
                        cmdV.Parameters.Add("@id", SqlDbType.Int).Value = voucherId.Value;
                        if (cmdV.ExecuteNonQuery() == 0)
                        {
                            tran.Rollback();
                            error = "Voucher đã hết lượt hoặc đang bị khóa!";
                            return false;
                        }
                    }

                    // 3.2 Tạo hóa đơn
                    using (var cmdO = new SqlCommand(@"
                        INSERT INTO Orders(EmployeeId, CustomerId, VoucherId, TotalAmount, DiscountAmount, Channel)
                        VALUES(@emp, @customer, @vid, @tong, @giam, N'POS');
                        SELECT CAST(SCOPE_IDENTITY() AS INT);", conn, tran))
                    {
                        cmdO.Parameters.Add("@customer", SqlDbType.Int).Value = (object?)customerId ?? DBNull.Value;
                        cmdO.Parameters.Add("@emp", SqlDbType.Int).Value = employeeId;
                        cmdO.Parameters.Add("@vid", SqlDbType.Int).Value =
                            voucherId.HasValue ? (object)voucherId.Value : DBNull.Value;
                        cmdO.Parameters.Add(TienParam("@tong", tongTien));
                        cmdO.Parameters.Add(TienParam("@giam", giamGia));
                        orderId = Convert.ToInt32(cmdO.ExecuteScalar());
                    }

                    // 3.3 Với từng thuốc: khóa các lô còn hạn, trừ dần tồn kho, ghi chi tiết đơn
                    foreach (CartItem item in items)
                    {
                        int canLay = item.SoLuong;
                        var cacLo = new List<(int Id, int Stock)>();

                        using (var cmdB = new SqlCommand(@"
                            SELECT Id, Stock
                            FROM Batches WITH (UPDLOCK, ROWLOCK)
                            WHERE MedicineId = @m AND Stock > 0
                              AND ExpiryDate >= CAST(GETDATE() AS DATE)
                            ORDER BY ExpiryDate, Id", conn, tran))
                        {
                            cmdB.Parameters.Add("@m", SqlDbType.Int).Value = item.MaThuoc;
                            using var rd = cmdB.ExecuteReader();
                            while (rd.Read())
                                cacLo.Add((rd.GetInt32(0), rd.GetInt32(1)));
                        }

                        foreach (var lo in cacLo)
                        {
                            if (canLay <= 0) break;
                            int lay = Math.Min(canLay, lo.Stock);

                            using (var cmdU = new SqlCommand(
                                "UPDATE Batches SET Stock = Stock - @q WHERE Id = @b AND Stock >= @q", conn, tran))
                            {
                                cmdU.Parameters.Add("@q", SqlDbType.Int).Value = lay;
                                cmdU.Parameters.Add("@b", SqlDbType.Int).Value = lo.Id;
                                if (cmdU.ExecuteNonQuery() == 0)
                                {
                                    tran.Rollback();
                                    error = $"Tồn kho của thuốc \"{item.TenThuoc}\" vừa thay đổi, vui lòng thử lại!";
                                    orderId = 0;
                                    return false;
                                }
                            }

                            using (var cmdD = new SqlCommand(@"
                                INSERT INTO OrderDetails(OrderId, BatchId, Quantity, UnitPrice)
                                VALUES(@o, @b, @q, @p)", conn, tran))
                            {
                                cmdD.Parameters.Add("@o", SqlDbType.Int).Value = orderId;
                                cmdD.Parameters.Add("@b", SqlDbType.Int).Value = lo.Id;
                                cmdD.Parameters.Add("@q", SqlDbType.Int).Value = lay;
                                cmdD.Parameters.Add(TienParam("@p", item.DonGia));
                                cmdD.ExecuteNonQuery();
                            }

                            canLay -= lay;
                        }

                        if (canLay > 0)
                        {
                            tran.Rollback();
                            error = $"Không đủ tồn kho (còn hạn) cho thuốc \"{item.TenThuoc}\"!";
                            orderId = 0;
                            return false;
                        }
                    }

                    tran.Commit();

                    DBConnection.NotifyDataChanged();
                    return true;
                }
                catch
                {
                    try { tran.Rollback(); } catch { }
                    throw;
                }
            }
            catch (Exception ex)
            {
                orderId = 0;
                error = ex.Message;
                return false;
            }
        }
    }
}
