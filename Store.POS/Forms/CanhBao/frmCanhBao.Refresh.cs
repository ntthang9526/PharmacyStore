using System.Data;
using Store.BLL;
using Store.DAL;
namespace Store.POS;
public partial class frmCanhBao
{

    public void LamMoiDuLieu()
    {
        using var loading=PosLoading.Show(this);
        if(!PosFormStyle.IsInDesigner(this)) LocCanhBao(throwOnError:true);
    }
}
