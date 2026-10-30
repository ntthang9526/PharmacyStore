using System.Data;
using Store.BLL;
using Store.DAL;
namespace Store.POS;
public partial class frmQuanLyThuoc
{

    public void LamMoiDuLieu()
    {
        using var loading=PosLoading.Show(this);
        if(PosFormStyle.IsInDesigner(this)) return;
        bool editing=txtTenThuoc.Enabled;string medicineId=maDangChon;
        int? category=cboNhomThuoc.SelectedValue is int cat?cat:null;
        int? supplier=cboNhaCungCap.SelectedValue is int sup?sup:null;
        GanDuLieuThuoc(bll.TimKiemThuoc(txtTimKiem.Text));
        PosFormStyle.RebindCombo(cboNhomThuoc,new NhomThuocBLL().GetAllNhomThuoc(),category);
        PosFormStyle.RebindCombo(cboNhaCungCap,new NhaCungCapBLL().GetAllNhaCungCap(),supplier);
        var row=dgvThuoc.Rows.Cast<DataGridViewRow>().FirstOrDefault(r=>r.DataBoundItem is DataRowView data && Convert.ToString(data["MaThuoc"])==medicineId);
        if(row!=null)
        {
            var record=(DataRowView)row.DataBoundItem;
            txtSoLuongTon.Text=Convert.ToString(record["SoLuongTon"]);
            khoaNhaCungCap=Convert.ToBoolean(record["KhoaNCC"]);
            if(editing && !isThem && khoaNhaCungCap)
            {
                cboNhaCungCap.SelectedValue=record["MaNCC"];
                cboNhaCungCap.Enabled=btnThemNCC.Enabled=false;
            }
            if(!editing) dgvThuoc_CellClick(dgvThuoc,new DataGridViewCellEventArgs(0,row.Index));
        }
        else if(!editing && medicineId.Length>0) ResetDetails();
    }
}
