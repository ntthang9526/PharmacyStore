using System.Security.Cryptography;
using System.Text;
namespace Store.BLL;

internal static class MatKhauNhanVien
{
    private const string Prefix = "PBKDF2-SHA256$";
    public static string Tao(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, 100000, HashAlgorithmName.SHA256, 32);
        return Prefix + "100000$" + Convert.ToBase64String(salt) + "$" + Convert.ToBase64String(hash);
    }
    public static bool KiemTra(string password, string stored)
    {
        // Giữ đăng nhập được tài khoản cũ; mật khẩu mới/đổi đều dùng PBKDF2.
        if (!stored.StartsWith(Prefix, StringComparison.Ordinal))
            return CryptographicOperations.FixedTimeEquals(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(stored));
        try
        {
            string[] parts = stored.Split('$');
            if (parts.Length != 4 || parts[1] != "100000") return false;
            byte[] salt = Convert.FromBase64String(parts[2]), expected = Convert.FromBase64String(parts[3]);
            if (salt.Length != 16 || expected.Length != 32) return false;
            return CryptographicOperations.FixedTimeEquals(expected,
                Rfc2898DeriveBytes.Pbkdf2(password, salt, 100000, HashAlgorithmName.SHA256, 32));
        }
        catch (FormatException) { return false; }
    }
}
