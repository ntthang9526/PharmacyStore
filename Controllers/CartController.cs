using Microsoft.AspNetCore.Mvc;
using PharmacyWeb.Services;

namespace PharmacyWeb.Controllers;

public class CartController : Controller
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var cart = await _cartService.GetCartAsync();
        return View(cart);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(int medicineId, int quantity = 1)
    {
        var success = await _cartService.AddToCartAsync(medicineId, quantity);
        if (!success)
        {
            TempData["ErrorMessage"] = "Không thể thêm sản phẩm vào giỏ (thuốc kê đơn, tạm hết hàng hoặc vượt quá số lượng có thể bán).";
        }
        else
        {
            TempData["SuccessMessage"] = "Đã thêm sản phẩm vào giỏ hàng!";
        }

        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            var cart = await _cartService.GetCartAsync();
            return Json(new { success, cartCount = cart.Items.Sum(i => i.Quantity), message = TempData["SuccessMessage"] ?? TempData["ErrorMessage"] });
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateQuantity(int medicineId, int quantity)
    {
        var success = await _cartService.UpdateQuantityAsync(medicineId, quantity);
        if (!success && quantity > 0)
        {
            TempData["ErrorMessage"] = "Số lượng yêu cầu vượt quá tồn kho có thể bán.";
        }

        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            var cart = await _cartService.GetCartAsync();
            return Json(new
            {
                success,
                cartCount = cart.Items.Sum(i => i.Quantity),
                subtotal = cart.SubtotalAmount,
                discount = cart.DiscountAmount,
                shipping = cart.ShippingFee,
                total = cart.TotalAmount,
                canCheckout = cart.CanCheckout
            });
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Remove(int medicineId)
    {
        await _cartService.RemoveFromCartAsync(medicineId);
        TempData["SuccessMessage"] = "Đã xóa sản phẩm khỏi giỏ hàng.";

        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            var cart = await _cartService.GetCartAsync();
            return Json(new
            {
                success = true,
                cartCount = cart.Items.Sum(i => i.Quantity),
                subtotal = cart.SubtotalAmount,
                discount = cart.DiscountAmount,
                shipping = cart.ShippingFee,
                total = cart.TotalAmount,
                canCheckout = cart.CanCheckout
            });
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ApplyVoucher(string voucherCode)
    {
        var res = await _cartService.ApplyVoucherAsync(voucherCode);
        if (res.Success)
        {
            TempData["SuccessMessage"] = res.Message;
        }
        else
        {
            TempData["ErrorMessage"] = res.Message;
        }

        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            var cart = await _cartService.GetCartAsync();
            return Json(new
            {
                success = res.Success,
                message = res.Message,
                discount = cart.DiscountAmount,
                total = cart.TotalAmount
            });
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult RemoveVoucher()
    {
        _cartService.RemoveVoucher();
        TempData["SuccessMessage"] = "Đã hủy áp dụng mã giảm giá.";
        return RedirectToAction(nameof(Index));
    }

    // REST API Endpoint: GET /api/cart
    [HttpGet("api/cart")]
    public async Task<IActionResult> ApiGetCart()
    {
        var cart = await _cartService.GetCartAsync();
        return Ok(new { success = true, data = cart });
    }
}
