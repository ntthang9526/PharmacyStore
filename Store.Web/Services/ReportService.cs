using Microsoft.EntityFrameworkCore;
using PharmacyWeb.Data;
using PharmacyWeb.Models.ViewModels;

namespace PharmacyWeb.Services;

public interface IReportService
{
    Task<AdminDashboardViewModel> GetDashboardMetricsAsync();
    Task<RevenueReportViewModel> GetRevenueReportAsync(DateTime? fromDate, DateTime? toDate);
    Task<string> GenerateRevenueCsvAsync(DateTime? fromDate, DateTime? toDate);
}

public class ReportService : IReportService
{
    private readonly PharmacyWebDbContext _db;

    public ReportService(PharmacyWebDbContext db)
    {
        _db = db;
    }

    public async Task<AdminDashboardViewModel> GetDashboardMetricsAsync()
    {
        var nowUtc = DateTime.UtcNow;
        var todayVn = nowUtc.AddHours(7).Date;
        var startOfMonthVn = new DateTime(todayVn.Year, todayVn.Month, 1);

        var startOfDayUtc = todayVn.AddHours(-7);
        var startOfMonthUtc = startOfMonthVn.AddHours(-7);

        var newOrdersCount = await _db.Orders.CountAsync(o => o.OrderStatus == "NEW");

        var paidTodayList = await _db.Orders
            .Where(o => o.PaymentStatus == "PAID" && o.PaidAt >= startOfDayUtc)
            .Select(o => o.TotalAmount)
            .ToListAsync();
        var paidToday = paidTodayList.Sum();

        var paidMonthList = await _db.Orders
            .Where(o => o.PaymentStatus == "PAID" && o.PaidAt >= startOfMonthUtc)
            .Select(o => o.TotalAmount)
            .ToListAsync();
        var paidMonth = paidMonthList.Sum();

        var totalAvailable = await _db.Medicines
            .Include(m => m.Batches)
            .Where(m => m.IsActive && m.IsSelling)
            .CountAsync(m => m.Batches.Any(b => !b.IsBlocked && b.ExpiryDate > nowUtc.Date && (b.Stock - b.ReservedQuantity) > 0));

        var lowStockBatches = await _db.Batches
            .Include(b => b.Medicine)
            .Where(b => b.Stock <= 5)
            .ToListAsync();

        var expiringLimit = nowUtc.Date.AddDays(90);
        var expiringSoonBatches = await _db.Batches
            .Include(b => b.Medicine)
            .Where(b => b.ExpiryDate > nowUtc.Date && b.ExpiryDate <= expiringLimit)
            .ToListAsync();

        var pendingPrescriptions = await _db.PrescriptionRequests
            .CountAsync(p => p.Status == "PENDING_REVIEW");

        var recentOrders = await _db.Orders
            .AsNoTracking()
            .OrderByDescending(o => o.OrderDate)
            .Take(5)
            .Select(o => new OrderRecentItemViewModel
            {
                Id = o.Id,
                Code = o.Code,
                CustomerName = o.CustomerName,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount,
                OrderStatus = o.OrderStatus,
                PaymentStatus = o.PaymentStatus
            })
            .ToListAsync();

        var alerts = new List<BatchAlertViewModel>();

        foreach (var b in expiringSoonBatches.Take(4))
        {
            alerts.Add(new BatchAlertViewModel
            {
                BatchId = b.Id,
                BatchNumber = b.BatchNumber,
                MedicineName = b.Medicine?.Name ?? "Thuốc",
                ExpiryDate = b.ExpiryDate,
                Stock = b.Stock,
                AlertType = "EXPIRING_SOON"
            });
        }

        foreach (var b in lowStockBatches.Take(4))
        {
            if (!alerts.Any(a => a.BatchId == b.Id))
            {
                alerts.Add(new BatchAlertViewModel
                {
                    BatchId = b.Id,
                    BatchNumber = b.BatchNumber,
                    MedicineName = b.Medicine?.Name ?? "Thuốc",
                    ExpiryDate = b.ExpiryDate,
                    Stock = b.Stock,
                    AlertType = "LOW_STOCK"
                });
            }
        }

        return new AdminDashboardViewModel
        {
            NewOrdersCount = newOrdersCount,
            PaidRevenueToday = paidToday,
            PaidRevenueMonth = paidMonth,
            TotalAvailableProducts = totalAvailable,
            LowStockCount = lowStockBatches.Count,
            ExpiringSoonCount = expiringSoonBatches.Count,
            PendingPrescriptionsCount = pendingPrescriptions,
            RecentOrders = recentOrders,
            BatchAlerts = alerts
        };
    }

    public async Task<RevenueReportViewModel> GetRevenueReportAsync(DateTime? fromDate, DateTime? toDate)
    {
        var nowVn = DateTime.UtcNow.AddHours(7);
        var start = fromDate ?? new DateTime(nowVn.Year, nowVn.Month, 1);
        var end = toDate ?? nowVn.Date.AddDays(1).AddTicks(-1);

        var startUtc = start.AddHours(-7);
        var endUtc = end.AddHours(-7);

        var paidOrders = await _db.Orders
            .AsNoTracking()
            .Where(o => o.PaymentStatus == "PAID" && o.PaidAt >= startUtc && o.PaidAt <= endUtc)
            .OrderByDescending(o => o.PaidAt)
            .Select(o => new RevenueOrderItemViewModel
            {
                OrderId = o.Id,
                OrderCode = o.Code,
                OrderDate = o.OrderDate,
                PaidAt = o.PaidAt,
                CustomerName = o.CustomerName,
                PaymentMethod = o.PaymentMethod,
                TotalAmount = o.TotalAmount,
                PaidBy = o.PaidByEmployee ?? "Hệ thống"
            })
            .ToListAsync();

        return new RevenueReportViewModel
        {
            FromDate = start,
            ToDate = end,
            TotalPaidOrders = paidOrders.Count,
            TotalCollectedRevenue = paidOrders.Sum(o => o.TotalAmount),
            Orders = paidOrders
        };
    }

    public async Task<string> GenerateRevenueCsvAsync(DateTime? fromDate, DateTime? toDate)
    {
        var report = await GetRevenueReportAsync(fromDate, toDate);
        var sb = new System.Text.StringBuilder();

        sb.AppendLine("Mã Đơn,Ngày Đặt,Ngày Thu Tiền,Khách Hàng,Phương Thức,Số Tiền Đã Thu (VNĐ),Người Thu");
        foreach (var o in report.Orders)
        {
            var orderDateVn = o.OrderDate.AddHours(7).ToString("dd/MM/yyyy HH:mm");
            var paidAtVn = o.PaidAt.HasValue ? o.PaidAt.Value.AddHours(7).ToString("dd/MM/yyyy HH:mm") : "-";
            sb.AppendLine($"\"{o.OrderCode}\",\"{orderDateVn}\",\"{paidAtVn}\",\"{o.CustomerName}\",\"{o.PaymentMethod}\",{o.TotalAmount:0},\"{o.PaidBy}\"");
        }

        return sb.ToString();
    }
}
