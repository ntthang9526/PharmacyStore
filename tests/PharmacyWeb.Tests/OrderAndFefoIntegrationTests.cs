using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PharmacyWeb.Data;
using PharmacyWeb.Data.Entities;
using PharmacyWeb.Models.ViewModels;
using PharmacyWeb.Services;
using Xunit;

namespace PharmacyWeb.Tests;

public class OrderAndFefoIntegrationTests : IDisposable
{
    private readonly SqliteConnection _connection;
    private readonly PharmacyWebDbContext _db;
    private readonly FakeCartService _fakeCartService;
    private readonly OrderService _orderService;

    public OrderAndFefoIntegrationTests()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<PharmacyWebDbContext>()
            .UseSqlite(_connection)
            .Options;

        _db = new PharmacyWebDbContext(options);
        _db.Database.EnsureCreated();

        _fakeCartService = new FakeCartService();
        _orderService = new OrderService(_db, _fakeCartService);

        SeedDatabase();
    }

    private void SeedDatabase()
    {
        var category = new Category { Id = 1, Name = "Giảm đau", Slug = "giam-dau" };
        var supplier = new Supplier { Id = 1, Name = "DHG", Phone = "02923" };
        _db.Categories.Add(category);
        _db.Suppliers.Add(supplier);

        // Product with 2 batches: Batch 1 expires in 20 days (stock 10), Batch 2 expires in 200 days (stock 50)
        var med = new Medicine
        {
            Id = 1,
            CategoryId = 1,
            SupplierId = 1,
            BarCode = "8935001",
            Name = "Panadol",
            Slug = "panadol",
            Unit = "Hộp",
            Price = 40000m,
            IsSelling = true,
            IsActive = true
        };
        _db.Medicines.Add(med);

        var today = DateTime.UtcNow.Date;
        var batch1 = new Batch
        {
            Id = 1,
            MedicineId = 1,
            BatchNumber = "LOT-EARLY",
            ExpiryDate = today.AddDays(20), // Earlier expiry -> FEFO must pick this first!
            Stock = 10,
            ReservedQuantity = 0,
            IsBlocked = false
        };

        var batch2 = new Batch
        {
            Id = 2,
            MedicineId = 1,
            BatchNumber = "LOT-LATER",
            ExpiryDate = today.AddDays(200),
            Stock = 50,
            ReservedQuantity = 0,
            IsBlocked = false
        };

        _db.Batches.AddRange(batch1, batch2);

        var customer = new CustomerProfile
        {
            Id = 1,
            FullName = "Nguyễn Văn Test",
            Phone = "0912345678",
            Email = "test@example.com",
            WebAccountActive = true,
            IsLocked = false
        };
        _db.CustomerProfiles.Add(customer);

        var voucher = new Voucher
        {
            Id = 1,
            Code = "TEST10K",
            DiscountType = "FIXED",
            DiscountValue = 10000m,
            MinOrderValue = 50000m,
            Quantity = 5,
            IsActive = true
        };
        _db.Vouchers.Add(voucher);

        _db.SaveChanges();
    }

    [Fact]
    public async Task Checkout_ShouldReserveBatchUsingFefoOrder()
    {
        // Customer buys 15 items: FEFO should take 10 from LOT-EARLY, and 5 from LOT-LATER
        _fakeCartService.Cart = new CartViewModel
        {
            Items = new List<CartItemViewModel>
            {
                new()
                {
                    MedicineId = 1,
                    MedicineName = "Panadol",
                    Unit = "Hộp",
                    Price = 40000m,
                    Quantity = 15,
                    AvailableStock = 60,
                    IsSelling = true,
                    IsActive = true
                }
            }
        };

        var model = new CheckoutFormViewModel
        {
            RecipientName = "Nguyễn Văn Test",
            Phone = "0912345678",
            Address = "123 Cần Thơ",
            PaymentMethod = "COD",
            IdempotencyKey = "unique-key-1"
        };

        var result = await _orderService.CheckoutAsync(1, model);

        Assert.True(result.Success);
        Assert.NotNull(result.OrderId);

        // Verify Batches Reserved Quantities
        var b1 = await _db.Batches.FindAsync(1);
        var b2 = await _db.Batches.FindAsync(2);

        Assert.Equal(10, b1!.ReservedQuantity); // 10 taken from earliest batch
        Assert.Equal(5, b2!.ReservedQuantity);  // 5 taken from second batch
        Assert.Equal(0, b1.AvailableStock);    // 10 - 10 = 0
        Assert.Equal(45, b2.AvailableStock);   // 50 - 5 = 45
    }

    [Fact]
    public async Task Checkout_WithIdempotencyKey_ShouldNotDuplicateOrderOrReservedStock()
    {
        _fakeCartService.Cart = new CartViewModel
        {
            Items = new List<CartItemViewModel>
            {
                new()
                {
                    MedicineId = 1,
                    MedicineName = "Panadol",
                    Unit = "Hộp",
                    Price = 40000m,
                    Quantity = 5,
                    AvailableStock = 60,
                    IsSelling = true,
                    IsActive = true
                }
            }
        };

        var model = new CheckoutFormViewModel
        {
            RecipientName = "Nguyễn Văn Test",
            Phone = "0912345678",
            Address = "123 Cần Thơ",
            PaymentMethod = "COD",
            IdempotencyKey = "same-idempotency-key"
        };

        // First attempt
        var res1 = await _orderService.CheckoutAsync(1, model);
        Assert.True(res1.Success);

        // Second attempt with exact same key
        var res2 = await _orderService.CheckoutAsync(1, model);
        Assert.True(res2.Success);
        Assert.Equal(res1.OrderId, res2.OrderId); // Returns existing order!

        // Total orders in DB should be 1
        var orderCount = await _db.Orders.CountAsync();
        Assert.Equal(1, orderCount);

        // Reserved stock should only be 5 (not 10)
        var b1 = await _db.Batches.FindAsync(1);
        Assert.Equal(5, b1!.ReservedQuantity);
    }

    [Fact]
    public async Task CancelOrder_ShouldReleaseReservedStockAndReturnVoucher()
    {
        _fakeCartService.Cart = new CartViewModel
        {
            Items = new List<CartItemViewModel>
            {
                new()
                {
                    MedicineId = 1,
                    MedicineName = "Panadol",
                    Unit = "Hộp",
                    Price = 40000m,
                    Quantity = 4,
                    AvailableStock = 60,
                    IsSelling = true,
                    IsActive = true
                }
            }
        };

        var model = new CheckoutFormViewModel
        {
            RecipientName = "Nguyễn Văn Test",
            Phone = "0912345678",
            Address = "123 Cần Thơ",
            PaymentMethod = "COD",
            VoucherCode = "TEST10K",
            IdempotencyKey = "key-cancel-test"
        };

        var checkoutRes = await _orderService.CheckoutAsync(1, model);
        Assert.True(checkoutRes.Success);

        var voucherBefore = await _db.Vouchers.FindAsync(1);
        Assert.Equal(4, voucherBefore!.Quantity); // Decremented from 5 to 4

        // Customer cancels order
        var cancelRes = await _orderService.CustomerCancelOrderAsync(checkoutRes.OrderId!.Value, 1, "Đổi ý");
        Assert.True(cancelRes.Success);

        // Verify reserved quantity is released
        var b1 = await _db.Batches.FindAsync(1);
        Assert.Equal(0, b1!.ReservedQuantity);

        // Verify voucher count is returned
        var voucherAfter = await _db.Vouchers.FindAsync(1);
        Assert.Equal(5, voucherAfter!.Quantity);

        // Verify order status
        var order = await _db.Orders.FindAsync(checkoutRes.OrderId.Value);
        Assert.Equal("CANCELLED", order!.OrderStatus);
    }

    [Fact]
    public async Task ConfirmPayment_ShouldPermanentlyDeductStock()
    {
        _fakeCartService.Cart = new CartViewModel
        {
            Items = new List<CartItemViewModel>
            {
                new()
                {
                    MedicineId = 1,
                    MedicineName = "Panadol",
                    Unit = "Hộp",
                    Price = 40000m,
                    Quantity = 6,
                    AvailableStock = 60,
                    IsSelling = true,
                    IsActive = true
                }
            }
        };

        var model = new CheckoutFormViewModel
        {
            RecipientName = "Nguyễn Văn Test",
            Phone = "0912345678",
            Address = "123 Cần Thơ",
            PaymentMethod = "COD",
            IdempotencyKey = "key-complete-test"
        };

        var checkoutRes = await _orderService.CheckoutAsync(1, model);
        var orderId = checkoutRes.OrderId!.Value;

        // Admin confirms payment
        var payRes = await _orderService.ConfirmCodPaymentAsync(orderId, "admin");
        Assert.True(payRes.Success);

        var order = await _db.Orders.FindAsync(orderId);
        Assert.Equal("COMPLETED", order!.OrderStatus);
        Assert.Equal("PAID", order.PaymentStatus);
        Assert.NotNull(order.PaidAt);
        Assert.Equal("admin", order.PaidByEmployee);

        // Stock permanently deducted: Batch 1 had 10, 6 deducted -> stock 4, reserved 0
        var b1 = await _db.Batches.FindAsync(1);
        Assert.Equal(4, b1!.Stock);
        Assert.Equal(0, b1.ReservedQuantity);
    }

    [Fact]
    public async Task ReportService_GetDashboardMetrics_ShouldExecuteWithoutSQLiteDecimalSumException()
    {
        var reportService = new ReportService(_db);
        var metrics = await reportService.GetDashboardMetricsAsync();

        Assert.NotNull(metrics);
        Assert.True(metrics.PaidRevenueToday >= 0);
        Assert.True(metrics.PaidRevenueMonth >= 0);
    }

    public void Dispose()
    {
        _db.Dispose();
        _connection.Dispose();
    }
}

public class FakeCartService : ICartService
{
    public CartViewModel Cart { get; set; } = new();

    public Task<CartViewModel> GetCartAsync() => Task.FromResult(Cart);
    public Task<bool> AddToCartAsync(int medicineId, int quantity) => Task.FromResult(true);
    public Task<bool> UpdateQuantityAsync(int medicineId, int quantity) => Task.FromResult(true);
    public Task<bool> RemoveFromCartAsync(int medicineId) => Task.FromResult(true);
    public Task ClearCartAsync()
    {
        Cart = new CartViewModel();
        return Task.CompletedTask;
    }
    public Task<(bool Success, string Message, decimal DiscountAmount)> ApplyVoucherAsync(string voucherCode) => Task.FromResult((true, "OK", 0m));
    public void RemoveVoucher() { }
}
