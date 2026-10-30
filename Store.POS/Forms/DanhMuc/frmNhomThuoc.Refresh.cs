using System.Data;
using Store.BLL;
using Store.DAL;
namespace Store.POS;
public partial class frmNhomThuoc
{

    public void LamMoiDuLieu()
    {
        if(PosFormStyle.IsInDesigner(this)) return;
        bool editing=txtTenNhom.Enabled;string id=maDangChon;
        dgvNhomThuoc.DataSource=bll.TimKiemNhom(txtTimNhom.Text);
        if(!editing && id.Length>0)
        {
            var row=dgvNhomThuoc.Rows.Cast<DataGridViewRow>().FirstOrDefault(r=>r.Cells["MaNhom"].Value.ToString()==id);
            if(row!=null) dgvNhomThuoc_CellClick(dgvNhomThuoc,new DataGridViewCellEventArgs(0,row.Index));else ResetDetails();
        }
    }
}
