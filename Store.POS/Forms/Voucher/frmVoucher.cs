using System.Data;
using Store.BLL;
namespace Store.POS;
public partial class frmVoucher : Form
{
    private readonly VoucherBLL bll=new();

    private int? selectedId, originalQuantity;
    public frmVoucher()
    {
        InitializeComponent();
        cboLoai.DisplayMember="Text";cboLoai.ValueMember="Value";
        cboLoai.DataSource=new[] {new {Text="Số tiền (đ)",Value="FIXED"},new {Text="Phần trăm (%)",Value="PERCENT"}};
        PosFormStyle.ConfigureGridSelection(this, posLayout, ResetDetails);
        btnTimKiem.Click+=(_,_)=>TimKiem();
        txtTimKiem.KeyDown+=(_,e)=> {if(e.KeyCode==Keys.Enter) {e.SuppressKeyPress=true;TimKiem();}};
        btnThem.Click+=(_,_)=> {ResetDetails();SetEditing(true);txtCode.Focus();};
        btnSua.Click+=(_,_)=> {if(selectedId.HasValue) SetEditing(true);else BaoLoi("Chọn voucher cần sửa trong bảng.");};
        btnHuy.Click+=(_,_)=>ResetDetails();
        btnLuu.Click+=(_,_)=>Luu();btnXoa.Click+=(_,_)=>Xoa();
        btnLamMoi.Click+=(_,_)=> {txtTimKiem.Clear();TimKiem();};btnDong.Click+=(_,_)=>Close();
        dgvVoucher.CellClick+=(_,e)=> {if(e.RowIndex>=0) Chon(dgvVoucher.Rows[e.RowIndex]);};
        dgvVoucher.KeyUp+=(_,e)=> {if(e.KeyCode is Keys.Up or Keys.Down or Keys.Home or Keys.End && dgvVoucher.CurrentRow!=null) Chon(dgvVoucher.CurrentRow);};
        ResetDetails();Load+=(_,_)=>TimKiem();
    }
    private void SetEditing(bool editing)
    {
        txtCode.Enabled=cboLoai.Enabled=nudGiaTri.Enabled=nudSoLuot.Enabled=chkHoatDong.Enabled=editing;
        btnLuu.Enabled=btnHuy.Enabled=editing;
        btnSua.Enabled=btnXoa.Enabled=!editing && selectedId.HasValue;
        btnThem.Enabled=!editing;
    }
    private void ResetDetails()
    {
        selectedId=originalQuantity=null;
        txtCode.Clear();cboLoai.SelectedIndex=-1;nudGiaTri.Value=nudSoLuot.Value=0;chkHoatDong.Checked=true;
        lblTrangThai.Text="";SetEditing(false);
    }
    private void Chon(DataGridViewRow row)
    {
        if(row.DataBoundItem is not DataRowView data) return;
        selectedId=(int)data["Id"];originalQuantity=(int)data["Quantity"];
        txtCode.Text=(string)data["Code"];cboLoai.SelectedValue=(string)data["DiscountType"];
        nudGiaTri.Value=Convert.ToDecimal(data["DiscountValue"]);nudSoLuot.Value=originalQuantity.Value;
        chkHoatDong.Checked=(bool)data["IsActive"];lblTrangThai.Text="";SetEditing(false);
    }
    private void TimKiem()
    {
        if (PosFormStyle.IsInDesigner(this)) return;
        try {ResetDetails();dgvVoucher.DataSource=bll.TimKiem(txtTimKiem.Text);lblTrangThai.Text=$"{dgvVoucher.Rows.Count} voucher.";}
        catch(Exception ex) {BaoLoi("Không tải được voucher: "+ex.Message);}
    }
    private void BaoLoi(string error) {lblTrangThai.ForeColor=Color.Firebrick;lblTrangThai.Text=error;}
    private void Luu()
    {
        if(!bll.Luu(selectedId,txtCode.Text,cboLoai.SelectedValue as string??"",nudGiaTri.Value,(int)nudSoLuot.Value,
            chkHoatDong.Checked,originalQuantity,out string error)) {BaoLoi(error);return;}
        TimKiem();lblTrangThai.ForeColor=Color.DarkGreen;lblTrangThai.Text="Đã lưu voucher.";
    }
    private void Xoa()
    {
        if(!selectedId.HasValue) return;
        if(MessageBox.Show(this,"Xóa voucher "+txtCode.Text+"?","Xác nhận",MessageBoxButtons.YesNo,MessageBoxIcon.Question)!=DialogResult.Yes) return;
        if(!bll.Xoa(selectedId.Value,out string error)) {BaoLoi(error);return;}
        TimKiem();
    }
}
