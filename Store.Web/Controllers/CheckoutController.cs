using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PharmacyWeb.Data;
using PharmacyWeb.Data.Entities;
using PharmacyWeb.Models;
using PharmacyWeb.Models.ViewModels;
using PharmacyWeb.Services;

namespace PharmacyWeb.Controllers;

[Authorize]
public class CheckoutController : Controller
{
    private readonly ICartService _cartService;
    private readonly IOrderService _orderService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly PharmacyWebDbContext _db;
    private readonly BankTransferOptions _bankOptions;

    public CheckoutController(
        ICartService cartService,
        IOrderService orderService,
        UserManager<ApplicationUser> userManager,
        PharmacyWebDbContext db,
        IOptions<BankTransferOptions>? bankOptions = null)
    {
        _cartService = cartService;
        _orderService = orderService;
        _userManager = userManager;
        _db = db;
        _bankOptions = bankOptions?.Value ?? new BankTransferOptions();
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var cart = await _cartService.GetCartAsync();
        if (!cart.Items.Any())
        {
            TempData["ErrorMessage"] = "Giỏ hàng của bạn đang trống.";
            return RedirectToAction("Index", "Cart");
        }

        if (!cart.CanCheckout)
        {
            TempData["ErrorMessage"] = "Một số sản phẩm trong giỏ đã hết hàng hoặc là thuốc kê đơn. Vui lòng kiểm tra lại giỏ hàng.";
            return RedirectToAction("Index", "Cart");
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null || !user.CustomerProfileId.HasValue)
        {
            return RedirectToAction("Login", "Account", new { returnUrl = "/Checkout" });
        }

        var profile = await _db.CustomerProfiles
            .Include(cp => cp.Addresses)
            .FirstOrDefaultAsync(cp => cp.Id == user.CustomerProfileId.Value);

        var defaultAddress = profile?.Addresses.FirstOrDefault(a => a.IsDefault) 
                             ?? profile?.Addresses.FirstOrDefault();

        var model = new CheckoutFormViewModel
        {
            RecipientName = defaultAddress?.RecipientName ?? profile?.FullName ?? user.FullName,
            Phone = defaultAddress?.Phone ?? profile?.Phone ?? user.PhoneNumber ?? string.Empty,
            Address = defaultAddress?.Address ?? string.Empty,
            PaymentMethod = "COD",
            VoucherCode = cart.AppliedVoucherCode,
            IdempotencyKey = Guid.NewGuid().ToString("N"),
            Cart = cart,
            BankTransferInfo = GetBankTransferInfo()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> PlaceOrder(CheckoutFormViewModel model)
    {
        var cart = await _cartService.GetCartAsync();
        model.Cart = cart;
        model.BankTransferInfo = GetBankTransferInfo();

        if (model.PaymentMethod == "BANK_TRANSFER" && (!_bankOptions.IsEnabled || string.IsNullOrWhiteSpace(_bankOptions.BankName) || string.IsNullOrWhiteSpace(_bankOptions.AccountNumber)))
        {
            ModelState.AddModelError(string.Empty, "Phương thức chuyển khoản ngân hàng hiện chưa sẵn sàng do chưa cấu hình tài khoản nhận tiền. Quý khách vui lòng chọn thanh toán khi nhận hàng (COD).");
            return View("Index", model);
        }

        if (!ModelState.IsValid)
        {
            return View("Index", model);
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null || !user.CustomerProfileId.HasValue)
        {
            return RedirectToAction("Login", "Account", new { returnUrl = "/Checkout" });
        }

        var result = await _orderService.CheckoutAsync(user.CustomerProfileId.Value, model);
        if (!result.Success || !result.OrderId.HasValue)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View("Index", model);
        }

        TempData["SuccessMessage"] = result.Message;
        return RedirectToAction(nameof(OrderResult), new { id = result.OrderId.Value });
    }

    [HttpGet("Checkout/OrderResult/{id:int}")]
    public async Task<IActionResult> OrderResult(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null || !user.CustomerProfileId.HasValue)
        {
            return RedirectToAction("Login", "Account");
        }

        var order = await _orderService.GetOrderResultAsync(id, user.CustomerProfileId.Value, isAdmin: User.IsInRole("Admin"));
        if (order == null)
        {
            return NotFound();
        }

        order.BankTransferInfo ??= GetBankTransferInfo();
        return View(order);
    }

    // REST API Endpoint: POST /api/orders
    [HttpPost("api/orders")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ApiCreateOrder([FromBody] CheckoutFormViewModel model)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null || !user.CustomerProfileId.HasValue)
        {
            return Unauthorized(new { success = false, message = "Vui lòng đăng nhập để đặt hàng." });
        }

        if (model.PaymentMethod == "BANK_TRANSFER" && (!_bankOptions.IsEnabled || string.IsNullOrWhiteSpace(_bankOptions.BankName) || string.IsNullOrWhiteSpace(_bankOptions.AccountNumber)))
        {
            return BadRequest(new { success = false, message = "Phương thức chuyển khoản ngân hàng hiện chưa sẵn sàng do chưa cấu hình tài khoản nhận tiền." });
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(new { success = false, message = "Dữ liệu đặt hàng không hợp lệ.", errors = ModelState });
        }

        var result = await _orderService.CheckoutAsync(user.CustomerProfileId.Value, model);
        if (!result.Success)
        {
            return Conflict(new { success = false, message = result.Message });
        }

        return CreatedAtAction(nameof(OrderResult), new { id = result.OrderId }, new
        {
            success = true,
            message = result.Message,
            orderId = result.OrderId,
            orderCode = result.OrderCode
        });
    }

    private BankTransferInfoViewModel GetBankTransferInfo()
    {
        return new BankTransferInfoViewModel
        {
            IsEnabled = _bankOptions.IsEnabled,
            IsDemoAccount = _bankOptions.IsDemoAccount,
            BankName = _bankOptions.BankName,
            AccountNumber = _bankOptions.AccountNumber,
            AccountHolder = _bankOptions.AccountHolder,
            TransferInstruction = _bankOptions.TransferInstruction,
            DemoNotice = _bankOptions.DemoNotice
        };
    }
}
