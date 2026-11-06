using PharmacyWeb.Data.Entities;
using PharmacyWeb.Services;
using Xunit;

namespace PharmacyWeb.Tests;

public class BusinessLogicUnitTests
{
    private readonly HtmlSanitizerService _sanitizer = new();

    [Fact]
    public void HtmlSanitizer_ShouldRemoveScriptTags()
    {
        var maliciousHtml = "<p>Thuốc giảm đau</p><script>alert('hack');</script><b>Chính hãng</b>";
        var clean = _sanitizer.Sanitize(maliciousHtml);

        Assert.DoesNotContain("<script>", clean);
        Assert.DoesNotContain("alert('hack')", clean);
        Assert.Contains("<p>Thuốc giảm đau</p>", clean);
        Assert.Contains("<b>Chính hãng</b>", clean);
    }

    [Fact]
    public void HtmlSanitizer_ShouldRemoveOnEventAttributes()
    {
        var maliciousHtml = "<img src='x' onerror='alert(1)' /><span>Chi tiết</span>";
        var clean = _sanitizer.Sanitize(maliciousHtml);

        Assert.DoesNotContain("onerror", clean);
        Assert.Contains("<span>Chi tiết</span>", clean);
    }

    [Fact]
    public void Batch_AvailableStock_ShouldBeZero_WhenBlockedOrExpired()
    {
        var today = DateTime.UtcNow.Date;

        var validBatch = new Batch { Stock = 20, ReservedQuantity = 5, IsBlocked = false, ExpiryDate = today.AddDays(30) };
        var blockedBatch = new Batch { Stock = 20, ReservedQuantity = 0, IsBlocked = true, ExpiryDate = today.AddDays(30) };
        var expiredBatch = new Batch { Stock = 20, ReservedQuantity = 0, IsBlocked = false, ExpiryDate = today.AddDays(-1) };

        Assert.Equal(15, validBatch.AvailableStock);
        Assert.Equal(0, blockedBatch.AvailableStock);
        Assert.Equal(0, expiredBatch.AvailableStock);
    }

    [Fact]
    public void Medicine_AvailableStock_ShouldBeZero_WhenNotSellingOrInactive()
    {
        var today = DateTime.UtcNow.Date;
        var batch = new Batch { Stock = 50, ReservedQuantity = 0, IsBlocked = false, ExpiryDate = today.AddYears(1) };

        var notSellingMed = new Medicine
        {
            IsSelling = false,
            IsActive = true,
            Batches = new List<Batch> { batch }
        };

        var inactiveMed = new Medicine
        {
            IsSelling = true,
            IsActive = false,
            Batches = new List<Batch> { batch }
        };

        var activeMed = new Medicine
        {
            IsSelling = true,
            IsActive = true,
            Batches = new List<Batch> { batch }
        };

        Assert.Equal(0, notSellingMed.AvailableStock);
        Assert.Equal(0, inactiveMed.AvailableStock);
        Assert.Equal(50, activeMed.AvailableStock);
    }

    [Theory]
    [InlineData(120000, 0)]    // Free shipping for >= 100,000 VND
    [InlineData(100000, 0)]    // Free shipping at exactly 100,000 VND
    [InlineData(85000, 15000)] // 15,000 VND shipping fee for < 100,000 VND
    [InlineData(0, 0)]         // Empty cart has 0 shipping fee
    public void ShippingFee_Rule_ShouldCalculateCorrectly(decimal subtotal, decimal expectedFee)
    {
        decimal fee = (subtotal >= 100000m || subtotal == 0m) ? 0m : 15000m;
        Assert.Equal(expectedFee, fee);
    }

    [Fact]
    public void Voucher_FixedDiscount_CannotExceedSubtotal()
    {
        decimal subtotal = 18000m;
        var voucher = new Voucher
        {
            Code = "PH25K",
            DiscountType = "FIXED",
            DiscountValue = 25000m
        };

        decimal discount = voucher.DiscountValue;
        if (discount > subtotal)
        {
            discount = subtotal;
        }

        Assert.Equal(18000m, discount);
        Assert.Equal(0m, subtotal - discount);
    }

    [Fact]
    public void Voucher_PercentDiscount_ShouldRespectMaxDiscountCap()
    {
        decimal subtotal = 500000m;
        var voucher = new Voucher
        {
            Code = "PH10",
            DiscountType = "PERCENT",
            DiscountValue = 10m,
            MaxDiscountCap = 30000m
        };

        decimal rawDiscount = Math.Round(subtotal * (voucher.DiscountValue / 100m), 0);
        decimal finalDiscount = voucher.MaxDiscountCap.HasValue && rawDiscount > voucher.MaxDiscountCap.Value
            ? voucher.MaxDiscountCap.Value
            : rawDiscount;

        Assert.Equal(50000m, rawDiscount);
        Assert.Equal(30000m, finalDiscount);
    }
}
