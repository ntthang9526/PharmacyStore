using System.Data;
using Microsoft.Data.SqlClient;
using Store.Shared;

namespace Store.DAL
{
    public class NhapKhoDAL
    {
        private static SqlParameter TienParam(string ten, decimal giaTri)
        {
            return new SqlParameter(ten, SqlDbType.Decimal) { Precision = 18, Scale = 2, Value = giaTri };
        }

        // Thuốc đang kinh doanh của một nhà cung cấp
        public DataTable LayThuocTheoNCC(int maNCC)
        {
            const string sql = @"
                SELECT Id AS MaThuoc, Name AS TenThuoc, Unit AS DonViTinh
                FROM Medicines
                WHERE SupplierId = @s AND IsActive = 1
                ORDER BY Name";

            using var conn = new SqlConnection(DBConnection.connectionString);
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@s", SqlDbType.Int).Value = maNCC;

            DataTable dt = new DataTable();
            using var da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }

        // Lưu phiếu nhập + CỘNG DỒN tồn kho trong một transaction.
        // Cùng thuốc + số lô + hạn dùng đã có -> cộng thêm vào Stock của lô đó; chưa có -> tạo lô mới.
        public bool TaoPhieuNhap(int employeeId, int supplierId, string? ghiChu, decimal tongVon,
                                 List<NhapKhoItem> items, out int stockInId, out string error)
        {
            stockInId = 0;
            error = "";

            using var conn = new SqlConnection(DBConnection.connectionString);
            try
            {
                conn.Open();
                using var tran = conn.BeginTransaction();
                try
                {
                    // 1. Phiếu nhập
                    using (var cmdP = new SqlCommand(@"
                        INSERT INTO StockIns(SupplierId, EmployeeId, TotalCost, Note)
                        VALUES(@s, @e, @t, @n);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);", conn, tran))
                    {
                        cmdP.Parameters.Add("@s", SqlDbType.Int).Value = supplierId;
                        cmdP.Parameters.Add("@e", SqlDbType.Int).Value = employeeId;
                        cmdP.Parameters.Add(TienParam("@t", tongVon));
                        cmdP.Parameters.Add("@n", SqlDbType.NVarChar, 250).Value =
                            string.IsNullOrWhiteSpace(ghiChu) ? DBNull.Value : (object)ghiChu;
                        stockInId = Convert.ToInt32(cmdP.ExecuteScalar());
                    }

                    // 2. Từng dòng: cộng dồn vào lô hoặc tạo lô mới
                    foreach (NhapKhoItem it in items)
                    {
                        int batchId;

                        using (var cmdF = new SqlCommand(@"
                            SELECT Id FROM Batches WITH (UPDLOCK, ROWLOCK)
                            WHERE MedicineId = @m AND BatchNumber = @n AND ExpiryDate = @d", conn, tran))
                        {
                            cmdF.Parameters.Add("@m", SqlDbType.Int).Value = it.MaThuoc;
                            cmdF.Parameters.Add("@n", SqlDbType.VarChar, 50).Value = it.SoLo;
                            cmdF.Parameters.Add("@d", SqlDbType.Date).Value = it.HanDung.Date;
                            object? found = cmdF.ExecuteScalar();

                            if (found != null && found != DBNull.Value)
                            {
                                batchId = Convert.ToInt32(found);
                                using var cmdU = new SqlCommand(
                                    "UPDATE Batches SET Stock = Stock + @q WHERE Id = @b", conn, tran);
                                cmdU.Parameters.Add("@q", SqlDbType.Int).Value = it.SoLuong;
                                cmdU.Parameters.Add("@b", SqlDbType.Int).Value = batchId;
                                cmdU.ExecuteNonQuery();
                            }
                            else
                            {
                                using var cmdI = new SqlCommand(@"
                                    INSERT INTO Batches(MedicineId, BatchNumber, ExpiryDate, Stock)
                                    VALUES(@m, @n, @d, @q);
                                    SELECT CAST(SCOPE_IDENTITY() AS INT);", conn, tran);
                                cmdI.Parameters.Add("@m", SqlDbType.Int).Value = it.MaThuoc;
                                cmdI.Parameters.Add("@n", SqlDbType.VarChar, 50).Value = it.SoLo;
                                cmdI.Parameters.Add("@d", SqlDbType.Date).Value = it.HanDung.Date;
                                cmdI.Parameters.Add("@q", SqlDbType.Int).Value = it.SoLuong;
                                batchId = Convert.ToInt32(cmdI.ExecuteScalar());
                            }
                        }

                        using (var cmdD = new SqlCommand(@"
                            INSERT INTO StockInDetails(StockInId, BatchId, Quantity, UnitCost)
                            VALUES(@p, @b, @q, @c)", conn, tran))
                        {
                            cmdD.Parameters.Add("@p", SqlDbType.Int).Value = stockInId;
                            cmdD.Parameters.Add("@b", SqlDbType.Int).Value = batchId;
                            cmdD.Parameters.Add("@q", SqlDbType.Int).Value = it.SoLuong;
                            cmdD.Parameters.Add(TienParam("@c", it.GiaNhap));
                            cmdD.ExecuteNonQuery();
                        }
                    }

                    tran.Commit();
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
                stockInId = 0;
                error = ex.Message;
                return false;
            }
        }
    }
}
