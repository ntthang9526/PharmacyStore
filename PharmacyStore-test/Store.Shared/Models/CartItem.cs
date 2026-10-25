namespace Store.Shared
{
    /// <summary>Một dòng trong giỏ hàng (theo thuốc, chưa tách theo lô).</summary>
    public class CartItem
    {
        public int MaThuoc { get; set; }
        public string TenThuoc { get; set; } = "";
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
    }
}
