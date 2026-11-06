using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PharmacyWeb.Data.Entities;

namespace PharmacyWeb.Data.Seed;

public static class DbInitializer
{
    public static async Task InitializeAsync(
        PharmacyWebDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager)
    {
        await context.Database.EnsureCreatedAsync();

        // 1. Roles
        string[] roles = ["Admin", "Customer"];
        foreach (var roleName in roles)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new ApplicationRole(roleName));
            }
        }

        // 2. Categories
        if (!await context.Categories.AnyAsync())
        {
            var categories = new List<Category>
            {
                new() { Id = 1, Name = "Thuốc giảm đau - hạ sốt", Slug = "giam-dau-ha-sot", Description = "Các sản phẩm giảm đau, hạ sốt thông dụng", IsActive = true, DisplayOrder = 1 },
                new() { Id = 2, Name = "Kháng sinh - Kháng viêm", Slug = "khang-sinh-khang-viem", Description = "Nhóm thuốc kháng sinh, kháng viêm cần đơn và tư vấn dược sĩ", IsActive = true, DisplayOrder = 2 },
                new() { Id = 3, Name = "Vitamin & Khoáng chất", Slug = "vitamin-khoang-chat", Description = "Bổ sung vitamin, khoáng chất nâng cao sức đề kháng", IsActive = true, DisplayOrder = 3 },
                new() { Id = 4, Name = "Tiêu hóa - Dạ dày", Slug = "tieu-hoa-da-day", Description = "Hỗ trợ tiêu hóa, giảm tiết acid dạ dày, bù nước điện giải", IsActive = true, DisplayOrder = 4 },
                new() { Id = 5, Name = "Dị ứng - Hô hấp", Slug = "di-ung-ho-hap", Description = "Thuốc chống dị ứng, siro ho, dung dịch vệ sinh mũi họng", IsActive = true, DisplayOrder = 5 },
                new() { Id = 6, Name = "Vật tư & Thiết bị y tế", Slug = "vat-tu-y-te", Description = "Băng gạc tiệt trùng, nhiệt kế, khẩu trang và chăm sóc vết thương", IsActive = true, DisplayOrder = 6 }
            };
            await context.Categories.AddRangeAsync(categories);
            await context.SaveChangesAsync();
        }

        // 3. Suppliers
        if (!await context.Suppliers.AnyAsync())
        {
            var suppliers = new List<Supplier>
            {
                new() { Id = 1, Name = "Dược Hậu Giang (DHG Pharma)", Phone = "02923891433", Address = "288 Bis Nguyễn Văn Cừ, Q. Ninh Kiều, Cần Thơ", IsActive = true },
                new() { Id = 2, Name = "Sanofi Việt Nam", Phone = "02838298526", Address = "Khu Công nghệ cao, TP. Thủ Đức, TP. Hồ Chí Minh", IsActive = true },
                new() { Id = 3, Name = "Dược phẩm Boston Việt Nam", Phone = "02743769606", Address = "KCN Việt Nam - Singapore, Thuận An, Bình Dương", IsActive = true },
                new() { Id = 4, Name = "Công ty Cổ phần Traphaco", Phone = "02436815097", Address = "75 Yên Ninh, Q. Ba Đình, Hà Nội", IsActive = true },
                new() { Id = 5, Name = "Công ty CP Dược phẩm Imexpharm", Phone = "02773851941", Address = "Số 4 Đường 30/4, TP. Cao Lãnh, Đồng Tháp", IsActive = true }
            };
            await context.Suppliers.AddRangeAsync(suppliers);
            await context.SaveChangesAsync();
        }

        // 4. Customers & Identity Users
        if (!await context.CustomerProfiles.AnyAsync())
        {
            var customers = new List<CustomerProfile>
            {
                new() {
                    Id = 1, FullName = "Trần Văn Tuấn", Phone = "0912345678", Email = "tuan.tran.demo@example.com",
                    Points = 15, WebAccountActive = true, IsLocked = false, CreatedAt = DateTime.UtcNow.AddMonths(-2),
                    Addresses = new List<CustomerAddress>
                    {
                        new() { Label = "Nhà riêng", RecipientName = "Trần Văn Tuấn", Phone = "0912345678", Address = "Số 42 Đường 3/2, P. Xuân Khánh, Q. Ninh Kiều, Cần Thơ", IsDefault = true },
                        new() { Label = "Văn phòng", RecipientName = "Trần Văn Tuấn", Phone = "0912345678", Address = "Tầng 4 Tòa nhà Công nghệ, Q. Cái Răng, Cần Thơ", IsDefault = false }
                    }
                },
                new() {
                    Id = 2, FullName = "Lê Thị Mai", Phone = "0987654321", Email = "mai.le.demo@example.com",
                    Points = 5, WebAccountActive = true, IsLocked = false, CreatedAt = DateTime.UtcNow.AddMonths(-1),
                    Addresses = new List<CustomerAddress>
                    {
                        new() { Label = "Nhà riêng", RecipientName = "Lê Thị Mai", Phone = "0987654321", Address = "118 Nguyễn Thị Minh Khai, Quận 3, TP. Hồ Chí Minh", IsDefault = true }
                    }
                },
                new() {
                    Id = 3, FullName = "Nguyễn Hoàng Nam", Phone = "0907010001", Email = "nam.nguyen.demo@example.com",
                    Points = 22, WebAccountActive = true, IsLocked = false, CreatedAt = DateTime.UtcNow.AddDays(-25),
                    Addresses = new List<CustomerAddress>
                    {
                        new() { Label = "Nhà riêng", RecipientName = "Nguyễn Hoàng Nam", Phone = "0907010001", Address = "25 Lý Thường Kiệt, Q. Hoàn Kiếm, Hà Nội", IsDefault = true }
                    }
                },
                new() {
                    Id = 4, FullName = "Phạm Thu Hà", Phone = "0907010002", Email = "ha.pham.demo@example.com",
                    Points = 8, WebAccountActive = true, IsLocked = false, CreatedAt = DateTime.UtcNow.AddDays(-20),
                    Addresses = new List<CustomerAddress>
                    {
                        new() { Label = "Nhà riêng", RecipientName = "Phạm Thu Hà", Phone = "0907010002", Address = "89 Lê Lợi, TP. Huế", IsDefault = true }
                    }
                },
                new() {
                    Id = 5, FullName = "Võ Minh Khôi", Phone = "0907010003", Email = "khoi.vo.demo@example.com",
                    Points = 0, WebAccountActive = true, IsLocked = false, CreatedAt = DateTime.UtcNow.AddDays(-15),
                    Addresses = new List<CustomerAddress>
                    {
                        new() { Label = "Nhà riêng", RecipientName = "Võ Minh Khôi", Phone = "0907010003", Address = "14 Nguyễn Văn Linh, Q. Hải Châu, Đà Nẵng", IsDefault = true }
                    }
                },
                new() {
                    Id = 6, FullName = "Đặng Ngọc Ánh", Phone = "0907010004", Email = "anh.dang.demo@example.com",
                    Points = 12, WebAccountActive = true, IsLocked = false, CreatedAt = DateTime.UtcNow.AddDays(-10),
                    Addresses = new List<CustomerAddress>
                    {
                        new() { Label = "Nhà riêng", RecipientName = "Đặng Ngọc Ánh", Phone = "0907010004", Address = "56 Đại lộ Hòa Bình, Q. Ninh Kiều, Cần Thơ", IsDefault = true }
                    }
                },
                new() {
                    Id = 7, FullName = "Bùi Quốc Bảo", Phone = "0907010005", Email = "bao.bui.demo@example.com",
                    Points = 3, WebAccountActive = true, IsLocked = true, CreatedAt = DateTime.UtcNow.AddDays(-8),
                    Addresses = new List<CustomerAddress>
                    {
                        new() { Label = "Nhà riêng", RecipientName = "Bùi Quốc Bảo", Phone = "0907010005", Address = "102 Trần Phú, TP. Nha Trang", IsDefault = true }
                    }
                },
                new() {
                    Id = 8, FullName = "Hoàng Thanh Trúc", Phone = "0907010006", Email = "truc.hoang.demo@example.com",
                    Points = 19, WebAccountActive = false, IsLocked = false, CreatedAt = DateTime.UtcNow.AddDays(-5),
                    Addresses = new List<CustomerAddress>()
                }
            };
            await context.CustomerProfiles.AddRangeAsync(customers);
            await context.SaveChangesAsync();
        }

        // Identity Seed Users
        // Admin user
        var adminEmail = "admin@pharmacy.local";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = "admin",
                Email = adminEmail,
                FullName = "Quản lý hệ thống (Admin)",
                EmailConfirmed = true,
                IsLocked = false
            };
            var createRes = await userManager.CreateAsync(adminUser, "Admin@123456");
            if (createRes.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }

        // Customer user (Trần Văn Tuấn)
        var cust1 = await context.CustomerProfiles.FirstOrDefaultAsync(c => c.Id == 1);
        if (cust1 != null)
        {
            var custUser = await userManager.FindByEmailAsync(cust1.Email);
            if (custUser == null)
            {
                custUser = new ApplicationUser
                {
                    UserName = cust1.Phone,
                    Email = cust1.Email,
                    PhoneNumber = cust1.Phone,
                    FullName = cust1.FullName,
                    EmailConfirmed = true,
                    IsLocked = false,
                    CustomerProfileId = cust1.Id
                };
                var createCustRes = await userManager.CreateAsync(custUser, "Customer@123456");
                if (createCustRes.Succeeded)
                {
                    await userManager.AddToRoleAsync(custUser, "Customer");
                    cust1.UserId = custUser.Id;
                    await context.SaveChangesAsync();
                }
            }
        }

        // Customer user (Lê Thị Mai)
        var cust2 = await context.CustomerProfiles.FirstOrDefaultAsync(c => c.Id == 2);
        if (cust2 != null)
        {
            var custUser2 = await userManager.FindByEmailAsync(cust2.Email);
            if (custUser2 == null)
            {
                custUser2 = new ApplicationUser
                {
                    UserName = cust2.Phone,
                    Email = cust2.Email,
                    PhoneNumber = cust2.Phone,
                    FullName = cust2.FullName,
                    EmailConfirmed = true,
                    IsLocked = false,
                    CustomerProfileId = cust2.Id
                };
                var createCustRes2 = await userManager.CreateAsync(custUser2, "Customer@123456");
                if (createCustRes2.Succeeded)
                {
                    await userManager.AddToRoleAsync(custUser2, "Customer");
                    cust2.UserId = custUser2.Id;
                    await context.SaveChangesAsync();
                }
            }
        }

        // 5. Medicines (20 items)
        if (!await context.Medicines.AnyAsync())
        {
            var medicines = new List<Medicine>
            {
                new() {
                    Id = 1, CategoryId = 1, SupplierId = 1, BarCode = "8935001800112",
                    Name = "Panadol Extra", Slug = "panadol-extra",
                    ActiveIngredient = "Paracetamol 500mg, Caffeine 65mg",
                    Unit = "Hộp", PackagingSpec = "Hộp 15 vỉ × 12 viên nén",
                    Price = 45000m, IsPrescription = false, IsSelling = true, IsActive = true, Featured = true,
                    DescriptionHtml = "<p><strong>Quy cách đóng gói:</strong> Hộp 15 vỉ × 12 viên nén bao phim.</p><p><strong>Hoạt chất niêm yết:</strong> Paracetamol 500mg, Caffeine 65mg.</p><ul><li>Đơn vị bán trên website: <strong>Hộp</strong>.</li><li>Bảo quản nơi khô mát, dưới 30°C.</li></ul>"
                },
                new() {
                    Id = 2, CategoryId = 1, SupplierId = 1, BarCode = "8935001800113",
                    Name = "Hapacol 250 Sủi Bọt", Slug = "hapacol-250",
                    ActiveIngredient = "Paracetamol 250mg",
                    Unit = "Hộp", PackagingSpec = "Hộp 24 gói bột sủi bọt hương cam",
                    Price = 38000m, IsPrescription = false, IsSelling = true, IsActive = true, Featured = true,
                    DescriptionHtml = "<p><strong>Quy cách:</strong> Hộp 24 gói bột sủi bọt.</p><p><strong>Hoạt chất:</strong> Paracetamol 250mg.</p><p>Hòa tan gói bột vào lượng nước vừa đủ trước khi dùng.</p>"
                },
                new() {
                    Id = 3, CategoryId = 2, SupplierId = 2, BarCode = "8935001800229",
                    Name = "Augmentin 1g", Slug = "augmentin-1g",
                    ActiveIngredient = "Amoxicillin 875mg, Clavulanic acid 125mg",
                    Unit = "Hộp", PackagingSpec = "Hộp 2 vỉ × 7 viên nén bao phim",
                    Price = 240000m, IsPrescription = true, IsSelling = true, IsActive = true, Featured = true,
                    DescriptionHtml = "<p><strong>Phân loại:</strong> Thuốc cần đơn bác sĩ — Chỉ bán khi có đơn thuốc hợp lệ và tư vấn của dược sĩ.</p><p><strong>Thành phần:</strong> Amoxicillin 875mg + Acid Clavulanic 125mg.</p>"
                },
                new() {
                    Id = 4, CategoryId = 3, SupplierId = 3, BarCode = "8935001800336",
                    Name = "Boston C 1000mg", Slug = "boston-c-1000mg",
                    ActiveIngredient = "Vitamin C (Acid Ascorbic) 1000mg",
                    Unit = "Tuýp", PackagingSpec = "Tuýp 20 viên sủi hương cam",
                    Price = 35000m, IsPrescription = false, IsSelling = true, IsActive = true, Featured = true,
                    DescriptionHtml = "<p><strong>Quy cách:</strong> Tuýp 20 viên nén sủi bọt.</p><p><strong>Hoạt chất:</strong> Vitamin C 1000mg.</p>"
                },
                new() {
                    Id = 5, CategoryId = 5, SupplierId = 3, BarCode = "9992030000005",
                    Name = "Loratadine 10mg Boston", Slug = "loratadine-10mg",
                    ActiveIngredient = "Loratadine 10mg",
                    Unit = "Hộp", PackagingSpec = "Hộp 2 vỉ × 10 viên nén",
                    Price = 28000m, IsPrescription = false, IsSelling = true, IsActive = true, Featured = true,
                    DescriptionHtml = "<p><strong>Hoạt chất:</strong> Loratadine 10mg.</p><p>Hỗ trợ giảm triệu chứng dị ứng, viêm mũi dị ứng.</p>"
                },
                new() {
                    Id = 6, CategoryId = 5, SupplierId = 5, BarCode = "9992030000006",
                    Name = "Cetirizine 10mg Imexpharm", Slug = "cetirizine-10mg",
                    ActiveIngredient = "Cetirizine dihydrochloride 10mg",
                    Unit = "Hộp", PackagingSpec = "Hộp 10 vỉ × 10 viên bao phim",
                    Price = 52000m, IsPrescription = false, IsSelling = true, IsActive = true, Featured = false,
                    DescriptionHtml = "<p><strong>Hoạt chất:</strong> Cetirizine dihydrochloride 10mg.</p><p>Sản phẩm minh họa tình huống tồn kho thấp (3 hộp có thể bán).</p>"
                },
                new() {
                    Id = 7, CategoryId = 4, SupplierId = 1, BarCode = "9992030000007",
                    Name = "Omeprazole 20mg DHG", Slug = "omeprazole-20mg",
                    ActiveIngredient = "Omeprazole 20mg",
                    Unit = "Hộp", PackagingSpec = "Hộp 3 vỉ × 10 viên nang cứng vi hạt",
                    Price = 42000m, IsPrescription = false, IsSelling = true, IsActive = true, Featured = true,
                    DescriptionHtml = "<p><strong>Hoạt chất:</strong> Omeprazole 20mg.</p><p>Giảm tiết acid dạ dày, hỗ trợ điều trị viêm loét dạ dày - tá tràng.</p>"
                },
                new() {
                    Id = 8, CategoryId = 4, SupplierId = 2, BarCode = "9992030000008",
                    Name = "Lansoprazole 30mg", Slug = "lansoprazole-30mg",
                    ActiveIngredient = "Lansoprazole 30mg",
                    Unit = "Hộp", PackagingSpec = "Hộp 2 vỉ × 14 viên nang",
                    Price = 115000m, IsPrescription = true, IsSelling = true, IsActive = true, Featured = false,
                    DescriptionHtml = "<p><strong>Hoạt chất:</strong> Lansoprazole 30mg. Thuốc kê đơn theo chỉ định của bác sĩ chuyên khoa.</p>"
                },
                new() {
                    Id = 9, CategoryId = 2, SupplierId = 5, BarCode = "9992030000009",
                    Name = "Amoxicillin 500mg Imexpharm", Slug = "amoxicillin-500mg",
                    ActiveIngredient = "Amoxicillin trihydrate 500mg",
                    Unit = "Hộp", PackagingSpec = "Hộp 10 vỉ × 10 viên nang cứng",
                    Price = 85000m, IsPrescription = true, IsSelling = true, IsActive = true, Featured = false,
                    DescriptionHtml = "<p>Kháng sinh Amoxicillin 500mg — Cần đơn bác sĩ và tư vấn dược sĩ.</p>"
                },
                new() {
                    Id = 10, CategoryId = 2, SupplierId = 3, BarCode = "9992030000010",
                    Name = "Azithromycin 500mg", Slug = "azithromycin-500mg",
                    ActiveIngredient = "Azithromycin dihydrate 500mg",
                    Unit = "Hộp", PackagingSpec = "Hộp 1 vỉ × 3 viên bao phim",
                    Price = 72000m, IsPrescription = true, IsSelling = true, IsActive = true, Featured = false,
                    DescriptionHtml = "<p>Kháng sinh nhóm Macrolid liều 3 ngày. Thuốc kê đơn.</p>"
                },
                new() {
                    Id = 11, CategoryId = 4, SupplierId = 4, BarCode = "9992030000011",
                    Name = "Berberin 100mg Traphaco", Slug = "berberin-100mg",
                    ActiveIngredient = "Berberin clorid 100mg",
                    Unit = "Chai", PackagingSpec = "Chai 100 viên bao đường",
                    Price = 48000m, IsPrescription = false, IsSelling = true, IsActive = true, Featured = true,
                    DescriptionHtml = "<p>Berberin thảo dược dùng khi rối loạn tiêu hóa, tiêu chảy.</p>"
                },
                new() {
                    Id = 12, CategoryId = 4, SupplierId = 1, BarCode = "9992030000012",
                    Name = "Oresol 245 DHG", Slug = "oresol-245",
                    ActiveIngredient = "Glucose khan, Natri clorid, Kali clorid, Natri citrat",
                    Unit = "Hộp", PackagingSpec = "Hộp 20 gói bột bù nước",
                    Price = 36000m, IsPrescription = false, IsSelling = true, IsActive = true, Featured = true,
                    DescriptionHtml = "<p>Bột pha dung dịch bù nước và điện giải tiêu chuẩn WHO.</p>"
                },
                new() {
                    Id = 13, CategoryId = 4, SupplierId = 2, BarCode = "9992030000013",
                    Name = "Smecta Hỗn Dịch Uống", Slug = "smecta-hon-dich",
                    ActiveIngredient = "Diosmectite 3g",
                    Unit = "Hộp", PackagingSpec = "Hộp 30 gói hỗn dịch uống hương cam vani",
                    Price = 118000m, IsPrescription = false, IsSelling = true, IsActive = true, Featured = true,
                    DescriptionHtml = "<p>Bảo vệ niêm mạc tiêu hóa, cầm tiêu chảy cấp và mạn tính.</p>"
                },
                new() {
                    Id = 14, CategoryId = 1, SupplierId = 2, BarCode = "9992030000014",
                    Name = "Efferalgan 500mg Viên Sủi", Slug = "efferalgan-500mg",
                    ActiveIngredient = "Paracetamol 500mg",
                    Unit = "Tuýp", PackagingSpec = "Tuýp 16 viên nén sủi bọt",
                    Price = 54000m, IsPrescription = false, IsSelling = true, IsActive = true, Featured = true,
                    DescriptionHtml = "<p>Giảm đau hạ sốt sủi bọt tác dụng nhanh.</p>"
                },
                new() {
                    Id = 15, CategoryId = 3, SupplierId = 4, BarCode = "9992030000015",
                    Name = "Kẽm Hữu Cơ Zinc Gluconate 70mg", Slug = "zinc-gluconate-70mg",
                    ActiveIngredient = "Zinc Gluconate 70mg (tương đương 10mg Kẽm)",
                    Unit = "Hộp", PackagingSpec = "Hộp 3 vỉ × 10 viên nén",
                    Price = 65000m, IsPrescription = false, IsSelling = true, IsActive = true, Featured = false,
                    DescriptionHtml = "<p>Bổ sung kẽm cho cơ thể, nâng cao đề kháng.</p>"
                },
                new() {
                    Id = 16, CategoryId = 3, SupplierId = 3, BarCode = "9992030000016",
                    Name = "Vitamin D3 1000IU Softgels", Slug = "vitamin-d3-1000iu",
                    ActiveIngredient = "Cholecalciferol (Vitamin D3) 1000 IU",
                    Unit = "Chai", PackagingSpec = "Chai 60 viên nang mềm",
                    Price = 95000m, IsPrescription = false, IsSelling = true, IsActive = true, Featured = false,
                    DescriptionHtml = "<p>Sản phẩm minh họa tình huống hết tồn có thể bán (lô đang khóa/hết hạn).</p>"
                },
                new() {
                    Id = 17, CategoryId = 5, SupplierId = 4, BarCode = "9992030000017",
                    Name = "Nước Muối Sinh Lý Natri Clorid 0.9%", Slug = "natri-clorid-0-9",
                    ActiveIngredient = "Natri Clorid 0.9%",
                    Unit = "Chai", PackagingSpec = "Chai 500ml dung dịch vệ sinh",
                    Price = 12000m, IsPrescription = false, IsSelling = true, IsActive = true, Featured = false,
                    DescriptionHtml = "<p>Dung dịch súc miệng và rửa ngoài y tế.</p>"
                },
                new() {
                    Id = 18, CategoryId = 6, SupplierId = 4, BarCode = "9992030000018",
                    Name = "Gạc Y Tế Tiệt Trùng 10x10cm", Slug = "gac-y-te-tiet-trung",
                    ActiveIngredient = "100% Cotton y tế tiệt trùng EO Gas",
                    Unit = "Gói", PackagingSpec = "Gói 10 miếng gạc 8 lớp",
                    Price = 18000m, IsPrescription = false, IsSelling = true, IsActive = true, Featured = false,
                    DescriptionHtml = "<p>Gạc y tế tiệt trùng chăm sóc vết thương ngoài da.</p>"
                },
                new() {
                    Id = 19, CategoryId = 6, SupplierId = 2, BarCode = "9992030000019",
                    Name = "Nhiệt Kế Điện Tử Đầu Mềm", Slug = "nhiet-ke-dien-tu",
                    ActiveIngredient = "Cảm biến nhiệt điện tử sai số ±0.1°C",
                    Unit = "Hộp", PackagingSpec = "Hộp 1 máy kèm pin và vỏ bảo vệ",
                    Price = 145000m, IsPrescription = false, IsSelling = false, IsActive = true, Featured = false,
                    DescriptionHtml = "<p>Minh họa sản phẩm Ngừng bán (IsSelling = false) tồn có thể bán bằng 0.</p>"
                },
                new() {
                    Id = 20, CategoryId = 5, SupplierId = 5, BarCode = "9992030000020",
                    Name = "Siro Thảo Dược Lá Thường Xuân", Slug = "siro-la-thuong-xuan",
                    ActiveIngredient = "Cao khô lá thường xuân 0.7g / 100ml",
                    Unit = "Chai", PackagingSpec = "Chai thủy tinh 100ml kèm cốc đong",
                    Price = 78000m, IsPrescription = false, IsSelling = true, IsActive = false, Featured = false,
                    DescriptionHtml = "<p>Minh họa sản phẩm Đã ẩn (IsActive = false) — không hiển thị ở Storefront.</p>"
                }
            };
            await context.Medicines.AddRangeAsync(medicines);
            await context.SaveChangesAsync();
        }

        // 6. Batches (24 batches)
        if (!await context.Batches.AnyAsync())
        {
            var batches = new List<Batch>
            {
                new() { Id = 1, MedicineId = 1, BatchNumber = "LOT-PND-2024A", ExpiryDate = DateTime.UtcNow.AddDays(40), Stock = 25, ReservedQuantity = 0, IsBlocked = false }, // Sắp hết hạn (<90 ngày)
                new() { Id = 2, MedicineId = 1, BatchNumber = "LOT-PND-2025B", ExpiryDate = DateTime.UtcNow.AddYears(1), Stock = 60, ReservedQuantity = 0, IsBlocked = false },
                new() { Id = 3, MedicineId = 2, BatchNumber = "LOT-HPC-2025A", ExpiryDate = DateTime.UtcNow.AddYears(1), Stock = 80, ReservedQuantity = 0, IsBlocked = false },
                new() { Id = 4, MedicineId = 3, BatchNumber = "LOT-AUG-2024X", ExpiryDate = DateTime.UtcNow.AddMonths(8), Stock = 30, ReservedQuantity = 0, IsBlocked = false },
                new() { Id = 5, MedicineId = 4, BatchNumber = "LOT-BTC-2025M", ExpiryDate = DateTime.UtcNow.AddYears(2), Stock = 50, ReservedQuantity = 0, IsBlocked = false },
                new() { Id = 6, MedicineId = 5, BatchNumber = "PH26-LOR-01", ExpiryDate = DateTime.UtcNow.AddMonths(10), Stock = 22, ReservedQuantity = 0, IsBlocked = false },
                new() { Id = 7, MedicineId = 5, BatchNumber = "PH26-LOR-02", ExpiryDate = DateTime.UtcNow.AddDays(-30), Stock = 12, ReservedQuantity = 0, IsBlocked = false }, // Đã hết hạn
                new() { Id = 8, MedicineId = 6, BatchNumber = "PH26-CET-01", ExpiryDate = DateTime.UtcNow.AddMonths(6), Stock = 3, ReservedQuantity = 0, IsBlocked = false },  // Tồn thấp
                new() { Id = 9, MedicineId = 6, BatchNumber = "PH26-CET-02", ExpiryDate = DateTime.UtcNow.AddMonths(12), Stock = 15, ReservedQuantity = 0, IsBlocked = true }, // Tạm khóa
                new() { Id = 10, MedicineId = 7, BatchNumber = "PH26-OME-01", ExpiryDate = DateTime.UtcNow.AddYears(1), Stock = 45, ReservedQuantity = 0, IsBlocked = false },
                new() { Id = 11, MedicineId = 8, BatchNumber = "PH26-LAN-01", ExpiryDate = DateTime.UtcNow.AddMonths(9), Stock = 18, ReservedQuantity = 0, IsBlocked = false },
                new() { Id = 12, MedicineId = 9, BatchNumber = "PH26-AMO-01", ExpiryDate = DateTime.UtcNow.AddMonths(11), Stock = 35, ReservedQuantity = 0, IsBlocked = false },
                new() { Id = 13, MedicineId = 10, BatchNumber = "PH26-AZI-01", ExpiryDate = DateTime.UtcNow.AddDays(50), Stock = 14, ReservedQuantity = 0, IsBlocked = false }, // Sắp hết hạn
                new() { Id = 14, MedicineId = 11, BatchNumber = "PH26-BER-01", ExpiryDate = DateTime.UtcNow.AddYears(2), Stock = 65, ReservedQuantity = 0, IsBlocked = false },
                new() { Id = 15, MedicineId = 12, BatchNumber = "PH26-ORE-01", ExpiryDate = DateTime.UtcNow.AddYears(2), Stock = 90, ReservedQuantity = 0, IsBlocked = false },
                new() { Id = 16, MedicineId = 13, BatchNumber = "PH26-SME-01", ExpiryDate = DateTime.UtcNow.AddYears(1), Stock = 28, ReservedQuantity = 0, IsBlocked = false },
                new() { Id = 17, MedicineId = 14, BatchNumber = "PH26-EFF-01", ExpiryDate = DateTime.UtcNow.AddMonths(10), Stock = 40, ReservedQuantity = 0, IsBlocked = false },
                new() { Id = 18, MedicineId = 15, BatchNumber = "PH26-ZIN-01", ExpiryDate = DateTime.UtcNow.AddYears(1), Stock = 24, ReservedQuantity = 0, IsBlocked = false },
                new() { Id = 19, MedicineId = 16, BatchNumber = "PH26-VD3-LOCK", ExpiryDate = DateTime.UtcNow.AddMonths(8), Stock = 20, ReservedQuantity = 0, IsBlocked = true }, // Khóa
                new() { Id = 20, MedicineId = 16, BatchNumber = "PH26-VD3-EXP", ExpiryDate = DateTime.UtcNow.AddDays(-60), Stock = 8, ReservedQuantity = 0, IsBlocked = false },  // Hết hạn
                new() { Id = 21, MedicineId = 17, BatchNumber = "PH26-NACL-01", ExpiryDate = DateTime.UtcNow.AddYears(2), Stock = 120, ReservedQuantity = 0, IsBlocked = false },
                new() { Id = 22, MedicineId = 18, BatchNumber = "PH26-GAC-01", ExpiryDate = DateTime.UtcNow.AddYears(2), Stock = 75, ReservedQuantity = 0, IsBlocked = false },
                new() { Id = 23, MedicineId = 19, BatchNumber = "PH26-THERM-01", ExpiryDate = DateTime.UtcNow.AddYears(3), Stock = 10, ReservedQuantity = 0, IsBlocked = false },
                new() { Id = 24, MedicineId = 20, BatchNumber = "PH26-SIRO-01", ExpiryDate = DateTime.UtcNow.AddMonths(8), Stock = 16, ReservedQuantity = 0, IsBlocked = false }
            };
            await context.Batches.AddRangeAsync(batches);
            await context.SaveChangesAsync();
        }

        // 7. Vouchers (6 vouchers)
        if (!await context.Vouchers.AnyAsync())
        {
            var vouchers = new List<Voucher>
            {
                new() { Id = 1, Code = "GIAM10K", DiscountType = "FIXED", DiscountValue = 10000m, MinOrderValue = 50000m, MaxDiscountCap = null, Quantity = 50, IsActive = true, Description = "Giảm trực tiếp 10.000 ₫ cho đơn từ 50.000 ₫" },
                new() { Id = 2, Code = "PH5K", DiscountType = "FIXED", DiscountValue = 5000m, MinOrderValue = 0m, MaxDiscountCap = null, Quantity = 35, IsActive = true, Description = "Giảm 5.000 ₫ cho mọi đơn hàng" },
                new() { Id = 3, Code = "PH25K", DiscountType = "FIXED", DiscountValue = 25000m, MinOrderValue = 0m, MaxDiscountCap = null, Quantity = 15, IsActive = true, Description = "Giảm 25.000 ₫ (tự động giới hạn không vượt quá tiền hàng)" },
                new() { Id = 4, Code = "PH10", DiscountType = "PERCENT", DiscountValue = 10m, MinOrderValue = 80000m, MaxDiscountCap = 30000m, Quantity = 20, IsActive = true, Description = "Giảm 10% (tối đa 30.000 ₫) cho đơn từ 80.000 ₫" },
                new() { Id = 5, Code = "PHHET", DiscountType = "FIXED", DiscountValue = 15000m, MinOrderValue = 50000m, MaxDiscountCap = null, Quantity = 0, IsActive = true, Description = "Voucher mẫu đã hết lượt sử dụng (Quantity = 0)" },
                new() { Id = 6, Code = "PHKHOA", DiscountType = "PERCENT", DiscountValue = 20m, MinOrderValue = 100000m, MaxDiscountCap = 50000m, Quantity = 10, IsActive = false, Description = "Voucher mẫu đang tạm khóa (IsActive = false)" }
            };
            await context.Vouchers.AddRangeAsync(vouchers);
            await context.SaveChangesAsync();
        }

        // 8. Orders (Historical & Seed Orders)
        if (!await context.Orders.AnyAsync())
        {
            var orders = new List<Order>
            {
                new() {
                    Id = 1, Code = "HD00923", CustomerId = 1, CustomerName = "Trần Văn Tuấn", CustomerPhone = "0912345678",
                    ShippingAddress = "Số 42 Đường 3/2, P. Xuân Khánh, Q. Ninh Kiều, Cần Thơ", Channel = "WEB",
                    OrderDate = DateTime.UtcNow.AddDays(-18),
                    OrderStatus = "COMPLETED", PaymentMethod = "COD", PaymentStatus = "PAID", ShippingStatus = "DELIVERED",
                    VoucherCode = "GIAM10K", SubtotalAmount = 135000m, DiscountAmount = 10000m, ShippingFee = 0m, TotalAmount = 125000m,
                    OrderVersion = 3, Note = "Giao giờ hành chính", PaidAt = DateTime.UtcNow.AddDays(-17), PaidByEmployee = "admin",
                    Items = new List<OrderItem>
                    {
                        new() { MedicineId = 1, MedicineName = "Panadol Extra", Unit = "Hộp", Quantity = 2, UnitPrice = 45000m, TotalPrice = 90000m },
                        new() { MedicineId = 11, MedicineName = "Berberin 100mg Traphaco", Unit = "Chai", Quantity = 1, UnitPrice = 45000m, TotalPrice = 45000m }
                    },
                    History = new List<OrderStatusHistory>
                    {
                        new() { Time = DateTime.UtcNow.AddDays(-18), Actor = "Khách hàng", Text = "Tạo đơn hàng trên website (COD)" },
                        new() { Time = DateTime.UtcNow.AddDays(-18).AddHours(1), Actor = "Admin", Text = "Xác nhận đơn & xuất kho theo lô FEFO" },
                        new() { Time = DateTime.UtcNow.AddDays(-17), Actor = "Admin", Text = "Giao hàng thành công & Xác nhận đã thu tiền COD" }
                    }
                },
                new() {
                    Id = 2, Code = "HD00931", CustomerId = 1, CustomerName = "Trần Văn Tuấn", CustomerPhone = "0912345678",
                    ShippingAddress = "Tầng 4 Tòa nhà Công nghệ, Q. Cái Răng, Cần Thơ", Channel = "WEB",
                    OrderDate = DateTime.UtcNow.AddHours(-3),
                    OrderStatus = "NEW", PaymentMethod = "COD", PaymentStatus = "UNPAID", ShippingStatus = "NOT_SHIPPED",
                    VoucherCode = null, SubtotalAmount = 96000m, DiscountAmount = 0m, ShippingFee = 15000m, TotalAmount = 111000m,
                    OrderVersion = 1, Note = "Đơn mới đặt — khách có thể thử bấm Yêu cầu hủy đơn",
                    Items = new List<OrderItem>
                    {
                        new() { MedicineId = 11, MedicineName = "Berberin 100mg Traphaco", Unit = "Chai", Quantity = 2, UnitPrice = 48000m, TotalPrice = 96000m }
                    },
                    History = new List<OrderStatusHistory>
                    {
                        new() { Time = DateTime.UtcNow.AddHours(-3), Actor = "Khách hàng", Text = "Tạo đơn hàng mới (Chưa thanh toán, Chưa giao)" }
                    }
                }
            };
            await context.Orders.AddRangeAsync(orders);
            await context.SaveChangesAsync();
        }

        // 9. StockIns
        if (!await context.StockIns.AnyAsync())
        {
            var stockIns = new List<StockIn>
            {
                new() {
                    Id = 1, Code = "PN2026-018", SupplierId = 1, EmployeeName = "Quản lý hệ thống (admin)",
                    ImportDate = DateTime.UtcNow.AddDays(-20), TotalCost = 3520000m, Note = "Nhập định kỳ tháng 10/2026",
                    Details = new List<StockInDetail>
                    {
                        new() { MedicineId = 1, BatchNumber = "LOT-PND-2025B", ExpiryDate = DateTime.UtcNow.AddYears(1), Quantity = 60, UnitCost = 34000m, TotalCost = 2040000m },
                        new() { MedicineId = 2, BatchNumber = "LOT-HPC-2025A", ExpiryDate = DateTime.UtcNow.AddYears(1), Quantity = 50, UnitCost = 29600m, TotalCost = 1480000m }
                    }
                }
            };
            await context.StockIns.AddRangeAsync(stockIns);
            await context.SaveChangesAsync();
        }

        // 10. Prescription Requests
        if (!await context.PrescriptionRequests.AnyAsync())
        {
            var requests = new List<PrescriptionRequest>
            {
                new() {
                    Id = 1, Code = "RX-2026-001", CustomerId = 1, PatientName = "Trần Văn Tuấn", Phone = "0912345678",
                    CreatedAt = DateTime.UtcNow.AddDays(-3), Status = "RESPONDED",
                    CustomerNote = "Tôi có đơn khám viêm họng tại bệnh viện, nhờ nhà thuốc kiểm tra giúp đơn thuốc đính kèm.",
                    PharmacistNote = "Nhà thuốc đã đối chiếu ảnh đơn thuốc RX-2026-001. Sản phẩm Augmentin 1g hiện còn hàng tại quầy. Quý khách vui lòng mang bản gốc đơn thuốc tới quầy nhận thuốc.",
                    UpdatedAt = DateTime.UtcNow.AddDays(-3).AddHours(2)
                },
                new() {
                    Id = 2, Code = "RX-2026-002", CustomerId = 1, PatientName = "Trần Văn Tuấn", Phone = "0912345678",
                    CreatedAt = DateTime.UtcNow.AddDays(-1), Status = "NEED_MORE_INFO",
                    CustomerNote = "Nhờ dược sĩ xem giúp đơn thuốc dạ dày này.",
                    PharmacistNote = "Ảnh chụp bị khuất góc chữ ký bác sĩ. Quý khách vui lòng chụp lại toàn bộ mặt đơn.",
                    UpdatedAt = DateTime.UtcNow.AddDays(-1).AddHours(1)
                },
                new() {
                    Id = 3, Code = "RX-2026-003", CustomerId = 2, PatientName = "Lê Thị Mai", Phone = "0987654321",
                    CreatedAt = DateTime.UtcNow.AddHours(-5), Status = "PENDING_REVIEW",
                    CustomerNote = "Tôi gửi đơn khám ngoại trú sáng nay, cần tư vấn tình trạng còn hàng.",
                    PharmacistNote = null
                }
            };
            await context.PrescriptionRequests.AddRangeAsync(requests);
            await context.SaveChangesAsync();
        }
    }
}
