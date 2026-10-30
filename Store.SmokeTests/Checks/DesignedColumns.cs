using System.Data;
using System.Reflection;
using Store.POS;
internal static partial class Program
{
    static void TestDesignedColumns()
    {
        (string Form,string Grid,string Field,string Binding,string Header)[] expected={
            ("frmHoaDon","dgvHoaDon","colCanThu","CanThanhToan","Cần thu"),
            ("frmHoaDon","dgvChiTiet","colNguonThongTin","NguonThongTin","Nguồn tên/đơn vị"),
            ("frmPhieuNhap","dgvChiTiet","colNguonThongTin","NguonThongTin","Nguồn tên/đơn vị"),
            ("frmLoHang","dgvLoHang","colTonCoTheBan","TonCoTheBan","Tồn có thể bán"),
            ("frmLoHang","dgvLoHang","colTrangThaiLo","TrangThaiLo","Trạng thái lô"),
            ("frmQuanLyThuoc","dgvThuoc","colTonCoTheBan","TonCoTheBan","Tồn có thể bán"),
            ("frmQuanLyThuoc","dgvThuoc","colTrangThaiBan","TrangThaiBan","Trạng thái bán"),
            ("frmBanHang","tableTimKiem","idDataGridViewTextBoxColumn","Id","Mã"),
            ("frmBanHang","tableTimKiem","nameDataGridViewTextBoxColumn1","Name","Tên thuốc"),
            ("frmBanHang","tableTimKiem","btnThemGioHang","",""),
            ("frmBanHang","tableGioHang","noDataGridViewTextBoxColumn","No","STT"),
            ("frmBanHang","tableGioHang","nameDataGridViewTextBoxColumn","Name","Tên thuốc"),
            ("frmBanHang","tableGioHang","unitDataGridViewTextBoxColumn","Unit","Đơn vị"),
            ("frmBanHang","tableGioHang","quantityDataGridViewTextBoxColumn","Quantity","SL"),
            ("frmBanHang","tableGioHang","priceDataGridViewTextBoxColumn","Price","Đơn giá"),
            ("frmBanHang","tableGioHang","totalDataGridViewTextBoxColumn","Total","Thành tiền"),
            ("frmBanHang","tableGioHang","btnAction","",""),
            ("frmBaoCao","dgvTheoNgay","dgvTheoNgayColumn1","Ngay","Ngày"),
            ("frmBaoCao","dgvTheoNgay","dgvTheoNgayColumn2","SoHoaDon","Số hóa đơn"),
            ("frmBaoCao","dgvTheoNgay","dgvTheoNgayColumn3","TongTien","Tổng tiền"),
            ("frmBaoCao","dgvTheoNgay","dgvTheoNgayColumn4","GiamGia","Giảm giá"),
            ("frmBaoCao","dgvTheoNgay","dgvTheoNgayColumn5","DoanhThu","Doanh thu"),
            ("frmBaoCao","dgvHoaDon","dgvHoaDonColumn1","MaHD","Mã hóa đơn"),
            ("frmBaoCao","dgvHoaDon","dgvHoaDonColumn2","NgayLap","Ngày thanh toán"),
            ("frmBaoCao","dgvHoaDon","dgvHoaDonColumn3","NhanVien","Nhân viên"),
            ("frmBaoCao","dgvHoaDon","dgvHoaDonColumn4","Voucher","Mã voucher"),
            ("frmBaoCao","dgvHoaDon","dgvHoaDonColumn5","TongTien","Tổng tiền"),
            ("frmBaoCao","dgvHoaDon","dgvHoaDonColumn6","GiamGia","Giảm giá"),
            ("frmBaoCao","dgvHoaDon","dgvHoaDonColumn7","ThucThu","Thực thu"),
            ("frmCanhBao","dgvSapHet","dgvSapHetColumn1","MaThuoc","Mã thuốc"),
            ("frmCanhBao","dgvSapHet","dgvSapHetColumn2","TenThuoc","Tên thuốc"),
            ("frmCanhBao","dgvSapHet","dgvSapHetColumn3","DonViTinh","Đơn vị tính"),
            ("frmCanhBao","dgvSapHet","dgvSapHetColumn4","NhaCungCap","Nhà cung cấp"),
            ("frmCanhBao","dgvSapHet","dgvSapHetColumn5","TonKho","Tồn có thể bán"),
            ("frmCanhBao","dgvSapHetHan","dgvSapHetHanColumn1","MaThuoc","Mã thuốc"),
            ("frmCanhBao","dgvSapHetHan","dgvSapHetHanColumn2","TenThuoc","Tên thuốc"),
            ("frmCanhBao","dgvSapHetHan","dgvSapHetHanColumn3","SoLo","Số lô"),
            ("frmCanhBao","dgvSapHetHan","dgvSapHetHanColumn4","HanDung","Hạn sử dụng"),
            ("frmCanhBao","dgvSapHetHan","dgvSapHetHanColumn5","SoNgayConLai","Số ngày còn lại"),
            ("frmCanhBao","dgvSapHetHan","dgvSapHetHanColumn6","TonLo","Tồn của lô"),
            ("frmCanhBao","dgvSapHetHan","dgvSapHetHanColumn7","TrangThai","Trạng thái"),
            ("frmCanhBao","dgvSapHetHan","colDaXuLy","","Đã xem"),
            ("frmKhachHang","dgvKhachHang","MaKH","MaKH","Mã khách hàng"),
            ("frmKhachHang","dgvKhachHang","TenKH","TenKH","Tên khách hàng"),
            ("frmKhachHang","dgvKhachHang","DienThoai","DienThoai","Số điện thoại"),
            ("frmKhachHang","dgvKhachHang","Diem","Diem","Điểm tích lũy"),
            ("frmNhaCungCap","dgvNhaCungCap","dgvNhaCungCapColumn1","MaNCC","Mã nhà cung cấp"),
            ("frmNhaCungCap","dgvNhaCungCap","dgvNhaCungCapColumn2","TenNCC","Nhà cung cấp"),
            ("frmNhaCungCap","dgvNhaCungCap","dgvNhaCungCapColumn3","DienThoai","Số điện thoại"),
            ("frmNhaCungCap","dgvNhaCungCap","dgvNhaCungCapColumn4","DiaChi","Địa chỉ"),
            ("frmNhomThuoc","dgvNhomThuoc","dgvNhomThuocColumn1","MaNhom","Mã nhóm thuốc"),
            ("frmNhomThuoc","dgvNhomThuoc","dgvNhomThuocColumn2","TenNhom","Nhóm thuốc"),
            ("frmXemTruocExcel","dgvDuLieu","previewCol0","Thuốc","Tên thuốc"),
            ("frmXemTruocExcel","dgvDuLieu","previewCol1","Mã vạch","Mã vạch"),
            ("frmXemTruocExcel","dgvDuLieu","previewCol2","Nhóm","Nhóm thuốc"),
            ("frmXemTruocExcel","dgvDuLieu","previewCol3","NCC","Nhà cung cấp"),
            ("frmXemTruocExcel","dgvDuLieu","previewCol4","Đơn vị","Đơn vị tính"),
            ("frmXemTruocExcel","dgvDuLieu","previewCol5","Giá bán","Giá bán"),
            ("frmXemTruocExcel","dgvDuLieu","previewCol6","Số lô","Số lô"),
            ("frmXemTruocExcel","dgvDuLieu","previewCol7","Hạn dùng","Hạn dùng"),
            ("frmXemTruocExcel","dgvDuLieu","previewCol8","Số lượng","Số lượng"),
            ("frmXemTruocExcel","dgvDuLieu","previewCol9","Giá nhập","Giá nhập"),
            ("frmHoaDon","dgvHoaDon","dgvHoaDonColumn1","MaHD","Mã HĐ"),
            ("frmHoaDon","dgvHoaDon","dgvHoaDonColumn2","NgayLap","Ngày lập"),
            ("frmHoaDon","dgvHoaDon","dgvHoaDonColumn3","NhanVien","Nhân viên"),
            ("frmHoaDon","dgvHoaDon","dgvHoaDonColumn4","KhachHang","Khách hàng"),
            ("frmHoaDon","dgvHoaDon","dgvHoaDonColumn5","Voucher","Mã voucher"),
            ("frmHoaDon","dgvHoaDon","dgvHoaDonColumn6","TongTien","Tổng tiền"),
            ("frmHoaDon","dgvHoaDon","dgvHoaDonColumn7","GiamGia","Giảm giá"),
            ("frmHoaDon","dgvHoaDon","dgvHoaDonColumn8","ThucThu","Thực thu"),
            ("frmHoaDon","dgvHoaDon","colPaymentMethod","HinhThucThanhToan","Hình thức thanh toán"),
            ("frmHoaDon","dgvHoaDon","colPaymentStatus","TrangThai","Trạng thái"),
            ("frmHoaDon","dgvChiTiet","dgvChiTietColumn1","TenThuoc","Tên thuốc"),
            ("frmHoaDon","dgvChiTiet","dgvChiTietColumn2","DonViTinh","Đơn vị tính"),
            ("frmHoaDon","dgvChiTiet","dgvChiTietColumn3","SoLo","Số lô"),
            ("frmHoaDon","dgvChiTiet","dgvChiTietColumn4","SoLuong","Số lượng"),
            ("frmHoaDon","dgvChiTiet","dgvChiTietColumn5","DonGia","Đơn giá"),
            ("frmHoaDon","dgvChiTiet","dgvChiTietColumn6","ThanhTien","Thành tiền"),
            ("frmHoaDon","dgvChiTiet","dgvChiTietColumn7","HuongDan","Hướng dẫn sử dụng"),
            ("frmLoHang","dgvLoHang","dgvLoHangColumn1","MaLo","Mã lô"),
            ("frmLoHang","dgvLoHang","dgvLoHangColumn2","SoLo","Số lô"),
            ("frmLoHang","dgvLoHang","dgvLoHangColumn3","TenThuoc","Tên thuốc"),
            ("frmLoHang","dgvLoHang","dgvLoHangColumn4","TenNCC","Nhà cung cấp"),
            ("frmLoHang","dgvLoHang","dgvLoHangColumn5","TenNhom","Nhóm thuốc"),
            ("frmLoHang","dgvLoHang","dgvLoHangColumn6","HanDung","Hạn sử dụng"),
            ("frmLoHang","dgvLoHang","dgvLoHangColumn7","TonKho","Tồn kho"),
            ("frmLoHang","dgvLoHang","dgvLoHangColumn8","SoLuongDaNhap","Tổng đã nhập"),
            ("frmLoHang","dgvLoHang","dgvLoHangColumn9","NgayNhapGanNhat","Nhập gần nhất"),
            ("frmLoHang","dgvLoHang","colGiaNhap","GiaNhapGanNhat","Giá nhập gần nhất"),
            ("frmNhanVien","dgvNhanVien","colMaNV","MaNV","Mã nhân viên"),
            ("frmNhanVien","dgvNhanVien","colHoTen","HoTen","Họ tên"),
            ("frmNhanVien","dgvNhanVien","colTaiKhoan","TaiKhoan","Tài khoản"),
            ("frmNhanVien","dgvNhanVien","colDienThoai","DienThoai","Số điện thoại"),
            ("frmNhanVien","dgvNhanVien","colVaiTro","TenVaiTro","Vai trò"),
            ("frmNhanVien","dgvNhanVien","colDangLam","DangLam","Đang làm việc"),
            ("frmNhapKho","dgvPhieu","dgvPhieuColumn1","MaThuoc","Mã thuốc"),
            ("frmNhapKho","dgvPhieu","dgvPhieuColumn2","TenThuoc","Tên thuốc"),
            ("frmNhapKho","dgvPhieu","dgvPhieuColumn3","DonViTinh","Đơn vị tính"),
            ("frmNhapKho","dgvPhieu","dgvPhieuColumn4","SoLo","Số lô"),
            ("frmNhapKho","dgvPhieu","dgvPhieuColumn5","HanDung","Hạn sử dụng"),
            ("frmNhapKho","dgvPhieu","dgvPhieuColumn6","SoLuong","Số lượng"),
            ("frmNhapKho","dgvPhieu","dgvPhieuColumn7","GiaNhap","Giá nhập"),
            ("frmNhapKho","dgvPhieu","dgvPhieuColumn8","ThanhTien","Thành tiền"),
            ("frmPhieuNhap","dgvPhieuNhap","dgvPhieuNhapCol0","MaPhieu","Mã phiếu nhập"),
            ("frmPhieuNhap","dgvPhieuNhap","dgvPhieuNhapCol1","NgayNhap","Ngày nhập"),
            ("frmPhieuNhap","dgvPhieuNhap","dgvPhieuNhapCol2","TenNCC","Nhà cung cấp"),
            ("frmPhieuNhap","dgvPhieuNhap","dgvPhieuNhapCol3","NhanVien","Nhân viên"),
            ("frmPhieuNhap","dgvPhieuNhap","dgvPhieuNhapCol4","TongTien","Tổng tiền"),
            ("frmPhieuNhap","dgvPhieuNhap","dgvPhieuNhapCol5","GhiChu","Ghi chú"),
            ("frmPhieuNhap","dgvChiTiet","dgvChiTietCol0","TenThuoc","Tên thuốc"),
            ("frmPhieuNhap","dgvChiTiet","dgvChiTietCol1","DonVi","Đơn vị tính"),
            ("frmPhieuNhap","dgvChiTiet","dgvChiTietCol2","SoLo","Số lô"),
            ("frmPhieuNhap","dgvChiTiet","dgvChiTietCol3","HanDung","Hạn sử dụng"),
            ("frmPhieuNhap","dgvChiTiet","dgvChiTietCol4","SoLuong","Số lượng"),
            ("frmPhieuNhap","dgvChiTiet","dgvChiTietCol5","GiaNhap","Giá nhập"),
            ("frmPhieuNhap","dgvChiTiet","dgvChiTietCol6","ThanhTien","Thành tiền"),
            ("frmQuanLyThuoc","dgvThuoc","MaThuoc","MaThuoc","Mã thuốc"),
            ("frmQuanLyThuoc","dgvThuoc","TenThuoc","TenThuoc","Tên thuốc"),
            ("frmQuanLyThuoc","dgvThuoc","MaNhom","MaNhom","Mã nhóm thuốc"),
            ("frmQuanLyThuoc","dgvThuoc","MaNCC","MaNCC","Mã nhà cung cấp"),
            ("frmQuanLyThuoc","dgvThuoc","TenNhom","TenNhom","Nhóm thuốc"),
            ("frmQuanLyThuoc","dgvThuoc","TenNCC","TenNCC","Nhà cung cấp"),
            ("frmQuanLyThuoc","dgvThuoc","DonViTinh","DonViTinh","Đơn vị"),
            ("frmQuanLyThuoc","dgvThuoc","DonGia","DonGia","Đơn giá"),
            ("frmQuanLyThuoc","dgvThuoc","SoLuongTon","SoLuongTon","Tồn kho"),
            ("frmQuanLyThuoc","dgvThuoc","colMaVach","MaVach","Mã vạch"),
            ("frmMain","dgvSapHet","dgvSapHetColumn1","MaThuoc","Mã thuốc"),
            ("frmMain","dgvSapHet","dgvSapHetColumn2","TenThuoc","Tên thuốc"),
            ("frmMain","dgvSapHet","dgvSapHetColumn3","DonViTinh","Đơn vị"),
            ("frmMain","dgvSapHet","dgvSapHetColumn4","NhaCungCap","Nhà cung cấp"),
            ("frmMain","dgvSapHet","dgvSapHetColumn5","TonKho","Tồn có thể bán"),
            ("frmMain","dgvSapHetHan","dgvSapHetHanColumn1","MaThuoc","Mã thuốc"),
            ("frmMain","dgvSapHetHan","dgvSapHetHanColumn2","TenThuoc","Tên thuốc"),
            ("frmMain","dgvSapHetHan","dgvSapHetHanColumn3","SoLo","Số lô"),
            ("frmMain","dgvSapHetHan","dgvSapHetHanColumn4","HanDung","Hạn sử dụng"),
            ("frmMain","dgvSapHetHan","dgvSapHetHanColumn5","SoNgayConLai","Số ngày còn lại"),
            ("frmMain","dgvSapHetHan","dgvSapHetHanColumn6","TonLo","Tồn của lô"),
            ("frmMain","dgvSapHetHan","dgvSapHetHanColumn7","TrangThai","Trạng thái"),
            ("frmMain","dgvGanNhat","dgvGanNhatColumn1","MaHD","Mã hóa đơn"),
            ("frmMain","dgvGanNhat","dgvGanNhatColumn2","NgayLap","Ngày lập"),
            ("frmMain","dgvGanNhat","dgvGanNhatColumn3","KhachHang","Khách hàng"),
            ("frmMain","dgvGanNhat","dgvGanNhatColumn4","NhanVien","Nhân viên"),
            ("frmMain","dgvGanNhat","dgvGanNhatColumn5","TongTien","Tổng tiền"),
            ("frmMain","dgvGanNhat","dgvGanNhatColumn6","GiamGia","Giảm giá"),
            ("frmMain","dgvGanNhat","dgvGanNhatColumn7","CanThanhToan","Cần thanh toán"),
            ("frmMain","dgvGanNhat","dgvGanNhatColumn8","ThucThu","Thực thu"),
            ("frmMain","dgvGanNhat","colRecentStatus","TrangThai","Trạng thái"),
            ("frmVoucher","dgvVoucher","dgvVoucherColumn1","Id","ID voucher"),
            ("frmVoucher","dgvVoucher","dgvVoucherColumn2","Code","Mã voucher"),
            ("frmVoucher","dgvVoucher","dgvVoucherColumn3","DiscountType","Mã loại giảm giá"),
            ("frmVoucher","dgvVoucher","dgvVoucherColumn4","LoaiGiam","Loại giảm giá"),
            ("frmVoucher","dgvVoucher","dgvVoucherColumn5","DiscountValue","Giá trị giảm"),
            ("frmVoucher","dgvVoucher","dgvVoucherColumn6","Quantity","Lượt còn lại"),
            ("frmVoucher","dgvVoucher","dgvVoucherColumn7","IsActive","Hoạt động")
        };
        foreach(var group in expected.GroupBy(x=>x.Form))
        {
            var type=typeof(frmMain).Assembly.GetType("Store.POS."+group.Key)!;
            using var form=(Form)Activator.CreateInstance(type)!;form.Site=new FormDesignSite(form);Offscreen(form);
            foreach(var table in group.GroupBy(x=>x.Grid))
            {
                var grid=UiField<DataGridView>(form,table.Key);
                Check(grid.Columns.Count==table.Count() && !grid.AutoGenerateColumns,group.Key+"/"+table.Key+" has all columns before loading data");
                Check(grid.Columns.Cast<DataGridViewColumn>().Where(c=>c.DataPropertyName.Length>0).All(c=>c.Name==c.DataPropertyName),group.Key+"/"+table.Key+" preserves data lookups after Designer renames components");
                foreach(var spec in table)
                {
                    var column=UiField<DataGridViewColumn>(form,spec.Field);
                    Check(column!=null && column.DataGridView==grid && column.DataPropertyName==spec.Binding && column.HeaderText==spec.Header,group.Key+"/"+spec.Field+" has Designer field, binding and Vietnamese header");
                    Check(column!.AutoSizeMode==DataGridViewAutoSizeColumnMode.None && column.Width>=column.MinimumWidth,group.Key+"/"+spec.Field+" allows Width editing without automatic sizing");
                    if(column.Visible)
                    {
                        // Validate the base width with no spare space for automatic fill.
                        var dock=grid.Dock;int frameWidth=grid.Width;grid.Dock=DockStyle.None;grid.Width=1;
                        int width=column.Width;column.Width=width+19;Application.DoEvents();
                        Check(column.Width==width+19,group.Key+"/"+spec.Field+" preserves the edited width");
                        column.Width=width;grid.Width=frameWidth;grid.Dock=dock;
                    }
                }
            }
            if(form is frmQuanLyThuoc)
            {
                var grid=UiField<DataGridView>(form,"dgvThuoc");
                Check(!grid.Columns["MaNhom"]!.Visible && !grid.Columns["MaNCC"]!.Visible,"medicine hides technical IDs and displays their names");
                Check(grid.Columns["DonGia"]!.DefaultCellStyle.Format=="N0","medicine money columns retain whole-dong formatting");
            }
            form.Dispose();
        }
    }
}
