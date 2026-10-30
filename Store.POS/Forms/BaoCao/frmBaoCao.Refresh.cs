using System.Data;
using Store.BLL;
using Store.DAL;
namespace Store.POS;
public partial class frmBaoCao
{

    public async void LamMoiDuLieu()
    {
        if(!PosFormStyle.IsInDesigner(this)) await XemBaoCaoAsync(false);
    }
}
