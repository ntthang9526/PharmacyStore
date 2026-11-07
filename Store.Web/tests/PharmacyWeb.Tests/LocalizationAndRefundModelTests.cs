using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using PharmacyWeb.Data;
using PharmacyWeb.Data.Entities;

namespace PharmacyWeb.Tests;

public class LocalizationAndRefundModelTests
{
    [Fact]
    public void WebOrdersEntity_ShouldMapRefundProperties()
    {
        using var db = new PharmacyWebDbContext(new DbContextOptionsBuilder<PharmacyWebDbContext>()
            .UseSqlServer("Server=unused;Database=PharmacyStoreDB;Integrated Security=True").Options);
        var orderEntity = db.Model.FindEntityType(typeof(Order));
        Assert.NotNull(orderEntity);

        Assert.NotNull(orderEntity.FindProperty(nameof(Order.IsRefunded)));
        Assert.NotNull(orderEntity.FindProperty(nameof(Order.RefundAmount)));
        Assert.NotNull(orderEntity.FindProperty(nameof(Order.RefundedAt)));
        Assert.NotNull(orderEntity.FindProperty(nameof(Order.RefundedByEmployee)));
        Assert.NotNull(orderEntity.FindProperty(nameof(Order.RefundReason)));
    }

    [Fact]
    public void SharedPosDatabase_GenerateIntegrationScript_ShouldContainRefundColumns()
    {
        using var db = new PharmacyWebDbContext(new DbContextOptionsBuilder<PharmacyWebDbContext>()
            .UseSqlServer("Server=unused;Database=PharmacyStoreDB;Integrated Security=True").Options);

        string script = SharedPosDatabase.GenerateIntegrationScript(db);
        Assert.Contains("IsRefunded", script);
        Assert.Contains("RefundAmount", script);
        Assert.Contains("RefundedAt", script);
        Assert.Contains("RefundedByEmployee", script);
        Assert.Contains("RefundReason", script);
    }

    [Fact]
    public void SharedResource_ShouldProvideVietnameseAndEnglishTranslations()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddLocalization();
        var provider = services.BuildServiceProvider();
        var factory = provider.GetRequiredService<IStringLocalizerFactory>();
        var localizer = factory.Create(typeof(PharmacyWeb.SharedResource));

        CultureInfo.CurrentUICulture = new CultureInfo("vi-VN");
        var viCart = localizer["Cart_Title"].Value;

        CultureInfo.CurrentUICulture = new CultureInfo("en-US");
        var enCart = localizer["Cart_Title"].Value;

        Assert.Equal("Giỏ Hàng", viCart);
        Assert.Equal("Shopping Cart", enCart);
    }

    [Fact]
    public void RequestLocalization_DefaultCulture_ShouldBeVietnamese()
    {
        var services = new ServiceCollection();
        var supportedCultures = new[]
        {
            new CultureInfo("vi-VN"),
            new CultureInfo("en-US")
        };

        services.Configure<RequestLocalizationOptions>(options =>
        {
            options.DefaultRequestCulture = new RequestCulture("vi-VN");
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
            options.RequestCultureProviders.Clear();
            options.RequestCultureProviders.Add(new CookieRequestCultureProvider { CookieName = "pharmacy_lang" });
            options.RequestCultureProviders.Add(new QueryStringRequestCultureProvider());
        });

        var provider = services.BuildServiceProvider();
        var options = provider.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;

        Assert.Equal("vi-VN", options.DefaultRequestCulture.Culture.Name);
        Assert.Equal("vi-VN", options.DefaultRequestCulture.UICulture.Name);
        Assert.Equal(2, options.RequestCultureProviders.Count);
        Assert.IsType<CookieRequestCultureProvider>(options.RequestCultureProviders[0]);
    }
}
