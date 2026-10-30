using System.Data;
using Store.DAL;
namespace Store.BLL;
public class LoHangBLL
{
    public bool DoiTrangThai(int id,bool khoa,out string error) => new LoHangDAL().DoiTrangThai(id,khoa,out error);
    public DataTable TimKiem(string keyword) => new LoHangDAL().TimKiem(keyword);
}
