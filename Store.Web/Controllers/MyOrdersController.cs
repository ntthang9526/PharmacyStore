using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PharmacyWeb.Data.Entities;
using PharmacyWeb.Services;

namespace PharmacyWeb.Controllers;

[Authorize]
public class MyOrdersController : Controller
{
    private readonly IOrderService _orderService;
    private readonly UserManager<ApplicationUser> _userManager;

    public MyOrdersController(IOrderService orderService, UserManager<ApplicationUser> userManager)
    {
        _orderService = orderService;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int page = 1)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null || !user.CustomerProfileId.HasValue)
        {
            return RedirectToAction("Login", "Account");
        }

        var result = await _orderService.GetCustomerOrdersAsync(user.CustomerProfileId.Value, page, 10);
        return View(result);
    }

    [HttpGet("don-hang/{id:int}")]
    public async Task<IActionResult> Details(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null || !user.CustomerProfileId.HasValue)
        {
            return RedirectToAction("Login", "Account");
        }

        var result = await _orderService.GetOrderResultAsync(id, user.CustomerProfileId.Value, isAdmin: User.IsInRole("Admin"));
        if (result == null)
        {
            return NotFound();
        }

        return View(result);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancel(int id, string reason)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null || !user.CustomerProfileId.HasValue)
        {
            return RedirectToAction("Login", "Account");
        }

        var res = await _orderService.CustomerCancelOrderAsync(id, user.CustomerProfileId.Value, reason ?? "Khách hàng đổi ý");
        if (res.Success)
        {
            TempData["SuccessMessage"] = res.Message;
        }
        else
        {
            TempData["ErrorMessage"] = res.Message;
        }

        return RedirectToAction(nameof(Index));
    }

    // REST API Endpoint: GET /api/me/orders
    [HttpGet("api/me/orders")]
    public async Task<IActionResult> ApiGetMyOrders(int page = 1, int pageSize = 10)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null || !user.CustomerProfileId.HasValue)
        {
            return Unauthorized();
        }

        var orders = await _orderService.GetCustomerOrdersAsync(user.CustomerProfileId.Value, page, pageSize);
        return Ok(new { success = true, data = orders });
    }

    // REST API Endpoint: POST /api/me/orders/{id}/cancel
    [HttpPost("api/me/orders/{id:int}/cancel")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ApiCancelOrder(int id, [FromBody] CancelRequestDto dto)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null || !user.CustomerProfileId.HasValue)
        {
            return Unauthorized();
        }

        var res = await _orderService.CustomerCancelOrderAsync(id, user.CustomerProfileId.Value, dto.Reason ?? "Khách hàng hủy đơn qua API");
        if (!res.Success)
        {
            return BadRequest(new { success = false, message = res.Message });
        }

        return Ok(new { success = true, message = res.Message });
    }
}

public class CancelRequestDto
{
    public string? Reason { get; set; }
}
