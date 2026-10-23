using System.Data;
using Store.BLL;
using Store.DAL;
namespace Store.POS;
public partial class frmNhaCungCap
{
    private void btnTamAn_Click(object? sender,EventArgs e) => CuaSoLamViec.TamAn(this);
    public void LamMoiDuLieu()
    {
        if(PosFormStyle.IsInDesigner(this)) return;
        bool editing=txtTenNCC.Enabled;string id=txtMaNCC.Text;
        dgvNhaCungCap.DataSource=bll.TimKiemNCC(txtTimKiem.Text);
        if(!editing && id.Length>0)
        {
            var row=dgvNhaCungCap.Rows.Cast<DataGridViewRow>().FirstOrDefault(r=>r.Cells["MaNCC"].Value.ToString()==id);
            if(row!=null) dgvNhaCungCap_CellClick(dgvNhaCungCap,new DataGridViewCellEventArgs(0,row.Index));else ResetDetails();
        }
    }
}
