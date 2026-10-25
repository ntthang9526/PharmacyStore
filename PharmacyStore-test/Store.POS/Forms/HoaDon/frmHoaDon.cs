using System.Data;
using Store.BLL;
using Store.POS.Common;

namespace Store.POS;
public partial class frmHoaDon : Form, ILamMoiDuLieu
{
    private readonly HoaDonBLL bll = new();
    private readonly int? maMoSan;
    private int? hoaDonDangXem;
    private DataRowView? dataDangXem;

    private readonly Button btnXuatDanhSachExcel;
    private readonly Button btnXuatChiTietExcel;

    public frmHoaDon() : this(null) { }
    public frmHoaDon(int? id)
    {
        maMoSan = id;
        InitializeComponent();
        PosFormStyle.ConfigureGridSelection(this, posLayout, XoaChiTiet);

        // Nút Xuất Excel danh sách hóa đơn cạnh nút Làm mới
        tableLayoutPanel12.SetColumnSpan(btnLamMoi, 1);
        btnXuatDanhSachExcel = new Button
        {
            Name = "btnXuatDanhSachExcel",
            Text = "Xuất Excel",
            Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold),
            BackColor = Color.FromArgb(22, 101, 52), // Xanh lá đậm
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand,
            Dock = DockStyle.Fill,
            Margin = new Padding(4, 4, 0, 0)
        };
        btnXuatDanhSachExcel.FlatAppearance.BorderSize = 0;
        btnXuatDanhSachExcel.Click += (_, _) => XuatDanhSachExcel();
        tableLayoutPanel12.Controls.Add(btnXuatDanhSachExcel, 1, 0);

