using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PharmacyWeb.Data;
using PharmacyWeb.Data.Entities;
using PharmacyWeb.Models.ViewModels;

namespace PharmacyWeb.Services;

public interface IProductService
{
    Task<PagedResult<ProductCardViewModel>> GetCatalogAsync(ProductFilterViewModel filter);
    Task<PagedResult<ProductCardViewModel>> GetAdminCatalogAsync(ProductFilterViewModel filter);
    Task<ProductDetailViewModel?> GetDetailBySlugAsync(string slug);
    Task<ProductDetailViewModel?> GetDetailByIdAsync(int id);
    Task<List<CategoryItemViewModel>> GetCategoriesWithCountAsync();
    Task<List<Category>> GetAllCategoriesAsync();
    Task<List<Supplier>> GetAllSuppliersAsync();
    Task<Medicine?> GetEntityByIdAsync(int id);
    Task<int> CreateProductAsync(AdminProductFormViewModel model);
    Task<bool> UpdateProductAsync(AdminProductFormViewModel model);
    Task<bool> ToggleActiveAsync(int id);
    Task<bool> ToggleSellingAsync(int id);
    Task<bool> SaveCategoryAsync(Category category);
    Task<(bool Success, string Message)> DeleteCategoryAsync(int id);
    Task<bool> SaveSupplierAsync(Supplier supplier);
    Task<(bool Success, string Message)> DeleteSupplierAsync(int id);
}

public class ProductService : IProductService
{
    private readonly PharmacyWebDbContext _databaseContext;
    private readonly IHtmlSanitizerService _htmlSanitizer;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProductService(
        PharmacyWebDbContext databaseContext,
        IHtmlSanitizerService htmlSanitizer,
        IWebHostEnvironment webHostEnvironment)
    {
        _databaseContext = databaseContext;
        _htmlSanitizer = htmlSanitizer;
        _webHostEnvironment = webHostEnvironment;
    }

    /// <summary>
    /// Lấy danh mục sản phẩm cho khách hàng xem trên storefront.
    /// Chỉ hiển thị các sản phẩm đang được kích hoạt (IsActive = true).
    /// </summary>
    public Task<PagedResult<ProductCardViewModel>> GetCatalogAsync(ProductFilterViewModel filter)
    {
        return BuildCatalogAsync(filter, isAdmin: false);
    }

    /// <summary>
    /// Lấy danh mục sản phẩm cho trang quản trị Admin.
    /// Cho phép xem cả sản phẩm đang kích hoạt và đã tạm ngưng.
    /// </summary>
    public Task<PagedResult<ProductCardViewModel>> GetAdminCatalogAsync(ProductFilterViewModel filter)
    {
        return BuildCatalogAsync(filter, isAdmin: true);
    }

