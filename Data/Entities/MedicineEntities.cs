using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyWeb.Data.Entities;

public class Medicine
{
    [Key]
    public int Id { get; set; }

    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    public int SupplierId { get; set; }
    public Supplier? Supplier { get; set; }

    [Required]
    [MaxLength(50)]
    public string BarCode { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    public string Slug { get; set; } = string.Empty;

    [MaxLength(200)]
    public string ActiveIngredient { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Unit { get; set; } = "Hộp";

    [MaxLength(150)]
    public string PackagingSpec { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    public bool IsPrescription { get; set; }
    public bool IsSelling { get; set; } = true;
    public bool IsActive { get; set; } = true;
    public bool Featured { get; set; }

    public string? DescriptionHtml { get; set; }
    public string? MainImageUrl { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Timestamp]
    public byte[]? RowVersion { get; set; }

    public ICollection<MedicineImage> Images { get; set; } = new List<MedicineImage>();
    public ICollection<Batch> Batches { get; set; } = new List<Batch>();
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public ICollection<StockInDetail> StockInDetails { get; set; } = new List<StockInDetail>();

    [NotMapped]
    public int TotalStock => Batches.Sum(b => b.Stock);

    [NotMapped]
    public int AvailableStock => (!IsActive || !IsSelling) ? 0 :
        Batches.Where(b => !b.IsBlocked && b.ExpiryDate > DateTime.UtcNow.Date)
               .Sum(b => Math.Max(0, b.Stock - b.ReservedQuantity));
}

public class MedicineImage
{
    [Key]
    public int Id { get; set; }

    public int MedicineId { get; set; }
    public Medicine? Medicine { get; set; }

    [Required]
    [MaxLength(255)]
    public string ImageUrl { get; set; } = string.Empty;

    public bool IsPrimary { get; set; }
    public int DisplayOrder { get; set; }
}

public class Batch
{
    [Key]
    public int Id { get; set; }

    public int MedicineId { get; set; }
    public Medicine? Medicine { get; set; }

    [Required]
    [MaxLength(50)]
    public string BatchNumber { get; set; } = string.Empty;

    public DateTime ExpiryDate { get; set; }

    public int Stock { get; set; }
    public int ReservedQuantity { get; set; }
    public bool IsBlocked { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [NotMapped]
    public int AvailableStock => (IsBlocked || ExpiryDate <= DateTime.UtcNow.Date) ? 0 : Math.Max(0, Stock - ReservedQuantity);

    public ICollection<OrderBatchAllocation> Allocations { get; set; } = new List<OrderBatchAllocation>();
}
