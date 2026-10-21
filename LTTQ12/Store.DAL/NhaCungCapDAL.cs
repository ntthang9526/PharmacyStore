
using System.Data;

namespace Store.DAL
{
    public class NhaCungCapDAL
    {
        private DBConnection dtProcess = new DBConnection();

        // 1. Lấy danh sách nhà cung cấp
        public DataTable GetAllNhaCungCap()
        {
            string query = "SELECT Id AS MaNCC, Name AS TenNCC, Phone AS DienThoai, Address AS DiaChi FROM Suppliers";
            return dtProcess.DocBang(query);
        }

        // Thêm nhà cung cấp (Có truyền MaNCC do người dùng tự nhập)
        public bool InsertNhaCungCap(string maNCC, string tenNCC, string dienThoai, string diaChi)
        {
            string query = string.Format("INSERT INTO Suppliers(Id, Name, Phone, Address) VALUES ('{0}', N'{1}', '{2}', N'{3}')", maNCC, tenNCC, dienThoai, diaChi);
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

        // 3. Sửa nhà cung cấp dựa vào Id
        public bool UpdateNhaCungCap(string maNCC, string tenNCC, string dienThoai, string diaChi)
        {
            string query = string.Format("UPDATE Suppliers SET Name = N'{1}', Phone = '{2}', Address = N'{3}' WHERE Id = {0}", maNCC, tenNCC, dienThoai, diaChi);
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

        // 4. Xóa nhà cung cấp dựa vào Id
        public bool DeleteNhaCungCap(string maNCC)
        {
            string query = string.Format("DELETE FROM Suppliers WHERE Id = {0}", maNCC);
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