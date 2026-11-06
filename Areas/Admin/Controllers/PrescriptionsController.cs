using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyWeb.Services;

namespace PharmacyWeb.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class PrescriptionsController : Controller
{
    private readonly IPrescriptionService _prescriptionService;

    public PrescriptionsController(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string? status, int page = 1)
    {
        var requests = await _prescriptionService.GetAdminRequestsAsync(status, page, 10);
        ViewBag.Status = status;
        return View(requests);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var request = await _prescriptionService.GetRequestByIdAsync(id, null, isAdmin: true);
        if (request == null) return NotFound();
        return View(request);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Respond(int id, string pharmacistNote, string newStatus)
    {
        if (string.IsNullOrWhiteSpace(pharmacistNote))
        {
            TempData["ErrorMessage"] = "Vui lòng nhập nội dung phản hồi tư vấn cho khách.";
            return RedirectToAction(nameof(Details), new { id });
        }

        var res = await _prescriptionService.RespondRequestAsync(id, pharmacistNote, newStatus);
        if (res.Success)
        {
            TempData["SuccessMessage"] = res.Message;
        }
        else
        {
            TempData["ErrorMessage"] = res.Message;
        }
        return RedirectToAction(nameof(Details), new { id });
    }
}
