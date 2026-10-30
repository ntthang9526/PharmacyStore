using System.Data;
using Store.DAL;
namespace Store.BLL;
public class HoaDonBLL
{
    private readonly HoaDonDAL dal=new();
    public DataTable TimKiem(string keyword,DateTime? from=null,DateTime? to=null,bool chiCho=false)
    {
        if(from.HasValue && to.HasValue && from.Value.Date>to.Value.Date)
            throw new ArgumentException("Từ ngày không được lớn hơn đến ngày.");
        if(to?.Date==DateTime.MaxValue.Date) throw new ArgumentException("Ngày kết thúc không hợp lệ.");
        return dal.TimKiem(keyword,from,to,chiCho);
    }
    public bool ThanhToanCho(int id,string method,decimal expectedPayable,out string error,byte[]? expectedVersion=null)
    {
        if(id<=0) {error="Chọn hóa đơn chờ thanh toán.";return false;}
        return new BanHangDAL().ThanhToanCho(id,method,expectedPayable,out error,expectedVersion);
    }
    public DataSet LayDonCho(int id)=>dal.LayDonCho(id);
    public DataSet LayBanIn(int id)
    {
        if(id<=0) throw new ArgumentException("Chọn hóa đơn để in.");
        return dal.LayBanIn(id);
    }
    public static Store.Shared.DonThuocInfo? DocDonThuoc(DataRow row)
    {
        if(row.IsNull("PrescriptionNumber")) return null;
        return new Store.Shared.DonThuocInfo {SoDon=Convert.ToString(row["PrescriptionNumber"])??"",BenhNhan=Convert.ToString(row["PatientName"])??"",
            NguoiKeDon=Convert.ToString(row["Prescriber"])??"",NgayKeDon=row.IsNull("PrescriptionDate")?DateTime.MinValue:(DateTime)row["PrescriptionDate"],DaKiemTra=!row.IsNull("PrescriptionCheckedBy")};
    }
    public bool KiemTraDonThuocCho(int id,out string error)
    {
        error="";
        try
        {
            var data=dal.LayDonCho(id);
            var rx=data.Tables[1].Rows.Cast<DataRow>().Where(r=>Convert.ToBoolean(r["KeDon"])).ToArray();
            if(rx.Length==0) return true;
            var info=DocDonThuoc(data.Tables[0].Rows[0]);
            error=info==null?"Chưa có thông tin đơn thuốc.":info.KiemTra()??"";
            if(error.Length==0 && rx.Any(r=>string.IsNullOrWhiteSpace(Convert.ToString(r["HuongDan"])))) error="Chưa có hướng dẫn dùng cho thuốc kê đơn.";
            if(error.Length>0) error+=" Chọn Sửa đơn → Đơn thuốc để bổ sung trước khi thu tiền.";
            return error.Length==0;
        }
        catch(Exception ex) {error=ex.Message;return false;}
    }
    public bool HuyHoaDonCho(int id,byte[] version,string reason,out string error)
        =>new BanHangDAL().HuyHoaDonCho(id,version,reason,out error);
    public DataTable LayGanNhat()=>dal.LayGanNhat();
    public DataTable ThongKeHomNay()=>dal.ThongKeHomNay();
    public DataTable ChiTiet(int id)
    {
        if(id<=0) throw new ArgumentException("Mã hóa đơn không hợp lệ.");
        return dal.ChiTiet(id);
    }
}
