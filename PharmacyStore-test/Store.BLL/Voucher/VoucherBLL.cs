using System.Data;
using Store.DAL;
using Store.Shared;

namespace Store.BLL
{
    public partial class VoucherBLL
    {
        private VoucherDAL dal = new VoucherDAL();

        // Kiểm tra mã voucher: tồn tại, đang hoạt động, còn lượt dùng
        public bool KiemTra(string code, out VoucherInfo? voucher, out string error)
        {
            voucher = null;
            error = "";

            if (string.IsNullOrWhiteSpace(code))
            {
                error = "Vui lòng nhập mã voucher!";
                return false;
            }

            try
            {
                DataTable dt = dal.LayVoucher(code.Trim().ToUpper());
                if (dt.Rows.Count == 0)
                {
                    error = "Mã voucher không tồn tại!";
                    return false;
                }

                DataRow r = dt.Rows[0];
                if (!Convert.ToBoolean(r["IsActive"]))
                {
                    error = "Voucher đang bị khóa!";
                    return false;
                }
                if (Convert.ToInt32(r["Quantity"]) <= 0)
                {
                    error = "Voucher đã hết lượt sử dụng!";
                    return false;
                }

                voucher = new VoucherInfo
                {
                    Id = Convert.ToInt32(r["Id"]),
                    Code = r["Code"].ToString() ?? "",
                    DiscountType = (r["DiscountType"].ToString() ?? "FIXED").Trim().ToUpper(),
                    DiscountValue = Convert.ToDecimal(r["DiscountValue"]),
                    Quantity = Convert.ToInt32(r["Quantity"])
                };
                return true;
            }
            catch (Exception ex)
            {
                error = "Lỗi kiểm tra voucher: " + ex.Message;
                return false;
            }
        }

        // Tính số tiền được giảm: PERCENT = % trên tổng tiền, FIXED = số tiền cố định; không vượt quá tổng tiền
        public decimal TinhGiamGia(VoucherInfo voucher, decimal tongTien)
        {
            if (tongTien <= 0) return 0;

            decimal giam = voucher.DiscountType == "PERCENT"
                ? TienTe.LamTron(tongTien * voucher.DiscountValue / 100m)
                : TienTe.LamTron(voucher.DiscountValue);

            if (giam < 0) giam = 0;
            if (giam > tongTien) giam = tongTien;
            return giam;
        }
    }
}
