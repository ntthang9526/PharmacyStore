using System.Data;
using Microsoft.Data.SqlClient;
namespace Store.DAL;

public enum Quyen { Xem, BanHang, KhachHang, NhapKho, QuanTri }

public static class PhanQuyenDAL
{
    // Danh tính của phiên POS; Task.Run kế thừa ngữ cảnh này.
    // shortcut: POS kết nối SQL trực tiếp; cần API kiểm tra quyền phía máy chủ khi triển khai không tin cậy máy trạm.
    private static readonly AsyncLocal<int> current = new();
    public static int NhanVienId { get => current.Value; set => current.Value=value; }
    public static bool ChoPhep(string role, Quyen permission) => role=="ADMIN" || role=="STAFF" && permission!=Quyen.QuanTri;
    public static (string HoTen,string VaiTro) LayPhien()
    {
        using var connection=DBConnection.GetDbConnection();
        return YeuCau(Quyen.Xem,connection);
    }
    public static (string HoTen,string VaiTro) YeuCau(Quyen permission,SqlConnection connection,SqlTransaction? transaction=null,int? employeeId=null)
    {
        if(NhanVienId<=0 || employeeId.HasValue && employeeId!=NhanVienId)
            throw new UnauthorizedAccessException("Phiên đăng nhập không hợp lệ. Vui lòng đăng nhập lại.");
        using var cmd=new SqlCommand("SELECT FullName,Role FROM Employees"+(transaction==null?"":" WITH (HOLDLOCK)")+" WHERE Id=@id AND IsActive=1",connection,transaction);
        cmd.Parameters.Add("@id",SqlDbType.Int).Value=NhanVienId;
        using var reader=cmd.ExecuteReader();
        if(!reader.Read()) throw new UnauthorizedAccessException("Tài khoản đã bị khóa hoặc không còn tồn tại. Vui lòng đăng nhập lại.");
        string name=reader.GetString(0),role=reader.GetString(1);
        if(!ChoPhep(role,permission)) throw new UnauthorizedAccessException("Chức năng này chỉ dành cho Admin.");
        return(name,role);
    }
}
