using System.Data;
using Store.BLL;
using Store.DAL;
namespace Store.POS;
public partial class frmNhapKho
{
    private void btnTamAn_Click(object? sender,EventArgs e) => CuaSoLamViec.TamAn(this);
    public void LamMoiDuLieu()
    {
        if(PosFormStyle.IsInDesigner(this)) return;
        int? supplier=cboNhaCungCap.SelectedValue is int sup?sup:null;
        int? medicine=cboThuoc.SelectedValue is int med?med:null;
        string lot=txtSoLo.Text;
        NapNhaCungCap(supplier);
        if(medicine.HasValue)
        {
            cboThuoc.SelectedValue=medicine.Value;
            if(cboThuoc.SelectedValue is not int selected || selected!=medicine.Value) cboThuoc.SelectedIndex=-1;
        }
        CapNhatNhapLo();if(txtSoLo.Enabled) txtSoLo.Text=lot;
        var catalog=new MedicineDAL().GetMedicines();
        foreach(DataRow row in dtPhieu.Rows)
            if(catalog.TryGetValue((int)row["MaThuoc"],out var item)) {row["TenThuoc"]=item.Name;row["DonViTinh"]=item.Unit;}
        CapNhatTongVon();
    }
}
