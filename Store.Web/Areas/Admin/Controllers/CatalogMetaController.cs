using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyWeb.Data.Entities;
using PharmacyWeb.Services;

namespace PharmacyWeb.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class CatalogMetaController : Controller
{
    private readonly IProductService _productService;

    public CatalogMetaController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        ViewBag.Categories = await _productService.GetAllCategoriesAsync();
        ViewBag.Suppliers = await _productService.GetAllSuppliersAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SaveCategory(Category category)
    {
        if (string.IsNullOrWhiteSpace(category.Name))
        {
            TempData["ErrorMessage"] = "Tên nhóm thuốc không được để trống.";
            return RedirectToAction(nameof(Index));
        }

        await _productService.SaveCategoryAsync(category);
        TempData["SuccessMessage"] = "Đã lưu thông tin nhóm thuốc!";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var res = await _productService.DeleteCategoryAsync(id);
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SaveSupplier(Supplier supplier)
    {
        if (string.IsNullOrWhiteSpace(supplier.Name))
        {
            TempData["ErrorMessage"] = "Tên nhà cung cấp không được để trống.";
            return RedirectToAction(nameof(Index));
        }

        await _productService.SaveSupplierAsync(supplier);
        TempData["SuccessMessage"] = "Đã lưu thông tin nhà cung cấp!";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteSupplier(int id)
    {
        var res = await _productService.DeleteSupplierAsync(id);
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
}
