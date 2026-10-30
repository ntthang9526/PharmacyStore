using System.Data;
using Microsoft.Data.SqlClient;
using Store.Shared;
namespace Store.DAL;

public partial class BanHangDAL
{
    private static void KiemTraDonThuoc(List<CartItem> items, DonThuocInfo? donThuoc, bool thanhToan)
    {
        foreach(var item in items)
        {
            item.HuongDan=(item.HuongDan??"").Trim();
            if(item.HuongDan.Length>500) throw new ArgumentException("Hướng dẫn dùng tối đa 500 ký tự cho mỗi thuốc.");
            if(thanhToan && item.KeDon && item.HuongDan.Length==0)
                throw new InvalidOperationException($"Nhập hướng dẫn sử dụng cho thuốc kê đơn {item.TenThuoc}. Chọn Sửa đơn để bổ sung nếu là hóa đơn chờ.");
        }
        if(donThuoc!=null && donThuoc.KiemTra() is string error) throw new InvalidOperationException(error);
        if(thanhToan && items.Any(i=>i.KeDon) && donThuoc==null)
            throw new InvalidOperationException("Hóa đơn có thuốc kê đơn. Chọn Sửa đơn để bổ sung và xác nhận thông tin đơn thuốc trước khi thanh toán.");
    }

    private static void LuuDonThuoc(SqlConnection conn,SqlTransaction tran,int orderId,DonThuocInfo? info,int employeeId)
    {
        using var cmd=new SqlCommand(@"UPDATE Orders SET PrescriptionNumber=@number,PatientName=@patient,Prescriber=@doctor,
            PrescriptionDate=@date,PrescriptionCheckedBy=@employee,PrescriptionCheckedAt=CASE WHEN @employee IS NULL THEN NULL ELSE SYSDATETIME() END WHERE Id=@id",conn,tran);
        cmd.Parameters.Add("@id",SqlDbType.Int).Value=orderId;
        cmd.Parameters.Add("@number",SqlDbType.NVarChar,100).Value=(object?)info?.SoDon.Trim()??DBNull.Value;
        cmd.Parameters.Add("@patient",SqlDbType.NVarChar,150).Value=(object?)info?.BenhNhan.Trim()??DBNull.Value;
        cmd.Parameters.Add("@doctor",SqlDbType.NVarChar,150).Value=(object?)info?.NguoiKeDon.Trim()??DBNull.Value;
        cmd.Parameters.Add("@date",SqlDbType.Date).Value=(object?)info?.NgayKeDon.Date??DBNull.Value;
        cmd.Parameters.Add("@employee",SqlDbType.Int).Value=info==null?DBNull.Value:employeeId;
        cmd.ExecuteNonQuery();
    }

    private static DonThuocInfo? DocDonThuoc(SqlConnection conn,SqlTransaction tran,int id)
    {
        using var cmd=new SqlCommand("SELECT PrescriptionNumber,PatientName,Prescriber,PrescriptionDate,PrescriptionCheckedBy FROM Orders WHERE Id=@id",conn,tran);
        cmd.Parameters.Add("@id",SqlDbType.Int).Value=id;
        using var reader=cmd.ExecuteReader();
        if(!reader.Read() || reader.IsDBNull(0)) return null;
        return new DonThuocInfo {SoDon=reader.GetString(0),BenhNhan=reader.IsDBNull(1)?"":reader.GetString(1),
            NguoiKeDon=reader.IsDBNull(2)?"":reader.GetString(2),NgayKeDon=reader.IsDBNull(3)?DateTime.MinValue:reader.GetDateTime(3),DaKiemTra=!reader.IsDBNull(4)};
    }

    private static void ThemHuongDan(SqlCommand cmd,CartItem item)
    {
        cmd.Parameters.Add("@dosage",SqlDbType.NVarChar,500).Value=item.HuongDan;
        cmd.Parameters.Add("@rx",SqlDbType.Bit).Value=item.KeDon;
    }
}
