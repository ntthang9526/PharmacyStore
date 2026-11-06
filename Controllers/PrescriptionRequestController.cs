using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PharmacyWeb.Data.Entities;
using PharmacyWeb.Models.ViewModels;
using PharmacyWeb.Services;

namespace PharmacyWeb.Controllers;

[Authorize]
public class PrescriptionRequestController : Controller
{
    private readonly IPrescriptionService _prescriptionService;
    private readonly UserManager<ApplicationUser> _userManager;

    public PrescriptionRequestController(
        IPrescriptionService prescriptionService,
        UserManager<ApplicationUser> userManager)
    {
        _prescriptionService = prescriptionService;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null || !user.CustomerProfileId.HasValue)
        {
            return RedirectToAction("Login", "Account");
        }

        var requests = await _prescriptionService.GetCustomerRequestsAsync(user.CustomerProfileId.Value);
        ViewBag.Requests = requests;

        var model = new PrescriptionUploadViewModel
        {
            PatientName = user.FullName,
            Phone = user.PhoneNumber ?? string.Empty
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upload(PrescriptionUploadViewModel model)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null || !user.CustomerProfileId.HasValue)
        {
            return RedirectToAction("Login", "Account");
        }

        if (!ModelState.IsValid)
        {
            ViewBag.Requests = await _prescriptionService.GetCustomerRequestsAsync(user.CustomerProfileId.Value);
            return View("Index", model);
        }

        var result = await _prescriptionService.CreateRequestAsync(user.CustomerProfileId.Value, model);
        if (result.Success)
        {
            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction(nameof(Index));
        }

        ModelState.AddModelError(string.Empty, result.Message);
        ViewBag.Requests = await _prescriptionService.GetCustomerRequestsAsync(user.CustomerProfileId.Value);
        return View("Index", model);
    }

    // Secure Image Download Endpoint (Private Medical Record)
    [HttpGet("api/prescriptions/images/{id:int}")]
    public async Task<IActionResult> GetImage(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        bool isAdmin = User.IsInRole("Admin");
        int? customerId = user?.CustomerProfileId;

        var (stream, contentType, fileName) = await _prescriptionService.GetSecureImageStreamAsync(id, customerId, isAdmin);
        if (stream == null)
        {
            return Forbid(); // 403 Forbidden if not authorized or not found
        }

        return File(stream, contentType, fileName);
    }

    // REST API Endpoint: GET /api/prescription-requests
    [HttpGet("api/prescription-requests")]
    public async Task<IActionResult> ApiGetRequests()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null || !user.CustomerProfileId.HasValue)
        {
            return Unauthorized();
        }

        var list = await _prescriptionService.GetCustomerRequestsAsync(user.CustomerProfileId.Value);
        return Ok(new { success = true, data = list });
    }

    // REST API Endpoint: POST /api/prescription-requests
    [HttpPost("api/prescription-requests")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ApiCreateRequest([FromForm] PrescriptionUploadViewModel model)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null || !user.CustomerProfileId.HasValue)
        {
            return Unauthorized();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(new { success = false, errors = ModelState });
        }

        var result = await _prescriptionService.CreateRequestAsync(user.CustomerProfileId.Value, model);
        if (!result.Success)
        {
            return BadRequest(new { success = false, message = result.Message });
        }

        return Ok(new { success = true, message = result.Message, requestId = result.RequestId, code = result.Code });
    }
}
