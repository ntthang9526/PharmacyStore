using Store.BLL;
using System.Data;
namespace Store.POS;
public partial class frmLoHang : Form, ILamMoiDuLieu
{
    private readonly LoHangBLL bll = new();

    public frmLoHang()
    {
        InitializeComponent();
            global::Store.POS.PosFormStyle.ConfigureDisabledButtons(this);
        PhanQuyenGiaoDien.Gan(this);
        PosFormStyle.ConfigureGridSelection(this, posLayout, CapNhatNutTrangThai, clearSelectionOnLeave: false);
        dgvLoHang.SelectionChanged+=(_,_)=>CapNhatNutTrangThai();
        btnDoiTrangThai.Click+=(_,_)=>DoiTrangThai();
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
            lblKetQua.Text = $"{data.Rows.Count} lô hàng. Tồn có thể bán đã loại lô hết hạn/bị khóa và thuốc ngừng bán.";
        }
        catch (Exception ex) { MessageBox.Show("Không tải được lô hàng: " + ex.Message, "Lỗi"); }
    }

    private DataRowView? LoDangChon => dgvLoHang.SelectedRows.Count==1 ? dgvLoHang.SelectedRows[0].DataBoundItem as DataRowView : null;
    private void CapNhatNutTrangThai()
    {
        var row=LoDangChon;
        btnDoiTrangThai.Enabled=PhienDangNhap.LaAdmin && row!=null;
        btnDoiTrangThai.Text=row!=null && (bool)row["KhoaLo"] ? "Mở khóa" : "Tạm khóa";
    }
    private void DoiTrangThai()
    {
        var row=LoDangChon;if(row==null || !PhienDangNhap.LaAdmin)return;
        int id=(int)row["MaLo"];bool khoa=!(bool)row["KhoaLo"];
        string action=khoa?"Tạm khóa":"Mở khóa";
        if(MessageBox.Show(this,$"{action} lô {row["SoLo"]}? Tồn kho không thay đổi.",action,MessageBoxButtons.YesNo,MessageBoxIcon.Question)!=DialogResult.Yes)return;
        if(!bll.DoiTrangThai(id,khoa,out string error))MessageBox.Show(this,error,"Không đổi được trạng thái",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        TimKiem();
    }
}