        // Nút Xuất chi tiết hóa đơn hiện tại
        btnXuatChiTietExcel = new Button
        {
            Name = "btnXuatChiTietExcel",
            Text = "Xuất HĐ này",
            Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold),
            BackColor = Color.FromArgb(15, 118, 110), // Teal
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand,
            Size = new Size(130, 36),
            Anchor = AnchorStyles.Top | AnchorStyles.Right,
            Visible = false
        };
        btnXuatChiTietExcel.FlatAppearance.BorderSize = 0;
        btnXuatChiTietExcel.Click += (_, _) => XuatChiTietExcel();

        // Gắn nút Xuất chi tiết vào góc trên của groupBox14 (Chi tiết hóa đơn)
        groupBox14.Controls.Add(btnXuatChiTietExcel);
        btnXuatChiTietExcel.BringToFront();
        groupBox14.Resize += (_, _) =>
        {
            btnXuatChiTietExcel.Location = new Point(groupBox14.Width - btnXuatChiTietExcel.Width - 16, 12);
        };

        dgvHoaDon.CellClick += (_, e) => { if (e.RowIndex >= 0) HienChiTiet(dgvHoaDon.Rows[e.RowIndex]); };
        dgvHoaDon.KeyUp += (_, e) => { if (e.KeyCode is Keys.Up or Keys.Down or Keys.Home or Keys.End && dgvHoaDon.CurrentRow != null) HienChiTiet(dgvHoaDon.CurrentRow); };
        btnTimKiem.Click += (_, _) => TimKiem();
        txtTimKiem.KeyDown += (_, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; TimKiem(); } };
        btnLamMoi.Click += (_, _) => { txtTimKiem.Clear(); dtpTuNgay.Checked = dtpDenNgay.Checked = false; TimKiem(); };
        Load += (_, _) =>
        {
            if (PosFormStyle.IsInDesigner(this)) return;
            dtpTuNgay.Value = DateTime.Today.AddMonths(-1); dtpDenNgay.Value = DateTime.Today;
            dtpTuNgay.Checked = dtpDenNgay.Checked = false;
            TimKiem();
            if (maMoSan.HasValue)
            {
                var row = dgvHoaDon.Rows.Cast<DataGridViewRow>().FirstOrDefault(r => Convert.ToInt32(r.Cells["MaHD"].Value) == maMoSan.Value);
                if (row != null) HienChiTiet(row);
            }
        };
    }

    private void XoaChiTiet()
    {
        hoaDonDangXem = null;
        dataDangXem = null;
        btnXuatChiTietExcel.Visible = false;
        dgvChiTiet.DataSource = null;
        lblChiTiet.Text = "Chọn hóa đơn trong bảng để xem các thuốc đã bán.";
    }

    private void TimKiem()
    {
        if (PosFormStyle.IsInDesigner(this)) return;
        try
        {
            var rows = bll.TimKiem(txtTimKiem.Text, dtpTuNgay.Checked ? dtpTuNgay.Value : null, dtpDenNgay.Checked ? dtpDenNgay.Value : null);
            XoaChiTiet();
            dgvHoaDon.DataSource = rows;
            decimal total = rows.Rows.Cast<DataRow>().Sum(row => (decimal)row["ThucThu"]);
            lblTrangThai.Text = $"{rows.Rows.Count} hóa đơn · Tổng thực thu: {total:N0} đ";
            lblTrangThai.ForeColor = SystemColors.ControlText;
        }
        catch (Exception ex) { lblTrangThai.ForeColor = Color.Firebrick; lblTrangThai.Text = ex.Message; }
    }

    private void HienChiTiet(DataGridViewRow row)
    {
        if (row.DataBoundItem is not DataRowView data) return;
        try
        {
            hoaDonDangXem = (int)data["MaHD"];
            dataDangXem = data;
            dgvChiTiet.DataSource = bll.ChiTiet((int)data["MaHD"]);
            btnXuatChiTietExcel.Visible = true;
            btnXuatChiTietExcel.Location = new Point(groupBox14.Width - btnXuatChiTietExcel.Width - 16, 12);
            string pt = data.Row.Table.Columns.Contains("HinhThucThanhToan") && data["HinhThucThanhToan"] != DBNull.Value ? data["HinhThucThanhToan"].ToString()! : "Tiền mặt";
            lblChiTiet.Text = $"HD{(int)data["MaHD"]:D5} · {data["KhachHang"]} · NV: {data["NhanVien"]} · PT: {pt}\nTiền hàng: {data["TongTien"]:N0} đ    Giảm: {data["GiamGia"]:N0} đ    Thực thu: {data["ThucThu"]:N0} đ";
        }
        catch (Exception ex) { XoaChiTiet(); lblTrangThai.Text = "Không tải được chi tiết: " + ex.Message; }
    }

    private void XuatDanhSachExcel()
    {
        if (dgvHoaDon.DataSource is not DataTable dt || dt.Rows.Count == 0)
        {
            MessageBox.Show("Không có dữ liệu hóa đơn để xuất Excel.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        try
        {
            ExcelHelper.XuatDanhSachHoaDon(dt, dtpTuNgay.Checked ? dtpTuNgay.Value : null, dtpDenNgay.Checked ? dtpDenNgay.Value : null);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Lỗi xuất Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void XuatChiTietExcel()
    {
        if (!hoaDonDangXem.HasValue || dataDangXem == null || dgvChiTiet.DataSource is not DataTable dtChiTiet)
        {
            MessageBox.Show("Vui lòng chọn một hóa đơn có chi tiết mặt hàng để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        try
        {
            ExcelHelper.XuatChiTietMotHoaDon(
                hoaDonDangXem.Value,
                dataDangXem["KhachHang"]?.ToString() ?? "Khách lẻ",
                dataDangXem["NhanVien"]?.ToString() ?? "",
                Convert.ToDateTime(dataDangXem["NgayLap"]),
                Convert.ToDecimal(dataDangXem["TongTien"]),
                Convert.ToDecimal(dataDangXem["GiamGia"]),
                Convert.ToDecimal(dataDangXem["ThucThu"]),
                dtChiTiet);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Lỗi xuất hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
