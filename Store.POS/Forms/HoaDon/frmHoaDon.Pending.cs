using System.Data;
namespace Store.POS;

public partial class frmHoaDon
{
    private void SuaDonCho()
    {
        var row=DongDangXem();
        if(row==null || (string)row["PaymentStatus"]!="PENDING") return;
        using var editor=new frmBanHang((int)row["MaHD"]);
        editor.ShowDialog(this);
        LamMoiDuLieu();
    }
    private void HuyDonCho()
    {
        var row=DongDangXem();
        if(row==null || (string)row["PaymentStatus"]!="PENDING") return;
        int id=(int)row["MaHD"];byte[] version=(byte[])row["OrderVersion"];
        using var dialog=new frmHuyHoaDonCho(id);
        if(dialog.ShowDialog(this)!=DialogResult.OK) return;
        if(!bll.HuyHoaDonCho(id,version,dialog.LyDo,out string error))
            MessageBox.Show(this,error,"Chưa hủy hóa đơn",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        else MessageBox.Show(this,$"Đã hủy HD{id:D5}.","Đã hủy");
        LamMoiDuLieu();
    }
}

