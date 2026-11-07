using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PharmacyWeb.Data;
using PharmacyWeb.Data.Entities;
using PharmacyWeb.Models;
using PharmacyWeb.Services;

namespace PharmacyWeb.Tests;

public class SupplementalVerificationTests_20261107 : IDisposable
{
    private readonly SqliteConnection _connection;
    private readonly PharmacyWebDbContext _db;
    private readonly BankTransferOptions _bankOptions = new()
    {
        IsEnabled = true,
        IsDemoAccount = true,
        BankName = "Vietcombank (Chi nhánh Cần Thơ)",
        AccountNumber = "1029384756",
        AccountHolder = "NHA THUOC PHARMACY STORE",
        TransferInstruction = "Dược sĩ sẽ kiểm tra biến động số dư và xác nhận ngay cho quý khách.",
        DemoNotice = "Thông tin tài khoản ngân hàng thử nghiệm phục vụ đồ án/demo."
    };

    public SupplementalVerificationTests_20261107()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<PharmacyWebDbContext>()
            .UseSqlite(_connection)
            .Options;

        _db = new PharmacyWebDbContext(options);
        _db.Database.EnsureCreated();

        SeedData();
    }

    private void SeedData()
    {
        _db.Categories.Add(new Category { Id = 1, Name = "Thuốc giảm đau", Slug = "nhom-giam-dau", IsActive = true });
        _db.Suppliers.Add(new Supplier { Id = 1, Name = "Dược Hậu Giang", Phone = "02923891433", IsActive = true });
        _db.CustomerProfiles.Add(new CustomerProfile { Id = 1, FullName = "Khách Hàng Test", Phone = "0912345678", Email = "khach@test.com", WebAccountActive = true });
        _db.Medicines.Add(new Medicine { Id = 1, CategoryId = 1, SupplierId = 1, Name = "Panadol Extra", Slug = "panadol-extra", Unit = "Hộp", Price = 35000m, IsActive = true, IsSelling = true });
        _db.Batches.Add(new Batch { Id = 10, MedicineId = 1, BatchNumber = "LOT-TEST-10", ExpiryDate = PharmacyClock.Today.AddDays(180), Stock = 10, ReservedQuantity = 2, IsBlocked = false });
        _db.SaveChanges();
    }

    public void Dispose()
    {
        _db.Dispose();
        _connection.Dispose();
    }

    [Fact]
    public async Task KT01_RefundOrder_OnShippingOrder_MustBeBlocked_AndPreserveStockAndStatus()
    {
        var cartService = new FakeCartService();
        var orderService = new OrderService(_db, cartService, Options.Create(_bankOptions));

        var order = new Order
        {
            Id = 501,
            Code = "HD-SHIPPING-01",
            CustomerId = 1,
            CustomerName = "Nguyễn Văn Giao",
            CustomerPhone = "0912345678",
            ShippingAddress = "123 Đường 3/2, Cần Thơ",
            PaymentMethod = "BANK_TRANSFER",
            PaymentStatus = "PAID",
            OrderStatus = "SHIPPING",
            ShippingStatus = "SHIPPING",
            OrderDate = DateTime.UtcNow.AddHours(-3),
            PaidAt = DateTime.UtcNow.AddHours(-2),
            PaidByEmployee = "cashier1",
            TotalAmount = 70000m,
            Allocations = new List<OrderBatchAllocation>
            {
                new() { BatchId = 10, Quantity = 2, IsDeducted = false }
            }
        };
        _db.Orders.Add(order);
        await _db.SaveChangesAsync();

        // Admin attempts to refund while order is SHIPPING -> MUST BE REJECTED!
        var refundResult = await orderService.RefundOrderAsync(501, "Khách yêu cầu hoàn tiền khi hàng đang giao", "admin");

        Assert.False(refundResult.Success);
        Assert.Contains("đang giao", refundResult.Message, StringComparison.OrdinalIgnoreCase);

        // Check that reservation and stock are untouched
        var batch = await _db.Batches.FindAsync(10);
        Assert.Equal(2, batch!.ReservedQuantity);
        Assert.Equal(10, batch.Stock);

        // Check that order status and payment status are unchanged
        var currentOrder = await _db.Orders.FindAsync(501);
        Assert.Equal("SHIPPING", currentOrder!.OrderStatus);
        Assert.Equal("PAID", currentOrder.PaymentStatus);
        Assert.False(currentOrder.IsRefunded);
        Assert.Equal(0m, currentOrder.RefundAmount);
        Assert.Null(currentOrder.RefundedAt);
    }

    [Fact]
    public async Task KT01_RefundOrder_OnCompletedOrder_MustBeBlocked()
    {
        var cartService = new FakeCartService();
        var orderService = new OrderService(_db, cartService, Options.Create(_bankOptions));

        var order = new Order
        {
            Id = 502,
            Code = "HD-COMPLETED-01",
            CustomerId = 1,
            CustomerName = "Trần Thị B",
            CustomerPhone = "0987654321",
            ShippingAddress = "Ninh Kiều, Cần Thơ",
            PaymentMethod = "BANK_TRANSFER",
            PaymentStatus = "PAID",
            OrderStatus = "COMPLETED",
            ShippingStatus = "DELIVERED",
            OrderDate = DateTime.UtcNow.AddDays(-1),
            PaidAt = DateTime.UtcNow.AddDays(-1),
            TotalAmount = 35000m
        };
        _db.Orders.Add(order);
        await _db.SaveChangesAsync();

        var refundResult = await orderService.RefundOrderAsync(502, "Yêu cầu hoàn đơn đã hoàn tất", "admin");
        Assert.False(refundResult.Success);
        Assert.Contains("hoàn tất", refundResult.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task KT01_RefundOrder_OnConfirmedOrder_ShouldSucceed_AndReleaseStock()
    {
        var cartService = new FakeCartService();
        var orderService = new OrderService(_db, cartService, Options.Create(_bankOptions));

        var order = new Order
        {
            Id = 503,
            Code = "HD-CONFIRMED-01",
            CustomerId = 1,
            CustomerName = "Lê Văn C",
            CustomerPhone = "0901234567",
            ShippingAddress = "Cái Răng, Cần Thơ",
            PaymentMethod = "BANK_TRANSFER",
            PaymentStatus = "PAID",
            OrderStatus = "CONFIRMED",
            ShippingStatus = "NOT_SHIPPED",
            OrderDate = DateTime.UtcNow.AddHours(-1),
            PaidAt = DateTime.UtcNow.AddMinutes(-30),
            PaidByEmployee = "cashier2",
            TotalAmount = 70000m,
            Allocations = new List<OrderBatchAllocation>
            {
                new() { BatchId = 10, Quantity = 2, IsDeducted = false }
            }
        };
        _db.Orders.Add(order);
        await _db.SaveChangesAsync();

        var refundResult = await orderService.RefundOrderAsync(503, "Khách đổi địa chỉ, hủy hoàn tiền trước giao", "admin");
        Assert.True(refundResult.Success);

        // Reserved stock released
        var batch = await _db.Batches.FindAsync(10);
        Assert.Equal(0, batch!.ReservedQuantity);
        Assert.Equal(10, batch.Stock);

        // Status updated to CANCELLED & REFUNDED
        var currentOrder = await _db.Orders.FindAsync(503);
        Assert.Equal("CANCELLED", currentOrder!.OrderStatus);
        Assert.Equal("REFUNDED", currentOrder.PaymentStatus);
        Assert.True(currentOrder.IsRefunded);
        Assert.Equal(70000m, currentOrder.RefundAmount);
        Assert.NotNull(currentOrder.RefundedAt);
        Assert.Equal("admin", currentOrder.RefundedByEmployee);
    }

    [Fact]
    public async Task KT02_RevenueCsv_SpecialCharactersAndQuotes_ShouldPreserveColumnsAndValues()
    {
        var reportService = new ReportService(_db);
        var today = PharmacyClock.Today;

        // Create order with complex customer name: A",B (contains quote and comma)
        // Also test complex refund reason with quotes and commas
        var order1 = new Order
        {
            Id = 601,
            Code = "HD-CSV-01",
            CustomerId = 1,
            CustomerName = "A\",B",
            CustomerPhone = "0911222333",
            ShippingAddress = "Địa chỉ test",
            PaymentMethod = "BANK_TRANSFER",
            PaymentStatus = "PAID",
            OrderStatus = "CONFIRMED",
            ShippingStatus = "NOT_SHIPPED",
            OrderDate = today.AddHours(8),
            PaidAt = today.AddHours(9),
            PaidByEmployee = "Thu ngân \"1\"",
            TotalAmount = 150000m
        };

        var order2 = new Order
        {
            Id = 602,
            Code = "HD-CSV-02",
            CustomerId = 1,
            CustomerName = "Khách Hàng \"VIP, Thân Thiết\"",
            CustomerPhone = "0944555666",
            ShippingAddress = "Địa chỉ test 2",
            PaymentMethod = "COD",
            PaymentStatus = "REFUNDED",
            OrderStatus = "CANCELLED",
            ShippingStatus = "NOT_SHIPPED",
            OrderDate = today.AddHours(10),
            PaidAt = today.AddHours(11),
            PaidByEmployee = "admin",
            TotalAmount = 200000m,
            IsRefunded = true,
            RefundAmount = 200000m,
            RefundedAt = today.AddHours(12),
            RefundedByEmployee = "Quản lý \"A, B\"",
            RefundReason = "Lý do: \"Thuốc bị ẩm\", khách đổi ý"
        };

        _db.Orders.AddRange(order1, order2);
        await _db.SaveChangesAsync();

        var csv = await reportService.GenerateRevenueCsvAsync(today, today);
        Assert.NotNull(csv);

        var lines = csv.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        Assert.True(lines.Length >= 3); // 1 header + 2 data rows

        // Parse each row using RFC 4180 compliant parsing
        var headerColumns = ParseCsvLine(lines[0]);
        Assert.Equal(13, headerColumns.Count);

        var dataRows = lines.Skip(1).Select(ParseCsvLine).ToList();
        Assert.Equal(2, dataRows.Count);

        var row1Columns = dataRows.First(r => r[0] == "HD-CSV-01");
        Assert.Equal(13, row1Columns.Count);
        Assert.Equal("A\",B", row1Columns[4]); // Customer name with quote and comma preserved exactly!
        Assert.Equal("150000", row1Columns[6]); // Amount collected
        Assert.Equal("0", row1Columns[7]); // Amount refunded
        Assert.Equal("150000", row1Columns[8]); // Net amount
        Assert.Equal("Thu ngân \"1\"", row1Columns[11]); // Paid by with internal quotes preserved!

        var row2Columns = dataRows.First(r => r[0] == "HD-CSV-02");
        Assert.Equal(13, row2Columns.Count);
        Assert.Equal("Khách Hàng \"VIP, Thân Thiết\"", row2Columns[4]);
        Assert.Equal("200000", row2Columns[6]);
        Assert.Equal("200000", row2Columns[7]);
        Assert.Equal("0", row2Columns[8]);
        Assert.Equal("Lý do: \"Thuốc bị ẩm\", khách đổi ý", row2Columns[10]);
        Assert.Equal("Quản lý \"A, B\"", row2Columns[12]);
    }

    /// <summary>
    /// Standard RFC 4180 CSV line parser
    /// </summary>
    private static List<string> ParseCsvLine(string line)
    {
        var result = new List<string>();
        var pattern = "(?:^|,)(\"(?:[^\"]+|\"\")*\"|[^,]*)";
        var matches = Regex.Matches(line, pattern);

        foreach (Match match in matches)
        {
            var value = match.Groups[1].Value;
            if (value.StartsWith("\"") && value.EndsWith("\""))
            {
                value = value[1..^1].Replace("\"\"", "\"");
            }
            result.Add(value);
        }
        return result;
    }
}
