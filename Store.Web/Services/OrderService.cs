using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PharmacyWeb.Data;
using PharmacyWeb.Data.Entities;
using PharmacyWeb.Models;
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
    Task<(bool Success, string Message)> RefundOrderAsync(int orderId, string refundReason, string employeeName);
}

public class OrderService : IOrderService
{
    private readonly PharmacyWebDbContext _databaseContext;
    private readonly ICartService _cartService;
    private readonly BankTransferOptions _bankTransferOptions;

    public OrderService(
        PharmacyWebDbContext databaseContext,
        ICartService cartService,
        IOptions<BankTransferOptions>? bankTransferOptions = null)
    {
        _databaseContext = databaseContext;
        _cartService = cartService;
        _bankTransferOptions = bankTransferOptions?.Value ?? new BankTransferOptions();
    }

    /// <summary>
    /// Xử lý quy trình đặt hàng từ giỏ hàng trực tuyến:
    /// 1. Kiểm tra chống trùng lặp (Idempotency check).
    /// 2. Kiểm tra tài khoản khách hàng và thành phần giỏ hàng.
    /// 3. Khóa giao dịch Serializable và phân bổ giữ hàng theo nguyên tắc FEFO (Hạn gần xuất trước).
    /// 4. Áp dụng trừ số lượng voucher (nếu có).
    /// 5. Tạo đơn hàng và ghi nhật ký trạng thái ban đầu.
    /// </summary>
    public async Task<(bool Success, string Message, int? OrderId, string? OrderCode)> CheckoutAsync(int customerId, CheckoutFormViewModel model)
    {
        // 1. Kiểm tra chống tạo đơn trùng lặp do khách hàng bấm liên tiếp (Idempotency)
        if (!string.IsNullOrWhiteSpace(model.IdempotencyKey))
        {
            var existingOrder = await _databaseContext.Orders
                .Include(order => order.Items)
                .FirstOrDefaultAsync(order => order.CustomerId == customerId && order.IdempotencyKey == model.IdempotencyKey);

            if (existingOrder != null)
            {
                return (true, "Đơn hàng đã được ghi nhận trước đó.", existingOrder.Id, existingOrder.Code);
            }
        }

        var cart = await _cartService.GetCartAsync();
        if (cart.Items.Count == 0)
        {
            return (false, "Giỏ hàng của bạn đang trống.", null, null);
        }

        // Quy định an toàn y tế: Thuốc kê đơn (Rx) không được đặt trực tiếp qua web
        if (cart.Items.Any(item => item.IsPrescription))
        {
            return (false, "Giỏ hàng có chứa thuốc kê đơn (Rx). Thuốc kê đơn cần gửi ảnh đơn để dược sĩ tư vấn.", null, null);
        }

        var customer = await _databaseContext.CustomerProfiles
            .FirstOrDefaultAsync(profile => profile.Id == customerId);
        if (customer == null || customer.IsLocked)
        {
            return (false, "Tài khoản khách hàng không hợp lệ hoặc đã bị tạm khóa.", null, null);
        }

        // Kiểm tra tính sẵn sàng của phương thức chuyển khoản ngân hàng (BS-05)
        bool isBankTransfer = model.PaymentMethod == "BANK_TRANSFER";
        if (isBankTransfer)
        {
            bool isBankConfigured = _bankTransferOptions.IsEnabled &&
                                   !string.IsNullOrWhiteSpace(_bankTransferOptions.BankName) &&
                                   !string.IsNullOrWhiteSpace(_bankTransferOptions.AccountNumber);

            if (!isBankConfigured)
            {
                return (false, "Phương thức chuyển khoản ngân hàng hiện chưa sẵn sàng do chưa cấu hình tài khoản nhận tiền. Quý khách vui lòng chọn thanh toán khi nhận hàng (COD).", null, null);
            }
        }

        // Mở transaction Serializable để đảm bảo tranh chấp tồn kho và voucher được xử lý an toàn tuyệt đối
        using var transaction = await _databaseContext.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
        try
        {
            var today = PharmacyClock.Today;
            var medicineIds = cart.Items
                .Select(item => item.MedicineId)
                .Distinct()
                .ToList();

            // Tải thông tin thuốc kèm các lô hàng còn tồn từ database
            var medicines = await _databaseContext.Medicines
                .Include(medicine => medicine.Batches)
                .Where(medicine => medicineIds.Contains(medicine.Id))
                .ToListAsync();

            decimal calculatedSubtotal = 0m;
            var orderItems = new List<OrderItem>();
            var allocations = new List<OrderBatchAllocation>();

            foreach (var cartItem in cart.Items)
            {
                var medicine = medicines.FirstOrDefault(m => m.Id == cartItem.MedicineId);
                if (medicine == null || !medicine.IsActive || !medicine.IsSelling)
                {
                    await transaction.RollbackAsync();
                    return (false, $"Sản phẩm '{cartItem.MedicineName}' hiện không còn kinh doanh hoặc đã ẩn.", null, null);
                }

                // Nguyên tắc FEFO: Lọc các lô không bị khóa và còn hạn dùng, sắp xếp hạn dùng gần nhất lên trước
                var candidateBatches = medicine.Batches
                    .Where(batch => !batch.IsBlocked && batch.ExpiryDate >= today)
                    .OrderBy(batch => batch.ExpiryDate)
                    .ThenBy(batch => batch.Id)
                    .ToList();

                int totalAvailableStock = candidateBatches.Sum(batch => Math.Max(0, batch.Stock - batch.ReservedQuantity));
                if (totalAvailableStock < cartItem.Quantity)
                {
                    await transaction.RollbackAsync();
                    return (false, $"Sản phẩm '{medicine.Name}' không đủ số lượng có thể bán (chỉ còn {totalAvailableStock} {medicine.Unit}).", null, null);
                }

                // Phân bổ số lượng cần mua trên các lô theo thứ tự ưu tiên FEFO
                int remainingQuantityToAllocate = cartItem.Quantity;
                foreach (var batch in candidateBatches)
                {
                    if (remainingQuantityToAllocate <= 0)
                    {
                        break;
                    }

                    int availableInThisBatch = Math.Max(0, batch.Stock - batch.ReservedQuantity);
                    if (availableInThisBatch <= 0)
                    {
                        continue;
                    }

                    int quantityFromBatch = Math.Min(availableInThisBatch, remainingQuantityToAllocate);

                    // Tăng lượng giữ trên lô để POS nhận biết và không bán lượng này
                    batch.ReservedQuantity += quantityFromBatch;
                    remainingQuantityToAllocate -= quantityFromBatch;

                    allocations.Add(new OrderBatchAllocation
                    {
                        BatchId = batch.Id,
                        Quantity = quantityFromBatch,
                        IsDeducted = false // Chưa xuất kho thật, chỉ đang giữ chỗ
                    });
                }

                decimal itemTotal = medicine.Price * cartItem.Quantity;
                calculatedSubtotal += itemTotal;

                orderItems.Add(new OrderItem
                {
                    MedicineId = medicine.Id,
                    MedicineName = medicine.Name,
                    Unit = medicine.Unit,
                    Quantity = cartItem.Quantity,
                    UnitPrice = medicine.Price,
                    TotalPrice = itemTotal,
                    IsPrescriptionSnapshot = medicine.IsPrescription
                });
            }

            // Xử lý tính toán và trừ lượt sử dụng của voucher
            decimal discountAmount = 0m;
            string? validVoucherCode = null;

            if (!string.IsNullOrWhiteSpace(model.VoucherCode))
            {
                string cleanVoucherCode = model.VoucherCode.Trim().ToUpperInvariant();
                var voucher = await _databaseContext.Vouchers
                    .FirstOrDefaultAsync(v => v.Code.ToUpper() == cleanVoucherCode);

                bool isVoucherValid = voucher != null &&
                                     voucher.IsActive &&
                                     voucher.Quantity > 0 &&
                                     (!voucher.StartDate.HasValue || DateTime.UtcNow >= voucher.StartDate.Value) &&
                                     (!voucher.EndDate.HasValue || DateTime.UtcNow <= voucher.EndDate.Value) &&
                                     calculatedSubtotal >= voucher.MinOrderValue;

                if (isVoucherValid && voucher != null)
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

                    // Giảm số lượt còn lại của voucher trong cùng giao dịch
                    voucher.Quantity -= 1;
                    validVoucherCode = voucher.Code;
                }
            }

            // Tính phí giao hàng: Đơn từ 100.000 ₫ được miễn phí giao hàng
            decimal shippingFee = (calculatedSubtotal >= 100000m || calculatedSubtotal == 0m) ? 0m : 15000m;
            decimal totalAmount = Math.Max(0m, calculatedSubtotal - discountAmount + shippingFee);

            // Sinh mã đơn hàng duy nhất dạng HDyymmddxxxx
            string orderCode = GenerateOrderCode();
            while (await _databaseContext.Orders.AnyAsync(order => order.Code == orderCode))
            {
                orderCode = GenerateOrderCode();
            }

            string paymentMethod = isBankTransfer ? "BANK_TRANSFER" : "COD";
            string paymentStatus = isBankTransfer ? "PENDING_TRANSFER" : "UNPAID";

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
                PaymentMethod = paymentMethod,
                PaymentStatus = paymentStatus,
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
                        Text = $"Đặt đơn hàng thành công qua kênh Web ({paymentMethod}). Tổng tiền: {totalAmount:N0} ₫"
                    }
                }
            };

            _databaseContext.Orders.Add(order);
            await _databaseContext.SaveChangesAsync();
            await transaction.CommitAsync();

            // Xóa sạch giỏ hàng trong session sau khi đã lưu đơn thành công
            await _cartService.ClearCartAsync();

            return (true, "Đặt hàng thành công!", order.Id, order.Code);
        }
        catch (Exception exception)
        {
            await transaction.RollbackAsync();
            return (false, $"Lỗi hệ thống khi xử lý đơn hàng: {exception.Message}", null, null);
        }
    }

    /// <summary>
    /// Lấy thông tin kết quả đặt hàng kèm dữ liệu tài khoản ngân hàng cấu hình.
    /// </summary>
    public async Task<OrderResultViewModel?> GetOrderResultAsync(int orderId, int customerId, bool isAdmin = false)
    {
        var query = _databaseContext.Orders
            .AsNoTracking()
            .Include(order => order.Items)
            .Where(order => order.Id == orderId);

        if (!isAdmin)
        {
            query = query.Where(order => order.CustomerId == customerId);
        }

        var order = await query.FirstOrDefaultAsync();
        if (order == null)
        {
            return null;
        }

        var itemsViewModel = order.Items.Select(item => new OrderItemViewModel
        {
            MedicineName = item.MedicineName,
            Unit = item.Unit,
            Quantity = item.Quantity,
            UnitPrice = item.UnitPrice,
            TotalPrice = item.TotalPrice
        }).ToList();

        var bankTransferInfo = new BankTransferInfoViewModel
        {
            IsEnabled = _bankTransferOptions.IsEnabled,
            IsDemoAccount = _bankTransferOptions.IsDemoAccount,
            BankName = _bankTransferOptions.BankName,
            AccountNumber = _bankTransferOptions.AccountNumber,
            AccountHolder = _bankTransferOptions.AccountHolder,
            TransferInstruction = _bankTransferOptions.TransferInstruction,
            DemoNotice = _bankTransferOptions.DemoNotice
        };

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
            BankTransferInfo = bankTransferInfo,
            Items = itemsViewModel
        };
    }

    /// <summary>
    /// Lấy danh sách đơn hàng của khách hàng theo phân trang.
    /// </summary>
    public async Task<PagedResult<MyOrderListViewModel>> GetCustomerOrdersAsync(int customerId, int page, int pageSize)
    {
        var query = _databaseContext.Orders
            .AsNoTracking()
            .Include(order => order.Items)
            .Where(order => order.CustomerId == customerId)
            .OrderByDescending(order => order.OrderDate);

        int totalCount = await query.CountAsync();
        var orderList = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(order => new MyOrderListViewModel
            {
                Id = order.Id,
                Code = order.Code,
                OrderDate = order.OrderDate,
                OrderStatus = order.OrderStatus,
                PaymentStatus = order.PaymentStatus,
                ShippingStatus = order.ShippingStatus,
                TotalAmount = order.TotalAmount,
                TotalItemCount = order.Items.Sum(item => item.Quantity),
                Items = order.Items.Select(item => new OrderItemViewModel
                {
                    MedicineName = item.MedicineName,
                    Unit = item.Unit,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    TotalPrice = item.TotalPrice
                }).ToList()
            })
            .ToListAsync();

        return new PagedResult<MyOrderListViewModel>
        {
            Items = orderList,
            Page = page,
            PageSize = pageSize,
            TotalItems = totalCount
        };
    }

    /// <summary>
    /// Khách hàng tự hủy đơn hàng khi đơn còn ở trạng thái Chờ xác nhận (NEW).
    /// </summary>
    public async Task<(bool Success, string Message)> CustomerCancelOrderAsync(int orderId, int customerId, string reason)
    {
        var order = await _databaseContext.Orders
            .Include(o => o.Allocations)
            .ThenInclude(allocation => allocation.Batch)
            .FirstOrDefaultAsync(o => o.Id == orderId && o.CustomerId == customerId);

        if (order == null)
        {
            return (false, "Không tìm thấy đơn hàng hoặc bạn không có quyền hủy đơn này.");
        }

        if (order.OrderStatus != "NEW")
        {
            return (false, "Chỉ có thể hủy đơn hàng khi đơn đang ở trạng thái 'Chờ xác nhận' (NEW).");
        }

        using var transaction = await _databaseContext.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
        try
        {
            // Trả lại số lượng đã giữ trên các lô hàng về kho
            foreach (var allocation in order.Allocations)
            {
                if (allocation.Batch != null && !allocation.IsDeducted)
                {
                    allocation.Batch.ReservedQuantity = Math.Max(0, allocation.Batch.ReservedQuantity - allocation.Quantity);
                }
            }

            // Hoàn lại lượt sử dụng voucher nếu đơn có dùng
            if (!string.IsNullOrWhiteSpace(order.VoucherCode))
            {
                var voucher = await _databaseContext.Vouchers
                    .FirstOrDefaultAsync(v => v.Code == order.VoucherCode);
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

            await _databaseContext.SaveChangesAsync();
            await transaction.CommitAsync();

            return (true, "Đã hủy đơn hàng thành công.");
        }
        catch (Exception exception)
        {
            await transaction.RollbackAsync();
            return (false, $"Lỗi khi hủy đơn hàng: {exception.Message}");
        }
    }

    /// <summary>
    /// Lấy danh sách đơn hàng cho Quản trị viên kèm bộ lọc trạng thái và từ khóa.
    /// </summary>
    public async Task<PagedResult<Order>> GetAdminOrdersAsync(string? status, string? paymentStatus, string? keyword, int page, int pageSize)
    {
        var query = _databaseContext.Orders
            .AsNoTracking()
            .Include(order => order.Items)
            .OrderByDescending(order => order.OrderDate)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(status))
        {
            query = query.Where(order => order.OrderStatus == status);
        }

        if (!string.IsNullOrWhiteSpace(paymentStatus))
        {
            query = query.Where(order => order.PaymentStatus == paymentStatus);
        }

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            string keywordLower = keyword.Trim().ToLower();
            query = query.Where(order => order.Code.ToLower().Contains(keywordLower) ||
                                         order.CustomerName.ToLower().Contains(keywordLower) ||
                                         order.CustomerPhone.Contains(keywordLower));
        }

        int totalCount = await query.CountAsync();
        var orders = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Order>
        {
            Items = orders,
            Page = page,
            PageSize = pageSize,
            TotalItems = totalCount
        };
    }

    /// <summary>
    /// Lấy chi tiết đơn hàng cho Quản trị viên kèm toàn bộ thông tin phân bổ lô và lịch sử trạng thái.
    /// </summary>
    public async Task<Order?> GetOrderDetailsForAdminAsync(int orderId)
    {
        return await _databaseContext.Orders
            .AsNoTracking()
            .Include(order => order.Items)
            .Include(order => order.Allocations).ThenInclude(allocation => allocation.Batch)
            .Include(order => order.History.OrderBy(historyItem => historyItem.Time))
            .FirstOrDefaultAsync(order => order.Id == orderId);
    }

    /// <summary>
    /// Quản trị viên cập nhật trạng thái đơn hàng theo đúng vòng đời:
    /// - NEW -> CONFIRMED, CANCELLED
    /// - CONFIRMED -> SHIPPING, CANCELLED
    /// - SHIPPING -> COMPLETED
    /// Tuân thủ nguyên tắc BS-01 (chặn hủy đơn đã thanh toán) và BS-02 (kiểm tra khóa lô trước khi hoàn tất).
    /// </summary>
    public async Task<(bool Success, string Message)> UpdateOrderStatusByAdminAsync(int orderId, string newStatus, string? cancelReason, string employeeName)
    {
        var order = await _databaseContext.Orders
            .Include(o => o.Allocations).ThenInclude(allocation => allocation.Batch)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null)
        {
            return (false, "Không tìm thấy đơn hàng.");
        }

        var allowedNextStatuses = order.OrderStatus switch
        {
            "NEW" => new[] { "CONFIRMED", "CANCELLED" },
            "CONFIRMED" => new[] { "SHIPPING", "CANCELLED" },
            "SHIPPING" => new[] { "COMPLETED" },
            _ => Array.Empty<string>()
        };

        if (!allowedNextStatuses.Contains(newStatus))
        {
            return (false, "Không thể chuyển đơn sang trạng thái này.");
        }

        if (newStatus == "COMPLETED" && order.PaymentStatus != "PAID")
        {
            return (false, "Vui lòng dùng thao tác xác nhận thu tiền để hoàn tất đơn.");
        }

        if (order.OrderStatus == "COMPLETED" || order.OrderStatus == "CANCELLED")
        {
            return (false, $"Đơn hàng đã ở trạng thái {order.OrderStatus}, không thể chuyển tiếp.");
        }

        using var transaction = await _databaseContext.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
        try
        {
            if (newStatus == "CANCELLED")
            {
                // BS-01: Chặn hủy trực tiếp khi đơn đã thanh toán. Phải qua luồng hoàn tiền rõ ràng.
                if (order.PaymentStatus == "PAID")
                {
                    return (false, "Đơn hàng đã thanh toán không thể hủy trực tiếp mà không xử lý hoàn tiền. Vui lòng sử dụng chức năng hoàn tiền.");
                }

                foreach (var allocation in order.Allocations)
                {
                    if (allocation.Batch != null && !allocation.IsDeducted)
                    {
                        allocation.Batch.ReservedQuantity = Math.Max(0, allocation.Batch.ReservedQuantity - allocation.Quantity);
                    }
                }

                if (!string.IsNullOrWhiteSpace(order.VoucherCode))
                {
                    var voucher = await _databaseContext.Vouchers
                        .FirstOrDefaultAsync(v => v.Code == order.VoucherCode);
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
                var today = PharmacyClock.Today;
                bool hasInvalidBatch = order.Allocations.Any(allocation =>
                    allocation.Batch == null || allocation.Batch.IsBlocked || allocation.Batch.ExpiryDate < today);

                if (hasInvalidBatch)
                {
                    return (false, "Lô hàng đã khóa hoặc hết hạn. Chưa thể giao đơn.");
                }

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
                // BS-02: Kiểm tra toàn bộ các lô phân bổ trước khi xuất kho vĩnh viễn
                var today = PharmacyClock.Today;
                var invalidAllocation = order.Allocations.FirstOrDefault(allocation =>
                    !allocation.IsDeducted &&
                    (allocation.Batch == null || allocation.Batch.IsBlocked || allocation.Batch.ExpiryDate < today));

                if (invalidAllocation != null)
                {
                    string batchIdentifier = invalidAllocation.Batch != null
                        ? $"Lô '{invalidAllocation.Batch.BatchNumber}'"
                        : "Một lô hàng trong đơn";

                    string failureReason = invalidAllocation.Batch == null
                        ? "không tồn tại"
                        : (invalidAllocation.Batch.IsBlocked
                            ? "đang bị khóa"
                            : $"đã hết hạn (HSD: {invalidAllocation.Batch.ExpiryDate:dd/MM/yyyy})");

                    return (false, $"{batchIdentifier} {failureReason}. Không thể hoàn tất đơn hàng. Vui lòng kiểm tra và xử lý lô hàng trong kho trước.");
                }

                // Trừ tồn kho thực tế và giải phóng lượng hàng đã giữ
                foreach (var allocation in order.Allocations)
                {
                    if (allocation.Batch != null && !allocation.IsDeducted)
                    {
                        allocation.Batch.Stock = Math.Max(0, allocation.Batch.Stock - allocation.Quantity);
                        allocation.Batch.ReservedQuantity = Math.Max(0, allocation.Batch.ReservedQuantity - allocation.Quantity);
                        allocation.IsDeducted = true;
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

            await _databaseContext.SaveChangesAsync();
            await transaction.CommitAsync();

            return (true, $"Đã cập nhật trạng thái đơn hàng sang '{newStatus}'.");
        }
        catch (Exception exception)
        {
            await transaction.RollbackAsync();
            return (false, $"Lỗi cập nhật trạng thái đơn: {exception.Message}");
        }
    }

    /// <summary>
    /// Xác nhận thu tiền cho đơn hàng:
    /// - Với Bank Transfer: Chỉ ghi nhận thanh toán PAID, không ép hoàn tất sớm trước khi xuất kho.
    /// - Với COD: Xác nhận thu tiền mặt và hoàn tất trừ kho vĩnh viễn (kèm kiểm tra an toàn lô BS-02).
    /// </summary>
    public async Task<(bool Success, string Message)> ConfirmCodPaymentAsync(int orderId, string employeeName)
    {
        var order = await _databaseContext.Orders
            .Include(o => o.Allocations).ThenInclude(allocation => allocation.Batch)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null)
        {
            return (false, "Không tìm thấy đơn hàng.");
        }

        if (order.PaymentStatus == "PAID")
        {
            return (false, "Đơn hàng này đã được xác nhận thanh toán trước đó.");
        }

        if (order.OrderStatus == "CANCELLED")
        {
            return (false, "Không thể xác nhận thanh toán cho đơn hàng đã hủy.");
        }

        bool isBankTransfer = order.PaymentMethod == "BANK_TRANSFER";

        if (!isBankTransfer && order.OrderStatus is not ("CONFIRMED" or "SHIPPING"))
        {
            return (false, "Chỉ xác nhận thu tiền COD cho đơn đã xác nhận hoặc đang giao.");
        }

        // BS-02: Kiểm tra toàn vẹn của lô thuốc trước khi thao tác tiền/kho
        var today = PharmacyClock.Today;
        var invalidAllocation = order.Allocations.FirstOrDefault(allocation =>
            !allocation.IsDeducted &&
            (allocation.Batch == null || allocation.Batch.IsBlocked || allocation.Batch.ExpiryDate < today));

        if (invalidAllocation != null)
        {
            string batchIdentifier = invalidAllocation.Batch != null
                ? $"Lô '{invalidAllocation.Batch.BatchNumber}'"
                : "Một lô hàng trong đơn";

            string failureReason = invalidAllocation.Batch == null
                ? "không tồn tại"
                : (invalidAllocation.Batch.IsBlocked
                    ? "đang bị khóa"
                    : $"đã hết hạn (HSD: {invalidAllocation.Batch.ExpiryDate:dd/MM/yyyy})");

            return (false, $"{batchIdentifier} {failureReason}. Không thể xác nhận thanh toán/hoàn tất đơn. Vui lòng kiểm tra lô hàng trong kho trước.");
        }

        using var transaction = await _databaseContext.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
        try
        {
            order.PaymentStatus = "PAID";
            order.PaidAt = DateTime.UtcNow;
            order.PaidByEmployee = employeeName;

            if (isBankTransfer)
            {
                // Với chuyển khoản ngân hàng: Ghi nhận tiền về, đơn sẵn sàng để đóng gói xuất giao
                order.OrderVersion += 1;
                order.History.Add(new OrderStatusHistory
                {
                    OrderId = order.Id,
                    Time = DateTime.UtcNow,
                    Actor = employeeName,
                    Text = $"Xác nhận đã nhận chuyển khoản ngân hàng: {order.TotalAmount:N0} ₫."
                });

                await _databaseContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return (true, $"Đã xác nhận nhận chuyển khoản {order.TotalAmount:N0} ₫ cho đơn {order.Code}. Đơn hàng sẵn sàng để xuất kho giao hàng.");
            }
            else
            {
                // Với COD: Thu tiền mặt khi bưu tá giao thành công -> hoàn tất xuất kho vĩnh viễn
                if (order.OrderStatus != "COMPLETED")
                {
                    foreach (var allocation in order.Allocations)
                    {
                        if (allocation.Batch != null && !allocation.IsDeducted)
                        {
                            allocation.Batch.Stock = Math.Max(0, allocation.Batch.Stock - allocation.Quantity);
                            allocation.Batch.ReservedQuantity = Math.Max(0, allocation.Batch.ReservedQuantity - allocation.Quantity);
                            allocation.IsDeducted = true;
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
                    Text = $"Xác nhận đã thu tiền COD: {order.TotalAmount:N0} ₫ và hoàn tất đơn hàng."
                });

                await _databaseContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return (true, $"Đã xác nhận thu tiền COD {order.TotalAmount:N0} ₫ và hoàn tất đơn {order.Code}.");
            }
        }
        catch (Exception exception)
        {
            await transaction.RollbackAsync();
            return (false, $"Lỗi xác nhận thanh toán: {exception.Message}");
        }
    }

    /// <summary>
    /// Xử lý hoàn tiền cho đơn hàng đã thanh toán (BS-01):
    /// - Đơn hàng chuyển sang CANCELLED, trạng thái thanh toán REFUNDED.
    /// - Bảo toàn nguyên vẹn lịch sử thu tiền gốc (PaidAt, PaidByEmployee).
    /// - Lưu vết số tiền hoàn, ngày hoàn, người thực hiện và lý do.
    /// - Giải phóng hàng giữ trong kho và phục hồi lượt voucher.
    /// - Chặn lặp lại thao tác hoàn tiền hai lần.
    /// </summary>
    public async Task<(bool Success, string Message)> RefundOrderAsync(int orderId, string refundReason, string employeeName)
    {
        if (string.IsNullOrWhiteSpace(refundReason))
        {
            return (false, "Vui lòng nhập lý do hoàn tiền.");
        }

        var order = await _databaseContext.Orders
            .Include(o => o.Allocations).ThenInclude(allocation => allocation.Batch)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null)
        {
            return (false, "Không tìm thấy đơn hàng.");
        }

        // Chống hoàn tiền lặp lại (Idempotent guard)
        if (order.IsRefunded || order.PaymentStatus == "REFUNDED")
        {
            return (false, "Đơn hàng này đã được xử lý hoàn tiền trước đó, không thể hoàn tiền hai lần.");
        }

        if (order.PaymentStatus != "PAID")
        {
            return (false, "Chỉ có thể hoàn tiền cho đơn hàng đã ghi nhận thanh toán (PAID).");
        }

        // KT-01: Chặn hoàn tiền và giải phóng tồn sớm khi đơn đang xuất giao (SHIPPING)
        if (order.OrderStatus == "SHIPPING")
        {
            return (false, "Đơn hàng đang giao không thể hoàn tiền theo luồng này. Cần xử lý giao thất bại hoặc nhận hàng trả trước khi hoàn tiền.");
        }

        if (order.OrderStatus == "COMPLETED")
        {
            return (false, "Đơn hàng đã hoàn tất giao hàng thành công, không thể hủy hoàn tiền theo luồng này.");
        }

        if (order.OrderStatus != "NEW" && order.OrderStatus != "CONFIRMED")
        {
            return (false, $"Không thể hoàn tiền cho đơn hàng ở trạng thái {order.OrderStatus}. Chỉ hỗ trợ đơn chưa xuất giao (Chờ xác nhận hoặc Đã xác nhận).");
        }

        using var transaction = await _databaseContext.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
        try
        {
            // 1. Giải phóng tồn kho giữ chỗ nếu chưa xuất kho
            foreach (var allocation in order.Allocations)
            {
                if (allocation.Batch != null && !allocation.IsDeducted)
                {
                    allocation.Batch.ReservedQuantity = Math.Max(0, allocation.Batch.ReservedQuantity - allocation.Quantity);
                }
            }

            // 2. Phục hồi lượt sử dụng voucher nếu có dùng
            if (!string.IsNullOrWhiteSpace(order.VoucherCode))
            {
                var voucher = await _databaseContext.Vouchers
                    .FirstOrDefaultAsync(v => v.Code == order.VoucherCode);
                if (voucher != null)
                {
                    voucher.Quantity += 1;
                }
            }

            // 3. Cập nhật thông tin hoàn tiền nhưng giữ nguyên ngày giờ thu tiền gốc
            string trimmedReason = refundReason.Trim();
            order.OrderStatus = "CANCELLED";
            order.ShippingStatus = "CANCELLED";
            order.PaymentStatus = "REFUNDED";
            order.IsRefunded = true;
            order.RefundAmount = order.TotalAmount;
            order.RefundedAt = DateTime.UtcNow;
            order.RefundedByEmployee = employeeName;
            order.RefundReason = trimmedReason;
            order.CancelReason = $"Đã hoàn tiền & Hủy đơn. Lý do: {trimmedReason}";
            order.OrderVersion += 1;

            // 4. Ghi vết kiểm toán
            order.History.Add(new OrderStatusHistory
            {
                OrderId = order.Id,
                Time = DateTime.UtcNow,
                Actor = employeeName,
                Text = $"Đã hoàn trả {order.RefundAmount:N0} ₫ cho khách hàng và hủy đơn. Lý do: {trimmedReason}"
            });

            await _databaseContext.SaveChangesAsync();
            await transaction.CommitAsync();

            return (true, $"Đã xử lý hoàn tiền {order.RefundAmount:N0} ₫ và hủy đơn hàng {order.Code} thành công.");
        }
        catch (Exception exception)
        {
            await transaction.RollbackAsync();
            return (false, $"Lỗi khi xử lý hoàn tiền đơn hàng: {exception.Message}");
        }
    }

    private static string GenerateOrderCode()
    {
        return $"HD{DateTime.UtcNow:yyMMdd}{new Random().Next(1000, 9999)}";
    }
}
