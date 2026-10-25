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
        private readonly ContextMenuStrip menuCuaSo;
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
            // Mục "Nhân viên" chỉ hiện khi đăng nhập Admin – sẽ được thêm động trong Shown
            menuQuanLyKhac.Opening += (_, _) => CapNhatMenuPhanQuyen();
            menuCuaSo = new ContextMenuStrip(components);
            btnCuaSo.Click += (_, _) => { CapNhatDanhSachCuaSo(); menuCuaSo.Show(btnCuaSo,new Point(0,btnCuaSo.Height)); };
            var timerDongBo = new System.Windows.Forms.Timer(components) { Interval=500 };
            timerDongBo.Tick += (_, _) => DongBoDuLieu();
            Shown += (_, _) =>
            {
                if (!PosFormStyle.IsInDesigner(this))
                {
                    timerDongBo.Start();
                    ApDungPhanQuyen(); // Ẩn/hiện chức năng theo vai trò ngay khi mở
                }
            };
            FormClosing += (_,e) =>
            {
                if(e.CloseReason==CloseReason.UserClosing && cuaSoDangMo.Count>0 &&
                    MessageBox.Show(this,"Đóng ứng dụng sẽ đóng cả các form đang tạm ẩn. Tiếp tục?","Đóng ứng dụng",MessageBoxButtons.YesNo,MessageBoxIcon.Question)!=DialogResult.Yes) e.Cancel=true;
            };
            ConfigureDashboard();
        }

        private void MoForm<T>() where T : Form, new()
        {
            MoCuaSo(typeof(T), () => new T());
        }

        private void MoCuaSo(Type type, Func<Form> create)
        {
            Form? form=null;
            try
            {
                DongBoDuLieu();
                if(cuaSoDangMo.TryGetValue(type,out form) && !form.IsDisposed)
                {
                    HienFormNhung(form);
                    if(form is ILamMoiDuLieu data) data.LamMoiDuLieu();
                    return;
                }
                form=create();
                form.TopLevel=false;form.FormBorderStyle=FormBorderStyle.None;form.ShowInTaskbar=false;
                form.KeyPreview=true;
                form.KeyDown+=(_,e)=>
                {
                    if(e.Modifiers==Keys.None && e.KeyCode is Keys.F1 or Keys.F2 or Keys.F3 or Keys.F4 or Keys.F5 or Keys.F6)
                    {e.SuppressKeyPress=true;frmMain_KeyDown(this,e);}
                };
                form.MinimumSize=new Size(900,570);form.MaximumSize=Size.Empty;
                form.Dock=DockStyle.Fill;contentHost.Controls.Add(form);
                cuaSoDangMo[type]=form;
                form.FormClosed+=(_,_)=>
                {
                    cuaSoDangMo.Remove(type);
                    if(!IsDisposed && !Disposing)
                    {
                        if(!cuaSoDangMo.Values.Any(f=>!f.IsDisposed && f.Visible)) VeTrangChu();
                        CapNhatDanhSachCuaSo();TaiTongQuan();
                    }
                };
                form.VisibleChanged+=(_,_)=> {if(!IsDisposed && !Disposing) CapNhatDanhSachCuaSo();};
                HienFormNhung(form);CapNhatDanhSachCuaSo();
            }
            catch(Exception ex)
            {
                if(form!=null && !form.Visible) {cuaSoDangMo.Remove(type);form.Dispose();}
                MessageBox.Show(this,"Không mở được chức năng: "+ex.Message,"Lỗi");
            }
        }
        private void HienFormNhung(Form form)
        {
            foreach(var other in cuaSoDangMo.Values.ToArray())
                if(other!=form && !other.IsDisposed) other.Hide();
            dashboard.Hide();form.Show();form.BringToFront();form.Select();
            CapNhatDanhSachCuaSo();
        }
        internal void VeTrangChu()
        {
            foreach(var form in cuaSoDangMo.Values.ToArray()) if(!form.IsDisposed) form.Hide();
            dashboard.Show();dashboard.BringToFront();TaiTongQuan();
            CapNhatDanhSachCuaSo();
        }
        /// <summary>
        /// Áp dụng phân quyền ngay khi form khởi động:
        /// - Admin: thấy toàn bộ chức năng kể cả Quản lý nhân viên.
        /// - Nhân viên thường: KHÔNG thấy mục Nhân viên.
        /// </summary>
        private void ApDungPhanQuyen()
        {
            if (PosFormStyle.IsInDesigner(this)) return;
            bool isAdmin = PhienDangNhap.IsAdmin;
            // Ký hiệu vai trò trên tiêu đề để dễ nhận biết
            lblTitle.Text = "HỆ THỐNG QUẢN LÍ NHÀ THUỐC" +
                            (isAdmin ? "  [QUẢN TRỊ VIÊN]" : "  [NHÂN VIÊN]");
        }

        /// <summary>
        /// Cập nhật menu "Quản lý khác" ngay trước khi hiển thị:
        /// thêm hoặc xóa mục "Nhân viên" tuỳ theo quyền.
        /// </summary>
        private void CapNhatMenuPhanQuyen()
        {
            const string TEN_NHAN_VIEN = "Nhân viên";
            bool isAdmin = PhienDangNhap.IsAdmin;

            // Tìm item Nhân viên nếu đã có
            ToolStripItem? existing = menuQuanLyKhac.Items
                .Cast<ToolStripItem>()
                .FirstOrDefault(i => i.Text == TEN_NHAN_VIEN);

            if (isAdmin && existing == null)
            {
                // Admin: thêm mục Nhân viên cuối menu
                menuQuanLyKhac.Items.Add(TEN_NHAN_VIEN, null, (_, _) => MoForm<frmNhanVien>());
            }
            else if (!isAdmin && existing != null)
            {
                // Nhân viên thường: bỏ mục này
                menuQuanLyKhac.Items.Remove(existing);
            }
        }

        private void CapNhatDanhSachCuaSo()
        {
            foreach(ToolStripItem item in menuCuaSo.Items.Cast<ToolStripItem>().ToArray()) item.Dispose();
            menuCuaSo.Items.Clear();
            foreach(var entry in cuaSoDangMo.ToArray())
            {
                var form=entry.Value;if(form.IsDisposed) continue;
                var item=menuCuaSo.Items.Add(form.Text+(form.Visible?"":" (đang ẩn)"));
                item.Click+=(_,_)=>MoCuaSo(entry.Key,()=>throw new InvalidOperationException("Form đã đóng."));
            }
            if(menuCuaSo.Items.Count==0) menuCuaSo.Items.Add("Chưa mở form nào").Enabled=false;
            btnCuaSo.Text=$"Form đang mở ({cuaSoDangMo.Count})";
        }
        private void DongBoDuLieu(bool force=false)
        {
            if(PosFormStyle.IsInDesigner(this) || dangDongBo || !Enabled || cuaSoDangMo.Values.Any(f=>!f.IsDisposed && !f.Enabled)) return;
            long version=Store.DAL.DBConnection.DataVersion;
            if(!force && version==phienBanDuLieu) return;
            dangDongBo=true;
            var errors=new List<string>();
            try
            {
                if(PhienDangNhap.NhanVienId>0)
                {
                    var account=new Store.DAL.NhanVienDAL().TimKiem("").Rows.Cast<System.Data.DataRow>()
                        .FirstOrDefault(r=>(int)r["MaNV"]==PhienDangNhap.NhanVienId);
                    if(account!=null) PhienDangNhap.HoTen=(string)account["HoTen"];
                }
                foreach(var form in cuaSoDangMo.Values.ToArray())
                    if(!form.IsDisposed && form is ILamMoiDuLieu data)
                        try {data.LamMoiDuLieu();} catch(Exception ex) {errors.Add(form.Text+": "+ex.Message);}
                TaiTongQuan();phienBanDuLieu=version;
                if(errors.Count>0) {lblTongQuan.ForeColor=Color.Firebrick;lblTongQuan.Text="Một số form chưa tải lại được. Bấm Làm mới để thử lại. "+string.Join("; ",errors);}
            }
            catch(Exception ex)
            {
                phienBanDuLieu=version;lblTongQuan.ForeColor=Color.Firebrick;
                lblTongQuan.Text="Chưa cập nhật được dữ liệu. Bấm Làm mới để thử lại. "+ex.Message;
            }
            finally {dangDongBo=false;}
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            VeTrangChu();DongBoDuLieu(true);
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
    }
}
