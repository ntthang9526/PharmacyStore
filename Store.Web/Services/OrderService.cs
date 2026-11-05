using Microsoft.EntityFrameworkCore;
using PharmacyWeb.Data;
using PharmacyWeb.Data.Entities;
using PharmacyWeb.Models.ViewModels;

namespace PharmacyWeb.Services;

public interface IOrderService
{
    Task<(bool Success, string Message, int? OrderId, string? OrderCode)> CheckoutAsync(int customerId, CheckoutFormViewModel model);
    Task<OrderResultViewModel?> GetOrderResultAsync(int orderId, int customerId, bool isAdmin = false);
    Task<PagedResult<MyOrderListViewModel>> GetCustomerOrdersAsync(int customerId, int page, int pageSize);
    Task<(bool Success, string Message)> CustomerCancelOrderAsync(int orderId, int customerId, string reason);
    Task<PagedResult<Order>> GetAdminOrdersAsync(string? status, string? paymentStatus, string? keyword, int page, int pageSize);
    Task<Order?> GetOrderDetailsForAdminAsync(int orderId);
    Task<(bool Success, string Message)> UpdateOrderStatusByAdminAsync(int orderId, string newStatus, string? cancelReason, string employeeName);
    Task<(bool Success, string Message)> ConfirmCodPaymentAsync(int orderId, string employeeName);
}

public class OrderService : IOrderService
{
    private readonly PharmacyWebDbContext _db;
    private readonly ICartService _cartService;

    public OrderService(PharmacyWebDbContext db, ICartService cartService)
    {
        _db = db;
        _cartService = cartService;
    }

    public async Task<(bool Success, string Message, int? OrderId, string? OrderCode)> CheckoutAsync(int customerId, CheckoutFormViewModel model)
    {
        // 1. Idempotency Check
        if (!string.IsNullOrWhiteSpace(model.IdempotencyKey))
        {
            var existingOrder = await _db.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.CustomerId == customerId && o.IdempotencyKey == model.IdempotencyKey);

            if (existingOrder != null)
            {
                return (true, "Đơn hàng đã được ghi nhận trước đó.", existingOrder.Id, existingOrder.Code);
            }
        }

        var cart = await _cartService.GetCartAsync();
        if (!cart.Items.Any())
        {
            return (false, "Giỏ hàng của bạn đang trống.", null, null);
        }

        if (cart.Items.Any(i => i.IsPrescription))
        {
            return (false, "Giỏ hàng có chứa thuốc kê đơn (Rx). Thuốc kê đơn cần gửi ảnh đơn để dược sĩ tư vấn.", null, null);
        }

        var customer = await _db.CustomerProfiles.FirstOrDefaultAsync(c => c.Id == customerId);
        if (customer == null || customer.IsLocked)
        {
            return (false, "Tài khoản khách hàng không hợp lệ hoặc đã bị tạm khóa.", null, null);
        }

