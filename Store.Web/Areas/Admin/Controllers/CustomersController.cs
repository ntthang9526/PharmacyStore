using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacyWeb.Data;
using PharmacyWeb.Data.Entities;

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
}
