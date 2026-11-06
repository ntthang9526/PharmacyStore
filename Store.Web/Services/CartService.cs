using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PharmacyWeb.Data;
using PharmacyWeb.Models.ViewModels;

namespace PharmacyWeb.Services;

public interface ICartService
{
    Task<CartViewModel> GetCartAsync();
    Task<bool> AddToCartAsync(int medicineId, int quantity);
    Task<bool> UpdateQuantityAsync(int medicineId, int quantity);
    Task<bool> RemoveFromCartAsync(int medicineId);
    Task ClearCartAsync();
    Task<(bool Success, string Message, decimal DiscountAmount)> ApplyVoucherAsync(string voucherCode);
    void RemoveVoucher();
}

public class CartSessionItem
{
    public int MedicineId { get; set; }
    public int Quantity { get; set; }
}

public class CartService : ICartService
{
    private const string CartSessionKey = "Pharmacy_Session_Cart_v1";
    private const string VoucherSessionKey = "Pharmacy_Session_Voucher_v1";

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly PharmacyWebDbContext _db;

    public CartService(IHttpContextAccessor httpContextAccessor, PharmacyWebDbContext db)
    {
        _httpContextAccessor = httpContextAccessor;
        _db = db;
    }

    private ISession Session => _httpContextAccessor.HttpContext?.Session 
        ?? throw new InvalidOperationException("Session is not available.");

    public async Task<CartViewModel> GetCartAsync()
    {
        var sessionItems = GetSessionItems();
        var vm = new CartViewModel();

        if (!sessionItems.Any())
        {
            return vm;
        }

        var medicineIds = sessionItems.Select(s => s.MedicineId).Distinct().ToList();
        var medicines = await _db.Medicines
            .AsNoTracking()
            .Include(m => m.Batches)
            .Where(m => medicineIds.Contains(m.Id))
            .ToDictionaryAsync(m => m.Id);

        var today = PharmacyWeb.Services.PharmacyClock.Today;

        foreach (var item in sessionItems)
        {
            if (medicines.TryGetValue(item.MedicineId, out var med))
            {
                var availStock = (!med.IsActive || !med.IsSelling) ? 0 :
                    med.Batches.Where(b => !b.IsBlocked && b.ExpiryDate >= today)
                               .Sum(b => Math.Max(0, b.Stock - b.ReservedQuantity));

                vm.Items.Add(new CartItemViewModel
                {
                    MedicineId = med.Id,
                    MedicineName = med.Name,
                    Slug = med.Slug,
                    Unit = med.Unit,
                    Price = med.Price, // Server-authoritative price
                    Quantity = item.Quantity,
                    AvailableStock = availStock,
                    IsPrescription = med.IsPrescription,
                    IsSelling = med.IsSelling,
                    IsActive = med.IsActive,
                    ImageUrl = med.MainImageUrl
                });
            }
        }

        // Apply voucher if saved in session
        var voucherCode = Session.GetString(VoucherSessionKey);
        if (!string.IsNullOrWhiteSpace(voucherCode))
        {
            var res = await EvaluateVoucherInternalAsync(voucherCode, vm.SubtotalAmount);
            vm.AppliedVoucherCode = voucherCode;
            vm.VoucherSuccess = res.Success;
            vm.VoucherMessage = res.Message;
            vm.DiscountAmount = res.DiscountAmount;
        }

        return vm;
    }

    public async Task<bool> AddToCartAsync(int medicineId, int quantity)
    {
        if (quantity <= 0) return false;

        var med = await _db.Medicines
            .Include(m => m.Batches)
            .FirstOrDefaultAsync(m => m.Id == medicineId && m.IsActive);

        if (med == null || med.IsPrescription || !med.IsSelling)
        {
            return false; // Prescription and non-selling cannot be directly added to cart
        }

        var today = PharmacyWeb.Services.PharmacyClock.Today;
        var availStock = med.Batches.Where(b => !b.IsBlocked && b.ExpiryDate >= today)
                                    .Sum(b => Math.Max(0, b.Stock - b.ReservedQuantity));

        var items = GetSessionItems();
        var existing = items.FirstOrDefault(i => i.MedicineId == medicineId);

        var targetQty = (existing?.Quantity ?? 0) + quantity;
        if (targetQty > availStock)
        {
            return false; // Exceeds available stock
        }

        if (existing != null)
        {
            existing.Quantity = targetQty;
        }
        else
        {
            items.Add(new CartSessionItem { MedicineId = medicineId, Quantity = quantity });
        }

        SaveSessionItems(items);
        return true;
    }

