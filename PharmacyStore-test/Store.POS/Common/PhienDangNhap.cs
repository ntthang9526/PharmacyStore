namespace Store.POS
{
    /// <summary>Lưu thông tin nhân viên đang đăng nhập, dùng chung cho toàn bộ các form.</summary>
    internal static class PhienDangNhap
    {
        public static int NhanVienId { get; set; }
        public static string HoTen { get; set; } = "";
        /// <summary>true = tài khoản Admin, được phép quản lý nhân viên.</summary>
        public static bool IsAdmin { get; set; }

        public static void Xoa()
        {
            NhanVienId = 0;
            HoTen = "";
            IsAdmin = false;
        }
    }
}
