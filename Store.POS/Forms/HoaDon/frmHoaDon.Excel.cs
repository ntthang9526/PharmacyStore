using System.Data;
using Store.POS.FormBanHang;
using Store.POS.Common;
namespace Store.POS;
public partial class frmHoaDon
{
    private DataRow? DongDangXem() => (dgvHoaDon.DataSource as DataTable)?.Rows.Cast<DataRow>().FirstOrDefault(r=>(int)r["MaHD"]==hoaDonDangXem);
    private void ThanhToanCho()
    {
        var row=DongDangXem();if(row==null || (string)row["PaymentStatus"]!="PENDING") return;
        string method=cboHinhThuc.SelectedItem as string??"";decimal due=(decimal)row["CanThanhToan"];
        if(method=="Chuyển khoản QR")
        {using var qr=new frmThanhToanQR(due);if(qr.ShowDialog(this)!=DialogResult.OK) return;}
        else if(MessageBox.Show(this,$"Xác nhận đã nhận đủ {due:N0} đ tiền mặt cho HD{hoaDonDangXem:D5}?","Thanh toán hóa đơn chờ",MessageBoxButtons.YesNo,MessageBoxIcon.Question)!=DialogResult.Yes) return;
        btnThanhToanCho.Enabled=false;
        try
        {
            if(!bll.ThanhToanCho(hoaDonDangXem!.Value,method,due,out string error)) {MessageBox.Show(this,error,"Chưa thanh toán");LamMoiDuLieu();return;}
            LamMoiDuLieu();MessageBox.Show(this,"Đã thanh toán. Tồn kho, voucher và doanh thu đã cập nhật.","Thành công");
        }
        finally {btnThanhToanCho.Enabled=true;}
    }
    private void XuatDanhSachExcel()
    {
        if(dgvHoaDon.DataSource is not DataTable data || data.Rows.Count==0) {MessageBox.Show(this,"Không có hóa đơn để xuất.");return;}
        try
        {
            ExcelHelper.XuatDanhSachHoaDon(data,dtpTuNgay.Checked?dtpTuNgay.Value:null,dtpDenNgay.Checked?dtpDenNgay.Value:null);
        }
        catch(Exception ex){MessageBox.Show(this,ex.Message,"Xuất Excel");}
    }
    private void XuatChiTietExcel()
    {
        var row=DongDangXem();if(row==null || dgvChiTiet.DataSource is not DataTable details) return;
        try
        {
            ExcelHelper.XuatChiTietMotHoaDon((int)row["MaHD"],(string)row["KhachHang"],(string)row["NhanVien"],(DateTime)row["NgayLap"],
                (decimal)row["TongTien"],(decimal)row["GiamGia"],(decimal)row["ThucThu"],details,(string)row["TrangThai"],(string)row["HinhThucThanhToan"]);
        }
        catch(Exception ex){MessageBox.Show(this,ex.Message,"Xuất Excel");}
    }
}
