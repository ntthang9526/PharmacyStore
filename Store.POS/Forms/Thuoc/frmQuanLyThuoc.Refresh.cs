using System.Data;
using Store.BLL;
using Store.DAL;
namespace Store.POS;
public partial class frmQuanLyThuoc
{

    public void LamMoiDuLieu()
    {
        if(PosFormStyle.IsInDesigner(this)) return;
        bool editing=txtTenThuoc.Enabled;string medicineId=maDangChon;
        int? category=cboNhomThuoc.SelectedValue is int cat?cat:null;
        int? supplier=cboNhaCungCap.SelectedValue is int sup?sup:null;
        dgvThuoc.DataSource=bll.TimKiemThuoc(txtTimKiem.Text);
        PosFormStyle.RebindCombo(cboNhomThuoc,new NhomThuocBLL().GetAllNhomThuoc(),category);
        PosFormStyle.RebindCombo(cboNhaCungCap,new NhaCungCapBLL().GetAllNhaCungCap(),supplier);
        var row=dgvThuoc.Rows.Cast<DataGridViewRow>().FirstOrDefault(r=>r.Cells["MaThuoc"].Value.ToString()==medicineId);
        if(row!=null)
        {
            txtSoLuongTon.Text=row.Cells["SoLuongTon"].Value.ToString();
            if(!editing) dgvThuoc_CellClick(dgvThuoc,new DataGridViewCellEventArgs(0,row.Index));
        }
        else if(!editing && medicineId.Length>0) ResetDetails();
    }
}
