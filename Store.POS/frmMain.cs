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
        public frmMain()
        {
            InitializeComponent();
            CanGiua(lblTitle, panelTieuDe);
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Trang chủ");
        }

        private static void CanGiua(Control control, Control vungChua)
        {
            control.Left = (vungChua.ClientSize.Width - control.Width) / 2;
            control.Top = (vungChua.ClientSize.Height - control.Height) / 2;
        }
        private void ShowFormBanHang()
        {
            using (var frmBanHang = new frmBanHang())
            {
                frmBanHang.ShowDialog(this);
            }
        }
        private void ShowFormQuanLyThuoc()
        {
            MessageBox.Show("Form quản lý thuốc");
        }
        private void ShowFormQuanLyKhac()
        {
            MessageBox.Show("Form quản lý khác");
        }
        private void ShowFormNhapKho()
        {
            MessageBox.Show("Form nhập kho");
        }
        private void ShowFormCanhBao()
        {
            MessageBox.Show("Form cảnh báo");
        }
        private void ShowFormBaoCao()
        {
            MessageBox.Show("Form báo cáo");
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