    private async Task<PagedResult<ProductCardViewModel>> BuildCatalogAsync(ProductFilterViewModel filter, bool isAdmin)
    {
        var query = _databaseContext.Medicines
            .AsNoTracking()
            .Include(medicine => medicine.Category)
            .Include(medicine => medicine.Batches)
            .AsQueryable();

        // 1. Phân quyền hiển thị: Storefront chỉ thấy thuốc active; Admin có thể lọc theo trạng thái
        if (!isAdmin)
        {
            query = query.Where(medicine => medicine.IsActive);
        }
        else if (filter.IsActive.HasValue)
        {
            query = query.Where(medicine => medicine.IsActive == filter.IsActive.Value);
        }

        // 2. Tìm kiếm theo từ khóa (tên thuốc hoặc hoạt chất)
        if (!string.IsNullOrWhiteSpace(filter.Keyword))
        {
            string keywordLower = filter.Keyword.Trim().ToLower();
            query = query.Where(medicine => medicine.Name.ToLower().Contains(keywordLower) ||
                                           medicine.ActiveIngredient.ToLower().Contains(keywordLower));
        }

        // 3. Lọc theo danh mục nhóm thuốc
        if (filter.CategoryId.HasValue && filter.CategoryId.Value > 0)
        {
            query = query.Where(medicine => medicine.CategoryId == filter.CategoryId.Value);
        }

        // 4. Lọc theo khoảng giá niêm yết
        if (filter.MinPrice.HasValue)
        {
            query = query.Where(medicine => medicine.Price >= filter.MinPrice.Value);
        }

        if (filter.MaxPrice.HasValue)
        {
            query = query.Where(medicine => medicine.Price <= filter.MaxPrice.Value);
        }

        // 5. Lọc chỉ lấy thuốc mua trực tiếp (không kê đơn OTC)
        if (filter.DirectPurchaseOnly == true)
        {
            query = query.Where(medicine => !medicine.IsPrescription);
        }

        // 6. Sắp xếp kết quả
        query = filter.SortBy switch
        {
            "price_asc" => query.OrderBy(medicine => medicine.Price),
            "price_desc" => query.OrderByDescending(medicine => medicine.Price),
            "name_asc" => query.OrderBy(medicine => medicine.Name),
            _ => query.OrderByDescending(medicine => medicine.Featured).ThenBy(medicine => medicine.Id)
        };

        var allMedicines = await query.ToListAsync();
        var today = PharmacyClock.Today;

        var productCardList = new List<ProductCardViewModel>();
        foreach (var medicine in allMedicines)
        {
            int availableStock = CalculateAvailableStock(medicine, today);
            var cardViewModel = MapToProductCardViewModel(medicine, availableStock);
            productCardList.Add(cardViewModel);
        }

        // 7. Lọc theo trạng thái còn hàng nếu người dùng tick chọn
        if (filter.OnlyInStock == true)
        {
            productCardList = productCardList
                .Where(card => card.AvailableStock > 0)
                .ToList();
        }

        int totalCount = productCardList.Count;
        int pageSize = filter.PageSize > 0 ? filter.PageSize : 12;
        int currentPage = Math.Max(1, filter.Page);

        var pagedItems = productCardList
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PagedResult<ProductCardViewModel>
        {
            Items = pagedItems,
            Page = currentPage,
            PageSize = pageSize,
            TotalItems = totalCount
        };
    }

    /// <summary>
    /// Lấy chi tiết thông tin thuốc theo đường dẫn thân thiện (Slug) trên storefront.
    /// </summary>
    public async Task<ProductDetailViewModel?> GetDetailBySlugAsync(string slug)
    {
        var medicine = await _databaseContext.Medicines
            .AsNoTracking()
            .Include(m => m.Category)
            .Include(m => m.Supplier)
            .Include(m => m.Images.OrderBy(img => img.DisplayOrder))
            .Include(m => m.Batches)
            .FirstOrDefaultAsync(m => m.Slug == slug && m.IsActive);

        if (medicine == null)
        {
            return null;
        }

        return MapToDetailViewModel(medicine);
    }

    /// <summary>
    /// Lấy chi tiết thông tin thuốc theo ID cho storefront.
    /// </summary>
    public async Task<ProductDetailViewModel?> GetDetailByIdAsync(int id)
    {
        var medicine = await _databaseContext.Medicines
            .AsNoTracking()
            .Include(m => m.Category)
            .Include(m => m.Supplier)
            .Include(m => m.Images.OrderBy(img => img.DisplayOrder))
            .Include(m => m.Batches)
            .FirstOrDefaultAsync(m => m.Id == id && m.IsActive);

        if (medicine == null)
        {
            return null;
        }

        return MapToDetailViewModel(medicine);
    }

