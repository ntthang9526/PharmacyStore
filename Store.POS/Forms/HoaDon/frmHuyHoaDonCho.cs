namespace Store.POS;
public partial class frmHuyHoaDonCho : Form
{
    public string LyDo=>txtLyDo.Text.Trim();
    public frmHuyHoaDonCho():this(0) {}
    public frmHuyHoaDonCho(int id)
    {
        InitializeComponent();
        lblThongTin.Text=$"Hủy HD{id:D5} chưa thanh toán.";
        btnXacNhan.Click+=(_,_)=>
        {
            if(LyDo.Length==0) {lblLoi.Text="Nhập lý do hủy.";txtLyDo.Focus();return;}
            DialogResult=DialogResult.OK;Close();
        };
        PosFormStyle.ConfigureDisabledButtons(this);
    }
}

