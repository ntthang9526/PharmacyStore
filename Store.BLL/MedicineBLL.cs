using Store.DAL;
using Store.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BLL
{
    public class MedicineBLL
    {
        private readonly MedicineDAL medicineDAL = new();
        public void KiemTraSoLuongConHan(int ma, int soLuong)
        {
            if (soLuong <= 0)
            {
                throw new Exception("Số lượng không hợp lệ!");
            }
            int soLuongTonKho = medicineDAL.SoLuongConHan(ma);
            if (soLuongTonKho < 0)
            {
                throw new Exception("Không tìm thấy sản phẩm!");
            }
            if (soLuongTonKho == 0)
            {
                throw new Exception("Sản phẩm đã hết hàng!");
            }
            if (soLuong > soLuongTonKho)
            {
                throw new Exception("Số lượng sản phẩm không đủ");
            }
        }
        public void KiemTraSoLuongConHan(Medicine medicine, int soLuong)
        {
            if (medicine.IsActive == false)
            {
                throw new Exception("Sản phẩm đã ngừng kinh doanh!");
            }
            if (soLuong <= 0)
            {
                throw new Exception("Số lượng không hợp lệ!");
            }
            int soLuongTonKho = medicineDAL.SoLuongConHan(medicine.Id);
            if (soLuongTonKho < 0)
            {
                throw new Exception("Không tìm thấy sản phẩm!");
            }
            if (soLuongTonKho == 0)
            {
                throw new Exception("Sản phẩm đã hết hàng!");
            }
            if (soLuong > soLuongTonKho)
            {
                throw new Exception("Số lượng sản phẩm không đủ");
            }
        }
    }
}