    private ProductDetailViewModel MapToDetailViewModel(Medicine medicine)
    {
        var today = PharmacyClock.Today;
        int availableStock = CalculateAvailableStock(medicine, today);

        string mainImageUrl = !string.IsNullOrEmpty(medicine.MainImageUrl)
            ? medicine.MainImageUrl
            : MedicineSvgHelper.GeneratePackagingSvg(medicine.Name, medicine.ActiveIngredient, medicine.PackagingSpec, medicine.Unit, medicine.IsPrescription);

        var detailViewModel = new ProductDetailViewModel
        {
            Id = medicine.Id,
            Name = medicine.Name,
            Slug = medicine.Slug,
            CategoryId = medicine.CategoryId,
            CategoryName = medicine.Category?.Name ?? string.Empty,
            SupplierName = medicine.Supplier?.Name ?? string.Empty,
            BarCode = medicine.BarCode,
            ActiveIngredient = medicine.ActiveIngredient ?? string.Empty,
            Unit = medicine.Unit,
            PackagingSpec = medicine.PackagingSpec,
            Price = medicine.Price,
            IsPrescription = medicine.IsPrescription,
            IsSelling = medicine.IsSelling,
            IsActive = medicine.IsActive,
            AvailableStock = availableStock,
            DescriptionHtml = _htmlSanitizer.Sanitize(medicine.DescriptionHtml),
            MainImageUrl = mainImageUrl,
            Images = medicine.Images.Select(img => img.ImageUrl).ToList()
        };

        if (string.IsNullOrEmpty(medicine.MainImageUrl) && detailViewModel.Images.Count == 0)
        {
            detailViewModel.Images.Add(detailViewModel.MainImageUrl);
        }
        else if (!string.IsNullOrEmpty(medicine.MainImageUrl) && !detailViewModel.Images.Contains(medicine.MainImageUrl))
        {
            detailViewModel.Images.Insert(0, medicine.MainImageUrl);
        }

        // Tải các sản phẩm tương tự cùng danh mục nhóm thuốc
        var relatedMedicines = _databaseContext.Medicines
            .AsNoTracking()
            .Include(m => m.Category)
            .Include(m => m.Batches)
            .Where(m => m.CategoryId == medicine.CategoryId && m.Id != medicine.Id && m.IsActive)
            .Take(4)
            .ToList();

        detailViewModel.RelatedProducts = relatedMedicines.Select(relatedMedicine =>
        {
            int relatedAvailableStock = CalculateAvailableStock(relatedMedicine, today);
            return MapToProductCardViewModel(relatedMedicine, relatedAvailableStock);
        }).ToList();

        return detailViewModel;
    }

    /// <summary>
    /// Lấy danh sách các nhóm thuốc kèm số lượng sản phẩm đang kinh doanh.
    /// </summary>
    public async Task<List<CategoryItemViewModel>> GetCategoriesWithCountAsync()
    {
        return await _databaseContext.Categories
            .AsNoTracking()
            .Where(category => category.IsActive)
            .OrderBy(category => category.DisplayOrder)
            .Select(category => new CategoryItemViewModel
            {
                Id = category.Id,
                Name = category.Name,
                Slug = category.Slug,
                ProductCount = category.Medicines.Count(medicine => medicine.IsActive)
            })
            .ToListAsync();
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await _databaseContext.Categories
            .AsNoTracking()
            .OrderBy(category => category.DisplayOrder)
            .ToListAsync();
    }

    public async Task<List<Supplier>> GetAllSuppliersAsync()
    {
        return await _databaseContext.Suppliers
            .AsNoTracking()
            .OrderBy(supplier => supplier.Name)
            .ToListAsync();
    }

    public async Task<Medicine?> GetEntityByIdAsync(int id)
    {
        return await _databaseContext.Medicines
            .Include(medicine => medicine.Images)
            .Include(medicine => medicine.Batches)
            .FirstOrDefaultAsync(medicine => medicine.Id == id);
    }

