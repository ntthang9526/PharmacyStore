using System.Globalization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using PharmacyWeb.Data;
using PharmacyWeb.Data.Entities;
using PharmacyWeb.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Database Configuration
var provider = builder.Configuration["DatabaseProvider"] ?? "SqlServer";
var sqlServerConn = Environment.GetEnvironmentVariable("PHARMACY_CONNECTION_STRING")
    ?? Store.DAL.DatabaseConfiguration.Load(builder.Environment.ContentRootPath);
if (!provider.Equals("SqlServer", StringComparison.OrdinalIgnoreCase))
{
    throw new InvalidOperationException("Website dùng SQL Server chung với POS.");
}
builder.Services.AddDbContext<PharmacyWebDbContext>(options =>
    options.UseSqlServer(sqlServerConn));

// 2. Identity Configuration
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.User.RequireUniqueEmail = false; // Allow username login by phone
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    options.Lockout.MaxFailedAccessAttempts = 5;
})
.AddEntityFrameworkStores<PharmacyWebDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "Pharmacy_Auth_v1";
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.ExpireTimeSpan = TimeSpan.FromDays(7);
    options.SlidingExpiration = true;
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Home/AccessDenied";
    options.Events.OnValidatePrincipal = async context =>
    {
        await SecurityStampValidator.ValidatePrincipalAsync(context);
        if (context.Principal?.Identity?.IsAuthenticated != true) return;

        var services = context.HttpContext.RequestServices;
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var user = await userManager.GetUserAsync(context.Principal);
        bool locked = user == null || user.IsLocked;
        if (!locked && user!.CustomerProfileId.HasValue)
        {
            var db = services.GetRequiredService<PharmacyWebDbContext>();
            locked = await db.CustomerProfiles.AnyAsync(c => c.Id == user.CustomerProfileId.Value && c.IsLocked);
        }
        if (locked)
        {
            context.RejectPrincipal();
            await context.HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
        }
    };
});

// 3. Session Configuration
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "Pharmacy_Session_v1";
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromHours(2);
});

builder.Services.AddHttpContextAccessor();

// Configuration Options
builder.Services.Configure<PharmacyWeb.Models.BankTransferOptions>(
    builder.Configuration.GetSection(PharmacyWeb.Models.BankTransferOptions.SectionName));

// 4. Business Services Registration
builder.Services.AddScoped<IHtmlSanitizerService, HtmlSanitizerService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();
builder.Services.AddScoped<IReportService, ReportService>();

// 5. MVC and Localization
builder.Services.AddLocalization();

builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
            factory.Create(typeof(PharmacyWeb.SharedResource));
    });

var supportedCultures = new[]
{
    new CultureInfo("vi-VN"),
    new CultureInfo("en-US")
};

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("vi-VN");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.RequestCultureProviders.Clear();
    options.RequestCultureProviders.Add(new CookieRequestCultureProvider
    {
        CookieName = "pharmacy_lang"
    });
    options.RequestCultureProviders.Add(new QueryStringRequestCultureProvider());
});

var app = builder.Build();

// 6. Seed Data Execution on Startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<PharmacyWebDbContext>();
        var userMgr = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleMgr = services.GetRequiredService<RoleManager<ApplicationRole>>();
        await SharedPosDatabase.InitializeAsync(context, userMgr, roleMgr, builder.Environment.ContentRootPath);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Lỗi xảy ra trong quá trình khởi tạo dữ liệu mẫu (Seed).");
        throw new InvalidOperationException("Chưa khởi tạo được website. Kiểm tra .env, .env.web và hướng dẫn CHAY-LOCAL.txt. Database mới cần chạy Store.DAL/Database/InitDatabase.sql.", ex);
    }
}

// 7. Request Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseRequestLocalization();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

// Area Route (Admin)
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

// Default Route (Storefront)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

// Required for WebApplicationFactory in integration tests
public partial class Program { }
