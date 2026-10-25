namespace Store.Shared
{
    /// <summary>Một dòng trên phiếu nhập kho (một thuốc - một lô).</summary>
    public class NhapKhoItem
    {
        public int MaThuoc { get; set; }
        public string TenThuoc { get; set; } = "";
        public string SoLo { get; set; } = "";
        public DateTime HanDung { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaNhap { get; set; }
    }
}
