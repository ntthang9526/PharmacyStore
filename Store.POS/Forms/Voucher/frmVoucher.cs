using System.Data;
using Store.BLL;
namespace Store.POS;
public partial class frmVoucher : Form, ILamMoiDuLieu
{
    private readonly VoucherBLL bll=new();

    private int? selectedId, originalQuantity;
    public frmVoucher()
    {
        InitializeComponent();
            global::Store.POS.PosFormStyle.ConfigureDisabledButtons(this);
            PhanQuyenGiaoDien.Gan(this);
        cboLoai.DisplayMember="Text";cboLoai.ValueMember="Value";
        cboLoai.DataSource=new[] {new {Text="Số tiền (đ)",Value="FIXED"},new {Text="Phần trăm (%)",Value="PERCENT"}};
        cboLoai.SelectedValueChanged += (_, _) =>
        {
            bool percent = cboLoai.SelectedValue as string == "PERCENT";
            nudGiaTri.DecimalPlaces = percent ? 2 : 0;
            if (!percent) nudGiaTri.Value = Store.Shared.TienTe.LamTron(nudGiaTri.Value);
        };
        dgvVoucher.CellFormatting += (_, e) =>
        {
            if (e.RowIndex >= 0 && dgvVoucher.Columns[e.ColumnIndex].DataPropertyName == "DiscountValue" &&
                dgvVoucher.Rows[e.RowIndex].DataBoundItem is DataRowView row && (string)row["DiscountType"] == "FIXED" && e.Value is decimal amount)
            {
                e.Value = Store.Shared.TienTe.LamTron(amount).ToString("N0");
                e.FormattingApplied = true;
            }
        };
        PosFormStyle.ConfigureRowNavigation(dgvVoucher,Chon);
        PosFormStyle.ConfigureGridSelection(this, posLayout, () => ResetDetails(false));
        btnTimKiem.Click+=(_,_)=>TimKiem();
        txtTimKiem.KeyDown+=(_,e)=> {if(e.KeyCode==Keys.Enter) {e.SuppressKeyPress=true;TimKiem();}};
        btnThem.Click+=(_,_)=> {ResetDetails();SetEditing(true);txtCode.Focus();};
        btnSua.Click+=(_,_)=> {if(selectedId.HasValue) SetEditing(true);else BaoLoi("Chọn voucher cần sửa trong bảng.");};
        btnHuy.Click+=(_,_)=>ResetDetails(true);
        btnLuu.Click+=(_,_)=>Luu();btnXoa.Click+=(_,_)=>Xoa();
        btnLamMoi.Click+=(_,_)=> {txtTimKiem.Clear();TimKiem();};
        dgvVoucher.CellClick+=(_,e)=> {if(e.RowIndex>=0) Chon(dgvVoucher.Rows[e.RowIndex]);};
        
        ResetDetails();Load+=(_,_)=>TimKiem();
    }
    private void SetEditing(bool editing)
    {
        dgvVoucher.Enabled=!editing;
        txtCode.Enabled=cboLoai.Enabled=nudGiaTri.Enabled=nudSoLuot.Enabled=chkHoatDong.Enabled=editing;
        btnLuu.Enabled=btnHuy.Enabled=editing;
        btnSua.Enabled=btnXoa.Enabled=!editing && selectedId.HasValue;
        btnThem.Enabled=!editing;
    }
    private void ResetDetails(bool force = false)
    {
        if (!force && txtCode.Enabled) return;
        selectedId=originalQuantity=null;
        txtCode.Clear();cboLoai.SelectedIndex=-1;nudGiaTri.Value=nudSoLuot.Value=0;chkHoatDong.Checked=true;
        lblTrangThai.Text="";SetEditing(false);
    }
    private void Chon(DataGridViewRow row)
    {
        if(!dgvVoucher.Enabled) return;
        if(row.DataBoundItem is not DataRowView data) return;
        selectedId=(int)data["Id"];originalQuantity=(int)data["Quantity"];
        txtCode.Text=(string)data["Code"];cboLoai.SelectedValue=(string)data["DiscountType"];
        nudGiaTri.Value=(string)data["DiscountType"]=="FIXED" ? Store.Shared.TienTe.LamTron(Convert.ToDecimal(data["DiscountValue"])) : Convert.ToDecimal(data["DiscountValue"]);nudSoLuot.Value=originalQuantity.Value;
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
        ResetDetails(true);TimKiem();lblTrangThai.ForeColor=Color.DarkGreen;lblTrangThai.Text="Đã lưu voucher.";
    }
    private void Xoa()
    {
        if(!selectedId.HasValue) return;
        if(MessageBox.Show(this,"Xóa voucher "+txtCode.Text+"?","Xác nhận",MessageBoxButtons.YesNo,MessageBoxIcon.Question)!=DialogResult.Yes) return;
        if(!bll.Xoa(selectedId.Value,out string error)) {BaoLoi(error);return;}
        TimKiem();
    }
}
