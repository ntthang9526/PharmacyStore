using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyWeb.Models.Api;
using PharmacyWeb.Models.ViewModels;
using PharmacyWeb.Services;

namespace PharmacyWeb.Controllers.Api;

/// <summary>
/// Web API theo mô hình RESTful cho tài nguyên "thuốc".
/// GET công khai; POST / PUT / DELETE chỉ dành cho vai trò Admin.
/// </summary>
[ApiController]
[Route("api/products")]
[Produces("application/json")]
public class ProductsApiController : ControllerBase
{
    private readonly IProductService _products;

    public ProductsApiController(IProductService products)
    {
        _products = products;
    }

    /// <summary>GET api/products?keyword=&amp;categoryId=&amp;minPrice=&amp;maxPrice=&amp;sortBy=&amp;page=&amp;pageSize=</summary>
    [HttpGet]
    public async Task<ActionResult<PagedApiResult<ProductCardViewModel>>> GetAll([FromQuery] ProductQueryDto query)
    {
        var filter = new ProductFilterViewModel
        {
            Keyword = query.Keyword,
            CategoryId = query.CategoryId,
            MinPrice = query.MinPrice,
            MaxPrice = query.MaxPrice,
            OnlyInStock = query.OnlyInStock,
            SortBy = string.IsNullOrWhiteSpace(query.SortBy) ? "default" : query.SortBy,
            Page = Math.Max(1, query.Page),
            PageSize = Math.Clamp(query.PageSize, 1, 50)
        };

        var result = await _products.GetCatalogAsync(filter);

        return Ok(new PagedApiResult<ProductCardViewModel>
        {
            Items = result.Items,
            Page = result.Page,
            PageSize = result.PageSize,
            TotalItems = result.TotalItems,
            TotalPages = result.TotalPages
        });
    }

    /// <summary>GET api/products/5</summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductDetailViewModel>> GetById(int id)
    {
        var detail = await _products.GetDetailByIdAsync(id);
        if (detail == null) return NotFound();
        return Ok(detail);
    }

    /// <summary>POST api/products (Admin)</summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] ProductWriteDto dto)
    {
        var invalid = await ValidateReferencesAsync(dto);
        if (invalid != null) return invalid;

        var newId = await _products.CreateProductAsync(ToForm(dto, 0));
        return CreatedAtAction(nameof(GetById), new { id = newId }, new { id = newId });
    }

    /// <summary>PUT api/products/5 (Admin)</summary>
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] ProductWriteDto dto)
    {
        var invalid = await ValidateReferencesAsync(dto);
        if (invalid != null) return invalid;

        var ok = await _products.UpdateProductAsync(ToForm(dto, id));
        return ok ? NoContent() : NotFound();
    }

    /// <summary>
    /// DELETE api/products/5 (Admin). Xóa mềm: ẩn thuốc khỏi website vì thuốc đã có thể
    /// được tham chiếu bởi lô hàng và đơn hàng (giữ toàn vẹn dữ liệu).
    /// </summary>
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _products.GetEntityByIdAsync(id);
        if (entity == null) return NotFound();

        if (entity.IsActive)
        {
            await _products.ToggleActiveAsync(id);
        }
        return NoContent();
    }

    private async Task<IActionResult?> ValidateReferencesAsync(ProductWriteDto dto)
    {
        var categories = await _products.GetAllCategoriesAsync();
        if (!categories.Any(c => c.Id == dto.CategoryId))
            ModelState.AddModelError(nameof(dto.CategoryId), "Danh mục không tồn tại");

        var suppliers = await _products.GetAllSuppliersAsync();
        if (!suppliers.Any(s => s.Id == dto.SupplierId))
            ModelState.AddModelError(nameof(dto.SupplierId), "Nhà cung cấp không tồn tại");

        return ModelState.IsValid ? null : ValidationProblem(ModelState);
    }

    private static AdminProductFormViewModel ToForm(ProductWriteDto dto, int id) => new()
    {
        Id = id,
        CategoryId = dto.CategoryId,
        SupplierId = dto.SupplierId,
        BarCode = dto.BarCode,
        Name = dto.Name,
        ActiveIngredient = dto.ActiveIngredient,
        Unit = dto.Unit,
        PackagingSpec = dto.PackagingSpec,
        Price = dto.Price,
        IsPrescription = dto.IsPrescription,
        IsSelling = dto.IsSelling,
        IsActive = dto.IsActive,
        Featured = dto.Featured,
        DescriptionHtml = dto.DescriptionHtml
    };
}
