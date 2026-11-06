using Microsoft.AspNetCore.Mvc;
using PharmacyWeb.Services;

namespace PharmacyWeb.Controllers.Api;

/// <summary>GET api/cart/count - số lượng sản phẩm trong giỏ (lưu trong Session).</summary>
[ApiController]
[Route("api/cart")]
[Produces("application/json")]
public class CartApiController : ControllerBase
{
    private readonly ICartService _cart;

    public CartApiController(ICartService cart)
    {
        _cart = cart;
    }

    [HttpGet("count")]
    public async Task<IActionResult> Count()
    {
        var cart = await _cart.GetCartAsync();
        return Ok(new { lines = cart.Items.Count, total = cart.TotalAmount });
    }
}
