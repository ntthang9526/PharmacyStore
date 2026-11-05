/* ==========================================================================
   components.js — Các Thành phần Giao diện Tái sử dụng (Phong cách Nhà thuốc Long Châu)
   _Header, _Footer, _ProductCard, _Pagination, _StatusBadge, _ConfirmModal, Toast
   ========================================================================== */

window.PharmacyComponents = (function () {
  const DB = window.PharmacyMockDB;
  const I18n = window.PharmacyI18n;
  let lastFocusedElement = null;

  function escapeHtml(str) {
    return String(str == null ? '' : str)
      .replace(/&/g, '&amp;')
      .replace(/</g, '&lt;')
      .replace(/>/g, '&gt;')
      .replace(/"/g, '&quot;')
      .replace(/'/g, '&#39;');
  }

  // Hệ thống SVG Icons đồng nhất (stroke 1.85px, aria-hidden="true")
  function icon(name, size = 16, extraClass = '') {
    const attrs = `width="${size}" height="${size}" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.85" stroke-linecap="round" stroke-linejoin="round" aria-hidden="true" class="flex-shrink-0 ${extraClass}"`;
    const paths = {
      capsule: `<path d="m10.5 20.5 10-10a4.95 4.95 0 1 0-7-7l-10 10a4.95 4.95 0 1 0 7 7Z"/><path d="m8.5 8.5 7 7"/>`,
      search: `<circle cx="11" cy="11" r="8"/><path d="m21 21-4.3-4.3"/>`,
      cart: `<circle cx="8" cy="21" r="1"/><circle cx="19" cy="21" r="1"/><path d="M2.05 2.05h2l2.66 12.42a2 2 0 0 0 2 1.58h9.78a2 2 0 0 0 1.95-1.57l1.65-7.43H5.12"/>`,
      rx: `<path d="M14 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V8z"/><polyline points="14 2 14 8 20 8"/><path d="M9 13h6"/><path d="M12 10v6"/>`,
      user: `<path d="M19 21v-2a4 4 0 0 0-4-4H9a4 4 0 0 0-4 4v2"/><circle cx="12" cy="7" r="4"/>`,
      shield: `<path d="M12 22s8-4 8-10V5l-8-3-8 3v7c0 6 8 10 8 10z"/>`,
      check: `<polyline points="20 6 9 17 4 12"/>`,
      alert: `<path d="m21.73 18-8-14a2 2 0 0 0-3.48 0l-8 14A2 2 0 0 0 4 21h16a2 2 0 0 0 1.73-3Z"/><line x1="12" y1="9" x2="12" y2="13"/><line x1="12" y1="17" x2="12.01" y2="17"/>`,
      xCircle: `<circle cx="12" cy="12" r="10"/><line x1="15" y1="9" x2="9" y2="15"/><line x1="9" y1="9" x2="15" y2="15"/>`,
      info: `<circle cx="12" cy="12" r="10"/><line x1="12" y1="16" x2="12" y2="12"/><line x1="12" y1="8" x2="12.01" y2="8"/>`,
      clock: `<circle cx="12" cy="12" r="10"/><polyline points="12 6 12 12 16 14"/>`,
      box: `<path d="M21 8a2 2 0 0 0-1-1.73l-7-4a2 2 0 0 0-2 0l-7 4A2 2 0 0 0 3 8v8a2 2 0 0 0 1 1.73l7 4a2 2 0 0 0 2 0l7-4A2 2 0 0 0 21 16Z"/><path d="m3.3 7 8.7 5 8.7-5"/><path d="M12 22V12"/>`,
      truck: `<rect x="1" y="3" width="15" height="13"/><polygon points="16 8 20 8 23 11 23 16 16 16 16 8"/><circle cx="5.5" cy="18.5" r="2.5"/><circle cx="18.5" cy="18.5" r="2.5"/>`,
      tag: `<path d="M12 2H2v10l9.29 9.29c.94.94 2.48.94 3.42 0l6.58-6.58c.94-.94.94-2.48 0-3.42L12 2Z"/><path d="M7 7h.01"/>`,
      filter: `<polygon points="22 3 2 3 10 12.46 10 19 14 21 14 12.46 22 3"/>`,
      plus: `<line x1="12" y1="5" x2="12" y2="19"/><line x1="5" y1="12" x2="19" y2="12"/>`,
      edit: `<path d="M11 4H4a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2v-7"/><path d="M18.5 2.5a2.121 2.121 0 0 1 3 3L12 15l-4 1 1-4 9.5-9.5z"/>`,
      trash: `<polyline points="3 6 5 6 21 6"/><path d="M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2"/>`,
      lock: `<rect x="3" y="11" width="18" height="11" rx="2" ry="2"/><path d="M7 11V7a5 5 0 0 1 10 0v4"/>`,
      unlock: `<rect x="3" y="11" width="18" height="11" rx="2" ry="2"/><path d="M7 11V7a5 5 0 0 1 9.9-1"/>`,
      eye: `<path d="M1 12s4-8 11-8 11 8 11 8-4 8-11 8-11-8-11-8z"/><circle cx="12" cy="12" r="3"/>`,
      refresh: `<polyline points="23 4 23 10 17 10"/><polyline points="1 20 1 14 7 14"/><path d="M3.51 9a9 9 0 0 1 14.85-3.36L23 10M1 14l4.64 4.36A9 9 0 0 0 20.49 15"/>`,
      chart: `<line x1="18" y1="20" x2="18" y2="10"/><line x1="12" y1="20" x2="12" y2="4"/><line x1="6" y1="20" x2="6" y2="14"/>`,
      menu: `<line x1="3" y1="12" x2="21" y2="12"/><line x1="3" y1="6" x2="21" y2="6"/><line x1="3" y1="18" x2="21" y2="18"/>`,
      upload: `<path d="M21 15v4a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2v-4"/><polyline points="17 8 12 3 7 8"/><line x1="12" y1="3" x2="12" y2="15"/>`,
      download: `<path d="M21 15v4a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2v-4"/><polyline points="7 10 12 15 17 10"/><line x1="12" y1="15" x2="12" y2="3"/>`,
      globe: `<circle cx="12" cy="12" r="10"/><line x1="2" y1="12" x2="22" y2="12"/><path d="M12 2a15.3 15.3 0 0 1 4 10 15.3 15.3 0 0 1-4 10 15.3 15.3 0 0 1-4-10 15.3 15.3 0 0 1 4-10z"/>`,
      phone: `<path d="M22 16.92v3a2 2 0 0 1-2.18 2 19.79 19.79 0 0 1-8.63-3.07 19.5 19.5 0 0 1-6-6 19.79 19.79 0 0 1-3.07-8.67A2 2 0 0 1 4.11 2h3a2 2 0 0 1 2 1.72 12.84 12.84 0 0 0 .7 2.81 2 2 0 0 1-.45 2.11L8.09 9.91a16 16 0 0 0 6 6l1.27-1.27a2 2 0 0 1 2.11-.45 12.84 12.84 0 0 0 2.81.7A2 2 0 0 1 22 16.92z"/>`
    };
    return `<svg ${attrs}>${paths[name] || paths.info}</svg>`;
  }

  // Nhãn trạng thái dùng chung (_StatusBadge.cshtml) — Luôn kết hợp Icon + Chữ
  function renderStatusBadge(domain, code) {
    const map = {
      order: {
        NEW: { cls: 'info', icon: 'clock', label: 'Mới tạo' },
        CONFIRMED: { cls: 'info', icon: 'check', label: 'Đã xác nhận' },
        PROCESSING: { cls: 'warning', icon: 'box', label: 'Đang chuẩn bị hàng' },
        COMPLETED: { cls: 'success', icon: 'check', label: 'Hoàn tất' },
        CANCELLED: { cls: 'danger', icon: 'xCircle', label: 'Đã hủy' }
      },
      payment: {
        UNPAID: { cls: 'warning', icon: 'clock', label: 'Chưa thanh toán (COD)' },
        PENDING_TRANSFER: { cls: 'warning', icon: 'clock', label: 'Chờ xác nhận CK' },
        PAID: { cls: 'success', icon: 'check', label: 'Đã thu tiền' },
        REFUNDED: { cls: 'neutral', icon: 'refresh', label: 'Đã hoàn tiền' }
      },
      shipping: {
        NOT_SHIPPED: { cls: 'neutral', icon: 'box', label: 'Chưa giao' },
        PACKING: { cls: 'info', icon: 'box', label: 'Đang đóng gói' },
        SHIPPING: { cls: 'warning', icon: 'truck', label: 'Đang giao hàng' },
        DELIVERED: { cls: 'success', icon: 'check', label: 'Đã giao thành công' },
        RETURNED: { cls: 'danger', icon: 'alert', label: 'Hoàn kho' }
      },
      rxRequest: {
        PENDING_REVIEW: { cls: 'warning', icon: 'clock', label: 'Chờ kiểm tra' },
        NEED_MORE_INFO: { cls: 'danger', icon: 'alert', label: 'Cần bổ sung ảnh/TT' },
        RESPONDED: { cls: 'success', icon: 'check', label: 'Đã phản hồi' }
      }
    };

    const item = (map[domain] && map[domain][code]) || { cls: 'neutral', icon: 'info', label: code };
    return `<span class="status-badge status-badge-${item.cls}">${icon(item.icon, 13)}<span>${escapeHtml(item.label)}</span></span>`;
  }

  // 1. Nút nổi chuyển đổi kiểm tra vai trò (Khách hàng <-> Quản trị viên)
  function renderPreviewToolbar() {
    const session = DB.getSession();
    return `
      <aside class="floating-role-switch" aria-label="Kiểm tra phân quyền hệ thống">
        <div class="role-switch-badge">
          <span class="role-switch-label">${icon('shield', 13)} Kiểm tra:</span>
          <button type="button" class="role-btn ${session.accessRole === 'CUSTOMER' ? 'active' : ''}" onclick="PharmacyApp.switchRole('CUSTOMER')" title="Chuyển sang giao diện Khách hàng (Storefront)">
            ${icon('user', 13)} Khách hàng
          </button>
          <button type="button" class="role-btn ${session.accessRole === 'ADMIN' ? 'active' : ''}" onclick="PharmacyApp.switchRole('ADMIN')" title="Chuyển sang khu vực Quản trị viên (Admin Portal)">
            ${icon('shield', 13)} Quản trị (Admin)
          </button>
        </div>
      </aside>
    `;
  }

  // 2. Storefront Header (_Header.cshtml) — Thiết kế chuẩn Nhà thuốc FPT Long Châu
  function renderStoreHeader(currentRoute, queryParams = {}) {
    const session = DB.getSession();
    const db = DB.getDb();
    const lang = I18n.getLang();
    const cartCount = (session.cart || []).reduce((sum, item) => sum + Number(item.quantity || 0), 0);
    const customer = db.customers.find(c => c.id === session.customerId) || db.customers[0];

    let accountControlHtml = '';
    if (session.accessRole === 'GUEST') {
      accountControlHtml = `
        <a href="#/login" class="lc-header-btn">
          ${icon('user', 15)} <span>${I18n.t('nav_login')}</span>
        </a>
      `;
    } else if (session.accessRole === 'CUSTOMER') {
      accountControlHtml = `
        <a href="#/my-orders" class="lc-header-btn d-none d-sm-inline-flex">
          ${icon('box', 15)} <span class="d-none d-md-inline">${I18n.t('nav_my_orders')}</span>
        </a>
        <a href="#/account" class="lc-header-btn">
          ${icon('user', 15)} <span>${escapeHtml(customer.fullName.split(' ').slice(-2).join(' '))}</span>
        </a>
      `;
    } else if (session.accessRole === 'ADMIN') {
      accountControlHtml = `
        <a href="#/admin/dashboard" class="lc-header-btn">
          ${icon('shield', 15)} <span>${I18n.t('nav_admin_portal')}</span>
        </a>
      `;
    }

    return `
      <header class="store-header">
        <!-- Dải thông tin trên cùng -->
        <div class="lc-top-strip d-none d-md-block">
          <div class="container-xxl px-3 d-flex align-items-center justify-content-between">
            <div class="d-flex align-items-center gap-3">
              <span>${icon('shield', 13)} Hệ thống Nhà thuốc Chính hãng • Dược sĩ tư vấn tận tâm</span>
            </div>
            <div class="d-flex align-items-center gap-3">
              <span>Tổng đài tư vấn: <strong>1800 6928</strong> (Miễn phí)</span>
              <span>•</span>
              <a href="#/about-policy">Chính sách &amp; Hỗ trợ</a>
              <span>•</span>
              <a href="#/admin/dashboard" onclick="PharmacyApp.switchRole('ADMIN')" class="text-decoration-none fw-semibold d-inline-flex align-items-center gap-1" style="color:#CCFBF1;">
                ${icon('shield', 13)} <span>Khu vực Quản trị (Admin)</span>
              </a>
            </div>
          </div>
        </div>

        <!-- Header Teal POS chủ đạo (Bố cục kiểu Long Châu) -->
        <div class="lc-main-header">
          <div class="container-xxl px-3 py-3">
            <div class="d-flex flex-wrap flex-lg-nowrap align-items-center justify-content-between gap-2 gap-xl-3">
              <!-- Brand Logo -->
              <a href="#/home" class="brand-mark flex-shrink-0">
                <span class="brand-icon-box">${icon('capsule', 24)}</span>
                <div>
                  <div class="brand-title">${escapeHtml(I18n.t('brand_name'))}</div>
                  <div class="brand-subtitle d-none d-sm-block">Hệ thống Nhà thuốc &amp; Dược phẩm</div>
                </div>
              </a>

              <!-- Thanh tìm kiếm viên thuốc (Pill Search) + Từ khóa gợi ý -->
              <form class="search-box-wrapper order-3 order-lg-2" onsubmit="PharmacyApp.handleHeaderSearch(event)">
                <div class="search-box-field">
                  <label for="globalSearchInput" class="visually-hidden">${I18n.t('search_placeholder')}</label>
                  <span class="search-box-icon">${icon('search', 18)}</span>
                  <input
                    id="globalSearchInput"
                    type="search"
                    class="search-box-input"
                    placeholder="${escapeHtml(I18n.t('search_placeholder'))}"
                    value="${escapeHtml(queryParams.q || '')}"
                  />
                  <button type="submit" class="search-box-btn">${I18n.t('search_btn')}</button>
                </div>
                <div class="lc-search-keywords d-none d-md-flex" aria-label="Từ khóa tìm kiếm phổ biến">
                  <a href="#/products?q=Panadol" class="lc-keyword-chip">Panadol Extra</a>
                  <a href="#/products?q=Oresol" class="lc-keyword-chip">Oresol bù nước</a>
                  <a href="#/products?q=Vitamin" class="lc-keyword-chip">Vitamin C 500mg</a>
                  <a href="#/products?q=Augmentin" class="lc-keyword-chip">Augmentin 1g</a>
                  <a href="#/products?q=Omega" class="lc-keyword-chip">Dầu cá Omega-3</a>
                </div>
              </form>

              <!-- Cụm nút chức năng bên phải -->
              <div class="d-flex align-items-center gap-2 order-2 order-lg-3 flex-shrink-0">
                <button type="button" class="lc-header-btn" onclick="PharmacyApp.toggleLanguage()" aria-label="Chuyển đổi ngôn ngữ Việt / Anh">
                  ${icon('globe', 15)} <span>${lang.toUpperCase()}</span>
                </button>
                ${accountControlHtml}
                <button type="button" class="lc-header-btn" onclick="PharmacyApp.switchRole('ADMIN')" title="Mở khu vực Quản trị & Dược sĩ để kiểm tra">
                  ${icon('shield', 15)} <span>Quản trị</span>
                </button>
                <a href="#/cart" class="lc-cart-pill">
                  ${icon('cart', 16)}
                  <span class="d-none d-sm-inline">${I18n.t('nav_cart')}</span>
                  <span class="lc-cart-badge tabular-nums" id="headerCartCount">${cartCount}</span>
                </a>
              </div>
            </div>
          </div>
        </div>

        <!-- Dải Danh mục Nền trắng Chuẩn Long Châu -->
        <nav class="category-nav-strip" aria-label="Danh mục nhóm thuốc">
          <div class="container-xxl px-3 py-1 d-flex align-items-center gap-1 overflow-auto">
            <a href="#/home" class="category-nav-link ${currentRoute === 'home' ? 'active' : ''}">
              ${I18n.t('nav_home')}
            </a>
            <a href="#/products" class="category-nav-link ${currentRoute === 'products' && !queryParams.categoryId ? 'active' : ''}">
              ${icon('capsule', 14)} Tất cả thuốc
            </a>
            ${db.categories.map(cat => `
              <a href="#/products?categoryId=${cat.id}" class="category-nav-link ${Number(queryParams.categoryId) === cat.id ? 'active' : ''}">
                ${escapeHtml(cat.name)}
              </a>
            `).join('')}
            <a href="#/about-policy" class="category-nav-link ms-auto ${currentRoute === 'about-policy' ? 'active' : ''}">
              Về chúng tôi
            </a>
            <a href="#/admin/dashboard" onclick="PharmacyApp.switchRole('ADMIN')" class="category-nav-link text-teal fw-semibold border-start ps-3" style="color:var(--color-primary-active);">
              ${icon('shield', 14)} Quản trị (Admin)
            </a>
          </div>
        </nav>
      </header>
    `;
  }

  // 3. Storefront Footer (_Footer.cshtml) — Giao diện chính thức tinh gọn
  function renderStoreFooter() {
    return `
      <footer class="bg-white border-top mt-auto pt-4 pb-4" style="border-top: 3px solid var(--color-primary) !important;">
        <div class="container-xxl px-3">
          <!-- Cam kết 4 trụ cột chuẩn Nhà thuốc -->
          <div class="row g-3 pb-4 mb-4 border-bottom" style="border-color:var(--color-border-subtle)!important;">
            <div class="col-6 col-lg-3">
              <div class="d-flex align-items-center gap-3">
                <span class="lc-quick-tile-icon">${icon('shield', 20)}</span>
                <div>
                  <div class="fw-bold small text-dark">Thuốc chính hãng 100%</div>
                  <div class="text-muted" style="font-size:12px;">Đảm bảo nguồn gốc &amp; hạn dùng</div>
                </div>
              </div>
            </div>
            <div class="col-6 col-lg-3">
              <div class="d-flex align-items-center gap-3">
                <span class="lc-quick-tile-icon orange">${icon('rx', 20)}</span>
                <div>
                  <div class="fw-bold small text-dark">Dược sĩ tư vấn tận tâm</div>
                  <div class="text-muted" style="font-size:12px;">Hỗ trợ kiểm tra đơn thuốc nhanh</div>
                </div>
              </div>
            </div>
            <div class="col-6 col-lg-3">
              <div class="d-flex align-items-center gap-3">
                <span class="lc-quick-tile-icon green">${icon('tag', 20)}</span>
                <div>
                  <div class="fw-bold small text-dark">Giá niêm yết minh bạch</div>
                  <div class="text-muted" style="font-size:12px;">Đúng giá theo từng đơn vị bán</div>
                </div>
              </div>
            </div>
            <div class="col-6 col-lg-3">
              <div class="d-flex align-items-center gap-3">
                <span class="lc-quick-tile-icon">${icon('truck', 20)}</span>
                <div>
                  <div class="fw-bold small text-dark">Giao hàng toàn quốc</div>
                  <div class="text-muted" style="font-size:12px;">Miễn phí vận chuyển đơn từ 150.000 ₫</div>
                </div>
              </div>
            </div>
          </div>

          <div class="row g-4">
            <div class="col-12 col-md-5">
              <div class="d-flex align-items-center gap-2 mb-2">
                <span class="brand-icon-box" style="width:34px;height:34px;">${icon('capsule', 18)}</span>
                <span class="fw-bold fs-5" style="color:var(--color-primary);">Pharmacy</span>
              </div>
              <p class="text-secondary small mb-2" style="max-width: 54ch;">
                Hệ thống Nhà thuốc Pharmacy chuyên cung cấp thuốc kê đơn, thuốc không kê đơn, vitamin và thiết bị y tế chính hãng đạt chuẩn GPP.
              </p>
              <div class="small text-secondary">
                <div><strong>Tổng đài tư vấn:</strong> <span class="text-teal fw-bold">1800 6928</span> (07:00 – 22:00 hàng ngày)</div>
                <div><strong>Email hỗ trợ:</strong> cskh@pharmacy.vn</div>
              </div>
            </div>
            <div class="col-6 col-md-3">
              <h2 class="h6 fw-bold mb-3" style="color:var(--color-primary-active);">Danh mục sản phẩm</h2>
              <ul class="list-unstyled small d-flex flex-column gap-2 mb-0">
                <li><a href="#/products" class="text-secondary text-decoration-none">Tất cả sản phẩm</a></li>
                <li><a href="#/products?rxMode=OTC" class="text-secondary text-decoration-none">Thuốc mua trực tiếp</a></li>
                <li><a href="#/products?rxMode=RX" class="text-secondary text-decoration-none">Thuốc cần đơn bác sĩ</a></li>
                <li><a href="#/prescription-upload" class="text-secondary text-decoration-none">Gửi đơn thuốc tư vấn</a></li>
              </ul>
            </div>
            <div class="col-6 col-md-4">
              <h2 class="h6 fw-bold mb-3" style="color:var(--color-primary-active);">Hỗ trợ khách hàng</h2>
              <ul class="list-unstyled small d-flex flex-column gap-2 mb-0 text-secondary">
                <li><a href="#/about-policy" class="text-secondary text-decoration-none">Chính sách đặt hàng &amp; giao nhận</a></li>
                <li><a href="#/about-policy" class="text-secondary text-decoration-none">Quy định mua thuốc theo đơn bác sĩ</a></li>
                <li><a href="#/my-orders" class="text-secondary text-decoration-none">Tra cứu trạng thái đơn hàng</a></li>
                <li><a href="#/admin/dashboard" onclick="PharmacyApp.switchRole('ADMIN')" class="text-secondary text-decoration-none">${icon('shield', 13)} Cổng Quản trị Dược sĩ / Admin</a></li>
              </ul>
            </div>
          </div>
          <hr class="my-4" style="border-color:var(--color-border-subtle);" />
          <div class="d-flex flex-wrap justify-content-between align-items-center gap-2 small text-muted">
            <span>© 2026 Hệ thống Nhà thuốc Pharmacy. Bảo lưu mọi quyền.</span>
            <span>Giấy chứng nhận Đủ điều kiện Kinh doanh Dược phẩm đạt chuẩn GPP</span>
          </div>
        </div>
      </footer>
    `;
  }

  // 4. Standardized Product Card (_ProductCard.cshtml) — Gọn gàng như giao diện chính thức
  function renderProductCard(med) {
    const isRx = Boolean(med.isPrescription);
    const isSelling = Boolean(med.isSelling);
    const avail = Number(med.availableStock) || 0;

    let topBadgesHtml = '';
    if (!isSelling) {
      topBadgesHtml = `<span class="status-badge status-badge-danger">${I18n.t('stock_stopped')}</span>`;
    } else if (avail <= 0) {
      topBadgesHtml = `<span class="status-badge status-badge-neutral">${I18n.t('stock_out')}</span>`;
    } else if (isRx) {
      topBadgesHtml = `<span class="status-badge status-badge-warning">${icon('rx', 12)} <span>${I18n.t('badge_rx')}</span></span>`;
    } else if (avail <= 5) {
      topBadgesHtml = `<span class="status-badge status-badge-warning">Sắp hết hàng</span>`;
    }

    let ctaButtonHtml = '';
    if (!isSelling) {
      ctaButtonHtml = `
        <button type="button" class="btn-ph w-100" disabled>
          <span>${I18n.t('btn_stopped_selling')}</span>
        </button>`;
    } else if (isRx) {
      ctaButtonHtml = `
        <a href="#/prescription-upload?medicineId=${med.id}" class="btn-ph btn-ph-rx w-100">
          ${icon('rx', 15)} <span>${I18n.t('btn_consult_rx')}</span>
        </a>`;
    } else if (avail <= 0) {
      ctaButtonHtml = `
        <button type="button" class="btn-ph w-100" disabled>
          <span>${I18n.t('btn_out_of_stock')}</span>
        </button>`;
    } else {
      ctaButtonHtml = `
        <button type="button" class="btn-ph btn-ph-primary w-100" onclick="PharmacyApp.addToCart(${med.id}, 1)">
          ${icon('cart', 15)} <span>${I18n.t('btn_add_cart')}</span>
        </button>`;
    }

    return `
      <article class="product-card" aria-label="${escapeHtml(med.name)}">
        <a href="#/product/${med.id}" class="product-card-media text-decoration-none">
          ${topBadgesHtml ? `<div class="product-card-badges">${topBadgesHtml}</div>` : ''}
          <img src="${med.primaryImage}" alt="Hình hộp ${escapeHtml(med.name)}" />
        </a>
        <div class="product-card-body">
          <a href="#/product/${med.id}" class="product-card-title" title="${escapeHtml(med.name)}">
            ${escapeHtml(med.name)}
          </a>
          <div class="lc-unit-pill-row">
            <span class="lc-unit-pill">${escapeHtml(med.unit)}</span>
            <span class="text-muted text-truncate" style="font-size:12px;">${escapeHtml(med.packagingSpec || med.activeIngredient || '')}</span>
          </div>
          <div class="product-card-price-row">
            <div class="d-flex align-items-baseline gap-1">
              <span class="product-card-price tabular-nums">${I18n.formatCurrency(med.price)}</span>
              <span class="product-card-unit">/ ${escapeHtml(med.unit)}</span>
            </div>
          </div>
          ${ctaButtonHtml}
        </div>
      </article>
    `;
  }

  // 5. Pagination Component (_Pagination.cshtml)
  function renderPagination(meta, onPageClickJs) {
    if (!meta || meta.totalPages <= 1) return '';
    const pages = [];
    for (let i = 1; i <= meta.totalPages; i++) {
      pages.push(`
        <li class="page-item ${i === meta.page ? 'active' : ''}">
          <button type="button" class="page-link" onclick="${onPageClickJs}(${i})" ${i === meta.page ? 'aria-current="page"' : ''}>${i}</button>
        </li>
      `);
    }
    return `
      <nav aria-label="Phân trang danh sách" class="d-flex flex-wrap align-items-center justify-content-between gap-2 mt-4 pt-2 border-top">
        <span class="small text-secondary tabular-nums">
          Hiển thị trang <strong>${meta.page}</strong> / <strong>${meta.totalPages}</strong> (Tổng <strong>${meta.totalItems}</strong> mục)
        </span>
        <ul class="pagination pagination-sm mb-0">
          <li class="page-item ${meta.page <= 1 ? 'disabled' : ''}">
            <button type="button" class="page-link" onclick="${onPageClickJs}(${meta.page - 1})" ${meta.page <= 1 ? 'disabled' : ''}>Trước</button>
          </li>
          ${pages.join('')}
          <li class="page-item ${meta.page >= meta.totalPages ? 'disabled' : ''}">
            <button type="button" class="page-link" onclick="${onPageClickJs}(${meta.page + 1})" ${meta.page >= meta.totalPages ? 'disabled' : ''}>Tiếp</button>
          </li>
        </ul>
      </nav>
    `;
  }

  // 6. Skeleton Loaders
  function renderSkeletonCards(count = 4) {
    return `<div class="row g-3">` + Array.from({ length: count }).map(() => `
      <div class="col-6 col-md-4 col-lg-3">
        <div class="product-card p-3">
          <div class="skeleton-box w-100 mb-3" style="height:140px;"></div>
          <div class="skeleton-box w-50 mb-2" style="height:12px;"></div>
          <div class="skeleton-box w-100 mb-2" style="height:20px;"></div>
          <div class="skeleton-box w-75 mb-3" style="height:16px;"></div>
          <div class="skeleton-box w-100" style="height:38px;"></div>
        </div>
      </div>
    `).join('') + `</div>`;
  }

  function renderSkeletonTable(rows = 5, cols = 6) {
    return `
      <div class="p-3">
        ${Array.from({ length: rows }).map(() => `
          <div class="d-flex gap-3 mb-3">
            ${Array.from({ length: cols }).map(() => `<div class="skeleton-box flex-grow-1" style="height:24px;"></div>`).join('')}
          </div>
        `).join('')}
      </div>
    `;
  }

  // 7. Toast Notification (Thay thế hoàn toàn alert())
  function showToast(message, type = 'success') {
    const stack = document.getElementById('toastStack');
    if (!stack) return;
    const id = 'toast_' + Date.now();
    const cls = type === 'error' ? 'toast-error' : (type === 'warning' ? 'toast-warning' : '');
    const ic = type === 'error' ? icon('xCircle', 18) : (type === 'warning' ? icon('alert', 18) : icon('check', 18));

    const item = document.createElement('div');
    item.id = id;
    item.className = `toast-item ${cls}`;
    item.setAttribute('role', 'status');
    item.setAttribute('aria-live', 'polite');
    item.innerHTML = `
      <div class="d-flex align-items-start gap-2">
        <span class="mt-1">${ic}</span>
        <div>${escapeHtml(message)}</div>
      </div>
      <button type="button" class="btn-close btn-sm" aria-label="Đóng thông báo" onclick="document.getElementById('${id}')?.remove()"></button>
    `;
    stack.appendChild(item);
    setTimeout(() => {
      if (item.parentNode) item.remove();
    }, 3800);
  }

  // 8. Accessible Modal System (Giữ focus & trả focus về nút mở khi đóng)
  function openModal({ title, bodyHtml, footerHtml = '', size = '', onMount = null }) {
    lastFocusedElement = document.activeElement;
    const root = document.getElementById('modalRoot');
    if (!root) return;

    const sizeCls = size === 'lg' ? 'modal-lg-ph' : (size === 'sm' ? 'modal-sm-ph' : '');
    root.innerHTML = `
      <div class="modal-backdrop-ph" id="activeModalBackdrop" role="dialog" aria-modal="true" aria-labelledby="activeModalTitle">
        <div class="modal-dialog-ph ${sizeCls}">
          <div class="modal-header-ph">
            <h2 class="h5 fw-bold mb-0" id="activeModalTitle">${escapeHtml(title)}</h2>
            <button type="button" class="btn-close" aria-label="Đóng hộp thoại" onclick="PharmacyComponents.closeModal()"></button>
          </div>
          <div class="modal-body-ph">${bodyHtml}</div>
          ${footerHtml ? `<div class="modal-footer-ph">${footerHtml}</div>` : ''}
        </div>
      </div>
    `;

    const dialog = root.querySelector('.modal-dialog-ph');
    const focusable = dialog.querySelectorAll('button, [href], input, select, textarea, [tabindex]:not([tabindex="-1"])');
    if (focusable.length) {
      focusable[0].focus();
    }

    if (typeof onMount === 'function') {
      onMount(dialog);
    }
  }

  function closeModal() {
    const root = document.getElementById('modalRoot');
    if (root) root.innerHTML = '';
    if (lastFocusedElement && typeof lastFocusedElement.focus === 'function') {
      lastFocusedElement.focus();
    }
  }

  function openConfirmModal({ title, message, confirmText = 'Xác nhận', cancelText = 'Hủy thao tác', danger = false, onConfirm }) {
    window.__pendingModalConfirm = () => {
      closeModal();
      if (typeof onConfirm === 'function') onConfirm();
    };
    openModal({
      title,
      size: 'sm',
      bodyHtml: `<p class="mb-0 text-secondary">${escapeHtml(message)}</p>`,
      footerHtml: `
        <button type="button" class="btn-ph btn-ph-secondary" onclick="PharmacyComponents.closeModal()">${escapeHtml(cancelText)}</button>
        <button type="button" class="btn-ph ${danger ? 'btn-ph-danger' : 'btn-ph-primary'}" onclick="window.__pendingModalConfirm()">
          ${escapeHtml(confirmText)}
        </button>
      `
    });
  }

  return {
    escapeHtml,
    icon,
    renderStatusBadge,
    renderPreviewToolbar,
    renderStoreHeader,
    renderStoreFooter,
    renderProductCard,
    renderPagination,
    renderSkeletonCards,
    renderSkeletonTable,
    showToast,
    openModal,
    closeModal,
    openConfirmModal
  };
})();
