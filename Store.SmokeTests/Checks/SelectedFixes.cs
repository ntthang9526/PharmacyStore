using System.ComponentModel;
using Store.BLL;
using Store.DAL;
using Store.POS;
using Store.POS.FormBanHang;
using Store.Shared;
using Store.Shared.Models;

internal static partial class Program
{
    static void TestSelectedFixes()
    {
        Check(TienTe.LamTron(1000.25m)==1000 && TienTe.LamTron(1000.50m)==1001 && TienTe.LamTron(1000.75m)==1001,
            "currency rounds to nearest whole dong, half upward");
        var row=new RowData {Price=1000.25m,Quantity=3};
        Check(row.Price==1000 && row.Total==3000,"unit price rounds before multiplying quantity");
        var vouchers=new VoucherBLL();
        Check(vouchers.TinhGiamGia(new VoucherInfo {DiscountType="FIXED",DiscountValue=100.50m},3000)==101,
            "fixed voucher rounds using the same currency rule");
        Check(vouchers.TinhGiamGia(new VoucherInfo {DiscountType="PERCENT",DiscountValue=12.5m},1004)==126,
            "fractional percentage remains supported with whole-dong discount");
        foreach(string phone in new[]{"", "090123456", "09012345678", "1901234567", "09012a4567", "０９０１２３４５６７", "0901234567\nX"})
        {
            var customers=new KhachHangBLL();
            Check(DanhMucValidation.CustomerPhoneError(phone)!=null &&
                  !customers.ThemMoiKhachHang("",prefix+" Invalid",phone,"0",out _) &&
                  !customers.SuaKhachHang(customerId.ToString(),prefix+" Invalid",phone,"0",out _),
                  "customer phone rejected consistently on create/update: "+phone.Replace("\n","\\n"));
        }
        Check(DanhMucValidation.CustomerPhoneError("0123456789")==null && DanhMucValidation.PhoneError("02812345678")==null,
            "customer accepts ten digits; supplier landline rule retained");
        Check((string)Scalar("SELECT FullName FROM Customers WHERE Id=@id",("@id",customerId))!=prefix+" Invalid",
            "invalid customer edits do not change saved record");

        using(var form=new frmQuanLyThuoc())
        {
            var grid=UiField<DataGridView>(form,"dgvThuoc");
            Check(!grid.AutoGenerateColumns && grid.Columns.Count==9,"medicine columns exist before data load in Designer");
            Check(grid.Columns["TenThuoc"].HeaderText=="Tên thuốc" && grid.Columns["DonGia"].DefaultCellStyle.Format=="N0" &&
                  !grid.Columns["MaNhom"].Visible && !grid.Columns["MaNCC"].Visible,
                  "medicine Designer declares Vietnamese headers, whole money and hidden foreign keys");
            Offscreen(form);
            grid.CurrentCell=grid.Rows.Cast<DataGridViewRow>().Single(r=>Convert.ToInt32(r.Cells["MaThuoc"].Value)==medicineId).Cells["TenThuoc"];
            typeof(DataGridView).GetMethod("OnCellClick",PrivateInstance)!.Invoke(grid,new object[]{new DataGridViewCellEventArgs(grid.Columns["TenThuoc"].Index,grid.CurrentCell.RowIndex)});
            Check(UiField<TextBox>(form,"txtDonGia").Text=="1000","medicine selected price editor has no fractional digits");
            Check(grid.Columns.Count==9 && grid.Columns["TenNCC"].Visible && grid.Columns["TenNhom"].Visible,
                "runtime preserves explicit columns and displays supplier/group names");
        }

        using(var form=new frmKhachHang())
        {
            var grid=UiField<DataGridView>(form,"dgvKhachHang");
            Check(!grid.AutoGenerateColumns && grid.Columns.Count==4 && grid.Columns["DienThoai"].HeaderText=="Điện thoại" &&
                  UiField<TextBox>(form,"txtDienThoai").MaxLength==10,"customer Designer preserves columns and ten-digit phone input");
        }
        string barcode="00"+medicineId.ToString("D11");
        Scalar("UPDATE Medicines SET BarCode=@code, Price=1000.25 WHERE Id=@id",("@code",barcode),("@id",medicineId));
        using(var form=new frmBanHang())
        {
            Offscreen(form);
            var cache=UiField<Dictionary<int,Medicine>>(form,"danhSachThuoc");
            Check(cache[medicineId].BarCode==barcode && cache[medicineId].Price==1000,
                "existing fractional DB price is normalized and leading-zero barcode loads unchanged");
            var phoneCheck=typeof(frmBanHang).GetMethod("ValidatePhone",PrivateInstance)!;
            Check((bool)phoneCheck.Invoke(form,new object[]{"0123456789"})! && !(bool)phoneCheck.Invoke(form,new object[]{"090123456"})!,
                "sales phone validation agrees with customer management");
            Check(UiField<TextBox>(form,"boxMaKhachHang").MaxLength==10,"sales phone input limits ten digits");
            UiField<TextBox>(form,"boxTimKiem").Text=barcode;
            Check(UiField<BindingList<Medicine>>(form,"timKiem").Any(m=>m.Id==medicineId),"sales text search finds barcode");
            var input=UiField<TextBox>(form,"boxNhapSanPham");
            input.Text=barcode;
            var key=new KeyEventArgs(Keys.Enter);
            typeof(frmBanHang).GetMethod("boxNhapSanPham_KeyDown",PrivateInstance)!.Invoke(form,new object[]{input,key});
            var cart=UiField<BindingList<RowData>>(form,"gioHang");
            Check(cart.Count==1 && cart[0].Id==medicineId && cart[0].Quantity==1 && key.SuppressKeyPress,
                "scanner Enter accepts thirteen-digit leading-zero barcode once");
            input.Text=medicineId.ToString();UiField<Button>(form,"btnNhapSanPham").PerformClick();
            Check(cart.Count==1 && cart[0].Quantity==2,"internal medicine ID remains accepted");
            cache[medicineId].BarCode="1";
            input.Text="1";UiField<Button>(form,"btnNhapSanPham").PerformClick();
            Check(cart.Count==1 && cart[0].Quantity==3,"exact barcode takes priority over colliding numeric internal ID");
            Check(UiField<decimal>(form,"tongTien")==3000,"sales amount is whole dong for legacy fractional price");
            UiField<TextBox>(form,"boxTienKhach").Text="3000";
            Check(UiField<TextBox>(form,"boxTraLai").Text=="0","displayed exact cash amount is sufficient with zero change");
            UiField<TextBox>(form,"boxTienKhach").Text="3000.25";
            Check(UiField<TextBox>(form,"boxTraLai").Text=="","pasted fractional cash is not accepted as whole-dong payment");
        }
        using(var qr=new frmThanhToanQR(1000.50m))
            Check(UiField<decimal>(qr,"tongTien")==1001 && UiField<Label>(qr,"lblTongTien").Text.Contains("1,001"),
                "QR stored amount and label share the whole-dong policy without network request");
    }
}
