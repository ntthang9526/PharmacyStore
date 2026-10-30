using System.ComponentModel;
using Store.Shared;
using Store.POS.FormBanHang;
namespace Store.POS;

public partial class frmDonThuoc : Form
{
    private readonly bool chiXem;
    public DonThuocInfo? DonThuoc {get;private set;}
    public Dictionary<int,string> HuongDan {get;private set;} = new();
    public frmDonThuoc() {InitializeComponent();PosFormStyle.ConfigureDisabledButtons(this);}
    public frmDonThuoc(DonThuocInfo? info,IEnumerable<RowData> items,bool readOnly=false):this()
    {
        chiXem=readOnly;
        txtSoDon.Text=info?.SoDon??"";txtBenhNhan.Text=info?.BenhNhan??"";txtNguoiKeDon.Text=info?.NguoiKeDon??"";
        if(info!=null && info.NgayKeDon>=dtpNgayKeDon.MinDate && info.NgayKeDon<=DateTime.Today) dtpNgayKeDon.Value=info.NgayKeDon;
        dtpNgayKeDon.MaxDate=DateTime.Today;chkDaKiemTra.Checked=info?.DaKiemTra??false;
        dgvHuongDan.DataSource=new BindingList<RowData>(items.Where(i=>i.IsPrescription).Select(i=>new RowData{Id=i.Id,Name=i.Name,HuongDan=i.HuongDan}).ToList());
        if(!readOnly)
        {
            txtSoDon.TextChanged+=(_,_)=>chkDaKiemTra.Checked=false;
            txtBenhNhan.TextChanged+=(_,_)=>chkDaKiemTra.Checked=false;
            txtNguoiKeDon.TextChanged+=(_,_)=>chkDaKiemTra.Checked=false;
            dtpNgayKeDon.ValueChanged+=(_,_)=>chkDaKiemTra.Checked=false;
            dgvHuongDan.CellValueChanged+=(_,e)=> {if(e.RowIndex>=0) chkDaKiemTra.Checked=false;};
        }
        if(readOnly)
        {
            txtSoDon.ReadOnly=txtBenhNhan.ReadOnly=txtNguoiKeDon.ReadOnly=true;
            dtpNgayKeDon.Enabled=chkDaKiemTra.Enabled=false;dgvHuongDan.ReadOnly=true;
            btnLuu.Text="Đóng";btnHuy.Visible=btnLuuNhap.Visible=false;
        }
    }
    private void btnLuu_Click(object? sender,EventArgs e)=>LuuThongTin(false);
    private void btnLuuNhap_Click(object? sender,EventArgs e)=>LuuThongTin(true);
    private void LuuThongTin(bool luuNhap)
    {
        if(chiXem) {DialogResult=DialogResult.OK;return;}
        dgvHuongDan.EndEdit();
        var info=new DonThuocInfo {SoDon=txtSoDon.Text.Trim(),BenhNhan=txtBenhNhan.Text.Trim(),NguoiKeDon=txtNguoiKeDon.Text.Trim(),NgayKeDon=dtpNgayKeDon.Value.Date,DaKiemTra=!luuNhap && chkDaKiemTra.Checked};
        string? error=info.KiemTra(!luuNhap);
        var rows=(BindingList<RowData>)dgvHuongDan.DataSource!;
        if(rows.Count==0) error="Giỏ hàng không có thuốc kê đơn.";
        foreach(var row in rows)
            if((!luuNhap && string.IsNullOrWhiteSpace(row.HuongDan)) || row.HuongDan.Trim().Length>500) {error="Nhập hướng dẫn dùng từ 1 đến 500 ký tự cho "+row.Name;break;}
        if(error!=null) {lblLoi.Text=error;return;}
        DonThuoc=info;HuongDan=rows.ToDictionary(i=>i.Id,i=>i.HuongDan.Trim());DialogResult=DialogResult.OK;
    }
}
