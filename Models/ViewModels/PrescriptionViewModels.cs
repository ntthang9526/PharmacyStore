using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace PharmacyWeb.Models.ViewModels;

public class PrescriptionUploadViewModel
{
    [Required(ErrorMessage = "Vui lòng nhập họ tên người bệnh")]
    [Display(Name = "Họ tên người bệnh")]
    [MaxLength(100)]
    public string PatientName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
    [RegularExpression(@"^0[35789]\d{8}$", ErrorMessage = "Số điện thoại không đúng định dạng (10 chữ số)")]
    [Display(Name = "Số điện thoại liên hệ")]
    public string Phone { get; set; } = string.Empty;

    [Display(Name = "Ghi chú hoặc câu hỏi cho dược sĩ")]
    [MaxLength(1000)]
    public string? CustomerNote { get; set; }

    [Display(Name = "Ảnh chụp đơn thuốc")]
    public List<IFormFile>? Images { get; set; }
}

public class PrescriptionRequestItemViewModel
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string PatientName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string Status { get; set; } = "PENDING_REVIEW";
    public string? CustomerNote { get; set; }
    public string? PharmacistNote { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public List<PrescriptionImageViewModel> Images { get; set; } = new();
}

public class PrescriptionImageViewModel
{
    public int Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public long FileSizeBytes { get; set; }
    public string ContentType { get; set; } = "image/jpeg";
    public string ViewUrl => $"/api/prescriptions/images/{Id}";
}
