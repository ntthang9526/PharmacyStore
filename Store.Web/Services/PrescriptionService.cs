using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PharmacyWeb.Data;
using PharmacyWeb.Data.Entities;
using PharmacyWeb.Models.ViewModels;

namespace PharmacyWeb.Services;

public interface IPrescriptionService
{
    Task<(bool Success, string Message, int? RequestId, string? Code)> CreateRequestAsync(int customerId, PrescriptionUploadViewModel model);
    Task<List<PrescriptionRequestItemViewModel>> GetCustomerRequestsAsync(int customerId);
    Task<PagedResult<PrescriptionRequestItemViewModel>> GetAdminRequestsAsync(string? status, int page, int pageSize);
    Task<PrescriptionRequestItemViewModel?> GetRequestByIdAsync(int id, int? customerId, bool isAdmin);
    Task<(bool Success, string Message)> RespondRequestAsync(int id, string pharmacistNote, string newStatus);
    Task<(Stream? Stream, string ContentType, string FileName)> GetSecureImageStreamAsync(int imageId, int? customerId, bool isAdmin);
}

public class PrescriptionService : IPrescriptionService
{
    private readonly PharmacyWebDbContext _db;
    private readonly IWebHostEnvironment _env;

    public PrescriptionService(PharmacyWebDbContext db, IWebHostEnvironment env)
    {
        _db = db;
        _env = env;
    }

    private string PrescriptionStorageDir => Path.Combine(_env.ContentRootPath, "App_Data", "Prescriptions");

    public async Task<(bool Success, string Message, int? RequestId, string? Code)> CreateRequestAsync(int customerId, PrescriptionUploadViewModel model)
    {
        if (model.Images == null || !model.Images.Any())
        {
            return (false, "Vui lòng đính kèm ít nhất 1 ảnh chụp đơn thuốc.", null, null);
        }

        var customer = await _db.CustomerProfiles.FirstOrDefaultAsync(c => c.Id == customerId);
        if (customer == null)
        {
            return (false, "Không tìm thấy hồ sơ khách hàng.", null, null);
        }

        if (!Directory.Exists(PrescriptionStorageDir))
        {
            Directory.CreateDirectory(PrescriptionStorageDir);
        }

        var code = $"RX-{DateTime.UtcNow:yyyy}-{new Random().Next(100, 999)}";
        while (await _db.PrescriptionRequests.AnyAsync(p => p.Code == code))
        {
            code = $"RX-{DateTime.UtcNow:yyyy}-{new Random().Next(100, 999)}";
        }

        var request = new PrescriptionRequest
        {
            Code = code,
            CustomerId = customerId,
            PatientName = model.PatientName.Trim(),
            Phone = model.Phone.Trim(),
            Status = "PENDING_REVIEW",
            CustomerNote = model.CustomerNote?.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };

        foreach (var file in model.Images)
        {
            if (file.Length == 0 || file.Length > 5 * 1024 * 1024) continue;

            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(ext)) continue;

            // Magic byte check
            using var stream = file.OpenReadStream();
            var header = new byte[8];
            var read = await stream.ReadAsync(header.AsMemory(0, header.Length));
            if (read < 4) continue;

            bool isJpeg = header[0] == 0xFF && header[1] == 0xD8 && header[2] == 0xFF;
            bool isPng = header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E && header[3] == 0x47;
            bool isWebp = header[0] == 0x52 && header[1] == 0x49 && header[2] == 0x46 && header[3] == 0x46;

            if (!isJpeg && !isPng && !isWebp) continue;

            var safeStoredName = $"{Guid.NewGuid():N}{ext}";
            var fullPath = Path.Combine(PrescriptionStorageDir, safeStoredName);

            using (var destStream = new FileStream(fullPath, FileMode.Create))
            {
                stream.Position = 0;
                await stream.CopyToAsync(destStream);
            }

            request.Images.Add(new PrescriptionImage
            {
                FileName = Path.GetFileName(file.FileName),
                StoredPath = safeStoredName,
                FileSizeBytes = file.Length,
                ContentType = file.ContentType
            });
        }

