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
    public bool ThanhToanCho(int id,string method,decimal expectedPayable,out string error)
    {
        if(id<=0) {error="Chọn hóa đơn chờ thanh toán.";return false;}
        return new BanHangDAL().ThanhToanCho(id,method,expectedPayable,out error);
    }
    public DataTable LayGanNhat()=>dal.LayGanNhat();
    public DataTable ThongKeHomNay()=>dal.ThongKeHomNay();
    public DataTable ChiTiet(int id)
    {
        if(id<=0) throw new ArgumentException("Mã hóa đơn không hợp lệ.");
        return dal.ChiTiet(id);
    }
}
