using System.Data;
using Store.BLL;
using Store.DAL;
namespace Store.POS;
public partial class frmBanHang
{

    public async void LamMoiDuLieu()=>await LamMoiDuLieuAsync();
    public Task<bool> LamMoiDuLieuAsync()
    {
        if(PosFormStyle.IsInDesigner(this)) return Task.FromResult(false);
        string? code=voucherApDung?.Code;int? customerId=khachHang?.Id;
        return PosBackground.LoadAsync(this,()=>
        {
            var catalog=medicineDAL.GetMedicines();
            Store.Shared.VoucherInfo? voucher=null;
            if(code!=null) voucherBLL.KiemTra(code,out voucher,out _);
            var customer=customerId.HasValue?customerDAL.GetCustomerById(customerId.Value):null;
            return (catalog,voucher,customer);
        },data=>
        {
            danhSachThuoc=data.catalog;
            for(int i=0;i<gioHang.Count;i++)
            {
                var row=gioHang[i];
                if(danhSachThuoc.TryGetValue(row.Id,out var medicine))
                {row.Name=medicine.Name;row.Unit=medicine.Unit;row.Price=medicine.Price;row.IsPrescription|=medicine.IsPrescription;gioHang.ResetItem(i);}
            }
            voucherApDung=data.voucher;
            khachHang=data.customer;
            if(khachHang!=null) boxMaKhachHang.Text=khachHang.Phone;
            CapNhatThongTinKhachHang();CapNhatThongTinVoucher();
            labelNhanVien.Text="Nhân viên: "+PhienDangNhap.HoTen;
            boxTimKiem_TextChanged(this,EventArgs.Empty);CapNhatGiaTien();
        },error=>MessageBox.Show(this,"Không tải lại được giỏ hàng: "+error.Message,"Bán hàng"));
    }
    private void KiemTraTonTrongGio(int id,int quantity)
    {
        if(quantity<=0) throw new InvalidOperationException("Số lượng phải lớn hơn 0.");
        if(!danhSachThuoc.TryGetValue(id,out var medicine) || medicine.AvailableStock<quantity)
            throw new InvalidOperationException("Không đủ tồn có thể bán. Bấm Làm mới để cập nhật.");
    }
    private void KiemTraGioHangHienTai()
    {
        var errors=new List<string>();
        foreach(var row in gioHang)
        {
            if(!danhSachThuoc.ContainsKey(row.Id)) errors.Add(row.Name+" đã ngừng bán");
            else
            {
                int stock=danhSachThuoc[row.Id].AvailableStock;
                if(row.Quantity>stock) errors.Add(row.Name+" chỉ còn "+stock);
            }
        }
        lblTinhTrangGioHang.Text=string.Join("; ",errors);
        btnThanhToan.Enabled=btnLuuCho.Enabled=gioHang.Count>0 && errors.Count==0;
        if(suaHoaDonId.HasValue)
            btnLuuCho.Enabled=gioHang.Count>0 && gioHang.All(row=>danhSachThuoc.ContainsKey(row.Id));
    }
}