    public async Task<bool> UpdateQuantityAsync(int medicineId, int quantity)
    {
        var items = GetSessionItems();
        var existing = items.FirstOrDefault(i => i.MedicineId == medicineId);
        if (existing == null) return false;

        if (quantity <= 0)
        {
            items.Remove(existing);
        }
        else
        {
            var med = await _db.Medicines
                .Include(m => m.Batches)
                .FirstOrDefaultAsync(m => m.Id == medicineId && m.IsActive);

            if (med == null || !med.IsSelling) return false;

            var today = PharmacyWeb.Services.PharmacyClock.Today;
            var availStock = med.Batches.Where(b => !b.IsBlocked && b.ExpiryDate >= today)
                                        .Sum(b => Math.Max(0, b.Stock - b.ReservedQuantity));

            if (quantity > availStock)
            {
                return false;
            }

            existing.Quantity = quantity;
        }

        SaveSessionItems(items);
        return true;
    }

    public Task<bool> RemoveFromCartAsync(int medicineId)
    {
        var items = GetSessionItems();
        var removed = items.RemoveAll(i => i.MedicineId == medicineId) > 0;
        if (removed)
        {
            SaveSessionItems(items);
        }
        return Task.FromResult(removed);
    }

    public Task ClearCartAsync()
    {
        Session.Remove(CartSessionKey);
        Session.Remove(VoucherSessionKey);
        return Task.CompletedTask;
    }

    public async Task<(bool Success, string Message, decimal DiscountAmount)> ApplyVoucherAsync(string voucherCode)
    {
        if (string.IsNullOrWhiteSpace(voucherCode))
        {
            RemoveVoucher();
            return (false, "Vui lòng nhập mã giảm giá", 0m);
        }

        var cart = await GetCartAsync();
        if (!cart.Items.Any())
        {
            return (false, "Giỏ hàng đang trống", 0m);
        }

        var res = await EvaluateVoucherInternalAsync(voucherCode.Trim(), cart.SubtotalAmount);
        if (res.Success)
        {
            Session.SetString(VoucherSessionKey, voucherCode.Trim().ToUpperInvariant());
        }
        else
        {
            Session.Remove(VoucherSessionKey);
        }

        return res;
    }

    public void RemoveVoucher()
    {
        Session.Remove(VoucherSessionKey);
    }

    private async Task<(bool Success, string Message, decimal DiscountAmount)> EvaluateVoucherInternalAsync(string code, decimal subtotal)
    {
        var cleanCode = code.Trim().ToUpperInvariant();
        var voucher = await _db.Vouchers.FirstOrDefaultAsync(v => v.Code.ToUpper() == cleanCode);

        if (voucher == null)
        {
            return (false, "Mã giảm giá không tồn tại.", 0m);
        }

        if (!voucher.IsActive)
        {
            return (false, "Mã giảm giá hiện đang tạm khóa.", 0m);
        }

        if (voucher.Quantity <= 0)
        {
            return (false, "Mã giảm giá đã hết lượt sử dụng.", 0m);
        }

        if (voucher.StartDate.HasValue && DateTime.UtcNow < voucher.StartDate.Value)
        {
            return (false, "Mã giảm giá chưa đến thời gian áp dụng.", 0m);
        }

        if (voucher.EndDate.HasValue && DateTime.UtcNow > voucher.EndDate.Value)
        {
            return (false, "Mã giảm giá đã hết hạn sử dụng.", 0m);
        }

        if (subtotal < voucher.MinOrderValue)
        {
            return (false, $"Đơn hàng tối thiểu để áp dụng mã là {voucher.MinOrderValue:N0} ₫.", 0m);
        }

        decimal discount = 0m;
        if (voucher.DiscountType == "PERCENT")
        {
            discount = Math.Round(subtotal * (voucher.DiscountValue / 100m), 0);
            if (voucher.MaxDiscountCap.HasValue && discount > voucher.MaxDiscountCap.Value)
            {
                discount = voucher.MaxDiscountCap.Value;
            }
        }
        else
        {
            discount = voucher.DiscountValue;
        }

        // Discount cannot exceed subtotal
        if (discount > subtotal)
        {
            discount = subtotal;
        }

        return (true, $"Áp dụng mã giảm giá {voucher.Code} thành công: giảm {discount:N0} ₫", discount);
    }

    private List<CartSessionItem> GetSessionItems()
    {
        var json = Session.GetString(CartSessionKey);
        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<CartSessionItem>();
        }

        try
        {
            return JsonSerializer.Deserialize<List<CartSessionItem>>(json) ?? new List<CartSessionItem>();
        }
        catch
        {
            return new List<CartSessionItem>();
        }
    }

    private void SaveSessionItems(List<CartSessionItem> items)
    {
        var json = JsonSerializer.Serialize(items);
        Session.SetString(CartSessionKey, json);
    }
}
