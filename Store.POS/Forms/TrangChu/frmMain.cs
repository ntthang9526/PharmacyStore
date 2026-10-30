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
        private long phienBanDatabase;
        private bool dangDongBo;
        private DateTime thuLaiDongBoSau;
        private bool dangKiemTraDatabase;

        public frmMain()
        {
            InitializeComponent();
            global::Store.POS.PosFormStyle.ConfigureDisabledButtons(this);
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
            Shown+=(_,_)=>CapNhatQuyen();
            var timerDongBo = new System.Windows.Forms.Timer(components) { Interval=500 };
            timerDongBo.Tick += (_, _) => DongBoDuLieu();
            Shown += (_, _) => { if(!PosFormStyle.IsInDesigner(this)) timerDongBo.Start(); };
            var timerDongBoDatabase = new System.Windows.Forms.Timer(components) { Interval = 15 * 1000 };
            timerDongBoDatabase.Tick += (_, _) => KiemTraDongBoDatabase();
            Shown += (_, _) => { if(!PosFormStyle.IsInDesigner(this)) timerDongBoDatabase.Start(); };
            this.FormClosing += (_,e) =>
            {
                if(DaDangXuat) return;
                if(e.CloseReason==CloseReason.UserClosing &&
                    MessageBox.Show(this,NoiDungXacNhanThoat(false),"Đóng ứng dụng",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2)!=DialogResult.Yes) e.Cancel=true;
            };
            var timerQuyen=new System.Windows.Forms.Timer(components) {Interval=5000};
            timerQuyen.Tick+=(_,_)=>
            {
                try {CapNhatQuyen();}
                catch(UnauthorizedAccessException ex)
                {
                    timerQuyen.Stop();timerDongBo.Stop();timerDongBoDatabase.Stop();
                    Enabled=false;MessageBox.Show(this,ex.Message,"Phiên đăng nhập");Dispose();
                }
                catch(Exception ex) {lblTongQuan.Text="Chưa kiểm tra được phiên đăng nhập: "+ex.Message;}
            };
            Shown+=(_,_)=> {if(!PosFormStyle.IsInDesigner(this)) timerQuyen.Start();};
            ConfigureDashboard();
            Shown+=(_,_)=>CapNhatMenu();
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
                CapNhatQuyen();
                if(PhanQuyenGiaoDien.ChiAdmin(type) && !PhienDangNhap.LaAdmin)
                    throw new UnauthorizedAccessException("Chức năng này chỉ dành cho Admin.");
                DongBoDuLieu();
                KiemTraDongBoDatabase();
                if(cuaSoDangMo.TryGetValue(type,out form) && !form.IsDisposed)
                {
                    HienFormNhung(form);
                    if(form is ILamMoiDuLieu data) data.LamMoiDuLieu();
                    return;
                }
                form=create();
                form.TopLevel=false;form.FormBorderStyle=FormBorderStyle.None;form.ShowInTaskbar=false;
                form.KeyPreview=true;
                form.KeyDown+=(_,e)=>frmMain_KeyDown(this,e);
                form.MinimumSize=Size.Empty;form.MaximumSize=Size.Empty;
                form.Dock=DockStyle.Fill;contentHost.Controls.Add(form);
                cuaSoDangMo[type]=form;
                form.FormClosed+=(_,_)=>
                {
                    cuaSoDangMo.Remove(type);
                    if(!IsDisposed && !Disposing)
                    {
                        if(!cuaSoDangMo.Values.Any(f=>!f.IsDisposed && f.Visible)) VeTrangChu();
                        TaiTongQuan();
                    }
                };
                HienFormNhung(form);
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
            dashboard.Hide();form.Show();PhanQuyenGiaoDien.ApDung(form);form.BringToFront();form.Select();CapNhatMenu(form);
            
        }
        internal void VeTrangChu()
        {
            foreach(var form in cuaSoDangMo.Values.ToArray()) if(!form.IsDisposed) form.Hide();
            dashboard.Show();dashboard.BringToFront();TaiTongQuan();CapNhatMenu();
            
        }
        private async void KiemTraDongBoDatabase()=>await KiemTraDongBoDatabaseAsync();
        private async Task KiemTraDongBoDatabaseAsync(Func<long?>? read=null)
        {
            if(IsDisposed || Disposing || PosFormStyle.IsInDesigner(this) || dangDongBo || dangKiemTraDatabase || !Enabled || cuaSoDangMo.Values.Any(f=>!f.IsDisposed && !f.Enabled)) return;
            dangKiemTraDatabase=true;
            try
            {
                long? dbVersion=await Task.Run(read ?? Store.DAL.DBConnection.ThuLayPhienBanDatabase);
                if(IsDisposed || Disposing || !dbVersion.HasValue || dangDongBo || !Enabled || cuaSoDangMo.Values.Any(f=>!f.IsDisposed && !f.Enabled)) return;
                if(dbVersion.Value>phienBanDatabase)
                {
                    if(await DongBoDuLieuAsync(force:true)) phienBanDatabase=dbVersion.Value;
                }
            }
            catch { }
            finally {dangKiemTraDatabase=false;}
        }

        private async void DongBoDuLieu(bool force=false)=>await DongBoDuLieuAsync(force);

        private async Task<bool> DongBoDuLieuAsync(bool force=false)
        {
            if(IsDisposed || Disposing || PosFormStyle.IsInDesigner(this) || dangDongBo || !Enabled || cuaSoDangMo.Values.Any(f=>!f.IsDisposed && !f.Enabled)) return false;
            long version=Store.DAL.DBConnection.DataVersion;
            if(!force && version==phienBanDuLieu) return true;
            if(!force && DateTime.UtcNow<thuLaiDongBoSau) return false;
            dangDongBo=true;
            var errors=new List<string>();
            try
            {
                CapNhatQuyen();
                foreach(var form in cuaSoDangMo.Values.ToArray())
                    if(!form.IsDisposed && form is ILamMoiDuLieu data)
                        try
                        {
                            if(!await data.LamMoiDuLieuAsync()) errors.Add(form.Text);
                            if(IsDisposed || Disposing) return false;
                        }
                        catch(Exception ex) {errors.Add(form.Text+": "+ex.Message);}
                if(!TaiTongQuan()) errors.Add("Tổng quan");
                if(errors.Count==0) phienBanDuLieu=version;
                thuLaiDongBoSau=errors.Count==0?DateTime.MinValue:DateTime.UtcNow.AddSeconds(15);
                if(errors.Count>0) {lblTongQuan.ForeColor=Color.Firebrick;lblTongQuan.Text="Chưa tải lại được một số màn hình. Bấm Làm mới để thử lại. "+string.Join("; ",errors);}
                return errors.Count==0;
            }
            catch(Exception ex)
            {
                if(IsDisposed || Disposing) return false;
                thuLaiDongBoSau=DateTime.UtcNow.AddSeconds(15);
                lblTongQuan.ForeColor=Color.Firebrick;
                lblTongQuan.Text="Chưa cập nhật được dữ liệu. Bấm Làm mới để thử lại. "+ex.Message;
                return false;
            }
            finally {dangDongBo=false;}
        }

        private void CapNhatQuyen()
        {
            if(PosFormStyle.IsInDesigner(this)) return;
            var account=Store.DAL.PhanQuyenDAL.LayPhien();
            bool changed=PhienDangNhap.VaiTro!=account.VaiTro;
            PhienDangNhap.HoTen=account.HoTen;PhienDangNhap.VaiTro=account.VaiTro;
            btnNhapKho.Enabled=true;
            menuQuanLyKhac.Items[6].Visible=PhienDangNhap.LaAdmin;
            // Khởi tạo lại editor có quyền thay đổi; giữ giỏ hàng và các màn hình chỉ xem.
            if(changed)
            {
                foreach(var pair in cuaSoDangMo.ToArray())
                    if(PhanQuyenGiaoDien.ChiAdmin(pair.Key) || pair.Value is frmQuanLyThuoc or frmNhomThuoc or frmNhaCungCap or frmVoucher or frmKhachHang or frmLoHang)
                    {cuaSoDangMo.Remove(pair.Key);pair.Value.Dispose();}
                VeTrangChu();
            }
            foreach(var form in cuaSoDangMo.Values.ToArray())
            {
                PhanQuyenGiaoDien.ApDung(form);
                if(changed && form is frmNhapKho stock) stock.LamMoiDuLieu();
            }
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
            if(e.Handled || e.Modifiers!=Keys.None) return;
            Button? button=e.KeyCode switch
            {
                Keys.F1=>btnTrangChu,
                Keys.F2=>btnBanHang,
                Keys.F4=>btnQuanLyThuoc,
                Keys.F5=>btnQuanLyKhac,
                Keys.F6=>btnNhapKho,
                Keys.F7=>btnCanhBao,
                Keys.F8=>btnBaoCao,
                Keys.F3=>btnHoaDon,
                _=>null
            };
            if(button==null) return;
            e.SuppressKeyPress=true;
            button.PerformClick();
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
    
        private string NoiDungXacNhanThoat(bool dangXuat)
        {
            var drafts=cuaSoDangMo.Values.Where(f=>!f.IsDisposed).Select(f=>f switch
            {
                frmBanHang sales=>sales.CanhBaoChuaLuu,
                frmNhapKho stock=>stock.CanhBaoChuaLuu,
                _=>null
            }).Where(s=>s!=null).ToArray();
            string question=dangXuat?"Bạn có muốn đăng xuất?":"Bạn muốn đóng ứng dụng?";
            return drafts.Length==0?question:string.Join(Environment.NewLine,drafts)+Environment.NewLine+
                "Dữ liệu chưa lưu sẽ bị mất. Chọn Không để quay lại lưu."+Environment.NewLine+question;
        }

        public bool DaDangXuat { get; private set; }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, NoiDungXacNhanThoat(true), "Xác nhận đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                DaDangXuat = true;
                Close();
            }
        }
    }
}
