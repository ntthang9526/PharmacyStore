using System.ComponentModel.DataAnnotations;

namespace PharmacyWeb.Models.Api;

/// <summary>Tham số lọc / phân trang cho GET api/products.</summary>
public class ProductQueryDto
{
    public string? Keyword { get; set; }
    public int? CategoryId { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public bool? OnlyInStock { get; set; }

    /// <summary>default | price_asc | price_desc | name_asc</summary>
    public string SortBy { get; set; } = "default";

    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 12;
}

/// <summary>Kết quả phân trang trả về cho client.</summary>
public class PagedApiResult<T>
{
    public List<T> Items { get; set; } = new();
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
}

/// <summary>Dữ liệu thêm / sửa thuốc qua API (JSON). Ảnh được upload ở form MVC của Admin.</summary>
public class ProductWriteDto
{
    [Required(ErrorMessage = "Vui lòng chọn danh mục thuốc")]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn nhà cung cấp")]
    public int SupplierId { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập mã vạch")]
    [MaxLength(50)]
    public string BarCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập tên thuốc")]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(200)]
    public string ActiveIngredient { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập đơn vị tính")]
    [MaxLength(50)]
    public string Unit { get; set; } = "Hộp";

    [MaxLength(150)]
    public string PackagingSpec { get; set; } = string.Empty;

    [Range(100, 100000000, ErrorMessage = "Giá phải từ 100 ₫ đến 100.000.000 ₫")]
    public decimal Price { get; set; }

    public bool IsPrescription { get; set; }
    public bool IsSelling { get; set; } = true;
    public bool IsActive { get; set; } = true;
    public bool Featured { get; set; }
    public string? DescriptionHtml { get; set; }
}
