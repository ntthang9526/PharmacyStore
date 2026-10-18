using Store.DAL;
using Store.Shared.Models;

namespace Store.BLL
{
    public class BatchBLL
    {
        private readonly BatchDAL batchDAL = new();

        public void KiemTraTonKho(int ma, int soLuong)
        {
            if (soLuong <= 0)
            {
                throw new Exception("Số lượng không hợp lệ!");
            }
            int soLuongTonKho = batchDAL.TimToanBoSoLuongConHan(ma);
            if (soLuong > soLuongTonKho)
            {
                throw new Exception("Số lượng tồn kho không đủ");
            }
        }
        public void KiemTraDuHang(Medicine med, int soLuong)
        {
            if (soLuong <= 0)
            {
                throw new Exception("Số lượng không hợp lệ");
            }
            int soLuongTonKho = batchDAL.TimToanBoSoLuongConHan(med.Id);
            if (soLuong > soLuongTonKho)
            {
                throw new Exception("Số lượng tồn kho không đủ");
            }
        }
    }
}
