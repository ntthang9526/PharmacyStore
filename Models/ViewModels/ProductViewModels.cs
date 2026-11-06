namespace PharmacyWeb.Models.ViewModels;

public class ProductCardViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string ActiveIngredient { get; set; } = string.Empty;
    public string Unit { get; set; } = "Hộp";
    public decimal Price { get; set; }
    public bool IsPrescription { get; set; }
    public bool IsSelling { get; set; }
    public bool IsActive { get; set; }
    public int AvailableStock { get; set; }
    public string? ImageUrl { get; set; }
}

public class ProductDetailViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string SupplierName { get; set; } = string.Empty;
    public string BarCode { get; set; } = string.Empty;
    public string ActiveIngredient { get; set; } = string.Empty;
    public string Unit { get; set; } = "Hộp";
    public string PackagingSpec { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool IsPrescription { get; set; }
    public bool IsSelling { get; set; }
    public bool IsActive { get; set; }
    public int AvailableStock { get; set; }
    public string? DescriptionHtml { get; set; }
    public string? MainImageUrl { get; set; }
    public List<string> Images { get; set; } = new();
    public List<ProductCardViewModel> RelatedProducts { get; set; } = new();
}

public class ProductFilterViewModel
{
    public string? Keyword { get; set; }
    public int? CategoryId { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public bool? OnlyInStock { get; set; }
    public bool? DirectPurchaseOnly { get; set; } // OTC only
    public string SortBy { get; set; } = "default"; // default, price_asc, price_desc, name_asc
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 12;

    public PagedResult<ProductCardViewModel> Results { get; set; } = new();
    public List<CategoryItemViewModel> Categories { get; set; } = new();
}

public class CategoryItemViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public int ProductCount { get; set; }
}
