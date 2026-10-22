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
            ConfigureDashboard();
        }

        private void MoForm<T>() where T : Form, new()
        {
            try { using var form = new T(); form.ShowDialog(this); }
            catch (Exception ex) { MessageBox.Show(this, "Không mở được chức năng: " + ex.Message, "Lỗi"); }
            finally { TaiTongQuan(); }
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            TaiTongQuan();
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
