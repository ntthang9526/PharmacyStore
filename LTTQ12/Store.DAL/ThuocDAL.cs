using System.Data;

namespace Store.DAL
{
    public class ThuocDAL
    {
        private DBConnection dtProcess = new DBConnection();

        // 1. Lấy toàn bộ danh sách thuốc và đổi tên cột cho khớp với Form
        public DataTable GetAllThuoc()
        {
            string query = @"
        SELECT 
            m.Id AS MaThuoc, 
            m.Name AS TenThuoc, 
            m.CategoryId AS MaNhom, 
            m.Unit AS DonViTinh, 
            m.Price AS DonGia, 
            ISNULL(SUM(b.Stock), 0) AS SoLuongTon 
        FROM Medicines m
        LEFT JOIN Batches b ON m.Id = b.MedicineId
        GROUP BY m.Id, m.Name, m.CategoryId, m.Unit, m.Price";
            return dtProcess.DocBang(query);
        }

        // 2. Thêm mới thuốc 
        public bool InsertThuoc(string maThuoc, string tenThuoc, string maNhom, string dvt, decimal donGia, int soLuongTon, out string dbError)
        {
            dbError = "";
            string strMaNhom = string.IsNullOrEmpty(maNhom) ? "NULL" : maNhom;
            string strDvt = string.IsNullOrEmpty(dvt) ? "NULL" : $"N'{dvt}'";

            string queryMedicines = string.Format(
                "INSERT INTO Medicines(Id, CategoryId, SupplierId, BarCode, Name, ActiveIngredient, Unit, Price, IsPrescription, IsActive) VALUES ('{0}', {1}, 1, '8935001800999', N'{2}', N'Hoạt chất mặc định', {3}, {4}, 0, 1)",
                maThuoc, strMaNhom, tenThuoc, strDvt, donGia
            );


            string batchNumber = "LOT-" + maThuoc + "-01";
            string queryBatches = string.Format(
                "INSERT INTO Batches(MedicineId, BatchNumber, ExpiryDate, Stock) VALUES ('{0}', '{1}', '2028-12-31', {2})",
                maThuoc, batchNumber, soLuongTon
            );

            try
            {
                dtProcess.CapNhatDuLieu(queryMedicines);
                dtProcess.CapNhatDuLieu(queryBatches);
                return true;
            }
            catch (Exception ex)
            {
                dbError = ex.Message;
                return false;
            }
        }

        // 3. Sửa thuốc dựa vào Id (Cập nhật cả thông tin thuốc và số lượng tồn vào bảng Batches)
        public bool UpdateThuoc(string maThuoc, string tenThuoc, string maNhom, string dvt, decimal donGia, int soLuongTon, out string dbError)
        {
            dbError = "";
            string strMaNhom = string.IsNullOrEmpty(maNhom) ? "NULL" : maNhom;
            string strDvt = string.IsNullOrEmpty(dvt) ? "NULL" : $"N'{dvt}'";

            // 1. Cập nhật thông tin cơ bản vào bảng Medicines
            string queryMedicines = string.Format(
                "UPDATE Medicines SET Name = N'{1}', CategoryId = {2}, Unit = {3}, Price = {4} WHERE Id = {0}",
                maThuoc, tenThuoc, strMaNhom, strDvt, donGia
            );

            // 2. Cập nhật số lượng tồn vào bảng Batches (Cập nhật cho lô của thuốc này)

            string queryBatches = string.Format(
                "UPDATE Batches SET Stock = {1} WHERE MedicineId = {0}",
                maThuoc, soLuongTon
            );

            try
            {
                dtProcess.CapNhatDuLieu(queryMedicines);
                dtProcess.CapNhatDuLieu(queryBatches);
                return true;
            }
            catch (Exception ex)
            {
                dbError = ex.Message;
                return false;
            }
        }

        // 4. Xóa thuốc dựa vào Id
        public bool DeleteThuoc(string maThuoc, out string dbError)
        {
            dbError = "";
            string query = string.Format("DELETE FROM Medicines WHERE Id = {0}", maThuoc);

            try
            {
                dtProcess.CapNhatDuLieu(query);
                return true;
            }
            catch (Exception ex)
            {
                dbError = ex.Message;
                return false;
            }
        }

        // 5. Tìm kiếm thuốc theo Mã thuốc hoặc Tên thuốc
        public DataTable TimKiemThuoc(string tuKhoa)
        {
            string query = string.Format(@"
        SELECT 
            m.Id AS MaThuoc, 
            m.Name AS TenThuoc, 
            m.CategoryId AS MaNhom, 
            m.Unit AS DonViTinh, 
            m.Price AS DonGia, 
            ISNULL(SUM(b.Stock), 0) AS SoLuongTon 
        FROM Medicines m
        LEFT JOIN Batches b ON m.Id = b.MedicineId
        WHERE CAST(m.Id AS NVARCHAR) LIKE '%{0}%' OR m.Name LIKE N'%{0}%'
        GROUP BY m.Id, m.Name, m.CategoryId, m.Unit, m.Price",
        tuKhoa
    );
            return dtProcess.DocBang(query);
        }
    }
}