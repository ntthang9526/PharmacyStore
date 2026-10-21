
using System.Data;


namespace Store.DAL
{
    public class KhachHangDAL
    {
        private DBConnection dtProcess = new DBConnection();

        // 1. Lấy danh sách khách hàng (Dùng AS để đổi tên cột SQL thành tên dùng trên giao diện)
        public DataTable GetAllKhachHang()
        {
            string query = "SELECT Id AS MaKH, FullName AS TenKH, Phone AS DienThoai, Points AS Diem FROM Customers";
            return dtProcess.DocBang(query);
        }

        // 2.Thêm khách hàng (Có truyền MaKH do người dùng tự nhập vào câu lệnh INSERT)
        public bool InsertKhachHang(string maKH, string tenKH, string dienThoai, string diem)
        {
            string query = string.Format(
                "INSERT INTO Customers(Id, FullName, Phone, Points) VALUES ('{0}', N'{1}', '{2}', {3})",
                maKH, tenKH, dienThoai, string.IsNullOrEmpty(diem) ? "0" : diem
            );
            try
            {
                dtProcess.CapNhatDuLieu(query);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // 3. Sửa khách hàng dựa theo Id
        public bool UpdateKhachHang(string maKH, string tenKH, string dienThoai, string diem)
        {
            string query = string.Format("UPDATE Customers SET FullName = N'{1}', Phone = '{2}', Points = {3} WHERE Id = {0}", maKH, tenKH, dienThoai, string.IsNullOrEmpty(diem) ? "0" : diem);
            try
            {
                dtProcess.CapNhatDuLieu(query);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // 4. Xóa khách hàng dựa theo Id
        public bool DeleteKhachHang(string maKH)
        {
            string query = string.Format("DELETE FROM Customers WHERE Id = {0}", maKH);
            try
            {
                dtProcess.CapNhatDuLieu(query);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}