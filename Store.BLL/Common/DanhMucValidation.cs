using System.Text.RegularExpressions;
namespace Store.BLL;
public static class DanhMucValidation
{
    public static string NormalizeName(string value) => Regex.Replace(value.Trim(), @"\s+", " ");
    public static string? NameError(string? value, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(value)) return "Không được để trống.";
        if (value.Trim().Length > maxLength) return $"Tối đa {maxLength} ký tự.";
        if (value.Any(char.IsControl)) return "Không nhập ký tự xuống dòng hoặc ký tự điều khiển.";
        return null;
    }
    public static string? CustomerPhoneError(string? value) =>
        Regex.IsMatch(value?.Trim() ?? "", @"\A0[0-9]{9}\z") ? null : "Số điện thoại khách hàng phải gồm đúng 10 chữ số, bắt đầu bằng 0.";

    public static string? PhoneError(string? value) =>
        Regex.IsMatch(value?.Trim() ?? "", @"^0[0-9]{8,10}$") ? null : "Số điện thoại gồm 9–11 chữ số, bắt đầu bằng 0.";
}
