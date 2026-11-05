using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyWeb.Data.Entities;

public class CustomerProfile
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string Phone { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;

    public int Points { get; set; }
    public bool WebAccountActive { get; set; } = true;
    public bool IsLocked { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string? UserId { get; set; }
    public ApplicationUser? User { get; set; }

    public ICollection<CustomerAddress> Addresses { get; set; } = new List<CustomerAddress>();
    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<PrescriptionRequest> PrescriptionRequests { get; set; } = new List<PrescriptionRequest>();
}

public class CustomerAddress
{
    [Key]
    public int Id { get; set; }

    public int CustomerProfileId { get; set; }
    public CustomerProfile? CustomerProfile { get; set; }

    [Required]
    [MaxLength(50)]
    public string Label { get; set; } = "Nhà riêng";

    [Required]
    [MaxLength(100)]
    public string RecipientName { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string Phone { get; set; } = string.Empty;

    [Required]
    [MaxLength(255)]
    public string Address { get; set; } = string.Empty;

    public bool IsDefault { get; set; }
}
