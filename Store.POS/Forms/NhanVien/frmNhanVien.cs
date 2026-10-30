using System.Data;
using Store.BLL;
namespace Store.POS;

public partial class frmNhanVien : Form, ILamMoiDuLieu
{
    private readonly NhanVienBLL bll=new();
    private int? selectedId;
    private bool selectedAdmin;
    private bool BaoVeAdmin => selectedAdmin || cboVaiTro.SelectedIndex==1 || txtTaiKhoan.Text.Trim().Equals("admin",StringComparison.OrdinalIgnoreCase);
    public frmNhanVien()
    {
        InitializeComponent();
            global::Store.POS.PosFormStyle.ConfigureDisabledButtons(this);
            PhanQuyenGiaoDien.Gan(this);
        PosFormStyle.ConfigureRowNavigation(dgvNhanVien,Chon);
        PosFormStyle.ConfigureGridSelection(this,posLayout,() => ResetDetails(false));
        btnThem.Click+=(_,_)=> {ResetDetails();SetEditing(true);txtHoTen.Focus();};
        btnSua.Click+=(_,_)=> {if(selectedId.HasValue) {SetEditing(true);txtHoTen.Focus();} else BaoLoi("Chọn nhân viên trong bảng.");};
        btnHuy.Click+=(_,_)=>ResetDetails(true);btnLuu.Click+=(_,_)=>Luu();btnKhoa.Click+=(_,_)=>Khoa();
        btnTimKiem.Click+=(_,_)=>TimKiem();btnLamMoi.Click+=(_,_)=> {txtTimKiem.Clear();TimKiem();};
        txtTimKiem.KeyDown+=(_,e)=> {if(e.KeyCode==Keys.Enter) {e.SuppressKeyPress=true;TimKiem();}};
        dgvNhanVien.CellClick+=(_,e)=> {if(e.RowIndex>=0) Chon(dgvNhanVien.Rows[e.RowIndex]);};
        
        cboVaiTro.SelectedIndexChanged+=(_,_)=> {if(btnLuu.Enabled) CapNhatBaoVeAdmin();};
        txtTaiKhoan.TextChanged+=(_,_)=> {if(btnLuu.Enabled) CapNhatBaoVeAdmin();};
        Load+=(_,_)=>TimKiem();ResetDetails();
    }
    private void SetEditing(bool edit)
    {
        dgvNhanVien.Enabled=!edit;
        txtHoTen.Enabled=txtTaiKhoan.Enabled=txtDienThoai.Enabled=txtMatKhau.Enabled=chkDangLam.Enabled=cboVaiTro.Enabled=edit;
        btnLuu.Enabled=btnHuy.Enabled=edit;btnThem.Enabled=!edit;
        btnSua.Enabled=!edit && selectedId.HasValue;
        CapNhatBaoVeAdmin();
    }
    private void CapNhatBaoVeAdmin()
    {
        bool editing=btnLuu.Enabled;
        chkDangLam.Enabled=editing && !BaoVeAdmin;
        if(editing && BaoVeAdmin) chkDangLam.Checked=true;
        btnKhoa.Enabled=!editing && selectedId.HasValue && !BaoVeAdmin;
    }
    private void ResetDetails(bool force = false)
    {
        if (!force && txtHoTen.Enabled) return;
        selectedId=null;selectedAdmin=false;txtHoTen.Clear();txtTaiKhoan.Clear();txtDienThoai.Clear();txtMatKhau.Clear();
        cboVaiTro.SelectedIndex=0;chkDangLam.Checked=true;lblTrangThai.Text="";SetEditing(false);
    }
    private void Chon(DataGridViewRow row)
    {
        if(!dgvNhanVien.Enabled) return;
        if(row.DataBoundItem is not DataRowView data) return;
        selectedAdmin=(string)data["VaiTro"]=="ADMIN" || ((string)data["TaiKhoan"]).Equals("admin",StringComparison.OrdinalIgnoreCase);
        selectedId=(int)data["MaNV"];txtHoTen.Text=(string)data["HoTen"];
        txtTaiKhoan.Text=(string)data["TaiKhoan"];txtDienThoai.Text=data["DienThoai"].ToString();
        cboVaiTro.SelectedIndex=(string)data["VaiTro"]=="ADMIN"?1:0;
        txtMatKhau.Clear();chkDangLam.Checked=(bool)data["DangLam"];SetEditing(false);
    }
    private void TimKiem()
    {
        if(PosFormStyle.IsInDesigner(this)) return;
        try {ResetDetails();dgvNhanVien.DataSource=bll.TimKiem(txtTimKiem.Text);lblTrangThai.Text=$"{dgvNhanVien.Rows.Count} nhân viên.";}
        catch(Exception ex) {BaoLoi(ex.Message);}
    }
    public void LamMoiDuLieu()
    {
        if(PosFormStyle.IsInDesigner(this)) return;
        bool editing=btnLuu.Enabled;int? id=selectedId;
        dgvNhanVien.DataSource=bll.TimKiem(txtTimKiem.Text);
        if(!editing && id.HasValue)
        {
            var row=dgvNhanVien.Rows.Cast<DataGridViewRow>().FirstOrDefault(r=>(int)r.Cells["MaNV"].Value==id);
            if(row!=null) Chon(row);else ResetDetails();
        }
    }
    private void Luu()
    {
        if(!bll.Luu(selectedId,txtHoTen.Text,txtTaiKhoan.Text,txtMatKhau.Text,txtDienThoai.Text,chkDangLam.Checked,
            PhienDangNhap.NhanVienId,out _,out string error,cboVaiTro.SelectedIndex==1?"ADMIN":"STAFF")) {BaoLoi(error);return;}
        ResetDetails(true);TimKiem();lblTrangThai.ForeColor=Color.DarkGreen;lblTrangThai.Text="Đã lưu nhân viên.";
    }
    private void Khoa()
    {
        if(!selectedId.HasValue) {BaoLoi("Chọn nhân viên cần khóa.");return;}
        if(BaoVeAdmin) {BaoLoi("Không được ngừng hoạt động hoặc khóa tài khoản Admin.");return;}
        if(MessageBox.Show(this,"Khóa tài khoản "+txtTaiKhoan.Text+"? Nhân viên sẽ không đăng nhập được.","Xác nhận",MessageBoxButtons.YesNo,MessageBoxIcon.Question)!=DialogResult.Yes) return;
        if(!bll.Khoa(selectedId.Value,PhienDangNhap.NhanVienId,out string error)) {BaoLoi(error);return;}
        TimKiem();lblTrangThai.ForeColor=Color.DarkGreen;lblTrangThai.Text="Đã khóa tài khoản.";
    }
    private void BaoLoi(string error) {lblTrangThai.ForeColor=Color.Firebrick;lblTrangThai.Text=error;}

}
