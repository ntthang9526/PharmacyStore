using Microsoft.AspNetCore.Mvc;
using PharmacyWeb.Models.ViewModels;
using PharmacyWeb.Services;

namespace PharmacyWeb.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] ProductFilterViewModel filter)
    {
        var result = await _productService.GetCatalogAsync(filter);
        var categories = await _productService.GetCategoriesWithCountAsync();

        filter.Results = result;
        filter.Categories = categories;

        // AJAX request returns partial view for seamless client-side filtering without reloading
        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            return PartialView("_ProductGridPartial", filter);
        }

        return View(filter);
    }

    [HttpGet("san-pham/{slug}")]
    [HttpGet("Products/Details/{id:int}")]
    public async Task<IActionResult> Details(string? slug, int? id)
    {
        ProductDetailViewModel? detail = null;
        if (!string.IsNullOrEmpty(slug))
        {
            detail = await _productService.GetDetailBySlugAsync(slug);
        }
        else if (id.HasValue)
        {
            detail = await _productService.GetDetailByIdAsync(id.Value);
        }

        if (detail == null)
        {
            return NotFound();
        }

        return View(detail);
    }

    // REST API Endpoint: GET /api/products
    [HttpGet("api/products")]
    public async Task<IActionResult> ApiGetProducts([FromQuery] ProductFilterViewModel filter)
    {
        var result = await _productService.GetCatalogAsync(filter);
        return Ok(new
        {
            success = true,
            data = result.Items,
            page = result.Page,
            pageSize = result.PageSize,
            totalItems = result.TotalItems,
            totalPages = result.TotalPages
        });
    }

    // REST API Endpoint: GET /api/products/{id}
    [HttpGet("api/products/{id:int}")]
    public async Task<IActionResult> ApiGetProductById(int id)
    {
        var item = await _productService.GetDetailByIdAsync(id);
        if (item == null)
        {
            return NotFound(new { success = false, message = "Không tìm thấy sản phẩm" });
        }
        return Ok(new { success = true, data = item });
    }

    // REST API Endpoint: GET /api/categories
    [HttpGet("api/categories")]
    public async Task<IActionResult> ApiGetCategories()
    {
        var categories = await _productService.GetCategoriesWithCountAsync();
        return Ok(new { success = true, data = categories });
    }
}
