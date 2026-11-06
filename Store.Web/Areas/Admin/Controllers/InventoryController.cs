using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PharmacyWeb.Models.ViewModels;
using PharmacyWeb.Services;

namespace PharmacyWeb.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class InventoryController : Controller
{
    private readonly IInventoryService _inventoryService;
    private readonly IProductService _productService;

    public InventoryController(IInventoryService inventoryService, IProductService productService)
    {
        _inventoryService = inventoryService;
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string? filterType, int? medicineId, int page = 1)
    {
        page = Math.Max(1, page);
        var batches = await _inventoryService.GetBatchesAsync(filterType, medicineId, page, 15);
        ViewBag.FilterType = filterType;
        ViewBag.Medicines = await _productService.GetCatalogAsync(new ProductFilterViewModel { Page = 1, PageSize = 1000 });
        return View(batches);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleLock(int batchId)
    {
        var success = await _inventoryService.ToggleBatchLockAsync(batchId);
        if (success)
        {
            TempData["SuccessMessage"] = "Đã thay đổi trạng thái khóa lô hàng.";
        }
        else
        {
            TempData["ErrorMessage"] = "Không thể cập nhật lô hàng.";
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> StockIn()
    {
        await PopulateStockInLookupsAsync();
        return View(new StockInFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> StockIn(StockInFormViewModel model)
    {
        if (!ModelState.IsValid || model.Lines == null || !model.Lines.Any())
        {
            TempData["ErrorMessage"] = "Vui lòng nhập đầy đủ nhà cung cấp và các dòng thuốc nhập kho.";
            await PopulateStockInLookupsAsync(model.SupplierId);
            return View(model);
        }

        var res = await _inventoryService.CreateStockInAsync(model, User.Identity?.Name ?? "Admin");
        if (res.Success)
        {
            TempData["SuccessMessage"] = res.Message;
            return RedirectToAction(nameof(StockInHistory));
        }

        TempData["ErrorMessage"] = res.Message;
        await PopulateStockInLookupsAsync(model.SupplierId);
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> StockInHistory(int page = 1)
    {
        page = Math.Max(1, page);
        var result = await _inventoryService.GetStockInsAsync(page, 10);
        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> StockInDetails(int id)
    {
        var stockIn = await _inventoryService.GetStockInDetailsAsync(id);
        if (stockIn == null) return NotFound();
        return View(stockIn);
    }

    private async Task PopulateStockInLookupsAsync(int? selectedSupplierId = null)
    {
        var suppliers = await _productService.GetAllSuppliersAsync();
        ViewBag.Suppliers = new SelectList(suppliers, "Id", "Name", selectedSupplierId);
        ViewBag.Medicines = await _productService.GetCatalogAsync(new ProductFilterViewModel { Page = 1, PageSize = 1000 });
    }
}
