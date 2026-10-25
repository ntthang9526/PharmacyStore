using System.Data;
using Microsoft.Data.SqlClient;
namespace Store.DAL;

public class NhanVienDAL
{
    public DataTable TimKiem(string keyword) => new DBConnection().DocBang(@"
        SELECT Id AS MaNV, FullName AS HoTen, Username AS TaiKhoan, Phone AS DienThoai, IsActive AS DangLam
        FROM Employees WHERE FullName LIKE @kw OR Username LIKE @kw OR Phone LIKE @kw OR CAST(Id AS varchar(20)) LIKE @kw
        ORDER BY Id", new SqlParameter("@kw", SqlDbType.NVarChar, -1) {Value=DBConnection.LikePattern(keyword)});

    public int Luu(int? id, string name, string username, string? hash, string? phone, bool active, int operatorId)
    {
        using var connection=DBConnection.GetDbConnection();
        using var transaction=connection.BeginTransaction(IsolationLevel.Serializable);
        KiemTraTaiKhoan(connection,transaction,id,active,operatorId);
        using var cmd=new SqlCommand(id.HasValue ? @"
            UPDATE Employees SET FullName=@name, Username=@user, Phone=@phone, IsActive=@active,
                PasswordHash=CASE WHEN @hash IS NULL THEN PasswordHash ELSE @hash END
            OUTPUT INSERTED.Id WHERE Id=@id" : @"
            INSERT INTO Employees(FullName,Username,PasswordHash,Phone,IsActive)
            OUTPUT INSERTED.Id VALUES(@name,@user,@hash,@phone,@active)",connection,transaction);
        cmd.Parameters.Add("@id",SqlDbType.Int).Value=(object?)id??DBNull.Value;
        cmd.Parameters.Add("@name",SqlDbType.NVarChar,100).Value=name;
        cmd.Parameters.Add("@user",SqlDbType.VarChar,50).Value=username;
        cmd.Parameters.Add("@hash",SqlDbType.VarChar,255).Value=(object?)hash??DBNull.Value;
        cmd.Parameters.Add("@phone",SqlDbType.VarChar,15).Value=(object?)phone??DBNull.Value;
        cmd.Parameters.Add("@active",SqlDbType.Bit).Value=active;
        int result=Convert.ToInt32(cmd.ExecuteScalar());
        transaction.Commit();DBConnection.NotifyDataChanged();
        return result;
    }
    public void Khoa(int id,int operatorId)
    {
        using var connection=DBConnection.GetDbConnection();
        using var transaction=connection.BeginTransaction(IsolationLevel.Serializable);
        KiemTraTaiKhoan(connection,transaction,id,false,operatorId);
        using var cmd=new SqlCommand("UPDATE Employees SET IsActive=0 WHERE Id=@id",connection,transaction);
        cmd.Parameters.Add("@id",SqlDbType.Int).Value=id;cmd.ExecuteNonQuery();
        transaction.Commit();DBConnection.NotifyDataChanged();
    }
    private static void KiemTraTaiKhoan(SqlConnection connection,SqlTransaction transaction,int? id,bool active,int operatorId)
    {
        var accounts=new Dictionary<int,bool>();
        using(var cmd=new SqlCommand("SELECT Id,IsActive FROM Employees WITH (UPDLOCK,HOLDLOCK) ORDER BY Id",connection,transaction))
        using(var reader=cmd.ExecuteReader()) while(reader.Read()) accounts.Add(reader.GetInt32(0),reader.GetBoolean(1));
        if(!accounts.TryGetValue(operatorId,out bool loggedIn) || !loggedIn)
            throw new InvalidOperationException("Vui lòng đăng nhập bằng tài khoản đang hoạt động.");
        if(id.HasValue && !accounts.ContainsKey(id.Value)) throw new InvalidOperationException("Nhân viên không còn tồn tại. Hãy tải lại danh sách.");
        if(!active && id==operatorId) throw new InvalidOperationException("Không thể khóa tài khoản đang đăng nhập.");
        if(!active && id.HasValue && accounts[id.Value] && accounts.Count(x=>x.Value)<=1)
            throw new InvalidOperationException("Phải giữ ít nhất một tài khoản đang hoạt động.");
    }
}
