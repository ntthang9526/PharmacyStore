using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyWeb.Data.Entities;

public class Voucher
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Code { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string DiscountType { get; set; } = "FIXED"; // FIXED or PERCENT

    [Column(TypeName = "decimal(18,2)")]
    public decimal DiscountValue { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal MinOrderValue { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? MaxDiscountCap { get; set; }

    public int Quantity { get; set; }
    public bool IsActive { get; set; } = true;

    [MaxLength(255)]
    public string? Description { get; set; }

    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

public class Order
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(30)]
    public string Code { get; set; } = string.Empty;

    public int CustomerId { get; set; }
    public CustomerProfile? Customer { get; set; }

    [Required]
    [MaxLength(100)]
    public string CustomerName { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string CustomerPhone { get; set; } = string.Empty;

    [Required]
    [MaxLength(255)]
    public string ShippingAddress { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string Channel { get; set; } = "WEB";

    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    [Required]
    [MaxLength(30)]
    public string OrderStatus { get; set; } = "NEW"; // NEW, CONFIRMED, SHIPPING, COMPLETED, CANCELLED

    [Required]
    [MaxLength(30)]
    public string PaymentMethod { get; set; } = "COD"; // COD, BANK_TRANSFER

    [Required]
    [MaxLength(30)]
    public string PaymentStatus { get; set; } = "UNPAID"; // UNPAID, PAID, PENDING_TRANSFER

    [Required]
    [MaxLength(30)]
    public string ShippingStatus { get; set; } = "NOT_SHIPPED"; // NOT_SHIPPED, SHIPPING, DELIVERED, CANCELLED

    [MaxLength(50)]
    public string? VoucherCode { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal SubtotalAmount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal DiscountAmount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal ShippingFee { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }

    [ConcurrencyCheck]
    public int OrderVersion { get; set; } = 1;

    [MaxLength(500)]
    public string? Note { get; set; }

    [MaxLength(500)]
    public string? CancelReason { get; set; }

    public DateTime? PaidAt { get; set; }
    [MaxLength(100)]
    public string? PaidByEmployee { get; set; }

    public bool IsRefunded { get; set; } = false;

    [Column(TypeName = "decimal(18,2)")]
    public decimal RefundAmount { get; set; } = 0m;

    public DateTime? RefundedAt { get; set; }

    [MaxLength(100)]
    public string? RefundedByEmployee { get; set; }

    [MaxLength(500)]
    public string? RefundReason { get; set; }

    public DateTime? ConfirmedAt { get; set; }
    public DateTime? ShippedAt { get; set; }
    public DateTime? CompletedAt { get; set; }

    [MaxLength(100)]
    public string? IdempotencyKey { get; set; }

    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    public ICollection<OrderBatchAllocation> Allocations { get; set; } = new List<OrderBatchAllocation>();
    public ICollection<OrderStatusHistory> History { get; set; } = new List<OrderStatusHistory>();
}

public class OrderItem
{
    [Key]
    public int Id { get; set; }

    public int OrderId { get; set; }
    public Order? Order { get; set; }

    public int MedicineId { get; set; }
    public Medicine? Medicine { get; set; }

    [Required]
    [MaxLength(150)]
    public string MedicineName { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Unit { get; set; } = "Hộp";

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitPrice { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalPrice { get; set; }

    public bool IsPrescriptionSnapshot { get; set; }
}

public class OrderBatchAllocation
{
    [Key]
    public int Id { get; set; }

    public int OrderId { get; set; }
    public Order? Order { get; set; }

    public int BatchId { get; set; }
    public Batch? Batch { get; set; }

    public int Quantity { get; set; }
    public bool IsDeducted { get; set; } // true when order is COMPLETED / shipped
}

public class OrderStatusHistory
{
    [Key]
    public int Id { get; set; }

    public int OrderId { get; set; }
    public Order? Order { get; set; }

    public DateTime Time { get; set; } = DateTime.UtcNow;

    [Required]
    [MaxLength(100)]
    public string Actor { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public string Text { get; set; } = string.Empty;
}
