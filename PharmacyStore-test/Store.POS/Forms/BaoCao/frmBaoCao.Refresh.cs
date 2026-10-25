using System.Data;
using Store.BLL;
using Store.DAL;
namespace Store.POS;
public partial class frmBaoCao
{
    private void btnTamAn_Click(object? sender,EventArgs e) => CuaSoLamViec.TamAn(this);
    public void LamMoiDuLieu()
    {
        if(!PosFormStyle.IsInDesigner(this)) XemBaoCao(false);
    }
}
