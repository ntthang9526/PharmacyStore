using System.Data;
using Store.BLL;
using Store.DAL;
namespace Store.POS;
public partial class frmKhachHang
{
    private void btnTamAn_Click(object? sender,EventArgs e) => CuaSoLamViec.TamAn(this);
    public void LamMoiDuLieu()
    {
        if(PosFormStyle.IsInDesigner(this)) return;
        bool editing=txtTenKH.Enabled;string id=txtMaKH.Text;
        dgvKhachHang.DataSource=bll.TimKiemKhachHang(txtTimKiem.Text);
        if(!editing && id.Length>0)
        {
            var row=dgvKhachHang.Rows.Cast<DataGridViewRow>().FirstOrDefault(r=>r.Cells["MaKH"].Value.ToString()==id);
            if(row!=null) dgvKhachHang_CellClick(dgvKhachHang,new DataGridViewCellEventArgs(0,row.Index));else ResetDetails();
        }
    }
}