    /// <summary>
    /// Thêm thuốc mới vào hệ thống (lưu chung vào bảng Medicines của POS).
    /// </summary>
    public async Task<int> CreateProductAsync(AdminProductFormViewModel model)
    {
        string generatedSlug = GenerateSlug(model.Name);
        string uniqueSlug = generatedSlug;
        int duplicateSuffixCounter = 1;

        while (await _databaseContext.Medicines.AnyAsync(medicine => medicine.Slug == uniqueSlug))
        {
            uniqueSlug = $"{generatedSlug}-{duplicateSuffixCounter++}";
        }

        string? mainImagePath = null;
        if (model.MainImageUpload != null)
        {
            mainImagePath = await SaveUploadedImageAsync(model.MainImageUpload);
        }

        var medicine = new Medicine
        {
            CategoryId = model.CategoryId,
            SupplierId = model.SupplierId,
            BarCode = model.BarCode.Trim(),
            Name = model.Name.Trim(),
            Slug = uniqueSlug,
            ActiveIngredient = model.ActiveIngredient?.Trim() ?? string.Empty,
            Unit = model.Unit.Trim(),
            PackagingSpec = model.PackagingSpec?.Trim() ?? string.Empty,
            Price = model.Price,
            IsPrescription = model.IsPrescription,
            IsSelling = model.IsSelling,
            IsActive = model.IsActive,
            Featured = model.Featured,
            DescriptionHtml = _htmlSanitizer.Sanitize(model.DescriptionHtml),
            MainImageUrl = mainImagePath,
            CreatedAt = DateTime.UtcNow
        };

        if (model.AdditionalImages != null)
        {
            int displayOrder = 1;
            foreach (var imageFile in model.AdditionalImages)
            {
                string? imagePath = await SaveUploadedImageAsync(imageFile);
                if (imagePath != null)
                {
                    medicine.Images.Add(new MedicineImage
                    {
                        ImageUrl = imagePath,
                        DisplayOrder = displayOrder++,
                        IsPrimary = false
                    });
                }
            }
        }

        _databaseContext.Medicines.Add(medicine);
        await _databaseContext.SaveChangesAsync();
        return medicine.Id;
    }

