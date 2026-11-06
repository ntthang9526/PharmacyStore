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
            .Where(o => (o.PaymentStatus == "PAID" || o.PaymentStatus == "REFUNDED" || o.IsRefunded) && o.PaidAt >= startOfDayUtc)
            .Select(o => o.TotalAmount)
            .ToListAsync();
        var refundedTodayList = await _db.Orders
            .Where(o => o.IsRefunded && o.RefundedAt >= startOfDayUtc)
            .Select(o => o.RefundAmount)
            .ToListAsync();
        var paidToday = paidTodayList.Sum() - refundedTodayList.Sum();

        var paidMonthList = await _db.Orders
            .Where(o => (o.PaymentStatus == "PAID" || o.PaymentStatus == "REFUNDED" || o.IsRefunded) && o.PaidAt >= startOfMonthUtc)
            .Select(o => o.TotalAmount)
            .ToListAsync();
        var refundedMonthList = await _db.Orders
            .Where(o => o.IsRefunded && o.RefundedAt >= startOfMonthUtc)
            .Select(o => o.RefundAmount)
            .ToListAsync();
        var paidMonth = paidMonthList.Sum() - refundedMonthList.Sum();

        var totalAvailable = await _db.Medicines
            .Include(m => m.Batches)
            .Where(m => m.IsActive && m.IsSelling)
            .CountAsync(m => m.Batches.Any(b => !b.IsBlocked && b.ExpiryDate >= todayVn && (b.Stock - b.ReservedQuantity) > 0));

        var lowStockBatches = await _db.Batches
            .Include(b => b.Medicine)
            .Where(b => b.Stock <= 5)
            .ToListAsync();

        var expiringLimit = todayVn.AddDays(90);
        var expiringSoonBatches = await _db.Batches
            .Include(b => b.Medicine)
            .Where(b => b.ExpiryDate >= todayVn && b.ExpiryDate <= expiringLimit)
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
        var todayVn = PharmacyClock.Today;
        var start = fromDate?.Date ?? new DateTime(todayVn.Year, todayVn.Month, 1);
        var end = toDate?.Date ?? todayVn;

        if (start > end)
        {
            (start, end) = (end, start);
        }

        var startUtc = start.AddHours(-7);
        var nextDayUtc = end.AddDays(1).AddHours(-7);

        // Fetch orders that were either paid in this period OR refunded in this period
        var relevantOrders = await _db.Orders
            .AsNoTracking()
            .Where(o => (o.PaymentStatus == "PAID" || o.PaymentStatus == "REFUNDED" || o.IsRefunded)
                     && ((o.PaidAt >= startUtc && o.PaidAt < nextDayUtc) ||
                         (o.RefundedAt >= startUtc && o.RefundedAt < nextDayUtc)))
            .OrderByDescending(o => o.PaidAt ?? o.RefundedAt ?? o.OrderDate)
            .ToListAsync();

        var orderItems = relevantOrders.Select(o =>
        {
            bool collectedInPeriod = o.PaidAt.HasValue && o.PaidAt.Value >= startUtc && o.PaidAt.Value < nextDayUtc;
            bool refundedInPeriod = o.IsRefunded && o.RefundedAt.HasValue && o.RefundedAt.Value >= startUtc && o.RefundedAt.Value < nextDayUtc;

            decimal collectedAmount = collectedInPeriod ? o.TotalAmount : 0m;
            decimal refundAmount = refundedInPeriod ? o.RefundAmount : 0m;

            return new RevenueOrderItemViewModel
            {
                OrderId = o.Id,
                OrderCode = o.Code,
                OrderDate = o.OrderDate,
                PaidAt = o.PaidAt,
                CustomerName = o.CustomerName,
                PaymentMethod = o.PaymentMethod,
                TotalAmount = o.TotalAmount,
                PaidBy = o.PaidByEmployee ?? "Hệ thống",
                OrderStatus = o.OrderStatus,
                PaymentStatus = o.PaymentStatus,
                IsRefunded = o.IsRefunded,
                RefundAmount = o.RefundAmount,
                RefundedAt = o.RefundedAt,
                RefundedBy = o.RefundedByEmployee,
                RefundReason = o.RefundReason,
                CollectedInPeriod = collectedAmount,
                RefundedInPeriod = refundAmount
            };
        }).ToList();

        var totalCollected = orderItems.Sum(o => o.CollectedInPeriod);
        var totalRefunded = orderItems.Sum(o => o.RefundedInPeriod);
        var paidCount = orderItems.Count(o => o.CollectedInPeriod > 0);
        var refundedCount = orderItems.Count(o => o.RefundedInPeriod > 0);

        return new RevenueReportViewModel
        {
            FromDate = start,
            ToDate = end,
            TotalPaidOrders = paidCount,
            TotalRefundedOrders = refundedCount,
            TotalCollectedRevenue = totalCollected,
            TotalRefundedAmount = totalRefunded,
            Orders = orderItems
        };
    }

    public async Task<string> GenerateRevenueCsvAsync(DateTime? fromDate, DateTime? toDate)
    {
        var report = await GetRevenueReportAsync(fromDate, toDate);
        var sb = new System.Text.StringBuilder();

        sb.AppendLine("Mã Đơn,Ngày Đặt,Ngày Thu Tiền,Trạng Thái TT,Khách Hàng,Phương Thức,Số Tiền Thu (VNĐ),Số Tiền Hoàn (VNĐ),Thực Nhận (VNĐ),Ngày Hoàn,Lý Do Hoàn,Người Thu,Người Hoàn");
        foreach (var o in report.Orders)
        {
            var orderDateVn = o.OrderDate.AddHours(7).ToString("dd/MM/yyyy HH:mm");
            var paidAtVn = o.PaidAt.HasValue ? o.PaidAt.Value.AddHours(7).ToString("dd/MM/yyyy HH:mm") : "-";
            var refundedAtVn = o.RefundedAt.HasValue ? o.RefundedAt.Value.AddHours(7).ToString("dd/MM/yyyy HH:mm") : "-";
            var net = o.CollectedInPeriod - o.RefundedInPeriod;
            var paymentStatusText = o.IsRefunded ? "Đã hoàn tiền" : "Đã thanh toán";
            var safeReason = (o.RefundReason ?? "-").Replace("\"", "\"\"");

            sb.AppendLine($"\"{o.OrderCode}\",\"{orderDateVn}\",\"{paidAtVn}\",\"{paymentStatusText}\",\"{o.CustomerName}\",\"{o.PaymentMethod}\",{o.CollectedInPeriod:0},{o.RefundedInPeriod:0},{net:0},\"{refundedAtVn}\",\"{safeReason}\",\"{o.PaidBy}\",\"{o.RefundedBy ?? "-"}\"");
        }

        return sb.ToString();
    }
}