        if (!request.Images.Any())
        {
            return (false, "Ảnh đính kèm không đúng định dạng hợp lệ (chỉ chấp nhận JPEG, PNG, WebP dưới 5MB).", null, null);
        }

        _db.PrescriptionRequests.Add(request);
        await _db.SaveChangesAsync();

        return (true, "Đã gửi yêu cầu tư vấn đơn thuốc thành công. Dược sĩ sẽ kiểm tra và phản hồi sớm nhất.", request.Id, request.Code);
    }

    public async Task<List<PrescriptionRequestItemViewModel>> GetCustomerRequestsAsync(int customerId)
    {
        var items = await _db.PrescriptionRequests
            .AsNoTracking()
            .Include(p => p.Images)
            .Where(p => p.CustomerId == customerId)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();

        return items.Select(MapToViewModel).ToList();
    }

    public async Task<PagedResult<PrescriptionRequestItemViewModel>> GetAdminRequestsAsync(string? status, int page, int pageSize)
    {
        var query = _db.PrescriptionRequests
            .AsNoTracking()
            .Include(p => p.Images)
            .OrderByDescending(p => p.CreatedAt)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(status))
        {
            query = query.Where(p => p.Status == status);
        }

        var total = await query.CountAsync();
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PagedResult<PrescriptionRequestItemViewModel>
        {
            Items = items.Select(MapToViewModel).ToList(),
            Page = page,
            PageSize = pageSize,
            TotalItems = total
        };
    }

    public async Task<PrescriptionRequestItemViewModel?> GetRequestByIdAsync(int id, int? customerId, bool isAdmin)
    {
        var query = _db.PrescriptionRequests
            .AsNoTracking()
            .Include(p => p.Images)
            .Where(p => p.Id == id);

        if (!isAdmin && customerId.HasValue)
        {
            query = query.Where(p => p.CustomerId == customerId.Value);
        }

        var item = await query.FirstOrDefaultAsync();
        return item == null ? null : MapToViewModel(item);
    }

    public async Task<(bool Success, string Message)> RespondRequestAsync(int id, string pharmacistNote, string newStatus)
    {
        var req = await _db.PrescriptionRequests.FindAsync(id);
        if (req == null) return (false, "Không tìm thấy yêu cầu.");

        req.PharmacistNote = pharmacistNote.Trim();
        req.Status = newStatus;
        req.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return (true, "Đã lưu phản hồi cho khách hàng thành công.");
    }

    public async Task<(Stream? Stream, string ContentType, string FileName)> GetSecureImageStreamAsync(int imageId, int? customerId, bool isAdmin)
    {
        var img = await _db.PrescriptionImages
            .Include(pi => pi.PrescriptionRequest)
            .FirstOrDefaultAsync(pi => pi.Id == imageId);

        if (img == null || img.PrescriptionRequest == null)
        {
            return (null, string.Empty, string.Empty);
        }

        // Ownership check: must be admin OR the customer who created it!
        if (!isAdmin && (!customerId.HasValue || img.PrescriptionRequest.CustomerId != customerId.Value))
        {
            return (null, string.Empty, string.Empty);
        }

        var fullPath = Path.Combine(PrescriptionStorageDir, img.StoredPath);
        if (!File.Exists(fullPath))
        {
            return (null, string.Empty, string.Empty);
        }

        var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read);
        return (stream, img.ContentType, img.FileName);
    }

    private static PrescriptionRequestItemViewModel MapToViewModel(PrescriptionRequest p)
    {
        return new PrescriptionRequestItemViewModel
        {
            Id = p.Id,
            Code = p.Code,
            PatientName = p.PatientName,
            Phone = p.Phone,
            CreatedAt = p.CreatedAt,
            Status = p.Status,
            CustomerNote = p.CustomerNote,
            PharmacistNote = p.PharmacistNote,
            UpdatedAt = p.UpdatedAt,
            Images = p.Images.Select(i => new PrescriptionImageViewModel
            {
                Id = i.Id,
                FileName = i.FileName,
                FileSizeBytes = i.FileSizeBytes,
                ContentType = i.ContentType
            }).ToList()
        };
    }
}
