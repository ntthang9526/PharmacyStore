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
        public frmBanHang():this(null) {}
        public frmBanHang(int? suaId)
        {
            suaHoaDonId=suaId;
            InitializeComponent();
            global::Store.POS.PosFormStyle.ConfigureDisabledButtons(this);
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
            tableGioHang.CellFormatting+=(_,e)=> {if(e.RowIndex>=0 && tableGioHang.Columns[e.ColumnIndex].DataPropertyName=="Name" && tableGioHang.Rows[e.RowIndex].DataBoundItem is RowData row && row.IsPrescription) e.Value=row.Name+" [Kê đơn]";};
            tableTimKiem.CellFormatting+=(_,e)=> {if(e.RowIndex>=0 && tableTimKiem.Columns[e.ColumnIndex].DataPropertyName=="Name" && tableTimKiem.Rows[e.RowIndex].DataBoundItem is Medicine m && m.IsPrescription) e.Value=m.Name+" [Kê đơn]";};
            tableGioHang.MouseLeave+=AnActionNeuChuotRaNgoai;
            tableGioHang.CellMouseLeave+=(_,_)=>AnActionNeuChuotRaNgoai(tableGioHang,EventArgs.Empty);
            btnNhapMaVoucher.Click += (_, _) => ApDungVoucher();
            btnHuyVoucher.Click += (_, _) => { voucherApDung = null; boxMaVoucher.Clear(); CapNhatThongTinVoucher(); CapNhatGiaTien(); };
            boxMaVoucher.TextChanged += (_, _) => { if (voucherApDung != null && !string.Equals(boxMaVoucher.Text.Trim(), voucherApDung.Code, StringComparison.OrdinalIgnoreCase)) { voucherApDung = null; CapNhatThongTinVoucher(); CapNhatGiaTien(); } };
            boxMaVoucher.KeyDown += (_, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    ApDungVoucher();
                }
            };
            btnHuyKhachHang.Click += (_, _) => { khachHang = null; boxMaKhachHang.Clear(); CapNhatThongTinKhachHang(); };
            boxMaKhachHang.TextChanged += (_, _) =>
            {
                if (khachHang != null && !string.Equals(boxMaKhachHang.Text.Trim(), khachHang.Phone, StringComparison.OrdinalIgnoreCase))
                {
                    khachHang = null;
                    CapNhatThongTinKhachHang();
                }
            };
            boxMaKhachHang.KeyDown += (_, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    ThemKhachHang();
                }
            };
            Load += frmBanHang_Load;
            groupThanhToan.LocationChanged += (_,_) => { if(panelAction.Visible) DinhViPanelAction(); };
            groupThanhToan.SizeChanged += (_,_) => { if(panelAction.Visible) DinhViPanelAction(); };
            VisibleChanged += (_,_) => { if(panelAction.Visible) DinhViPanelAction(); };
            tableGioHang.Scroll += (_,_) => { if(panelAction.Visible) DinhViPanelAction(); };
            if (!PosFormStyle.IsInDesigner(this))
            {
                PosFormStyle.ApplyTableStyle(tableGioHang);
                PosFormStyle.ApplyTableStyle(tableTimKiem);
                CapNhatThongTinKhachHang();
                CapNhatThongTinVoucher();
            }
        }
        private async void frmBanHang_Load(object? sender, EventArgs e)
        {
            if (PosFormStyle.IsInDesigner(this)) return;
            labelNhanVien.Text = "Nhân viên: " + PhienDangNhap.HoTen;
            lblTinhTrangGioHang.Text="Đang tải thuốc…";
            if(!await PosBackground.LoadAsync(this,medicineDAL.GetMedicines,data=>danhSachThuoc=data,error=>MessageBox.Show(this,"Không tải được thuốc: "+error.Message,"Bán hàng"))) return;
            if (danhSachThuoc == null)
            {
                MessageBox.Show("Không tải được danh sách thuốc.");
                return;
            }
            tableGioHang.DataSource = gioHang;
            tableTimKiem.DataSource = timKiem;
            try {NapHoaDonCho();}
            catch(Exception ex) {MessageBox.Show(this,ex.Message,"Không mở được đơn chờ");BeginInvoke(new Action(Close));return;}
            GhiNhanThuocKeDon();
            gioHang.ListChanged+=(_,_)=>KiemTraThayDoiThuocKeDon();
            CapNhatGiaTien();
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

        private void tableGioHang_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            tableGioHang.ClearSelection();
            tableGioHang.CurrentCell = null;
            if(dongDangThaoTac!=null)
                foreach(DataGridViewRow row in tableGioHang.Rows)
                    if(ReferenceEquals(row.DataBoundItem,dongDangThaoTac)) {row.Selected=true;break;}
        }

        internal string? CanhBaoChuaLuu => gioHang.Count>0
            ? $"Giỏ hàng bán hàng: {gioHang.Count} dòng thuốc chưa lưu."
            : (!string.IsNullOrWhiteSpace(boxMaKhachHang.Text) || !string.IsNullOrWhiteSpace(boxMaVoucher.Text) || !string.IsNullOrWhiteSpace(boxTienKhach.Text))
                ? "Thông tin bán hàng đang nhập chưa lưu." : null;

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
                        (item.Value.BarCode?.Contains(text, StringComparison.OrdinalIgnoreCase) ?? false) ||
                        (item.Value.ActiveIngredient?.Contains(text, StringComparison.OrdinalIgnoreCase) ?? false))
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
            var cartBounds=RectangleToClient(groupGioHang.RectangleToScreen(groupGioHang.ClientRectangle));
            int left=Math.Clamp(rowPosition.X+cell.Width-panelAction.Width,cartBounds.Left+6,Math.Max(cartBounds.Left+6,cartBounds.Right-panelAction.Width-6));
            int top=rowPosition.Y+cell.Height;
            if(top+panelAction.Height>cartBounds.Bottom-8) top=rowPosition.Y-panelAction.Height;
            top=Math.Clamp(top,cartBounds.Top+28,Math.Max(cartBounds.Top+28,cartBounds.Bottom-panelAction.Height-8));
            panelAction.Location=new Point(left,top);
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
                    MessageBox.Show("Nhập mã thuốc hoặc mã vạch.", "Cảnh báo");
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
            try { if(!suaHoaDonId.HasValue) KiemTraTonTrongGio(medicine.Id,1); }
            catch (Exception ex) { MessageBox.Show(ex.Message); return; }
            var existingRow = gioHang.FirstOrDefault(row => row.Id == medicine.Id);
            if (existingRow != null)
            {
                try
                {
                    if(!suaHoaDonId.HasValue) KiemTraTonTrongGio(medicine.Id,existingRow.Quantity+1);
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
                    Price = medicine.Price, IsPrescription=medicine.IsPrescription
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
                if(!suaHoaDonId.HasValue) KiemTraTonTrongGio(row.Id,quantity);
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
                MessageBox.Show("Mã vạch bị trùng. Chọn thuốc trong danh sách.", "Cảnh báo");
                boxTimKiem.Text = text;
                boxTimKiem.Focus();
                return;
            }
            Medicine? medicine = barcodeMatches.SingleOrDefault();
            if (medicine == null && int.TryParse(text, out int ma)) danhSachThuoc.TryGetValue(ma, out medicine);
            if (medicine == null)
            {
                MessageBox.Show("Không tìm thấy thuốc với mã này.", "Cảnh báo");
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
            if(gioHang.Count==0) donThuoc=null;
            if(dongDangThaoTac!=null && !gioHang.Contains(dongDangThaoTac)) AnThaoTacGioHang();
            decimal subtotal = gioHang.Sum(row => row.Total);
            decimal discount = voucherApDung == null ? 0 : voucherBLL.TinhGiamGia(voucherApDung, subtotal);
            tongTien = subtotal - discount;
            labelTienTamTinh.Text = $"{subtotal:N0} đ";
            labelTienGiamGia.Text = $"{discount:N0} đ";
            labelTienTongTien.Text = $"{tongTien:N0} đ";
            boxTienKhach_TextChanged(this, EventArgs.Empty);
            KiemTraGioHangHienTai();
            CapNhatNutDonThuoc();
        }

        private void ApDungVoucher()
        {
            if (voucherBLL.KiemTra(boxMaVoucher.Text, out var voucher, out string error))
                voucherApDung = voucher;
            else { voucherApDung = null; MessageBox.Show(error, "Voucher"); }
            CapNhatThongTinVoucher();
            CapNhatGiaTien();
        }
        private void CapNhatThongTinVoucher()
        {
            lblMaVoucher.Text = "Mã voucher";
        }
        private void CapNhatThongTinKhachHang()
        {
            labelMaKhachHang.Text = khachHang?.FullName ?? "SĐT khách hàng";
        }
        private void ThemKhachHang()
        {
            string phone = boxMaKhachHang.Text.Trim();
            if (!ValidatePhone(phone)) { MessageBox.Show(DanhMucValidation.CustomerPhoneError(phone)); return; }
            try
            {
                khachHang = customerDAL.GetCustomerByPhone(phone);
                if (khachHang == null && MessageBox.Show("Chưa có khách hàng này. Thêm mới?", "Khách hàng", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using var form = new frmKhachHang(phone);
                    if(form.ShowDialog(this)==DialogResult.OK && form.CreatedPhone is string savedPhone)
                    {
                        boxMaKhachHang.Text=savedPhone;
                        khachHang=customerDAL.GetCustomerByPhone(savedPhone);
                    }
                }
                CapNhatThongTinKhachHang();
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
            if(suaHoaDonId.HasValue) {Close();return;}
            DialogResult option = MessageBox.Show("Hủy giỏ hàng hiện tại?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2);
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

        private async void btnThanhToan_Click(object sender, EventArgs e)
        {
            try
            {
                decimal previous=tongTien;if(!await LamMoiDuLieuAsync()) return;
                if(previous!=tongTien) {MessageBox.Show("Giá hoặc voucher đã đổi. Kiểm tra tổng tiền rồi thanh toán lại.","Đã cập nhật dữ liệu");return;}
                if(gioHang.Count>0 && !btnThanhToan.Enabled) {MessageBox.Show(lblTinhTrangGioHang.Text,"Kiểm tra giỏ hàng");return;}
            }
            catch(Exception ex) {MessageBox.Show("Không kiểm tra được giỏ hàng: "+ex.Message);return;}
            if (gioHang.Count == 0)
            {
                MessageBox.Show("Giỏ hàng trống.");
                return;
            }
            if (radioChuyenKhoan.Checked == false && radioTienMat.Checked == false)
            {
                MessageBox.Show("Chọn hình thức thanh toán.");
                return;
            }
            if(!ChuanBiDonThuoc()) return;
            if (radioTienMat.Checked == true)
            {
                if (string.IsNullOrEmpty(boxTienKhach.Text))
                {
                    MessageBox.Show("Nhập tiền khách đưa.");
                    return;
                }
                if (!decimal.TryParse(boxTienKhach.Text.Trim(), System.Globalization.NumberStyles.None, System.Globalization.CultureInfo.InvariantCulture, out decimal received) || tongTien > received)
                {
                    MessageBox.Show("Tiền khách đưa chưa đủ.");
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
            if(!pending && !ChuanBiDonThuoc()) return;
            var items = gioHang.Select(row => new CartItem { MaThuoc = row.Id, TenThuoc = row.Name, SoLuong = row.Quantity, DonGia = row.Price,KeDon=row.IsPrescription,HuongDan=row.HuongDan }).ToList();
            if (!banHangBLL.ThanhToan(PhienDangNhap.NhanVienId, items, voucherApDung?.Code, khachHang?.Id, tongTien,
                    out int orderId, out decimal subtotal, out decimal discount, out string error,method,pending,suaHoaDonId,phienBanDonCho,gioHang.Any(r=>r.IsPrescription)?donThuoc:null))
            {
                MessageBox.Show("Chưa lưu hóa đơn: " + error, "Thanh toán");
                return;
            }
            if(suaHoaDonId.HasValue)
            {
                MessageBox.Show(this,$"Đã lưu thay đổi HD{orderId:D5}.","Đã lưu thay đổi");
                DialogResult=DialogResult.OK;Close();return;
            }
            MessageBox.Show(pending?$"Đã lưu HD{orderId:D5} chờ thanh toán.":$"Đã thanh toán HD{orderId:D5} ({method}). Thực thu: {subtotal-discount:N0} đ", "Thành công");
            LamMoiHoaDon();
        }

        private void LamMoiHoaDon()
        {
            voucherApDung = null;
            donThuoc=null;
            khachHang = null;
            gioHang.Clear();
            boxMaVoucher.Clear();
            boxMaKhachHang.Clear();
            boxTienKhach.Clear();
            radioChuyenKhoan.Checked = false;
            radioTienMat.Checked = false;
            panelTienMat.Visible = false;
            CapNhatThongTinKhachHang();
            CapNhatThongTinVoucher();
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
