using System.Diagnostics;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using PharmacyWeb.Models;
using PharmacyWeb.Models.ViewModels;
using PharmacyWeb.Services;

namespace PharmacyWeb.Controllers;

public class HomeController : Controller
{
    private readonly IProductService _productService;

    public HomeController(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
        var allMedsResult = await _productService.GetCatalogAsync(new ProductFilterViewModel { Page = 1, PageSize = 24 });
        var allMeds = allMedsResult.Items;

        var featuredMeds = allMeds.Where(m => !m.IsPrescription).Take(4).ToList();
        var rxMeds = allMeds.Where(m => m.IsPrescription).Take(4).ToList();
        var healthcareMeds = allMeds.Where(m => !m.IsPrescription).Skip(4).Take(4).ToList();

        var categories = await _productService.GetCategoriesWithCountAsync();

        ViewBag.Categories = categories;
        ViewBag.FeaturedProducts = featuredMeds;
        ViewBag.RxProducts = rxMeds;
        ViewBag.HealthcareProducts = healthcareMeds;

        return View();
    }

    public IActionResult Policies()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }

    [HttpGet("Home/AccessDenied")]
    [HttpGet("403")]
    public IActionResult AccessDenied()
    {
        Response.StatusCode = 403;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SetLanguage(string culture, string returnUrl)
    {
        Response.Cookies.Append(
            "pharmacy_lang",
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1), SameSite = SameSiteMode.Lax, HttpOnly = false }
        );

        if (Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }
        return RedirectToAction(nameof(Index));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