        // Open Database Transaction for atomic FEFO allocation, voucher deduction & order creation
        using var transaction = await _db.Database.BeginTransactionAsync();
        try
        {
            var today = DateTime.UtcNow.Date;
            var medicineIds = cart.Items.Select(i => i.MedicineId).Distinct().ToList();

            // Load fresh medicines with batches for locking
            var medicines = await _db.Medicines
                .Include(m => m.Batches)
                .Where(m => medicineIds.Contains(m.Id))
                .ToListAsync();

            decimal calculatedSubtotal = 0m;
            var orderItems = new List<OrderItem>();
            var allocations = new List<OrderBatchAllocation>();

            foreach (var cartItem in cart.Items)
            {
                var med = medicines.FirstOrDefault(m => m.Id == cartItem.MedicineId);
                if (med == null || !med.IsActive || !med.IsSelling)
                {
                    await transaction.RollbackAsync();
                    return (false, $"Sản phẩm '{cartItem.MedicineName}' hiện không còn kinh doanh hoặc đã ẩn.", null, null);
                }

                // FEFO Allocation: batches ordered by ExpiryDate ASC, then Id ASC
                var candidateBatches = med.Batches
                    .Where(b => !b.IsBlocked && b.ExpiryDate > today)
                    .OrderBy(b => b.ExpiryDate)
                    .ThenBy(b => b.Id)
                    .ToList();

                var totalAvailable = candidateBatches.Sum(b => Math.Max(0, b.Stock - b.ReservedQuantity));
                if (totalAvailable < cartItem.Quantity)
                {
                    await transaction.RollbackAsync();
                    return (false, $"Sản phẩm '{med.Name}' không đủ số lượng có thể bán (chỉ còn {totalAvailable} {med.Unit}).", null, null);
                }

                // Allocate across batches
                int remainingToAllocate = cartItem.Quantity;
                foreach (var batch in candidateBatches)
                {
                    if (remainingToAllocate <= 0) break;

                    var batchAvail = Math.Max(0, batch.Stock - batch.ReservedQuantity);
                    if (batchAvail <= 0) continue;

                    var qtyFromBatch = Math.Min(batchAvail, remainingToAllocate);
                    batch.ReservedQuantity += qtyFromBatch;
                    remainingToAllocate -= qtyFromBatch;

                    allocations.Add(new OrderBatchAllocation
                    {
                        BatchId = batch.Id,
                        Quantity = qtyFromBatch,
                        IsDeducted = false
                    });
                }

                var itemTotal = med.Price * cartItem.Quantity;
                calculatedSubtotal += itemTotal;

                orderItems.Add(new OrderItem
                {
                    MedicineId = med.Id,
                    MedicineName = med.Name,
                    Unit = med.Unit,
                    Quantity = cartItem.Quantity,
                    UnitPrice = med.Price,
                    TotalPrice = itemTotal,
                    IsPrescriptionSnapshot = med.IsPrescription
                });
            }

            // Voucher calculation & lock
            decimal discountAmount = 0m;
            string? validVoucherCode = null;

            if (!string.IsNullOrWhiteSpace(model.VoucherCode))
            {
                var cleanCode = model.VoucherCode.Trim().ToUpperInvariant();
                var voucher = await _db.Vouchers.FirstOrDefaultAsync(v => v.Code.ToUpper() == cleanCode);

                if (voucher != null && voucher.IsActive && voucher.Quantity > 0 &&
                    (!voucher.StartDate.HasValue || DateTime.UtcNow >= voucher.StartDate.Value) &&
                    (!voucher.EndDate.HasValue || DateTime.UtcNow <= voucher.EndDate.Value) &&
                    calculatedSubtotal >= voucher.MinOrderValue)
                {
                    if (voucher.DiscountType == "PERCENT")
                    {
                        discountAmount = Math.Round(calculatedSubtotal * (voucher.DiscountValue / 100m), 0);
                        if (voucher.MaxDiscountCap.HasValue && discountAmount > voucher.MaxDiscountCap.Value)
                        {
                            discountAmount = voucher.MaxDiscountCap.Value;
                        }
                    }
                    else
                    {
                        discountAmount = voucher.DiscountValue;
                    }

                    if (discountAmount > calculatedSubtotal)
                    {
                        discountAmount = calculatedSubtotal;
                    }

                    // Decrement voucher quantity in same transaction
                    voucher.Quantity -= 1;
                    validVoucherCode = voucher.Code;
                }
            }

            decimal shippingFee = calculatedSubtotal >= 100000m || calculatedSubtotal == 0 ? 0m : 15000m;
            decimal totalAmount = Math.Max(0m, calculatedSubtotal - discountAmount + shippingFee);

            // Generate unique Order Code
            var orderCode = $"HD{DateTime.UtcNow:yyMMdd}{new Random().Next(1000, 9999)}";
            while (await _db.Orders.AnyAsync(o => o.Code == orderCode))
            {
                orderCode = $"HD{DateTime.UtcNow:yyMMdd}{new Random().Next(1000, 9999)}";
            }

            var order = new Order
            {
                Code = orderCode,
                CustomerId = customerId,
                CustomerName = model.RecipientName.Trim(),
                CustomerPhone = model.Phone.Trim(),
                ShippingAddress = model.Address.Trim(),
                Channel = "WEB",
                OrderDate = DateTime.UtcNow,
                OrderStatus = "NEW",
                PaymentMethod = model.PaymentMethod == "BANK_TRANSFER" ? "BANK_TRANSFER" : "COD",
                PaymentStatus = model.PaymentMethod == "BANK_TRANSFER" ? "PENDING_TRANSFER" : "UNPAID",
                ShippingStatus = "NOT_SHIPPED",
                VoucherCode = validVoucherCode,
                SubtotalAmount = calculatedSubtotal,
                DiscountAmount = discountAmount,
                ShippingFee = shippingFee,
                TotalAmount = totalAmount,
                OrderVersion = 1,
                Note = model.Note?.Trim(),
                IdempotencyKey = model.IdempotencyKey,
                Items = orderItems,
                Allocations = allocations,
                History = new List<OrderStatusHistory>
                {
                    new()
                    {
                        Time = DateTime.UtcNow,
                        Actor = model.RecipientName.Trim(),
                        Text = $"Đặt đơn hàng thành công qua kênh Web ({model.PaymentMethod}). Tổng tiền: {totalAmount:N0} ₫"
                    }
                }
            };

            _db.Orders.Add(order);
            await _db.SaveChangesAsync();
            await transaction.CommitAsync();

            // Clear session cart on successful checkout
            await _cartService.ClearCartAsync();

            return (true, "Đặt hàng thành công!", order.Id, order.Code);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return (false, $"Lỗi hệ thống khi xử lý đơn hàng: {ex.Message}", null, null);
        }
    }

    public async Task<OrderResultViewModel?> GetOrderResultAsync(int orderId, int customerId, bool isAdmin = false)
    {
        var query = _db.Orders
            .AsNoTracking()
            .Include(o => o.Items)
            .Where(o => o.Id == orderId);

        if (!isAdmin)
        {
            query = query.Where(o => o.CustomerId == customerId);
        }

        var order = await query.FirstOrDefaultAsync();
        if (order == null) return null;

        return new OrderResultViewModel
        {
            OrderId = order.Id,
            OrderCode = order.Code,
            CustomerName = order.CustomerName,
            CustomerPhone = order.CustomerPhone,
            ShippingAddress = order.ShippingAddress,
            TotalAmount = order.TotalAmount,
            PaymentMethod = order.PaymentMethod,
            OrderStatus = order.OrderStatus,
            PaymentStatus = order.PaymentStatus,
            OrderDate = order.OrderDate,
            Items = order.Items.Select(i => new OrderItemViewModel
            {
                MedicineName = i.MedicineName,
                Unit = i.Unit,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                TotalPrice = i.TotalPrice
            }).ToList()
        };
    }

    public async Task<PagedResult<MyOrderListViewModel>> GetCustomerOrdersAsync(int customerId, int page, int pageSize)
    {
        var query = _db.Orders
            .AsNoTracking()
            .Include(o => o.Items)
            .Where(o => o.CustomerId == customerId)
            .OrderByDescending(o => o.OrderDate);

        var total = await query.CountAsync();
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(o => new MyOrderListViewModel
            {
                Id = o.Id,
                Code = o.Code,
                OrderDate = o.OrderDate,
                OrderStatus = o.OrderStatus,
                PaymentStatus = o.PaymentStatus,
                ShippingStatus = o.ShippingStatus,
                TotalAmount = o.TotalAmount,
                TotalItemCount = o.Items.Sum(i => i.Quantity),
                Items = o.Items.Select(i => new OrderItemViewModel
                {
                    MedicineName = i.MedicineName,
                    Unit = i.Unit,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    TotalPrice = i.TotalPrice
                }).ToList()
            })
            .ToListAsync();

        return new PagedResult<MyOrderListViewModel>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalItems = total
        };
    }

    public async Task<(bool Success, string Message)> CustomerCancelOrderAsync(int orderId, int customerId, string reason)
    {
        var order = await _db.Orders
            .Include(o => o.Allocations)
            .ThenInclude(a => a.Batch)
            .FirstOrDefaultAsync(o => o.Id == orderId && o.CustomerId == customerId);

        if (order == null)
        {
            return (false, "Không tìm thấy đơn hàng hoặc bạn không có quyền hủy đơn này.");
        }

        if (order.OrderStatus != "NEW")
        {
            return (false, "Chỉ có thể hủy đơn hàng khi đơn đang ở trạng thái 'Chờ xác nhận' (NEW).");
        }

        using var transaction = await _db.Database.BeginTransactionAsync();
        try
        {
            // Release reserved quantities on batches
            foreach (var alloc in order.Allocations)
            {
                if (alloc.Batch != null && !alloc.IsDeducted)
                {
                    alloc.Batch.ReservedQuantity = Math.Max(0, alloc.Batch.ReservedQuantity - alloc.Quantity);
                }
            }

            // Return voucher if used
            if (!string.IsNullOrWhiteSpace(order.VoucherCode))
            {
                var voucher = await _db.Vouchers.FirstOrDefaultAsync(v => v.Code == order.VoucherCode);
                if (voucher != null)
                {
                    voucher.Quantity += 1;
                }
            }

            order.OrderStatus = "CANCELLED";
            order.ShippingStatus = "CANCELLED";
            order.CancelReason = reason;
            order.OrderVersion += 1;

            order.History.Add(new OrderStatusHistory
            {
                OrderId = order.Id,
                Time = DateTime.UtcNow,
                Actor = order.CustomerName,
                Text = $"Khách hàng đã hủy đơn. Lý do: {reason}"
            });

            await _db.SaveChangesAsync();
            await transaction.CommitAsync();
            return (true, "Đã hủy đơn hàng thành công.");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return (false, $"Lỗi khi hủy đơn hàng: {ex.Message}");
        }
    }

    public async Task<PagedResult<Order>> GetAdminOrdersAsync(string? status, string? paymentStatus, string? keyword, int page, int pageSize)
    {
        var query = _db.Orders
            .AsNoTracking()
            .Include(o => o.Items)
            .OrderByDescending(o => o.OrderDate)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(status))
        {
            query = query.Where(o => o.OrderStatus == status);
        }

        if (!string.IsNullOrWhiteSpace(paymentStatus))
        {
            query = query.Where(o => o.PaymentStatus == paymentStatus);
        }

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            var kw = keyword.Trim().ToLower();
            query = query.Where(o => o.Code.ToLower().Contains(kw) || o.CustomerName.ToLower().Contains(kw) || o.CustomerPhone.Contains(kw));
        }

        var total = await query.CountAsync();
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PagedResult<Order>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalItems = total
        };
    }

    public async Task<Order?> GetOrderDetailsForAdminAsync(int orderId)
    {
        return await _db.Orders
            .AsNoTracking()
            .Include(o => o.Items)
            .Include(o => o.Allocations).ThenInclude(a => a.Batch)
            .Include(o => o.History.OrderBy(h => h.Time))
            .FirstOrDefaultAsync(o => o.Id == orderId);
    }

    public async Task<(bool Success, string Message)> UpdateOrderStatusByAdminAsync(int orderId, string newStatus, string? cancelReason, string employeeName)
    {
        var order = await _db.Orders
            .Include(o => o.Allocations).ThenInclude(a => a.Batch)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null) return (false, "Không tìm thấy đơn hàng.");

        // Valid transitions:
        // NEW -> CONFIRMED, CANCELLED
        // CONFIRMED -> SHIPPING, CANCELLED
        // SHIPPING -> COMPLETED, CANCELLED
        // COMPLETED -> (cannot change)
        // CANCELLED -> (cannot change)

        if (order.OrderStatus == "COMPLETED" || order.OrderStatus == "CANCELLED")
        {
            return (false, $"Đơn hàng đã ở trạng thái {order.OrderStatus}, không thể chuyển tiếp.");
        }

        using var transaction = await _db.Database.BeginTransactionAsync();
        try
        {
            if (newStatus == "CANCELLED")
            {
                foreach (var alloc in order.Allocations)
                {
                    if (alloc.Batch != null && !alloc.IsDeducted)
                    {
                        alloc.Batch.ReservedQuantity = Math.Max(0, alloc.Batch.ReservedQuantity - alloc.Quantity);
                    }
                }

                if (!string.IsNullOrWhiteSpace(order.VoucherCode))
                {
                    var voucher = await _db.Vouchers.FirstOrDefaultAsync(v => v.Code == order.VoucherCode);
                    if (voucher != null)
                    {
                        voucher.Quantity += 1;
                    }
                }

                order.OrderStatus = "CANCELLED";
                order.ShippingStatus = "CANCELLED";
                order.CancelReason = cancelReason ?? "Admin hủy đơn";
                order.OrderVersion += 1;

                order.History.Add(new OrderStatusHistory
                {
                    OrderId = order.Id,
                    Time = DateTime.UtcNow,
                    Actor = employeeName,
                    Text = $"Admin đã hủy đơn. Lý do: {order.CancelReason}"
                });
            }
            else if (newStatus == "CONFIRMED")
            {
                order.OrderStatus = "CONFIRMED";
                order.ConfirmedAt = DateTime.UtcNow;
                order.OrderVersion += 1;
                order.History.Add(new OrderStatusHistory
                {
                    OrderId = order.Id,
                    Time = DateTime.UtcNow,
                    Actor = employeeName,
                    Text = "Admin đã xác nhận đơn hàng."
                });
            }
            else if (newStatus == "SHIPPING")
            {
                order.OrderStatus = "SHIPPING";
                order.ShippingStatus = "SHIPPING";
                order.ShippedAt = DateTime.UtcNow;
                order.OrderVersion += 1;
                order.History.Add(new OrderStatusHistory
                {
                    OrderId = order.Id,
                    Time = DateTime.UtcNow,
                    Actor = employeeName,
                    Text = "Đơn hàng đang được giao cho khách."
                });
            }
            else if (newStatus == "COMPLETED")
            {
                // Completing order permanently deducts batch stock!
                foreach (var alloc in order.Allocations)
                {
                    if (alloc.Batch != null && !alloc.IsDeducted)
                    {
                        alloc.Batch.Stock = Math.Max(0, alloc.Batch.Stock - alloc.Quantity);
                        alloc.Batch.ReservedQuantity = Math.Max(0, alloc.Batch.ReservedQuantity - alloc.Quantity);
                        alloc.IsDeducted = true;
                    }
                }

                order.OrderStatus = "COMPLETED";
                order.ShippingStatus = "DELIVERED";
                order.CompletedAt = DateTime.UtcNow;
                order.OrderVersion += 1;

                order.History.Add(new OrderStatusHistory
                {
                    OrderId = order.Id,
                    Time = DateTime.UtcNow,
                    Actor = employeeName,
                    Text = "Giao hàng thành công và hoàn tất đơn hàng."
                });
            }

            await _db.SaveChangesAsync();
            await transaction.CommitAsync();
            return (true, $"Đã cập nhật trạng thái đơn hàng sang '{newStatus}'.");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return (false, $"Lỗi cập nhật trạng thái đơn: {ex.Message}");
        }
    }

    public async Task<(bool Success, string Message)> ConfirmCodPaymentAsync(int orderId, string employeeName)
    {
        var order = await _db.Orders
            .Include(o => o.Allocations).ThenInclude(a => a.Batch)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null) return (false, "Không tìm thấy đơn hàng.");
        if (order.PaymentStatus == "PAID") return (false, "Đơn hàng này đã được xác nhận thanh toán trước đó.");

        using var transaction = await _db.Database.BeginTransactionAsync();
        try
        {
            order.PaymentStatus = "PAID";
            order.PaidAt = DateTime.UtcNow;
            order.PaidByEmployee = employeeName;

            // If not completed yet, complete it now
            if (order.OrderStatus != "COMPLETED")
            {
                foreach (var alloc in order.Allocations)
                {
                    if (alloc.Batch != null && !alloc.IsDeducted)
                    {
                        alloc.Batch.Stock = Math.Max(0, alloc.Batch.Stock - alloc.Quantity);
                        alloc.Batch.ReservedQuantity = Math.Max(0, alloc.Batch.ReservedQuantity - alloc.Quantity);
                        alloc.IsDeducted = true;
                    }
                }
                order.OrderStatus = "COMPLETED";
                order.ShippingStatus = "DELIVERED";
                order.CompletedAt = DateTime.UtcNow;
            }

            order.OrderVersion += 1;
            order.History.Add(new OrderStatusHistory
            {
                OrderId = order.Id,
                Time = DateTime.UtcNow,
                Actor = employeeName,
                Text = $"Xác nhận đã thu tiền COD: {order.TotalAmount:N0} ₫."
            });

            await _db.SaveChangesAsync();
            await transaction.CommitAsync();
            return (true, $"Đã xác nhận thu tiền {order.TotalAmount:N0} ₫ cho đơn {order.Code}.");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return (false, $"Lỗi xác nhận thanh toán: {ex.Message}");
        }
    }
}
