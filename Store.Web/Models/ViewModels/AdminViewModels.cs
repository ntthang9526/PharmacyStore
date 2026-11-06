using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace PharmacyWeb.Models.ViewModels;

public class CreateCustomerWebAccountViewModel
{
    [Range(1, int.MaxValue)]
    public int CustomerId { get; set; }
    [Required, EmailAddress, MaxLength(100)]
    public string Email { get; set; } = string.Empty;
    [Required, MinLength(8), MaxLength(100)]
    public string InitialPassword { get; set; } = string.Empty;
}

public class AdminDashboardViewModel
{
    public int NewOrdersCount { get; set; }
    public decimal PaidRevenueToday { get; set; }
    public decimal PaidRevenueMonth { get; set; }
    public int TotalAvailableProducts { get; set; }
    public int LowStockCount { get; set; }
    public int ExpiringSoonCount { get; set; }
    public int PendingPrescriptionsCount { get; set; }
    public List<OrderRecentItemViewModel> RecentOrders { get; set; } = new();
    public List<BatchAlertViewModel> BatchAlerts { get; set; } = new();
}

public class OrderRecentItemViewModel
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string OrderStatus { get; set; } = "NEW";
    public string PaymentStatus { get; set; } = "UNPAID";
}

public class BatchAlertViewModel
{
    public int BatchId { get; set; }
    public string BatchNumber { get; set; } = string.Empty;
    public string MedicineName { get; set; } = string.Empty;
    public DateTime ExpiryDate { get; set; }
    public int Stock { get; set; }
    public string AlertType { get; set; } = "EXPIRING_SOON"; // EXPIRING_SOON, EXPIRED, LOW_STOCK, BLOCKED
}

public class AdminProductFormViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn danh mục thuốc")]
    [Display(Name = "Danh mục thuốc")]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn nhà cung cấp")]
    [Display(Name = "Nhà cung cấp")]
    public int SupplierId { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập mã vạch")]
    [MaxLength(50)]
    [Display(Name = "Mã vạch")]
    public string BarCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập tên thuốc")]
    [MaxLength(150)]
    [Display(Name = "Tên thuốc / Sản phẩm")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(150)]
    [Display(Name = "Hoạt chất chính")]
    public string ActiveIngredient { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập đơn vị tính")]
    [MaxLength(30)]
    [Display(Name = "Đơn vị tính (Hộp, Chai, Tuýp,...)")]
    public string Unit { get; set; } = "Hộp";

    [MaxLength(150)]
    [Display(Name = "Quy cách đóng gói")]
    public string PackagingSpec { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập đơn giá")]
    [Range(100, 100000000, ErrorMessage = "Giá phải từ 100 ₫ đến 100.000.000 ₫")]
    [Display(Name = "Giá bán niêm yết (₫)")]
    public decimal Price { get; set; }

    [Display(Name = "Thuốc cần đơn bác sĩ")]
    public bool IsPrescription { get; set; }

    [Display(Name = "Cho phép bán hàng (Hệ thống Web & POS)")]
    public bool IsSelling { get; set; } = true;

    [Display(Name = "Kích hoạt hoạt động (Hệ thống chung: Web & POS)")]
    public bool IsActive { get; set; } = true;

    [Display(Name = "Sản phẩm nổi bật")]
    public bool Featured { get; set; }

    [Display(Name = "Mô tả chi tiết")]
    public string? DescriptionHtml { get; set; }

    [Display(Name = "Ảnh đại diện chính")]
    public IFormFile? MainImageUpload { get; set; }
    public string? ExistingMainImageUrl { get; set; }

    [Display(Name = "Thêm ảnh phụ")]
    public List<IFormFile>? AdditionalImages { get; set; }
    public List<string> ExistingImages { get; set; } = new();

    public byte[]? RowVersion { get; set; }
}

public class StockInFormViewModel
{
    [Required(ErrorMessage = "Vui lòng chọn nhà cung cấp")]
    public int SupplierId { get; set; }

    [MaxLength(500)]
    public string? Note { get; set; }

    public List<StockInLineViewModel> Lines { get; set; } = new();
}

public class StockInLineViewModel
{
    public int MedicineId { get; set; }
    public string MedicineName { get; set; } = string.Empty;
    public string BatchNumber { get; set; } = string.Empty;
    public DateTime ExpiryDate { get; set; }
    public int Quantity { get; set; }
    public decimal UnitCost { get; set; }
    public decimal TotalCost => Quantity * UnitCost;
}

public class VoucherFormViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập mã voucher")]
    [MaxLength(50)]
    public string Code { get; set; } = string.Empty;

    [Required]
    public string DiscountType { get; set; } = "FIXED"; // FIXED or PERCENT

    [Required(ErrorMessage = "Vui lòng nhập giá trị giảm")]
    [Range(1, 10000000, ErrorMessage = "Giá trị giảm không hợp lệ")]
    public decimal DiscountValue { get; set; }

    [Range(0, 100000000, ErrorMessage = "Đơn tối thiểu không hợp lệ")]
    public decimal MinOrderValue { get; set; }

    public decimal? MaxDiscountCap { get; set; }

    [Range(0, 100000, ErrorMessage = "Số lượng không hợp lệ")]
    public int Quantity { get; set; }

    public bool IsActive { get; set; } = true;

    [MaxLength(255)]
    public string? Description { get; set; }
}

public class RevenueReportViewModel
{
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public int TotalPaidOrders { get; set; }
    public int TotalRefundedOrders { get; set; }
    public decimal TotalCollectedRevenue { get; set; }
    public decimal TotalRefundedAmount { get; set; }
    public decimal NetRevenue => TotalCollectedRevenue - TotalRefundedAmount;
    public List<RevenueOrderItemViewModel> Orders { get; set; } = new();
}

public class RevenueOrderItemViewModel
{
    public int OrderId { get; set; }
    public string OrderCode { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
    public DateTime? PaidAt { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = "COD";
    public decimal TotalAmount { get; set; }
    public string PaidBy { get; set; } = string.Empty;
    public string OrderStatus { get; set; } = string.Empty;
    public string PaymentStatus { get; set; } = string.Empty;
    public bool IsRefunded { get; set; }
    public decimal RefundAmount { get; set; }
    public DateTime? RefundedAt { get; set; }
    public string? RefundedBy { get; set; }
    public string? RefundReason { get; set; }
    public decimal CollectedInPeriod { get; set; }
    public decimal RefundedInPeriod { get; set; }
    public decimal NetAmount => CollectedInPeriod - RefundedInPeriod;
}
