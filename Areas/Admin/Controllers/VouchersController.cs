using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacyWeb.Data;
using PharmacyWeb.Data.Entities;
using PharmacyWeb.Models.ViewModels;

namespace PharmacyWeb.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class VouchersController : Controller
{
    private readonly PharmacyWebDbContext _db;

    public VouchersController(PharmacyWebDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var vouchers = await _db.Vouchers.AsNoTracking().OrderBy(v => v.Code).ToListAsync();
        return View(vouchers);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new VoucherFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(VoucherFormViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var cleanCode = model.Code.Trim().ToUpperInvariant();
        if (await _db.Vouchers.AnyAsync(v => v.Code.ToUpper() == cleanCode))
        {
            ModelState.AddModelError("Code", "Mã giảm giá này đã tồn tại.");
            return View(model);
        }

        var voucher = new Voucher
        {
            Code = cleanCode,
            DiscountType = model.DiscountType,
            DiscountValue = model.DiscountValue,
            MinOrderValue = model.MinOrderValue,
            MaxDiscountCap = model.MaxDiscountCap,
            Quantity = model.Quantity,
            IsActive = model.IsActive,
            Description = model.Description?.Trim()
        };

        _db.Vouchers.Add(voucher);
        await _db.SaveChangesAsync();

        TempData["SuccessMessage"] = $"Thêm mã giảm giá {voucher.Code} thành công!";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var v = await _db.Vouchers.FindAsync(id);
        if (v == null) return NotFound();

        var model = new VoucherFormViewModel
        {
            Id = v.Id,
            Code = v.Code,
            DiscountType = v.DiscountType,
            DiscountValue = v.DiscountValue,
            MinOrderValue = v.MinOrderValue,
            MaxDiscountCap = v.MaxDiscountCap,
            Quantity = v.Quantity,
            IsActive = v.IsActive,
            Description = v.Description
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(VoucherFormViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var v = await _db.Vouchers.FindAsync(model.Id);
        if (v == null) return NotFound();

        v.DiscountType = model.DiscountType;
        v.DiscountValue = model.DiscountValue;
        v.MinOrderValue = model.MinOrderValue;
        v.MaxDiscountCap = model.MaxDiscountCap;
        v.Quantity = model.Quantity;
        v.IsActive = model.IsActive;
        v.Description = model.Description?.Trim();

        await _db.SaveChangesAsync();
        TempData["SuccessMessage"] = $"Cập nhật mã giảm giá {v.Code} thành công!";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleActive(int id)
    {
        var v = await _db.Vouchers.FindAsync(id);
        if (v == null) return NotFound();

        v.IsActive = !v.IsActive;
        await _db.SaveChangesAsync();

        TempData["SuccessMessage"] = $"Đã {(v.IsActive ? "kích hoạt" : "tạm khóa")} mã {v.Code}.";
        return RedirectToAction(nameof(Index));
    }
}
