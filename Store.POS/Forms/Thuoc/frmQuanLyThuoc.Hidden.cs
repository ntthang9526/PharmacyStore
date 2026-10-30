namespace Store.POS;

public partial class frmQuanLyThuoc
{
    private bool ChiThuocDaAn => PhienDangNhap.LaAdmin && chkDaAn.Checked;

    private void chkDaAn_CheckedChanged(object? sender,EventArgs e)
    {
        if(PosFormStyle.IsInDesigner(this)) return;
        btnTimKiem_Click(this,EventArgs.Empty);
    }

    private void HienLaiThuoc()
    {
        if(!PhienDangNhap.LaAdmin) return;
        if(string.IsNullOrEmpty(maDangChon))
        {MessageBox.Show(this,"Chọn thuốc cần hiện lại.","Thông báo");return;}
        if(MessageBox.Show(this,"Hiện lại thuốc này? Trạng thái bán được giữ nguyên.","Hiện lại thuốc",
            MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2)!=DialogResult.Yes) return;
        if(!bll.HienLaiThuoc(maDangChon,out string error))
        {MessageBox.Show(this,error,"Chưa hiện lại thuốc");return;}
        ResetDetails(true);
        LoadDataToGrid();
        MessageBox.Show(this,"Đã hiện lại thuốc.","Thông báo");
    }
}
