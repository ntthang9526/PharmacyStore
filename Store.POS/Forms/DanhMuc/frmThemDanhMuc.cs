using Store.BLL;
namespace Store.POS;
public partial class frmThemDanhMuc : Form
{

    private readonly ErrorProvider errors;
    public int CreatedId { get; private set; }

    public frmThemDanhMuc()
    {
        InitializeComponent();
            global::Store.POS.PosFormStyle.ConfigureDisabledButtons(this);
            PhanQuyenGiaoDien.Gan(this);
        errors = new ErrorProvider { ContainerControl = this, BlinkStyle = ErrorBlinkStyle.NeverBlink };
        btnLuu.Click += Luu;
        AcceptButton = btnLuu;
        CancelButton = btnHuy;
        Shown += (_, _) => txtTen.Focus();
    }

    private void Luu(object? sender, EventArgs e)
    {
        errors.Clear();
        lblLoi.Text = "";
        Control? firstInvalid = null;
        void Validate(Control input, string? error)
        {
            if (error == null) return;
            errors.SetError(input, error);
            if (firstInvalid == null) lblLoi.Text = error;
            firstInvalid ??= input;
        }
        Validate(txtTen, DanhMucValidation.NameError(txtTen.Text, 150));
        Validate(txtDienThoai, DanhMucValidation.PhoneError(txtDienThoai.Text));
        Validate(txtDiaChi, DanhMucValidation.NameError(txtDiaChi.Text, 250));
        if (firstInvalid != null) { firstInvalid.Focus(); return; }
        btnLuu.Enabled = false;
        try
        {
            int id;
            string error;
            bool saved = new NhaCungCapBLL().ThemNhanhNCC(txtTen.Text, txtDienThoai.Text, txtDiaChi.Text, out id, out error);
            if (!saved) { lblLoi.Text = error; txtTen.Focus(); return; }
            CreatedId = id;
            DialogResult = DialogResult.OK;
        }
        finally { btnLuu.Enabled = true; }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing) { errors.Dispose(); components?.Dispose(); }
        base.Dispose(disposing);
    }
}
