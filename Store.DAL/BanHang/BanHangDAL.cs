using System.Data;
using Microsoft.Data.SqlClient;
using Store.Shared;

namespace Store.DAL
{
    public partial class BanHangDAL
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
                      AND b.Stock > 0 AND b.IsBlocked=0
                      AND b.ExpiryDate >= CAST(GETDATE() AS DATE)
                WHERE m.IsActive = 1 AND m.IsSelling=1
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
            const string sql = "SELECT Name, Price FROM Medicines WHERE Id = @id AND IsActive = 1 AND IsSelling=1";

            using var conn = new SqlConnection(DBConnection.connectionString);
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = maThuoc;

            DataTable dt = new DataTable();
            using var da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }

        public bool TaoHoaDon(int employeeId,int? customerId,int? voucherId,decimal tongTien,decimal giamGia,
            List<CartItem> items,out int orderId,out string error,string paymentMethod="Tiền mặt",bool choThanhToan=false,DonThuocInfo? donThuoc=null)
        {
            orderId=0;error="";
            try
            {
                KiemTraPhuongThuc(paymentMethod,choThanhToan);
                using var conn=DBConnection.GetDbConnection();using var tran=conn.BeginTransaction(IsolationLevel.Serializable);
                var actor=PhanQuyenDAL.YeuCau(Quyen.BanHang,conn,tran,employeeId);
                var snapshots=KiemTraThuoc(conn,tran,items,tongTien,true);
                KiemTraDonThuoc(items,donThuoc,!choThanhToan);
                KiemTraVoucher(conn,tran,voucherId,tongTien,giamGia,!choThanhToan);
                using(var cmd=new SqlCommand(@"
                    INSERT INTO Orders(EmployeeId,CustomerId,VoucherId,TotalAmount,DiscountAmount,Channel,PaymentMethod,PaymentStatus,PaidAt,PaidEmployeeId,CustomerNameSnapshot,CustomerPhoneSnapshot,EmployeeNameSnapshot,PaidEmployeeNameSnapshot,VoucherCodeSnapshot)
                    OUTPUT INSERTED.Id VALUES(@emp,@customer,@voucher,@total,@discount,N'POS',@method,@status,
                        CASE WHEN @status='PAID' THEN SYSDATETIME() ELSE NULL END,
                        CASE WHEN @status='PAID' THEN @emp ELSE NULL END,
                        COALESCE((SELECT FullName FROM Customers WHERE Id=@customer),N'Khách lẻ'),
                        COALESCE((SELECT Phone FROM Customers WHERE Id=@customer),''),@actorName,
                        CASE WHEN @status='PAID' THEN @actorName ELSE NULL END,COALESCE((SELECT Code FROM Vouchers WHERE Id=@voucher),''))",conn,tran))
                {
                    cmd.Parameters.Add("@emp",SqlDbType.Int).Value=employeeId;
                    cmd.Parameters.Add("@actorName",SqlDbType.NVarChar,100).Value=actor.HoTen;
                    cmd.Parameters.Add("@customer",SqlDbType.Int).Value=(object?)customerId??DBNull.Value;
                    cmd.Parameters.Add("@voucher",SqlDbType.Int).Value=(object?)voucherId??DBNull.Value;
                    cmd.Parameters.Add(TienParam("@total",tongTien));cmd.Parameters.Add(TienParam("@discount",giamGia));
                    cmd.Parameters.Add("@method",SqlDbType.NVarChar,50).Value=paymentMethod;
                    cmd.Parameters.Add("@status",SqlDbType.VarChar,10).Value=choThanhToan?"PENDING":"PAID";
                    orderId=Convert.ToInt32(cmd.ExecuteScalar());
                }
                LuuDonThuoc(conn,tran,orderId,donThuoc,employeeId);
                if(choThanhToan)
                {
                    foreach(var item in items)
                    {
                        using var cmd=new SqlCommand("INSERT INTO OrderPendingItems(OrderId,MedicineId,Quantity,UnitPrice,MedicineNameSnapshot,MedicineUnitSnapshot,DosageInstruction,IsPrescriptionSnapshot) VALUES(@o,@m,@q,@p,@name,@unit,@dosage,@rx)",conn,tran);
                        cmd.Parameters.Add("@o",SqlDbType.Int).Value=orderId;cmd.Parameters.Add("@m",SqlDbType.Int).Value=item.MaThuoc;
                        cmd.Parameters.Add("@q",SqlDbType.Int).Value=item.SoLuong;cmd.Parameters.Add(TienParam("@p",item.DonGia));
                        cmd.Parameters.Add("@name",SqlDbType.NVarChar,150).Value=(object?)snapshots[item.MaThuoc].Name??DBNull.Value;
                        cmd.Parameters.Add("@unit",SqlDbType.NVarChar,30).Value=(object?)snapshots[item.MaThuoc].Unit??DBNull.Value;
                        ThemHuongDan(cmd,item);
                        cmd.ExecuteNonQuery();
                    }
                }
                else TruKho(conn,tran,orderId,items,snapshots);
                tran.Commit();DBConnection.NotifyDataChanged();return true;
            }
            catch(Exception ex) {orderId=0;error=ex.Message;return false;}
        }

        public bool ThanhToanCho(int orderId,string paymentMethod,decimal expectedPayable,out string error,byte[]? expectedVersion=null)
        {
            error="";
            try
            {
                KiemTraPhuongThuc(paymentMethod,false);
                using var conn=DBConnection.GetDbConnection();using var tran=conn.BeginTransaction(IsolationLevel.Serializable);
                var actor=PhanQuyenDAL.YeuCau(Quyen.BanHang,conn,tran);
                if(expectedVersion!=null) KiemTraDonCho(conn,tran,orderId,expectedVersion);
                int? voucherId;decimal total,discount;
                using(var cmd=new SqlCommand("SELECT PaymentStatus,VoucherId,TotalAmount,DiscountAmount FROM Orders WITH (UPDLOCK,HOLDLOCK) WHERE Id=@id",conn,tran))
                {
                    cmd.Parameters.Add("@id",SqlDbType.Int).Value=orderId;using var reader=cmd.ExecuteReader();
                    if(!reader.Read()) throw new InvalidOperationException("Hóa đơn không còn tồn tại.");
                    if(reader.GetString(0)!="PENDING") throw new InvalidOperationException("Hóa đơn đã thanh toán hoặc đã hủy. Không thể thanh toán.");
                    voucherId=reader.IsDBNull(1)?null:reader.GetInt32(1);total=reader.GetDecimal(2);discount=reader.GetDecimal(3);
                }
                if(total-discount!=expectedPayable) throw new InvalidOperationException("Số tiền hóa đơn đã đổi. Hãy tải lại trước khi xác nhận.");
                var items=new List<CartItem>();
                var snapshots=new Dictionary<int,(string? Name,string? Unit)>();
                using(var cmd=new SqlCommand(@"SELECT d.MedicineId,COALESCE(d.MedicineNameSnapshot,m.Name),d.Quantity,d.UnitPrice,d.MedicineNameSnapshot,d.MedicineUnitSnapshot,COALESCE(d.DosageInstruction,''),COALESCE(d.IsPrescriptionSnapshot,m.IsPrescription) FROM OrderPendingItems d
                    JOIN Medicines m ON m.Id=d.MedicineId WHERE d.OrderId=@id ORDER BY d.MedicineId",conn,tran))
                {
                    cmd.Parameters.Add("@id",SqlDbType.Int).Value=orderId;using var reader=cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        int medicineId=reader.GetInt32(0);
                        items.Add(new CartItem{MaThuoc=medicineId,TenThuoc=reader.GetString(1),SoLuong=reader.GetInt32(2),DonGia=reader.GetDecimal(3),HuongDan=reader.GetString(6),KeDon=reader.GetBoolean(7)});
                        snapshots.Add(medicineId,(reader.IsDBNull(4)?null:reader.GetString(4),reader.IsDBNull(5)?null:reader.GetString(5)));
                    }
                }
                // Giá đã chốt khi lập hóa đơn chờ, không sửa lại theo giá danh mục mới.
                KiemTraThuoc(conn,tran,items,total,false);
                KiemTraDonThuoc(items,DocDonThuoc(conn,tran,orderId),true);
                KiemTraVoucher(conn,tran,voucherId,total,discount,true);
                TruKho(conn,tran,orderId,items,snapshots);
                using(var cmd=new SqlCommand(@"UPDATE Orders SET PaymentStatus='PAID',PaymentMethod=@method,PaidAt=SYSDATETIME(),PaidEmployeeId=@emp,PaidEmployeeNameSnapshot=@actorName,
                    CustomerNameSnapshot=COALESCE(CustomerNameSnapshot,(SELECT FullName FROM Customers WHERE Id=Orders.CustomerId),N'Khách lẻ'),
                    CustomerPhoneSnapshot=COALESCE(CustomerPhoneSnapshot,(SELECT Phone FROM Customers WHERE Id=Orders.CustomerId),''),
                    VoucherCodeSnapshot=COALESCE(VoucherCodeSnapshot,(SELECT Code FROM Vouchers WHERE Id=Orders.VoucherId),'') WHERE Id=@id;
                    DELETE FROM OrderPendingItems WHERE OrderId=@id",conn,tran))
                {
                    cmd.Parameters.Add("@id",SqlDbType.Int).Value=orderId;cmd.Parameters.Add("@emp",SqlDbType.Int).Value=PhanQuyenDAL.NhanVienId;
                    cmd.Parameters.Add("@actorName",SqlDbType.NVarChar,100).Value=actor.HoTen;
                    cmd.Parameters.Add("@method",SqlDbType.NVarChar,50).Value=paymentMethod;cmd.ExecuteNonQuery();
                }
                tran.Commit();DBConnection.NotifyDataChanged();return true;
            }
            catch(Exception ex) {error=ex.Message;return false;}
        }
        private static void KiemTraPhuongThuc(string method,bool pending)
        {
            if(method is not ("Tiền mặt" or "Chuyển khoản QR") && !(pending && method=="Chưa xác định"))
                throw new ArgumentException("Hình thức thanh toán không hợp lệ.");
        }
        private static Dictionary<int,(string? Name,string? Unit)> KiemTraThuoc(SqlConnection conn,SqlTransaction tran,List<CartItem> items,decimal total,bool currentPrice)
        {
            if(items.Count==0 || items.Select(x=>x.MaThuoc).Distinct().Count()!=items.Count)
                throw new InvalidOperationException("Chi tiết hóa đơn trống hoặc trùng thuốc.");
            var snapshots=new Dictionary<int,(string? Name,string? Unit)>();
            decimal sum=0;
            foreach(var item in items.OrderBy(x=>x.MaThuoc))
            {
                using var cmd=new SqlCommand("SELECT Price,Name,Unit,IsPrescription FROM Medicines WITH (UPDLOCK,HOLDLOCK) WHERE Id=@id AND IsActive=1 AND IsSelling=1",conn,tran);
                cmd.Parameters.Add("@id",SqlDbType.Int).Value=item.MaThuoc;using var reader=cmd.ExecuteReader();
                if(!reader.Read() || item.SoLuong<=0 || item.DonGia<0 || item.DonGia!=TienTe.LamTron(item.DonGia) || currentPrice && TienTe.LamTron(reader.GetDecimal(0))!=item.DonGia)
                    throw new InvalidOperationException("Giá hoặc trạng thái thuốc đã đổi. Vui lòng kiểm tra lại hóa đơn.");
                item.KeDon |= reader.GetBoolean(3);
                snapshots.Add(item.MaThuoc,(reader.GetString(1),reader.GetString(2)));
                sum+=item.DonGia*item.SoLuong;
            }
            if(sum!=total) throw new InvalidOperationException("Tổng tiền không khớp chi tiết hóa đơn.");
            return snapshots;
        }
        private static void KiemTraVoucher(SqlConnection conn,SqlTransaction tran,int? voucherId,decimal total,decimal discount,bool consume)
        {
            decimal verified=0;
            if(voucherId.HasValue)
            {
                using(var cmd=new SqlCommand("SELECT DiscountType,DiscountValue FROM Vouchers WITH (UPDLOCK,HOLDLOCK) WHERE Id=@id AND IsActive=1 AND Quantity>0",conn,tran))
                {
                    cmd.Parameters.Add("@id",SqlDbType.Int).Value=voucherId.Value;using var reader=cmd.ExecuteReader();
                    if(!reader.Read()) throw new InvalidOperationException("Voucher đã hết lượt hoặc bị khóa. Chưa thể thanh toán hóa đơn này.");
                    verified=reader.GetString(0).Trim().ToUpperInvariant()=="PERCENT"?TienTe.LamTron(total*reader.GetDecimal(1)/100m):TienTe.LamTron(reader.GetDecimal(1));
                    verified=Math.Clamp(verified,0,total);
                }
            }
            if(discount!=verified || discount<0 || discount>total) throw new InvalidOperationException("Giá trị voucher đã đổi. Hãy kiểm tra lại hóa đơn.");
            if(consume && voucherId.HasValue)
            {
                using var cmd=new SqlCommand("UPDATE Vouchers SET Quantity=Quantity-1 WHERE Id=@id AND IsActive=1 AND Quantity>0",conn,tran);
                cmd.Parameters.Add("@id",SqlDbType.Int).Value=voucherId.Value;
                if(cmd.ExecuteNonQuery()!=1) throw new InvalidOperationException("Voucher vừa hết lượt. Vui lòng thử lại.");
            }
        }
        private static void TruKho(SqlConnection conn,SqlTransaction tran,int orderId,List<CartItem> items,Dictionary<int,(string? Name,string? Unit)> snapshots)
        {
            foreach(var item in items.OrderBy(x=>x.MaThuoc))
            {
                int remaining=item.SoLuong;var batches=new List<(int Id,int Stock)>();
                using(var cmd=new SqlCommand(@"SELECT Id,Stock FROM Batches WITH (UPDLOCK,HOLDLOCK) WHERE MedicineId=@m AND Stock>0 AND IsBlocked=0
                    AND ExpiryDate>=CAST(GETDATE() AS date) ORDER BY ExpiryDate,Id",conn,tran))
                {
                    cmd.Parameters.Add("@m",SqlDbType.Int).Value=item.MaThuoc;using var reader=cmd.ExecuteReader();
                    while(reader.Read()) batches.Add((reader.GetInt32(0),reader.GetInt32(1)));
                }
                foreach(var batch in batches)
                {
                    if(remaining<=0) break;int quantity=Math.Min(remaining,batch.Stock);
                    using(var cmd=new SqlCommand("UPDATE Batches SET Stock=Stock-@q WHERE Id=@id AND Stock>=@q AND IsBlocked=0",conn,tran))
                    {
                        cmd.Parameters.Add("@id",SqlDbType.Int).Value=batch.Id;cmd.Parameters.Add("@q",SqlDbType.Int).Value=quantity;
                        if(cmd.ExecuteNonQuery()!=1) throw new InvalidOperationException("Tồn kho vừa thay đổi. Vui lòng thử lại.");
                    }
                    using(var cmd=new SqlCommand("INSERT INTO OrderDetails(OrderId,BatchId,Quantity,UnitPrice,MedicineNameSnapshot,MedicineUnitSnapshot,DosageInstruction,IsPrescriptionSnapshot) VALUES(@o,@b,@q,@p,@name,@unit,@dosage,@rx)",conn,tran))
                    {
                        cmd.Parameters.Add("@o",SqlDbType.Int).Value=orderId;cmd.Parameters.Add("@b",SqlDbType.Int).Value=batch.Id;
                        cmd.Parameters.Add("@q",SqlDbType.Int).Value=quantity;cmd.Parameters.Add(TienParam("@p",item.DonGia));
                        cmd.Parameters.Add("@name",SqlDbType.NVarChar,150).Value=(object?)snapshots[item.MaThuoc].Name??DBNull.Value;
                        cmd.Parameters.Add("@unit",SqlDbType.NVarChar,30).Value=(object?)snapshots[item.MaThuoc].Unit??DBNull.Value;
                        ThemHuongDan(cmd,item);
                        cmd.ExecuteNonQuery();
                    }
                    remaining-=quantity;
                }
                if(remaining>0) throw new InvalidOperationException($"Không đủ tồn kho còn hạn, được phép bán cho thuốc {item.TenThuoc}. Hóa đơn vẫn chờ thanh toán.");
            }
        }
    }
}
