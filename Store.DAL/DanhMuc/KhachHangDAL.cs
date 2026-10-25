using System.Data;
using Microsoft.Data.SqlClient;
namespace Store.DAL;
public class KhachHangDAL
{
    private readonly DBConnection db = new();
    public DataTable GetAllKhachHang() => db.DocBang("SELECT Id AS MaKH, FullName AS TenKH, Phone AS DienThoai, Points AS Diem FROM Customers ORDER BY Id");
    public bool InsertKhachHang(string id,string tenKH,string dienThoai,string diem) => Luu(null,tenKH,dienThoai,int.Parse(diem));
    public bool UpdateKhachHang(string id,string tenKH,string dienThoai,string diem) => Luu(int.Parse(id),tenKH,dienThoai,int.Parse(diem));
    private static bool Luu(int? id,string name,string phone,int points)
    {
        using var connection=DBConnection.GetDbConnection();
        using var transaction=connection.BeginTransaction(IsolationLevel.Serializable);
        var actor=PhanQuyenDAL.YeuCau(Quyen.KhachHang,connection,transaction);
        if(actor.VaiTro!="ADMIN")
        {
            int oldPoints=0;
            if(id.HasValue)
            {
                using var get=new SqlCommand("SELECT Points FROM Customers WITH (UPDLOCK,HOLDLOCK) WHERE Id=@id",connection,transaction);
                get.Parameters.Add("@id",SqlDbType.Int).Value=id.Value;
                object? old=get.ExecuteScalar();
                if(old==null) throw new InvalidOperationException("Khách hàng không còn tồn tại.");
                oldPoints=Convert.ToInt32(old);
            }
            if(points!=oldPoints) throw new UnauthorizedAccessException("Chỉ Admin được điều chỉnh điểm khách hàng.");
        }
        using var cmd=new SqlCommand(id.HasValue?
            "UPDATE Customers SET FullName=@name,Phone=@phone,Points=@points WHERE Id=@id":
            "INSERT INTO Customers(FullName,Phone,Points) VALUES(@name,@phone,@points)",connection,transaction);
        cmd.Parameters.Add("@id",SqlDbType.Int).Value=(object?)id??DBNull.Value;
        cmd.Parameters.Add("@name",SqlDbType.NVarChar,100).Value=name.Trim();
        cmd.Parameters.Add("@phone",SqlDbType.VarChar,15).Value=phone.Trim();
        cmd.Parameters.Add("@points",SqlDbType.Int).Value=points;
        bool saved=cmd.ExecuteNonQuery()==1;transaction.Commit();
        if(saved) DBConnection.NotifyDataChanged();return saved;
    }
    public bool DeleteKhachHang(string id) => db.CapNhatDuLieu(
        "DELETE FROM Customers WHERE Id = @id",
        new SqlParameter("@id", SqlDbType.Int) { Value = int.Parse(id) });
    public DataTable TimKiemKhachHang(string keyword) => db.DocBang(
        "SELECT Id AS MaKH, FullName AS TenKH, Phone AS DienThoai, Points AS Diem FROM Customers WHERE FullName LIKE @kw OR Phone LIKE @kw OR CAST(Id AS NVARCHAR(20)) LIKE @kw ORDER BY FullName",
        new SqlParameter("@kw", SqlDbType.NVarChar, -1) { Value = DBConnection.LikePattern(keyword) });

}
