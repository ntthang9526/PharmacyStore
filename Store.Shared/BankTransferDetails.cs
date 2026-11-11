using System.Globalization;

namespace Store.Shared;

public static class BankTransferDetails
{
    public const string BankCode = "970415";
    public const string BankName = "VietinBank";
    public const string AccountNumber = "109669936206";
    public const string AccountHolder = "NGUYEN TRUNG THANG";

    public static string CreateQrImageUrl(decimal amount, string transferMemo)
    {
        string vndAmount = TienTe.LamTron(amount).ToString("0", CultureInfo.InvariantCulture);
        return $"https://img.vietqr.io/image/{BankCode}-{AccountNumber}-compact.svg" +
               $"?amount={vndAmount}&addInfo={Uri.EscapeDataString(transferMemo)}" +
               $"&accountName={Uri.EscapeDataString(AccountHolder)}";
    }
}
