using Store.Shared;

namespace PharmacyWeb.Models;

public class BankTransferOptions
{
    public const string SectionName = "PaymentSettings:BankTransfer";

    public bool IsEnabled { get; set; } = true;
    public string TransferInstruction { get; set; } = "Nhà thuốc sẽ xác nhận thanh toán sau khi nhận được chuyển khoản.";

    public string BankName => BankTransferDetails.BankName;
    public string AccountNumber => BankTransferDetails.AccountNumber;
    public string AccountHolder => BankTransferDetails.AccountHolder;
}
