
using System.Data;


namespace Store.DAL
{
    public class NhomThuocDAL
    {
        private DBConnection dtProcess = new DBConnection();

        // 1. Lấy danh sách nhóm thuốc
        public DataTable GetAllNhomThuoc()
        {
            string query = "SELECT Id AS MaNhom, Name AS TenNhom FROM Categories";
            return dtProcess.DocBang(query);
        }

        // 2. Thêm nhóm thuốc (Có truyền MaNhom/Id do người dùng tự nhập)
        public bool InsertNhomThuoc(string maNhom, string tenNhom)
        {
            string query = string.Format("INSERT INTO Categories(Id, Name) VALUES ('{0}', N'{1}')", maNhom, tenNhom);
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

        // 2. Sửa: Cập nhật Name dựa theo điều kiện Id
        public bool UpdateNhomThuoc(string maNhom, string tenNhom)
        {
            string query = string.Format("UPDATE Categories SET Name = N'{1}' WHERE Id = {0}", maNhom, tenNhom);
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

        // 3. Xóa: Xóa dựa theo Id
        public bool DeleteNhomThuoc(string maNhom)
        {
            string query = string.Format("DELETE FROM Categories WHERE Id = {0}", maNhom);
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