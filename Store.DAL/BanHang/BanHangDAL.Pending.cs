using System.Data;
using Microsoft.Data.SqlClient;
using Store.Shared;
namespace Store.DAL;

public partial class BanHangDAL
{
    private static void KiemTraDonCho(SqlConnection conn,SqlTransaction tran,int id,byte[] expectedVersion)
    {
        if(id<=0 || expectedVersion==null || expectedVersion.Length!=8)
            throw new ArgumentException("Hãy tải lại hóa đơn trước khi thao tác.");
        using var cmd=new SqlCommand("SELECT PaymentStatus,OrderVersion FROM Orders WITH (UPDLOCK,HOLDLOCK) WHERE Id=@id",conn,tran);
        cmd.Parameters.Add("@id",SqlDbType.Int).Value=id;
        using var reader=cmd.ExecuteReader();
        if(!reader.Read()) throw new InvalidOperationException("Hóa đơn không còn tồn tại.");
        if(reader.GetString(0)!="PENDING") throw new InvalidOperationException("Chỉ được sửa/hủy hóa đơn chưa thanh toán.");
        if(!((byte[])reader[1]).SequenceEqual(expectedVersion))
            throw new InvalidOperationException("Hóa đơn đã được thay đổi. Hãy đóng màn hình sửa và tải lại hóa đơn.");
    }

    public bool SuaHoaDonCho(int id,byte[] expectedVersion,int employeeId,int? customerId,int? voucherId,
        decimal total,decimal discount,List<CartItem> items,out string error,DonThuocInfo? donThuoc=null)
    {
        error="";
        try
        {
            using var conn=DBConnection.GetDbConnection();
            using var tran=conn.BeginTransaction(IsolationLevel.Serializable);
            var actor=PhanQuyenDAL.YeuCau(Quyen.BanHang,conn,tran,employeeId);
            KiemTraDonCho(conn,tran,id,expectedVersion);
            var prescriptionIds=new HashSet<int>();
            using(var old=new SqlCommand("SELECT MedicineId FROM OrderPendingItems WHERE OrderId=@id AND IsPrescriptionSnapshot=1",conn,tran))
            {
                old.Parameters.Add("@id",SqlDbType.Int).Value=id;
                using var reader=old.ExecuteReader();while(reader.Read()) prescriptionIds.Add(reader.GetInt32(0));
            }
            foreach(var item in items) item.KeDon |= prescriptionIds.Contains(item.MaThuoc);
            var snapshots=KiemTraThuoc(conn,tran,items,total,true);
            KiemTraDonThuoc(items,donThuoc,false);
            KiemTraVoucher(conn,tran,voucherId,total,discount,false);
            using(var cmd=new SqlCommand(@"UPDATE Orders SET CustomerId=@customer,VoucherId=@voucher,TotalAmount=@total,
                DiscountAmount=@discount,PaymentMethod=N'Chưa xác định',ModifiedAt=SYSDATETIME(),ModifiedEmployeeId=@emp,ModifiedEmployeeNameSnapshot=@actorName,
                CustomerNameSnapshot=COALESCE((SELECT FullName FROM Customers WHERE Id=@customer),N'Khách lẻ'),
                CustomerPhoneSnapshot=COALESCE((SELECT Phone FROM Customers WHERE Id=@customer),''),
                VoucherCodeSnapshot=COALESCE((SELECT Code FROM Vouchers WHERE Id=@voucher),'') WHERE Id=@id;
                DELETE FROM OrderPendingItems WHERE OrderId=@id;",conn,tran))
            {
                cmd.Parameters.Add("@id",SqlDbType.Int).Value=id;
                cmd.Parameters.Add("@customer",SqlDbType.Int).Value=(object?)customerId??DBNull.Value;
                cmd.Parameters.Add("@voucher",SqlDbType.Int).Value=(object?)voucherId??DBNull.Value;
                cmd.Parameters.Add("@emp",SqlDbType.Int).Value=employeeId;
                cmd.Parameters.Add("@actorName",SqlDbType.NVarChar,100).Value=actor.HoTen;
                cmd.Parameters.Add(TienParam("@total",total));cmd.Parameters.Add(TienParam("@discount",discount));
                cmd.ExecuteNonQuery();
            }
            LuuDonThuoc(conn,tran,id,donThuoc,employeeId);
            foreach(var item in items)
            {
                using var cmd=new SqlCommand(@"INSERT INTO OrderPendingItems(OrderId,MedicineId,Quantity,UnitPrice,MedicineNameSnapshot,MedicineUnitSnapshot,DosageInstruction,IsPrescriptionSnapshot)
                    VALUES(@o,@m,@q,@p,@name,@unit,@dosage,@rx)",conn,tran);
                cmd.Parameters.Add("@o",SqlDbType.Int).Value=id;cmd.Parameters.Add("@m",SqlDbType.Int).Value=item.MaThuoc;
                cmd.Parameters.Add("@q",SqlDbType.Int).Value=item.SoLuong;cmd.Parameters.Add(TienParam("@p",item.DonGia));
                cmd.Parameters.Add("@name",SqlDbType.NVarChar,150).Value=(object?)snapshots[item.MaThuoc].Name??DBNull.Value;
                cmd.Parameters.Add("@unit",SqlDbType.NVarChar,30).Value=(object?)snapshots[item.MaThuoc].Unit??DBNull.Value;
                ThemHuongDan(cmd,item);
                cmd.ExecuteNonQuery();
            }
            tran.Commit();DBConnection.NotifyDataChanged();return true;
        }
        catch(Exception ex) {error=ex.Message;return false;}
    }

    public bool HuyHoaDonCho(int id,byte[] expectedVersion,string reason,out string error)
    {
        error="";
        reason=(reason??"").Trim();
        if(reason.Length==0 || reason.Length>250) {error="Nhập lý do hủy từ 1 đến 250 ký tự.";return false;}
        try
        {
            using var conn=DBConnection.GetDbConnection();
            using var tran=conn.BeginTransaction(IsolationLevel.Serializable);
            var actor=PhanQuyenDAL.YeuCau(Quyen.BanHang,conn,tran);
            KiemTraDonCho(conn,tran,id,expectedVersion);
            using var cmd=new SqlCommand(@"UPDATE Orders SET PaymentStatus='CANCELLED',CancelReason=@reason,
                CancelledAt=SYSDATETIME(),CancelledEmployeeId=@emp,CancelledEmployeeNameSnapshot=@actorName WHERE Id=@id",conn,tran);
            cmd.Parameters.Add("@id",SqlDbType.Int).Value=id;
            cmd.Parameters.Add("@reason",SqlDbType.NVarChar,250).Value=reason;
            cmd.Parameters.Add("@emp",SqlDbType.Int).Value=PhanQuyenDAL.NhanVienId;
            cmd.Parameters.Add("@actorName",SqlDbType.NVarChar,100).Value=actor.HoTen;
            cmd.ExecuteNonQuery();
            tran.Commit();DBConnection.NotifyDataChanged();return true;
        }
        catch(Exception ex) {error=ex.Message;return false;}
    }
}

