using System.Data;
using Store.DAL;
namespace Store.BLL;
public class LoHangBLL
{
    public DataTable TimKiem(string keyword) => new LoHangDAL().TimKiem(keyword);
}
