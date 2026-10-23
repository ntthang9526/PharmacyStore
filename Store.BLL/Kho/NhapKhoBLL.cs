using System.Data;
using Store.DAL;
using Store.Shared;

namespace Store.BLL
{
    public class NhapKhoBLL
    {
        private NhapKhoDAL dal = new NhapKhoDAL();
        private NhaCungCapDAL nccDal = new NhaCungCapDAL();

        public DataTable LayNhaCungCap()
        {
            return nccDal.GetAllNhaCungCap();   // Cột: MaNCC, TenNCC, DienThoai, DiaChi
        }

        public DataTable LayThuocTheoNCC(int maNCC)
        {
            return dal.LayThuocTheoNCC(maNCC);
        }

        // Kiểm tra phiếu -> tính tiền vốn -> lưu và cộng dồn tồn kho
        public bool LuuPhieuNhap(int maNhanVien, int maNCC, string? ghiChu, List<NhapKhoItem> dsThuoc,
                                 out int maPhieu, out decimal tongVon, out string error)
        {
            maPhieu = 0;
            tongVon = 0;
            error = "";

            if (maNhanVien <= 0)
            {
                error = "Chưa đăng nhập!";
                return false;
            }
            if (maNCC <= 0)
            {
                error = "Vui lòng chọn nhà cung cấp!";
                return false;
            }
            if (dsThuoc == null || dsThuoc.Count == 0)
            {
                error = "Phiếu nhập chưa có thuốc nào!";
                return false;
            }

            foreach (NhapKhoItem it in dsThuoc)
            {
                if (it.SoLuong <= 0)
                {
                    error = $"Số lượng của \"{it.TenThuoc}\" phải lớn hơn 0!";
                    return false;
                }
                it.GiaNhap = TienTe.LamTron(it.GiaNhap);
                if (it.GiaNhap <= 0)
                {
                    error = $"Giá nhập của \"{it.TenThuoc}\" phải lớn hơn 0!";
                    return false;
                }
                if (string.IsNullOrWhiteSpace(it.SoLo))
                {
                    error = $"Vui lòng nhập số lô cho \"{it.TenThuoc}\"!";
                    return false;
                }
                if (it.HanDung.Date <= DateTime.Today)
                {
                    error = $"Lô {it.SoLo} của \"{it.TenThuoc}\" đã hết hạn hoặc hết hạn hôm nay, không thể nhập kho!";
                    return false;
                }

                it.SoLo = it.SoLo.Trim().ToUpper();
                tongVon += it.SoLuong * it.GiaNhap;
            }

            string? note = string.IsNullOrWhiteSpace(ghiChu) ? null : ghiChu.Trim();
            if (note != null && note.Length > 250) note = note.Substring(0, 250);

            try
            {
                return dal.TaoPhieuNhap(maNhanVien, maNCC, note, tongVon, dsThuoc, out maPhieu, out error);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
    }
}
