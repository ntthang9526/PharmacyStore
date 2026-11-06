using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyWeb.Services;

namespace PharmacyWeb.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class OrdersController : Controller
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string? status, string? paymentStatus, string? keyword, int page = 1)
    {
        page = Math.Max(1, page);
        var orders = await _orderService.GetAdminOrdersAsync(status, paymentStatus, keyword, page, 12);
        ViewBag.Status = status;
        ViewBag.PaymentStatus = paymentStatus;
        ViewBag.Keyword = keyword;
        return View(orders);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var order = await _orderService.GetOrderDetailsForAdminAsync(id);
        if (order == null) return NotFound();
        return View(order);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateStatus(int id, string newStatus, string? cancelReason)
    {
        var employee = User.Identity?.Name ?? "admin";
        var res = await _orderService.UpdateOrderStatusByAdminAsync(id, newStatus, cancelReason, employee);
        if (res.Success)
        {
            TempData["SuccessMessage"] = res.Message;
        }
        else
        {
            TempData["ErrorMessage"] = res.Message;
        }
        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ConfirmPayment(int id)
    {
        var employee = User.Identity?.Name ?? "admin";
        var res = await _orderService.ConfirmCodPaymentAsync(id, employee);
        if (res.Success)
        {
            TempData["SuccessMessage"] = res.Message;
        }
        else
        {
            TempData["ErrorMessage"] = res.Message;
        }
        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Refund(int id, string refundReason)
    {
        var employee = User.Identity?.Name ?? "admin";
        var res = await _orderService.RefundOrderAsync(id, refundReason, employee);
        if (res.Success)
        {
            TempData["SuccessMessage"] = res.Message;
        }
        else
        {
            TempData["ErrorMessage"] = res.Message;
        }
        return RedirectToAction(nameof(Details), new { id });
    }
}
