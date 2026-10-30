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
        if(method is not ("Tiền mặt" or "Chuyển khoản QR"))
        {
            MessageBox.Show(this,"Chọn Tiền mặt hoặc Chuyển khoản QR.","Hình thức thanh toán");
            cboHinhThuc.Focus();return;
        }
        int id=(int)row["MaHD"];
        if(!bll.KiemTraDonThuocCho(id,out string prescriptionError)) {MessageBox.Show(this,prescriptionError,"Kiểm tra đơn thuốc");return;}
        byte[] version=(byte[])row["OrderVersion"];
        if(method=="Chuyển khoản QR")
        {using var qr=new frmThanhToanQR(due);if(qr.ShowDialog(this)!=DialogResult.OK) return;}
        else if(MessageBox.Show(this,$"Đã nhận đủ {due:N0} đ tiền mặt cho HD{hoaDonDangXem:D5}?","Thanh toán hóa đơn chờ",MessageBoxButtons.YesNo,MessageBoxIcon.Question)!=DialogResult.Yes) return;
        btnThanhToanCho.Enabled=false;
        try
        {
            if(!bll.ThanhToanCho(id,method,due,out string error,version)) {MessageBox.Show(this,error,"Chưa thanh toán");LamMoiDuLieu();return;}
            LamMoiDuLieu();MessageBox.Show(this,"Đã thanh toán hóa đơn.","Thành công");
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
                (decimal)row["TongTien"],(decimal)row["GiamGia"],(decimal)row["ThucThu"],details,
                (string)row["TrangThai"]+((string)row["PaymentStatus"]=="CANCELLED"?$" · {row["NhanVienHuy"]} · {row["CancelledAt"]:dd/MM/yyyy HH:mm} · {row["CancelReason"]}":""),
                (string)row["HinhThucThanhToan"],headerSource:(string)row["NguonDauHoaDon"]);
        }
        catch(Exception ex){MessageBox.Show(this,ex.Message,"Xuất Excel");}
    }
}
