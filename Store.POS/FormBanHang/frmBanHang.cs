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
    public partial class frmBanHang : Form
    {
        public frmBanHang()
        {
            InitializeComponent();
            
        }

        private void frmBanHang_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
                this.Close();
            }
        }

        private void frmBanHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            //var res = MessageBox.Show("Are u sure?", "",MessageBoxButtons.YesNo);
            //if (res == DialogResult.No)
            //{
            //    e.Cancel = true;
            //}
        }

        private void frmBanHang_FormClosed(object sender, FormClosedEventArgs e)
        {
            //MessageBox.Show("Stupid Dog");
        }
    }
}
