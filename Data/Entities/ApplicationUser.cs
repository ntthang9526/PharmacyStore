using Microsoft.AspNetCore.Identity;

namespace PharmacyWeb.Data.Entities;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
    public bool IsLocked { get; set; }
    public int? CustomerProfileId { get; set; }
    public CustomerProfile? CustomerProfile { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
