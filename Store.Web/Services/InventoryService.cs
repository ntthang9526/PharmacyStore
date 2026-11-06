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
    private readonly PharmacyWebDbContext _db;

    public InventoryService(PharmacyWebDbContext db)
    {
        _db = db;
    }

    public async Task<PagedResult<Batch>> GetBatchesAsync(string? filterType, int? medicineId, int page, int pageSize)
    {
        var today = PharmacyWeb.Services.PharmacyClock.Today;
        var soonLimit = today.AddDays(90);

        var query = _db.Batches
            .AsNoTracking()
            .Include(b => b.Medicine)
            .OrderBy(b => b.ExpiryDate)
            .AsQueryable();

        if (medicineId.HasValue && medicineId.Value > 0)
        {
            query = query.Where(b => b.MedicineId == medicineId.Value);
        }

        query = filterType switch
        {
            "EXPIRING_SOON" => query.Where(b => b.ExpiryDate >= today && b.ExpiryDate <= soonLimit),
            "EXPIRED" => query.Where(b => b.ExpiryDate < today),
            "LOW_STOCK" => query.Where(b => b.Stock <= 5),
            "BLOCKED" => query.Where(b => b.IsBlocked),
            _ => query
        };

        var total = await query.CountAsync();
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PagedResult<Batch>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalItems = total
        };
    }

    public async Task<bool> ToggleBatchLockAsync(int batchId)
    {
        var batch = await _db.Batches.FindAsync(batchId);
        if (batch == null) return false;

        batch.IsBlocked = !batch.IsBlocked;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<(bool Success, string Message, int? StockInId)> CreateStockInAsync(StockInFormViewModel model, string employeeName)
    {
        if (model.SupplierId <= 0 || !model.Lines.Any())
        {
            return (false, "Phiếu nhập phải có nhà cung cấp và ít nhất một dòng thuốc.", null);
        }
        if (!await _db.Suppliers.AnyAsync(s => s.Id == model.SupplierId && s.IsActive))
            return (false, "Nhà cung cấp không hợp lệ.", null);
        var medicineIds = model.Lines.Select(l => l.MedicineId).Distinct().ToList();
        var allowedIds = await _db.Medicines.Where(m => medicineIds.Contains(m.Id) && m.SupplierId == model.SupplierId && m.IsActive)
            .Select(m => m.Id).ToListAsync();
        if (allowedIds.Count != medicineIds.Count)
            return (false, "Có thuốc không thuộc nhà cung cấp đã chọn hoặc đã bị ẩn.", null);
        if (model.Lines.Any(l => l.Quantity <= 0 || l.UnitCost <= 0 || string.IsNullOrWhiteSpace(l.BatchNumber) || l.ExpiryDate.Date <= PharmacyClock.Today))
            return (false, "Kiểm tra số lượng, giá nhập, số lô và hạn dùng của các dòng thuốc.", null);
        if (model.Lines.GroupBy(l => new { l.MedicineId, Lot = l.BatchNumber.Trim(), Date = l.ExpiryDate.Date }).Any(g => g.Count() > 1))
            return (false, "Mỗi lô thuốc chỉ nhập một dòng trong cùng phiếu.", null);

        using var transaction = await _db.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
        try
        {
            var code = $"PN{DateTime.UtcNow:yyyy}-{new Random().Next(100, 999)}";
            while (await _db.StockIns.AnyAsync(s => s.Code == code))
            {
                code = $"PN{DateTime.UtcNow:yyyy}-{new Random().Next(100, 999)}";
            }

            decimal totalCost = 0m;
            var details = new List<StockInDetail>();

            foreach (var line in model.Lines)
            {
                if (line.Quantity <= 0 || line.UnitCost < 0) continue;

                var lineTotal = line.Quantity * line.UnitCost;
                totalCost += lineTotal;

                details.Add(new StockInDetail
                {
                    MedicineId = line.MedicineId,
                    BatchNumber = line.BatchNumber.Trim(),
                    ExpiryDate = line.ExpiryDate,
                    Quantity = line.Quantity,
                    UnitCost = line.UnitCost,
                    TotalCost = lineTotal
                });

                // Update or create batch
                var batch = await _db.Batches.FirstOrDefaultAsync(b =>
                    b.MedicineId == line.MedicineId && b.BatchNumber == line.BatchNumber.Trim() && b.ExpiryDate == line.ExpiryDate.Date);

                if (batch != null)
                {
                    batch.Stock += line.Quantity;
                }
                else
                {
                    _db.Batches.Add(new Batch
                    {
                        MedicineId = line.MedicineId,
                        BatchNumber = line.BatchNumber.Trim(),
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
                Code = code,
                SupplierId = model.SupplierId,
                EmployeeName = employeeName,
                ImportDate = DateTime.UtcNow,
                TotalCost = totalCost,
                Note = model.Note,
                CreatedAt = DateTime.UtcNow,
                Details = details
            };

            _db.StockIns.Add(stockIn);
            await _db.SaveChangesAsync();
            await transaction.CommitAsync();

            return (true, $"Lập phiếu nhập {stockIn.Code} thành công. Tổng tiền: {totalCost:N0} ₫", stockIn.Id);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return (false, $"Lỗi lập phiếu nhập kho: {ex.Message}", null);
        }
    }

    public async Task<PagedResult<StockIn>> GetStockInsAsync(int page, int pageSize)
    {
        var query = _db.StockIns
            .AsNoTracking()
            .Include(s => s.Supplier)
            .Include(s => s.Details).ThenInclude(d => d.Medicine)
            .OrderByDescending(s => s.ImportDate);

        var total = await query.CountAsync();
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PagedResult<StockIn>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalItems = total
        };
    }

    public async Task<StockIn?> GetStockInDetailsAsync(int id)
    {
        return await _db.StockIns
            .AsNoTracking()
            .Include(s => s.Supplier)
            .Include(s => s.Details).ThenInclude(d => d.Medicine)
            .FirstOrDefaultAsync(s => s.Id == id);
    }
}
