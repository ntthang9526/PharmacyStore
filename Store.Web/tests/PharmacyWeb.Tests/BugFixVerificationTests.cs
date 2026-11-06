using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PharmacyWeb.Data;
using PharmacyWeb.Data.Entities;
using PharmacyWeb.Models.ViewModels;
using PharmacyWeb.Services;
using Xunit;

namespace PharmacyWeb.Tests;

public class BugFixVerificationTests : IDisposable
{
    private readonly SqliteConnection _connection;
    private readonly PharmacyWebDbContext _db;
    private readonly HtmlSanitizerService _sanitizer = new();

    public BugFixVerificationTests()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<PharmacyWebDbContext>()
            .UseSqlite(_connection)
            .Options;

        _db = new PharmacyWebDbContext(options);
        _db.Database.EnsureCreated();

        // Seed basic category, supplier, customer, and medicine for foreign key constraints
        _db.Categories.Add(new Category { Id = 1, Name = "Thuốc giảm đau", Slug = "nhom-giam-dau", IsActive = true });
        _db.Suppliers.Add(new Supplier { Id = 1, Name = "Dược Hậu Giang", Phone = "02923891433", IsActive = true });
        _db.CustomerProfiles.Add(new CustomerProfile { Id = 1, FullName = "Nguyễn Văn A", Phone = "0912345678", Email = "a@example.com" });
        _db.Medicines.Add(new Medicine { Id = 1, CategoryId = 1, SupplierId = 1, Name = "Med Base", Slug = "med-base", Unit = "Hộp", Price = 10000m, IsActive = true, IsSelling = true });
        _db.SaveChanges();
    }

    public void Dispose()
    {
        _db.Dispose();
        _connection.Dispose();
    }

    [Fact]
    public void WEB05_HtmlSanitizer_ShouldNeutralizeUnquotedJavascriptUri_AndDangerousSchemes()
    {
        // 1. Unquoted javascript URI
        var unquotedInput = "<a href=javascript:alert(1)>click me</a>";
        var cleanUnquoted = _sanitizer.Sanitize(unquotedInput);
        Assert.DoesNotContain("javascript:", cleanUnquoted, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("href=\"#\"", cleanUnquoted);

        // 2. HTML-entity encoded javascript URI
        var encodedInput = "<a href=\"jav&#97;script:alert(2)\">click me</a>";
        var cleanEncoded = _sanitizer.Sanitize(encodedInput);
        Assert.DoesNotContain("javascript:", cleanEncoded, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("href=\"#\"", cleanEncoded);

        // 3. Vbscript and unquoted event handler
        var vbscriptInput = "<a href='vbscript:msgbox(1)'>test</a><img src=x onerror=alert(3)>";
        var cleanVbscript = _sanitizer.Sanitize(vbscriptInput);
        Assert.DoesNotContain("vbscript:", cleanVbscript, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("onerror", cleanVbscript, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task WEB02_RevenueReport_ShouldIncludeTransactionsThroughoutEndOfDay()
    {
        var reportService = new ReportService(_db);

        // Order paid at 12:00 Vietnam time on 2026-11-05 (05:00 UTC)
        var orderDate = new DateTime(2026, 11, 5, 4, 0, 0, DateTimeKind.Utc);
        var paidAtUtc = new DateTime(2026, 11, 5, 5, 0, 0, DateTimeKind.Utc);

        _db.Orders.Add(new Order
        {
            Id = 101,
            Code = "HD261105001",
            CustomerId = 1,
            CustomerName = "Nguyễn Văn A",
            CustomerPhone = "0912345678",
            ShippingAddress = "Cần Thơ",
            PaymentMethod = "COD",
            PaymentStatus = "PAID",
            OrderStatus = "COMPLETED",
            OrderDate = orderDate,
            PaidAt = paidAtUtc,
            TotalAmount = 150000m
        });
        await _db.SaveChangesAsync();

        // Query with fromDate = 2026-11-05, toDate = 2026-11-05
        var report = await reportService.GetRevenueReportAsync(new DateTime(2026, 11, 5), new DateTime(2026, 11, 5));

        Assert.Equal(1, report.TotalPaidOrders);
        Assert.Equal(150000m, report.TotalCollectedRevenue);
        Assert.Single(report.Orders);
    }

    [Fact]
    public async Task WEB03_ProductCatalog_StorefrontShouldHideInactive_AdminShouldAccessAll()
    {
        var productService = new ProductService(_db, _sanitizer, null!);

        var activeMed = new Medicine
        {
            Id = 10,
            CategoryId = 1,
            SupplierId = 1,
            Name = "Paracetamol 500mg",
            Slug = "paracetamol-500mg-test",
            BarCode = "8931111111",
            Unit = "Hộp",
            Price = 30000m,
            IsActive = true,
            IsSelling = true
        };

        var inactiveMed = new Medicine
        {
            Id = 20,
            CategoryId = 1,
            SupplierId = 1,
            Name = "Thuốc Ngừng Kinh Doanh",
            Slug = "thuoc-ngung-kd-test",
            BarCode = "8932222222",
            Unit = "Hộp",
            Price = 45000m,
            IsActive = false,
            IsSelling = false
        };

        _db.Medicines.AddRange(activeMed, inactiveMed);
        await _db.SaveChangesAsync();

        // Storefront catalog: only active
        var publicCatalog = await productService.GetCatalogAsync(new ProductFilterViewModel());
        Assert.Contains(publicCatalog.Items, m => m.Id == 10);
        Assert.DoesNotContain(publicCatalog.Items, m => m.Id == 20);

        // Admin catalog: sees both active and inactive
        var adminCatalog = await productService.GetAdminCatalogAsync(new ProductFilterViewModel());
        Assert.Contains(adminCatalog.Items, m => m.Id == 10);
        Assert.Contains(adminCatalog.Items, m => m.Id == 20);

        // Public detail by slug and ID: inactive returns null
        var publicDetailSlug = await productService.GetDetailBySlugAsync("thuoc-ngung-kd-test");
        Assert.Null(publicDetailSlug);

        var publicDetailId = await productService.GetDetailByIdAsync(20);
        Assert.Null(publicDetailId);

        // Active medicine detail succeeds
        var activeDetail = await productService.GetDetailByIdAsync(10);
        Assert.NotNull(activeDetail);
        Assert.Equal("Paracetamol 500mg", activeDetail.Name);
    }

    [Fact]
    public async Task WEB04_ProductService_UpdateProduct_ShouldThrowConcurrency_OnStaleRowVersion()
    {
        var productService = new ProductService(_db, _sanitizer, null!);

        var originalRowVersion = new byte[] { 1, 2, 3, 4 };
        var med = new Medicine
        {
            Id = 30,
            CategoryId = 1,
            SupplierId = 1,
            Name = "Amoxicillin 500mg",
            Slug = "amoxicillin-500mg-test",
            BarCode = "8930000001",
            Unit = "Vỉ",
            Price = 45000m,
            IsActive = true,
            IsSelling = true,
            RowVersion = originalRowVersion
        };
        _db.Medicines.Add(med);
        await _db.SaveChangesAsync();

        // Stale form submission with outdated RowVersion
        var staleForm = new AdminProductFormViewModel
        {
            Id = 30,
            CategoryId = 1,
            SupplierId = 1,
            Name = "Amoxicillin 500mg (Updated)",
            BarCode = "8930000001",
            Unit = "Vỉ",
            Price = 45000m,
            RowVersion = new byte[] { 9, 9, 9, 9 } // Mismatched RowVersion
        };

        await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () =>
        {
            await productService.UpdateProductAsync(staleForm);
        });
    }

    [Fact]
    public async Task WEB08_OrderService_BankTransfer_ShouldConfirmPaymentWithoutPrematureCompletion()
    {
        var cartService = new CartService(new TestHttpContextAccessor(), _db);
        var orderService = new OrderService(_db, cartService);

        var batch = new Batch
        {
            Id = 40,
            MedicineId = 1,
            BatchNumber = "LOT-TEST-BT",
            ExpiryDate = DateTime.UtcNow.AddYears(1),
            Stock = 50,
            ReservedQuantity = 2,
            IsBlocked = false
        };
        _db.Batches.Add(batch);

        var order = new Order
        {
            Id = 50,
            Code = "HD261106BT",
            CustomerId = 1,
            CustomerName = "Trần Thị B",
            CustomerPhone = "0987654321",
            ShippingAddress = "Ninh Kiều, Cần Thơ",
            PaymentMethod = "BANK_TRANSFER",
            PaymentStatus = "PENDING_TRANSFER",
            OrderStatus = "CONFIRMED",
            ShippingStatus = "NOT_SHIPPED",
            OrderDate = DateTime.UtcNow,
            TotalAmount = 200000m,
            Allocations = new List<OrderBatchAllocation>
            {
                new() { BatchId = 40, Quantity = 2, IsDeducted = false }
            }
        };
        _db.Orders.Add(order);
        await _db.SaveChangesAsync();

        // Admin confirms receipt of bank transfer
        var res = await orderService.ConfirmCodPaymentAsync(50, "admin");

        Assert.True(res.Success);

        var updatedOrder = await _db.Orders.FindAsync(50);
        Assert.NotNull(updatedOrder);
        Assert.Equal("PAID", updatedOrder.PaymentStatus);
        // Order remains in CONFIRMED and NOT_SHIPPED (not prematurely completed or delivered!)
        Assert.Equal("CONFIRMED", updatedOrder.OrderStatus);
        Assert.Equal("NOT_SHIPPED", updatedOrder.ShippingStatus);

        // Stock allocations remain reserved, NOT yet deducted
        var alloc = await _db.OrderBatchAllocations.FirstOrDefaultAsync(a => a.OrderId == 50);
        Assert.NotNull(alloc);
        Assert.False(alloc.IsDeducted);
        Assert.Equal(50, batch.Stock);
        Assert.Equal(2, batch.ReservedQuantity);
    }

    private class TestHttpContextAccessor : Microsoft.AspNetCore.Http.IHttpContextAccessor
    {
        public Microsoft.AspNetCore.Http.HttpContext? HttpContext { get; set; } = new Microsoft.AspNetCore.Http.DefaultHttpContext();
    }
}
