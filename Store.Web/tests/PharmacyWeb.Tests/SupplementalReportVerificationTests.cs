using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PharmacyWeb.Data;
using PharmacyWeb.Data.Entities;
using PharmacyWeb.Models;
using PharmacyWeb.Models.ViewModels;
using PharmacyWeb.Services;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Xunit;

namespace PharmacyWeb.Tests;

public class SupplementalReportVerificationTests : IDisposable
{
    private readonly SqliteConnection _connection;
    private readonly PharmacyWebDbContext _db;
    private readonly HtmlSanitizerService _sanitizer = new();
    private readonly BankTransferOptions _bankOptions = new()
    {
        IsEnabled = true,
        IsDemoAccount = true,
        BankName = "Vietcombank (Chi nhánh Cần Thơ)",
        AccountNumber = "1029384756",
        AccountHolder = "NHA THUOC PHARMACY STORE",
        TransferInstruction = "Dược sĩ sẽ kiểm tra biến động số dư và xác nhận ngay cho quý khách.",
        DemoNotice = "Thông tin tài khoản ngân hàng thử nghiệm phục vụ đồ án/demo. Quý khách vui lòng không thực hiện giao dịch chuyển tiền thật."
    };

    public SupplementalReportVerificationTests()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<PharmacyWebDbContext>()
            .UseSqlite(_connection)
            .Options;

        _db = new PharmacyWebDbContext(options);
        _db.Database.EnsureCreated();

