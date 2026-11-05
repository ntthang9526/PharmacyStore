/* ==========================================================================
   i18n.js — Đa ngôn ngữ (VI / EN) Toàn diện, Định dạng Tiền tệ/Ngày & SVG System
   Không dùng thuật ngữ viết tắt khó hiểu (Rx, OTC, HC, BC)
   Hỗ trợ dịch tự động toàn bộ DOM (kể cả nội dung tải bất đồng bộ AJAX / Modal)
   ========================================================================== */

window.PharmacyI18n = (function () {
  const STORAGE_KEY = 'pharmacy_proto_lang';

  const dictionary = {
    vi: {
      brand_name: 'Pharmacy',
      brand_tagline: 'Hệ thống Bán hàng & Quản lý Dược phẩm',
      search_placeholder: 'Tìm tên thuốc, công dụng, thành phần (vd: Panadol, Oresol, Vitamin C)...',
      search_btn: 'Tìm kiếm',
      nav_home: 'Trang chủ',
      nav_catalog: 'Tất cả thuốc',
      nav_rx_upload: 'Gửi đơn thuốc',
      nav_my_orders: 'Đơn hàng',
      nav_account: 'Tài khoản',
      nav_policy: 'Hệ thống & Chính sách',
      nav_cart: 'Giỏ hàng',
      nav_login: 'Đăng nhập',
      nav_register: 'Đăng ký',
      nav_logout: 'Đăng xuất',
      nav_admin_portal: 'Quản trị hệ thống',
      nav_back_store: 'Về trang bán hàng',
      btn_add_cart: 'Chọn mua',
      btn_consult_rx: 'Gửi đơn tư vấn',
      btn_out_of_stock: 'Tạm hết hàng',
      btn_stopped_selling: 'Ngừng kinh doanh',
      btn_checkout: 'Tiến hành đặt hàng',
      btn_place_order: 'Xác nhận đặt hàng',
      badge_otc: 'Mua trực tiếp',
      badge_rx: 'Cần đơn bác sĩ',
      stock_available: 'Còn hàng',
      stock_low: 'Sắp hết',
      stock_out: 'Tạm hết hàng',
      stock_stopped: 'Ngừng bán',
      cart_title: 'Giỏ hàng của bạn',
      cart_empty: 'Giỏ hàng đang trống',
      checkout_title: 'Thông tin đặt hàng & Thanh toán',
      admin_dashboard: 'Tổng quan (Dashboard)',
      admin_products: 'Quản lý Sản phẩm',
      admin_cat_sup: 'Nhóm thuốc & Nhà cung cấp',
      admin_inventory: 'Kho / Lô hàng & Phiếu nhập',
      admin_orders: 'Quản lý Đơn hàng',
      admin_prescriptions: 'Yêu cầu Đơn thuốc',
      admin_customers: 'Khách hàng & Tài khoản Web',
      admin_vouchers: 'Quản lý Mã giảm giá',
      admin_reports: 'Báo cáo Doanh thu'
    },
    en: {
      brand_name: 'Pharmacy',
      brand_tagline: 'Online Pharmacy & Dispensary Management',
      search_placeholder: 'Search medicine name, uses, ingredients (e.g. Panadol, Oresol, Vitamin C)...',
      search_btn: 'Search',
      nav_home: 'Home',
      nav_catalog: 'All Medicines',
      nav_rx_upload: 'Submit Prescription',
      nav_my_orders: 'My Orders',
      nav_account: 'Account',
      nav_policy: 'Policies & Contact',
      nav_cart: 'Cart',
      nav_login: 'Sign In',
      nav_register: 'Register',
      nav_logout: 'Sign Out',
      nav_admin_portal: 'Admin Portal',
      nav_back_store: 'Back to Storefront',
      btn_add_cart: 'Buy Now',
      btn_consult_rx: 'Submit Prescription',
      btn_out_of_stock: 'Out of Stock',
      btn_stopped_selling: 'Discontinued',
      btn_checkout: 'Proceed to Checkout',
      btn_place_order: 'Place Order',
      badge_otc: 'Direct Purchase',
      badge_rx: 'Doctor Prescription',
      stock_available: 'In Stock',
      stock_low: 'Low Stock',
      stock_out: 'Out of Stock',
      stock_stopped: 'Not Selling',
      cart_title: 'Your Shopping Cart',
      cart_empty: 'Your cart is empty',
      checkout_title: 'Checkout & Delivery Information',
      admin_dashboard: 'Dashboard Overview',
      admin_products: 'Products Management',
      admin_cat_sup: 'Categories & Suppliers',
      admin_inventory: 'Batches & Stock-In',
      admin_orders: 'Orders Management',
      admin_prescriptions: 'Prescription Requests',
      admin_customers: 'Customers & Web Accounts',
      admin_vouchers: 'Vouchers Management',
      admin_reports: 'Revenue Reports'
    }
  };

  // Từ điển dịch chính xác các cụm từ giao diện Việt -> Anh (Khi chọn ngôn ngữ EN)
  const exactViToEn = {
    // Preview Toolbar
    'CHẾ ĐỘ PREVIEW (Người chấm):': 'PREVIEW MODE (Grader):',
    'Khách chưa đăng nhập': 'Guest (Not Signed In)',
    'ADMIN (Quản trị viên)': 'ADMIN (Administrator)',
    'Đang bật Lỗi API (503)': 'Simulating API Error (503)',
    'Mô phỏng lỗi API: Tắt': 'Simulate API Error: Off',
    'Reset dữ liệu mẫu': 'Reset Sample Data',

    // Header & Category Bar
    'Trung tâm Dược phẩm & Chăm sóc Sức khỏe Chính hãng (Prototype MVC)': 'Authentic Pharmacy & Healthcare Center (MVC Prototype)',
    'Tổng đài tư vấn:': 'Support Hotline:',
    '(Miễn phí mô phỏng)': '(Simulated Toll-Free)',
    'Quy định mua thuốc & Đơn bác sĩ': 'Purchase & Prescription Policy',
    'Tất cả thuốc': 'All Medicines',
    'Thuốc giảm đau - hạ sốt': 'Pain Relief & Fever',
    'Kháng sinh - Kháng viêm': 'Antibiotics & Anti-inflammatory',
    'Vitamin & Khoáng chất': 'Vitamins & Minerals',
    'Tiêu hóa - Dạ dày': 'Digestive & Stomach Care',
    'Dị ứng - Hô hấp': 'Allergy & Respiratory',
    'Vật tư & Thiết bị y tế': 'Medical Supplies & Devices',
    'Hệ thống & Chính sách': 'System & Policies',
    'Oresol bù nước': 'Oresol Rehydration',
    'Dầu cá Omega-3': 'Omega-3 Fish Oil',

    // Home Hero & Quick Tiles
    'Thuốc chính hãng • Rõ số lô & Hạn dùng': 'Authentic Medicines • Clear Batch & Expiry',
    'Phân tách Thuốc mua trực tiếp & Thuốc cần đơn bác sĩ': 'Clear Split: Direct Purchase vs. Doctor Prescription',
    'Hệ thống Tra cứu Dược phẩm & Đặt thuốc Trực tuyến Minh bạch': 'Transparent Online Medicine Lookup & Ordering System',
    'Chọn mua thuốc trực tiếp': 'Shop Direct Medicines',
    'Cần mua thuốc theo đơn': 'Buy with Prescription',
    'Mua thuốc theo đơn bác sĩ': 'Buy with Doctor Prescription',
    'Dược sĩ tư vấn': 'Pharmacist Review',
    'Quy trình 3 bước gửi đơn thuốc để dược sĩ tư vấn:': '3 simple steps to submit a doctor prescription for review:',
    'Gửi ảnh đơn thuốc ngay': 'Upload Prescription Photo',
    'Cần mua thuốc': 'Submit Prescription',
    'Gửi đơn tư vấn nhanh': 'Quick pharmacist review',
    'Mua trực tiếp': 'Direct Purchase',
    'Không cần đơn bác sĩ': 'No prescription needed',
    'Đặt mua trực tiếp': 'Buy directly online',
    'Thuốc theo đơn': 'Prescription Drugs',
    'Cần đơn bác sĩ': 'Doctor prescription required',
    'Đơn hàng của tôi': 'My Orders',
    'Tra cứu trạng thái': 'Track order status',
    'Vitamin & Đề kháng': 'Vitamins & Immunity',
    'Bổ sung khoáng chất': 'Daily supplements',
    'Vật tư y tế': 'Medical Supplies',
    'Tủ thuốc gia đình': 'Family first-aid kit',
    'Sản phẩm bán chạy • Mua trực tiếp': 'Best Sellers • Direct Purchase',
    'Niêm yết đúng đơn vị bán và trừ tồn tự động trên các lô khả dụng': 'Listed by exact selling unit with automatic stock check across valid batches',
    'Xem tất cả thuốc mua trực tiếp': 'View All Direct Medicines',
    'Danh mục thuốc cần đơn bác sĩ (Gửi đơn tư vấn)': 'Doctor Prescription Medicines (Consultation Required)',
    'Các sản phẩm này cần có đơn bác sĩ và không thanh toán tự động cùng giỏ hàng mua trực tiếp': 'These items require a valid doctor prescription and are not checked out automatically with regular items',
    'Xem nhóm thuốc theo đơn': 'View Prescription Medicines',
    'Tủ thuốc gia đình • Tiêu hóa, Hô hấp & Vật tư y tế': 'Family Healthcare • Digestive, Respiratory & Medical Supplies',
    'Bổ sung điện giải, vệ sinh mũi họng và chăm sóc vết thương hàng ngày': 'Electrolyte rehydration, nasal hygiene, and daily wound care essentials',
    'Khám phá thêm': 'Explore More',

    // Product Card & Units
    'Đơn vị niêm yết': 'Listed unit',
    'Hộp': 'Box',
    'Tuýp': 'Tube',
    'Chai': 'Bottle',
    'Gói': 'Pack',
    'Vỉ': 'Blister',
    'Lọ': 'Vial',
    'Còn hàng': 'In Stock',
    'Tạm hết hàng': 'Out of Stock',
    'Ngừng bán': 'Discontinued',
    'Ngừng kinh doanh': 'Discontinued',
    'Chọn mua': 'Buy Now',
    'Gửi đơn tư vấn': 'Submit Prescription',

    // Footer
    'Thuốc chính hãng 100%': '100% Authentic Medicines',
    'Rõ số lô, hạn dùng & thành phần': 'Clear batch number, expiry & ingredients',
    'Tư vấn đúng theo đơn': 'Accurate Prescription Care',
    'Tách bạch thuốc thường & thuốc theo đơn': 'Clear separation of direct vs. prescription items',
    'Niêm yết đúng đơn vị': 'Exact Unit Pricing',
    'Minh bạch giá theo Hộp / Chai / Tuýp': 'Transparent pricing per Box / Bottle / Tube',
    'Kiểm tồn theo lô tự động': 'Automated Batch Stock Check',
    'Chặn tự động lô hết hạn / bị khóa': 'Automatically blocks expired or locked batches',
    'Danh mục & Mua sắm': 'Categories & Shopping',
    'Tra cứu toàn bộ thuốc': 'Browse All Medicines',
    'Thuốc mua trực tiếp': 'Direct Purchase Medicines',
    'Thuốc cần đơn bác sĩ': 'Doctor Prescription Medicines',
    'Gửi ảnh đơn thuốc tư vấn': 'Upload Prescription Photo',
    'Giỏ hàng & Mã giảm giá': 'Shopping Cart & Vouchers',
    'Chính sách & Hỗ trợ (Dữ liệu mẫu)': 'Policies & Support (Sample Data)',
    'Quy định phân tách Thuốc mua trực tiếp & Thuốc cần đơn bác sĩ': 'Policy on Direct Purchase vs. Doctor Prescription Medicines',
    'Chính sách kiểm tra tồn kho theo lô & bảo mật': 'Batch Inventory Verification & Privacy Policy',
    'Địa chỉ mẫu: Khu vực Thực hành CNTT, Trường Đại học [Dữ liệu mẫu]': 'Sample Address: IT Practice Center, University Campus [Sample Data]',

    // Catalog & Filters
    'Danh sách & Tìm kiếm sản phẩm': 'Product Catalog & Search',
    'Bộ lọc tìm kiếm': 'Search Filters',
    'Bộ lọc': 'Filters',
    'Từ khóa tìm kiếm': 'Search Keyword',
    'Nhóm thuốc': 'Medicine Category',
    'Phân loại mua hàng': 'Purchase Type',
    'Phân loại': 'Purchase Type',
    'Tất cả (Mua trực tiếp + Tư vấn)': 'All (Direct Purchase + Prescription)',
    'Tất cả': 'All',
    'Khoảng giá theo đơn vị': 'Price Range per Unit',
    'Khoảng giá': 'Price Range',
    'Mọi mức giá': 'All Prices',
    'Dưới 50.000 ₫': 'Under 50,000 ₫',
    'Từ 50.000 ₫ – 100.000 ₫': '50,000 ₫ – 100,000 ₫',
    'Trên 100.000 ₫': 'Over 100,000 ₫',
    'Nhà cung cấp': 'Supplier',
    'Tất cả nhà cung cấp': 'All Suppliers',
    'Chỉ hiện thuốc đang còn tồn bán': 'In-stock items only',
    'Chỉ hiện thuốc còn tồn bán': 'In-stock items only',
    'Xóa toàn bộ lọc': 'Clear All Filters',
    'Xóa lọc': 'Clear Filters',
    'Áp dụng': 'Apply',
    'Sắp xếp:': 'Sort by:',
    'Nổi bật / Đề xuất': 'Featured / Recommended',
    'Giá tăng dần': 'Price: Low to High',
    'Giá giảm dần': 'Price: High to Low',
    'Tên A → Z': 'Name: A → Z',
    'Đang lọc theo:': 'Active filters:',
    'Xóa tất cả': 'Clear all',
    'Không tìm thấy sản phẩm khớp với bộ lọc hiện tại': 'No medicines match your current filters',
    'Thử xóa bớt điều kiện lọc hoặc tìm theo tên hoạt chất (ví dụ: Paracetamol, Vitamin C).': 'Try clearing some filters or searching by ingredient name (e.g. Paracetamol, Vitamin C).',
    'Xóa bộ lọc & Xem lại tất cả': 'Reset Filters & View All',
    'Trước': 'Prev',
    'Tiếp': 'Next',

    // Product Detail
    'Giá niêm yết theo đơn vị bán:': 'Listed price per selling unit:',
    'Thành phần chính:': 'Active Ingredients:',
    'Hoạt chất chính:': 'Active Ingredients:',
    'Nhóm danh mục:': 'Category:',
    'Nhà cung cấp:': 'Supplier:',
    'Quy cách đóng gói:': 'Packaging:',
    'Thêm vào giỏ hàng': 'Add to Cart',
    'Đặt mua ngay': 'Buy Now',
    'Xem nhanh trạng thái mẫu:': 'Quick sample states:',
    'Thuốc mua trực tiếp còn hàng': 'In-stock Direct Medicine',
    'Thông tin mô tả & Hướng dẫn bảo quản': 'Product Description & Storage Guide',
    'Sản phẩm liên quan cùng nhóm': 'Related Medicines in Category',
    'Ngừng kinh doanh tạm thời': 'Temporarily Discontinued',
    'Tạm hết hàng có thể bán': 'Currently Out of Sellable Stock',
    'Thuốc cần đơn bác sĩ — Cần gửi đơn để dược sĩ tư vấn': 'Doctor Prescription Required — Pharmacist Review Needed',
    'Gửi đơn thuốc / Yêu cầu tư vấn cho sản phẩm này': 'Submit Prescription / Request Consultation for This Item',

    // Cart & Checkout
    'Thử trạng thái giỏ:': 'Test cart states:',
    'Bình thường': 'Normal',
    'Thiếu tồn kho': 'Stock Shortage',
    'Giá thay đổi': 'Price Changed',
    'Voucher hết lượt': 'Exhausted Voucher',
    'Khám phá thuốc mua trực tiếp': 'Explore Direct Medicines',
    'Nạp nhanh 2 thuốc mẫu vào giỏ': 'Load 2 Sample Items into Cart',
    'Sản phẩm & Đơn vị': 'Product & Unit',
    'Đơn giá': 'Unit Price',
    'Số lượng': 'Quantity',
    'Thành tiền': 'Subtotal',
    'Xóa': 'Remove',
    'Mã giảm giá (Voucher)': 'Promo Code (Voucher)',
    'Chọn nhanh mã mẫu để thử:': 'Quick sample codes to test:',
    'Tóm tắt thanh toán': 'Order Summary',
    'Giảm giá voucher:': 'Voucher Discount:',
    'Phí giao hàng dự kiến:': 'Estimated Shipping:',
    'Miễn phí (0 ₫)': 'Free (0 ₫)',
    'Tổng thanh toán:': 'Total Payment:',
    '1. Thông tin người nhận hàng': '1. Recipient Information',
    '2. Hình thức giao nhận & Phương thức thanh toán': '2. Delivery & Payment Method',
    '3. Kiểm tra sản phẩm & Tổng tiền': '3. Review Items & Total',
    'Giao hàng tận nơi tiêu chuẩn': 'Standard Home Delivery',
    'Nhận tại quầy Nhà thuốc Pharmacy': 'Pick Up at Pharmacy Counter',
    'Thanh toán khi nhận hàng (COD)': 'Cash on Delivery (COD)',
    'Chuyển khoản ngân hàng': 'Bank Transfer',
    'Tổng cộng cần thanh toán:': 'Grand Total:',
    'Quay lại chỉnh sửa giỏ hàng': 'Back to Edit Cart',

    // Status Badges
    'Mới tạo': 'Newly Created',
    'Đã xác nhận': 'Confirmed',
    'Đang chuẩn bị hàng': 'Preparing Order',
    'Hoàn tất': 'Completed',
    'Đã hủy': 'Cancelled',
    'Chưa thanh toán (COD)': 'Unpaid (COD)',
    'Chờ xác nhận CK': 'Pending Transfer',
    'Đã thu tiền': 'Paid',
    'Đã hoàn tiền': 'Refunded',
    'Chưa giao': 'Not Shipped',
    'Đang đóng gói': 'Packing',
    'Đang giao hàng': 'Out for Delivery',
    'Đã giao thành công': 'Delivered',
    'Hoàn kho': 'Returned to Stock',
    'Chờ kiểm tra': 'Pending Review',
    'Cần bổ sung ảnh/TT': 'Needs More Info',
    'Đã phản hồi': 'Responded',

    // Admin Common
    'Vận hành & Danh mục': 'Operations & Catalog',
    'Xem Storefront': 'View Storefront',
    'Bảng điều khiển Quản trị (Dashboard)': 'Admin Dashboard Overview',
    'Số liệu đồng bộ trực tiếp với Đơn hàng, Kho lô và Sản phẩm hiện có': 'Real-time metrics synced with Orders, Batch Inventory, and Products',
    'Kỳ thống kê:': 'Period:',
    'ĐƠN MỚI / CHỜ XỬ LÝ': 'NEW / PENDING ORDERS',
    'Cần xử lý': 'Action Needed',
    'Mở danh sách đơn mới →': 'Open new orders →',
    'Xem chi tiết báo cáo →': 'View detailed report →',
    'Quản lý danh mục thuốc →': 'Manage products →',
    'Kiểm tra lô hàng & nhập kho →': 'Check batches & stock-in →',

    // Additional UI & Pages (Storefront & Admin)
    'Chuyển tới nội dung chính': 'Skip to main content',
    'Trang chủ': 'Home',
    'Giỏ hàng': 'Shopping Cart',
    'Tra cứu nhanh theo tên thuốc, công dụng hoặc thành phần. Đặt mua trực tiếp': 'Quickly search by medicine name, benefits, or ingredients. Buy',
    'thuốc thông thường': 'everyday medicines',
    'đúng đơn vị niêm yết hoặc gửi ảnh': 'by their listed unit or upload a',
    'đơn thuốc của bác sĩ': 'doctor prescription photo',
    'để dược sĩ kiểm tra.': 'for pharmacist verification.',
    'Bước 1:': 'Step 1:',
    'Chụp/tải lên ảnh đơn thuốc rõ nét (≤ 5MB).': 'Take or upload a clear photo of your prescription (≤ 5MB).',
    'Bước 2:': 'Step 2:',
    'Nhập họ tên & số điện thoại nhận tư vấn.': 'Enter your full name & phone number for consultation.',
    'Bước 3:': 'Step 3:',
    'Dược sĩ kiểm tra đơn & phản hồi trực tiếp.': 'Our pharmacist reviews the prescription & responds directly.',
    'Hệ thống giao diện mẫu bán hàng dược phẩm trực tuyến & quản trị kho lô theo bố cục hiện đại, kế thừa đồng bộ tên thương hiệu, màu sắc Teal/Mint và nghiệp vụ từ phần mềm POS WinForms.': 'Online pharmacy storefront & batch inventory management prototype with a modern layout, inheriting the brand identity, Teal/Mint color palette, and business rules from the WinForms POS software.',
    'Lưu ý dữ liệu mẫu bài tập:': 'Academic Prototype Data Notice:',
    'Mọi mức giá, tồn kho, thương hiệu hiển thị và phản hồi đơn thuốc trên trang này là dữ liệu giả lập phục vụ trình diễn đồ án môn học ASP.NET MVC, không phải giao dịch thương mại thực tế.': 'All prices, inventory levels, displayed brands, and prescription responses on this site are simulated sample data for an ASP.NET MVC academic project, not real commercial transactions.',
    'Tổng đài mẫu:': 'Sample Hotline:',
    '1800-0000 (Chỉ mô phỏng)': '1800-0000 (Simulated Only)',
    '© 2026 Pharmacy Store Project (Phong cách Long Châu) — Chuẩn bị cho ASP.NET MVC (Razor Views & Areas/Admin).': '© 2026 Pharmacy Store Project — Prepared for ASP.NET MVC (Razor Views & Areas/Admin).',
    'Hỗ trợ bàn phím (Tab/Esc) • Tương phản WCAG AA • Responsive 375px–1440px': 'Keyboard Accessible (Tab/Esc) • WCAG AA Contrast • Responsive 375px–1440px',
    'Tìm thấy': 'Found',
    'sản phẩm phù hợp': 'matching medicines',
    'Hiển thị trang': 'Showing page',
    '(Tổng': '(Total',
    'mục)': 'items)',
    'Chỉ bán đúng theo đơn vị': 'Sold strictly by listed unit:',
    '(Không tự chia nhỏ hộp/vỉ/viên khi chưa có quy tắc quy đổi).': '(No splitting of boxes/blisters/tablets without conversion rules).',
    'Hộp 15 vỉ × 12 viên nén': 'Box of 15 blisters × 12 tablets',
    'Hộp 15 vỉ × 12 viên nén bao phim.': 'Box of 15 blisters × 12 film-coated tablets.',
    'Hoạt chất niêm yết:': 'Listed Ingredients:',
    'Thành phần:': 'Ingredients:',
    'Phân loại:': 'Category Type:',
    'Quy cách:': 'Packaging:',
    'Đơn vị tính:': 'Selling Unit:',
    'Đơn vị bán trên website:': 'Website Selling Unit:',
    '(không tách lẻ vỉ/viên khi chưa có quy tắc quy đổi).': '(no splitting into blisters/tablets without conversion rules).',
    'Bảo quản nơi khô mát, dưới 30°C, tránh ánh nắng trực tiếp.': 'Store in a cool, dry place below 30°C, away from direct sunlight.',
    'Lưu ý chuyên môn:': 'Professional Notice:',
    'Thông tin trên website chỉ mang tính chất trưng bày danh mục mẫu của bài tập, không thay thế chỉ định chẩn đoán hay liều dùng của bác sĩ.': 'Information on this website is for sample catalog demonstration only and does not replace a doctor diagnosis or dosage instructions.',
    'Đưa sản phẩm vào giỏ chưa giữ tồn kho; hệ thống sẽ kiểm tra lại tồn và giá khi xác nhận đặt hàng.': 'Adding items to cart does not reserve stock; inventory and prices are re-verified upon order confirmation.',
    'Đơn vị bán:': 'Selling unit:',
    '• Tồn có thể bán:': '• Sellable stock:',
    'Nhập mã voucher': 'Enter promo code',
    'Bỏ mã': 'Remove code',
    'PHHET (Hết lượt)': 'PHHET (Exhausted)',
    'PHKHOA (Đã khóa)': 'PHKHOA (Locked)',
    'Checkout — Xác nhận đặt hàng': 'Checkout — Order Confirmation',
    'Sổ địa chỉ: Nhà riêng': 'Address Book: Home',
    'Sổ địa chỉ: Văn phòng': 'Address Book: Office',
    'Họ và tên người nhận': 'Recipient Full Name',
    'Số điện thoại liên hệ': 'Contact Phone Number',
    'Địa chỉ nhận hàng chi tiết': 'Detailed Delivery Address',
    'Ghi chú giao hàng (Tùy chọn)': 'Delivery Note (Optional)',
    'Miễn phí cho đơn từ 150.000 ₫ (dưới 150k phí 15.000 ₫)': 'Free for orders from 150,000 ₫ (15,000 ₫ under 150k)',
    'Giữ đơn chờ tại quầy • Phí vận chuyển 0 ₫': 'Hold order at pharmacy counter • 0 ₫ shipping fee',
    'Trạng thái thanh toán sau khi đặt:': 'Payment status after placing order:',
    'Chưa thanh toán': 'Unpaid',
    'Trạng thái sau khi đặt:': 'Status after placing order:',
    'Chờ xác nhận chuyển khoản': 'Pending transfer verification',
    'Tạm tính:': 'Subtotal:',
    'Phí giao hàng:': 'Shipping Fee:',
    'Chỉ hiển thị các đơn hàng thuộc tài khoản đang đăng nhập. Có thể yêu cầu hủy khi đơn ở trạng thái Mới tạo/Đã xác nhận và chưa thu tiền.': 'Displays only orders belonging to the signed-in account. Cancellation can be requested while an order is Newly Created/Confirmed and unpaid.',
    'Đang giao': 'Out for Delivery',
    'Chi tiết & Lịch sử': 'Details & History',
    'Yêu cầu hủy đơn': 'Request Cancellation',
    'Hồ sơ tài khoản & Sổ địa chỉ nhận hàng': 'Account Profile & Delivery Address Book',
    'Quản lý thông tin liên hệ và địa chỉ giao hàng mặc định cho vai trò': 'Manage contact details and default shipping addresses for role',
    'Xem Đơn của tôi': 'View My Orders',
    'Thông tin hồ sơ khách hàng': 'Customer Profile Information',
    'Họ và tên': 'Full Name',
    'Số điện thoại liên kết POS': 'POS Linked Phone Number',
    'Vai trò tài khoản (Chỉ đọc)': 'Account Role (Read-only)',
    'Email nhận thông báo': 'Notification Email',
    'Điểm tích lũy tại quầy (Dữ liệu quản lý chỉ đọc)': 'Counter Loyalty Points (Read-only)',
    'Lưu thông tin hồ sơ': 'Save Profile Changes',
    'Đổi mật khẩu đăng nhập': 'Change Login Password',
    'Mật khẩu hiện tại': 'Current Password',
    'Mật khẩu mới (≥ 6 ký tự)': 'New Password (≥ 6 characters)',
    'Cập nhật mật khẩu': 'Update Password',
    'Mặc định': 'Default',
    'Đặt mặc định': 'Set as Default',
    'Thêm địa chỉ nhận hàng mới': 'Add New Delivery Address',
    'Nhãn': 'Label',
    'SĐT nhận hàng': 'Recipient Phone',
    'Địa chỉ cụ thể': 'Street Address',
    'Thêm vào sổ địa chỉ': 'Add to Address Book',
    'Gửi đơn thuốc & Yêu cầu dược sĩ tư vấn': 'Upload Prescription & Request Pharmacist Consultation',
    'Luồng dành riêng cho các thuốc cần đơn của bác sĩ. Không tự động thanh toán trực tiếp khi chưa có đối chiếu đơn thuốc.': 'Dedicated workflow for medicines requiring a doctor prescription. Not checked out automatically without prescription verification.',
    '1. Tải ảnh chụp đơn thuốc & Thông tin liên hệ': '1. Upload Prescription Photo & Contact Info',
    'Chọn file ảnh đơn thuốc (JPG, PNG, WEBP ≤ 5MB)': 'Select prescription image (JPG, PNG, WEBP ≤ 5MB)',
    'Tạo thêm ảnh đơn mẫu': 'Add sample prescription image',
    'Thử file lỗi (>5MB / sai loại)': 'Test invalid file (>5MB / wrong type)',
    'Họ tên người bệnh / người liên hệ': 'Patient / Contact Full Name',
    'Số điện thoại nhận tư vấn': 'Consultation Phone Number',
    'Thuốc kê đơn cần hỏi (Tùy chọn đánh dấu)': 'Prescription medicines to inquire about (Optional)',
    'Ghi chú cho dược sĩ': 'Note for Pharmacist',
    'Gửi yêu cầu tư vấn đơn thuốc': 'Submit Prescription Consultation Request',
    '2. Trạng thái các yêu cầu đơn thuốc đã gửi': '2. Submitted Prescription Requests Status',
    'Mở dưới quyền Admin để phản hồi →': 'Open as Admin to respond →',
    'Người gửi:': 'Sender:',
    'Thuốc hỏi:': 'Requested items:',
    'Ghi chú khách:': 'Customer note:',
    'Phản hồi từ nhà thuốc:': 'Pharmacist response:',
    'Đang chờ quản trị viên / dược sĩ kiểm tra ảnh đơn...': 'Waiting for pharmacist / admin to review prescription photo...',
    'Tôi có đơn khám viêm họng tại bệnh viện, nhờ nhà thuốc kiểm tra giúp đơn thuốc đính kèm.': 'I have a hospital prescription for sore throat; please help review the attached prescription.',
    'Nhà thuốc đã đối chiếu ảnh đơn thuốc mẫu RX-2026-001. Sản phẩm Augmentin 1g hiện còn hàng tại quầy. Quý khách vui lòng mang bản gốc đơn thuốc tới nhà thuốc hoặc xác nhận qua SĐT để dược sĩ hướng dẫn nhận thuốc theo đúng quy định.': 'Our pharmacist verified prescription RX-2026-001. Augmentin 1g is currently in stock at the counter. Please bring the original prescription or confirm by phone for dispensing guidance.',
    'Nhờ dược sĩ xem giúp đơn thuốc dạ dày này.': 'Please help check this stomach prescription.',
    'Ảnh chụp phần ngày kê đơn và chữ ký người kê đơn bị khuất góc dưới. Quý khách vui lòng chụp bổ sung toàn bộ mặt tờ đơn thuốc rõ nét để dược sĩ kiểm tra.': 'The prescription date and doctor signature at the bottom corner are cut off. Please upload a clear full-page photo for verification.',
    'Tôi gửi đơn khám ngoại trú sáng nay, cần tư vấn tình trạng còn hàng.': 'I uploaded my outpatient prescription from this morning; please advise on stock availability.',
    'Chính sách hoạt động & Thông tin liên hệ (Dữ liệu mẫu)': 'Operating Policies & Contact Info (Sample Data)',
    'Trang này minh họa cấu trúc thông tin chính sách và quy tắc nghiệp vụ của hệ thống website': 'This page illustrates the policy structure and business rules of the',
    'phục vụ bài tập lớn ASP.NET MVC.': 'website for the ASP.NET MVC project.',
    '1. Quy tắc hiển thị giá & Đơn vị tính': '1. Unit Pricing & Display Rules',
    'Mọi sản phẩm đều niêm yết giá theo đúng một đơn vị bán chuẩn (Hộp, Tuýp, Chai, Gói). Website không tự ý quy đổi sang đơn vị nhỏ hơn nếu chưa có quy tắc quy đổi chuẩn trong CSDL.': 'Every product is priced strictly by its standard selling unit (Box, Tube, Bottle, Pack). No unit splitting occurs without database conversion rules.',
    '2. Quy tắc tách luồng Thuốc cần đơn bác sĩ': '2. Doctor Prescription Separation Rules',
    'Thuốc có cờ': 'Medicines flagged with',
    'được gắn nhãn rõ ràng và chỉ cho phép gửi yêu cầu tư vấn kèm ảnh đơn thuốc, không thanh toán trực tiếp chung với giỏ hàng thông thường.': 'are clearly labeled and only allow submitting a consultation request with a prescription photo, never direct cart checkout.',
    '3. Quy tắc Tồn kho có thể bán & Xuất kho FEFO': '3. Sellable Stock & FEFO Dispensing Rules',
    'Tồn có thể bán chỉ tính các lô còn hạn và không bị khóa của thuốc đang mở bán. Khi xuất kho thực tế ở Backend, hệ thống ưu tiên lô có hạn dùng gần nhất trước (FEFO).': 'Sellable stock counts only unexpired, unlocked batches of active products. Backend dispensing prioritizes First-Expired, First-Out (FEFO) batches.',
    '4. Thông tin liên hệ minh họa [Dữ liệu mẫu]': '4. Sample Contact Information [Sample Data]',
    'Đơn vị thực hiện: Nhóm Sinh viên Đồ án ASP.NET MVC': 'Prepared by: ASP.NET MVC Student Project Team',
    'Địa chỉ mẫu: Khu thực hành Khoa CNTT [Dữ liệu mẫu]': 'Sample Address: IT Faculty Practice Lab [Sample Data]',
    'Email mẫu:': 'Sample Email:',
    'Đăng nhập tài khoản': 'Account Sign In',
    'Đăng nhập để đặt hàng, quản lý sổ địa chỉ và theo dõi đơn thuốc.': 'Sign in to place orders, manage addresses, and track prescriptions.',
    'Số điện thoại hoặc Tên đăng nhập': 'Phone Number or Username',
    'Mật khẩu': 'Password',
    'Quên mật khẩu?': 'Forgot password?',
    'Hiện': 'Show',
    'Ẩn': 'Hide',
    'Đăng nhập': 'Sign In',
    'Tài khoản mẫu để bấm thử nhanh:': 'Sample accounts for quick testing:',
    'Khách: Trần Văn Tuấn': 'Customer: Tran Van Tuan',
    'Khách bị khóa: Bùi Quốc Bảo': 'Locked Customer: Bui Quoc Bao',
    'Quản trị: admin': 'Admin: admin',
    'Chưa có tài khoản khách hàng?': 'Do not have a customer account yet?',
    'Đăng ký ngay': 'Register now',
    '(Quản trị viên)': '(Administrator)',
    '7 ngày gần nhất': 'Last 7 days',
    '30 ngày qua (Tháng 10–11/2026)': 'Last 30 days (Oct–Nov 2026)',
    'Toàn bộ kỳ dữ liệu mẫu': 'All sample period',
    'Định nghĩa: Đơn ở trạng thái Mới tạo hoặc Đã xác nhận, chưa hoàn tất giao.': 'Definition: Orders in Newly Created or Confirmed status, not yet delivered.',
    'Định nghĩa: Chỉ cộng đơn đã thu tiền (PAID). Loại trừ đơn hủy và đơn chưa thu.': 'Definition: Sums only PAID orders. Excludes cancelled and unpaid orders.',
    'SẢN PHẨM CÓ THỂ BÁN': 'SELLABLE PRODUCTS',
    'Định nghĩa: Thuốc đang mở bán, chưa ẩn và có tổng tồn lô hợp lệ > 0.': 'Definition: Active medicines open for sale with valid batch stock > 0.',
    'CẢNH BÁO TỒN / HẠN LÔ': 'STOCK / BATCH EXPIRY ALERTS',
    'Cần kiểm tra': 'Needs Check',
    'Định nghĩa: Thuốc đang bán có tồn ≤ 5 hoặc lô sắp hết hạn / hết hạn / đang khóa.': 'Definition: Active medicines with stock ≤ 5 or near-expiry / expired / locked batches.',
    'Biểu đồ Doanh thu thực thu theo ngày': 'Daily Collected Revenue Chart',
    'Chỉ thống kê các hóa đơn có trạng thái thanh toán': 'Counts only invoices with payment status',
    'Tổng doanh thu đã thu trong kỳ:': 'Total collected revenue in period:',
    'Cảnh báo Lô hàng sắp hết hạn / Đã khóa & Tồn thấp': 'Near-Expiry / Locked Batches & Low Stock Alerts',
    'Xử lý tại Kho/Lô': 'Manage in Inventory',
    'Tên thuốc': 'Medicine Name',
    'Số lô': 'Batch No.',
    'Hạn dùng': 'Expiry Date',
    'Tồn lô': 'Batch Stock',
    'Trạng thái lô': 'Batch Status',
    'Hết hạn': 'Expired',
    'Tạm khóa': 'Locked',
    'Được xuất bán': 'Sellable',
    'Đơn hàng gần nhất cần theo dõi': 'Recent Orders to Monitor',
    'Mã đơn': 'Order Code',
    'Khách hàng': 'Customer',
    'Ngày đặt': 'Order Date',
    'Trạng thái đơn': 'Order Status',
    'Thanh toán': 'Payment',
    'Giao hàng': 'Shipping',
    'Tổng tiền': 'Total Amount',
    'Thao tác': 'Actions',
    'Xử lý': 'Manage',
    'Quản lý Danh mục Sản phẩm & Thuốc': 'Product & Medicine Catalog Management',
    'Thêm/sửa thuốc, tải nhiều ảnh, soạn mô tả Rich Text và đổi trạng thái Đang bán / Ngừng bán / Đã ẩn': 'Add/edit medicines, upload multiple photos, edit Rich Text descriptions, and toggle Selling / Stopped / Hidden states',
    'Thêm sản phẩm mới': 'Add New Medicine',
    'Tìm theo tên, hoạt chất, mã vạch': 'Search by name, ingredient, barcode',
    'Tất cả nhóm': 'All Categories',
    'Trạng thái bán / Phân loại': 'Selling Status / Type',
    'Đang bán': 'Selling',
    'Ngừng bán (IsSelling=0)': 'Stopped (IsSelling=0)',
    'Đã ẩn (IsActive=0)': 'Hidden (IsActive=0)',
    'Ảnh': 'Image',
    'Tên thuốc & Hoạt chất': 'Medicine & Ingredients',
    'Nhóm': 'Category',
    'Đơn vị': 'Unit',
    'Giá bán': 'Selling Price',
    'Tồn thực tế': 'Physical Stock',
    'Tồn có thể bán': 'Sellable Stock',
    'Trạng thái': 'Status',
    'Sửa': 'Edit',
    'Mở bán': 'Resume Selling',
    'Hiện lại': 'Unhide',

    // Official Production UI Strings
    'Hệ thống Nhà thuốc Chính hãng • Dược sĩ tư vấn tận tâm': 'Authentic Pharmacy System • Dedicated Pharmacist Care',
    'Tổng đài tư vấn:': 'Consultation Hotline:',
    '(Miễn phí)': '(Toll-Free)',
    'Chính sách & Hỗ trợ': 'Policies & Support',
    'Hệ thống Nhà thuốc & Dược phẩm': 'Healthcare & Pharmacy System',
    'Về chúng tôi': 'About Us',
    'Đảm bảo nguồn gốc & hạn dùng': 'Guaranteed origin & expiry dates',
    'Dược sĩ tư vấn tận tâm': 'Dedicated Pharmacist Care',
    'Hỗ trợ kiểm tra đơn thuốc nhanh': 'Fast prescription review & support',
    'Giá niêm yết minh bạch': 'Transparent Listed Pricing',
    'Đúng giá theo từng đơn vị bán': 'Accurate price per selling unit',
    'Giao hàng toàn quốc': 'Nationwide Delivery',
    'Miễn phí vận chuyển đơn từ 150.000 ₫': 'Free shipping on orders from 150,000 ₫',
    'Hệ thống Nhà thuốc Pharmacy chuyên cung cấp thuốc kê đơn, thuốc không kê đơn, vitamin và thiết bị y tế chính hãng đạt chuẩn GPP.': 'Pharmacy System specializes in providing authentic prescription medicines, everyday healthcare, vitamins, and GPP-certified medical supplies.',
    '(07:00 – 22:00 hàng ngày)': '(07:00 – 22:00 daily)',
    'Email hỗ trợ:': 'Support Email:',
    'Danh mục sản phẩm': 'Product Categories',
    'Tất cả sản phẩm': 'All Products',
    'Gửi đơn thuốc tư vấn': 'Submit Prescription',
    'Hỗ trợ khách hàng': 'Customer Support',
    'Chính sách đặt hàng & giao nhận': 'Ordering & Delivery Policy',
    'Quy định mua thuốc theo đơn bác sĩ': 'Prescription Medicine Policy',
    'Tra cứu trạng thái đơn hàng': 'Track Order Status',
    'Cổng Quản trị Dược sĩ / Admin': 'Pharmacist / Admin Portal',
    '© 2026 Hệ thống Nhà thuốc Pharmacy. Bảo lưu mọi quyền.': '© 2026 Pharmacy System. All rights reserved.',
    'Giấy chứng nhận Đủ điều kiện Kinh doanh Dược phẩm đạt chuẩn GPP': 'GPP-Certified Pharmaceutical Business License',
    'Nhà thuốc chuẩn GPP • Chính hãng 100%': 'GPP-Certified Pharmacy • 100% Authentic',
    'Chăm sóc Sức khỏe Toàn diện cho Cả Gia đình': 'Complete Healthcare for Your Entire Family',
    'Đặt mua nhanh thuốc thông thường, vitamin và thiết bị y tế giao tận nơi hoặc gửi ảnh đơn thuốc để dược sĩ tư vấn chuyên môn.': 'Order everyday medicines, vitamins, and medical devices for home delivery, or upload your prescription photo for professional pharmacist consultation.',
    'Mua thuốc trực tuyến': 'Shop Medicines Online',
    'Mã ưu đãi:': 'Promo codes:',
    'Tải lên ảnh chụp đơn thuốc rõ nét.': 'Upload a clear photo of your prescription.',
    'Nhập họ tên & số điện thoại liên hệ.': 'Enter your full name & contact phone number.',
    'Dược sĩ liên hệ tư vấn & hướng dẫn nhận thuốc.': 'Our pharmacist contacts you with guidance.',
    'Sản phẩm bán chạy nổi bật': 'Best-Selling Featured Products',
    'Xem tất cả': 'View All',
    'Thuốc theo đơn bác sĩ': 'Doctor Prescription Medicines',
    'Dược sĩ chuyên môn kiểm tra đơn thuốc và tư vấn trực tiếp': 'Verified by licensed pharmacists with direct consultation',
    'Tủ thuốc gia đình & Chăm sóc sức khỏe': 'Family Healthcare & Medical Supplies',
    'Lưu ý:': 'Note:',
    'Thông tin sản phẩm trên website chỉ mang tính chất tham khảo, không thay thế chẩn đoán hoặc chỉ định của bác sĩ. Vui lòng đọc kỹ hướng dẫn sử dụng trước khi dùng.': 'Product information on this website is for reference only and does not replace a doctor diagnosis or prescription. Please read the instructions carefully before use.',
    '← Tiếp tục mua thuốc': '← Continue Shopping',
    'Thông báo cập nhật giỏ hàng:': 'Cart Update Notice:',
    'Bạn chưa có sản phẩm nào trong giỏ hàng.': 'You have no items in your shopping cart.',
    'Khám phá sản phẩm': 'Explore Products',
    'Mã ưu đãi / Giảm giá': 'Promo / Discount Code',
    'Nhập mã ưu đãi': 'Enter promo code',
    'Mã giảm giá hiện có:': 'Available promo codes:',
    'Tóm tắt đơn hàng': 'Order Summary',
    'Thanh toán & Đặt hàng': 'Checkout & Payment',
    'Kiểm tra hàng và thanh toán tiền mặt cho nhân viên giao hàng': 'Inspect your package and pay cash upon delivery',
    'Chuyển khoản nhanh qua mã VietQR hoặc số tài khoản nhà thuốc': 'Fast bank transfer via VietQR or pharmacy bank account',
    'Cảm ơn bạn đã đặt hàng tại': 'Thank you for ordering at',
    '. Dược sĩ đang kiểm tra đơn hàng và sẽ sớm liên hệ xác nhận giao hàng cho bạn.': '. Our pharmacist is reviewing your order and will contact you shortly to confirm delivery.',
    'Hồ sơ tài khoản & Sổ địa chỉ': 'Account Profile & Address Book',
    'Quản lý thông tin liên hệ và địa chỉ giao hàng của bạn': 'Manage your contact details and delivery addresses',
    'Đăng xuất': 'Sign Out',
    'Thông tin cá nhân': 'Personal Information',
    'Số điện thoại': 'Phone Number',
    'Điểm tích lũy thành viên': 'Member Loyalty Points',
    'Lưu thay đổi': 'Save Changes',
    'Đơn hàng của tôi': 'My Orders',
    'Theo dõi tiến độ giao hàng và lịch sử mua sắm của bạn': 'Track your delivery progress and order history',
    'Tải lên ảnh chụp đơn thuốc (JPG, PNG, WEBP ≤ 5MB)': 'Upload prescription photo (JPG, PNG, WEBP ≤ 5MB)',
    '2. Lịch sử đơn thuốc đã gửi': '2. Submitted Prescription History',
    'Giới thiệu & Chính sách Nhà thuốc Pharmacy': 'About Us & Pharmacy Policies',
    'Hệ thống Nhà thuốc Pharmacy cam kết cung cấp thuốc chính hãng, niêm yết giá minh bạch và đồng hành chăm sóc sức khỏe cùng mọi gia đình.': 'Pharmacy System is committed to providing 100% authentic medicines, transparent unit pricing, and dedicated healthcare for every family.',
    '1. Cam kết chất lượng & Giá niêm yết': '1. Quality Commitment & Transparent Pricing',
    '100% sản phẩm đều có nguồn gốc xuất xứ rõ ràng, kiểm soát chặt chẽ số lô và hạn sử dụng, niêm yết giá chuẩn theo từng đơn vị bán (Hộp, Tuýp, Chai, Gói).': '100% of products have verified origins, strict batch and expiry tracking, and transparent pricing per standard selling unit (Box, Tube, Bottle, Pack).',
    '2. Quy định tư vấn Thuốc theo đơn bác sĩ': '2. Doctor Prescription Consultation Policy',
    'Các thuốc kê đơn luôn được dược sĩ chuyên môn kiểm tra đơn thuốc hợp lệ và tư vấn hướng dẫn sử dụng chi tiết trước khi cấp phát.': 'Prescription medicines are always reviewed by licensed pharmacists against a valid prescription with detailed usage guidance before dispensing.',
    '3. Chính sách giao nhận & Đổi trả': '3. Delivery & Return Policy',
    'Miễn phí giao hàng tiêu chuẩn cho đơn từ 150.000 ₫ hoặc hỗ trợ giữ hàng nhận trực tiếp tại quầy nhà thuốc gần nhất.': 'Free standard shipping on orders from 150,000 ₫ or convenient counter pickup at the nearest pharmacy.',
    '4. Thông tin liên hệ hỗ trợ': '4. Customer Support Contact',
    'Tổng đài tư vấn miễn phí:': 'Toll-free Hotline:',
    'Email chăm sóc khách hàng:': 'Customer Care Email:',
    'Trụ sở chính: Số 42 Đường 3/2, Q. Ninh Kiều, TP. Cần Thơ': 'Headquarters: 42 3/2 Street, Ninh Kieu District, Can Tho City',
    '30 ngày qua': 'Last 30 days',
    'Toàn thời gian': 'All time',
    'Đơn mới đặt hoặc đang chờ đóng gói giao hàng': 'Newly placed orders awaiting confirmation or packing',
    'Đã thu': 'Paid',
    'Tổng doanh thu các đơn hàng đã thanh toán': 'Total revenue from paid orders',
    'SẢN PHẨM ĐANG BÁN': 'ACTIVE PRODUCTS',
    'Sản phẩm đang mở bán và còn tồn kho khả dụng': 'Products open for sale with available stock',
    'Sản phẩm sắp hết tồn hoặc lô hàng cận hạn': 'Low-stock products or near-expiry batches',
    'Hình ảnh sản phẩm (Hỗ trợ tải nhiều ảnh, chọn ảnh đại diện chính)': 'Product Images (Upload multiple photos, select primary image)',
    'Mô tả sản phẩm & Hướng dẫn bảo quản': 'Product Description & Storage Guide',
    'Khu vực Quản trị (Admin)': 'Admin Portal',
    'Quản trị (Admin)': 'Admin Portal',
    'Kiểm tra:': 'Role Switch:',
    'Khách hàng': 'Customer',
    'Quản trị': 'Admin',
    'Xem Storefront': 'View Storefront',
    'Xem trang Khách hàng (Storefront)': 'View Storefront',
    'Về trang Khách hàng (Storefront)': 'Back to Storefront'
  };

  let currentLang = localStorage.getItem(STORAGE_KEY) || 'vi';
  let isMutatingDom = false;

  function getLang() {
    return currentLang;
  }

  function setLang(lang) {
    if (lang !== 'vi' && lang !== 'en') return;
    currentLang = lang;
    localStorage.setItem(STORAGE_KEY, lang);
    document.documentElement.lang = lang;
  }

  function t(key, fallback) {
    const pack = dictionary[currentLang] || dictionary.vi;
    return pack[key] || dictionary.vi[key] || fallback || key;
  }

  function tr(viText, enText) {
    return currentLang === 'en' ? enText : viText;
  }

  // Dịch một chuỗi tiếng Việt sang tiếng Anh (hỗ trợ cả khớp chính xác và mẫu câu động)
  function translateTextNodeValue(rawStr) {
    if (currentLang !== 'en' || !rawStr) return rawStr;
    const trimmed = rawStr.trim();
    if (!trimmed) return rawStr;

    // 1. Khớp chính xác trong từ điển
    if (exactViToEn[trimmed]) {
      return rawStr.replace(trimmed, exactViToEn[trimmed]);
    }

    // 2. Các mẫu câu động phổ biến
    let out = trimmed;
    out = out
      .replace(/^Tất cả\s+(\d+)\s+thuốc$/i, 'All $1 medicines')
      .replace(/^(\d+)\s+sản phẩm$/i, '$1 products')
      .replace(/^Còn\s+(\d+)\s+(Hộp|Tuýp|Chai|Gói|Vỉ|Lọ)$/i, (_, n, u) => `${n} ${exactViToEn[u] || u} left`)
      .replace(/^Còn hàng\s+\((\d+)\s+(Hộp|Tuýp|Chai|Gói|Vỉ|Lọ)\s+có thể bán\)$/i, (_, n, u) => `In Stock (${n} ${exactViToEn[u] || u} available)`)
      .replace(/^Sắp hết hàng\s+\(Còn\s+(\d+)\s+(Hộp|Tuýp|Chai|Gói|Vỉ|Lọ)\)$/i, (_, n, u) => `Low Stock (${n} ${exactViToEn[u] || u} left)`)
      .replace(/^Số lượng\s+\((Hộp|Tuýp|Chai|Gói|Vỉ|Lọ)\)$/i, (_, u) => `Quantity (${exactViToEn[u] || u})`)
      .replace(/^\/ (Hộp|Tuýp|Chai|Gói|Vỉ|Lọ)$/i, (_, u) => `/ ${exactViToEn[u] || u}`)
      .replace(/^Thành phần:\s*(.+)$/i, 'Ingredients: $1')
      .replace(/^HC:\s*(.+)$/i, 'Ingredients: $1')
      .replace(/^Mã vạch:\s*(.+)$/i, 'Barcode: $1')
      .replace(/^Ưu đãi mẫu:/i, 'Sample promos:')
      .replace(/^Tất cả nhóm\s*\((\d+)\)$/i, 'All categories ($1)')
      .replace(/^Tất cả\s*\((\d+)\)$/i, 'All ($1)')
      .replace(/^Tìm thấy\s+(\d+)\s+sản phẩm phù hợp$/i, 'Found $1 matching medicines')
      .replace(/^Hiển thị trang\s+(\d+)\s*\/\s*(\d+)\s*\(Tổng\s+(\d+)\s+mục\)$/i, 'Showing page $1 of $2 (Total $3 items)')
      .replace(/^Cài đặt Cookie & Phiên\s*\((.+)\)$/i, 'Cookie & Session Settings ($1)')
      .replace(/^CUSTOMER\s*\((.+)\)$/i, 'CUSTOMER (Customer)')
      .replace(/^Văn Tuấn$/i, 'Customer')
      .replace(/^Đơn hàng của tôi\s*\((.+)\)$/i, 'My Orders ($1)')
      .replace(/^Sổ địa chỉ nhận hàng\s*\((\d+)\)$/i, 'Delivery Address Book ($1)')
      .replace(/^Nhà riêng\s*—\s*(.+)$/i, 'Home — $1')
      .replace(/^Văn phòng\s*—\s*(.+)$/i, 'Office — $1')
      .replace(/^Đã áp dụng mã\s+([A-Z0-9]+):\s+giảm\s+(.+)$/i, 'Applied code $1: -$2')
      .replace(/^Tạm tính\s*\((\d+)\s+(đơn vị|sản phẩm)\):$/i, 'Subtotal ($1 items):')
      .replace(/^(\d+)\s+(Hộp|Tuýp|Chai|Gói|Vỉ|Lọ)\s*×\s*(.+)$/i, (_, n, u, p) => `${n} ${exactViToEn[u] || u} × ${p}`)
      .replace(/^—\s*(\d+)\s+(Hộp|Tuýp|Chai|Gói|Vỉ|Lọ)\s*×\s*(.+)$/i, (_, n, u, p) => `— ${n} ${exactViToEn[u] || u} × ${p}`)
      .replace(/^(.+)\s*\(([\d,.]+\s*₫)\/(Hộp|Tuýp|Chai|Gói|Vỉ|Lọ)\)$/i, (_, name, price, u) => `${name} (${price}/${exactViToEn[u] || u})`)
      .replace(/^Ngày đặt:\s*(.+?)\s*•\s*Kênh:\s*(.+)$/i, 'Ordered: $1 • Channel: $2')
      .replace(/^Địa chỉ:\s*(.+)$/i, 'Address: $1')
      .replace(/^Lý do hủy:\s*(.+)$/i, 'Cancel reason: Cancelled by customer before packing')
      .replace(/^Xem ảnh đơn\s*\((.+)\)$/i, 'View Prescription ($1)')
      .replace(/^(\d+)\s+đơn$/i, '$1 orders')
      .replace(/^DOANH THU ĐÃ THU\s*\((\d+)\s+ĐƠN\)$/i, 'COLLECTED REVENUE ($1 ORDERS)')
      .replace(/^Trên tổng\s+(\d+)$/i, 'Out of $1')
      .replace(/^(\d+)\s+thuốc\s*•\s*(\d+)\s+lô$/i, '$1 medicines • $2 batches')
      .replace(/^Được xuất bán\s*\(Còn\s+(\d+)\s+ngày\)$/i, 'Sellable ($1 days left)')
      .replace(/^Xem toàn bộ\s+(\d+)\s+đơn hàng$/i, 'View all $1 orders');

    if (out !== trimmed) {
      return rawStr.replace(trimmed, out);
    }
    return rawStr;
  }

  // Quét và dịch tự động các Text Node & thuộc tính trong vùng DOM khi ở chế độ EN
  function applyDomTranslation(root = document.body) {
    if (currentLang !== 'en' || !root || isMutatingDom) return;
    isMutatingDom = true;
    try {
      const walker = document.createTreeWalker(root, NodeFilter.SHOW_TEXT, null);
      const textNodes = [];
      let node;
      while ((node = walker.nextNode())) {
        const parentTag = node.parentElement ? node.parentElement.tagName : '';
        if (parentTag !== 'SCRIPT' && parentTag !== 'STYLE') {
          textNodes.push(node);
        }
      }
      textNodes.forEach(tNode => {
        const translated = translateTextNodeValue(tNode.nodeValue);
        if (translated !== tNode.nodeValue) {
          tNode.nodeValue = translated;
        }
      });

      const attrEls = root.querySelectorAll('[placeholder], [title], [aria-label]');
      attrEls.forEach(el => {
        ['placeholder', 'title', 'aria-label'].forEach(attr => {
          const val = el.getAttribute(attr);
          if (val) {
            const trVal = translateTextNodeValue(val);
            if (trVal !== val) el.setAttribute(attr, trVal);
          }
        });
      });
    } finally {
      isMutatingDom = false;
    }
  }

  // Khởi tạo MutationObserver để mọi nội dung AJAX (lọc danh mục, mở modal, toast) đều tự dịch sang EN
  function initDomObserver() {
    if (typeof MutationObserver === 'undefined' || !document.body) return;
    const observer = new MutationObserver(() => {
      if (currentLang === 'en' && !isMutatingDom) {
        applyDomTranslation(document.body);
      }
    });
    observer.observe(document.body, { childList: true, subtree: true });
  }

  if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', initDomObserver);
  } else {
    setTimeout(initDomObserver, 0);
  }

  // Định dạng tiền tệ luôn giữ đơn vị VND (₫), dùng non-breaking space để không bao giờ rớt dòng chữ ₫
  function formatCurrency(amount) {
    const num = Number(amount) || 0;
    const locale = currentLang === 'en' ? 'en-US' : 'vi-VN';
    return new Intl.NumberFormat(locale, { maximumFractionDigits: 0 }).format(num) + '\u00A0₫';
  }

  function formatDate(dateStr, includeTime = false) {
    if (!dateStr) return '—';
    const d = new Date(dateStr);
    if (isNaN(d.getTime())) return dateStr;
    const locale = currentLang === 'en' ? 'en-GB' : 'vi-VN';
    const opts = includeTime
      ? { day: '2-digit', month: '2-digit', year: 'numeric', hour: '2-digit', minute: '2-digit' }
      : { day: '2-digit', month: '2-digit', year: 'numeric' };
    return new Intl.DateTimeFormat(locale, opts).format(d);
  }

  // Tạo ảnh hộp thuốc SVG tinh gọn, trang nhã như bao bì dược phẩm thực tế (không lặp lại chữ thừa)
  function generateMedicineSvgDataUrl(med) {
    const isRx = med && med.isPrescription;
    const isEn = currentLang === 'en';
    const accent = isRx ? '#0284C7' : '#0F766E';
    const softBg = isRx ? '#E0F2FE' : '#CCFBF1';
    const lightTint = isRx ? '#F0F9FF' : '#F0FDFA';
    const shortName = (med && med.name ? med.name : 'Pharmacy').slice(0, 22);
    const subText = (med && med.activeIngredient ? med.activeIngredient : (isEn ? 'Healthcare Product' : 'Chăm sóc sức khỏe')).slice(0, 30);
    const specText = (med && med.packagingSpec ? med.packagingSpec : (med && med.unit ? med.unit : 'Hộp')).slice(0, 28);

    const svg = `<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 320 220" width="320" height="220">
      <rect width="320" height="220" rx="12" fill="#FFFFFF"/>
      <rect x="34" y="20" width="252" height="180" rx="14" fill="#FFFFFF" stroke="#E2E8F0" stroke-width="2"/>
      <path d="M34 34 C34 26 40 20 48 20 L272 20 C280 20 286 26 286 34 L286 68 L34 68 Z" fill="${lightTint}"/>
      <rect x="34" y="64" width="252" height="4" fill="${accent}"/>
      <circle cx="62" cy="44" r="14" fill="${softBg}"/>
      <path d="M62 36 v16 M54 44 h16" stroke="${accent}" stroke-width="3" stroke-linecap="round"/>
      <text x="86" y="48" fill="${accent}" font-family="sans-serif" font-size="11.5" font-weight="800" letter-spacing="0.6">PHARMACY</text>
      <text x="50" y="106" fill="#0F172A" font-family="sans-serif" font-size="17.5" font-weight="800">${escapeXml(shortName)}</text>
      <text x="50" y="130" fill="#475569" font-family="sans-serif" font-size="12" font-weight="600">${escapeXml(subText)}</text>
      <rect x="50" y="152" width="160" height="26" rx="13" fill="#F8FAFC" stroke="#E2E8F0"/>
      <text x="64" y="169" fill="#334155" font-family="sans-serif" font-size="11" font-weight="600">${escapeXml(specText)}</text>
    </svg>`;
    return 'data:image/svg+xml;utf8,' + encodeURIComponent(svg);
  }

  // Tạo ảnh đơn thuốc SVG chuẩn phòng khám
  function generatePrescriptionSampleSvg(code, patientName, doctorName, dateStr, itemsText) {
    const cleanDoctor = String(doctorName || 'BS. Nguyễn Văn Minh').replace(/\s*\(Mẫu\)/gi, '');
    const svg = `<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 600 720" width="600" height="720">
      <rect width="600" height="720" fill="#F8FAFC"/>
      <rect x="24" y="24" width="552" height="672" rx="10" fill="#FFFFFF" stroke="#CBD5E1" stroke-width="2"/>
      <rect x="24" y="24" width="552" height="84" rx="10" fill="#F0FDFA"/>
      <text x="50" y="58" fill="#0F766E" font-family="sans-serif" font-size="20" font-weight="800">ĐƠN THUỐC KHÁM BỆNH</text>
      <text x="50" y="86" fill="#334155" font-family="monospace" font-size="14">Mã đơn: ${escapeXml(code)} • Ngày kê: ${escapeXml(dateStr)}</text>
      <line x1="48" y1="126" x2="552" y2="126" stroke="#CCFBF1" stroke-width="2"/>
      <text x="50" y="162" fill="#0F172A" font-family="sans-serif" font-size="15" font-weight="600">Họ tên người bệnh: ${escapeXml(patientName)}</text>
      <text x="50" y="192" fill="#334155" font-family="sans-serif" font-size="14">Bác sĩ khám bệnh: ${escapeXml(cleanDoctor)}</text>
      <rect x="48" y="220" width="504" height="330" rx="8" fill="#F8FAFC" stroke="#E2E8F0"/>
      <text x="68" y="254" fill="#0F766E" font-family="sans-serif" font-size="15" font-weight="700">Chỉ định thuốc:</text>
      <text x="68" y="294" fill="#0F172A" font-family="sans-serif" font-size="15" font-weight="600">1. ${escapeXml(itemsText || 'Augmentin 1g (Amoxicillin + Clavulanic acid) — 1 Hộp')}</text>
      <text x="86" y="322" fill="#475569" font-family="sans-serif" font-size="13">Uống sau bữa ăn theo chỉ dẫn của bác sĩ.</text>
      <text x="68" y="370" fill="#0F172A" font-family="sans-serif" font-size="15" font-weight="600">2. Nước muối sinh lý Natri Clorid 0.9% — 2 Chai</text>
      <text x="86" y="398" fill="#475569" font-family="sans-serif" font-size="13">Vệ sinh mũi họng 2–3 lần mỗi ngày.</text>
      <text x="420" y="615" fill="#334155" font-family="sans-serif" font-size="13" font-weight="600" text-anchor="middle">Bác sĩ khám bệnh</text>
      <text x="420" y="655" fill="#0F766E" font-family="sans-serif" font-size="14" font-weight="700" text-anchor="middle">${escapeXml(cleanDoctor)}</text>
    </svg>`;
    return 'data:image/svg+xml;utf8,' + encodeURIComponent(svg);
  }

  function escapeXml(unsafe) {
    return String(unsafe || '').replace(/[<>&'"]/g, function (c) {
      switch (c) {
        case '<': return '&lt;';
        case '>': return '&gt;';
        case '&': return '&amp;';
        case '\'': return '&apos;';
        case '"': return '&quot;';
        default: return c;
      }
    });
  }

  return {
    getLang,
    setLang,
    t,
    tr,
    applyDomTranslation,
    formatCurrency,
    formatDate,
    generateMedicineSvgDataUrl,
    generatePrescriptionSampleSvg
  };
})();
