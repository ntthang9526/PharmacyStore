using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PharmacyWeb.Data;
using PharmacyWeb.Data.Entities;
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

/// <summary>
/// Dữ liệu mục giỏ hàng lưu gọn trong Session của trình duyệt khách hàng.
/// Chỉ lưu ID thuốc và số lượng để đảm bảo giá và tồn kho luôn được kiểm tra mới nhất từ cơ sở dữ liệu.
/// </summary>
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
    private readonly PharmacyWebDbContext _databaseContext;

    public CartService(IHttpContextAccessor httpContextAccessor, PharmacyWebDbContext databaseContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _databaseContext = databaseContext;
    }

    private ISession Session => _httpContextAccessor.HttpContext?.Session 
        ?? throw new InvalidOperationException("Session hiện tại không khả dụng.");

    /// <summary>
    /// Lấy toàn bộ giỏ hàng của khách từ session và tra cứu thông tin giá, tồn kho mới nhất từ database.
    /// </summary>
    public async Task<CartViewModel> GetCartAsync()
    {
        var sessionItems = GetSessionItems();
        var cartViewModel = new CartViewModel();

        if (sessionItems.Count == 0)
        {
            return cartViewModel;
        }

        // Lấy danh sách ID thuốc để truy vấn một lần duy nhất (tránh lỗi N+1 query)
        var medicineIds = sessionItems
            .Select(sessionItem => sessionItem.MedicineId)
            .Distinct()
            .ToList();

        var medicinesDictionary = await _databaseContext.Medicines
            .AsNoTracking()
            .Include(medicine => medicine.Batches)
            .Where(medicine => medicineIds.Contains(medicine.Id))
            .ToDictionaryAsync(medicine => medicine.Id);

        var today = PharmacyClock.Today;

        foreach (var sessionItem in sessionItems)
        {
            if (medicinesDictionary.TryGetValue(sessionItem.MedicineId, out var medicine))
            {
                int availableStock = CalculateAvailableStock(medicine, today);

                var cartItemViewModel = new CartItemViewModel
                {
                    MedicineId = medicine.Id,
                    MedicineName = medicine.Name,
                    Slug = medicine.Slug,
                    Unit = medicine.Unit,
                    Price = medicine.Price, // Giá chuẩn xác lấy trực tiếp từ database của quầy POS
                    Quantity = sessionItem.Quantity,
                    AvailableStock = availableStock,
                    IsPrescription = medicine.IsPrescription,
                    IsSelling = medicine.IsSelling,
                    IsActive = medicine.IsActive,
                    ImageUrl = medicine.MainImageUrl
                };

                cartViewModel.Items.Add(cartItemViewModel);
            }
        }

        // Kiểm tra và áp dụng mã voucher nếu khách đã lưu trong session trước đó
        var savedVoucherCode = Session.GetString(VoucherSessionKey);
        if (!string.IsNullOrWhiteSpace(savedVoucherCode))
        {
            var voucherEvaluationResult = await EvaluateVoucherInternalAsync(savedVoucherCode, cartViewModel.SubtotalAmount);
            cartViewModel.AppliedVoucherCode = savedVoucherCode;
            cartViewModel.VoucherSuccess = voucherEvaluationResult.Success;
            cartViewModel.VoucherMessage = voucherEvaluationResult.Message;
            cartViewModel.DiscountAmount = voucherEvaluationResult.DiscountAmount;
        }

        return cartViewModel;
    }

    /// <summary>
    /// Thêm thuốc vào giỏ hàng với kiểm tra thuốc kê đơn (Rx) và tồn kho có thể bán.
    /// </summary>
    public async Task<bool> AddToCartAsync(int medicineId, int quantity)
    {
        if (quantity <= 0)
        {
            return false;
        }

        var medicine = await _databaseContext.Medicines
            .Include(m => m.Batches)
            .FirstOrDefaultAsync(m => m.Id == medicineId && m.IsActive);

        // Quy định an toàn y tế: Thuốc cần đơn bác sĩ (Rx) không được mua trực tiếp tự do
        if (medicine == null || medicine.IsPrescription || !medicine.IsSelling)
        {
            return false;
        }

        var today = PharmacyClock.Today;
        int availableStock = CalculateAvailableStock(medicine, today);

        var sessionItems = GetSessionItems();
        var existingItem = sessionItems.FirstOrDefault(item => item.MedicineId == medicineId);

        int currentQuantityInCart = existingItem != null ? existingItem.Quantity : 0;
        int targetTotalQuantity = currentQuantityInCart + quantity;

        // Chặn thêm quá số lượng thuốc thực tế có thể bán trong kho
        if (targetTotalQuantity > availableStock)
        {
            return false;
        }

        if (existingItem != null)
        {
            existingItem.Quantity = targetTotalQuantity;
        }
        else
        {
            sessionItems.Add(new CartSessionItem
            {
                MedicineId = medicineId,
                Quantity = quantity
            });
        }

        SaveSessionItems(sessionItems);
        return true;
    }

    /// <summary>
    /// Cập nhật lại số lượng sản phẩm trong giỏ hàng. Nếu số lượng <= 0 thì tự động xóa khỏi giỏ.
    /// </summary>
    public async Task<bool> UpdateQuantityAsync(int medicineId, int quantity)
    {
        var sessionItems = GetSessionItems();
        var existingItem = sessionItems.FirstOrDefault(item => item.MedicineId == medicineId);
        if (existingItem == null)
        {
            return false;
        }

        if (quantity <= 0)
        {
            sessionItems.Remove(existingItem);
        }
        else
        {
            var medicine = await _databaseContext.Medicines
                .Include(m => m.Batches)
                .FirstOrDefaultAsync(m => m.Id == medicineId && m.IsActive);

            if (medicine == null || !medicine.IsSelling)
            {
                return false;
            }

            var today = PharmacyClock.Today;
            int availableStock = CalculateAvailableStock(medicine, today);

            if (quantity > availableStock)
            {
                return false;
            }

            existingItem.Quantity = quantity;
        }

        SaveSessionItems(sessionItems);
        return true;
    }

    /// <summary>
    /// Xóa một sản phẩm ra khỏi giỏ hàng.
    /// </summary>
    public Task<bool> RemoveFromCartAsync(int medicineId)
    {
        var sessionItems = GetSessionItems();
        int removedCount = sessionItems.RemoveAll(item => item.MedicineId == medicineId);
        bool hasRemoved = removedCount > 0;

        if (hasRemoved)
        {
            SaveSessionItems(sessionItems);
        }

        return Task.FromResult(hasRemoved);
    }

    /// <summary>
    /// Xóa toàn bộ giỏ hàng và voucher sau khi đặt hàng thành công.
    /// </summary>
    public Task ClearCartAsync()
    {
        Session.Remove(CartSessionKey);
        Session.Remove(VoucherSessionKey);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Kiểm tra và lưu voucher vào session nếu hợp lệ.
    /// </summary>
    public async Task<(bool Success, string Message, decimal DiscountAmount)> ApplyVoucherAsync(string voucherCode)
    {
        if (string.IsNullOrWhiteSpace(voucherCode))
        {
            RemoveVoucher();
            return (false, "Vui lòng nhập mã giảm giá.", 0m);
        }

        var cart = await GetCartAsync();
        if (cart.Items.Count == 0)
        {
            return (false, "Giỏ hàng của bạn đang trống.", 0m);
        }

        string cleanVoucherCode = voucherCode.Trim().ToUpperInvariant();
        var evaluationResult = await EvaluateVoucherInternalAsync(cleanVoucherCode, cart.SubtotalAmount);

        if (evaluationResult.Success)
        {
            Session.SetString(VoucherSessionKey, cleanVoucherCode);
        }
        else
        {
            Session.Remove(VoucherSessionKey);
        }

        return evaluationResult;
    }

    public void RemoveVoucher()
    {
        Session.Remove(VoucherSessionKey);
    }

    /// <summary>
    /// Tính toán số lượng tồn kho có thể bán của thuốc.
    /// Quy tắc: Chỉ tính từ các lô không bị khóa (IsBlocked = false) và còn hạn sử dụng (ExpiryDate >= today).
    /// </summary>
    private static int CalculateAvailableStock(Medicine medicine, DateTime currentDate)
    {
        if (!medicine.IsActive || !medicine.IsSelling)
        {
            return 0;
        }

        int totalAvailableStock = 0;

        foreach (var batch in medicine.Batches)
        {
            bool isBatchValid = !batch.IsBlocked && batch.ExpiryDate >= currentDate;
            if (isBatchValid)
            {
                int remainingInBatch = batch.Stock - batch.ReservedQuantity;
                if (remainingInBatch > 0)
                {
                    totalAvailableStock += remainingInBatch;
                }
            }
        }

        return totalAvailableStock;
    }

    /// <summary>
    /// Đánh giá tính hợp lệ và giá trị giảm giá của voucher.
    /// </summary>
    private async Task<(bool Success, string Message, decimal DiscountAmount)> EvaluateVoucherInternalAsync(string voucherCode, decimal subtotalAmount)
    {
        string normalizedCode = voucherCode.Trim().ToUpperInvariant();
        var voucher = await _databaseContext.Vouchers
            .FirstOrDefaultAsync(v => v.Code.ToUpper() == normalizedCode);

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

        var currentUtcTime = DateTime.UtcNow;

        if (voucher.StartDate.HasValue && currentUtcTime < voucher.StartDate.Value)
        {
            return (false, "Mã giảm giá chưa đến thời gian áp dụng.", 0m);
        }

        if (voucher.EndDate.HasValue && currentUtcTime > voucher.EndDate.Value)
        {
            return (false, "Mã giảm giá đã hết hạn sử dụng.", 0m);
        }

        if (subtotalAmount < voucher.MinOrderValue)
        {
            return (false, $"Đơn hàng tối thiểu để áp dụng mã là {voucher.MinOrderValue:N0} ₫.", 0m);
        }

        decimal calculatedDiscount = 0m;

        if (voucher.DiscountType == "PERCENT")
        {
            calculatedDiscount = Math.Round(subtotalAmount * (voucher.DiscountValue / 100m), 0);
            if (voucher.MaxDiscountCap.HasValue && calculatedDiscount > voucher.MaxDiscountCap.Value)
            {
                calculatedDiscount = voucher.MaxDiscountCap.Value;
            }
        }
        else
        {
            calculatedDiscount = voucher.DiscountValue;
        }

        // Tiền giảm giá không bao giờ được vượt quá giá trị đơn hàng
        if (calculatedDiscount > subtotalAmount)
        {
            calculatedDiscount = subtotalAmount;
        }

        return (true, $"Áp dụng mã giảm giá {voucher.Code} thành công: giảm {calculatedDiscount:N0} ₫", calculatedDiscount);
    }

    private List<CartSessionItem> GetSessionItems()
    {
        string? jsonString = Session.GetString(CartSessionKey);
        if (string.IsNullOrWhiteSpace(jsonString))
        {
            return new List<CartSessionItem>();
        }

        try
        {
            var deserializedItems = JsonSerializer.Deserialize<List<CartSessionItem>>(jsonString);
            return deserializedItems ?? new List<CartSessionItem>();
        }
        catch
        {
            return new List<CartSessionItem>();
        }
    }

    private void SaveSessionItems(List<CartSessionItem> sessionItems)
    {
        string jsonString = JsonSerializer.Serialize(sessionItems);
        Session.SetString(CartSessionKey, jsonString);
    }
}
