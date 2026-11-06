using System.ComponentModel.DataAnnotations;

namespace PharmacyWeb.Data.Entities;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Slug { get; set; } = string.Empty;

    [MaxLength(255)]
    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;
    public int DisplayOrder { get; set; }

    public ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();
}

public class Supplier
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(15)]
    public string Phone { get; set; } = string.Empty;

    [MaxLength(250)]
    public string Address { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();
    public ICollection<StockIn> StockIns { get; set; } = new List<StockIn>();
}
