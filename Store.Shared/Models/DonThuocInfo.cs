namespace Store.Shared;

public sealed class DonThuocInfo
{
    public string SoDon { get; set; } = "";
    public string BenhNhan { get; set; } = "";
    public string NguoiKeDon { get; set; } = "";
    public DateTime NgayKeDon { get; set; } = DateTime.Today;
    public bool DaKiemTra { get; set; }

    public string? KiemTra(bool dayDu=true)
    {
        if(SoDon.Trim().Length>100 || BenhNhan.Trim().Length>150 || NguoiKeDon.Trim().Length>150 || (SoDon+BenhNhan+NguoiKeDon).Any(char.IsControl)) return "Số đơn tối đa 100 ký tự, tên người bệnh/người kê đơn tối đa 150 ký tự, không có ký tự điều khiển.";
        if (dayDu && string.IsNullOrWhiteSpace(SoDon)) return "Nhập mã/số đơn thuốc từ 1 đến 100 ký tự.";
        if (dayDu && string.IsNullOrWhiteSpace(BenhNhan)) return "Nhập tên người bệnh từ 1 đến 150 ký tự.";
        if (dayDu && string.IsNullOrWhiteSpace(NguoiKeDon)) return "Nhập người kê đơn từ 1 đến 150 ký tự.";
        if (NgayKeDon.Date < new DateTime(1900,1,1) || NgayKeDon.Date > DateTime.Today) return "Ngày kê đơn không hợp lệ hoặc nằm trong tương lai.";
        if (dayDu && !DaKiemTra) return "Xác nhận đã kiểm tra đơn thuốc trước khi thanh toán.";
        return null;
    }
}
