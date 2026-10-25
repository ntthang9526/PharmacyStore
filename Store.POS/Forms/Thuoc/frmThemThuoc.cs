using Store.BLL;
namespace Store.POS;
public partial class frmThemThuoc : Form
{
    private readonly int supplierId;

    public int CreatedId {get;private set;}

    public frmThemThuoc() : this(0, "Chưa chọn nhà cung cấp") {}

    public frmThemThuoc(int supplierId,string supplierName)
    {
        this.supplierId=supplierId;
        InitializeComponent();
            PhanQuyenGiaoDien.Gan(this);
        label3.Text="NCC: "+supplierName;
        Load+=(_,_)=>NapNhom();
        btnThemNhom.Click+=(_,_)=>
        {
            using var dialog=new frmThemNhomThuoc();
            if(dialog.ShowDialog(this)!=DialogResult.OK) return;
            NapNhom(dialog.CreatedId);
        };
        btnLuu.Click+=(_,_)=>Luu();AcceptButton=btnLuu;CancelButton=btnHuy;
    }
    private void NapNhom(int? selected=null)
    {
        if (PosFormStyle.IsInDesigner(this)) return;
        try
        {
            cboNhom.DisplayMember="TenNhom";cboNhom.ValueMember="MaNhom";
            cboNhom.DataSource=new NhomThuocBLL().GetAllNhomThuoc();
            if(selected.HasValue) cboNhom.SelectedValue=selected.Value; else cboNhom.SelectedIndex=-1;
        }
        catch(Exception ex) {lblLoi.Text="Không tải được nhóm thuốc: "+ex.Message;}
    }
    private void Luu()
    {
        btnLuu.Enabled=false;
        try
        {
            if(!new ThuocBLL().ThemNhanhThuoc(txtTen.Text,cboNhom.SelectedValue is int id?id:0,
                supplierId,txtDonVi.Text,nudGiaBan.Value,out int created,out string error))
            {lblLoi.Text=error;return;}
            CreatedId=created;DialogResult=DialogResult.OK;
        }
        finally {btnLuu.Enabled=true;}
    }
}
