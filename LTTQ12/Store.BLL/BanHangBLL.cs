using System.Data;
using Store.DAL;
using Store.Shared;

namespace Store.BLL
{
    public class BanHangBLL
    {
        private BanHangDAL dal = new BanHangDAL();
        private VoucherBLL voucherBll = new VoucherBLL();

        public DataTable TimThuoc(string tuKhoa)
        {
            return dal.TimThuocBan((tuKhoa ?? "").Trim());
        }

        // Thanh toán: lấy giá từ DB (không tin giá phía client) -> tính tổng -> áp voucher -> lưu hóa đơn + trừ tồn kho
        public bool ThanhToan(int maNhanVien, List<CartItem> gioHang, string? maVoucher,
                              out int maHoaDon, out decimal tongTien, out decimal giamGia, out string error)
        {
            maHoaDon = 0;
            tongTien = 0;
            giamGia = 0;
            error = "";

            if (maNhanVien <= 0)
            {
                error = "Chưa đăng nhập!";
                return false;
            }
            if (gioHang == null || gioHang.Count == 0)
            {
                error = "Giỏ hàng đang trống!";
                return false;
            }

            // Gộp các dòng trùng thuốc, bỏ dòng số lượng <= 0
            List<CartItem> items = gioHang
                .Where(x => x.SoLuong > 0)
                .GroupBy(x => x.MaThuoc)
                .Select(g => new CartItem { MaThuoc = g.Key, SoLuong = g.Sum(x => x.SoLuong) })
                .ToList();

            if (items.Count == 0)
            {
                error = "Số lượng thuốc phải lớn hơn 0!";
                return false;
            }

            try
            {
                foreach (CartItem it in items)
                {
                    DataTable dt = dal.LayThongTinThuoc(it.MaThuoc);
                    if (dt.Rows.Count == 0)
                    {
                        error = $"Thuốc mã {it.MaThuoc} không tồn tại hoặc đã ngừng bán!";
                        return false;
                    }
                    it.TenThuoc = dt.Rows[0]["Name"].ToString() ?? "";
                    it.DonGia = Convert.ToDecimal(dt.Rows[0]["Price"]);
                    tongTien += it.DonGia * it.SoLuong;
                }

                int? voucherId = null;
                if (!string.IsNullOrWhiteSpace(maVoucher))
                {
                    if (!voucherBll.KiemTra(maVoucher, out VoucherInfo? v, out error))
                        return false;

                    voucherId = v!.Id;
                    giamGia = voucherBll.TinhGiamGia(v, tongTien);
                }

                return dal.TaoHoaDon(maNhanVien, voucherId, tongTien, giamGia, items, out maHoaDon, out error);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
    }
}