    /// <summary>
    /// Cập nhật thông tin thuốc.
    /// Có cơ chế Concurrency Token (RowVersion) chống ghi đè khi POS hoặc phiên khác vừa cập nhật (WEB-04).
    /// </summary>
    public async Task<bool> UpdateProductAsync(AdminProductFormViewModel model)
    {
        var medicine = await _databaseContext.Medicines
            .Include(m => m.Images)
            .FirstOrDefaultAsync(m => m.Id == model.Id);

        if (medicine == null)
        {
            return false;
        }

        // Kiểm tra xung đột dữ liệu đồng thời nếu RowVersion của form không khớp với cơ sở dữ liệu
        if (model.RowVersion != null && medicine.RowVersion != null && !model.RowVersion.SequenceEqual(medicine.RowVersion))
        {
            throw new DbUpdateConcurrencyException("Dữ liệu thuốc đã được cập nhật bởi một phiên làm việc khác (POS hoặc Web). Vui lòng tải lại trang để xem thông tin mới nhất.");
        }

        if (model.RowVersion != null)
        {
            _databaseContext.Entry(medicine).Property(m => m.RowVersion).OriginalValue = model.RowVersion;
        }

        medicine.CategoryId = model.CategoryId;
        medicine.SupplierId = model.SupplierId;
        medicine.BarCode = model.BarCode.Trim();
        medicine.Name = model.Name.Trim();
        medicine.ActiveIngredient = model.ActiveIngredient?.Trim() ?? string.Empty;
        medicine.Unit = model.Unit.Trim();
        medicine.PackagingSpec = model.PackagingSpec?.Trim() ?? string.Empty;
        medicine.Price = model.Price;
        medicine.IsPrescription = model.IsPrescription;
        medicine.IsSelling = model.IsSelling;
        medicine.IsActive = model.IsActive;
        medicine.Featured = model.Featured;
        medicine.DescriptionHtml = _htmlSanitizer.Sanitize(model.DescriptionHtml);

        if (model.MainImageUpload != null)
        {
            string? newMainImagePath = await SaveUploadedImageAsync(model.MainImageUpload);
            if (newMainImagePath != null)
            {
                medicine.MainImageUrl = newMainImagePath;
            }
        }

        if (model.AdditionalImages != null)
        {
            int nextDisplayOrder = medicine.Images.Count + 1;
            foreach (var imageFile in model.AdditionalImages)
            {
                string? additionalImagePath = await SaveUploadedImageAsync(imageFile);
                if (additionalImagePath != null)
                {
                    medicine.Images.Add(new MedicineImage
                    {
                        ImageUrl = additionalImagePath,
                        DisplayOrder = nextDisplayOrder++,
                        IsPrimary = false
                    });
                }
            }
        }

        await _databaseContext.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Đảo trạng thái kích hoạt hoạt động của thuốc (IsActive).
    /// Lưu ý: Cờ IsActive dùng chung cả Web & POS (BS-04).
    /// </summary>
    public async Task<bool> ToggleActiveAsync(int id)
    {
        var medicine = await _databaseContext.Medicines.FindAsync(id);
        if (medicine == null)
        {
            return false;
        }

        medicine.IsActive = !medicine.IsActive;
        await _databaseContext.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Đảo trạng thái cho phép bán hàng của thuốc (IsSelling).
    /// </summary>
    public async Task<bool> ToggleSellingAsync(int id)
    {
        var medicine = await _databaseContext.Medicines.FindAsync(id);
        if (medicine == null)
        {
            return false;
        }

        medicine.IsSelling = !medicine.IsSelling;
        await _databaseContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> SaveCategoryAsync(Category category)
    {
        if (category.Id == 0)
        {
            category.Slug = GenerateSlug(category.Name);
            _databaseContext.Categories.Add(category);
        }
        else
        {
            var existingCategory = await _databaseContext.Categories.FindAsync(category.Id);
            if (existingCategory == null)
            {
                return false;
            }

            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;
            existingCategory.IsActive = category.IsActive;
            existingCategory.DisplayOrder = category.DisplayOrder;
        }

        await _databaseContext.SaveChangesAsync();
        return true;
    }

    public async Task<(bool Success, string Message)> DeleteCategoryAsync(int id)
    {
        var category = await _databaseContext.Categories
            .Include(c => c.Medicines)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
        {
            return (false, "Không tìm thấy danh mục.");
        }

        if (category.Medicines.Count > 0)
        {
            return (false, $"Không thể xóa danh mục '{category.Name}' vì vẫn còn {category.Medicines.Count} sản phẩm liên kết. Hãy ẩn danh mục thay vì xóa.");
        }

        _databaseContext.Categories.Remove(category);
        await _databaseContext.SaveChangesAsync();
        return (true, "Đã xóa danh mục thành công.");
    }

    public async Task<bool> SaveSupplierAsync(Supplier supplier)
    {
        if (supplier.Id == 0)
        {
            _databaseContext.Suppliers.Add(supplier);
        }
        else
        {
            var existingSupplier = await _databaseContext.Suppliers.FindAsync(supplier.Id);
            if (existingSupplier == null)
            {
                return false;
            }

            existingSupplier.Name = supplier.Name;
            existingSupplier.Phone = supplier.Phone;
            existingSupplier.Address = supplier.Address;
            existingSupplier.IsActive = supplier.IsActive;
        }

        await _databaseContext.SaveChangesAsync();
        return true;
    }

    public async Task<(bool Success, string Message)> DeleteSupplierAsync(int id)
    {
        var supplier = await _databaseContext.Suppliers
            .Include(s => s.Medicines)
            .Include(s => s.StockIns)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (supplier == null)
        {
            return (false, "Không tìm thấy nhà cung cấp.");
        }

        if (supplier.Medicines.Count > 0 || supplier.StockIns.Count > 0)
        {
            return (false, $"Không thể xóa nhà cung cấp '{supplier.Name}' vì còn thuốc hoặc phiếu nhập liên kết.");
        }

        _databaseContext.Suppliers.Remove(supplier);
        await _databaseContext.SaveChangesAsync();
        return (true, "Đã xóa nhà cung cấp thành công.");
    }

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

    private static ProductCardViewModel MapToProductCardViewModel(Medicine medicine, int availableStock)
    {
        string imageUrl = !string.IsNullOrEmpty(medicine.MainImageUrl)
            ? medicine.MainImageUrl
            : MedicineSvgHelper.GeneratePackagingSvg(medicine.Name, medicine.ActiveIngredient, medicine.PackagingSpec, medicine.Unit, medicine.IsPrescription);

        return new ProductCardViewModel
        {
            Id = medicine.Id,
            SupplierId = medicine.SupplierId,
            Name = medicine.Name,
            Slug = medicine.Slug,
            CategoryName = medicine.Category?.Name ?? string.Empty,
            ActiveIngredient = medicine.ActiveIngredient ?? string.Empty,
            Unit = medicine.Unit,
            Price = medicine.Price,
            IsPrescription = medicine.IsPrescription,
            IsSelling = medicine.IsSelling,
            IsActive = medicine.IsActive,
            AvailableStock = availableStock,
            ImageUrl = imageUrl
        };
    }

    /// <summary>
    /// Lưu tệp ảnh tải lên với kiểm tra phần mở rộng và kiểm tra nội dung byte (Magic bytes check) để ngăn chặn mã độc.
    /// </summary>
    private async Task<string?> SaveUploadedImageAsync(IFormFile file)
    {
        if (file.Length == 0 || file.Length > 5 * 1024 * 1024)
        {
            return null;
        }

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
        string fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!allowedExtensions.Contains(fileExtension))
        {
            return null;
        }

        // Kiểm tra chữ ký byte đầu tệp (Magic Bytes) để ngăn chặn tệp giả mạo đuôi ảnh
        using (var stream = file.OpenReadStream())
        {
            var headerBytes = new byte[8];
            int bytesRead = await stream.ReadAsync(headerBytes.AsMemory(0, headerBytes.Length));
            if (bytesRead < 4)
            {
                return null;
            }

            bool isJpeg = headerBytes[0] == 0xFF && headerBytes[1] == 0xD8 && headerBytes[2] == 0xFF;
            bool isPng = headerBytes[0] == 0x89 && headerBytes[1] == 0x50 && headerBytes[2] == 0x4E && headerBytes[3] == 0x47;
            bool isWebp = headerBytes[0] == 0x52 && headerBytes[1] == 0x49 && headerBytes[2] == 0x46 && headerBytes[3] == 0x46;

            if (!isJpeg && !isPng && !isWebp)
            {
                return null;
            }
        }

        string uploadsDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "products");
        if (!Directory.Exists(uploadsDirectory))
        {
            Directory.CreateDirectory(uploadsDirectory);
        }

        string uniqueFileName = $"{Guid.NewGuid():N}{fileExtension}";
        string physicalFilePath = Path.Combine(uploadsDirectory, uniqueFileName);

        using (var destinationStream = new FileStream(physicalFilePath, FileMode.Create))
        {
            await file.CopyToAsync(destinationStream);
        }

        return $"/uploads/products/{uniqueFileName}";
    }

    private static string GenerateSlug(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return Guid.NewGuid().ToString("N")[..8];
        }

        string normalized = text.ToLowerInvariant().Trim();
        normalized = Regex.Replace(normalized, @"[áàảãạăắằẳẵặâấầẩẫậ]", "a");
        normalized = Regex.Replace(normalized, @"[éèẻẽẹêếềểễệ]", "e");
        normalized = Regex.Replace(normalized, @"[íìỉĩị]", "i");
        normalized = Regex.Replace(normalized, @"[óòỏõọôốồổỗộơớờởỡợ]", "o");
        normalized = Regex.Replace(normalized, @"[úùũụưứừửữự]", "u");
        normalized = Regex.Replace(normalized, @"[ýỳỷỹỵ]", "y");
        normalized = Regex.Replace(normalized, @"[đ]", "d");
        normalized = Regex.Replace(normalized, @"[^a-z0-9\s-]", "");
        normalized = Regex.Replace(normalized, @"\s+", "-").Trim('-');

        return string.IsNullOrEmpty(normalized) ? Guid.NewGuid().ToString("N")[..8] : normalized;
    }
}
