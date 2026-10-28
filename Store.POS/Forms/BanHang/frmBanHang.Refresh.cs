using System.Data;
using Store.BLL;
using Store.DAL;
namespace Store.POS;
public partial class frmBanHang
{

    public void LamMoiDuLieu()
    {
        if(PosFormStyle.IsInDesigner(this)) return;
        danhSachThuoc=medicineDAL.GetMedicines();
        for(int i=0;i<gioHang.Count;i++)
        {
            var row=gioHang[i];
            if(danhSachThuoc.TryGetValue(row.Id,out var medicine))
                {row.Name=medicine.Name;row.Unit=medicine.Unit;row.Price=medicine.Price;gioHang.ResetItem(i);}
        }
        if(voucherApDung!=null)
        {
            voucherApDung=voucherBLL.KiemTra(voucherApDung.Code,out var freshVoucher,out _)?freshVoucher:null;
            CapNhatThongTinVoucher();
        }
        if(khachHang!=null)
        {
            var customer=customerDAL.GetCustomerById(khachHang.Id);
            if(customer!=null) boxMaKhachHang.Text=customer.Phone;
            khachHang=customer;CapNhatThongTinKhachHang();
        }
        labelNhanVien.Text="Nhân viên: "+PhienDangNhap.HoTen;
        boxTimKiem_TextChanged(this,EventArgs.Empty);CapNhatGiaTien();
    }
    private void KiemTraGioHangHienTai()
    {
        var errors=new List<string>();
        foreach(var row in gioHang)
        {
            if(!danhSachThuoc.ContainsKey(row.Id)) errors.Add(row.Name+" đã ngừng bán");
            else
            {
                int stock=medicineDAL.SoLuongConHan(row.Id);
                if(row.Quantity>stock) errors.Add(row.Name+" chỉ còn "+stock);
            }
        }
        lblTinhTrangGioHang.Text=string.Join("; ",errors);
        btnThanhToan.Enabled=btnLuuCho.Enabled=gioHang.Count>0 && errors.Count==0;
    }
}
