using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PharmacyWeb.Data.Entities;

namespace PharmacyWeb.Data;

public class ApplicationRole : IdentityRole
{
    public ApplicationRole() : base() { }
    public ApplicationRole(string roleName) : base(roleName) { }
}

public class PharmacyWebDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public PharmacyWebDbContext(DbContextOptions<PharmacyWebDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();
    public DbSet<Medicine> Medicines => Set<Medicine>();
    public DbSet<MedicineImage> MedicineImages => Set<MedicineImage>();
    public DbSet<Batch> Batches => Set<Batch>();
    public DbSet<Voucher> Vouchers => Set<Voucher>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<OrderBatchAllocation> OrderBatchAllocations => Set<OrderBatchAllocation>();
    public DbSet<OrderStatusHistory> OrderStatusHistories => Set<OrderStatusHistory>();
    public DbSet<StockIn> StockIns => Set<StockIn>();
    public DbSet<StockInDetail> StockInDetails => Set<StockInDetail>();
    public DbSet<PrescriptionRequest> PrescriptionRequests => Set<PrescriptionRequest>();
    public DbSet<PrescriptionImage> PrescriptionImages => Set<PrescriptionImage>();
    public DbSet<CustomerProfile> CustomerProfiles => Set<CustomerProfile>();
    public DbSet<CustomerAddress> CustomerAddresses => Set<CustomerAddress>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Unique indexes
        builder.Entity<Category>().HasIndex(c => c.Slug).IsUnique();
        builder.Entity<Medicine>().HasIndex(m => m.Slug).IsUnique();
        builder.Entity<Medicine>().HasIndex(m => m.BarCode).IsUnique();
        builder.Entity<Voucher>().HasIndex(v => v.Code).IsUnique();
        builder.Entity<Order>().HasIndex(o => o.Code).IsUnique();
        builder.Entity<Order>().HasIndex(o => o.IdempotencyKey);
        builder.Entity<PrescriptionRequest>().HasIndex(p => p.Code).IsUnique();
        builder.Entity<StockIn>().HasIndex(s => s.Code).IsUnique();

        // Foreign keys with Restrict to prevent accidental cascade drops
        builder.Entity<Medicine>()
            .HasOne(m => m.Category)
            .WithMany(c => c.Medicines)
            .HasForeignKey(m => m.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Medicine>()
            .HasOne(m => m.Supplier)
            .WithMany(s => s.Medicines)
            .HasForeignKey(m => m.SupplierId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Batch>()
            .HasOne(b => b.Medicine)
            .WithMany(m => m.Batches)
            .HasForeignKey(b => b.MedicineId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.Items)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<OrderItem>()
            .HasOne(oi => oi.Medicine)
            .WithMany(m => m.OrderItems)
            .HasForeignKey(oi => oi.MedicineId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<OrderBatchAllocation>()
            .HasOne(oba => oba.Order)
            .WithMany(o => o.Allocations)
            .HasForeignKey(oba => oba.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<OrderBatchAllocation>()
            .HasOne(oba => oba.Batch)
            .WithMany(b => b.Allocations)
            .HasForeignKey(oba => oba.BatchId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<StockIn>()
            .HasOne(si => si.Supplier)
            .WithMany(s => s.StockIns)
            .HasForeignKey(si => si.SupplierId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<StockInDetail>()
            .HasOne(sid => sid.StockIn)
            .WithMany(si => si.Details)
            .HasForeignKey(sid => sid.StockInId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<StockInDetail>()
            .HasOne(sid => sid.Medicine)
            .WithMany(m => m.StockInDetails)
            .HasForeignKey(sid => sid.MedicineId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<CustomerProfile>()
            .HasOne(cp => cp.User)
            .WithOne(u => u.CustomerProfile)
            .HasForeignKey<CustomerProfile>(cp => cp.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Entity<CustomerAddress>()
            .HasOne(ca => ca.CustomerProfile)
            .WithMany(cp => cp.Addresses)
            .HasForeignKey(ca => ca.CustomerProfileId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Order>()
            .HasOne(o => o.Customer)
            .WithMany(cp => cp.Orders)
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<PrescriptionRequest>()
            .HasOne(pr => pr.Customer)
            .WithMany(cp => cp.PrescriptionRequests)
            .HasForeignKey(pr => pr.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<PrescriptionImage>()
            .HasOne(pi => pi.PrescriptionRequest)
            .WithMany(pr => pr.Images)
            .HasForeignKey(pi => pi.PrescriptionRequestId)
            .OnDelete(DeleteBehavior.Cascade);

        // Decimal precision configurations
        builder.Entity<Medicine>().Property(m => m.Price).HasPrecision(18, 2);
        builder.Entity<Order>().Property(o => o.SubtotalAmount).HasPrecision(18, 2);
        builder.Entity<Order>().Property(o => o.DiscountAmount).HasPrecision(18, 2);
        builder.Entity<Order>().Property(o => o.ShippingFee).HasPrecision(18, 2);
        builder.Entity<Order>().Property(o => o.TotalAmount).HasPrecision(18, 2);
        builder.Entity<OrderItem>().Property(oi => oi.UnitPrice).HasPrecision(18, 2);
        builder.Entity<OrderItem>().Property(oi => oi.TotalPrice).HasPrecision(18, 2);
        builder.Entity<Voucher>().Property(v => v.DiscountValue).HasPrecision(18, 2);
        builder.Entity<Voucher>().Property(v => v.MinOrderValue).HasPrecision(18, 2);
        builder.Entity<Voucher>().Property(v => v.MaxDiscountCap).HasPrecision(18, 2);
        builder.Entity<StockIn>().Property(si => si.TotalCost).HasPrecision(18, 2);
        builder.Entity<StockInDetail>().Property(sid => sid.UnitCost).HasPrecision(18, 2);
        builder.Entity<StockInDetail>().Property(sid => sid.TotalCost).HasPrecision(18, 2);
    }
}
