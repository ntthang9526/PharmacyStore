namespace Store.POS
{
    /// <summary>Lưu thông tin nhân viên đang đăng nhập, dùng chung cho toàn bộ các form.</summary>
    internal static class PhienDangNhap
    {
        public static int NhanVienId { get; set; }
        public static string HoTen { get; set; } = "";

        public static void Xoa()
        {
            NhanVienId = 0;
            HoTen = "";
        }
    }
}
