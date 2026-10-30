using System.Data;
using Store.BLL;
using Store.DAL;
namespace Store.POS;
public partial class frmCanhBao
{

    public void LamMoiDuLieu()
    {
        if(!PosFormStyle.IsInDesigner(this)) LocCanhBao();
    }
}
