using System.Data;
using Store.BLL;
using Store.DAL;
namespace Store.POS;
public partial class frmVoucher
{

    public void LamMoiDuLieu()
    {
        using var loading=PosLoading.Show(this);
        if(PosFormStyle.IsInDesigner(this)) return;
        bool editing=btnLuu.Enabled;int? id=selectedId;
        dgvVoucher.DataSource=bll.TimKiem(txtTimKiem.Text);
        if(!editing && id.HasValue)
        {
            var row=dgvVoucher.Rows.Cast<DataGridViewRow>().FirstOrDefault(r=>(int)r.Cells["Id"].Value==id);
            if(row!=null) Chon(row);else ResetDetails();
        }
    }
}
