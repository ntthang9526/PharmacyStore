using System.Data;
using Microsoft.Data.SqlClient;
namespace Store.DAL;
public class MatKhauDAL
{
    public string LayHash()
    {
        using var connection=DBConnection.GetDbConnection();
        PhanQuyenDAL.YeuCau(Quyen.Xem,connection);
        using var cmd=new SqlCommand("SELECT PasswordHash FROM Employees WHERE Id=@id AND IsActive=1",connection);
        cmd.Parameters.Add("@id",SqlDbType.Int).Value=PhanQuyenDAL.NhanVienId;
        return (string?)cmd.ExecuteScalar()??throw new UnauthorizedAccessException("Tài khoản không còn hoạt động.");
    }
    public bool Doi(string oldHash,string newHash)
    {
        using var connection=DBConnection.GetDbConnection();
        using var transaction=connection.BeginTransaction();
        PhanQuyenDAL.YeuCau(Quyen.Xem,connection,transaction);
        using var cmd=new SqlCommand("UPDATE Employees SET PasswordHash=@new WHERE Id=@id AND IsActive=1 AND PasswordHash COLLATE Latin1_General_100_BIN2=@old",connection,transaction);
        cmd.Parameters.Add("@id",SqlDbType.Int).Value=PhanQuyenDAL.NhanVienId;
        cmd.Parameters.Add("@old",SqlDbType.VarChar,255).Value=oldHash;
        cmd.Parameters.Add("@new",SqlDbType.VarChar,255).Value=newHash;
        bool saved=cmd.ExecuteNonQuery()==1;transaction.Commit();
        if(saved) DBConnection.NotifyDataChanged();return saved;
    }
}