        SeedBasicData();
    }

    private void SeedBasicData()
    {
        _db.Categories.Add(new Category { Id = 1, Name = "Thuốc giảm đau", Slug = "nhom-giam-dau", IsActive = true });
        _db.Suppliers.Add(new Supplier { Id = 1, Name = "Dược Hậu Giang", Phone = "02923891433", IsActive = true });
        _db.CustomerProfiles.Add(new CustomerProfile { Id = 1, FullName = "Nguyễn Văn A", Phone = "0912345678", Email = "a@example.com", WebAccountActive = true });
        _db.Medicines.Add(new Medicine { Id = 1, CategoryId = 1, SupplierId = 1, Name = "Paracetamol 500mg", Slug = "paracetamol-500", Unit = "Hộp", Price = 25000m, IsActive = true, IsSelling = true });
        _db.SaveChanges();
    }

    public void Dispose()
    {
        _db.Dispose();
        _connection.Dispose();
    }

    // =========================================================================
    // BS-01: Hủy đơn đã thu chuyển khoản nhưng chưa có xử lý hoàn tiền
    // =========================================================================

    [Fact]
    public async Task BS01_DirectCancellation_OnPaidOrder_MustBeBlocked()
    {
        var cartService = new FakeCartService();
        var orderService = new OrderService(_db, cartService, Options.Create(_bankOptions));

        var batch = new Batch
        {
            Id = 10,
            MedicineId = 1,
            BatchNumber = "LOT-BS01-A",
            ExpiryDate = DateTime.UtcNow.AddYears(1),
            Stock = 50,
            ReservedQuantity = 2,
            IsBlocked = false
        };
        _db.Batches.Add(batch);

        var order = new Order
        {
            Id = 101,
            Code = "HD26110601",
            CustomerId = 1,
            CustomerName = "Nguyễn Văn A",
            CustomerPhone = "0912345678",
            ShippingAddress = "Cần Thơ",
            PaymentMethod = "BANK_TRANSFER",
            PaymentStatus = "PAID",
            OrderStatus = "CONFIRMED",
            ShippingStatus = "NOT_SHIPPED",
            OrderDate = DateTime.UtcNow,
            PaidAt = DateTime.UtcNow,
            PaidByEmployee = "admin",
            TotalAmount = 50000m,
            Allocations = new List<OrderBatchAllocation>
            {
                new() { BatchId = 10, Quantity = 2, IsDeducted = false }
            }
        };
        _db.Orders.Add(order);
        await _db.SaveChangesAsync();

        // Admin attempts direct cancel without refund
        var cancelResult = await orderService.UpdateOrderStatusByAdminAsync(101, "CANCELLED", "Hủy trực tiếp", "admin");

        // Must be blocked!
        Assert.False(cancelResult.Success);
        Assert.Contains("hoàn tiền", cancelResult.Message, StringComparison.OrdinalIgnoreCase);

        // Order remains CONFIRMED and PAID
        var reloadedOrder = await _db.Orders.FindAsync(101);
        Assert.Equal("CONFIRMED", reloadedOrder!.OrderStatus);
        Assert.Equal("PAID", reloadedOrder.PaymentStatus);
    }

    [Fact]
    public async Task BS01_UnpaidOrder_CanBeCancelled_AndReleasesBatchReservation()
    {
        var cartService = new FakeCartService();
        var orderService = new OrderService(_db, cartService, Options.Create(_bankOptions));

        var batch = new Batch
        {
            Id = 11,
            MedicineId = 1,
            BatchNumber = "LOT-BS01-B",
            ExpiryDate = DateTime.UtcNow.AddYears(1),
            Stock = 30,
            ReservedQuantity = 3,
            IsBlocked = false
        };
        _db.Batches.Add(batch);

        var order = new Order
        {
            Id = 102,
            Code = "HD26110602",
            CustomerId = 1,
            CustomerName = "Nguyễn Văn A",
            CustomerPhone = "0912345678",
            ShippingAddress = "Cần Thơ",
            PaymentMethod = "BANK_TRANSFER",
            PaymentStatus = "PENDING_TRANSFER",
            OrderStatus = "CONFIRMED",
            ShippingStatus = "NOT_SHIPPED",
            OrderDate = DateTime.UtcNow,
            TotalAmount = 75000m,
            Allocations = new List<OrderBatchAllocation>
            {
                new() { BatchId = 11, Quantity = 3, IsDeducted = false }
            }
        };
        _db.Orders.Add(order);
        await _db.SaveChangesAsync();

        var cancelResult = await orderService.UpdateOrderStatusByAdminAsync(102, "CANCELLED", "Khách đổi ý chưa thanh toán", "admin");

        Assert.True(cancelResult.Success);
        var updatedOrder = await _db.Orders.FindAsync(102);
        Assert.Equal("CANCELLED", updatedOrder!.OrderStatus);

        // Batch reservation released
        var updatedBatch = await _db.Batches.FindAsync(11);
        Assert.Equal(0, updatedBatch!.ReservedQuantity);
    }

    [Fact]
    public async Task BS01_RefundOrder_Succeeds_MarksCancelledAndRefunded_PreservesOriginalHistory()
    {
        var cartService = new FakeCartService();
        var orderService = new OrderService(_db, cartService, Options.Create(_bankOptions));

        var batch = new Batch
        {
            Id = 12,
            MedicineId = 1,
            BatchNumber = "LOT-BS01-C",
            ExpiryDate = DateTime.UtcNow.AddYears(1),
            Stock = 20,
            ReservedQuantity = 2,
            IsBlocked = false
        };
        _db.Batches.Add(batch);

        var originalPaidAt = DateTime.UtcNow.AddHours(-2);
        var order = new Order
        {
            Id = 103,
            Code = "HD26110603",
            CustomerId = 1,
            CustomerName = "Nguyễn Văn A",
            CustomerPhone = "0912345678",
            ShippingAddress = "Cần Thơ",
            PaymentMethod = "BANK_TRANSFER",
            PaymentStatus = "PAID",
            OrderStatus = "CONFIRMED",
            ShippingStatus = "NOT_SHIPPED",
            OrderDate = originalPaidAt.AddMinutes(-10),
            PaidAt = originalPaidAt,
            PaidByEmployee = "cashier1",
            TotalAmount = 50000m,
            Allocations = new List<OrderBatchAllocation>
            {
                new() { BatchId = 12, Quantity = 2, IsDeducted = false }
            }
        };
        _db.Orders.Add(order);
        await _db.SaveChangesAsync();

        // Admin performs refund
        var refundRes = await orderService.RefundOrderAsync(103, "Khách yêu cầu hoàn tiền qua hotline", "admin_lead");

        Assert.True(refundRes.Success);
        Assert.Contains("50,000", refundRes.Message);

        var updatedOrder = await _db.Orders.Include(o => o.History).FirstOrDefaultAsync(o => o.Id == 103);
        Assert.NotNull(updatedOrder);
        Assert.Equal("CANCELLED", updatedOrder.OrderStatus);
        Assert.Equal("REFUNDED", updatedOrder.PaymentStatus);
        Assert.True(updatedOrder.IsRefunded);
        Assert.Equal(50000m, updatedOrder.RefundAmount);
        Assert.NotNull(updatedOrder.RefundedAt);
        Assert.Equal("admin_lead", updatedOrder.RefundedByEmployee);
        Assert.Equal("Khách yêu cầu hoàn tiền qua hotline", updatedOrder.RefundReason);

        // Original payment audit trail preserved!
        Assert.Equal(originalPaidAt, updatedOrder.PaidAt);
        Assert.Equal("cashier1", updatedOrder.PaidByEmployee);

        // Batch reservation released
        var updatedBatch = await _db.Batches.FindAsync(12);
        Assert.Equal(0, updatedBatch!.ReservedQuantity);

        // History trail recorded
        Assert.Contains(updatedOrder.History, h => h.Actor == "admin_lead" && h.Text.Contains("50,000"));
    }

    [Fact]
    public async Task BS01_RefundOrder_CannotRefundTwice()
    {
        var cartService = new FakeCartService();
        var orderService = new OrderService(_db, cartService, Options.Create(_bankOptions));

        var order = new Order
        {
            Id = 104,
            Code = "HD26110604",
            CustomerId = 1,
            CustomerName = "Nguyễn Văn A",
            CustomerPhone = "0912345678",
            ShippingAddress = "Cần Thơ",
            PaymentMethod = "BANK_TRANSFER",
            PaymentStatus = "PAID",
            OrderStatus = "CONFIRMED",
            TotalAmount = 50000m
        };
        _db.Orders.Add(order);
        await _db.SaveChangesAsync();

        // 1st refund
        var firstRefund = await orderService.RefundOrderAsync(104, "Lý do 1", "admin");
        Assert.True(firstRefund.Success);

        // 2nd refund attempt
        var secondRefund = await orderService.RefundOrderAsync(104, "Lý do 2", "admin");
        Assert.False(secondRefund.Success);
        Assert.Contains("không thể hoàn tiền hai lần", secondRefund.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task BS01_RevenueReport_ReconcilesCollected_Refunded_AndNetRevenue()
    {
        var reportService = new ReportService(_db);

        var today = PharmacyClock.Today;
        var paidAtUtc = today.AddHours(3).ToUniversalTime();

        // Order 1: Paid 100,000, not refunded
        _db.Orders.Add(new Order
        {
            Id = 201,
            Code = "HD261106R1",
            CustomerId = 1,
            CustomerName = "Khách A",
            CustomerPhone = "0912345678",
            ShippingAddress = "Cần Thơ",
            PaymentMethod = "COD",
            PaymentStatus = "PAID",
            OrderStatus = "COMPLETED",
            OrderDate = paidAtUtc.AddHours(-1),
            PaidAt = paidAtUtc,
            TotalAmount = 100000m,
            IsRefunded = false
        });

        // Order 2: Paid 200,000, and refunded 200,000
        _db.Orders.Add(new Order
        {
            Id = 202,
            Code = "HD261106R2",
            CustomerId = 1,
            CustomerName = "Khách B",
            CustomerPhone = "0987654321",
            ShippingAddress = "Cần Thơ",
            PaymentMethod = "BANK_TRANSFER",
            PaymentStatus = "REFUNDED",
            OrderStatus = "CANCELLED",
            OrderDate = paidAtUtc.AddHours(-2),
            PaidAt = paidAtUtc,
            IsRefunded = true,
            RefundAmount = 200000m,
            RefundedAt = paidAtUtc.AddMinutes(30),
            RefundedByEmployee = "admin",
            RefundReason = "Hủy đơn",
            TotalAmount = 200000m
        });

        await _db.SaveChangesAsync();

        var report = await reportService.GetRevenueReportAsync(today, today);

        // 2 paid transactions in period
        Assert.Equal(2, report.TotalPaidOrders);
        // 1 refunded transaction in period
        Assert.Equal(1, report.TotalRefundedOrders);
        // Total collected = 100,000 + 200,000 = 300,000
        Assert.Equal(300000m, report.TotalCollectedRevenue);
        // Total refunded = 200,000
        Assert.Equal(200000m, report.TotalRefundedAmount);
        // Net revenue = 300,000 - 200,000 = 100,000
        Assert.Equal(100000m, report.NetRevenue);

        // CSV export verification
        var csv = await reportService.GenerateRevenueCsvAsync(today, today);
        Assert.Contains("HD261106R1", csv);
        Assert.Contains("HD261106R2", csv);
        Assert.Contains("Đã hoàn tiền", csv);
        Assert.Contains("200000", csv);
    }

    // =========================================================================
    // BS-02: Hoàn tất đơn vẫn trừ tồn của lô đang khóa
    // =========================================================================

    [Fact]
    public async Task BS02_CompletingOrder_WithBlockedBatch_MustFailAndNotDeductStock()
    {
        var cartService = new FakeCartService();
        var orderService = new OrderService(_db, cartService, Options.Create(_bankOptions));

        var batch = new Batch
        {
            Id = 30,
            MedicineId = 1,
            BatchNumber = "LOT-BLOCKED-30",
            ExpiryDate = DateTime.UtcNow.AddYears(1),
            Stock = 10,
            ReservedQuantity = 2,
            IsBlocked = true // Blocked batch!
        };
        _db.Batches.Add(batch);

        var order = new Order
        {
            Id = 301,
            Code = "HD261106BLOCKED",
            CustomerId = 1,
            CustomerName = "Nguyễn Văn A",
            CustomerPhone = "0912345678",
            ShippingAddress = "Cần Thơ",
            PaymentMethod = "BANK_TRANSFER",
            PaymentStatus = "PAID",
            OrderStatus = "SHIPPING",
            ShippingStatus = "SHIPPING",
            OrderDate = DateTime.UtcNow,
            PaidAt = DateTime.UtcNow,
            TotalAmount = 50000m,
            Allocations = new List<OrderBatchAllocation>
            {
                new() { BatchId = 30, Quantity = 1, IsDeducted = false }
            }
        };
        _db.Orders.Add(order);
        await _db.SaveChangesAsync();

        // Attempt to complete order
        var result = await orderService.UpdateOrderStatusByAdminAsync(301, "COMPLETED", null, "admin");

        // Must fail!
        Assert.False(result.Success);
        Assert.Contains("đang bị khóa", result.Message);
        Assert.Contains("LOT-BLOCKED-30", result.Message);

        // Stock MUST NOT be changed or deducted!
        var reloadedBatch = await _db.Batches.FindAsync(30);
        Assert.Equal(10, reloadedBatch!.Stock); // Still 10, not 9!
        Assert.Equal(2, reloadedBatch.ReservedQuantity);

        // Order remains in SHIPPING
        var reloadedOrder = await _db.Orders.FindAsync(301);
        Assert.Equal("SHIPPING", reloadedOrder!.OrderStatus);
    }

    [Fact]
    public async Task BS02_CompletingOrder_WithExpiredBatch_MustFailAndNotDeductStock()
    {
        var cartService = new FakeCartService();
        var orderService = new OrderService(_db, cartService, Options.Create(_bankOptions));

        var expiredBatch = new Batch
        {
            Id = 31,
            MedicineId = 1,
            BatchNumber = "LOT-EXPIRED-31",
            ExpiryDate = PharmacyClock.Today.AddDays(-1), // Expired!
            Stock = 15,
            ReservedQuantity = 1,
            IsBlocked = false
        };
        _db.Batches.Add(expiredBatch);

        var order = new Order
        {
            Id = 302,
            Code = "HD261106EXPIRED",
            CustomerId = 1,
            CustomerName = "Nguyễn Văn A",
            CustomerPhone = "0912345678",
            ShippingAddress = "Cần Thơ",
            PaymentMethod = "BANK_TRANSFER",
            PaymentStatus = "PAID",
            OrderStatus = "SHIPPING",
            ShippingStatus = "SHIPPING",
            OrderDate = DateTime.UtcNow,
            TotalAmount = 25000m,
            Allocations = new List<OrderBatchAllocation>
            {
                new() { BatchId = 31, Quantity = 1, IsDeducted = false }
            }
        };
        _db.Orders.Add(order);
        await _db.SaveChangesAsync();

        var result = await orderService.UpdateOrderStatusByAdminAsync(302, "COMPLETED", null, "admin");

        Assert.False(result.Success);
        Assert.Contains("hết hạn", result.Message);
        Assert.Contains("LOT-EXPIRED-31", result.Message);

        var reloadedBatch = await _db.Batches.FindAsync(31);
        Assert.Equal(15, reloadedBatch!.Stock);
    }

    [Fact]
    public async Task BS02_CompletingOrder_MultipleAllocations_OneInvalid_RollsBackAll()
    {
        var cartService = new FakeCartService();
        var orderService = new OrderService(_db, cartService, Options.Create(_bankOptions));

        var validBatch = new Batch
        {
            Id = 32,
            MedicineId = 1,
            BatchNumber = "LOT-VALID-32",
            ExpiryDate = PharmacyClock.Today.AddYears(1),
            Stock = 20,
            ReservedQuantity = 2,
            IsBlocked = false
        };
        var blockedBatch = new Batch
        {
            Id = 33,
            MedicineId = 1,
            BatchNumber = "LOT-BLOCKED-33",
            ExpiryDate = PharmacyClock.Today.AddYears(1),
            Stock = 10,
            ReservedQuantity = 1,
            IsBlocked = true // Blocked!
        };
        _db.Batches.AddRange(validBatch, blockedBatch);

        var order = new Order
        {
            Id = 303,
            Code = "HD261106MULTI",
            CustomerId = 1,
            CustomerName = "Nguyễn Văn A",
            CustomerPhone = "0912345678",
            ShippingAddress = "Cần Thơ",
            PaymentMethod = "BANK_TRANSFER",
            PaymentStatus = "PAID",
            OrderStatus = "SHIPPING",
            ShippingStatus = "SHIPPING",
            OrderDate = DateTime.UtcNow,
            TotalAmount = 75000m,
            Allocations = new List<OrderBatchAllocation>
            {
                new() { BatchId = 32, Quantity = 2, IsDeducted = false },
                new() { BatchId = 33, Quantity = 1, IsDeducted = false }
            }
        };
        _db.Orders.Add(order);
        await _db.SaveChangesAsync();

        var result = await orderService.UpdateOrderStatusByAdminAsync(303, "COMPLETED", null, "admin");

        Assert.False(result.Success);
        Assert.Contains("LOT-BLOCKED-33", result.Message);

        // Neither batch is deducted!
        var b32 = await _db.Batches.FindAsync(32);
        var b33 = await _db.Batches.FindAsync(33);
        Assert.Equal(20, b32!.Stock);
        Assert.Equal(10, b33!.Stock);
        Assert.Equal(2, b32.ReservedQuantity);
        Assert.Equal(1, b33.ReservedQuantity);
    }

    [Fact]
    public async Task BS02_CompletingOrder_ValidBatches_DeductsOnce_AndRejectsRepeatedCompletion()
    {
        var cartService = new FakeCartService();
        var orderService = new OrderService(_db, cartService, Options.Create(_bankOptions));

        var validBatch = new Batch
        {
            Id = 34,
            MedicineId = 1,
            BatchNumber = "LOT-VALID-34",
            ExpiryDate = PharmacyClock.Today.AddYears(1),
            Stock = 20,
            ReservedQuantity = 2,
            IsBlocked = false
        };
        _db.Batches.Add(validBatch);

        var order = new Order
        {
            Id = 304,
            Code = "HD261106OK",
            CustomerId = 1,
            CustomerName = "Nguyễn Văn A",
            CustomerPhone = "0912345678",
            ShippingAddress = "Cần Thơ",
            PaymentMethod = "BANK_TRANSFER",
            PaymentStatus = "PAID",
            OrderStatus = "SHIPPING",
            ShippingStatus = "SHIPPING",
            OrderDate = DateTime.UtcNow,
            TotalAmount = 50000m,
            Allocations = new List<OrderBatchAllocation>
            {
                new() { BatchId = 34, Quantity = 2, IsDeducted = false }
            }
        };
        _db.Orders.Add(order);
        await _db.SaveChangesAsync();

        // 1st completion
        var firstResult = await orderService.UpdateOrderStatusByAdminAsync(304, "COMPLETED", null, "admin");
        Assert.True(firstResult.Success);

        var b34After = await _db.Batches.FindAsync(34);
        Assert.Equal(18, b34After!.Stock);
        Assert.Equal(0, b34After.ReservedQuantity);

        // 2nd completion attempt must fail (cannot transition already completed order)
        var secondResult = await orderService.UpdateOrderStatusByAdminAsync(304, "COMPLETED", null, "admin");
        Assert.False(secondResult.Success);

        // Stock still 18, not deducted twice
        var b34Second = await _db.Batches.FindAsync(34);
        Assert.Equal(18, b34Second!.Stock);
    }

    // =========================================================================
    // BS-03: Bộ lọc HTML còn giữ thuộc tính sự kiện trong cú pháp SVG đặc biệt
    // =========================================================================

    [Fact]
    public void BS03_HtmlSanitizer_ShouldEliminateSvgWithSlashOnload()
    {
        // 1. Target vector reported in BS-03: <svg/onload=alert(1)>
        var svgInput = "<svg/onload=alert(1)>";
        var cleanSvg = _sanitizer.Sanitize(svgInput);
        Assert.DoesNotContain("onload", cleanSvg, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("alert", cleanSvg, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("<svg", cleanSvg, StringComparison.OrdinalIgnoreCase);

        // 2. SVG with attributes and slash onload: <svg id="x"/onload=alert(1)>
        var svgWithAttr = "<svg id=\"x\"/onload=alert(1)>";
        var cleanSvgAttr = _sanitizer.Sanitize(svgWithAttr);
        Assert.DoesNotContain("onload", cleanSvgAttr, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("<svg", cleanSvgAttr, StringComparison.OrdinalIgnoreCase);

        // 3. Nested/surrounding content
        var mixed = "<p>Mô tả thuốc</p><svg/onload=alert(1)><b>Chính hãng</b>";
        var cleanMixed = _sanitizer.Sanitize(mixed);
        Assert.DoesNotContain("onload", cleanMixed, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("<p>Mô tả thuốc</p>", cleanMixed);
        Assert.Contains("<b>Chính hãng</b>", cleanMixed);
    }

    [Fact]
    public void BS03_HtmlSanitizer_ShouldEliminateMathAndDangerousContainers()
    {
        var mathInput = "<math><mi/xlink:href=\"javascript:alert(1)\">click</mi></math>";
        var cleanMath = _sanitizer.Sanitize(mathInput);
        Assert.DoesNotContain("<math", cleanMath, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("javascript:", cleanMath, StringComparison.OrdinalIgnoreCase);

        var iframeSlash = "<iframe/src=\"javascript:alert(2)\">";
        var cleanIframe = _sanitizer.Sanitize(iframeSlash);
        Assert.DoesNotContain("<iframe", cleanIframe, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("javascript:", cleanIframe, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void BS03_HtmlSanitizer_ShouldPreserveValidEditorFormatting()
    {
        var validHtml = "<p>Thuốc giảm đau <strong>rất tốt</strong> và <em>an toàn</em>.</p>" +
                        "<h3>Hướng dẫn sử dụng:</h3>" +
                        "<ul><li>Uống sau bữa ăn</li><li>Mỗi lần 1 viên</li></ul>" +
                        "<a href=\"https://nhathuoc.example.com\" target=\"_blank\">Xem thêm</a>";

        var sanitized = _sanitizer.Sanitize(validHtml);

        Assert.Contains("<p>Thuốc giảm đau <strong>rất tốt</strong> và <em>an toàn</em>.</p>", sanitized);
        Assert.Contains("<h3>Hướng dẫn sử dụng:</h3>", sanitized);
        Assert.Contains("<ul><li>Uống sau bữa ăn</li><li>Mỗi lần 1 viên</li></ul>", sanitized);
        Assert.Contains("href=\"https://nhathuoc.example.com\"", sanitized);
        Assert.Contains("rel=\"noopener noreferrer\"", sanitized);
    }

    // =========================================================================
    // BS-04: Nhãn hiển thị web dùng cờ trạng thái chung với POS
    // =========================================================================

    [Fact]
    public void BS04_ProductFormViewModel_LabelsIndicateSharedWebAndPosScope()
    {
        var type = typeof(AdminProductFormViewModel);
        var activeProp = type.GetProperty(nameof(AdminProductFormViewModel.IsActive));
        var sellingProp = type.GetProperty(nameof(AdminProductFormViewModel.IsSelling));

        Assert.NotNull(activeProp);
        Assert.NotNull(sellingProp);

        var activeDisplay = activeProp!.GetCustomAttribute<DisplayAttribute>();
        var sellingDisplay = sellingProp!.GetCustomAttribute<DisplayAttribute>();

        Assert.NotNull(activeDisplay);
        Assert.NotNull(sellingDisplay);

        // Display labels MUST mention both Web & POS so operators understand system-wide scope
        Assert.Contains("POS", activeDisplay!.Name, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("Web", activeDisplay.Name, StringComparison.OrdinalIgnoreCase);

        Assert.Contains("POS", sellingDisplay!.Name, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("Web", sellingDisplay.Name, StringComparison.OrdinalIgnoreCase);
    }

    // =========================================================================
    // BS-05: Thông tin nhận chuyển khoản ghi cố định trong giao diện
    // =========================================================================

    [Fact]
    public async Task BS05_BankTransfer_DisabledInConfig_BlocksBankTransferCheckout()
    {
        var disabledOptions = new BankTransferOptions
        {
            IsEnabled = false // Disabled in config!
        };

        var cartService = new FakeCartService();
        cartService.Cart.Items.Add(new CartItemViewModel
        {
            MedicineId = 1,
            MedicineName = "Paracetamol 500mg",
            Price = 25000m,
            Quantity = 1,
            AvailableStock = 10,
            IsSelling = true,
            IsActive = true
        });

        var orderService = new OrderService(_db, cartService, Options.Create(disabledOptions));

        var model = new CheckoutFormViewModel
        {
            RecipientName = "Nguyễn Văn A",
            Phone = "0912345678",
            Address = "Cần Thơ",
            PaymentMethod = "BANK_TRANSFER" // Selected bank transfer
        };

        var res = await orderService.CheckoutAsync(1, model);

        Assert.False(res.Success);
        Assert.Contains("chưa sẵn sàng", res.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task BS05_BankTransfer_OrderResult_ReturnsConfiguredBankDetails()
    {
        var customOptions = new BankTransferOptions
        {
            IsEnabled = true,
            IsDemoAccount = true,
            BankName = "MBBank (Chi nhánh Cần Thơ)",
            AccountNumber = "0987654321999",
            AccountHolder = "CONG TY DUOC PHARMACY",
            TransferInstruction = "Vui lòng chuyển đúng cú pháp để duyệt tự động.",
            DemoNotice = "Tài khoản kiểm thử bài tập lớn."
        };

        var cartService = new FakeCartService();
        var orderService = new OrderService(_db, cartService, Options.Create(customOptions));

        var order = new Order
        {
            Id = 501,
            Code = "HD261106BTCONFIG",
            CustomerId = 1,
            CustomerName = "Nguyễn Văn A",
            CustomerPhone = "0912345678",
            ShippingAddress = "Cần Thơ",
            PaymentMethod = "BANK_TRANSFER",
            PaymentStatus = "PENDING_TRANSFER",
            OrderStatus = "NEW",
            TotalAmount = 100000m
        };
        _db.Orders.Add(order);
        await _db.SaveChangesAsync();

        var result = await orderService.GetOrderResultAsync(501, 1);

        Assert.NotNull(result);
        Assert.NotNull(result!.BankTransferInfo);
        Assert.Equal("MBBank (Chi nhánh Cần Thơ)", result.BankTransferInfo!.BankName);
        Assert.Equal("0987654321999", result.BankTransferInfo.AccountNumber);
        Assert.Equal("CONG TY DUOC PHARMACY", result.BankTransferInfo.AccountHolder);
        Assert.True(result.BankTransferInfo.IsDemoAccount);
        Assert.Equal("Tài khoản kiểm thử bài tập lớn.", result.BankTransferInfo.DemoNotice);
    }

    // =========================================================================
    // Full Order Lifecycles: đặt đơn → xác nhận → nhận chuyển khoản → xuất giao → hoàn tất / hủy hoàn tiền
    // =========================================================================

    [Fact]
    public async Task FullLifecycle_BankTransfer_Checkout_Confirm_Paid_Ship_Complete()
    {
        var batch = new Batch
        {
            Id = 60,
            MedicineId = 1,
            BatchNumber = "LOT-LIFE-1",
            ExpiryDate = DateTime.UtcNow.AddYears(1),
            Stock = 20,
            ReservedQuantity = 0,
            IsBlocked = false
        };
        _db.Batches.Add(batch);
        await _db.SaveChangesAsync();

        var cartService = new FakeCartService();
        cartService.Cart.Items.Add(new CartItemViewModel
        {
            MedicineId = 1,
            MedicineName = "Paracetamol 500mg",
            Price = 25000m,
            Quantity = 2,
            AvailableStock = 20,
            IsSelling = true,
            IsActive = true
        });

        var orderService = new OrderService(_db, cartService, Options.Create(_bankOptions));
        var reportService = new ReportService(_db);

        // 1. Customer places order (BANK_TRANSFER)
        var checkoutModel = new CheckoutFormViewModel
        {
            RecipientName = "Nguyễn Văn A",
            Phone = "0912345678",
            Address = "Ninh Kiều, Cần Thơ",
            PaymentMethod = "BANK_TRANSFER",
            IdempotencyKey = "key-life-1"
        };
        var checkoutRes = await orderService.CheckoutAsync(1, checkoutModel);
        Assert.True(checkoutRes.Success);
        var orderId = checkoutRes.OrderId!.Value;

        // Check batch reserved
        var b = await _db.Batches.FindAsync(60);
        Assert.Equal(2, b!.ReservedQuantity);
        Assert.Equal(20, b.Stock);

        // 2. Admin confirms order
        var confirmRes = await orderService.UpdateOrderStatusByAdminAsync(orderId, "CONFIRMED", null, "admin");
        Assert.True(confirmRes.Success);

        // 3. Admin confirms receipt of bank transfer
        var payRes = await orderService.ConfirmCodPaymentAsync(orderId, "admin");
        Assert.True(payRes.Success);
        var orderAfterPay = await _db.Orders.FindAsync(orderId);
        Assert.Equal("CONFIRMED", orderAfterPay!.OrderStatus);
        Assert.Equal("PAID", orderAfterPay.PaymentStatus);

        // 4. Admin ships order
        var shipRes = await orderService.UpdateOrderStatusByAdminAsync(orderId, "SHIPPING", null, "admin");
        Assert.True(shipRes.Success);

        // 5. Simulate batch being blocked while in shipping!
        b.IsBlocked = true;
        await _db.SaveChangesAsync();

        // Attempt complete -> MUST FAIL due to blocked batch (BS-02)!
        var blockedCompleteRes = await orderService.UpdateOrderStatusByAdminAsync(orderId, "COMPLETED", null, "admin");
        Assert.False(blockedCompleteRes.Success);
        Assert.Contains("đang bị khóa", blockedCompleteRes.Message);

        // 6. Admin resolves batch issue (unblocks)
        b.IsBlocked = false;
        await _db.SaveChangesAsync();

        // Admin completes order successfully
        var completeRes = await orderService.UpdateOrderStatusByAdminAsync(orderId, "COMPLETED", null, "admin");
        Assert.True(completeRes.Success);

        // Stock permanently deducted: Stock 18, Reserved 0
        var bFinal = await _db.Batches.FindAsync(60);
        Assert.Equal(18, bFinal!.Stock);
        Assert.Equal(0, bFinal.ReservedQuantity);

        var finalOrder = await _db.Orders.FindAsync(orderId);
        Assert.Equal("COMPLETED", finalOrder!.OrderStatus);
        Assert.Equal("DELIVERED", finalOrder.ShippingStatus);

        // 7. Verify Revenue Report includes the completed order
        var report = await reportService.GetRevenueReportAsync(PharmacyClock.Today, PharmacyClock.Today);
        Assert.Contains(report.Orders, o => o.OrderId == orderId && o.TotalAmount == finalOrder.TotalAmount);
    }

    [Fact]
    public async Task FullLifecycle_BankTransfer_Checkout_Confirm_Paid_CancelAndRefund()
    {
        var batch = new Batch
        {
            Id = 61,
            MedicineId = 1,
            BatchNumber = "LOT-LIFE-2",
            ExpiryDate = DateTime.UtcNow.AddYears(1),
            Stock = 15,
            ReservedQuantity = 0,
            IsBlocked = false
        };
        _db.Batches.Add(batch);
        await _db.SaveChangesAsync();

        var cartService = new FakeCartService();
        cartService.Cart.Items.Add(new CartItemViewModel
        {
            MedicineId = 1,
            MedicineName = "Paracetamol 500mg",
            Price = 25000m,
            Quantity = 1,
            AvailableStock = 15,
            IsSelling = true,
            IsActive = true
        });

        var orderService = new OrderService(_db, cartService, Options.Create(_bankOptions));
        var reportService = new ReportService(_db);

        // 1. Customer places order
        var checkoutRes = await orderService.CheckoutAsync(1, new CheckoutFormViewModel
        {
            RecipientName = "Nguyễn Văn A",
            Phone = "0912345678",
            Address = "Ninh Kiều, Cần Thơ",
            PaymentMethod = "BANK_TRANSFER",
            IdempotencyKey = "key-life-2"
        });
        Assert.True(checkoutRes.Success);
        var orderId = checkoutRes.OrderId!.Value;

        // 2. Admin confirms order
        await orderService.UpdateOrderStatusByAdminAsync(orderId, "CONFIRMED", null, "admin");

        // 3. Admin confirms receipt of bank transfer
        await orderService.ConfirmCodPaymentAsync(orderId, "cashier");

        // 4. Admin tries direct cancel -> MUST BE BLOCKED (BS-01)!
        var directCancel = await orderService.UpdateOrderStatusByAdminAsync(orderId, "CANCELLED", "Hủy thường", "admin");
        Assert.False(directCancel.Success);

        // 5. Admin performs refund & cancel
        var refundRes = await orderService.RefundOrderAsync(orderId, "Khách yêu cầu hủy qua điện thoại và hoàn khoản chuyển", "admin");
        Assert.True(refundRes.Success);

        // Reserved batch quantity released
        var b = await _db.Batches.FindAsync(61);
        Assert.Equal(0, b!.ReservedQuantity);
        Assert.Equal(15, b.Stock);

        // Order marked CANCELLED, payment REFUNDED
        var order = await _db.Orders.FindAsync(orderId);
        Assert.Equal("CANCELLED", order!.OrderStatus);
        Assert.Equal("REFUNDED", order.PaymentStatus);
        Assert.True(order.IsRefunded);
        Assert.NotNull(order.PaidAt); // Original payment date kept!

        // 6. Cannot refund again
        var secondRefund = await orderService.RefundOrderAsync(orderId, "Thử lại", "admin");
        Assert.False(secondRefund.Success);

        // 7. Verify Revenue Report reconciles collected and refunded
        var report = await reportService.GetRevenueReportAsync(PharmacyClock.Today, PharmacyClock.Today);
        var reportedItem = report.Orders.FirstOrDefault(o => o.OrderId == orderId);
        Assert.NotNull(reportedItem);
        Assert.True(reportedItem!.IsRefunded);
        Assert.Equal(reportedItem.CollectedInPeriod, reportedItem.RefundedInPeriod);
        Assert.Equal(0m, reportedItem.NetAmount);
    }
}
