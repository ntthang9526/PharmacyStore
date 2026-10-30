using System.Data;
using Microsoft.Data.SqlClient;
namespace Store.DAL;
public partial class HoaDonDAL
{
    public DataSet LayBanIn(int id)
    {
        using var conn=DBConnection.GetDbConnection();using var tran=conn.BeginTransaction(IsolationLevel.Serializable);
        PhanQuyenDAL.YeuCau(Quyen.Xem,conn,tran);
        using var cmd=new SqlCommand(string.Format(SelectSql,"")+" WHERE o.Id=@id;"+DetailSql,conn,tran);
        cmd.Parameters.Add("@id",SqlDbType.Int).Value=id;
        using var adapter=new SqlDataAdapter(cmd);var data=new DataSet();adapter.Fill(data);
        if(data.Tables[0].Rows.Count!=1 || (string)data.Tables[0].Rows[0]["PaymentStatus"]!="PAID") throw new InvalidOperationException("Chỉ in hóa đơn đã thanh toán. Hãy tải lại danh sách hóa đơn.");
        if(data.Tables[1].Rows.Count==0) throw new InvalidOperationException("Hóa đơn không có chi tiết để in.");
        tran.Commit();return data;
    }
}
