using Microsoft.AspNetCore.Mvc;
using PharmacyWeb.Models.ViewModels;
using PharmacyWeb.Services;

namespace PharmacyWeb.Controllers.Api;

/// <summary>GET api/categories - danh mục kèm số lượng thuốc.</summary>
[ApiController]
[Route("api/categories")]
[Produces("application/json")]
public class CategoriesApiController : ControllerBase
{
    private readonly IProductService _products;

    public CategoriesApiController(IProductService products)
    {
        _products = products;
    }

    [HttpGet]
    public async Task<ActionResult<List<CategoryItemViewModel>>> GetAll()
    {
        return Ok(await _products.GetCategoriesWithCountAsync());
    }
}
