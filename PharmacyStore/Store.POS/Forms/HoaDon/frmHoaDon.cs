using System.Data;
using Store.BLL;
namespace Store.POS;
public partial class frmHoaDon : Form
{
    private readonly HoaDonBLL bll=new();
    private readonly int? maMoSan;

    public frmHoaDon():this(null) {}
    public frmHoaDon(int? id)
    {
        maMoSan=id;
        InitializeComponent();
        PosFormStyle.ConfigureGridSelection(this, posLayout, XoaChiTiet);
        dgvHoaDon.CellClick+=(_,e)=> {if(e.RowIndex>=0) HienChiTiet(dgvHoaDon.Rows[e.RowIndex]);};
        dgvHoaDon.KeyUp+=(_,e)=> {if(e.KeyCode is Keys.Up or Keys.Down or Keys.Home or Keys.End && dgvHoaDon.CurrentRow!=null) HienChiTiet(dgvHoaDon.CurrentRow);};
        btnTimKiem.Click+=(_,_)=>TimKiem();
        txtTimKiem.KeyDown+=(_,e)=> {if(e.KeyCode==Keys.Enter) {e.SuppressKeyPress=true;TimKiem();}};
        btnLamMoi.Click+=(_,_)=> {txtTimKiem.Clear();dtpTuNgay.Checked=dtpDenNgay.Checked=false;TimKiem();};
        Load+=(_,_)=>
        {
            if (PosFormStyle.IsInDesigner(this)) return;
            dtpTuNgay.Value=DateTime.Today.AddMonths(-1); dtpDenNgay.Value=DateTime.Today;
            dtpTuNgay.Checked=dtpDenNgay.Checked=false;
            TimKiem();
            if(maMoSan.HasValue)
            {
                var row=dgvHoaDon.Rows.Cast<DataGridViewRow>().FirstOrDefault(r=>Convert.ToInt32(r.Cells["MaHD"].Value)==maMoSan.Value);
                if(row!=null) HienChiTiet(row);
            }
        };
    }
    private void XoaChiTiet()
    {
        dgvChiTiet.DataSource=null;
        lblChiTiet.Text="Chọn hóa đơn trong bảng để xem các thuốc đã bán.";
    }
    private void TimKiem()
    {
        if (PosFormStyle.IsInDesigner(this)) return;
        try
        {
            var rows=bll.TimKiem(txtTimKiem.Text,dtpTuNgay.Checked?dtpTuNgay.Value:null,dtpDenNgay.Checked?dtpDenNgay.Value:null);
            XoaChiTiet();dgvHoaDon.DataSource=rows;
            decimal total=rows.Rows.Cast<DataRow>().Sum(row=>(decimal)row["ThucThu"]);
            lblTrangThai.Text=$"{rows.Rows.Count} hóa đơn · Tổng thực thu: {total:#,##0.##} đ";
            lblTrangThai.ForeColor=SystemColors.ControlText;
        }
        catch(Exception ex) {lblTrangThai.ForeColor=Color.Firebrick;lblTrangThai.Text=ex.Message;}
    }
    private void HienChiTiet(DataGridViewRow row)
    {
        if(row.DataBoundItem is not DataRowView data) return;
        try
        {
            dgvChiTiet.DataSource=bll.ChiTiet((int)data["MaHD"]);
            lblChiTiet.Text=$"HD{(int)data["MaHD"]:D5} · {data["KhachHang"]} · NV: {data["NhanVien"]}\nTiền hàng: {data["TongTien"]:#,##0.##} đ    Giảm: {data["GiamGia"]:#,##0.##} đ    Thực thu: {data["ThucThu"]:#,##0.##} đ";
        }
        catch(Exception ex) {XoaChiTiet();lblTrangThai.Text="Không tải được chi tiết: "+ex.Message;}
    }

}
