using System.Globalization;
using System.Text;
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
    private readonly PharmacyWebDbContext _databaseContext;

    public ReportService(PharmacyWebDbContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    /// <summary>
    /// Thu thập các chỉ số KPI tổng quan cho Dashboard Quản trị:
    /// - Đơn hàng mới cần duyệt.
    /// - Thực thu hôm nay & tháng này (Doanh thu đã trừ các khoản hoàn tiền nếu có).
    /// - Số sản phẩm có thể bán, cảnh báo hết hạn và cảnh báo tồn kho thấp.
    /// </summary>
    public async Task<AdminDashboardViewModel> GetDashboardMetricsAsync()
    {
        var currentUtcTime = DateTime.UtcNow;
        var todayVietnamTime = currentUtcTime.AddHours(7).Date;
        var startOfMonthVietnamTime = new DateTime(todayVietnamTime.Year, todayVietnamTime.Month, 1);

        // Chuyển mốc thời gian Việt Nam (UTC+7) về chuẩn UTC để truy vấn cơ sở dữ liệu
        var startOfDayUtc = todayVietnamTime.AddHours(-7);
        var startOfMonthUtc = startOfMonthVietnamTime.AddHours(-7);

        int newOrdersCount = await _databaseContext.Orders
            .CountAsync(order => order.OrderStatus == "NEW");

        // Tính doanh thu thực hôm nay: Tổng tiền đã thu trừ đi tổng tiền đã hoàn lại trong ngày
        var paidTodayList = await _databaseContext.Orders
            .Where(order => (order.PaymentStatus == "PAID" || order.PaymentStatus == "REFUNDED" || order.IsRefunded) &&
                            order.PaidAt >= startOfDayUtc)
            .Select(order => order.TotalAmount)
            .ToListAsync();

        var refundedTodayList = await _databaseContext.Orders
            .Where(order => order.IsRefunded && order.RefundedAt >= startOfDayUtc)
            .Select(order => order.RefundAmount)
            .ToListAsync();

        decimal paidToday = paidTodayList.Sum() - refundedTodayList.Sum();

        // Tính doanh thu thực tháng này: Tổng tiền đã thu trừ đi tổng tiền đã hoàn lại trong tháng
        var paidMonthList = await _databaseContext.Orders
            .Where(order => (order.PaymentStatus == "PAID" || order.PaymentStatus == "REFUNDED" || order.IsRefunded) &&
                            order.PaidAt >= startOfMonthUtc)
            .Select(order => order.TotalAmount)
            .ToListAsync();

        var refundedMonthList = await _databaseContext.Orders
            .Where(order => order.IsRefunded && order.RefundedAt >= startOfMonthUtc)
            .Select(order => order.RefundAmount)
            .ToListAsync();

        decimal paidMonth = paidMonthList.Sum() - refundedMonthList.Sum();

        int totalAvailableProducts = await _databaseContext.Medicines
            .Include(medicine => medicine.Batches)
            .Where(medicine => medicine.IsActive && medicine.IsSelling)
            .CountAsync(medicine => medicine.Batches.Any(batch =>
                !batch.IsBlocked &&
                batch.ExpiryDate >= todayVietnamTime &&
                (batch.Stock - batch.ReservedQuantity) > 0));

        var lowStockBatches = await _databaseContext.Batches
            .Include(batch => batch.Medicine)
            .Where(batch => batch.Stock <= 5)
            .ToListAsync();

        var expiringLimitDate = todayVietnamTime.AddDays(90);
        var expiringSoonBatches = await _databaseContext.Batches
            .Include(batch => batch.Medicine)
            .Where(batch => batch.ExpiryDate >= todayVietnamTime && batch.ExpiryDate <= expiringLimitDate)
            .ToListAsync();

        int pendingPrescriptionsCount = await _databaseContext.PrescriptionRequests
            .CountAsync(request => request.Status == "PENDING_REVIEW");

        var recentOrders = await _databaseContext.Orders
            .AsNoTracking()
            .OrderByDescending(order => order.OrderDate)
            .Take(5)
            .Select(order => new OrderRecentItemViewModel
            {
                Id = order.Id,
                Code = order.Code,
                CustomerName = order.CustomerName,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                OrderStatus = order.OrderStatus,
                PaymentStatus = order.PaymentStatus
            })
            .ToListAsync();

        var batchAlerts = new List<BatchAlertViewModel>();

        foreach (var batch in expiringSoonBatches.Take(4))
        {
            batchAlerts.Add(new BatchAlertViewModel
            {
                BatchId = batch.Id,
                BatchNumber = batch.BatchNumber,
                MedicineName = batch.Medicine?.Name ?? "Thuốc",
                ExpiryDate = batch.ExpiryDate,
                Stock = batch.Stock,
                AlertType = "EXPIRING_SOON"
            });
        }

        foreach (var batch in lowStockBatches.Take(4))
        {
            bool isAlreadyInAlerts = batchAlerts.Any(alert => alert.BatchId == batch.Id);
            if (!isAlreadyInAlerts)
            {
                batchAlerts.Add(new BatchAlertViewModel
                {
                    BatchId = batch.Id,
                    BatchNumber = batch.BatchNumber,
                    MedicineName = batch.Medicine?.Name ?? "Thuốc",
                    ExpiryDate = batch.ExpiryDate,
                    Stock = batch.Stock,
                    AlertType = "LOW_STOCK"
                });
            }
        }

        return new AdminDashboardViewModel
        {
            NewOrdersCount = newOrdersCount,
            PaidRevenueToday = paidToday,
            PaidRevenueMonth = paidMonth,
            TotalAvailableProducts = totalAvailableProducts,
            LowStockCount = lowStockBatches.Count,
            ExpiringSoonCount = expiringSoonBatches.Count,
            PendingPrescriptionsCount = pendingPrescriptionsCount,
            RecentOrders = recentOrders,
            BatchAlerts = batchAlerts
        };
    }

    /// <summary>
    /// Xuất báo cáo doanh thu tài chính theo khoảng thời gian tùy chọn.
    /// Đối chiếu 3 đại lượng: Tiền thu, Tiền hoàn và Doanh thu thực nhận (BS-01).
    /// </summary>
    public async Task<RevenueReportViewModel> GetRevenueReportAsync(DateTime? fromDate, DateTime? toDate)
    {
        var todayVietnamTime = PharmacyClock.Today;
        var startDate = fromDate?.Date ?? new DateTime(todayVietnamTime.Year, todayVietnamTime.Month, 1);
        var endDate = toDate?.Date ?? todayVietnamTime;

        if (startDate > endDate)
        {
            (startDate, endDate) = (endDate, startDate);
        }

        var startUtcTime = startDate.AddHours(-7);
        var endUtcTime = endDate.AddDays(1).AddHours(-7); // Hết ngày cuối cùng (23:59:59)

        // Lấy tất cả đơn có phát sinh giao dịch thu tiền HOẶC hoàn tiền trong kỳ báo cáo
        var relevantOrders = await _databaseContext.Orders
            .AsNoTracking()
            .Where(order => (order.PaymentStatus == "PAID" || order.PaymentStatus == "REFUNDED" || order.IsRefunded) &&
                            ((order.PaidAt >= startUtcTime && order.PaidAt < endUtcTime) ||
                             (order.RefundedAt >= startUtcTime && order.RefundedAt < endUtcTime)))
            .OrderByDescending(order => order.PaidAt ?? order.RefundedAt ?? order.OrderDate)
            .ToListAsync();

        var reportOrderItems = relevantOrders.Select(order =>
        {
            bool isCollectedInThisPeriod = order.PaidAt.HasValue &&
                                          order.PaidAt.Value >= startUtcTime &&
                                          order.PaidAt.Value < endUtcTime;

            bool isRefundedInThisPeriod = order.IsRefunded &&
                                         order.RefundedAt.HasValue &&
                                         order.RefundedAt.Value >= startUtcTime &&
                                         order.RefundedAt.Value < endUtcTime;

            decimal collectedAmount = isCollectedInThisPeriod ? order.TotalAmount : 0m;
            decimal refundAmount = isRefundedInThisPeriod ? order.RefundAmount : 0m;

            return new RevenueOrderItemViewModel
            {
                OrderId = order.Id,
                OrderCode = order.Code,
                OrderDate = order.OrderDate,
                PaidAt = order.PaidAt,
                CustomerName = order.CustomerName,
                PaymentMethod = order.PaymentMethod,
                TotalAmount = order.TotalAmount,
                PaidBy = order.PaidByEmployee ?? "Hệ thống",
                OrderStatus = order.OrderStatus,
                PaymentStatus = order.PaymentStatus,
                IsRefunded = order.IsRefunded,
                RefundAmount = order.RefundAmount,
                RefundedAt = order.RefundedAt,
                RefundedBy = order.RefundedByEmployee,
                RefundReason = order.RefundReason,
                CollectedInPeriod = collectedAmount,
                RefundedInPeriod = refundAmount
            };
        }).ToList();

        decimal totalCollected = reportOrderItems.Sum(item => item.CollectedInPeriod);
        decimal totalRefunded = reportOrderItems.Sum(item => item.RefundedInPeriod);
        int totalPaidOrdersCount = reportOrderItems.Count(item => item.CollectedInPeriod > 0);
        int totalRefundedOrdersCount = reportOrderItems.Count(item => item.RefundedInPeriod > 0);

        return new RevenueReportViewModel
        {
            FromDate = startDate,
            ToDate = endDate,
            TotalPaidOrders = totalPaidOrdersCount,
            TotalRefundedOrders = totalRefundedOrdersCount,
            TotalCollectedRevenue = totalCollected,
            TotalRefundedAmount = totalRefunded,
            Orders = reportOrderItems
        };
    }

    /// <summary>
    /// Xuất báo cáo doanh thu ra định dạng tệp CSV để người vận hành mở trong Excel đối chiếu.
    /// KT-02: Chuẩn hóa escape toàn bộ trường chuỗi theo chuẩn RFC 4180 (nhân đôi nháy kép và bọc trong nháy kép),
    /// bảo đảm không bị lệch cột khi dữ liệu chứa dấu phẩy, nháy kép, xuống dòng hoặc ký tự đặc biệt.
    /// </summary>
    public async Task<string> GenerateRevenueCsvAsync(DateTime? fromDate, DateTime? toDate)
    {
        var revenueReport = await GetRevenueReportAsync(fromDate, toDate);
        var csvBuilder = new StringBuilder();

        // Tiêu đề các cột dữ liệu (13 cột chuẩn)
        csvBuilder.AppendLine("Mã Đơn,Ngày Đặt,Ngày Thu Tiền,Trạng Thái TT,Khách Hàng,Phương Thức,Số Tiền Thu (VNĐ),Số Tiền Hoàn (VNĐ),Thực Nhận (VNĐ),Ngày Hoàn,Lý Do Hoàn,Người Thu,Người Hoàn");

        foreach (var order in revenueReport.Orders)
        {
            string orderDateVietnamTime = order.OrderDate.AddHours(7).ToString("dd/MM/yyyy HH:mm");
            string paidAtVietnamTime = order.PaidAt.HasValue ? order.PaidAt.Value.AddHours(7).ToString("dd/MM/yyyy HH:mm") : "-";
            string refundedAtVietnamTime = order.RefundedAt.HasValue ? order.RefundedAt.Value.AddHours(7).ToString("dd/MM/yyyy HH:mm") : "-";
            
            decimal netCollectedAmount = order.CollectedInPeriod - order.RefundedInPeriod;
            string paymentStatusText = order.IsRefunded ? "Đã hoàn tiền" : "Đã thanh toán";

            csvBuilder.AppendLine(string.Join(",",
                EscapeCsvField(order.OrderCode),
                EscapeCsvField(orderDateVietnamTime),
                EscapeCsvField(paidAtVietnamTime),
                EscapeCsvField(paymentStatusText),
                EscapeCsvField(order.CustomerName),
                EscapeCsvField(order.PaymentMethod),
                order.CollectedInPeriod.ToString("0", CultureInfo.InvariantCulture),
                order.RefundedInPeriod.ToString("0", CultureInfo.InvariantCulture),
                netCollectedAmount.ToString("0", CultureInfo.InvariantCulture),
                EscapeCsvField(refundedAtVietnamTime),
                EscapeCsvField(order.RefundReason ?? "-"),
                EscapeCsvField(order.PaidBy ?? "-"),
                EscapeCsvField(order.RefundedBy ?? "-")
            ));
        }

        return csvBuilder.ToString();
    }

    private static string EscapeCsvField(string? value)
    {
        if (string.IsNullOrEmpty(value)) return "\"\"";
        return $"\"{value.Replace("\"", "\"\"")}\"";
    }
}
