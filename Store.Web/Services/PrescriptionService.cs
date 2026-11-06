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
    private readonly PharmacyWebDbContext _databaseContext;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public PrescriptionService(PharmacyWebDbContext databaseContext, IWebHostEnvironment webHostEnvironment)
    {
        _databaseContext = databaseContext;
        _webHostEnvironment = webHostEnvironment;
    }

    /// <summary>
    /// Thư mục lưu trữ riêng tư cho ảnh đơn thuốc bệnh nhân.
    /// Đặt trong App_Data (ngoài wwwroot) để bảo vệ quyền riêng tư y tế, ngăn người ngoài truy cập trực tiếp qua URL.
    /// </summary>
    private string PrescriptionStorageDirectory => Path.Combine(_webHostEnvironment.ContentRootPath, "App_Data", "Prescriptions");

    /// <summary>
    /// Khách hàng gửi ảnh toa thuốc kèm triệu chứng để Dược sĩ tư vấn.
    /// </summary>
    public async Task<(bool Success, string Message, int? RequestId, string? Code)> CreateRequestAsync(int customerId, PrescriptionUploadViewModel model)
    {
        if (model.Images == null || model.Images.Count == 0)
        {
            return (false, "Vui lòng đính kèm ít nhất 1 ảnh chụp đơn thuốc.", null, null);
        }

        var customer = await _databaseContext.CustomerProfiles
            .FirstOrDefaultAsync(profile => profile.Id == customerId);
        if (customer == null)
        {
            return (false, "Không tìm thấy hồ sơ khách hàng.", null, null);
        }

        if (!Directory.Exists(PrescriptionStorageDirectory))
        {
            Directory.CreateDirectory(PrescriptionStorageDirectory);
        }

        string requestCode = GeneratePrescriptionCode();
        while (await _databaseContext.PrescriptionRequests.AnyAsync(request => request.Code == requestCode))
        {
            requestCode = GeneratePrescriptionCode();
        }

        var prescriptionRequest = new PrescriptionRequest
        {
            Code = requestCode,
            CustomerId = customerId,
            PatientName = model.PatientName.Trim(),
            Phone = model.Phone.Trim(),
            Status = "PENDING_REVIEW",
            CustomerNote = model.CustomerNote?.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };

        foreach (var imageFile in model.Images)
        {
            if (imageFile.Length == 0 || imageFile.Length > 5 * 1024 * 1024)
            {
                continue;
            }

            string fileExtension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(fileExtension))
            {
                continue;
            }

            // Kiểm tra chữ ký byte tệp ảnh (Magic Bytes) để ngăn chặn tệp thực thi độc hại
            using var fileStream = imageFile.OpenReadStream();
            var headerBytes = new byte[8];
            int bytesRead = await fileStream.ReadAsync(headerBytes.AsMemory(0, headerBytes.Length));
            if (bytesRead < 4)
            {
                continue;
            }

            bool isJpeg = headerBytes[0] == 0xFF && headerBytes[1] == 0xD8 && headerBytes[2] == 0xFF;
            bool isPng = headerBytes[0] == 0x89 && headerBytes[1] == 0x50 && headerBytes[2] == 0x4E && headerBytes[3] == 0x47;
            bool isWebp = headerBytes[0] == 0x52 && headerBytes[1] == 0x49 && headerBytes[2] == 0x46 && headerBytes[3] == 0x46;

            if (!isJpeg && !isPng && !isWebp)
            {
                continue;
            }

            string safeStoredFileName = $"{Guid.NewGuid():N}{fileExtension}";
            string physicalFilePath = Path.Combine(PrescriptionStorageDirectory, safeStoredFileName);

            using (var destinationStream = new FileStream(physicalFilePath, FileMode.Create))
            {
                fileStream.Position = 0;
                await fileStream.CopyToAsync(destinationStream);
            }

            prescriptionRequest.Images.Add(new PrescriptionImage
            {
                FileName = Path.GetFileName(imageFile.FileName),
                StoredPath = safeStoredFileName,
                FileSizeBytes = imageFile.Length,
                ContentType = imageFile.ContentType
            });
        }

        if (prescriptionRequest.Images.Count == 0)
        {
            return (false, "Ảnh đính kèm không đúng định dạng hợp lệ (chỉ chấp nhận JPEG, PNG, WebP dưới 5MB).", null, null);
        }

        _databaseContext.PrescriptionRequests.Add(prescriptionRequest);
        await _databaseContext.SaveChangesAsync();

        return (true, "Đã gửi yêu cầu tư vấn đơn thuốc thành công. Dược sĩ sẽ kiểm tra và phản hồi sớm nhất.", prescriptionRequest.Id, prescriptionRequest.Code);
    }

    /// <summary>
    /// Lấy danh sách các yêu cầu tư vấn đơn thuốc của một khách hàng cụ thể.
    /// </summary>
    public async Task<List<PrescriptionRequestItemViewModel>> GetCustomerRequestsAsync(int customerId)
    {
        var requests = await _databaseContext.PrescriptionRequests
            .AsNoTracking()
            .Include(request => request.Images)
            .Where(request => request.CustomerId == customerId)
            .OrderByDescending(request => request.CreatedAt)
            .ToListAsync();

        return requests.Select(MapToViewModel).ToList();
    }

    /// <summary>
    /// Lấy danh sách các yêu cầu tư vấn đơn thuốc cho Quản trị viên / Dược sĩ.
    /// </summary>
    public async Task<PagedResult<PrescriptionRequestItemViewModel>> GetAdminRequestsAsync(string? status, int page, int pageSize)
    {
        var query = _databaseContext.PrescriptionRequests
            .AsNoTracking()
            .Include(request => request.Images)
            .OrderByDescending(request => request.CreatedAt)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(status))
        {
            query = query.Where(request => request.Status == status);
        }

        int totalCount = await query.CountAsync();
        var requests = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<PrescriptionRequestItemViewModel>
        {
            Items = requests.Select(MapToViewModel).ToList(),
            Page = page,
            PageSize = pageSize,
            TotalItems = totalCount
        };
    }

    /// <summary>
    /// Lấy chi tiết một yêu cầu đơn thuốc kèm kiểm tra quyền sở hữu (tránh lỗi IDOR).
    /// </summary>
    public async Task<PrescriptionRequestItemViewModel?> GetRequestByIdAsync(int id, int? customerId, bool isAdmin)
    {
        var query = _databaseContext.PrescriptionRequests
            .AsNoTracking()
            .Include(request => request.Images)
            .Where(request => request.Id == id);

        // Khách hàng thông thường chỉ được xem đơn thuốc của chính mình
        if (!isAdmin && customerId.HasValue)
        {
            query = query.Where(request => request.CustomerId == customerId.Value);
        }

        var request = await query.FirstOrDefaultAsync();
        if (request == null)
        {
            return null;
        }

        return MapToViewModel(request);
    }

    /// <summary>
    /// Dược sĩ lưu phản hồi tư vấn và cập nhật trạng thái đơn thuốc.
    /// </summary>
    public async Task<(bool Success, string Message)> RespondRequestAsync(int id, string pharmacistNote, string newStatus)
    {
        var request = await _databaseContext.PrescriptionRequests.FindAsync(id);
        if (request == null)
        {
            return (false, "Không tìm thấy yêu cầu tư vấn đơn thuốc.");
        }

        request.PharmacistNote = pharmacistNote.Trim();
        request.Status = newStatus;
        request.UpdatedAt = DateTime.UtcNow;

        await _databaseContext.SaveChangesAsync();
        return (true, "Đã lưu phản hồi cho khách hàng thành công.");
    }

    /// <summary>
    /// Trả về luồng đọc ảnh đơn thuốc bảo mật, có kiểm tra quyền sở hữu của khách hàng hoặc quyền Admin.
    /// </summary>
    public async Task<(Stream? Stream, string ContentType, string FileName)> GetSecureImageStreamAsync(int imageId, int? customerId, bool isAdmin)
    {
        var image = await _databaseContext.PrescriptionImages
            .Include(pi => pi.PrescriptionRequest)
            .FirstOrDefaultAsync(pi => pi.Id == imageId);

        if (image == null || image.PrescriptionRequest == null)
        {
            return (null, string.Empty, string.Empty);
        }

        // Kiểm tra quyền sở hữu: Người xem phải là Admin hoặc chính khách hàng đã tạo yêu cầu
        if (!isAdmin && (!customerId.HasValue || image.PrescriptionRequest.CustomerId != customerId.Value))
        {
            return (null, string.Empty, string.Empty);
        }

        string fullPhysicalPath = Path.Combine(PrescriptionStorageDirectory, image.StoredPath);
        if (!File.Exists(fullPhysicalPath))
        {
            return (null, string.Empty, string.Empty);
        }

        var fileStream = new FileStream(fullPhysicalPath, FileMode.Open, FileAccess.Read, FileShare.Read);
        return (fileStream, image.ContentType, image.FileName);
    }

    private static PrescriptionRequestItemViewModel MapToViewModel(PrescriptionRequest prescriptionRequest)
    {
        return new PrescriptionRequestItemViewModel
        {
            Id = prescriptionRequest.Id,
            Code = prescriptionRequest.Code,
            PatientName = prescriptionRequest.PatientName,
            Phone = prescriptionRequest.Phone,
            CreatedAt = prescriptionRequest.CreatedAt,
            Status = prescriptionRequest.Status,
            CustomerNote = prescriptionRequest.CustomerNote,
            PharmacistNote = prescriptionRequest.PharmacistNote,
            UpdatedAt = prescriptionRequest.UpdatedAt,
            Images = prescriptionRequest.Images.Select(img => new PrescriptionImageViewModel
            {
                Id = img.Id,
                FileName = img.FileName,
                FileSizeBytes = img.FileSizeBytes,
                ContentType = img.ContentType
            }).ToList()
        };
    }

    private static string GeneratePrescriptionCode()
    {
        return $"RX-{DateTime.UtcNow:yyyy}-{new Random().Next(100, 999)}";
    }
}
