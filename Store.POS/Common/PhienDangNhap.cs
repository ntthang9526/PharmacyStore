namespace Store.POS
{
    /// <summary>Lưu thông tin nhân viên đang đăng nhập, dùng chung cho toàn bộ các form.</summary>
    internal static class PhienDangNhap
    {
        public static int NhanVienId { get => Store.DAL.PhanQuyenDAL.NhanVienId; set => Store.DAL.PhanQuyenDAL.NhanVienId=value; }
        public static string VaiTro { get; set; } = "STAFF";
        public static bool LaAdmin => VaiTro=="ADMIN";
        public static string HoTen { get; set; } = "";

        public static void Xoa()
        {
            NhanVienId = 0;
            VaiTro = "STAFF";
            HoTen = "";
        }
    }
}
