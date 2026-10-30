using System.Data;
using Store.BLL;
using Store.DAL;
namespace Store.POS;
public partial class frmBaoCao
{

    public async void LamMoiDuLieu()=>await LamMoiDuLieuAsync();
    public Task<bool> LamMoiDuLieuAsync()=>PosFormStyle.IsInDesigner(this)
        ? Task.FromResult(false) : XemBaoCaoAsync(false);
}
