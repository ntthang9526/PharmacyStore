namespace Store.Shared
{
    /// <summary>Thông tin voucher đọc từ bảng Vouchers.</summary>
    public class VoucherInfo
    {
        public int Id { get; set; }
        public string Code { get; set; } = "";
        public string DiscountType { get; set; } = "FIXED";   // FIXED hoặc PERCENT
        public decimal DiscountValue { get; set; }
        public int Quantity { get; set; }
    }
}
