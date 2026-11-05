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
    private readonly PharmacyWebDbContext _db;
    private readonly IHtmlSanitizerService _sanitizer;
    private readonly IWebHostEnvironment _env;

    public ProductService(PharmacyWebDbContext db, IHtmlSanitizerService sanitizer, IWebHostEnvironment env)
    {
        _db = db;
        _sanitizer = sanitizer;
        _env = env;
    }

    public async Task<PagedResult<ProductCardViewModel>> GetCatalogAsync(ProductFilterViewModel filter)
    {
        var query = _db.Medicines
            .AsNoTracking()
            .Include(m => m.Category)
            .Include(m => m.Batches)
            .Where(m => m.IsActive); // Storefront only shows IsActive

        if (!string.IsNullOrWhiteSpace(filter.Keyword))
        {
            var kw = filter.Keyword.Trim().ToLower();
            query = query.Where(m => m.Name.ToLower().Contains(kw) || m.ActiveIngredient.ToLower().Contains(kw));
        }

        if (filter.CategoryId.HasValue && filter.CategoryId.Value > 0)
        {
            query = query.Where(m => m.CategoryId == filter.CategoryId.Value);
        }

        if (filter.MinPrice.HasValue)
        {
            query = query.Where(m => m.Price >= filter.MinPrice.Value);
        }

        if (filter.MaxPrice.HasValue)
        {
            query = query.Where(m => m.Price <= filter.MaxPrice.Value);
        }

        if (filter.DirectPurchaseOnly == true)
        {
            query = query.Where(m => !m.IsPrescription);
        }

        // Sorting
        query = filter.SortBy switch
        {
            "price_asc" => query.OrderBy(m => m.Price),
            "price_desc" => query.OrderByDescending(m => m.Price),
            "name_asc" => query.OrderBy(m => m.Name),
            _ => query.OrderByDescending(m => m.Featured).ThenBy(m => m.Id)
        };

        var allItems = await query.ToListAsync();

        var today = DateTime.UtcNow.Date;
        var mapped = allItems.Select(m =>
        {
            var availStock = (!m.IsActive || !m.IsSelling) ? 0 :
                m.Batches.Where(b => !b.IsBlocked && b.ExpiryDate > today)
                         .Sum(b => Math.Max(0, b.Stock - b.ReservedQuantity));

            return new ProductCardViewModel
            {
                Id = m.Id,
                Name = m.Name,
                Slug = m.Slug,
                CategoryName = m.Category?.Name ?? string.Empty,
                ActiveIngredient = m.ActiveIngredient,
                Unit = m.Unit,
                Price = m.Price,
                IsPrescription = m.IsPrescription,
                IsSelling = m.IsSelling,
                IsActive = m.IsActive,
                AvailableStock = availStock,
                ImageUrl = !string.IsNullOrEmpty(m.MainImageUrl) 
                    ? m.MainImageUrl 
                    : MedicineSvgHelper.GeneratePackagingSvg(m.Name, m.ActiveIngredient, m.PackagingSpec, m.Unit, m.IsPrescription)
            };
        });

        if (filter.OnlyInStock == true)
        {
            mapped = mapped.Where(m => m.AvailableStock > 0);
        }

        var list = mapped.ToList();
        var total = list.Count;
        var pageSize = filter.PageSize > 0 ? filter.PageSize : 12;
        var page = Math.Max(1, filter.Page);

        var pagedItems = list.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        return new PagedResult<ProductCardViewModel>
        {
            Items = pagedItems,
            Page = page,
            PageSize = pageSize,
            TotalItems = total
        };
    }

    public async Task<ProductDetailViewModel?> GetDetailBySlugAsync(string slug)
    {
        var med = await _db.Medicines
            .AsNoTracking()
            .Include(m => m.Category)
            .Include(m => m.Supplier)
            .Include(m => m.Images.OrderBy(i => i.DisplayOrder))
            .Include(m => m.Batches)
            .FirstOrDefaultAsync(m => m.Slug == slug);

        if (med == null) return null;
        return MapToDetailViewModel(med);
    }

    public async Task<ProductDetailViewModel?> GetDetailByIdAsync(int id)
    {
        var med = await _db.Medicines
            .AsNoTracking()
            .Include(m => m.Category)
            .Include(m => m.Supplier)
            .Include(m => m.Images.OrderBy(i => i.DisplayOrder))
            .Include(m => m.Batches)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (med == null) return null;
        return MapToDetailViewModel(med);
    }

    private ProductDetailViewModel MapToDetailViewModel(Medicine med)
    {
        var today = DateTime.UtcNow.Date;
        var availStock = (!med.IsActive || !med.IsSelling) ? 0 :
            med.Batches.Where(b => !b.IsBlocked && b.ExpiryDate > today)
                       .Sum(b => Math.Max(0, b.Stock - b.ReservedQuantity));

        var vm = new ProductDetailViewModel
        {
            Id = med.Id,
            Name = med.Name,
            Slug = med.Slug,
            CategoryId = med.CategoryId,
            CategoryName = med.Category?.Name ?? string.Empty,
            SupplierName = med.Supplier?.Name ?? string.Empty,
            BarCode = med.BarCode,
            ActiveIngredient = med.ActiveIngredient,
            Unit = med.Unit,
            PackagingSpec = med.PackagingSpec,
            Price = med.Price,
            IsPrescription = med.IsPrescription,
            IsSelling = med.IsSelling,
            IsActive = med.IsActive,
            AvailableStock = availStock,
            DescriptionHtml = med.DescriptionHtml,
            MainImageUrl = !string.IsNullOrEmpty(med.MainImageUrl) 
                ? med.MainImageUrl 
                : MedicineSvgHelper.GeneratePackagingSvg(med.Name, med.ActiveIngredient, med.PackagingSpec, med.Unit, med.IsPrescription),
            Images = med.Images.Select(i => i.ImageUrl).ToList()
        };

        if (string.IsNullOrEmpty(med.MainImageUrl) && !vm.Images.Any())
        {
            vm.Images.Add(vm.MainImageUrl);
        }
        else if (!string.IsNullOrEmpty(med.MainImageUrl) && !vm.Images.Contains(med.MainImageUrl))
        {
            vm.Images.Insert(0, med.MainImageUrl);
        }

        // Related products in same category
        var related = _db.Medicines
            .AsNoTracking()
            .Include(m => m.Category)
            .Include(m => m.Batches)
            .Where(m => m.CategoryId == med.CategoryId && m.Id != med.Id && m.IsActive)
            .Take(4)
            .ToList();

        vm.RelatedProducts = related.Select(r => new ProductCardViewModel
        {
            Id = r.Id,
            Name = r.Name,
            Slug = r.Slug,
            CategoryName = r.Category?.Name ?? string.Empty,
            ActiveIngredient = r.ActiveIngredient,
            Unit = r.Unit,
            Price = r.Price,
            IsPrescription = r.IsPrescription,
            IsSelling = r.IsSelling,
            IsActive = r.IsActive,
            AvailableStock = (!r.IsActive || !r.IsSelling) ? 0 :
                r.Batches.Where(b => !b.IsBlocked && b.ExpiryDate > today).Sum(b => Math.Max(0, b.Stock - b.ReservedQuantity)),
            ImageUrl = !string.IsNullOrEmpty(r.MainImageUrl) 
                ? r.MainImageUrl 
                : MedicineSvgHelper.GeneratePackagingSvg(r.Name, r.ActiveIngredient, r.PackagingSpec, r.Unit, r.IsPrescription)
        }).ToList();

        return vm;
    }

    public async Task<List<CategoryItemViewModel>> GetCategoriesWithCountAsync()
    {
        return await _db.Categories
            .AsNoTracking()
            .Where(c => c.IsActive)
            .OrderBy(c => c.DisplayOrder)
            .Select(c => new CategoryItemViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Slug = c.Slug,
                ProductCount = c.Medicines.Count(m => m.IsActive)
            })
            .ToListAsync();
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await _db.Categories.AsNoTracking().OrderBy(c => c.DisplayOrder).ToListAsync();
    }

    public async Task<List<Supplier>> GetAllSuppliersAsync()
    {
        return await _db.Suppliers.AsNoTracking().OrderBy(s => s.Name).ToListAsync();
    }

    public async Task<Medicine?> GetEntityByIdAsync(int id)
    {
        return await _db.Medicines
            .Include(m => m.Images)
            .Include(m => m.Batches)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<int> CreateProductAsync(AdminProductFormViewModel model)
    {
        var slug = GenerateSlug(model.Name);
        var baseSlug = slug;
        int count = 1;
        while (await _db.Medicines.AnyAsync(m => m.Slug == slug))
        {
            slug = $"{baseSlug}-{count++}";
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
            Slug = slug,
            ActiveIngredient = model.ActiveIngredient?.Trim() ?? string.Empty,
            Unit = model.Unit.Trim(),
            PackagingSpec = model.PackagingSpec?.Trim() ?? string.Empty,
            Price = model.Price,
            IsPrescription = model.IsPrescription,
            IsSelling = model.IsSelling,
            IsActive = model.IsActive,
            Featured = model.Featured,
            DescriptionHtml = _sanitizer.Sanitize(model.DescriptionHtml),
            MainImageUrl = mainImagePath,
            CreatedAt = DateTime.UtcNow
        };

        if (model.AdditionalImages != null)
        {
            int order = 1;
            foreach (var imgFile in model.AdditionalImages)
            {
                var path = await SaveUploadedImageAsync(imgFile);
                if (path != null)
                {
                    medicine.Images.Add(new MedicineImage
                    {
                        ImageUrl = path,
                        DisplayOrder = order++,
                        IsPrimary = false
                    });
                }
            }
        }

        _db.Medicines.Add(medicine);
        await _db.SaveChangesAsync();
        return medicine.Id;
    }

    public async Task<bool> UpdateProductAsync(AdminProductFormViewModel model)
    {
        var med = await _db.Medicines
            .Include(m => m.Images)
            .FirstOrDefaultAsync(m => m.Id == model.Id);

        if (med == null) return false;

        med.CategoryId = model.CategoryId;
        med.SupplierId = model.SupplierId;
        med.BarCode = model.BarCode.Trim();
        med.Name = model.Name.Trim();
        med.ActiveIngredient = model.ActiveIngredient?.Trim() ?? string.Empty;
        med.Unit = model.Unit.Trim();
        med.PackagingSpec = model.PackagingSpec?.Trim() ?? string.Empty;
        med.Price = model.Price;
        med.IsPrescription = model.IsPrescription;
        med.IsSelling = model.IsSelling;
        med.IsActive = model.IsActive;
        med.Featured = model.Featured;
        med.DescriptionHtml = _sanitizer.Sanitize(model.DescriptionHtml);

        if (model.MainImageUpload != null)
        {
            var newPath = await SaveUploadedImageAsync(model.MainImageUpload);
            if (newPath != null)
            {
                med.MainImageUrl = newPath;
            }
        }

        if (model.AdditionalImages != null)
        {
            int order = med.Images.Count + 1;
            foreach (var imgFile in model.AdditionalImages)
            {
                var path = await SaveUploadedImageAsync(imgFile);
                if (path != null)
                {
                    med.Images.Add(new MedicineImage
                    {
                        ImageUrl = path,
                        DisplayOrder = order++,
                        IsPrimary = false
                    });
                }
            }
        }

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ToggleActiveAsync(int id)
    {
        var med = await _db.Medicines.FindAsync(id);
        if (med == null) return false;
        med.IsActive = !med.IsActive;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ToggleSellingAsync(int id)
    {
        var med = await _db.Medicines.FindAsync(id);
        if (med == null) return false;
        med.IsSelling = !med.IsSelling;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> SaveCategoryAsync(Category category)
    {
        if (category.Id == 0)
        {
            category.Slug = GenerateSlug(category.Name);
            _db.Categories.Add(category);
        }
        else
        {
            var existing = await _db.Categories.FindAsync(category.Id);
            if (existing == null) return false;
            existing.Name = category.Name;
            existing.Description = category.Description;
            existing.IsActive = category.IsActive;
            existing.DisplayOrder = category.DisplayOrder;
        }
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<(bool Success, string Message)> DeleteCategoryAsync(int id)
    {
        var cat = await _db.Categories.Include(c => c.Medicines).FirstOrDefaultAsync(c => c.Id == id);
        if (cat == null) return (false, "Không tìm thấy danh mục");
        if (cat.Medicines.Any())
        {
            return (false, $"Không thể xóa danh mục '{cat.Name}' vì vẫn còn {cat.Medicines.Count} sản phẩm liên kết. Hãy ẩn danh mục thay vì xóa.");
        }
        _db.Categories.Remove(cat);
        await _db.SaveChangesAsync();
        return (true, "Đã xóa danh mục thành công.");
    }

    public async Task<bool> SaveSupplierAsync(Supplier supplier)
    {
        if (supplier.Id == 0)
        {
            _db.Suppliers.Add(supplier);
        }
        else
        {
            var existing = await _db.Suppliers.FindAsync(supplier.Id);
            if (existing == null) return false;
            existing.Name = supplier.Name;
            existing.Phone = supplier.Phone;
            existing.Address = supplier.Address;
            existing.IsActive = supplier.IsActive;
        }
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<(bool Success, string Message)> DeleteSupplierAsync(int id)
    {
        var sup = await _db.Suppliers.Include(s => s.Medicines).Include(s => s.StockIns).FirstOrDefaultAsync(s => s.Id == id);
        if (sup == null) return (false, "Không tìm thấy nhà cung cấp");
        if (sup.Medicines.Any() || sup.StockIns.Any())
        {
            return (false, $"Không thể xóa nhà cung cấp '{sup.Name}' vì còn thuốc hoặc phiếu nhập liên kết.");
        }
        _db.Suppliers.Remove(sup);
        await _db.SaveChangesAsync();
        return (true, "Đã xóa nhà cung cấp thành công.");
    }

    private async Task<string?> SaveUploadedImageAsync(IFormFile file)
    {
        if (file.Length == 0 || file.Length > 5 * 1024 * 1024) return null;

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!allowedExtensions.Contains(ext)) return null;

        // Magic bytes check
        using (var stream = file.OpenReadStream())
        {
            var header = new byte[8];
            var bytesRead = await stream.ReadAsync(header.AsMemory(0, header.Length));
            if (bytesRead < 4) return null;

            bool isJpeg = header[0] == 0xFF && header[1] == 0xD8 && header[2] == 0xFF;
            bool isPng = header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E && header[3] == 0x47;
            bool isWebp = header[0] == 0x52 && header[1] == 0x49 && header[2] == 0x46 && header[3] == 0x46;

            if (!isJpeg && !isPng && !isWebp) return null;
        }

        var uploadsDir = Path.Combine(_env.WebRootPath, "uploads", "products");
        if (!Directory.Exists(uploadsDir))
        {
            Directory.CreateDirectory(uploadsDir);
        }

        var uniqueFileName = $"{Guid.NewGuid():N}{ext}";
        var filePath = Path.Combine(uploadsDir, uniqueFileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        return $"/uploads/products/{uniqueFileName}";
    }

    private static string GenerateSlug(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return Guid.NewGuid().ToString("N")[..8];
        var normalized = text.ToLowerInvariant().Trim();
        normalized = Regex.Replace(normalized, @"[áàảãạăắằẳẵặâấầẩẫậ]", "a");
        normalized = Regex.Replace(normalized, @"[éèẻẽẹêếềểễệ]", "e");
        normalized = Regex.Replace(normalized, @"[íìỉĩị]", "i");
        normalized = Regex.Replace(normalized, @"[óòỏõọôốồổỗộơớờởỡợ]", "o");
        normalized = Regex.Replace(normalized, @"[úùủũụưứừửữự]", "u");
        normalized = Regex.Replace(normalized, @"[ýỳỷỹỵ]", "y");
        normalized = Regex.Replace(normalized, @"[đ]", "d");
        normalized = Regex.Replace(normalized, @"[^a-z0-9\s-]", "");
        normalized = Regex.Replace(normalized, @"\s+", "-").Trim('-');
        return string.IsNullOrEmpty(normalized) ? Guid.NewGuid().ToString("N")[..8] : normalized;
    }
}
