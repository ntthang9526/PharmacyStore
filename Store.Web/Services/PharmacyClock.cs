namespace PharmacyWeb.Services;

public static class PharmacyClock
{
    // Hạn trên nhãn dùng hết ngày tại Việt Nam; đồng nhất với POS trên máy local.
    public static DateTime Today => DateTime.UtcNow.AddHours(7).Date;
}
