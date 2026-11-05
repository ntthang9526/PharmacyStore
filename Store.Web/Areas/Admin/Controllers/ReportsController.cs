using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyWeb.Services;

namespace PharmacyWeb.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ReportsController : Controller
{
    private readonly IReportService _reportService;

    public ReportsController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(DateTime? fromDate, DateTime? toDate)
    {
        var vm = await _reportService.GetRevenueReportAsync(fromDate, toDate);
        return View(vm);
    }

    [HttpGet]
    public async Task<IActionResult> ExportCsv(DateTime? fromDate, DateTime? toDate)
    {
        var csv = await _reportService.GenerateRevenueCsvAsync(fromDate, toDate);
        var bytes = Encoding.UTF8.GetPreamble().Concat(Encoding.UTF8.GetBytes(csv)).ToArray();
        var fileName = $"BaoCaoDoanhThu_{DateTime.UtcNow:yyyyMMdd_HHmmss}.csv";
        return File(bytes, "text/csv; charset=utf-8", fileName);
    }
}
