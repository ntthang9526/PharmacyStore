using System.ComponentModel.DataAnnotations;

namespace PharmacyWeb.Models.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Vui lòng nhập số điện thoại hoặc email")]
    [Display(Name = "Số điện thoại / Email")]
    public string UsernameOrEmail { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
    [DataType(DataType.Password)]
    [Display(Name = "Mật khẩu")]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "Ghi nhớ đăng nhập")]
    public bool RememberMe { get; set; }

    public string? ReturnUrl { get; set; }
}

public class RegisterViewModel
{
    [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
    [Display(Name = "Họ và tên")]
    [MaxLength(100, ErrorMessage = "Họ tên không vượt quá 100 ký tự")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
    [Display(Name = "Số điện thoại")]
    [RegularExpression(@"^0[35789]\d{8}$", ErrorMessage = "Số điện thoại không đúng định dạng (10 chữ số)")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập email")]
    [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải từ 6 ký tự trở lên")]
    [DataType(DataType.Password)]
    [Display(Name = "Mật khẩu")]
    public string Password { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    [Display(Name = "Xác nhận mật khẩu")]
    [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập địa chỉ giao hàng")]
    [Display(Name = "Địa chỉ nhận hàng mặc định")]
    [MaxLength(255)]
    public string Address { get; set; } = string.Empty;
}

public class ChangePasswordViewModel
{
    [Required(ErrorMessage = "Vui lòng nhập mật khẩu hiện tại")]
    [DataType(DataType.Password)]
    [Display(Name = "Mật khẩu hiện tại")]
    public string CurrentPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập mật khẩu mới")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu mới phải từ 6 ký tự trở lên")]
    [DataType(DataType.Password)]
    [Display(Name = "Mật khẩu mới")]
    public string NewPassword { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    [Display(Name = "Xác nhận mật khẩu mới")]
    [Compare("NewPassword", ErrorMessage = "Mật khẩu xác nhận không khớp")]
    public string ConfirmPassword { get; set; } = string.Empty;
}

public class CustomerProfileViewModel
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int Points { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<AddressItemViewModel> Addresses { get; set; } = new();
    public ChangePasswordViewModel ChangePassword { get; set; } = new();
}

public class AddressItemViewModel
{
    public int Id { get; set; }
    public string Label { get; set; } = string.Empty;
    public string RecipientName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public bool IsDefault { get; set; }
}

public class MyOrderListViewModel
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
    public string OrderStatus { get; set; } = "NEW";
    public string PaymentStatus { get; set; } = "UNPAID";
    public string ShippingStatus { get; set; } = "NOT_SHIPPED";
    public decimal TotalAmount { get; set; }
    public int TotalItemCount { get; set; }
    public bool CanCancel => OrderStatus == "NEW";
    public List<OrderItemViewModel> Items { get; set; } = new();
}
