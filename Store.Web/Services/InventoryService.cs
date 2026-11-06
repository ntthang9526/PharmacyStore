using Microsoft.EntityFrameworkCore;
using PharmacyWeb.Data;
using PharmacyWeb.Data.Entities;
using PharmacyWeb.Models.ViewModels;

namespace PharmacyWeb.Services;

public interface IInventoryService
{
    Task<PagedResult<Batch>> GetBatchesAsync(string? filterType, int? medicineId, int page, int pageSize);
    Task<bool> ToggleBatchLockAsync(int batchId);
    Task<(bool Success, string Message, int? StockInId)> CreateStockInAsync(StockInFormViewModel model, string employeeName);
    Task<PagedResult<StockIn>> GetStockInsAsync(int page, int pageSize);
    Task<StockIn?> GetStockInDetailsAsync(int id);
}

public class InventoryService : IInventoryService
{
    private readonly PharmacyWebDbContext _databaseContext;

    public InventoryService(PharmacyWebDbContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    /// <summary>
    /// Tra cứu danh sách các lô thuốc trong kho theo phân trang và bộ lọc:
    /// - EXPIRING_SOON: Lô thuốc sắp hết hạn (trong vòng 90 ngày tới).
    /// - EXPIRED: Lô thuốc đã quá hạn sử dụng.
    /// - LOW_STOCK: Lô thuốc có số lượng tồn thấp (<= 5 đơn vị).
    /// - BLOCKED: Lô thuốc đang bị khóa tạm thời.
    /// </summary>
    public async Task<PagedResult<Batch>> GetBatchesAsync(string? filterType, int? medicineId, int page, int pageSize)
    {
        var today = PharmacyClock.Today;
        var expiringSoonThresholdDate = today.AddDays(90);

        var query = _databaseContext.Batches
            .AsNoTracking()
            .Include(batch => batch.Medicine)
            .OrderBy(batch => batch.ExpiryDate)
            .AsQueryable();

        if (medicineId.HasValue && medicineId.Value > 0)
        {
            query = query.Where(batch => batch.MedicineId == medicineId.Value);
        }

        query = filterType switch
        {
            "EXPIRING_SOON" => query.Where(batch => batch.ExpiryDate >= today && batch.ExpiryDate <= expiringSoonThresholdDate),
            "EXPIRED" => query.Where(batch => batch.ExpiryDate < today),
            "LOW_STOCK" => query.Where(batch => batch.Stock <= 5),
            "BLOCKED" => query.Where(batch => batch.IsBlocked),
            _ => query
        };

        int totalCount = await query.CountAsync();
        var batches = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Batch>
        {
            Items = batches,
            Page = page,
            PageSize = pageSize,
            TotalItems = totalCount
        };
    }

    /// <summary>
    /// Bật hoặc tắt trạng thái khóa của một lô thuốc (IsBlocked).
    /// Khi một lô bị khóa, nó sẽ lập tức bị loại khỏi tồn có thể bán và chặn mọi xuất kho liên quan.
    /// </summary>
    public async Task<bool> ToggleBatchLockAsync(int batchId)
    {
        var batch = await _databaseContext.Batches.FindAsync(batchId);
        if (batch == null)
        {
            return false;
        }

        batch.IsBlocked = !batch.IsBlocked;
        await _databaseContext.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Lập phiếu nhập kho thuốc mới với nhiều dòng:
    /// - Kiểm tra hợp lệ Nhà cung cấp.
    /// - Kiểm tra các thuốc nhập phải thuộc đúng Nhà cung cấp đã chọn.
    /// - Kiểm tra hạn sử dụng của thuốc phải sau ngày hiện tại.
    /// - Cập nhật tăng số lượng tồn vào lô cũ (nếu trùng số lô và hạn dùng) hoặc tạo lô mới.
    /// </summary>
    public async Task<(bool Success, string Message, int? StockInId)> CreateStockInAsync(StockInFormViewModel model, string employeeName)
    {
        // 1. Kiểm tra điều kiện cơ bản của phiếu nhập
        if (model.SupplierId <= 0 || model.Lines.Count == 0)
        {
            return (false, "Phiếu nhập phải có nhà cung cấp và ít nhất một dòng thuốc.", null);
        }

        bool isSupplierValid = await _databaseContext.Suppliers
            .AnyAsync(supplier => supplier.Id == model.SupplierId && supplier.IsActive);
        if (!isSupplierValid)
        {
            return (false, "Nhà cung cấp không hợp lệ hoặc đã bị tạm ngưng.", null);
        }

        // 2. Kiểm tra các thuốc nhập có thuộc đúng nhà cung cấp này không
        var requestedMedicineIds = model.Lines
            .Select(line => line.MedicineId)
            .Distinct()
            .ToList();

        var allowedMedicineIds = await _databaseContext.Medicines
            .Where(medicine => requestedMedicineIds.Contains(medicine.Id) &&
                               medicine.SupplierId == model.SupplierId &&
                               medicine.IsActive)
            .Select(medicine => medicine.Id)
            .ToListAsync();

        if (allowedMedicineIds.Count != requestedMedicineIds.Count)
        {
            return (false, "Có thuốc không thuộc nhà cung cấp đã chọn hoặc đã bị ẩn.", null);
        }

        // 3. Kiểm tra từng dòng thuốc: số lượng > 0, giá vốn > 0, có số lô và hạn dùng phải còn hiệu lực
        var today = PharmacyClock.Today;
        foreach (var line in model.Lines)
        {
            bool isQuantityInvalid = line.Quantity <= 0;
            bool isCostInvalid = line.UnitCost <= 0;
            bool isBatchNumberEmpty = string.IsNullOrWhiteSpace(line.BatchNumber);
            bool isExpiryDatePast = line.ExpiryDate.Date <= today;

            if (isQuantityInvalid || isCostInvalid || isBatchNumberEmpty || isExpiryDatePast)
            {
                return (false, "Vui lòng kiểm tra lại số lượng, giá nhập, số lô và hạn dùng của các dòng thuốc (Hạn dùng phải sau ngày hiện tại).", null);
            }
        }

        // 4. Tránh trùng lặp cùng một lô thuốc xuất hiện nhiều lần trong cùng một phiếu nhập
        bool hasDuplicateBatchInForm = model.Lines
            .GroupBy(line => new { line.MedicineId, LotNumber = line.BatchNumber.Trim(), Expiry = line.ExpiryDate.Date })
            .Any(group => group.Count() > 1);

        if (hasDuplicateBatchInForm)
        {
            return (false, "Mỗi lô thuốc chỉ được nhập một dòng duy nhất trong cùng phiếu nhập.", null);
        }

        using var transaction = await _databaseContext.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
        try
        {
            // Sinh mã phiếu nhập tự động dạng PNyyyy-xxx
            string stockInCode = GenerateStockInCode();
            while (await _databaseContext.StockIns.AnyAsync(stockIn => stockIn.Code == stockInCode))
            {
                stockInCode = GenerateStockInCode();
            }

            decimal totalCost = 0m;
            var stockInDetails = new List<StockInDetail>();

            foreach (var line in model.Lines)
            {
                decimal lineTotalCost = line.Quantity * line.UnitCost;
                totalCost += lineTotalCost;

                string trimmedBatchNumber = line.BatchNumber.Trim();

                stockInDetails.Add(new StockInDetail
                {
                    MedicineId = line.MedicineId,
                    BatchNumber = trimmedBatchNumber,
                    ExpiryDate = line.ExpiryDate,
                    Quantity = line.Quantity,
                    UnitCost = line.UnitCost,
                    TotalCost = lineTotalCost
                });

                // Cập nhật tăng tồn kho cho lô đã có sẵn, hoặc tạo mới bản ghi Lô hàng
                var existingBatch = await _databaseContext.Batches.FirstOrDefaultAsync(batch =>
                    batch.MedicineId == line.MedicineId &&
                    batch.BatchNumber == trimmedBatchNumber &&
                    batch.ExpiryDate == line.ExpiryDate.Date);

                if (existingBatch != null)
                {
                    existingBatch.Stock += line.Quantity;
                }
                else
                {
                    _databaseContext.Batches.Add(new Batch
                    {
                        MedicineId = line.MedicineId,
                        BatchNumber = trimmedBatchNumber,
                        ExpiryDate = line.ExpiryDate.Date,
                        Stock = line.Quantity,
                        ReservedQuantity = 0,
                        IsBlocked = false,
                        CreatedAt = DateTime.UtcNow
                    });
                }
            }

            var stockIn = new StockIn
            {
                Code = stockInCode,
                SupplierId = model.SupplierId,
                EmployeeName = employeeName,
                ImportDate = DateTime.UtcNow,
                TotalCost = totalCost,
                Note = model.Note,
                CreatedAt = DateTime.UtcNow,
                Details = stockInDetails
            };

            _databaseContext.StockIns.Add(stockIn);
            await _databaseContext.SaveChangesAsync();
            await transaction.CommitAsync();

            return (true, $"Lập phiếu nhập {stockIn.Code} thành công. Tổng tiền: {totalCost:N0} ₫", stockIn.Id);
        }
        catch (Exception exception)
        {
            await transaction.RollbackAsync();
            return (false, $"Lỗi lập phiếu nhập kho: {exception.Message}", null);
        }
    }

    /// <summary>
    /// Lấy danh sách phiếu nhập kho theo phân trang.
    /// </summary>
    public async Task<PagedResult<StockIn>> GetStockInsAsync(int page, int pageSize)
    {
        var query = _databaseContext.StockIns
            .AsNoTracking()
            .Include(stockIn => stockIn.Supplier)
            .Include(stockIn => stockIn.Details).ThenInclude(detail => detail.Medicine)
            .OrderByDescending(stockIn => stockIn.ImportDate);

        int totalCount = await query.CountAsync();
        var stockIns = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<StockIn>
        {
            Items = stockIns,
            Page = page,
            PageSize = pageSize,
            TotalItems = totalCount
        };
    }

    /// <summary>
    /// Lấy chi tiết thông tin và các dòng sản phẩm của một phiếu nhập kho.
    /// </summary>
    public async Task<StockIn?> GetStockInDetailsAsync(int id)
    {
        return await _databaseContext.StockIns
            .AsNoTracking()
            .Include(stockIn => stockIn.Supplier)
            .Include(stockIn => stockIn.Details).ThenInclude(detail => detail.Medicine)
            .FirstOrDefaultAsync(stockIn => stockIn.Id == id);
    }

    private static string GenerateStockInCode()
    {
        return $"PN{DateTime.UtcNow:yyyy}-{new Random().Next(100, 999)}";
    }
}
