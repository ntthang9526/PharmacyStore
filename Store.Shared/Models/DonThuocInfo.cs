namespace Store.Shared;

public sealed class DonThuocInfo
{
    public string SoDon { get; set; } = "";
    public string BenhNhan { get; set; } = "";
    public string NguoiKeDon { get; set; } = "";
    public DateTime NgayKeDon { get; set; } = DateTime.Today;
    public bool DaKiemTra { get; set; }

    public string? KiemTra()
    {
        if (string.IsNullOrWhiteSpace(SoDon) || SoDon.Trim().Length > 100) return "Nhập mã/số đơn thuốc từ 1 đến 100 ký tự.";
        if (string.IsNullOrWhiteSpace(BenhNhan) || BenhNhan.Trim().Length > 150) return "Nhập tên người bệnh từ 1 đến 150 ký tự.";
        if (string.IsNullOrWhiteSpace(NguoiKeDon) || NguoiKeDon.Trim().Length > 150) return "Nhập người kê đơn từ 1 đến 150 ký tự.";
        if (NgayKeDon.Date < new DateTime(1900,1,1) || NgayKeDon.Date > DateTime.Today) return "Ngày kê đơn không hợp lệ hoặc nằm trong tương lai.";
        if (!DaKiemTra) return "Xác nhận đã kiểm tra đơn thuốc trước khi thanh toán.";
        return null;
    }
}
