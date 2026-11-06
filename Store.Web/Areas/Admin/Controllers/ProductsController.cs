using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PharmacyWeb.Models.ViewModels;
using PharmacyWeb.Services;

namespace PharmacyWeb.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
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
        filter.Page = Math.Max(1, filter.Page);
        // Admin can see both Active and Inactive medicines, and filter by IsActive if specified
        var result = await _productService.GetAdminCatalogAsync(filter);
        var categories = await _productService.GetCategoriesWithCountAsync();

        filter.Results = result;
        filter.Categories = categories;

        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            return PartialView("_AdminProductTablePartial", filter);
        }

        return View(filter);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await PopulateDropdownsAsync();
        return View(new AdminProductFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AdminProductFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await PopulateDropdownsAsync();
            return View(model);
        }

        var id = await _productService.CreateProductAsync(model);
        TempData["SuccessMessage"] = $"Thêm thuốc '{model.Name}' thành công!";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var med = await _productService.GetEntityByIdAsync(id);
        if (med == null) return NotFound();

        await PopulateDropdownsAsync();

        var model = new AdminProductFormViewModel
        {
            Id = med.Id,
            CategoryId = med.CategoryId,
            SupplierId = med.SupplierId,
            BarCode = med.BarCode,
            Name = med.Name,
            ActiveIngredient = med.ActiveIngredient,
            Unit = med.Unit,
            PackagingSpec = med.PackagingSpec,
            Price = med.Price,
            IsPrescription = med.IsPrescription,
            IsSelling = med.IsSelling,
            IsActive = med.IsActive,
            Featured = med.Featured,
            DescriptionHtml = med.DescriptionHtml,
            ExistingMainImageUrl = med.MainImageUrl,
            ExistingImages = med.Images.Select(i => i.ImageUrl).ToList(),
            RowVersion = med.RowVersion
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(AdminProductFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await PopulateDropdownsAsync();
            return View(model);
        }

        try
        {
            var success = await _productService.UpdateProductAsync(model);
            if (!success)
            {
                TempData["ErrorMessage"] = "Không thể cập nhật sản phẩm. Vui lòng thử lại.";
                await PopulateDropdownsAsync();
                return View(model);
            }
        }
        catch (DbUpdateConcurrencyException)
        {
            ModelState.AddModelError(string.Empty, "Dữ liệu thuốc đã được cập nhật từ một phiên làm việc khác (POS hoặc quản trị viên khác). Vui lòng tải lại trang để cập nhật thông tin mới nhất.");
            await PopulateDropdownsAsync();
            return View(model);
        }

        TempData["SuccessMessage"] = $"Cập nhật thuốc '{model.Name}' thành công!";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleActive(int id)
    {
        await _productService.ToggleActiveAsync(id);
        TempData["SuccessMessage"] = "Đã thay đổi trạng thái hiển thị của sản phẩm.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleSelling(int id)
    {
        await _productService.ToggleSellingAsync(id);
        TempData["SuccessMessage"] = "Đã thay đổi trạng thái bán của sản phẩm.";
        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateDropdownsAsync()
    {
        var categories = await _productService.GetAllCategoriesAsync();
        var suppliers = await _productService.GetAllSuppliersAsync();

        ViewBag.CategoryOptions = new SelectList(categories, "Id", "Name");
        ViewBag.SupplierOptions = new SelectList(suppliers, "Id", "Name");
    }
}
