using Store.BLL;
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Store.POS
{
    public partial class frmBanHang : Form
    {
        private readonly BindingList<RowData> gioHang = new();
        private readonly BindingList<Medicine> timKiem = new();
        private readonly MedicineDAL medicineDAL = new();
        private readonly MedicineBLL medicineBLL = new();
        private readonly BatchDAL batchDAL = new();
        private readonly Dictionary<int, Medicine> danhSachThuoc = new();
        private readonly Dictionary<Medicine, int> danhSachGioHang = new();
        private bool isAddedVoucher = false;
        private readonly CustomerDAL customerDAL = new();
        private bool isAddedCustomer = false;
        private decimal tongTien;
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

            timerDateTime.Start();
            UpdateDateTime();
        }
        private void timerDateTime_Tick(object sender, EventArgs e)
        {
            UpdateDateTime();
        }
        private void UpdateDateTime()
        {
            lblDateTime.Text = "Thời gian: " + DateTime.Now.ToString("HH:mm:ss") + " | " + DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void frmBanHang_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                this.Close();
            }
        }
        private void tableGioHang_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            tableGioHang.ClearSelection();
            tableGioHang.CurrentCell = null;
        }

        private void frmBanHang_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void frmBanHang_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        // Chức năng tìm kiếm
        private void tableTimKiem_CellContentClick(object sender, DataGridViewCellEventArgs e)///Logic
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            if (tableTimKiem.Rows[e.RowIndex].DataBoundItem is not Medicine medicine)
            {
                return;
            }
            ThemSanPhamVaoGioHang(medicine);
            CapNhatGiaTien();
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
                    tableTimKiem.Size = new Size(tableTimKiem.Size.Width, 33 * 4);
                }
                else tableTimKiem.Size = new Size(tableTimKiem.Size.Width, 33 * listData.Count);
            }
            if (string.IsNullOrWhiteSpace(text))
            {
                tableTimKiem.Visible = false;
            }

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
        private void btnXoaTimKiem_Click(object sender, EventArgs e)
        {
            boxTimKiem.Text = "";
            tableTimKiem.Visible = false;
        }

        // Giỏ hàng
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
        // Thao tác sản phẩm trên giỏ hàng
        private void numericSoLuong_KeyDown(object sender, KeyEventArgs e)
        {
            if (tableGioHang.SelectedRows.Count < 0)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                CapNhatSoLuongGioHang();
                CapNhatGiaTien();
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
        private void btnSua_Click(object sender, EventArgs e)
        {
            CapNhatSoLuongGioHang();
            CapNhatGiaTien();
            panelAction.Visible = false;
        }
        private void btnNhapSanPham_Click(object sender, EventArgs e)
        {
            NhapGioHang();
        }
        private void boxNhapSanPham_KeyDown(object sender, KeyEventArgs e)///Logic
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrWhiteSpace(boxNhapSanPham.Text.Trim()))
                {
                    MessageBox.Show("Mã sản phẩm không được để trống!", "Cảnh báo");
                    return;
                }
                NhapGioHang();
                CapNhatGiaTien();
            }
        }
        // Hàm hỗ trợ/tái sử dụng
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
        private void DemSoThuTu()
        {
            for (int i = 0; i < gioHang.Count; i++)
            {
                gioHang[i].No = i + 1;
                gioHang.ResetItem(i);
            }
        }
        private void ThemSanPhamVaoGioHang(Medicine medicine)
        {
            var existingRow = gioHang.FirstOrDefault(row => row.Id == medicine.Id);
            if (existingRow != null)
            {
                try
                {
                    medicineBLL.KiemTraSoLuongConHan(medicine, existingRow.Quantity + 1);
                    existingRow.Quantity++;
                    gioHang.ResetItem(gioHang.IndexOf(existingRow));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
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
        }
        private void CapNhatSoLuongGioHang()///Logic
        {
            if (tableGioHang.SelectedRows[0].DataBoundItem is RowData row)
            {
                int soLuong = int.Parse(numericSoLuong.Value.ToString());
                if (soLuong == 0)
                {
                    gioHang.Remove(row);
                }
                int totalStock = medicineDAL.SoLuongConHan(row.Id);
                if (totalStock == -1)
                {
                    MessageBox.Show("Vui lòng kiểm tra lại!");
                    return;
                }
                try
                {
                    medicineBLL.KiemTraSoLuongConHan(row.Id, soLuong);
                    row.Quantity = soLuong;
                    gioHang.ResetItem(gioHang.IndexOf(row));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            panelAction.Visible = false;
        }
        private void NhapGioHang()
        {
            string text = boxNhapSanPham.Text.Trim();
            if (int.TryParse(text, out int ma))
            {
                Medicine medicine = danhSachThuoc.FirstOrDefault(med => med.Key == ma).Value;
                if (medicine == null)
                {
                    MessageBox.Show("Không tìm thấy sản phẩm!", "Cảnh báo");
                    boxNhapSanPham.SelectAll();
                    boxNhapSanPham.Focus();
                    return;
                }
                ThemSanPhamVaoGioHang(medicine);
                CapNhatGiaTien();
                boxNhapSanPham.Clear();
                boxNhapSanPham.Focus();
            }
            else
            {
                MessageBox.Show("Mã sản phẩm không hợp lệ!");
                boxNhapSanPham.SelectAll();
                boxNhapSanPham.Focus();
                return;
            }
        }


        // Thanh toán
        public void CapNhatGiaTien()
        {
            decimal tamTinh = 0;
            tongTien = 0;
            //Voucher
            foreach (var row in gioHang)
            {
                tamTinh += row.Total;
            }
            labelTienTamTinh.Text = tamTinh + " đ";
            if (isAddedVoucher)
            {

            }
            if (isAddedCustomer)
            {

            }
            tongTien = tamTinh /* - voucher*/;
            labelTienTongTien.Text = tongTien + " đ";
        }
        private void ThemKhachHang()// Note
        {
            string text = boxMaKhachHang.Text.ToString();
            if (!ValidatePhone(text))
            {
                MessageBox.Show("Số điện thoại không hợp lệ!");
                return;
            }
            Customer? _customer = customerDAL.GetCustomerByPhone(text);
            if (_customer == null)
            {
                DialogResult option = MessageBox.Show("Không tìm thấy khách hàng, bạn có muốn thêm mới?", "Lỗi", MessageBoxButtons.YesNo);
                if (option == DialogResult.No)
                {
                    boxMaKhachHang.Clear();
                    return;
                }
                else
                {
                    MessageBox.Show("Thêm mới khách hàng");
                }
            }
        }


        private bool ValidatePhone(string phone)
        {
            return Regex.IsMatch(phone, @"^0[35789]\d{8}$");
        }

        private void tableGioHang_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            CapNhatGiaTien();
        }

        private void tableGioHang_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CapNhatGiaTien();
        }

        private void btnNhapKhachHang_Click(object sender, EventArgs e)
        {
            ThemKhachHang();
        }

        private void btnHuyHoaDon_Click(object sender, EventArgs e)
        {
            DialogResult option = MessageBox.Show("Bạn có muốn hủy hóa đơn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2);
            if (option == DialogResult.Yes)
            {
                gioHang.Clear();
                radioChuyenKhoan.Checked = false;
                radioTienMat.Checked = false;
            }
        }

        private void boxTienKhach_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void boxTienKhach_TextChanged(object sender, EventArgs e)
        {
            string input = boxTienKhach.Text.Trim();
            if (decimal.TryParse(input, out decimal tienKhachDua))
            {
                if (tienKhachDua >= tongTien)
                {
                    decimal tienTraLai = tienKhachDua - tongTien;
                    boxTraLai.Text = tienTraLai + "";
                }
                else
                {
                    boxTraLai.Text = "Chưa đủ tiền";
                }
            }
            else
            {
                boxTraLai.Text = "";
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (gioHang.Count == 0)
            {
                MessageBox.Show("Giỏ hàng đang trống!");
                return;
            }
            if (radioChuyenKhoan.Checked == false && radioTienMat.Checked == false)
            {
                MessageBox.Show("Chưa chọn phương thức thanh toán!");
                return;
            }
            if (radioTienMat.Checked == true)
            {
                if (string.IsNullOrEmpty(boxTienKhach.Text))
                {
                    MessageBox.Show("Vui lòng nhập số tiền!");
                    return;
                }
                if (tongTien > decimal.Parse(boxTienKhach.Text))
                {
                    MessageBox.Show("Chưa đủ tiền!");
                    return;
                }
                else
                {
                    LuuHoaDon();
                }
            }
            if (radioChuyenKhoan.Checked == true)
            {
                using (var frmQR = new frmThanhToanQR(tongTien))
                {
                    if (frmQR.ShowDialog(this) == DialogResult.OK)
                    {
                        LuuHoaDon();
                    }
                    else
                    {

                    }
                }
            }

        }
        private void LuuHoaDon()
        {
            MessageBox.Show("Thanh toán thành công");
            gioHang.Clear();
            boxTienKhach.Clear();
        }

        private void radioTienMat_CheckedChanged(object sender, EventArgs e)
        {
            boxTienKhach.Enabled = false;
            panelTienMat.Visible = false;
        }

        private void radioTienMat_Click(object sender, EventArgs e)
        {
            if (gioHang.Count == 0)
            {
                radioTienMat.Checked = false;
                return;
            }
            boxTienKhach.Enabled = true;
            panelTienMat.Visible = true;
        }

        private void radioChuyenKhoan_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioChuyenKhoan_Click(object sender, EventArgs e)
        {
            if (gioHang.Count == 0)
            {
                radioChuyenKhoan.Checked = false;
                return;
            }
        }
    }
}
