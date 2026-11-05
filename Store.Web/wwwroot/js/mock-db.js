/* ==========================================================================
   mock-db.js — Cơ sở dữ liệu Mẫu Dùng chung (Shared In-Memory + LocalStorage)
   Tuân thủ đúng quy tắc nghiệp vụ của Store.Shared, Store.DAL và SELLING-STATUS.md
   ========================================================================== */

window.PharmacyMockDB = (function () {
  const DB_KEY = 'pharmacy_web_prototype_db_v2';
  const SESSION_KEY = 'pharmacy_web_prototype_session_v2';
  const TODAY_STR = '2026-11-03'; // Ngày tham chiếu của môi trường hiện tại

  function getInitialData() {
    const categories = [
      { id: 1, name: 'Thuốc giảm đau - hạ sốt', slug: 'giam-dau-ha-sot', description: 'Các sản phẩm giảm đau, hạ sốt thông dụng' },
      { id: 2, name: 'Kháng sinh - Kháng viêm', slug: 'khang-sinh-khang-viem', description: 'Nhóm thuốc kháng sinh, kháng viêm cần đơn và tư vấn dược sĩ' },
      { id: 3, name: 'Vitamin & Khoáng chất', slug: 'vitamin-khoang-chat', description: 'Bổ sung vitamin, khoáng chất nâng cao sức đề kháng' },
      { id: 4, name: 'Tiêu hóa - Dạ dày', slug: 'tieu-hoa-da-day', description: 'Hỗ trợ tiêu hóa, giảm tiết acid dạ dày, bù nước điện giải' },
      { id: 5, name: 'Dị ứng - Hô hấp', slug: 'di-ung-ho-hap', description: 'Thuốc chống dị ứng, siro ho, dung dịch vệ sinh mũi họng' },
      { id: 6, name: 'Vật tư & Thiết bị y tế', slug: 'vat-tu-y-te', description: 'Băng gạc tiệt trùng, nhiệt kế, khẩu trang và chăm sóc vết thương' }
    ];

    const suppliers = [
      { id: 1, name: 'Dược Hậu Giang (DHG Pharma)', phone: '02923891433', address: '288 Bis Nguyễn Văn Cừ, Q. Ninh Kiều, Cần Thơ' },
      { id: 2, name: 'Sanofi Việt Nam', phone: '02838298526', address: 'Khu Công nghệ cao, TP. Thủ Đức, TP. Hồ Chí Minh' },
      { id: 3, name: 'Dược phẩm Boston Việt Nam', phone: '02743769606', address: 'KCN Việt Nam - Singapore, Thuận An, Bình Dương' },
      { id: 4, name: 'Công ty Cổ phần Traphaco', phone: '02436815097', address: '75 Yên Ninh, Q. Ba Đình, Hà Nội' },
      { id: 5, name: 'Công ty CP Dược phẩm Imexpharm', phone: '02773851941', address: 'Số 4 Đường 30/4, TP. Cao Lãnh, Đồng Tháp' }
    ];

    // 20 sản phẩm bao phủ đủ OTC, Rx (IsPrescription), Đang bán, Ngừng bán (IsSelling=false), Đã ẩn (IsActive=false), Hết hàng
    const medicines = [
      {
        id: 1, categoryId: 1, supplierId: 1, barCode: '8935001800112',
        name: 'Panadol Extra', slug: 'panadol-extra',
        activeIngredient: 'Paracetamol 500mg, Caffeine 65mg',
        unit: 'Hộp', packagingSpec: 'Hộp 15 vỉ × 12 viên nén',
        price: 45000, isPrescription: false, isSelling: true, isActive: true,
        featured: true,
        descriptionHtml: '<p><strong>Quy cách đóng gói:</strong> Hộp 15 vỉ × 12 viên nén bao phim.</p><p><strong>Hoạt chất niêm yết:</strong> Paracetamol 500mg, Caffeine 65mg.</p><ul><li>Đơn vị bán trên website: <strong>Hộp</strong> (không tách lẻ vỉ/viên khi chưa có quy tắc quy đổi).</li><li>Bảo quản nơi khô mát, dưới 30°C, tránh ánh nắng trực tiếp.</li></ul>'
      },
      {
        id: 2, categoryId: 1, supplierId: 1, barCode: '8935001800113',
        name: 'Hapacol 250 Sủi Bọt', slug: 'hapacol-250',
        activeIngredient: 'Paracetamol 250mg',
        unit: 'Hộp', packagingSpec: 'Hộp 24 gói bột sủi bọt hương cam',
        price: 38000, isPrescription: false, isSelling: true, isActive: true,
        featured: true,
        descriptionHtml: '<p><strong>Quy cách:</strong> Hộp 24 gói bột sủi bọt.</p><p><strong>Hoạt chất:</strong> Paracetamol 250mg.</p><p>Hòa tan gói bột vào lượng nước vừa đủ trước khi dùng. Đọc kỹ tờ hướng dẫn sử dụng trong hộp.</p>'
      },
      {
        id: 3, categoryId: 2, supplierId: 2, barCode: '8935001800229',
        name: 'Augmentin 1g', slug: 'augmentin-1g',
        activeIngredient: 'Amoxicillin 875mg, Clavulanic acid 125mg',
        unit: 'Hộp', packagingSpec: 'Hộp 2 vỉ × 7 viên nén bao phim',
        price: 240000, isPrescription: true, isSelling: true, isActive: true,
        featured: true,
        descriptionHtml: '<p><strong>Phân loại:</strong> Thuốc cần đơn bác sĩ — Chỉ bán khi có đơn thuốc hợp lệ và tư vấn của dược sĩ.</p><p><strong>Thành phần:</strong> Amoxicillin 875mg + Acid Clavulanic 125mg.</p><p>Vui lòng bấm <em>Gửi đơn tư vấn</em> để tải ảnh đơn thuốc của bác sĩ.</p>'
      },
      {
        id: 4, categoryId: 3, supplierId: 3, barCode: '8935001800336',
        name: 'Boston C 1000mg', slug: 'boston-c-1000mg',
        activeIngredient: 'Vitamin C (Acid Ascorbic) 1000mg',
        unit: 'Tuýp', packagingSpec: 'Tuýp 20 viên sủi hương cam',
        price: 35000, isPrescription: false, isSelling: true, isActive: true,
        featured: true,
        descriptionHtml: '<p><strong>Quy cách:</strong> Tuýp nhựa 20 viên nén sủi bọt.</p><p><strong>Hoạt chất:</strong> Vitamin C 1000mg.</p><p>Đậy kín nắp tuýp có chất hút ẩm ngay sau khi lấy viên sủi.</p>'
      },
      {
        id: 5, categoryId: 5, supplierId: 3, barCode: '9992030000005',
        name: 'Loratadine 10mg Boston', slug: 'loratadine-10mg',
        activeIngredient: 'Loratadine 10mg',
        unit: 'Hộp', packagingSpec: 'Hộp 2 vỉ × 10 viên nén',
        price: 28000, isPrescription: false, isSelling: true, isActive: true,
        featured: true,
        descriptionHtml: '<p><strong>Hoạt chất:</strong> Loratadine 10mg.</p><p><strong>Quy cách:</strong> Hộp 2 vỉ × 10 viên nén.</p>'
      },
      {
        id: 6, categoryId: 5, supplierId: 5, barCode: '9992030000006',
        name: 'Cetirizine 10mg Imexpharm', slug: 'cetirizine-10mg',
        activeIngredient: 'Cetirizine dihydrochloride 10mg',
        unit: 'Hộp', packagingSpec: 'Hộp 10 vỉ × 10 viên bao phim',
        price: 52000, isPrescription: false, isSelling: true, isActive: true,
        featured: false,
        descriptionHtml: '<p><strong>Hoạt chất:</strong> Cetirizine dihydrochloride 10mg.</p><p>Sản phẩm minh họa tình huống tồn kho thấp (chỉ còn 3 hộp có thể bán).</p>'
      },
      {
        id: 7, categoryId: 4, supplierId: 1, barCode: '9992030000007',
        name: 'Omeprazole 20mg DHG', slug: 'omeprazole-20mg',
        activeIngredient: 'Omeprazole 20mg',
        unit: 'Hộp', packagingSpec: 'Hộp 3 vỉ × 10 viên nang cứng vi hạt',
        price: 42000, isPrescription: false, isSelling: true, isActive: true,
        featured: true,
        descriptionHtml: '<p><strong>Hoạt chất:</strong> Omeprazole 20mg.</p><p>Viên nang chứa vi hạt bao tan trong ruột. Bảo quản nơi khô ráo.</p>'
      },
      {
        id: 8, categoryId: 4, supplierId: 2, barCode: '9992030000008',
        name: 'Lansoprazole 30mg', slug: 'lansoprazole-30mg',
        activeIngredient: 'Lansoprazole 30mg',
        unit: 'Hộp', packagingSpec: 'Hộp 2 vỉ × 14 viên nang',
        price: 115000, isPrescription: true, isSelling: true, isActive: true,
        featured: false,
        descriptionHtml: '<p><strong>Phân loại:</strong> Thuốc cần đơn bác sĩ.</p><p><strong>Thành phần:</strong> Lansoprazole 30mg. Cần gửi đơn thuốc để dược sĩ kiểm tra trước khi mua.</p>'
      },
      {
        id: 9, categoryId: 2, supplierId: 5, barCode: '9992030000009',
        name: 'Amoxicillin 500mg Imexpharm', slug: 'amoxicillin-500mg',
        activeIngredient: 'Amoxicillin trihydrate 500mg',
        unit: 'Hộp', packagingSpec: 'Hộp 10 vỉ × 10 viên nang cứng',
        price: 85000, isPrescription: true, isSelling: true, isActive: true,
        featured: false,
        descriptionHtml: '<p><strong>Phân loại:</strong> Thuốc kháng sinh cần đơn bác sĩ.</p><p><strong>Thành phần:</strong> Amoxicillin 500mg. Chỉ xuất bán theo đơn hợp lệ.</p>'
      },
      {
        id: 10, categoryId: 2, supplierId: 2, barCode: '9992030000010',
        name: 'Azithromycin 500mg', slug: 'azithromycin-500mg',
        activeIngredient: 'Azithromycin dihydrate 500mg',
        unit: 'Hộp', packagingSpec: 'Hộp 1 vỉ × 3 viên nén bao phim',
        price: 96000, isPrescription: true, isSelling: true, isActive: true,
        featured: false,
        descriptionHtml: '<p><strong>Phân loại:</strong> Thuốc kháng sinh cần đơn bác sĩ.</p><p><strong>Thành phần:</strong> Azithromycin 500mg.</p>'
      },
      {
        id: 11, categoryId: 4, supplierId: 4, barCode: '9992030000011',
        name: 'Berberin 100mg Traphaco', slug: 'berberin-100mg',
        activeIngredient: 'Berberin clorid 100mg',
        unit: 'Chai', packagingSpec: 'Chai 100 viên bao đường',
        price: 48000, isPrescription: false, isSelling: true, isActive: true,
        featured: true,
        descriptionHtml: '<p><strong>Hoạt chất:</strong> Berberin clorid 100mg chiết xuất thảo dược.</p><p>Đơn vị bán: Chai 100 viên.</p>'
      },
      {
        id: 12, categoryId: 4, supplierId: 1, barCode: '9992030000012',
        name: 'Oresol 245 Bù Nước Điện Giải', slug: 'oresol-245',
        activeIngredient: 'Natri clorid, Kali clorid, Natri citrat, Glucose khan',
        unit: 'Hộp', packagingSpec: 'Hộp 20 gói bột pha dung dịch uống',
        price: 32000, isPrescription: false, isSelling: true, isActive: true,
        featured: false,
        descriptionHtml: '<p><strong>Quy cách:</strong> Hộp 20 gói. Pha đúng 1 gói với 200ml nước đun sôi để nguội theo đúng tỉ lệ ghi trên bao bì.</p>'
      },
      {
        id: 13, categoryId: 4, supplierId: 2, barCode: '9992030000013',
        name: 'Smecta Hỗn Dịch Uống', slug: 'smecta-goi',
        activeIngredient: 'Diosmectite 3g',
        unit: 'Hộp', packagingSpec: 'Hộp 30 gói bột pha hỗn dịch uống',
        price: 118000, isPrescription: false, isSelling: true, isActive: true,
        featured: false,
        descriptionHtml: '<p><strong>Hoạt chất:</strong> Diosmectite 3g.</p><p><strong>Đơn vị tính:</strong> Hộp 30 gói.</p>'
      },
      {
        id: 14, categoryId: 1, supplierId: 2, barCode: '9992030000014',
        name: 'Efferalgan 500mg Viên Sủi', slug: 'efferalgan-500mg',
        activeIngredient: 'Paracetamol 500mg',
        unit: 'Tuýp', packagingSpec: 'Tuýp 16 viên nén sủi bọt',
        price: 54000, isPrescription: false, isSelling: true, isActive: true,
        featured: true,
        descriptionHtml: '<p><strong>Hoạt chất:</strong> Paracetamol 500mg dạng sủi bọt hòa tan nhanh.</p><p>Đơn vị bán: Tuýp 16 viên.</p>'
      },
      {
        id: 15, categoryId: 3, supplierId: 4, barCode: '9992030000015',
        name: 'Kẽm Hữu Cơ Zinc Gluconate 70mg', slug: 'zinc-gluconate-70mg',
        activeIngredient: 'Zinc Gluconate 70mg (tương đương 10mg Kẽm)',
        unit: 'Hộp', packagingSpec: 'Hộp 3 vỉ × 10 viên nén',
        price: 65000, isPrescription: false, isSelling: true, isActive: true,
        featured: false,
        descriptionHtml: '<p>Bổ sung kẽm cho cơ thể. Đơn vị bán: Hộp 30 viên.</p>'
      },
      {
        id: 16, categoryId: 3, supplierId: 3, barCode: '9992030000016',
        name: 'Vitamin D3 1000IU Softgels', slug: 'vitamin-d3-1000iu',
        activeIngredient: 'Cholecalciferol (Vitamin D3) 1000 IU',
        unit: 'Chai', packagingSpec: 'Chai 60 viên nang mềm',
        price: 95000, isPrescription: false, isSelling: true, isActive: true,
        featured: false,
        descriptionHtml: '<p><strong>Hoạt chất:</strong> Cholecalciferol 1000 IU.</p><p>Sản phẩm minh họa tình huống hết tồn có thể bán (do lô đang tạm khóa/hết hạn).</p>'
      },
      {
        id: 17, categoryId: 5, supplierId: 4, barCode: '9992030000017',
        name: 'Nước Muối Sinh Lý Natri Clorid 0.9%', slug: 'natri-clorid-0-9',
        activeIngredient: 'Natri Clorid 0.9%',
        unit: 'Chai', packagingSpec: 'Chai 500ml dung dịch vệ sinh',
        price: 12000, isPrescription: false, isSelling: true, isActive: true,
        featured: false,
        descriptionHtml: '<p>Dung dịch Natri Clorid 0.9% dùng súc miệng và rửa ngoài. Đơn vị bán: Chai 500ml.</p>'
      },
      {
        id: 18, categoryId: 6, supplierId: 4, barCode: '9992030000018',
        name: 'Gạc Y Tế Tiệt Trùng 10x10cm', slug: 'gac-y-te-tiet-trung',
        activeIngredient: '100% Cotton y tế tiệt trùng EO Gas',
        unit: 'Gói', packagingSpec: 'Gói 10 miếng gạc 8 lớp',
        price: 18000, isPrescription: false, isSelling: true, isActive: true,
        featured: false,
        descriptionHtml: '<p>Gạc y tế tiệt trùng dùng chăm sóc vết thương ngoài da. Đơn vị bán: Gói 10 miếng.</p>'
      },
      {
        id: 19, categoryId: 6, supplierId: 2, barCode: '9992030000019',
        name: 'Nhiệt Kế Điện Tử Đầu Mềm', slug: 'nhiet-ke-dien-tu',
        activeIngredient: 'Cảm biến nhiệt điện tử sai số ±0.1°C',
        unit: 'Hộp', packagingSpec: 'Hộp 1 máy kèm pin và vỏ bảo vệ',
        price: 145000, isPrescription: false, isSelling: false, isActive: true, // Minh họa thuốc/SP đang Ngừng bán (IsSelling = false)
        featured: false,
        descriptionHtml: '<p><strong>Trạng thái mẫu:</strong> Sản phẩm đang ở trạng thái <em>Ngừng bán (IsSelling = false)</em> để minh họa quy tắc tồn thực tế còn nhưng tồn có thể bán bằng 0.</p>'
      },
      {
        id: 20, categoryId: 5, supplierId: 5, barCode: '9992030000020',
        name: 'Siro Thảo Dược Lá Thường Xuân', slug: 'siro-la-thuong-xuan',
        activeIngredient: 'Cao khô lá thường xuân 0.7g / 100ml',
        unit: 'Chai', packagingSpec: 'Chai thủy tinh 100ml kèm cốc đong',
        price: 78000, isPrescription: false, isSelling: true, isActive: false, // Minh họa sản phẩm Đã ẩn (IsActive = false)
        featured: false,
        descriptionHtml: '<p>Sản phẩm mẫu ở trạng thái Đã ẩn danh mục (IsActive = false) — chỉ xuất hiện trong bảng Quản trị để Admin hiện lại, không xuất hiện trên Storefront.</p>'
      }
    ];

    // Lô hàng (Batches) — Minh họa đủ: Được xuất bán, Sắp hết hạn (< 90 ngày so với 2026-11-03), Đã hết hạn (< 2026-11-03), và Tạm khóa (IsBlocked = true)
    const batches = [
      { id: 1, medicineId: 1, batchNumber: 'LOT-PND-2024A', expiryDate: '2026-12-15', stock: 25, isBlocked: false }, // Sắp hết hạn (~42 ngày)
      { id: 2, medicineId: 1, batchNumber: 'LOT-PND-2025B', expiryDate: '2027-10-15', stock: 60, isBlocked: false },
      { id: 3, medicineId: 2, batchNumber: 'LOT-HPC-2025A', expiryDate: '2027-08-20', stock: 80, isBlocked: false },
      { id: 4, medicineId: 3, batchNumber: 'LOT-AUG-2024X', expiryDate: '2027-05-20', stock: 30, isBlocked: false },
      { id: 5, medicineId: 4, batchNumber: 'LOT-BTC-2025M', expiryDate: '2028-01-10', stock: 50, isBlocked: false },
      { id: 6, medicineId: 5, batchNumber: 'PH26-LOR-01', expiryDate: '2027-06-30', stock: 22, isBlocked: false },
      { id: 7, medicineId: 5, batchNumber: 'PH26-LOR-02', expiryDate: '2026-09-15', stock: 12, isBlocked: false }, // Đã hết hạn -> loại khỏi AvailableStock
      { id: 8, medicineId: 6, batchNumber: 'PH26-CET-01', expiryDate: '2027-04-10', stock: 3, isBlocked: false },  // Tồn thấp (3 hộp)
      { id: 9, medicineId: 6, batchNumber: 'PH26-CET-02', expiryDate: '2027-09-01', stock: 15, isBlocked: true },  // Đang tạm khóa -> loại khỏi AvailableStock
      { id: 10, medicineId: 7, batchNumber: 'PH26-OME-01', expiryDate: '2027-11-20', stock: 45, isBlocked: false },
      { id: 11, medicineId: 8, batchNumber: 'PH26-LAN-01', expiryDate: '2027-07-15', stock: 18, isBlocked: false },
      { id: 12, medicineId: 9, batchNumber: 'PH26-AMO-01', expiryDate: '2027-08-12', stock: 35, isBlocked: false },
      { id: 13, medicineId: 10, batchNumber: 'PH26-AZI-01', expiryDate: '2026-12-28', stock: 14, isBlocked: false }, // Sắp hết hạn
      { id: 14, medicineId: 11, batchNumber: 'PH26-BER-01', expiryDate: '2028-03-01', stock: 65, isBlocked: false },
      { id: 15, medicineId: 12, batchNumber: 'PH26-ORE-01', expiryDate: '2028-05-10', stock: 90, isBlocked: false },
      { id: 16, medicineId: 13, batchNumber: 'PH26-SME-01', expiryDate: '2027-12-01', stock: 28, isBlocked: false },
      { id: 17, medicineId: 14, batchNumber: 'PH26-EFF-01', expiryDate: '2027-09-18', stock: 40, isBlocked: false },
      { id: 18, medicineId: 15, batchNumber: 'PH26-ZIN-01', expiryDate: '2027-10-05', stock: 24, isBlocked: false },
      { id: 19, medicineId: 16, batchNumber: 'PH26-VD3-LOCK', expiryDate: '2027-06-01', stock: 20, isBlocked: true }, // Bị khóa -> Med 16 có Total=28, Available=0
      { id: 20, medicineId: 16, batchNumber: 'PH26-VD3-EXP', expiryDate: '2026-08-01', stock: 8, isBlocked: false },  // Đã hết hạn -> Med 16 Available=0
      { id: 21, medicineId: 17, batchNumber: 'PH26-NACL-01', expiryDate: '2028-06-30', stock: 120, isBlocked: false },
      { id: 22, medicineId: 18, batchNumber: 'PH26-GAC-01', expiryDate: '2028-12-31', stock: 75, isBlocked: false },
      { id: 23, medicineId: 19, batchNumber: 'PH26-THERM-01', expiryDate: '2029-01-01', stock: 10, isBlocked: false }, // Med 19 IsSelling=false -> Available=0
      { id: 24, medicineId: 20, batchNumber: 'PH26-SIRO-01', expiryDate: '2027-05-01', stock: 16, isBlocked: false }  // Med 20 IsActive=false -> Available=0
    ];

    // 8 Khách hàng mẫu (Tài khoản web tách biệt với Employees)
    const customers = [
      {
        id: 1, fullName: 'Trần Văn Tuấn', phone: '0912345678', email: 'tuan.tran.demo@example.com',
        points: 15, webAccountActive: true, isLocked: false, createdAt: '2026-09-10',
        addresses: [
          { id: 101, label: 'Nhà riêng', recipientName: 'Trần Văn Tuấn', phone: '0912345678', address: 'Số 42 Đường 3/2, P. Xuân Khánh, Q. Ninh Kiều, Cần Thơ', isDefault: true },
          { id: 102, label: 'Văn phòng', recipientName: 'Trần Văn Tuấn', phone: '0912345678', address: 'Tầng 4 Tòa nhà Công nghệ, Q. Cái Răng, Cần Thơ', isDefault: false }
        ]
      },
      {
        id: 2, fullName: 'Lê Thị Mai', phone: '0987654321', email: 'mai.le.demo@example.com',
        points: 5, webAccountActive: true, isLocked: false, createdAt: '2026-09-15',
        addresses: [
          { id: 201, label: 'Nhà riêng', recipientName: 'Lê Thị Mai', phone: '0987654321', address: '118 Nguyễn Thị Minh Khai, Quận 3, TP. Hồ Chí Minh', isDefault: true }
        ]
      },
      {
        id: 3, fullName: 'Nguyễn Hoàng Nam', phone: '0907010001', email: 'nam.nguyen.demo@example.com',
        points: 22, webAccountActive: true, isLocked: false, createdAt: '2026-10-01',
        addresses: [
          { id: 301, label: 'Nhà riêng', recipientName: 'Nguyễn Hoàng Nam', phone: '0907010001', address: '25 Lý Thường Kiệt, Q. Hoàn Kiếm, Hà Nội', isDefault: true }
        ]
      },
      {
        id: 4, fullName: 'Phạm Thu Hà', phone: '0907010002', email: 'ha.pham.demo@example.com',
        points: 8, webAccountActive: true, isLocked: false, createdAt: '2026-10-04',
        addresses: [
          { id: 401, label: 'Nhà riêng', recipientName: 'Phạm Thu Hà', phone: '0907010002', address: '89 Lê Lợi, TP. Huế', isDefault: true }
        ]
      },
      {
        id: 5, fullName: 'Võ Minh Khôi', phone: '0907010003', email: 'khoi.vo.demo@example.com',
        points: 0, webAccountActive: true, isLocked: false, createdAt: '2026-10-12',
        addresses: [
          { id: 501, label: 'Nhà riêng', recipientName: 'Võ Minh Khôi', phone: '0907010003', address: '14 Nguyễn Văn Linh, Q. Hải Châu, Đà Nẵng', isDefault: true }
        ]
      },
      {
        id: 6, fullName: 'Đặng Ngọc Ánh', phone: '0907010004', email: 'anh.dang.demo@example.com',
        points: 12, webAccountActive: true, isLocked: false, createdAt: '2026-10-18',
        addresses: [
          { id: 601, label: 'Nhà riêng', recipientName: 'Đặng Ngọc Ánh', phone: '0907010004', address: '56 Đại lộ Hòa Bình, Q. Ninh Kiều, Cần Thơ', isDefault: true }
        ]
      },
      {
        id: 7, fullName: 'Bùi Quốc Bảo', phone: '0907010005', email: 'bao.bui.demo@example.com',
        points: 3, webAccountActive: true, isLocked: true, createdAt: '2026-10-20', // Tài khoản bị khóa để minh họa
        addresses: [
          { id: 701, label: 'Nhà riêng', recipientName: 'Bùi Quốc Bảo', phone: '0907010005', address: '102 Trần Phú, TP. Nha Trang', isDefault: true }
        ]
      },
      {
        id: 8, fullName: 'Hoàng Thanh Trúc', phone: '0907010006', email: 'truc.hoang.demo@example.com',
        points: 19, webAccountActive: false, isLocked: false, createdAt: '2026-10-25', // Khách mua tại POS chưa kích hoạt web
        addresses: []
      }
    ];

    // 6 Voucher mẫu (kế thừa từ InitDatabase.sql và HUONG-DAN-PHARMACY.md)
    const vouchers = [
      { id: 1, code: 'GIAM10K', discountType: 'FIXED', discountValue: 10000, minOrderValue: 50000, maxDiscountCap: null, quantity: 50, isActive: true, description: 'Giảm trực tiếp 10.000 ₫ cho đơn từ 50.000 ₫' },
      { id: 2, code: 'PH5K', discountType: 'FIXED', discountValue: 5000, minOrderValue: 0, maxDiscountCap: null, quantity: 35, isActive: true, description: 'Giảm 5.000 ₫ cho mọi đơn hàng' },
      { id: 3, code: 'PH25K', discountType: 'FIXED', discountValue: 25000, minOrderValue: 0, maxDiscountCap: null, quantity: 15, isActive: true, description: 'Giảm 25.000 ₫ (tự động giới hạn không vượt quá tiền hàng)' },
      { id: 4, code: 'PH10', discountType: 'PERCENT', discountValue: 10, minOrderValue: 80000, maxDiscountCap: 30000, quantity: 20, isActive: true, description: 'Giảm 10% (tối đa 30.000 ₫) cho đơn từ 80.000 ₫' },
      { id: 5, code: 'PHHET', discountType: 'FIXED', discountValue: 15000, minOrderValue: 50000, maxDiscountCap: null, quantity: 0, isActive: true, description: 'Voucher mẫu đã hết lượt sử dụng (Quantity = 0)' },
      { id: 6, code: 'PHKHOA', discountType: 'PERCENT', discountValue: 20, minOrderValue: 100000, maxDiscountCap: 50000, quantity: 10, isActive: false, description: 'Voucher mẫu đang tạm khóa (IsActive = false)' }
    ];

    // 12 Đơn hàng nhiều trạng thái (Tách biệt OrderStatus, PaymentStatus, ShippingStatus)
    const orders = [
      {
        id: 923, code: 'HD00923', customerId: 1, customerName: 'Trần Văn Tuấn', customerPhone: '0912345678',
        shippingAddress: 'Số 42 Đường 3/2, P. Xuân Khánh, Q. Ninh Kiều, Cần Thơ',
        channel: 'WEB', orderDate: '2026-10-18T09:15:00',
        orderStatus: 'COMPLETED', paymentMethod: 'COD', paymentStatus: 'PAID', shippingStatus: 'DELIVERED',
        voucherCode: 'GIAM10K', subtotalAmount: 135000, discountAmount: 10000, shippingFee: 0, totalAmount: 125000,
        orderVersion: 3, note: 'Giao giờ hành chính',
        items: [
          { medicineId: 1, medicineName: 'Panadol Extra', unit: 'Hộp', quantity: 2, unitPrice: 45000 },
          { medicineId: 11, medicineName: 'Berberin 100mg Traphaco', unit: 'Chai', quantity: 1, unitPrice: 45000 }
        ],
        history: [
          { time: '2026-10-18T09:15:00', actor: 'Khách hàng', text: 'Tạo đơn hàng trên website (COD)' },
          { time: '2026-10-18T10:00:00', actor: 'Admin', text: 'Xác nhận đơn & xuất kho theo lô FEFO (LOT-PND-2024A)' },
          { time: '2026-10-19T14:30:00', actor: 'Admin', text: 'Giao hàng thành công & Xác nhận đã thu tiền (PAID)' }
        ]
      },
      {
        id: 924, code: 'HD00924', customerId: 2, customerName: 'Lê Thị Mai', customerPhone: '0987654321',
        shippingAddress: '118 Nguyễn Thị Minh Khai, Quận 3, TP. Hồ Chí Minh',
        channel: 'WEB', orderDate: '2026-10-21T15:20:00',
        orderStatus: 'COMPLETED', paymentMethod: 'BANK_TRANSFER', paymentStatus: 'PAID', shippingStatus: 'DELIVERED',
        voucherCode: 'PH10', subtotalAmount: 192000, discountAmount: 19200, shippingFee: 0, totalAmount: 172800,
        orderVersion: 4, note: '',
        items: [
          { medicineId: 13, medicineName: 'Smecta Hỗn Dịch Uống', unit: 'Hộp', quantity: 1, unitPrice: 118000 },
          { medicineId: 2, medicineName: 'Hapacol 250 Sủi Bọt', unit: 'Hộp', quantity: 1, unitPrice: 38000 },
          { medicineId: 17, medicineName: 'Nước Muối Sinh Lý Natri Clorid 0.9%', unit: 'Chai', quantity: 3, unitPrice: 12000 }
        ],
        history: [
          { time: '2026-10-21T15:20:00', actor: 'Khách hàng', text: 'Đặt hàng chọn Chuyển khoản ngân hàng' },
          { time: '2026-10-21T15:45:00', actor: 'Admin', text: 'Xác nhận đã nhận chuyển khoản (PAID)' },
          { time: '2026-10-22T11:10:00', actor: 'Admin', text: 'Hoàn tất giao hàng' }
        ]
      },
      {
        id: 925, code: 'HD00925', customerId: 3, customerName: 'Nguyễn Hoàng Nam', customerPhone: '0907010001',
        shippingAddress: '25 Lý Thường Kiệt, Q. Hoàn Kiếm, Hà Nội',
        channel: 'WEB', orderDate: '2026-10-24T11:05:00',
        orderStatus: 'COMPLETED', paymentMethod: 'COD', paymentStatus: 'PAID', shippingStatus: 'DELIVERED',
        voucherCode: null, subtotalAmount: 105000, discountAmount: 0, shippingFee: 15000, totalAmount: 120000,
        orderVersion: 2, note: '',
        items: [
          { medicineId: 4, medicineName: 'Boston C 1000mg', unit: 'Tuýp', quantity: 3, unitPrice: 35000 }
        ],
        history: [
          { time: '2026-10-24T11:05:00', actor: 'Khách hàng', text: 'Đặt hàng trên website' },
          { time: '2026-10-25T16:00:00', actor: 'Admin', text: 'Đã giao hàng & thu tiền COD' }
        ]
      },
      {
        id: 926, code: 'HD00926', customerId: 4, customerName: 'Phạm Thu Hà', customerPhone: '0907010002',
        shippingAddress: '89 Lê Lợi, TP. Huế',
        channel: 'POS', orderDate: '2026-10-26T08:40:00',
        orderStatus: 'COMPLETED', paymentMethod: 'CASH', paymentStatus: 'PAID', shippingStatus: 'DELIVERED',
        voucherCode: 'PH5K', subtotalAmount: 90000, discountAmount: 5000, shippingFee: 0, totalAmount: 85000,
        orderVersion: 1, note: 'Mua trực tiếp tại quầy POS',
        items: [
          { medicineId: 1, medicineName: 'Panadol Extra', unit: 'Hộp', quantity: 2, unitPrice: 45000 }
        ],
        history: [
          { time: '2026-10-26T08:40:00', actor: 'Dược sĩ Mai (POS)', text: 'Thanh toán trực tiếp tại quầy' }
        ]
      },
      {
        id: 927, code: 'HD00927', customerId: 6, customerName: 'Đặng Ngọc Ánh', customerPhone: '0907010004',
        shippingAddress: '56 Đại lộ Hòa Bình, Q. Ninh Kiều, Cần Thơ',
        channel: 'WEB', orderDate: '2026-10-28T17:15:00',
        orderStatus: 'COMPLETED', paymentMethod: 'BANK_TRANSFER', paymentStatus: 'PAID', shippingStatus: 'DELIVERED',
        voucherCode: 'GIAM10K', subtotalAmount: 162000, discountAmount: 10000, shippingFee: 0, totalAmount: 152000,
        orderVersion: 3, note: '',
        items: [
          { medicineId: 14, medicineName: 'Efferalgan 500mg Viên Sủi', unit: 'Tuýp', quantity: 3, unitPrice: 54000 }
        ],
        history: [
          { time: '2026-10-28T17:15:00', actor: 'Khách hàng', text: 'Tạo đơn hàng chuyển khoản' },
          { time: '2026-10-28T18:00:00', actor: 'Admin', text: 'Đã đối soát thanh toán & giao hàng' }
        ]
      },
      {
        id: 928, code: 'HD00928', customerId: 1, customerName: 'Trần Văn Tuấn', customerPhone: '0912345678',
        shippingAddress: 'Số 42 Đường 3/2, P. Xuân Khánh, Q. Ninh Kiều, Cần Thơ',
        channel: 'WEB', orderDate: '2026-10-30T14:10:00',
        orderStatus: 'COMPLETED', paymentMethod: 'COD', paymentStatus: 'PAID', shippingStatus: 'DELIVERED',
        voucherCode: null, subtotalAmount: 149000, discountAmount: 0, shippingFee: 0, totalAmount: 149000,
        orderVersion: 2, note: '',
        items: [
          { medicineId: 7, medicineName: 'Omeprazole 20mg DHG', unit: 'Hộp', quantity: 2, unitPrice: 42000 },
          { medicineId: 15, medicineName: 'Kẽm Hữu Cơ Zinc Gluconate 70mg', unit: 'Hộp', quantity: 1, unitPrice: 65000 }
        ],
        history: [
          { time: '2026-10-30T14:10:00', actor: 'Khách hàng', text: 'Tạo đơn hàng trên website' },
          { time: '2026-10-31T10:20:00', actor: 'Admin', text: 'Đã giao hàng và thu tiền COD' }
        ]
      },
      {
        id: 929, code: 'HD00929', customerId: 1, customerName: 'Trần Văn Tuấn', customerPhone: '0912345678',
        shippingAddress: 'Số 42 Đường 3/2, P. Xuân Khánh, Q. Ninh Kiều, Cần Thơ',
        channel: 'WEB', orderDate: '2026-11-02T16:30:00',
        orderStatus: 'PROCESSING', paymentMethod: 'COD', paymentStatus: 'UNPAID', shippingStatus: 'SHIPPING',
        voucherCode: 'PH5K', subtotalAmount: 109000, discountAmount: 5000, shippingFee: 15000, totalAmount: 119000,
        orderVersion: 2, note: 'Gọi trước khi giao 15 phút',
        items: [
          { medicineId: 1, medicineName: 'Panadol Extra', unit: 'Hộp', quantity: 1, unitPrice: 45000 },
          { medicineId: 12, medicineName: 'Oresol 245 Bù Nước Điện Giải', unit: 'Hộp', quantity: 2, unitPrice: 32000 }
        ],
        history: [
          { time: '2026-11-02T16:30:00', actor: 'Khách hàng', text: 'Đặt hàng COD (Chưa thanh toán)' },
          { time: '2026-11-02T17:15:00', actor: 'Admin', text: 'Đã đóng gói và bàn giao đơn vị vận chuyển' }
        ]
      },
      {
        id: 930, code: 'HD00930', customerId: 2, customerName: 'Lê Thị Mai', customerPhone: '0987654321',
        shippingAddress: '118 Nguyễn Thị Minh Khai, Quận 3, TP. Hồ Chí Minh',
        channel: 'WEB', orderDate: '2026-11-03T08:20:00',
        orderStatus: 'CONFIRMED', paymentMethod: 'BANK_TRANSFER', paymentStatus: 'PENDING_TRANSFER', shippingStatus: 'PACKING',
        voucherCode: 'GIAM10K', subtotalAmount: 154000, discountAmount: 10000, shippingFee: 0, totalAmount: 144000,
        orderVersion: 1, note: 'Chờ xác nhận biên lai chuyển khoản',
        items: [
          { medicineId: 13, medicineName: 'Smecta Hỗn Dịch Uống', unit: 'Hộp', quantity: 1, unitPrice: 118000 },
          { medicineId: 18, medicineName: 'Gạc Y Tế Tiệt Trùng 10x10cm', unit: 'Gói', quantity: 2, unitPrice: 18000 }
        ],
        history: [
          { time: '2026-11-03T08:20:00', actor: 'Khách hàng', text: 'Đặt hàng chuyển khoản (Trạng thái: Chờ xác nhận chuyển khoản)' }
        ]
      },
      {
        id: 931, code: 'HD00931', customerId: 1, customerName: 'Trần Văn Tuấn', customerPhone: '0912345678',
        shippingAddress: 'Tầng 4 Tòa nhà Công nghệ, Q. Cái Răng, Cần Thơ',
        channel: 'WEB', orderDate: '2026-11-03T10:45:00',
        orderStatus: 'NEW', paymentMethod: 'COD', paymentStatus: 'UNPAID', shippingStatus: 'NOT_SHIPPED',
        voucherCode: null, subtotalAmount: 96000, discountAmount: 0, shippingFee: 15000, totalAmount: 111000,
        orderVersion: 1, note: 'Đơn mới đặt — khách có thể thử bấm Yêu cầu hủy đơn',
        items: [
          { medicineId: 11, medicineName: 'Berberin 100mg Traphaco', unit: 'Chai', quantity: 2, unitPrice: 48000 }
        ],
        history: [
          { time: '2026-11-03T10:45:00', actor: 'Khách hàng', text: 'Tạo đơn hàng mới (Chưa thanh toán, Chưa giao)' }
        ]
      },
      {
        id: 932, code: 'HD00932', customerId: 5, customerName: 'Võ Minh Khôi', customerPhone: '0907010003',
        shippingAddress: '14 Nguyễn Văn Linh, Q. Hải Châu, Đà Nẵng',
        channel: 'WEB', orderDate: '2026-11-03T11:30:00',
        orderStatus: 'NEW', paymentMethod: 'BANK_TRANSFER', paymentStatus: 'PENDING_TRANSFER', shippingStatus: 'NOT_SHIPPED',
        voucherCode: 'PH10', subtotalAmount: 130000, discountAmount: 13000, shippingFee: 0, totalAmount: 117000,
        orderVersion: 1, note: '',
        items: [
          { medicineId: 15, medicineName: 'Kẽm Hữu Cơ Zinc Gluconate 70mg', unit: 'Hộp', quantity: 2, unitPrice: 65000 }
        ],
        history: [
          { time: '2026-11-03T11:30:00', actor: 'Khách hàng', text: 'Đặt hàng mới qua chuyển khoản' }
        ]
      },
      {
        id: 933, code: 'HD00933', customerId: 3, customerName: 'Nguyễn Hoàng Nam', customerPhone: '0907010001',
        shippingAddress: '25 Lý Thường Kiệt, Q. Hoàn Kiếm, Hà Nội',
        channel: 'WEB', orderDate: '2026-11-03T14:00:00',
        orderStatus: 'NEW', paymentMethod: 'COD', paymentStatus: 'UNPAID', shippingStatus: 'NOT_SHIPPED',
        voucherCode: null, subtotalAmount: 84000, discountAmount: 0, shippingFee: 15000, totalAmount: 99000,
        orderVersion: 1, note: '',
        items: [
          { medicineId: 5, medicineName: 'Loratadine 10mg Boston', unit: 'Hộp', quantity: 3, unitPrice: 28000 }
        ],
        history: [
          { time: '2026-11-03T14:00:00', actor: 'Khách hàng', text: 'Đặt hàng COD mới' }
        ]
      },
      {
        id: 934, code: 'HD00934', customerId: 1, customerName: 'Trần Văn Tuấn', customerPhone: '0912345678',
        shippingAddress: 'Số 42 Đường 3/2, P. Xuân Khánh, Q. Ninh Kiều, Cần Thơ',
        channel: 'WEB', orderDate: '2026-10-29T19:00:00',
        orderStatus: 'CANCELLED', paymentMethod: 'COD', paymentStatus: 'UNPAID', shippingStatus: 'NOT_SHIPPED',
        voucherCode: null, subtotalAmount: 70000, discountAmount: 0, shippingFee: 15000, totalAmount: 85000,
        orderVersion: 2, cancelReason: 'Khách đặt nhầm số lượng và chủ động yêu cầu hủy trước khi đóng gói', note: '',
        items: [
          { medicineId: 4, medicineName: 'Boston C 1000mg', unit: 'Tuýp', quantity: 2, unitPrice: 35000 }
        ],
        history: [
          { time: '2026-10-29T19:00:00', actor: 'Khách hàng', text: 'Tạo đơn hàng trên website' },
          { time: '2026-10-29T19:25:00', actor: 'Khách hàng', text: 'Đã hủy đơn: Khách đặt nhầm số lượng' }
        ]
      }
    ];

    // Phiếu nhập kho đã lưu (StockIns)
    const stockIns = [
      {
        id: 501, code: 'PN2026-018', supplierId: 1, supplierName: 'Dược Hậu Giang (DHG Pharma)',
        employeeName: 'Quản lý hệ thống (admin)', importDate: '2026-10-12T09:00:00',
        totalCost: 3520000, note: 'Nhập định kỳ tháng 10/2026 — PHARMACY20261030',
        lines: [
          { medicineId: 1, medicineName: 'Panadol Extra', unit: 'Hộp', batchNumber: 'LOT-PND-2025B', expiryDate: '2027-10-15', quantity: 60, unitCost: 34000 },
          { medicineId: 2, medicineName: 'Hapacol 250 Sủi Bọt', unit: 'Hộp', batchNumber: 'LOT-HPC-2025A', expiryDate: '2027-08-20', quantity: 50, unitCost: 29600 }
        ]
      },
      {
        id: 502, code: 'PN2026-019', supplierId: 2, supplierName: 'Sanofi Việt Nam',
        employeeName: 'Quản lý hệ thống (admin)', importDate: '2026-10-19T14:20:00',
        totalCost: 8220000, note: 'Nhập kháng sinh & tiêu hóa — PHARMACY20261030',
        lines: [
          { medicineId: 3, medicineName: 'Augmentin 1g', unit: 'Hộp', batchNumber: 'LOT-AUG-2024X', expiryDate: '2027-05-20', quantity: 30, unitCost: 195000 },
          { medicineId: 13, medicineName: 'Smecta Hỗn Dịch Uống', unit: 'Hộp', batchNumber: 'PH26-SME-01', expiryDate: '2027-12-01', quantity: 25, unitCost: 94800 }
        ]
      },
      {
        id: 503, code: 'PN2026-020', supplierId: 3, supplierName: 'Dược phẩm Boston Việt Nam',
        employeeName: 'Quản lý hệ thống (admin)', importDate: '2026-10-27T10:15:00',
        totalCost: 1790000, note: 'Bổ sung Vitamin C & Loratadine — PHARMACY20261030',
        lines: [
          { medicineId: 4, medicineName: 'Boston C 1000mg', unit: 'Tuýp', batchNumber: 'LOT-BTC-2025M', expiryDate: '2028-01-10', quantity: 50, unitCost: 26000 },
          { medicineId: 5, medicineName: 'Loratadine 10mg Boston', unit: 'Hộp', batchNumber: 'PH26-LOR-01', expiryDate: '2027-06-30', quantity: 25, unitCost: 19600 }
        ]
      }
    ];

    // Yêu cầu tư vấn đơn thuốc (PrescriptionRequests)
    const prescriptionRequests = [
      {
        id: 301, code: 'RX-2026-001', customerId: 1, patientName: 'Trần Văn Tuấn', phone: '0912345678',
        createdAt: '2026-11-01T09:30:00',
        status: 'RESPONDED', // PENDING_REVIEW | NEED_MORE_INFO | RESPONDED
        requestedMedicines: [{ medicineId: 3, name: 'Augmentin 1g', unit: 'Hộp' }],
        customerNote: 'Tôi có đơn khám viêm họng tại bệnh viện, nhờ nhà thuốc kiểm tra giúp đơn thuốc đính kèm.',
        pharmacistNote: 'Nhà thuốc đã đối chiếu ảnh đơn thuốc mẫu RX-2026-001. Sản phẩm Augmentin 1g hiện còn hàng tại quầy. Quý khách vui lòng mang bản gốc đơn thuốc tới nhà thuốc hoặc xác nhận qua SĐT để dược sĩ hướng dẫn nhận thuốc theo đúng quy định.',
        updatedAt: '2026-11-01T10:15:00',
        images: [
          {
            name: 'don-thuoc-augmentin-mau.svg',
            sizeKb: 142,
            dataUrl: window.PharmacyI18n.generatePrescriptionSampleSvg('RX-2026-001', 'Trần Văn Tuấn', 'BS. Nguyễn Văn Minh (Mẫu)', '01/11/2026', 'Augmentin 1g (Amoxicillin + Clavulanic acid) — 01 Hộp')
          }
        ]
      },
      {
        id: 302, code: 'RX-2026-002', customerId: 1, patientName: 'Trần Văn Tuấn', phone: '0912345678',
        createdAt: '2026-11-02T15:10:00',
        status: 'NEED_MORE_INFO',
        requestedMedicines: [{ medicineId: 8, name: 'Lansoprazole 30mg', unit: 'Hộp' }],
        customerNote: 'Nhờ dược sĩ xem giúp đơn thuốc dạ dày này.',
        pharmacistNote: 'Ảnh chụp phần ngày kê đơn và chữ ký người kê đơn bị khuất góc dưới. Quý khách vui lòng chụp bổ sung toàn bộ mặt tờ đơn thuốc rõ nét để dược sĩ kiểm tra.',
        updatedAt: '2026-11-02T16:00:00',
        images: [
          {
            name: 'don-da-day-chup-thieu.svg',
            sizeKb: 98,
            dataUrl: window.PharmacyI18n.generatePrescriptionSampleSvg('RX-2026-002 (Thiếu góc)', 'Trần Văn Tuấn', 'Chưa rõ chữ ký (Mẫu)', '02/11/2026', 'Lansoprazole 30mg — 01 Hộp')
          }
        ]
      },
      {
        id: 303, code: 'RX-2026-003', customerId: 2, patientName: 'Lê Thị Mai', phone: '0987654321',
        createdAt: '2026-11-03T09:05:00',
        status: 'PENDING_REVIEW',
        requestedMedicines: [
          { medicineId: 9, name: 'Amoxicillin 500mg Imexpharm', unit: 'Hộp' },
          { medicineId: 10, name: 'Azithromycin 500mg', unit: 'Hộp' }
        ],
        customerNote: 'Tôi gửi đơn khám ngoại trú sáng nay, cần tư vấn tình trạng còn hàng.',
        pharmacistNote: '',
        updatedAt: '2026-11-03T09:05:00',
        images: [
          {
            name: 'don-khang-sinh-0311.svg',
            sizeKb: 165,
            dataUrl: window.PharmacyI18n.generatePrescriptionSampleSvg('RX-2026-003', 'Lê Thị Mai', 'BS. Lê Hoàng Quân (Mẫu)', '03/11/2026', 'Amoxicillin 500mg — 01 Hộp')
          }
        ]
      }
    ];

    return {
      categories,
      suppliers,
      medicines,
      batches,
      customers,
      vouchers,
      orders,
      stockIns,
      prescriptionRequests,
      stockInDraft: [
        { medicineId: 6, batchNumber: 'PH26-CET-NEW', expiryDate: '2028-04-30', quantity: 30, unitCost: 38000 },
        { medicineId: 1, batchNumber: 'LOT-PND-2025B', expiryDate: '2027-10-15', quantity: 20, unitCost: 34000 }
      ]
    };
  }

  function getInitialSession() {
    return {
      accessRole: 'CUSTOMER', // 'GUEST' | 'CUSTOMER' | 'ADMIN'
      customerId: 1,          // Trần Văn Tuấn
      adminUser: { username: 'admin', fullName: 'Quản lý hệ thống' },
      cart: [
        { medicineId: 1, quantity: 2, addedPrice: 45000 },
        { medicineId: 4, quantity: 1, addedPrice: 35000 }
      ],
      appliedVoucherCode: 'GIAM10K',
      cookieConsent: 'ACCEPTED', // 'PENDING' | 'ACCEPTED' | 'ESSENTIAL_ONLY'
      simulateLatencyMs: 220,
      simulateApiError: false,
      cartSimulationMode: 'NORMAL' // 'NORMAL' | 'STOCK_SHORTAGE' | 'PRICE_CHANGED'
    };
  }

  let dbState = null;
  let sessionState = null;

  function load() {
    try {
      const rawDb = localStorage.getItem(DB_KEY);
      dbState = rawDb ? JSON.parse(rawDb) : getInitialData();
    } catch (e) {
      dbState = getInitialData();
    }

    try {
      const rawSession = localStorage.getItem(SESSION_KEY);
      sessionState = rawSession ? Object.assign(getInitialSession(), JSON.parse(rawSession)) : getInitialSession();
    } catch (e) {
      sessionState = getInitialSession();
    }
  }

  function saveDb() {
    try {
      localStorage.setItem(DB_KEY, JSON.stringify(dbState));
    } catch (e) {
      // Ignore quota errors in private mode
    }
  }

  function saveSession() {
    try {
      localStorage.setItem(SESSION_KEY, JSON.stringify(sessionState));
    } catch (e) {
      // Ignore quota errors
    }
  }

  function resetAll() {
    dbState = getInitialData();
    sessionState = getInitialSession();
    saveDb();
    saveSession();
    return { db: dbState, session: sessionState };
  }

  // Tính toán trạng thái lô hàng theo đúng SELLING-STATUS.md
  function computeBatchStatus(batch, medicine) {
    const isExpired = batch.expiryDate < TODAY_STR;
    const daysToExpiry = Math.ceil((new Date(batch.expiryDate) - new Date(TODAY_STR)) / (1000 * 60 * 60 * 24));
    const isNearExpiry = !isExpired && daysToExpiry <= 90;

    if (!medicine || !medicine.isActive) {
      return { code: 'MED_HIDDEN', label: 'Thuốc đã ẩn', badgeType: 'neutral', canSell: false, isExpired, isNearExpiry, daysToExpiry };
    }
    if (!medicine.isSelling) {
      return { code: 'MED_STOPPED', label: 'Thuốc ngừng bán', badgeType: 'danger', canSell: false, isExpired, isNearExpiry, daysToExpiry };
    }
    if (batch.isBlocked) {
      return { code: 'BLOCKED', label: 'Tạm khóa', badgeType: 'danger', canSell: false, isExpired, isNearExpiry, daysToExpiry };
    }
    if (isExpired) {
      return { code: 'EXPIRED', label: 'Hết hạn', badgeType: 'danger', canSell: false, isExpired, isNearExpiry, daysToExpiry };
    }
    if (batch.stock <= 0) {
      return { code: 'EMPTY', label: 'Hết tồn lô', badgeType: 'neutral', canSell: false, isExpired, isNearExpiry, daysToExpiry };
    }
    if (isNearExpiry) {
      return { code: 'SELLABLE_NEAR_EXPIRY', label: `Được xuất bán (Còn ${daysToExpiry} ngày)`, badgeType: 'warning', canSell: true, isExpired, isNearExpiry, daysToExpiry };
    }
    return { code: 'SELLABLE', label: 'Được xuất bán', badgeType: 'success', canSell: true, isExpired, isNearExpiry, daysToExpiry };
  }

  // Làm giàu thông tin thuốc với TotalStock và AvailableStock tính động từ Batches
  function enrichMedicine(med) {
    if (!med) return null;
    const cat = dbState.categories.find(c => c.id === med.categoryId);
    const sup = dbState.suppliers.find(s => s.id === med.supplierId);
    const medBatches = dbState.batches.filter(b => b.medicineId === med.id);

    let totalStock = 0;
    let availableStock = 0;

    medBatches.forEach(b => {
      const st = Number(b.stock) || 0;
      totalStock += st;
      const status = computeBatchStatus(b, med);
      if (status.canSell) {
        availableStock += st;
      }
    });

    const primaryImage = (med.images && med.images.length > 0)
      ? (med.images.find(img => img.isPrimary) || med.images[0]).url
      : window.PharmacyI18n.generateMedicineSvgDataUrl(med);

    return Object.assign({}, med, {
      categoryName: cat ? cat.name : 'Chưa phân loại',
      supplierName: sup ? sup.name : 'Chưa xác định',
      totalStock,
      availableStock,
      primaryImage,
      images: (med.images && med.images.length > 0) ? med.images : [
        { id: 1, url: primaryImage, name: `${med.slug || 'med'}-front.svg`, isPrimary: true }
      ]
    });
  }

  load();

  return {
    TODAY_STR,
    getDb: () => dbState,
    getSession: () => sessionState,
    saveDb,
    saveSession,
    resetAll,
    computeBatchStatus,
    enrichMedicine
  };
})();
