namespace PharmacyWeb.Models;

public class BankTransferOptions
{
    public const string SectionName = "PaymentSettings:BankTransfer";

    public bool IsEnabled { get; set; } = true;
    public bool IsDemoAccount { get; set; } = true;
    public string BankName { get; set; } = "Vietcombank (Chi nhánh Cần Thơ)";
    public string AccountNumber { get; set; } = "1029384756";
    public string AccountHolder { get; set; } = "NHA THUOC PHARMACY STORE";
    public string TransferInstruction { get; set; } = "Dược sĩ sẽ kiểm tra biến động số dư và xác nhận ngay cho quý khách.";
    public string DemoNotice { get; set; } = "Thông tin tài khoản ngân hàng thử nghiệm phục vụ đồ án/demo. Quý khách vui lòng không thực hiện giao dịch chuyển tiền thật.";
}
