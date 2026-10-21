namespace Store.POS
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Vòng lặp: Đăng nhập -> Màn hình chính -> (Đăng xuất thì quay lại Đăng nhập)
            while (true)
            {
                using (frmLogin login = new frmLogin())
                {
                    if (login.ShowDialog() != DialogResult.OK)
                        break; // Bấm Thoát ở màn hình đăng nhập
                }

                frmMain main = new frmMain();
                Application.Run(main);

                if (!main.DangXuat)
                    break; // Đóng cửa sổ chính = thoát chương trình

                PhienDangNhap.Xoa();
            }
        }
    }
}
