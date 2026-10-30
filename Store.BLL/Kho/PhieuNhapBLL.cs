using System.Data;
using Store.DAL;
namespace Store.BLL;
public class PhieuNhapBLL
{
    private readonly PhieuNhapDAL dal=new();
    public DataTable TimKiem(string keyword,DateTime? from,DateTime? to)
    {
        if(from?.Date>to?.Date) throw new ArgumentException("Ngày bắt đầu phải trước hoặc bằng ngày kết thúc.");
        if(to?.Date>=DateTime.MaxValue.Date) throw new ArgumentException("Ngày kết thúc không hợp lệ.");
        return dal.TimKiem(keyword,from,to);
    }
    public DataTable ChiTiet(int id) => dal.ChiTiet(id);
}
