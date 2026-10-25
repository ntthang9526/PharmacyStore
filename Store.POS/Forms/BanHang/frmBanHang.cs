using Store.Shared;
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
    public partial class frmBanHang : Form, ILamMoiDuLieu
    {
        private readonly BindingList<RowData> gioHang = new();
        private readonly BindingList<Medicine> timKiem = new();
        private readonly MedicineDAL medicineDAL = new();
        private readonly MedicineBLL medicineBLL = new();
        private readonly BatchDAL batchDAL = new();
        private Dictionary<int, Medicine> danhSachThuoc = new();
        private readonly Dictionary<Medicine, int> danhSachGioHang = new();
        private VoucherInfo? voucherApDung;
        private readonly VoucherBLL voucherBLL = new();
        private readonly BanHangBLL banHangBLL = new();
        private readonly CustomerDAL customerDAL = new();
        private Customer? khachHang;
        private decimal tongTien;
        private RowData? dongDangThaoTac;
        private readonly System.Windows.Forms.Timer timerAnPanelAction;
        public frmBanHang()
        {
            InitializeComponent();
            btnLuuCho.Click+=(_,_)=>LuuHoaDon("Chưa xác định",true);
            timerAnPanelAction=new System.Windows.Forms.Timer(components ??= new Container()) {Interval=180};
            timerAnPanelAction.Tick+=(_,_)=> {timerAnPanelAction.Stop();KiemTraAnPanelAction(Cursor.Position);};
            void WireActionHover(Control control)
            {
                control.MouseEnter+=(_,_)=>timerAnPanelAction.Stop();
                control.MouseLeave+=AnActionNeuChuotRaNgoai;
                foreach(Control child in control.Controls) WireActionHover(child);
            }
            WireActionHover(panelAction);
            tableGioHang.MouseLeave+=AnActionNeuChuotRaNgoai;
            tableGioHang.CellMouseLeave+=(_,_)=>AnActionNeuChuotRaNgoai(tableGioHang,EventArgs.Empty);
            btnNhapMaVoucher.Click += (_, _) => ApDungVoucher();
            btnHuyVoucher.Click += (_, _) => { voucherApDung = null; boxMaVoucher.Clear(); CapNhatGiaTien(); };
            boxMaVoucher.TextChanged += (_, _) => { if (voucherApDung != null && !string.Equals(boxMaVoucher.Text.Trim(), voucherApDung.Code, StringComparison.OrdinalIgnoreCase)) { voucherApDung = null; CapNhatGiaTien(); } };
            btnHuyKhachHang.Click += (_, _) => { khachHang = null; boxMaKhachHang.Clear(); labelMaKhachHang.Text = "SĐT khách hàng"; };
            boxMaKhachHang.TextChanged += (_, _) => { khachHang = null; labelMaKhachHang.Text = "SĐT khách hàng"; };
            Load += frmBanHang_Load;
            groupThanhToan.LocationChanged += (_,_) => { if(panelAction.Visible) DinhViPanelAction(); };
            groupThanhToan.SizeChanged += (_,_) => { if(panelAction.Visible) DinhViPanelAction(); };
            VisibleChanged += (_,_) => { if(panelAction.Visible) DinhViPanelAction(); };
            tableGioHang.Scroll += (_,_) => { if(panelAction.Visible) DinhViPanelAction(); };
        }
        private void frmBanHang_Load(object? sender, EventArgs e)
        {
            if (PosFormStyle.IsInDesigner(this)) return;
            labelNhanVien.Text = "Nhân viên: " + PhienDangNhap.HoTen;
            danhSachThuoc = medicineDAL.GetMedicines();
            if (danhSachThuoc == null)
            {
                MessageBox.Show("Không tải được danh sách thuốc.");
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
            if(dongDangThaoTac!=null)
                foreach(DataGridViewRow row in tableGioHang.Rows)
                    if(ReferenceEquals(row.DataBoundItem,dongDangThaoTac)) {row.Selected=true;break;}
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
            string text = boxTimKiem.Text.Trim().ToLowerInvariant();
            if (!string.IsNullOrWhiteSpace(text))
            {
                Dictionary<int, Medicine> listData = new();
                if (danhSachThuoc == null) return;
                foreach (var item in danhSachThuoc)
                {
                    if (item.Key.ToString().Contains(text) || item.Value.Name.Contains(text, StringComparison.OrdinalIgnoreCase) ||
                        (item.Value.BarCode?.Contains(text, StringComparison.OrdinalIgnoreCase) ?? false))
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
            tableTimKiem.Location=groupGioHang.PointToClient(panel1.PointToScreen(new Point(0,panel1.Height)));
            tableTimKiem.Width=panel1.Width;tableTimKiem.BringToFront();
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
        private void tableGioHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex>=0 && e.ColumnIndex>=0) HienThaoTacGioHang(e.RowIndex);
        }
        private void tableGioHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex>=0 && e.ColumnIndex>=0 && tableGioHang.Columns[e.ColumnIndex].Name=="btnAction")
                HienThaoTacGioHang(e.RowIndex);
        }
        private void tableGioHang_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex>=0 && e.ColumnIndex>=0 && tableGioHang.Columns[e.ColumnIndex].Name=="btnAction")
                HienThaoTacGioHang(e.RowIndex);
            else AnActionNeuChuotRaNgoai(tableGioHang,EventArgs.Empty);
        }
        private void AnActionNeuChuotRaNgoai(object? sender,EventArgs e)
        {
            if(panelAction.Visible) {timerAnPanelAction.Stop();timerAnPanelAction.Start();}
        }
        private void KiemTraAnPanelAction(Point mousePosition)
        {
            if(!panelAction.Visible || panelAction.RectangleToScreen(panelAction.ClientRectangle).Contains(mousePosition)) return;
            var row=tableGioHang.Rows.Cast<DataGridViewRow>().FirstOrDefault(r=>ReferenceEquals(r.DataBoundItem,dongDangThaoTac));
            if(row!=null)
            {
                var button=tableGioHang.GetCellDisplayRectangle(tableGioHang.Columns["btnAction"].Index,row.Index,true);
                if(!button.IsEmpty && tableGioHang.RectangleToScreen(button).Contains(mousePosition)) return;
            }
            AnThaoTacGioHang();
        }
        private void HienThaoTacGioHang(int index)
        {
            if(index<0 || index>=tableGioHang.Rows.Count || tableGioHang.Rows[index].DataBoundItem is not RowData row) return;
            tableGioHang.ClearSelection();tableGioHang.Rows[index].Selected=true;
            if(!ReferenceEquals(dongDangThaoTac,row)) numericSoLuong.Value=row.Quantity;
            timerAnPanelAction.Stop();dongDangThaoTac=row;DinhViPanelAction();panelAction.Visible=true;
        }
        private void DinhViPanelAction()
        {
            if(dongDangThaoTac==null) return;
            var row=tableGioHang.Rows.Cast<DataGridViewRow>().FirstOrDefault(r=>ReferenceEquals(r.DataBoundItem,dongDangThaoTac));
            if(row==null) return;
            var cell=tableGioHang.GetCellDisplayRectangle(tableGioHang.Columns["btnAction"].Index,row.Index,true);
            var rowPosition=PointToClient(tableGioHang.PointToScreen(cell.Location));
            var paymentPosition=PointToClient(groupThanhToan.PointToScreen(Point.Empty));
            int top=paymentPosition.Y+20;
            int bottom=Math.Max(top,paymentPosition.Y+groupThanhToan.Height-panelAction.Height-4);
            panelAction.Location=new Point(paymentPosition.X+4,Math.Clamp(rowPosition.Y,top,bottom));
            panelAction.BringToFront();
        }
        private void AnThaoTacGioHang()
        {
            timerAnPanelAction.Stop();dongDangThaoTac=null;panelAction.Visible=false;
            tableGioHang.ClearSelection();tableGioHang.CurrentCell=null;
        }
        private void numericSoLuong_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                e.SuppressKeyPress=true;
                CapNhatSoLuongGioHang();CapNhatGiaTien();
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if(dongDangThaoTac==null || !gioHang.Contains(dongDangThaoTac)) {AnThaoTacGioHang();return;}
            var row=dongDangThaoTac;AnThaoTacGioHang();gioHang.Remove(row);
            DemSoThuTu();CapNhatGiaTien();
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            CapNhatSoLuongGioHang();CapNhatGiaTien();
        }
        private void btnNhapSanPham_Click(object sender, EventArgs e)
        {
            NhapGioHang();
            CapNhatGiaTien();
        }
        private void boxNhapSanPham_KeyDown(object sender, KeyEventArgs e)///Logic
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
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
            try { medicineBLL.KiemTraSoLuongConHan(medicine, 1); }
            catch (Exception ex) { MessageBox.Show(ex.Message); return; }
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
        private void CapNhatSoLuongGioHang()
        {
            if(dongDangThaoTac==null || !gioHang.Contains(dongDangThaoTac)) {AnThaoTacGioHang();return;}
            var row=dongDangThaoTac;int quantity=(int)numericSoLuong.Value;
            if(quantity==0) {AnThaoTacGioHang();gioHang.Remove(row);DemSoThuTu();return;}
            try
            {
                medicineBLL.KiemTraSoLuongConHan(row.Id,quantity);
                row.Quantity=quantity;gioHang.ResetItem(gioHang.IndexOf(row));AnThaoTacGioHang();
            }
            catch(Exception ex) {MessageBox.Show(ex.Message,"Số lượng");}
        }
        private void NhapGioHang()
        {
            string text = boxNhapSanPham.Text.Trim();
            var barcodeMatches = danhSachThuoc.Values.Where(m =>
                !string.IsNullOrWhiteSpace(m.BarCode) && string.Equals(m.BarCode.Trim(), text, StringComparison.OrdinalIgnoreCase)).ToList();
            if (barcodeMatches.Count > 1)
            {
                MessageBox.Show("Mã vạch đang trùng ở nhiều thuốc. Hãy tìm và chọn đúng thuốc trong danh sách.", "Cảnh báo");
                boxTimKiem.Text = text;
                boxTimKiem.Focus();
                return;
            }
            Medicine? medicine = barcodeMatches.SingleOrDefault();
            if (medicine == null && int.TryParse(text, out int ma)) danhSachThuoc.TryGetValue(ma, out medicine);
            if (medicine == null)
            {
                MessageBox.Show("Không tìm thấy thuốc theo mã thuốc hoặc mã vạch!", "Cảnh báo");
                boxNhapSanPham.SelectAll();
                boxNhapSanPham.Focus();
                return;
            }
            ThemSanPhamVaoGioHang(medicine);
            CapNhatGiaTien();
            boxNhapSanPham.Clear();
            boxNhapSanPham.Focus();
        }


        // Thanh toán
        public void CapNhatGiaTien()
        {
            if(dongDangThaoTac!=null && !gioHang.Contains(dongDangThaoTac)) AnThaoTacGioHang();
            decimal subtotal = gioHang.Sum(row => row.Total);
            decimal discount = voucherApDung == null ? 0 : voucherBLL.TinhGiamGia(voucherApDung, subtotal);
            tongTien = subtotal - discount;
            labelTienTamTinh.Text = $"{subtotal:N0} đ";
            labelTienGiamGia.Text = $"{discount:N0} đ";
            labelTienTongTien.Text = $"{tongTien:N0} đ";
            boxTienKhach_TextChanged(this, EventArgs.Empty);
            KiemTraGioHangHienTai();
        }

        private void ApDungVoucher()
        {
            if (voucherBLL.KiemTra(boxMaVoucher.Text, out var voucher, out string error))
                voucherApDung = voucher;
            else { voucherApDung = null; MessageBox.Show(error, "Voucher"); }
            CapNhatGiaTien();
        }
        private void ThemKhachHang()
        {
            string phone = boxMaKhachHang.Text.Trim();
            if (!ValidatePhone(phone)) { MessageBox.Show(DanhMucValidation.CustomerPhoneError(phone)); return; }
            try
            {
                khachHang = customerDAL.GetCustomerByPhone(phone);
                if (khachHang == null && MessageBox.Show("Không tìm thấy khách hàng. Mở quản lý để thêm mới?", "Khách hàng", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using var form = new frmKhachHang();
                    form.ShowDialog(this);
                    khachHang = customerDAL.GetCustomerByPhone(phone);
                }
                labelMaKhachHang.Text = khachHang == null ? "SĐT khách hàng" : khachHang.FullName;
            }
            catch (Exception ex) { MessageBox.Show("Không tra cứu được khách hàng: " + ex.Message); }
        }


        private bool ValidatePhone(string phone)
        {
            return DanhMucValidation.CustomerPhoneError(phone) == null;
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
                LamMoiHoaDon();
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
            if (decimal.TryParse(input, System.Globalization.NumberStyles.None, System.Globalization.CultureInfo.InvariantCulture, out decimal tienKhachDua))
            {
                if (tienKhachDua >= tongTien)
                {
                    decimal tienTraLai = tienKhachDua - tongTien;
                    boxTraLai.Text = tienTraLai.ToString("N0");
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
            try
            {
                decimal previous=tongTien;LamMoiDuLieu();
                if(previous!=tongTien) {MessageBox.Show("Giá thuốc hoặc voucher đã thay đổi. Hãy kiểm tra tổng tiền mới và bấm thanh toán lại.","Đã cập nhật dữ liệu");return;}
                if(gioHang.Count>0 && !btnThanhToan.Enabled) {MessageBox.Show(lblTinhTrangGioHang.Text,"Kiểm tra giỏ hàng");return;}
            }
            catch(Exception ex) {MessageBox.Show("Không kiểm tra được giỏ hàng: "+ex.Message);return;}
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
                if (!decimal.TryParse(boxTienKhach.Text.Trim(), System.Globalization.NumberStyles.None, System.Globalization.CultureInfo.InvariantCulture, out decimal received) || tongTien > received)
                {
                    MessageBox.Show("Chưa đủ tiền!");
                    return;
                }
                else
                {
                    LuuHoaDon("Tiền mặt");
                }
            }
            if (radioChuyenKhoan.Checked == true)
            {
                using (var frmQR = new frmThanhToanQR(tongTien))
                {
                    if (frmQR.ShowDialog(this) == DialogResult.OK)
                    {
                        LuuHoaDon("Chuyển khoản QR");
                    }
                    else
                    {

                    }
                }
            }

        }
        private void LuuHoaDon(string method="Tiền mặt",bool pending=false)
        {
            var items = gioHang.Select(row => new CartItem { MaThuoc = row.Id, TenThuoc = row.Name, SoLuong = row.Quantity, DonGia = row.Price }).ToList();
            if (!banHangBLL.ThanhToan(PhienDangNhap.NhanVienId, items, voucherApDung?.Code, khachHang?.Id, tongTien,
                    out int orderId, out decimal subtotal, out decimal discount, out string error,method,pending))
            {
                MessageBox.Show("Chưa lưu hóa đơn: " + error, "Thanh toán");
                return;
            }
            MessageBox.Show(pending?$"Đã lưu HD{orderId:D5} chờ thanh toán. Chưa trừ kho và chưa tính doanh thu.":$"Đã thanh toán HD{orderId:D5} ({method}). Thực thu: {subtotal-discount:N0} đ", "Thành công");
            LamMoiHoaDon();
        }

        private void LamMoiHoaDon()
        {
            voucherApDung = null;
            khachHang = null;
            gioHang.Clear();
            boxMaVoucher.Clear();
            boxMaKhachHang.Clear();
            labelMaKhachHang.Text = "SĐT khách hàng";
            boxTienKhach.Clear();
            radioChuyenKhoan.Checked = false;
            radioTienMat.Checked = false;
            panelTienMat.Visible = false;
            CapNhatGiaTien();
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
