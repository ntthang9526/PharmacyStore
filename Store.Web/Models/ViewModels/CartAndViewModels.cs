using System.ComponentModel.DataAnnotations;

namespace PharmacyWeb.Models.ViewModels;

public class CartItemViewModel
{
    public int MedicineId { get; set; }
    public string MedicineName { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Unit { get; set; } = "Hộp";
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice => Price * Quantity;
    public int AvailableStock { get; set; }
    public bool IsPrescription { get; set; }
    public bool IsSelling { get; set; }
    public bool IsActive { get; set; }
    public bool HasStockShortage => Quantity > AvailableStock || !IsSelling || !IsActive;
    public string? ImageUrl { get; set; }
}

public class CartViewModel
{
    public List<CartItemViewModel> Items { get; set; } = new();
    public decimal SubtotalAmount => Items.Sum(i => i.TotalPrice);
    public decimal DiscountAmount { get; set; }
    public decimal ShippingFee => SubtotalAmount >= 100000m || SubtotalAmount == 0 ? 0m : 15000m;
    public decimal TotalAmount => Math.Max(0m, SubtotalAmount - DiscountAmount + ShippingFee);

    public string? AppliedVoucherCode { get; set; }
    public string? VoucherMessage { get; set; }
    public bool VoucherSuccess { get; set; }

    public bool CanCheckout => Items.Any() && !Items.Any(i => i.HasStockShortage || i.IsPrescription);
}

public class CheckoutFormViewModel
{
    [Required(ErrorMessage = "Vui lòng nhập họ tên người nhận")]
    [Display(Name = "Họ tên người nhận")]
    [MaxLength(100, ErrorMessage = "Họ tên không vượt quá 100 ký tự")]
    public string RecipientName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
    [Display(Name = "Số điện thoại")]
    [RegularExpression(@"^0[35789]\d{8}$", ErrorMessage = "Số điện thoại không đúng định dạng di động Việt Nam (10 chữ số)")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập địa chỉ giao hàng")]
    [Display(Name = "Địa chỉ nhận hàng")]
    [MaxLength(255, ErrorMessage = "Địa chỉ không vượt quá 255 ký tự")]
    public string Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng chọn phương thức thanh toán")]
    [Display(Name = "Phương thức thanh toán")]
    public string PaymentMethod { get; set; } = "COD"; // COD or BANK_TRANSFER

    [Display(Name = "Ghi chú giao hàng")]
    [MaxLength(500, ErrorMessage = "Ghi chú không vượt quá 500 ký tự")]
    public string? Note { get; set; }

    public string? VoucherCode { get; set; }

    [Required]
    public string IdempotencyKey { get; set; } = Guid.NewGuid().ToString("N");

    // Summary for display
    public CartViewModel Cart { get; set; } = new();
    public BankTransferOptions? BankTransferInfo { get; set; }
}

public class OrderResultViewModel
{
    public int OrderId { get; set; }
    public string OrderCode { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerPhone { get; set; } = string.Empty;
    public string ShippingAddress { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public string PaymentMethod { get; set; } = "COD";
    public string OrderStatus { get; set; } = "NEW";
    public string PaymentStatus { get; set; } = "UNPAID";
    public DateTime OrderDate { get; set; }
    public List<OrderItemViewModel> Items { get; set; } = new();
    public BankTransferOptions? BankTransferInfo { get; set; }
}

public class OrderItemViewModel
{
    public string MedicineName { get; set; } = string.Empty;
    public string Unit { get; set; } = "Hộp";
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
}
