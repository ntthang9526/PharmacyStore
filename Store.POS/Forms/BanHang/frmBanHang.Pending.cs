using System.Data;
using Store.BLL;
using Store.POS.FormBanHang;
namespace Store.POS;

public partial class frmBanHang
{
    private readonly int? suaHoaDonId;
    private byte[]? phienBanDonCho;

    private void NapHoaDonCho()
    {
        if(!suaHoaDonId.HasValue) return;
        var data=new HoaDonBLL().LayDonCho(suaHoaDonId.Value);
        var header=data.Tables[0].Rows[0];
        donThuoc=HoaDonBLL.DocDonThuoc(header);
        phienBanDonCho=(byte[])header["OrderVersion"];
        groupGioHang.Text=$"Sửa HD{suaHoaDonId:D5} · giá hiện hành";
        Text=$"Sửa hóa đơn HD{suaHoaDonId:D5}";
        groupThanhToan.Text="Thông tin đơn chờ";
        btnLuuCho.Text="Lưu thay đổi";btnHuyHoaDon.Text="Bỏ thay đổi";
        btnThanhToan.Visible=radioTienMat.Visible=radioChuyenKhoan.Visible=panelTienMat.Visible=false;
        foreach(DataRow item in data.Tables[1].Rows)
        {
            int id=(int)item["MedicineId"];
            danhSachThuoc.TryGetValue(id,out var medicine);
            gioHang.Add(new RowData{Id=id,No=gioHang.Count+1,Name=medicine?.Name??(string)item["TenThuoc"],
                Unit=medicine?.Unit??(string)item["DonViTinh"],Quantity=(int)item["Quantity"],
                Price=medicine?.Price??(decimal)item["UnitPrice"],IsPrescription=Convert.ToBoolean(item["KeDon"]) || medicine?.IsPrescription==true,HuongDan=(string)item["HuongDan"]});
        }
        if(!header.IsNull("CustomerId"))
        {
            khachHang=customerDAL.GetCustomerById((int)header["CustomerId"]);
            boxMaKhachHang.Text=khachHang?.Phone??"";
        }
        string code=(string)header["Voucher"];
        boxMaVoucher.Text=code;
        if(code.Length>0)
        {
            if(voucherBLL.KiemTra(code,out var voucher,out string error)) voucherApDung=voucher;
            else
            {
                boxMaVoucher.Clear();
                MessageBox.Show(this,"Đã bỏ voucher cũ vì không còn dùng được. "+error,"Sửa hóa đơn chờ");
            }
        }
        CapNhatThongTinKhachHang();CapNhatThongTinVoucher();
    }
}

