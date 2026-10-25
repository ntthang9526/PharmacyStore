namespace Store.Shared;

public static class TienTe
{
    // Làm tròn từng đơn giá trước khi nhân số lượng; dùng cùng quy tắc ở POS và DB.
    public static decimal LamTron(decimal value) => Math.Round(value, 0, MidpointRounding.AwayFromZero);
}
