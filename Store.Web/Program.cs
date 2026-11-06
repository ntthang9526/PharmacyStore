using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using PharmacyWeb.Data;
using PharmacyWeb.Data.Entities;
using PharmacyWeb.Data.Seed;
using PharmacyWeb.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Database Configuration
var provider = builder.Configuration["DatabaseProvider"] ?? "SqlServer";
var sqlServerConn = Environment.GetEnvironmentVariable("PHARMACY_CONNECTION_STRING")
    ?? Store.DAL.DatabaseConfiguration.Load(builder.Environment.ContentRootPath);
var sqliteConn = builder.Configuration.GetConnectionString("SqliteConnection") ?? "Data Source=PharmacyWeb.db";

if (provider.Equals("SqlServer", StringComparison.OrdinalIgnoreCase))
{
    builder.Services.AddDbContext<PharmacyWebDbContext>(options =>
        options.UseSqlServer(sqlServerConn));
}
else if (provider.Equals("Sqlite", StringComparison.OrdinalIgnoreCase) && builder.Environment.IsEnvironment("Testing"))
{
    builder.Services.AddDbContext<PharmacyWebDbContext>(options =>
        options.UseSqlite(sqliteConn));
}
else
{
    throw new InvalidOperationException("Website dùng SQL Server chung với POS. SQLite chỉ dành cho môi trường Testing.");
}

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
builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

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
    options.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider
    {
        CookieName = "pharmacy_lang"
    });
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
        if (context.Database.IsSqlServer())
            await SharedPosDatabase.InitializeAsync(context, userMgr, roleMgr, builder.Environment.ContentRootPath);
        else
            await DbInitializer.InitializeAsync(context, userMgr, roleMgr);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Lỗi xảy ra trong quá trình khởi tạo dữ liệu mẫu (Seed).");
        throw new InvalidOperationException("Chưa khởi tạo được database web dùng chung POS. Kiểm tra .env và script Database/IntegratePosDatabase.sql.", ex);
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
