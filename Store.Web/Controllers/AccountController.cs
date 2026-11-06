using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacyWeb.Data;
using PharmacyWeb.Data.Entities;
using PharmacyWeb.Models.ViewModels;

namespace PharmacyWeb.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly PharmacyWebDbContext _db;

    public AccountController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        PharmacyWebDbContext db)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _db = db;
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToLocal(returnUrl);
        }
        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var input = model.UsernameOrEmail.Trim();
        var user = await _userManager.FindByEmailAsync(input)
                   ?? await _userManager.FindByNameAsync(input);

        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Số điện thoại, email hoặc mật khẩu không chính xác.");
            return View(model);
        }

        if (user.IsLocked)
        {
            ModelState.AddModelError(string.Empty, "Tài khoản của bạn đã bị khóa. Vui lòng liên hệ nhà thuốc.");
            return View(model);
        }

        var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: true);

        if (result.Succeeded)
        {
            if (await _userManager.IsInRoleAsync(user, "Admin") && string.IsNullOrEmpty(model.ReturnUrl))
            {
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            }
            return RedirectToLocal(model.ReturnUrl);
        }

        if (result.IsLockedOut)
        {
            ModelState.AddModelError(string.Empty, "Tài khoản tạm thời bị khóa do nhập sai mật khẩu nhiều lần. Vui lòng thử lại sau 15 phút.");
            return View(model);
        }

        ModelState.AddModelError(string.Empty, "Số điện thoại, email hoặc mật khẩu không chính xác.");
        return View(model);
    }

    [HttpGet]
    public IActionResult Register()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Home");
        }
        return View(new RegisterViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var phone = model.Phone.Trim();
        var email = model.Email.Trim().ToLowerInvariant();

        if (await _userManager.FindByNameAsync(phone) != null)
        {
            ModelState.AddModelError("Phone", "Số điện thoại này đã được đăng ký tài khoản.");
            return View(model);
        }

        if (await _userManager.FindByEmailAsync(email) != null)
        {
            ModelState.AddModelError("Email", "Email này đã được sử dụng.");
            return View(model);
        }

        // Không tự chiếm hồ sơ POS chỉ bằng một số điện thoại chưa xác minh.
        if (await _db.CustomerProfiles.AnyAsync(c => c.Phone == phone))
        {
            ModelState.AddModelError("Phone", "Số điện thoại đã có hồ sơ tại nhà thuốc. Vui lòng liên hệ quản trị viên để liên kết tài khoản web.");
            return View(model);
        }

        await using var registrationTransaction = await _db.Database.BeginTransactionAsync();
        // 1. Create CustomerProfile
        var customerProfile = new CustomerProfile
        {
            FullName = model.FullName.Trim(),
            Phone = phone,
            Email = email,
            Points = 0,
            WebAccountActive = true,
            IsLocked = false,
            CreatedAt = DateTime.UtcNow,
            Addresses = new List<CustomerAddress>
            {
                new()
                {
                    Label = "Nhà riêng",
                    RecipientName = model.FullName.Trim(),
                    Phone = phone,
                    Address = model.Address.Trim(),
                    IsDefault = true
                }
            }
        };

        _db.CustomerProfiles.Add(customerProfile);
        await _db.SaveChangesAsync();

        // 2. Create ApplicationUser strictly with Customer role (No overposting of Admin)
        var user = new ApplicationUser
        {
            UserName = phone,
            Email = email,
            PhoneNumber = phone,
            FullName = model.FullName.Trim(),
            EmailConfirmed = true,
            CustomerProfileId = customerProfile.Id,
            CreatedAt = DateTime.UtcNow
        };

        var createResult = await _userManager.CreateAsync(user, model.Password);
        if (createResult.Succeeded)
        {
            var roleResult = await _userManager.AddToRoleAsync(user, "Customer");
            if (!roleResult.Succeeded)
            {
                await registrationTransaction.RollbackAsync();
                ModelState.AddModelError(string.Empty, "Chưa tạo được quyền tài khoản. Vui lòng thử lại.");
                return View(model);
            }

            customerProfile.UserId = user.Id;
            await _db.SaveChangesAsync();

            await registrationTransaction.CommitAsync();
            await _signInManager.SignInAsync(user, isPersistent: false);
            TempData["SuccessMessage"] = "Đăng ký tài khoản thành công!";
            return RedirectToAction("Index", "Home");
        }

        await registrationTransaction.RollbackAsync();
        foreach (var error in createResult.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Challenge();

        var profile = await _db.CustomerProfiles
            .AsNoTracking()
            .Include(cp => cp.Addresses)
            .FirstOrDefaultAsync(cp => cp.Id == user.CustomerProfileId);

        var vm = new CustomerProfileViewModel
        {
            FullName = user.FullName,
            Phone = user.PhoneNumber ?? user.UserName ?? string.Empty,
            Email = user.Email ?? string.Empty,
            Points = profile?.Points ?? 0,
            CreatedAt = user.CreatedAt,
            Addresses = profile?.Addresses.Select(a => new AddressItemViewModel
            {
                Id = a.Id,
                Label = a.Label,
                RecipientName = a.RecipientName,
                Phone = a.Phone,
                Address = a.Address,
                IsDefault = a.IsDefault
            }).ToList() ?? new()
        };

        return View(vm);
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword([Bind(Prefix = "ChangePassword")] ChangePasswordViewModel model)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "Dữ liệu đổi mật khẩu không hợp lệ.";
            return RedirectToAction(nameof(Profile));
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Challenge();

        var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        if (result.Succeeded)
        {
            await _signInManager.RefreshSignInAsync(user);
            TempData["SuccessMessage"] = "Đổi mật khẩu thành công!";
        }
        else
        {
            TempData["ErrorMessage"] = string.Join("; ", result.Errors.Select(e => e.Description));
        }

        return RedirectToAction(nameof(Profile));
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateProfile(UpdateProfileViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var firstError = ModelState.Values.SelectMany(v => v.Errors).FirstOrDefault()?.ErrorMessage ?? "Dữ liệu không hợp lệ.";
            TempData["ErrorMessage"] = firstError;
            return RedirectToAction(nameof(Profile));
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Challenge();

        var trimmedName = model.FullName.Trim();
        var trimmedEmail = model.Email.Trim().ToLowerInvariant();

        // Kiểm tra email đã được tài khoản khác sử dụng chưa
        var existingWithEmail = await _userManager.FindByEmailAsync(trimmedEmail);
        if (existingWithEmail != null && existingWithEmail.Id != user.Id)
        {
            TempData["ErrorMessage"] = "Email này đã được sử dụng bởi một tài khoản khác.";
            return RedirectToAction(nameof(Profile));
        }

        user.FullName = trimmedName;
        user.Email = trimmedEmail;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            TempData["ErrorMessage"] = string.Join("; ", result.Errors.Select(e => e.Description));
            return RedirectToAction(nameof(Profile));
        }

        if (user.CustomerProfileId.HasValue)
        {
            var profile = await _db.CustomerProfiles.FindAsync(user.CustomerProfileId.Value);
            if (profile != null)
            {
                profile.FullName = user.FullName;
                profile.Email = user.Email;
                await _db.SaveChangesAsync();
            }
        }

        TempData["SuccessMessage"] = "Cập nhật thông tin hồ sơ thành công!";
        return RedirectToAction(nameof(Profile));
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddAddress(AddressItemViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var firstError = ModelState.Values.SelectMany(v => v.Errors).FirstOrDefault()?.ErrorMessage ?? "Vui lòng nhập đầy đủ thông tin địa chỉ hợp lệ.";
            TempData["ErrorMessage"] = firstError;
            return RedirectToAction(nameof(Profile));
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null || !user.CustomerProfileId.HasValue) return Challenge();

        if (model.IsDefault)
        {
            var oldDefaults = await _db.CustomerAddresses
                .Where(a => a.CustomerProfileId == user.CustomerProfileId.Value && a.IsDefault)
                .ToListAsync();
            foreach (var a in oldDefaults) a.IsDefault = false;
        }

        var newAddress = new CustomerAddress
        {
            CustomerProfileId = user.CustomerProfileId.Value,
            Label = string.IsNullOrWhiteSpace(model.Label) ? "Nhà riêng" : model.Label.Trim(),
            RecipientName = model.RecipientName.Trim(),
            Phone = model.Phone.Trim(),
            Address = model.Address.Trim(),
            IsDefault = model.IsDefault
        };

        _db.CustomerAddresses.Add(newAddress);
        await _db.SaveChangesAsync();

        TempData["SuccessMessage"] = "Thêm địa chỉ giao hàng mới thành công!";
        return RedirectToAction(nameof(Profile));
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteAddress(int addressId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null || !user.CustomerProfileId.HasValue) return Challenge();

        var addr = await _db.CustomerAddresses.FirstOrDefaultAsync(a => a.Id == addressId && a.CustomerProfileId == user.CustomerProfileId.Value);
        if (addr != null)
        {
            _db.CustomerAddresses.Remove(addr);
            await _db.SaveChangesAsync();
            TempData["SuccessMessage"] = "Đã xóa địa chỉ.";
        }

        return RedirectToAction(nameof(Profile));
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SetDefaultAddress(int addressId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null || !user.CustomerProfileId.HasValue) return Challenge();

        var addrs = await _db.CustomerAddresses.Where(a => a.CustomerProfileId == user.CustomerProfileId.Value).ToListAsync();
        foreach (var a in addrs)
        {
            a.IsDefault = (a.Id == addressId);
        }
        await _db.SaveChangesAsync();
        TempData["SuccessMessage"] = "Đã đổi địa chỉ nhận hàng mặc định.";

        return RedirectToAction(nameof(Profile));
    }

    // REST API Endpoint: GET /api/me
    [HttpGet("api/me")]
    public async Task<IActionResult> ApiGetMe()
    {
        if (User.Identity?.IsAuthenticated != true)
        {
            return Ok(new { isAuthenticated = false, role = "GUEST" });
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Ok(new { isAuthenticated = false, role = "GUEST" });
        }

        var roles = await _userManager.GetRolesAsync(user);
        var role = roles.FirstOrDefault() ?? "Customer";

        return Ok(new
        {
            isAuthenticated = true,
            id = user.Id,
            userName = user.UserName,
            fullName = user.FullName,
            email = user.Email,
            role = role,
            customerProfileId = user.CustomerProfileId
        });
    }

    private IActionResult RedirectToLocal(string? returnUrl)
    {
        if (Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }
        return RedirectToAction("Index", "Home");
    }
}
