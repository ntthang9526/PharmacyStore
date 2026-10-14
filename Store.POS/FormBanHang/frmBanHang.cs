using Store.DAL;
using Store.POS.FormBanHang;
using Store.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Store.POS
{
    public partial class frmBanHang : Form
    {
        private readonly BindingList<RowData> gioHang = new();
        private readonly BindingList<Medicine> timKiem = new();
        private readonly MedicineDAL medicineDAL = new();
        private readonly Dictionary<int, Medicine>? danhSachThuoc = new();
        private readonly Dictionary<Medicine, int> danhSachGioHang = new();
        public frmBanHang()
        {
            InitializeComponent();
            foreach (Control control in panelAction.Controls)
            {
                control.MouseLeave += AnActionNeuChuotRaNgoai;
            }
            tableGioHang.AutoGenerateColumns = false;
            danhSachThuoc = medicineDAL.GetMedicines();
            if (danhSachThuoc == null)
            {
                MessageBox.Show("Null");
                return;
            }
            tableGioHang.DataSource = gioHang;
            tableTimKiem.DataSource = timKiem;
        }

        private void frmBanHang_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                this.Close();
            }
        }

        private void frmBanHang_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void frmBanHang_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void tableGioHang_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            tableGioHang.ClearSelection();
            tableGioHang.CurrentCell = null;
        }


        private void tableGioHang_Leave(object sender, EventArgs e)
        {
            if (panelAction.Visible == true)
            {
                return;
            }
            tableGioHang.ClearSelection();
            tableGioHang.CurrentCell = null;
        }
        private void tableGioHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (tableGioHang.Columns[e.ColumnIndex].Name == "btnAction")
            {
                Rectangle cell = tableGioHang.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                Point viTri = this.PointToClient(tableGioHang.PointToScreen(cell.Location));
                panelAction.Location = new Point(panelAction.Location.X, viTri.Y);
                panelAction.Visible = true;
            }
        }
        private void ThemVaoGioHang(Medicine medicine)
        {
            var rowData = new RowData
            {
                Id = medicine.Id,
                Name = medicine.Name,
                Price = medicine.Price,
                Quantity = 1,
                Unit = medicine.Unit
            };
        }

        private void tableGioHang_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            var row = tableGioHang.Rows[e.RowIndex];
            if (row.IsNewRow) return;

            if (!row.Selected)
            {
                tableGioHang.ClearSelection();
                row.Selected = true;
            }
            if (tableGioHang.Columns[e.ColumnIndex].Name != "btnAction")
            {
                panelAction.Visible = false;
                return;
            }
            if (tableGioHang.SelectedRows.Count > 0 && tableGioHang.SelectedRows[0].DataBoundItem is RowData currRow)
            {
                numericSoLuong.Value = currRow.Quantity;
            }
            Rectangle cell = tableGioHang.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            Point viTri = this.PointToClient(tableGioHang.PointToScreen(cell.Location));
            panelAction.Location = new Point(panelAction.Location.X, viTri.Y);
            panelAction.Visible = true;
        }

        private void AnActionNeuChuotRaNgoai(object? sender, EventArgs e)
        {
            Point chuot = Cursor.Position;

            bool trongBang = tableGioHang.ClientRectangle.Contains(
                tableGioHang.PointToClient(chuot));

            bool trongPanel = panelAction.Visible &&
                panelAction.ClientRectangle.Contains(
                    panelAction.PointToClient(chuot));

            if (!trongBang && !trongPanel)
            {
                panelAction.Visible = false;
                tableGioHang.ClearSelection();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (tableGioHang.SelectedRows.Count > 0 && tableGioHang.SelectedRows[0].DataBoundItem is RowData row)
            {
                gioHang.Remove(row);
                DemSoThuTu();
                panelAction.Visible = false;
            }
        }
        private void DemSoThuTu()
        {
            for (int i = 0; i < gioHang.Count; i++)
            {
                gioHang[i].No = i + 1;
                gioHang.ResetItem(i);
            }
        }

        private void boxTimKiem_TextChanged(object sender, EventArgs e)
        {
            string text = boxTimKiem.Text.ToLower();
            if (!string.IsNullOrWhiteSpace(text))
            {
                Dictionary<int, Medicine> listData = new();
                if (danhSachThuoc == null) return;
                foreach (var item in danhSachThuoc)
                {
                    if (item.Key.ToString().Contains(text) || item.Value.Name.ToString().ToLower().Contains(text))
                    {
                        listData.Add(item.Key, item.Value);
                    }
                }
                if (listData.Count == 0)
                {
                    tableTimKiem.Visible = false;
                    return;
                }
                FillTableTimKiem(listData);
                if (listData.Count >= 4)
                {
                    tableTimKiem.Size = new Size(tableTimKiem.Size.Width, 35 * 4);
                }
                else tableTimKiem.Size = new Size(tableTimKiem.Size.Width, 35 * listData.Count);
            }
            if (string.IsNullOrWhiteSpace(text))
            {
                tableTimKiem.Visible = false;
            }

        }
        private void ResizeTable()
        {

        }
        private void FillTableTimKiem(Dictionary<int, Medicine> listData)
        {
            timKiem.Clear();
            foreach (var item in listData)
            {
                timKiem.Add(item.Value);
            }
            tableTimKiem.Visible = true;

        }

        private void frmBanHang_Load(object sender, EventArgs e)
        {

        }

        private void tableTimKiem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            if (tableTimKiem.Columns[e.ColumnIndex].Name != "btnThemGioHang")
            {
                return;
            }
            if (tableTimKiem.Rows[e.RowIndex].DataBoundItem is not Medicine medicine)
            {
                return;
            }
            var existingRow = gioHang.FirstOrDefault(row => row.Id == medicine.Id);
            if (existingRow != null)
            {
                existingRow.Quantity++;
                gioHang.ResetItem(gioHang.IndexOf(existingRow));
            }
            else
            {
                gioHang.Add(new RowData
                {
                    Id = medicine.Id,
                    No = gioHang.Count + 1,
                    Name = medicine.Name,
                    Unit = medicine.Unit,
                    Quantity = 1,
                    Price = medicine.Price
                });
            }
            boxTimKiem.Text = "";
            tableTimKiem.Visible = false;
        }

        private void tableTimKiem_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            tableTimKiem.ClearSelection();
            tableTimKiem.CurrentCell = null;
        }

        private void tableTimKiem_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            var row = tableTimKiem.Rows[e.RowIndex];
            if (row.IsNewRow) return;

            if (!row.Selected)
            {
                tableTimKiem.ClearSelection();
                row.Selected = true;
            }
        }

        private void btnXoaTimKiem_Click(object sender, EventArgs e)
        {
            boxTimKiem.Text = "";
            tableTimKiem.Visible = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (tableGioHang.SelectedRows.Count > 0 && tableGioHang.SelectedRows[0].DataBoundItem is RowData row)
            {

            }
        }
    }
}
