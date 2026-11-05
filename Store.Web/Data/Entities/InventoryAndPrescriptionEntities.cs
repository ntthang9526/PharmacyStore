using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyWeb.Data.Entities;

public class StockIn
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(30)]
    public string Code { get; set; } = string.Empty;

    public int SupplierId { get; set; }
    public Supplier? Supplier { get; set; }

    [Required]
    [MaxLength(100)]
    public string EmployeeName { get; set; } = "Quản lý hệ thống (admin)";

    public DateTime ImportDate { get; set; } = DateTime.UtcNow;

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalCost { get; set; }

    [MaxLength(500)]
    public string? Note { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<StockInDetail> Details { get; set; } = new List<StockInDetail>();
}

public class StockInDetail
{
    [Key]
    public int Id { get; set; }

    public int StockInId { get; set; }
    public StockIn? StockIn { get; set; }

    public int MedicineId { get; set; }
    public Medicine? Medicine { get; set; }

    [Required]
    [MaxLength(50)]
    public string BatchNumber { get; set; } = string.Empty;

    public DateTime ExpiryDate { get; set; }
    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitCost { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalCost { get; set; }
}

public class PrescriptionRequest
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
    public string PatientName { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string Phone { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    [MaxLength(30)]
    public string Status { get; set; } = "PENDING_REVIEW"; // PENDING_REVIEW, NEED_MORE_INFO, RESPONDED

    [MaxLength(1000)]
    public string? CustomerNote { get; set; }

    [MaxLength(1000)]
    public string? PharmacistNote { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public ICollection<PrescriptionImage> Images { get; set; } = new List<PrescriptionImage>();
}

public class PrescriptionImage
{
    [Key]
    public int Id { get; set; }

    public int PrescriptionRequestId { get; set; }
    public PrescriptionRequest? PrescriptionRequest { get; set; }

    [Required]
    [MaxLength(255)]
    public string FileName { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public string StoredPath { get; set; } = string.Empty;

    public long FileSizeBytes { get; set; }

    [MaxLength(100)]
    public string ContentType { get; set; } = "image/jpeg";
}
