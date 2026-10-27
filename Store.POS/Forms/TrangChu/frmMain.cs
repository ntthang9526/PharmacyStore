using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Store.POS
{
    public partial class frmMain : Form
    {
        private readonly ContextMenuStrip menuQuanLyKhac;
        private readonly Dictionary<Type, Form> cuaSoDangMo = new();
        private long phienBanDuLieu = Store.DAL.DBConnection.DataVersion;
        private bool dangDongBo;

        public frmMain()
        {
            InitializeComponent();
            menuQuanLyKhac = new ContextMenuStrip(components ??= new Container());
            menuQuanLyKhac.Items.Add("Nhóm thuốc", null, (_, _) => MoForm<frmNhomThuoc>());
            menuQuanLyKhac.Items.Add("Nhà cung cấp", null, (_, _) => MoForm<frmNhaCungCap>());
            menuQuanLyKhac.Items.Add("Khách hàng", null, (_, _) => MoForm<frmKhachHang>());
            menuQuanLyKhac.Items.Add("Lô hàng", null, (_, _) => MoForm<frmLoHang>());
            menuQuanLyKhac.Items.Add("Voucher", null, (_, _) => MoForm<frmVoucher>());
            menuQuanLyKhac.Items.Add("Hóa đơn", null, (_, _) => MoForm<frmHoaDon>());
            menuQuanLyKhac.Items.Add("Nhân viên", null, (_, _) => MoForm<frmNhanVien>());
            menuQuanLyKhac.Items.Add("Phiếu nhập hàng", null, (_, _) => MoForm<frmPhieuNhap>());
            menuQuanLyKhac.Items.Add("Đổi mật khẩu", null, (_, _) => MoForm<frmDoiMatKhau>());
            Shown += (_, _) => CapNhatQuyen();
            var timerDongBo = new System.Windows.Forms.Timer(components) { Interval = 500 };
            timerDongBo.Tick += (_, _) => DongBoDuLieu();
            Shown += (_, _) => { if (!PosFormStyle.IsInDesigner(this)) timerDongBo.Start(); };
            FormClosing += (_, e) =>
            {
                if (e.CloseReason == CloseReason.UserClosing && cuaSoDangMo.Count > 0 &&
                    MessageBox.Show(this, "Đóng ứng dụng sẽ đóng cả các form đang tạm ẩn. Tiếp tục?", "Đóng ứng dụng", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) e.Cancel = true;
            };
            var timerQuyen = new System.Windows.Forms.Timer(components) { Interval = 5000 };
            timerQuyen.Tick += (_, _) =>
            {
                try { CapNhatQuyen(); }
                catch (UnauthorizedAccessException ex)
                {
                    timerQuyen.Stop(); timerDongBo.Stop();
                    Enabled = false; MessageBox.Show(this, ex.Message, "Phiên đăng nhập"); Dispose();
                }
                catch (Exception ex) { lblTongQuan.Text = "Chưa kiểm tra được phiên đăng nhập: " + ex.Message; }
            };
            Shown += (_, _) => { if (!PosFormStyle.IsInDesigner(this)) timerQuyen.Start(); };
            ConfigureDashboard();
        }

        private void MoForm<T>() where T : Form, new()
        {
            MoCuaSo(typeof(T), () => new T());
        }

        private void MoCuaSo(Type type, Func<Form> create)
        {
            Form? form = null;
            try
            {
                CapNhatQuyen();
                if (PhanQuyenGiaoDien.ChiAdmin(type) && !PhienDangNhap.LaAdmin)
                    throw new UnauthorizedAccessException("Chức năng này chỉ dành cho Admin.");
                DongBoDuLieu();
                if (cuaSoDangMo.TryGetValue(type, out form) && !form.IsDisposed)
                {
                    HienFormNhung(form);
                    if (form is ILamMoiDuLieu data) data.LamMoiDuLieu();
                    return;
                }
                form = create();
                form.TopLevel = false; form.FormBorderStyle = FormBorderStyle.None; form.ShowInTaskbar = false;
                form.KeyPreview = true;
                form.KeyDown += (_, e) =>
                {
                    if (e.Modifiers == Keys.None && e.KeyCode is Keys.F1 or Keys.F2 or Keys.F3 or Keys.F4 or Keys.F5 or Keys.F6)
                    { e.SuppressKeyPress = true; frmMain_KeyDown(this, e); }
                };
                form.MinimumSize = Size.Empty; form.MaximumSize = Size.Empty;
                form.Dock = DockStyle.Fill; contentHost.Controls.Add(form);
                cuaSoDangMo[type] = form;
                form.FormClosed += (_, _) =>
                {
                    cuaSoDangMo.Remove(type);
                    if (!IsDisposed && !Disposing)
                    {
                        if (!cuaSoDangMo.Values.Any(f => !f.IsDisposed && f.Visible)) VeTrangChu();
                        TaiTongQuan();
                    }
                };
                HienFormNhung(form);
            }
            catch (Exception ex)
            {
                if (form != null && !form.Visible) { cuaSoDangMo.Remove(type); form.Dispose(); }
                MessageBox.Show(this, "Không mở được chức năng: " + ex.Message, "Lỗi");
            }
        }
        private void HienFormNhung(Form form)
        {
            foreach (var other in cuaSoDangMo.Values.ToArray())
                if (other != form && !other.IsDisposed) other.Hide();
            dashboard.Hide(); form.Show(); PhanQuyenGiaoDien.ApDung(form); form.BringToFront(); form.Select();

        }
        internal void VeTrangChu()
        {
            foreach (var form in cuaSoDangMo.Values.ToArray()) if (!form.IsDisposed) form.Hide();
            dashboard.Show(); dashboard.BringToFront(); TaiTongQuan();

        }
        private void DongBoDuLieu(bool force = false)
        {
            if (PosFormStyle.IsInDesigner(this) || dangDongBo || !Enabled || cuaSoDangMo.Values.Any(f => !f.IsDisposed && !f.Enabled)) return;
            long version = Store.DAL.DBConnection.DataVersion;
            if (!force && version == phienBanDuLieu) return;
            dangDongBo = true;
            var errors = new List<string>();
            try
            {
                CapNhatQuyen();
                foreach (var form in cuaSoDangMo.Values.ToArray())
                    if (!form.IsDisposed && form is ILamMoiDuLieu data)
                        try { data.LamMoiDuLieu(); } catch (Exception ex) { errors.Add(form.Text + ": " + ex.Message); }
                TaiTongQuan(); phienBanDuLieu = version;
                if (errors.Count > 0) { lblTongQuan.ForeColor = Color.Firebrick; lblTongQuan.Text = "Một số form chưa tải lại được. Bấm Làm mới để thử lại. " + string.Join("; ", errors); }
            }
            catch (Exception ex)
            {
                phienBanDuLieu = version; lblTongQuan.ForeColor = Color.Firebrick;
                lblTongQuan.Text = "Chưa cập nhật được dữ liệu. Bấm Làm mới để thử lại. " + ex.Message;
            }
            finally { dangDongBo = false; }
        }

        private void CapNhatQuyen()
        {
            if (PosFormStyle.IsInDesigner(this)) return;
            var account = Store.DAL.PhanQuyenDAL.LayPhien();
            bool changed = PhienDangNhap.VaiTro != account.VaiTro;
            PhienDangNhap.HoTen = account.HoTen; PhienDangNhap.VaiTro = account.VaiTro;
            btnNhapKho.Enabled = true;
            menuQuanLyKhac.Items[6].Visible = PhienDangNhap.LaAdmin;
            // Khởi tạo lại editor có quyền thay đổi; giữ giỏ hàng và các màn hình chỉ xem.
            if (changed)
            {
                foreach (var pair in cuaSoDangMo.ToArray())
                    if (PhanQuyenGiaoDien.ChiAdmin(pair.Key) || pair.Value is frmQuanLyThuoc or frmNhomThuoc or frmNhaCungCap or frmVoucher or frmKhachHang)
                    { cuaSoDangMo.Remove(pair.Key); pair.Value.Dispose(); }
                VeTrangChu();
            }
            foreach (var form in cuaSoDangMo.Values.ToArray())
            {
                PhanQuyenGiaoDien.ApDung(form);
                if (changed && form is frmNhapKho stock) stock.LamMoiDuLieu();
            }
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            VeTrangChu(); DongBoDuLieu(true);
        }

        private void ShowFormBanHang()
        {
            MoForm<frmBanHang>();
        }
        private void ShowFormQuanLyThuoc()
        {
            MoForm<frmQuanLyThuoc>();
        }
        private void ShowFormQuanLyKhac()
        {
            menuQuanLyKhac.Show(btnQuanLyKhac, new Point(0, btnQuanLyKhac.Height));
        }
        private void ShowFormNhapKho()
        {
            MoForm<frmNhapKho>();
        }
        private void ShowFormCanhBao()
        {
            MoForm<frmCanhBao>();
        }
        private void ShowFormBaoCao()
        {
            MoForm<frmBaoCao>();
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                ShowFormBanHang();
            }
            if (e.KeyCode == Keys.F2)
            {
                ShowFormQuanLyThuoc();
            }
            if (e.KeyCode == Keys.F3)
            {
                ShowFormQuanLyKhac();
            }
            if (e.KeyCode == Keys.F4)
            {
                ShowFormNhapKho();
            }
            if (e.KeyCode == Keys.F5)
            {
                ShowFormCanhBao();
            }
            if (e.KeyCode == Keys.F6)
            {
                ShowFormBaoCao();
            }
        }

        private void btnBanHang_Click(object sender, EventArgs e)
        {
            ShowFormBanHang();
        }

        private void btnQuanLyThuoc_Click(object sender, EventArgs e)
        {
            ShowFormQuanLyThuoc();
        }

        private void btnQuanLyKhac_Click(object sender, EventArgs e)
        {
            ShowFormQuanLyKhac();
        }

        private void btnNhapKho_Click(object sender, EventArgs e)
        {
            ShowFormNhapKho();
        }

        private void btnCanhBao_Click(object sender, EventArgs e)
        {
            ShowFormCanhBao();
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            ShowFormBaoCao();
        }

        private void dgvGanNhat_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
