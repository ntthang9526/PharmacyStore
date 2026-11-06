using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacyWeb.Data;
using PharmacyWeb.Data.Entities;
using PharmacyWeb.Models.ViewModels;

namespace PharmacyWeb.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class CustomersController : Controller
{
    private readonly PharmacyWebDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public CustomersController(PharmacyWebDbContext db, UserManager<ApplicationUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string? keyword, int page = 1)
    {
        page = Math.Max(1, page);
        var query = _db.CustomerProfiles
            .AsNoTracking()
            .Include(c => c.Addresses)
            .OrderByDescending(c => c.CreatedAt)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            var kw = keyword.Trim().ToLower();
            query = query.Where(c => c.FullName.ToLower().Contains(kw) || c.Phone.Contains(kw) || c.Email.ToLower().Contains(kw));
        }

        var total = await query.CountAsync();
        var items = await query.Skip((page - 1) * 10).Take(10).ToListAsync();

        ViewBag.Keyword = keyword;
        ViewBag.TotalItems = total;
        ViewBag.CurrentPage = page;

        return View(items);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var customer = await _db.CustomerProfiles
            .AsNoTracking()
            .Include(c => c.Addresses)
            .Include(c => c.Orders)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (customer == null) return NotFound();
        return View(customer);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleLock(int id)
    {
        var customer = await _db.CustomerProfiles.FindAsync(id);
        if (customer == null) return NotFound();

        customer.IsLocked = !customer.IsLocked;

        if (!string.IsNullOrEmpty(customer.UserId))
        {
            var user = await _userManager.FindByIdAsync(customer.UserId);
            if (user != null)
            {
                user.IsLocked = customer.IsLocked;
                await _userManager.UpdateAsync(user);
            }
        }

        await _db.SaveChangesAsync();
        TempData["SuccessMessage"] = $"Đã {(customer.IsLocked ? "khóa" : "mở khóa")} tài khoản của khách '{customer.FullName}'.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateWebAccount(CreateCustomerWebAccountViewModel model)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "Nhập email hợp lệ và mật khẩu ban đầu từ 8 ký tự.";
            return RedirectToAction(nameof(Details), new { id = model.CustomerId });
        }
        var customer = await _db.CustomerProfiles.FindAsync(model.CustomerId);
        if (customer == null) return NotFound();
        if (!string.IsNullOrEmpty(customer.UserId) || await _userManager.FindByNameAsync(customer.Phone) != null ||
            await _userManager.FindByEmailAsync(model.Email.Trim()) != null)
        {
            TempData["ErrorMessage"] = "Hồ sơ, số điện thoại hoặc email đã có tài khoản web.";
            return RedirectToAction(nameof(Details), new { id = customer.Id });
        }
        await using var transaction = await _db.Database.BeginTransactionAsync();
        var user = new ApplicationUser { UserName = customer.Phone, PhoneNumber = customer.Phone,
            Email = model.Email.Trim().ToLowerInvariant(), FullName = customer.FullName,
            CustomerProfileId = customer.Id, IsLocked = customer.IsLocked };
        var created = await _userManager.CreateAsync(user, model.InitialPassword);
        if (!created.Succeeded || !(await _userManager.AddToRoleAsync(user, "Customer")).Succeeded)
        {
            await transaction.RollbackAsync();
            TempData["ErrorMessage"] = "Chưa tạo được tài khoản. Kiểm tra email và độ mạnh mật khẩu.";
            return RedirectToAction(nameof(Details), new { id = customer.Id });
        }
        customer.UserId = user.Id;
        customer.Email = user.Email!;
        customer.WebAccountActive = true;
        await _db.SaveChangesAsync();
        await transaction.CommitAsync();
        TempData["SuccessMessage"] = "Đã tạo tài khoản web bằng số điện thoại của khách.";
        return RedirectToAction(nameof(Details), new { id = customer.Id });
    }
}
