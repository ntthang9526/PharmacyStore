using Store.BLL;
namespace Store.POS;
public partial class frmLoHang : Form, ILamMoiDuLieu
{
    private readonly LoHangBLL bll = new();

    public frmLoHang()
    {
        InitializeComponent();
        PosFormStyle.ConfigureGridSelection(this, posLayout, null);
        btnTimKiem.Click += (_, _) => TimKiem();
        btnLamMoi.Click += (_, _) => { txtTimKiem.Clear(); TimKiem(); };
                txtTimKiem.KeyDown += (_, e) =>
        {
            if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; TimKiem(); }
        };
        Load += (_, _) => TimKiem();
    }

    private void TimKiem()
    {
        if (PosFormStyle.IsInDesigner(this)) return;
        try
        {
            var data = bll.TimKiem(txtTimKiem.Text);
            dgvLoHang.DataSource = data;
            lblKetQua.Text = $"{data.Rows.Count} lô hàng. Dấu —: chưa có dữ liệu phiếu nhập.";
        }
        catch (Exception ex) { MessageBox.Show("Không tải được lô hàng: " + ex.Message, "Lỗi"); }
    }

}
