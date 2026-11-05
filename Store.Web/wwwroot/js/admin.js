/* ==========================================================================
   admin.js — Khu vực Quản trị Hệ thống (Areas/Admin Views)
   1. Dashboard (#/admin/dashboard)
   2. Quản lý Sản phẩm + Upload nhiều ảnh + Rich Text Editor (#/admin/products)
   3. Nhóm thuốc & Nhà cung cấp (#/admin/categories-suppliers)
   4. Kho / Lô hàng & Phiếu nhập (#/admin/inventory)
   5. Quản lý Đơn hàng & OrderVersion (#/admin/orders)
   6. Yêu cầu Đơn thuốc (#/admin/prescriptions)
   7. Khách hàng & Tài khoản Web (#/admin/customers)
   8. Quản lý Voucher (#/admin/vouchers)
   9. Báo cáo Doanh thu (#/admin/reports)
   ========================================================================== */

window.PharmacyAdmin = (function () {
  const DB = window.PharmacyMockDB;
  const API = window.PharmacyApiService;
  const C = window.PharmacyComponents;
  const I18n = window.PharmacyI18n;
  const esc = C.escapeHtml;

  let sidebarCollapsed = false;
  let mobileSidebarOpen = false;

  // Trạng thái bộ lọc các màn Admin
  let dashPeriod = '30d';
  let prodFilter = { q: '', categoryId: '', status: 'ALL', page: 1, pageSize: 7 };
  let invTab = 'batches'; // 'batches' | 'stockin'
  let batchFilter = { q: '', state: 'ALL', page: 1, pageSize: 8 };
  let orderFilter = { q: '', orderStatus: 'ALL', paymentStatus: 'ALL', page: 1, pageSize: 6 };
  let rxAdminFilter = 'ALL';
  let custFilter = { q: '', status: 'ALL' };
  let reportFilter = { from: '2026-10-01', to: '2026-11-03' };

  // Trạng thái tạm của ảnh trong Modal Thêm/Sửa sản phẩm
  let editingProductImages = [];

  const adminNavItems = [
    { key: 'dashboard', route: '#/admin/dashboard', icon: 'chart', labelKey: 'admin_dashboard' },
    { key: 'products', route: '#/admin/products', icon: 'capsule', labelKey: 'admin_products' },
    { key: 'categories-suppliers', route: '#/admin/categories-suppliers', icon: 'tag', labelKey: 'admin_cat_sup' },
    { key: 'inventory', route: '#/admin/inventory', icon: 'box', labelKey: 'admin_inventory' },
    { key: 'orders', route: '#/admin/orders', icon: 'truck', labelKey: 'admin_orders' },
    { key: 'prescriptions', route: '#/admin/prescriptions', icon: 'rx', labelKey: 'admin_prescriptions' },
    { key: 'customers', route: '#/admin/customers', icon: 'user', labelKey: 'admin_customers' },
    { key: 'vouchers', route: '#/admin/vouchers', icon: 'tag', labelKey: 'admin_vouchers' },
    { key: 'reports', route: '#/admin/reports', icon: 'chart', labelKey: 'admin_reports' }
  ];

  // Layout Khung Quản trị (_AdminLayout.cshtml)
  function renderAdminShell(container, sectionKey, queryParams = {}) {
    const db = DB.getDb();
    const pendingRxCount = db.prescriptionRequests.filter(r => r.status === 'PENDING_REVIEW').length;
    const newOrdersCount = db.orders.filter(o => o.orderStatus === 'NEW').length;
    const currentNav = adminNavItems.find(n => n.key === sectionKey) || adminNavItems[0];
    const lang = I18n.getLang();

    container.innerHTML = `
      <div class="admin-shell">
        ${mobileSidebarOpen ? `<div class="mobile-drawer-backdrop d-lg-none" onclick="PharmacyAdmin.toggleMobileSidebar(false)"></div>` : ''}

        <!-- Admin Sidebar (_AdminSidebar.cshtml) -->
        <aside class="admin-sidebar ${sidebarCollapsed ? 'collapsed' : ''} ${mobileSidebarOpen ? 'mobile-open' : ''}" aria-label="Menu Quản trị">
          <div class="p-3 border-bottom border-secondary border-opacity-25 d-flex align-items-center justify-content-between">
            <a href="#/admin/dashboard" class="d-flex align-items-center gap-2 text-white text-decoration-none">
              <span class="brand-icon-box" style="width:34px;height:34px;">${C.icon('capsule', 18)}</span>
              <div class="sidebar-brand-text">
                <div class="fw-bold lh-1">Pharmacy Admin</div>
                <small class="text-secondary" style="font-size:11px;">Areas / Admin Portal</small>
              </div>
            </a>
            <button type="button" class="btn btn-sm text-secondary d-none d-lg-inline-flex p-1" onclick="PharmacyAdmin.toggleSidebarCollapse()" aria-label="Thu gọn hoặc mở rộng thanh bên">
              ${C.icon('menu', 18)}
            </button>
          </div>

          <div class="py-2 flex-grow-1 overflow-auto">
            <div class="px-3 py-1 text-uppercase text-secondary sidebar-section-title" style="font-size:11px;font-weight:700;">Vận hành &amp; Danh mục</div>
            ${adminNavItems.map(item => {
              let badge = '';
              if (item.key === 'orders' && newOrdersCount > 0) {
                badge = `<span class="badge rounded-pill bg-warning text-dark ms-auto sidebar-label">${newOrdersCount}</span>`;
              } else if (item.key === 'prescriptions' && pendingRxCount > 0) {
                badge = `<span class="badge rounded-pill bg-info text-dark ms-auto sidebar-label">${pendingRxCount}</span>`;
              }
              return `
                <a href="${item.route}" class="admin-nav-link ${item.key === sectionKey ? 'active' : ''}" onclick="PharmacyAdmin.toggleMobileSidebar(false)">
                  ${C.icon(item.icon, 18)}
                  <span class="sidebar-label">${esc(I18n.t(item.labelKey))}</span>
                  ${badge}
                </a>
              `;
            }).join('')}
          </div>

          <div class="p-3 border-top border-secondary border-opacity-25">
            <a href="#/home" onclick="PharmacyApp.switchRole('CUSTOMER')" class="admin-nav-link m-0 bg-dark">
              ${C.icon('globe', 16)}
              <span class="sidebar-label">Về trang Khách hàng (Storefront)</span>
            </a>
          </div>
        </aside>

        <!-- Main Area -->
        <div class="admin-main-wrapper">
          <!-- Admin Topbar (_AdminTopbar.cshtml) -->
          <header class="admin-topbar">
            <div class="d-flex align-items-center gap-2 min-w-0">
              <button type="button" class="btn-ph btn-ph-secondary btn-ph-sm d-lg-none" onclick="PharmacyAdmin.toggleMobileSidebar(true)" aria-label="Mở menu quản trị">
                ${C.icon('menu', 18)}
              </button>
              <nav aria-label="breadcrumb" class="min-w-0">
                <ol class="breadcrumb small mb-0 flex-nowrap">
                  <li class="breadcrumb-item d-none d-sm-inline"><a href="#/admin/dashboard" class="text-decoration-none">Admin</a></li>
                  <li class="breadcrumb-item active fw-semibold text-truncate" aria-current="page">${esc(I18n.t(currentNav.labelKey))}</li>
                </ol>
              </nav>
            </div>

            <div class="d-flex align-items-center gap-2 flex-shrink-0">
              <button type="button" class="btn-ph btn-ph-secondary btn-ph-sm" onclick="PharmacyApp.toggleLanguage()" aria-label="Đổi ngôn ngữ giao diện">
                ${C.icon('globe', 14)} <span>${lang.toUpperCase()}</span>
              </button>
              <a href="#/home" onclick="PharmacyApp.switchRole('CUSTOMER')" class="btn-ph btn-ph-secondary btn-ph-sm d-none d-sm-inline-flex">
                ${C.icon('globe', 14)} Xem Storefront
              </a>
              <span class="status-badge status-badge-success">
                ${C.icon('shield', 13)} <span>admin<span class="d-none d-md-inline"> (Quản trị viên)</span></span>
              </span>
            </div>
          </header>

          <!-- Nội dung từng màn hình Quản trị -->
          <div class="admin-content-area" id="adminSectionContainer"></div>
        </div>
      </div>
    `;

    const secContainer = document.getElementById('adminSectionContainer');
    switch (sectionKey) {
      case 'dashboard': renderDashboardSection(secContainer); break;
      case 'products': renderProductsSection(secContainer, queryParams); break;
      case 'categories-suppliers': renderCategoriesSuppliersSection(secContainer); break;
      case 'inventory': renderInventorySection(secContainer, queryParams); break;
      case 'orders': renderOrdersSection(secContainer, queryParams); break;
      case 'prescriptions': renderPrescriptionsSection(secContainer); break;
      case 'customers': renderCustomersSection(secContainer); break;
      case 'vouchers': renderVouchersSection(secContainer); break;
      case 'reports': renderReportsSection(secContainer); break;
      default: renderDashboardSection(secContainer);
    }
  }

  function toggleSidebarCollapse() {
    sidebarCollapsed = !sidebarCollapsed;
    window.PharmacyApp.refreshCurrentRoute();
  }

  function toggleMobileSidebar(open) {
    mobileSidebarOpen = Boolean(open);
    window.PharmacyApp.refreshCurrentRoute();
  }

  // ==================== 1. DASHBOARD (#/admin/dashboard) ====================
  function renderDashboardSection(el) {
    const db = DB.getDb();
    const enrichedMeds = db.medicines.map(m => DB.enrichMedicine(m));

    // Doanh thu đã thu: CHỈ tính đơn có PaymentStatus === 'PAID' và OrderStatus !== 'CANCELLED'
    const paidOrders = db.orders.filter(o => o.paymentStatus === 'PAID' && o.orderStatus !== 'CANCELLED');
    const collectedRevenue = paidOrders.reduce((s, o) => s + Number(o.totalAmount || 0), 0);
    const newOrders = db.orders.filter(o => ['NEW', 'CONFIRMED'].includes(o.orderStatus));
    const sellableProducts = enrichedMeds.filter(m => m.isActive && m.isSelling && m.availableStock > 0);

    // Cảnh báo tồn thấp (AvailableStock <= 5 cho thuốc đang bán) hoặc lô sắp hết hạn / hết hạn
    const lowStockMeds = enrichedMeds.filter(m => m.isActive && m.isSelling && m.availableStock <= 5);
    const batchAlerts = db.batches.map(b => {
      const m = db.medicines.find(x => x.id === b.medicineId);
      const st = DB.computeBatchStatus(b, m);
      return { batch: b, med: m, st };
    }).filter(x => x.st.isExpired || x.st.isNearExpiry || x.batch.isBlocked);

    const chartBars = [
      { label: '18/10', val: 125000 },
      { label: '21/10', val: 172800 },
      { label: '24/10', val: 120000 },
      { label: '26/10', val: 85000 },
      { label: '28/10', val: 152000 },
      { label: '30/10', val: 149000 }
    ];
    const maxVal = Math.max(...chartBars.map(b => b.val), 200000);

    el.innerHTML = `
      <div class="d-flex flex-wrap align-items-center justify-content-between gap-2 mb-4">
        <div>
          <h1 class="h4 fw-bold mb-1">Bảng điều khiển Quản trị (Dashboard)</h1>
          <p class="small text-secondary mb-0">Số liệu đồng bộ trực tiếp với Đơn hàng, Kho lô và Sản phẩm hiện có</p>
        </div>
        <div class="d-flex align-items-center gap-2">
          <label for="dashPeriodSel" class="small text-secondary mb-0">Kỳ thống kê:</label>
          <select id="dashPeriodSel" class="form-select-ph py-1 px-2" style="width:auto;min-height:36px;font-size:14px;" onchange="PharmacyAdmin.changeDashPeriod(this.value)">
            <option value="7d" ${dashPeriod === '7d' ? 'selected' : ''}>7 ngày gần nhất</option>
            <option value="30d" ${dashPeriod === '30d' ? 'selected' : ''}>30 ngày qua</option>
            <option value="all" ${dashPeriod === 'all' ? 'selected' : ''}>Toàn thời gian</option>
          </select>
        </div>
      </div>

      <!-- 4 KPI Cards tinh gọn -->
      <div class="row g-3 mb-4">
        <div class="col-12 col-sm-6 col-xl-3">
          <div class="kpi-card">
            <div>
              <div class="d-flex justify-content-between align-items-center">
                <span class="kpi-card-title">ĐƠN MỚI / CHỜ XỬ LÝ</span>
                <span class="status-badge status-badge-info">Cần xử lý</span>
              </div>
              <div class="kpi-card-value tabular-nums">${newOrders.length} đơn</div>
              <div class="kpi-card-def">Đơn mới đặt hoặc đang chờ đóng gói giao hàng</div>
            </div>
            <a href="#/admin/orders?status=NEW" class="small fw-semibold text-decoration-none" style="color:var(--color-primary);">Mở danh sách đơn mới →</a>
          </div>
        </div>

        <div class="col-12 col-sm-6 col-xl-3">
          <div class="kpi-card">
            <div>
              <div class="d-flex justify-content-between align-items-center">
                <span class="kpi-card-title">DOANH THU ĐÃ THU (${paidOrders.length} ĐƠN)</span>
                <span class="status-badge status-badge-success">Đã thu</span>
              </div>
              <div class="kpi-card-value tabular-nums" style="color:var(--color-primary-active);">${I18n.formatCurrency(collectedRevenue)}</div>
              <div class="kpi-card-def">Tổng doanh thu các đơn hàng đã thanh toán</div>
            </div>
            <a href="#/admin/reports" class="small fw-semibold text-decoration-none" style="color:var(--color-primary);">Xem chi tiết báo cáo →</a>
          </div>
        </div>

        <div class="col-12 col-sm-6 col-xl-3">
          <div class="kpi-card">
            <div>
              <div class="d-flex justify-content-between align-items-center">
                <span class="kpi-card-title">SẢN PHẨM ĐANG BÁN</span>
                <span class="status-badge status-badge-neutral">Trên tổng ${enrichedMeds.length}</span>
              </div>
              <div class="kpi-card-value tabular-nums">${sellableProducts.length} / ${enrichedMeds.length}</div>
              <div class="kpi-card-def">Sản phẩm đang mở bán và còn tồn kho khả dụng</div>
            </div>
            <a href="#/admin/products" class="small fw-semibold text-decoration-none" style="color:var(--color-primary);">Quản lý danh mục thuốc →</a>
          </div>
        </div>

        <div class="col-12 col-sm-6 col-xl-3">
          <div class="kpi-card">
            <div>
              <div class="d-flex justify-content-between align-items-center">
                <span class="kpi-card-title">CẢNH BÁO TỒN / HẠN LÔ</span>
                <span class="status-badge status-badge-warning">Cần kiểm tra</span>
              </div>
              <div class="kpi-card-value tabular-nums text-danger">${lowStockMeds.length} thuốc • ${batchAlerts.length} lô</div>
              <div class="kpi-card-def">Sản phẩm sắp hết tồn hoặc lô hàng cận hạn</div>
            </div>
            <a href="#/admin/inventory" class="small fw-semibold text-decoration-none" style="color:var(--color-primary);">Kiểm tra lô hàng &amp; nhập kho →</a>
          </div>
        </div>
      </div>

      <div class="row g-4 mb-4">
        <!-- Biểu đồ doanh thu đã thu (SVG gọn nhẹ, rõ số liệu) -->
        <div class="col-12 col-lg-5">
          <div class="admin-panel p-4 h-100">
            <h2 class="h6 fw-bold mb-1">Biểu đồ Doanh thu thực thu theo ngày</h2>
            <p class="small text-secondary mb-3">Chỉ thống kê các hóa đơn có trạng thái thanh toán <code>PAID</code></p>
            <div class="d-flex align-items-end justify-content-between gap-2 pt-3 pb-2 px-2 border-bottom" style="height:210px;">
              ${chartBars.map(b => {
                const hPct = Math.max(15, Math.round((b.val / maxVal) * 140));
                return `
                  <div class="d-flex flex-column align-items-center flex-grow-1">
                    <span class="small text-muted tabular-nums mb-1" style="font-size:11px;">${Math.round(b.val / 1000)}k</span>
                    <div class="w-100 rounded-top" style="max-width:38px;height:${hPct}px;background:var(--color-primary);" title="${b.label}: ${I18n.formatCurrency(b.val)}"></div>
                    <span class="small fw-medium mt-2 tabular-nums" style="font-size:12px;">${b.label}</span>
                  </div>
                `;
              }).join('')}
            </div>
            <div class="small text-muted mt-3">
              Tổng doanh thu đã thu trong kỳ: <strong class="text-dark tabular-nums">${I18n.formatCurrency(collectedRevenue)}</strong>
            </div>
          </div>
        </div>

        <!-- Bảng Cảnh báo Tồn thấp & Lô hạn gần -->
        <div class="col-12 col-lg-7">
          <div class="admin-panel p-4 h-100">
            <div class="d-flex justify-content-between align-items-center mb-3">
              <h2 class="h6 fw-bold mb-0">Cảnh báo Lô hàng sắp hết hạn / Đã khóa &amp; Tồn thấp</h2>
              <a href="#/admin/inventory" class="btn-ph btn-ph-secondary btn-ph-sm">Xử lý tại Kho/Lô</a>
            </div>
            <div class="admin-table-container">
              <table class="table-ph">
                <thead>
                  <tr>
                    <th>Tên thuốc</th>
                    <th>Số lô</th>
                    <th>Hạn dùng</th>
                    <th class="text-end">Tồn lô</th>
                    <th>Trạng thái lô</th>
                  </tr>
                </thead>
                <tbody>
                  ${batchAlerts.slice(0, 6).map(item => `
                    <tr>
                      <td class="fw-medium">${esc(item.med ? item.med.name : '')}</td>
                      <td class="font-mono-code">${esc(item.batch.batchNumber)}</td>
                      <td class="tabular-nums">${I18n.formatDate(item.batch.expiryDate)}</td>
                      <td class="text-end tabular-nums fw-semibold">${item.batch.stock}</td>
                      <td><span class="status-badge status-badge-${item.st.badgeType}">${esc(item.st.label)}</span></td>
                    </tr>
                  `).join('')}
                </tbody>
              </table>
            </div>
          </div>
        </div>
      </div>

      <!-- Bảng Đơn hàng gần nhất -->
      <div class="admin-panel p-4">
        <div class="d-flex justify-content-between align-items-center mb-3">
          <h2 class="h6 fw-bold mb-0">Đơn hàng gần nhất cần theo dõi</h2>
          <a href="#/admin/orders" class="btn-ph btn-ph-primary btn-ph-sm">Xem toàn bộ ${db.orders.length} đơn hàng</a>
        </div>
        <div class="admin-table-container">
          <table class="table-ph">
            <thead>
              <tr>
                <th>Mã đơn</th>
                <th>Khách hàng</th>
                <th>Ngày đặt</th>
                <th>Trạng thái đơn</th>
                <th>Thanh toán</th>
                <th>Giao hàng</th>
                <th class="text-end">Tổng tiền</th>
                <th class="text-end">Thao tác</th>
              </tr>
            </thead>
            <tbody>
              ${db.orders.slice(0, 5).map(o => `
                <tr>
                  <td class="font-mono-code fw-bold">${esc(o.code)}</td>
                  <td>${esc(o.customerName)} <span class="text-muted small">(${esc(o.customerPhone)})</span></td>
                  <td class="tabular-nums small">${I18n.formatDate(o.orderDate, true)}</td>
                  <td>${C.renderStatusBadge('order', o.orderStatus)}</td>
                  <td>${C.renderStatusBadge('payment', o.paymentStatus)}</td>
                  <td>${C.renderStatusBadge('shipping', o.shippingStatus)}</td>
                  <td class="text-end tabular-nums fw-bold">${I18n.formatCurrency(o.totalAmount)}</td>
                  <td class="text-end">
                    <button type="button" class="btn-ph btn-ph-secondary btn-ph-sm" onclick="PharmacyAdmin.openAdminOrderModal(${o.id})">Xử lý</button>
                  </td>
                </tr>
              `).join('')}
            </tbody>
          </table>
        </div>
      </div>
    `;
  }

  function changeDashPeriod(val) {
    dashPeriod = val;
    window.PharmacyApp.refreshCurrentRoute();
  }

  // ==================== 2. QUẢN LÝ SẢN PHẨM (#/admin/products) ====================
  function renderProductsSection(el) {
    const db = DB.getDb();
    let list = db.medicines.map(m => DB.enrichMedicine(m));

    if (prodFilter.q) {
      const q = prodFilter.q.toLowerCase();
      list = list.filter(m =>
        m.name.toLowerCase().includes(q) ||
        (m.activeIngredient && m.activeIngredient.toLowerCase().includes(q)) ||
        (m.barCode && m.barCode.toLowerCase().includes(q))
      );
    }
    if (prodFilter.categoryId) {
      list = list.filter(m => String(m.categoryId) === String(prodFilter.categoryId));
    }
    if (prodFilter.status === 'SELLING') list = list.filter(m => m.isActive && m.isSelling);
    else if (prodFilter.status === 'STOPPED') list = list.filter(m => m.isActive && !m.isSelling);
    else if (prodFilter.status === 'HIDDEN') list = list.filter(m => !m.isActive);
    else if (prodFilter.status === 'RX') list = list.filter(m => m.isPrescription);

    const totalItems = list.length;
    const totalPages = Math.max(1, Math.ceil(totalItems / prodFilter.pageSize));
    const safePage = Math.min(prodFilter.page, totalPages);
    const paged = list.slice((safePage - 1) * prodFilter.pageSize, safePage * prodFilter.pageSize);

    el.innerHTML = `
      <div class="d-flex flex-wrap align-items-center justify-content-between gap-2 mb-3">
        <div>
          <h1 class="h4 fw-bold mb-1">Quản lý Danh mục Sản phẩm &amp; Thuốc</h1>
          <p class="small text-secondary mb-0">Thêm/sửa thuốc, tải nhiều ảnh, soạn mô tả Rich Text và đổi trạng thái Đang bán / Ngừng bán / Đã ẩn</p>
        </div>
        <button type="button" class="btn-ph btn-ph-primary" onclick="PharmacyAdmin.openProductFormModal(null)">
          ${C.icon('plus', 16)} Thêm sản phẩm mới
        </button>
      </div>

      <!-- Bộ lọc bảng sản phẩm -->
      <div class="admin-panel p-3 mb-3">
        <div class="row g-2 align-items-end">
          <div class="col-12 col-md-4">
            <label for="admProdQ" class="form-label-ph">Tìm theo tên, hoạt chất, mã vạch</label>
            <input id="admProdQ" type="search" class="form-control-ph" placeholder="Nhập tên thuốc hoặc mã vạch..." value="${esc(prodFilter.q)}" oninput="PharmacyAdmin.updateProdFilter('q', this.value)" />
          </div>
          <div class="col-6 col-md-3">
            <label for="admProdCat" class="form-label-ph">Nhóm thuốc</label>
            <select id="admProdCat" class="form-select-ph" onchange="PharmacyAdmin.updateProdFilter('categoryId', this.value)">
              <option value="">Tất cả nhóm</option>
              ${db.categories.map(c => `<option value="${c.id}" ${String(prodFilter.categoryId) === String(c.id) ? 'selected' : ''}>${esc(c.name)}</option>`).join('')}
            </select>
          </div>
          <div class="col-6 col-md-3">
            <label for="admProdSt" class="form-label-ph">Trạng thái bán / Phân loại</label>
            <select id="admProdSt" class="form-select-ph" onchange="PharmacyAdmin.updateProdFilter('status', this.value)">
              <option value="ALL" ${prodFilter.status === 'ALL' ? 'selected' : ''}>Tất cả (${db.medicines.length})</option>
              <option value="SELLING" ${prodFilter.status === 'SELLING' ? 'selected' : ''}>Đang bán</option>
              <option value="STOPPED" ${prodFilter.status === 'STOPPED' ? 'selected' : ''}>Ngừng bán (IsSelling=0)</option>
              <option value="HIDDEN" ${prodFilter.status === 'HIDDEN' ? 'selected' : ''}>Đã ẩn (IsActive=0)</option>
              <option value="RX" ${prodFilter.status === 'RX' ? 'selected' : ''}>Thuốc cần đơn bác sĩ</option>
            </select>
          </div>
          <div class="col-12 col-md-2">
            <button type="button" class="btn-ph btn-ph-secondary w-100" onclick="PharmacyAdmin.resetProdFilter()">Xóa lọc</button>
          </div>
        </div>
      </div>

      <!-- Bảng sản phẩm -->
      <div class="admin-panel p-3">
        <div class="admin-table-container">
          <table class="table-ph">
            <thead>
              <tr>
                <th>Ảnh</th>
                <th>Tên thuốc &amp; Hoạt chất</th>
                <th>Nhóm</th>
                <th>Đơn vị</th>
                <th class="text-end">Giá bán</th>
                <th class="text-end">Tồn thực tế</th>
                <th class="text-end">Tồn có thể bán</th>
                <th>Trạng thái</th>
                <th class="text-end">Thao tác</th>
              </tr>
            </thead>
            <tbody>
              ${paged.length === 0 ? `
                <tr><td colspan="9" class="text-center py-4 text-secondary">Không có sản phẩm nào khớp điều kiện lọc.</td></tr>
              ` : paged.map(m => `
                <tr>
                  <td>
                    <img src="${m.primaryImage}" alt="" width="48" height="36" class="rounded border bg-light" style="object-fit:contain;" />
                  </td>
                  <td>
                    <div class="fw-semibold">${esc(m.name)} ${m.isPrescription ? `<span class="status-badge status-badge-info ms-1">Cần đơn bác sĩ</span>` : ''}</div>
                    <div class="small text-muted">${esc(m.activeIngredient || '—')} • <span class="font-mono-code">${esc(m.barCode || '')}</span></div>
                  </td>
                  <td>${esc(m.categoryName)}</td>
                  <td>${esc(m.unit)}</td>
                  <td class="text-end tabular-nums fw-semibold">${I18n.formatCurrency(m.price)}</td>
                  <td class="text-end tabular-nums">${m.totalStock}</td>
                  <td class="text-end tabular-nums fw-bold ${m.availableStock === 0 ? 'text-danger' : 'text-success'}">${m.availableStock}</td>
                  <td>
                    ${!m.isActive
                      ? `<span class="status-badge status-badge-neutral">Đã ẩn danh mục</span>`
                      : (m.isSelling
                          ? `<span class="status-badge status-badge-success">${C.icon('check', 12)} Đang bán</span>`
                          : `<span class="status-badge status-badge-danger">${C.icon('xCircle', 12)} Ngừng bán</span>`)}
                  </td>
                  <td class="text-end">
                    <div class="d-inline-flex gap-1">
                      <button type="button" class="btn-ph btn-ph-secondary btn-ph-sm" onclick="PharmacyAdmin.openProductFormModal(${m.id})">
                        ${C.icon('edit', 13)} Sửa
                      </button>
                      <button type="button" class="btn-ph btn-ph-secondary btn-ph-sm" onclick="PharmacyAdmin.confirmToggleSelling(${m.id})">
                        ${m.isSelling ? 'Ngừng bán' : 'Mở bán'}
                      </button>
                      <button type="button" class="btn-ph ${m.isActive ? 'btn-ph-danger-outline' : 'btn-ph-mint'} btn-ph-sm" onclick="PharmacyAdmin.confirmToggleActive(${m.id})">
                        ${m.isActive ? 'Ẩn' : 'Hiện lại'}
                      </button>
                    </div>
                  </td>
                </tr>
              `).join('')}
            </tbody>
          </table>
        </div>
        ${C.renderPagination({ page: safePage, totalPages, totalItems }, 'PharmacyAdmin.changeProdPage')}
      </div>
    `;
  }

  function updateProdFilter(k, v) {
    prodFilter[k] = v;
    prodFilter.page = 1;
    window.PharmacyApp.refreshCurrentRoute();
  }

  function resetProdFilter() {
    prodFilter = { q: '', categoryId: '', status: 'ALL', page: 1, pageSize: 7 };
    window.PharmacyApp.refreshCurrentRoute();
  }

  function changeProdPage(p) {
    prodFilter.page = Number(p) || 1;
    window.PharmacyApp.refreshCurrentRoute();
  }

  function confirmToggleSelling(medId) {
    const med = DB.getDb().medicines.find(m => m.id === Number(medId));
    if (!med) return;
    const nextState = !med.isSelling;
    C.openConfirmModal({
      title: `${nextState ? 'Mở bán lại' : 'Tạm ngừng bán'} sản phẩm`,
      message: nextState
        ? `Mở bán lại "${med.name}"? Tồn có thể bán từ các lô hợp lệ sẽ được khôi phục.`
        : `Tạm ngừng bán "${med.name}"? Theo quy tắc SELLING-STATUS, tồn thực tế vẫn giữ nguyên nhưng tồn có thể bán sẽ về 0.`,
      confirmText: nextState ? 'Mở bán lại' : 'Xác nhận ngừng bán',
      danger: !nextState,
      onConfirm: () => {
        med.isSelling = nextState;
        DB.saveDb();
        C.showToast(`Đã chuyển "${med.name}" sang trạng thái ${nextState ? 'Đang bán' : 'Ngừng bán'}.`);
        window.PharmacyApp.refreshCurrentRoute();
      }
    });
  }

  function confirmToggleActive(medId) {
    const med = DB.getDb().medicines.find(m => m.id === Number(medId));
    if (!med) return;
    const nextActive = !med.isActive;
    C.openConfirmModal({
      title: `${nextActive ? 'Hiện lại' : 'Ẩn'} danh mục thuốc`,
      message: nextActive
        ? `Hiện lại "${med.name}" trên danh sách tìm kiếm của khách hàng?`
        : `Ẩn "${med.name}" khỏi Storefront (Xóa mềm IsActive = false)? Lịch sử hóa đơn và tồn kho vẫn được giữ nguyên.`,
      confirmText: nextActive ? 'Hiện lại' : 'Ẩn sản phẩm',
      danger: !nextActive,
      onConfirm: () => {
        med.isActive = nextActive;
        DB.saveDb();
        C.showToast(`Đã ${nextActive ? 'hiện lại' : 'ẩn'} sản phẩm "${med.name}".`);
        window.PharmacyApp.refreshCurrentRoute();
      }
    });
  }

  // Form Modal Thêm / Sửa Sản phẩm có Upload nhiều ảnh & Rich Text Editor
  function openProductFormModal(medId) {
    const db = DB.getDb();
    const raw = medId ? db.medicines.find(m => m.id === Number(medId)) : null;
    const med = raw ? DB.enrichMedicine(raw) : {
      id: null,
      name: '',
      categoryId: db.categories[0].id,
      supplierId: db.suppliers[0].id,
      barCode: '8935001800999',
      activeIngredient: '',
      unit: 'Hộp',
      packagingSpec: 'Hộp 3 vỉ × 10 viên',
      price: 45000,
      isPrescription: false,
      isSelling: true,
      isActive: true,
      totalStock: 0,
      availableStock: 0,
      descriptionHtml: '<p><strong>Quy cách đóng gói:</strong> Hộp tiêu chuẩn.</p><p>Bảo quản nơi khô mát, dưới 30°C.</p>',
      images: []
    };

    editingProductImages = (med.images || []).map(img => Object.assign({}, img));

    C.openModal({
      title: med.id ? `Chỉnh sửa thuốc #${med.id}: ${med.name}` : 'Thêm sản phẩm / thuốc mới',
      size: 'lg',
      bodyHtml: `
        <form id="adminProductModalForm" onsubmit="PharmacyAdmin.handleSaveProductForm(event, ${med.id || 'null'})" novalidate>
          <div class="row g-3 mb-3">
            <div class="col-12 col-md-8">
              <label for="pfName" class="form-label-ph">Tên thuốc / sản phẩm <span class="text-danger">*</span></label>
              <input id="pfName" type="text" class="form-control-ph" required maxlength="150" value="${esc(med.name)}" placeholder="Ví dụ: Panadol Extra" />
              <div class="field-error-msg d-none" id="err_pfName"></div>
            </div>
            <div class="col-12 col-md-4">
              <label for="pfBarcode" class="form-label-ph">Mã vạch (BarCode)</label>
              <input id="pfBarcode" type="text" class="form-control-ph font-mono-code" maxlength="50" value="${esc(med.barCode || '')}" />
            </div>

            <div class="col-12 col-md-6">
              <label for="pfCategory" class="form-label-ph">Nhóm thuốc <span class="text-danger">*</span></label>
              <select id="pfCategory" class="form-select-ph" required>
                ${db.categories.map(c => `<option value="${c.id}" ${c.id === med.categoryId ? 'selected' : ''}>${esc(c.name)}</option>`).join('')}
              </select>
            </div>
            <div class="col-12 col-md-6">
              <label for="pfSupplier" class="form-label-ph">Nhà cung cấp <span class="text-danger">*</span></label>
              <select id="pfSupplier" class="form-select-ph" required>
                ${db.suppliers.map(s => `<option value="${s.id}" ${s.id === med.supplierId ? 'selected' : ''}>${esc(s.name)}</option>`).join('')}
              </select>
            </div>

            <div class="col-12 col-md-6">
              <label for="pfIngredient" class="form-label-ph">Hoạt chất hàm lượng</label>
              <input id="pfIngredient" type="text" class="form-control-ph" maxlength="150" value="${esc(med.activeIngredient || '')}" placeholder="Ví dụ: Paracetamol 500mg" />
            </div>
            <div class="col-6 col-md-3">
              <label for="pfUnit" class="form-label-ph">Đơn vị bán <span class="text-danger">*</span></label>
              <input id="pfUnit" type="text" class="form-control-ph" required maxlength="30" value="${esc(med.unit)}" placeholder="Hộp / Tuýp / Chai" />
            </div>
            <div class="col-6 col-md-3">
              <label for="pfPrice" class="form-label-ph">Giá bán (₫) <span class="text-danger">*</span></label>
              <input id="pfPrice" type="number" class="form-control-ph tabular-nums" required min="1000" step="500" value="${med.price}" />
              <div class="field-error-msg d-none" id="err_pfPrice"></div>
            </div>

            <div class="col-12 col-md-6">
              <label for="pfSpec" class="form-label-ph">Quy cách đóng gói trưng bày</label>
              <input id="pfSpec" type="text" class="form-control-ph" value="${esc(med.packagingSpec || '')}" />
            </div>
            ${med.id ? `
              <div class="col-6 col-md-3">
                <label for="pfTotalStockRo" class="form-label-ph">Tồn thực tế (Chỉ đọc)</label>
                <input id="pfTotalStockRo" type="text" class="form-control-ph tabular-nums" readonly value="${med.totalStock} ${esc(med.unit)}" />
              </div>
              <div class="col-6 col-md-3">
                <label for="pfAvailStockRo" class="form-label-ph">Tồn có thể bán (Chỉ đọc)</label>
                <input id="pfAvailStockRo" type="text" class="form-control-ph tabular-nums" readonly value="${med.availableStock} ${esc(med.unit)}" />
              </div>
            ` : `
              <div class="col-6 col-md-3">
                <label for="pfInitStock" class="form-label-ph">Số lượng lô khởi tạo</label>
                <input id="pfInitStock" type="number" class="form-control-ph tabular-nums" value="30" min="0" />
              </div>
              <div class="col-6 col-md-3">
                <label for="pfInitBatch" class="form-label-ph">Mã lô khởi tạo</label>
                <input id="pfInitBatch" type="text" class="form-control-ph font-mono-code" value="PH26-NEW-01" />
              </div>
            `}
          </div>

          <!-- Cờ trạng thái -->
          <div class="p-3 rounded-3 border bg-light mb-3 d-flex flex-wrap gap-4">
            <div class="form-check">
              <input class="form-check-input" type="checkbox" id="pfIsPrescription" ${med.isPrescription ? 'checked' : ''} />
              <label class="form-check-label fw-semibold" for="pfIsPrescription">Thuốc cần đơn bác sĩ (Yêu cầu gửi đơn tư vấn)</label>
            </div>
            <div class="form-check">
              <input class="form-check-input" type="checkbox" id="pfIsSelling" ${med.isSelling ? 'checked' : ''} />
              <label class="form-check-label fw-semibold" for="pfIsSelling">Đang mở bán (IsSelling)</label>
            </div>
            <div class="form-check">
              <input class="form-check-input" type="checkbox" id="pfIsActive" ${med.isActive ? 'checked' : ''} />
              <label class="form-check-label fw-semibold" for="pfIsActive">Hiển thị danh mục (IsActive)</label>
            </div>
          </div>

          <!-- Upload nhiều ảnh, preview & chọn ảnh chính -->
          <div class="mb-3">
            <label for="pfImageInput" class="form-label-ph">Hình ảnh sản phẩm (Hỗ trợ tải nhiều ảnh, chọn ảnh đại diện chính)</label>
            <input id="pfImageInput" type="file" accept="image/jpeg,image/png,image/webp,image/svg+xml" multiple class="form-control-ph" onchange="PharmacyAdmin.handleProductImagesSelected(this)" />
            <div class="field-error-msg d-none" id="err_pfImageInput"></div>
            <div id="pfImagesPreviewGrid" class="row g-2 mt-1"></div>
          </div>

          <!-- Rich Text Editor cho mô tả sản phẩm -->
          <div class="mb-2">
            <label class="form-label-ph mb-1">Mô tả sản phẩm &amp; Hướng dẫn bảo quản</label>
            <div class="rte-container">
              <div class="rte-toolbar" role="toolbar" aria-label="Thanh công cụ định dạng văn bản">
                <button type="button" class="rte-btn" onclick="PharmacyAdmin.execRte('bold')"><strong>B</strong> Đậm</button>
                <button type="button" class="rte-btn" onclick="PharmacyAdmin.execRte('italic')"><em>I</em> Nghiêng</button>
                <button type="button" class="rte-btn" onclick="PharmacyAdmin.execRte('insertUnorderedList')">• Danh sách</button>
                <button type="button" class="rte-btn" onclick="PharmacyAdmin.execRte('insertOrderedList')">1. Đánh số</button>
                <button type="button" class="rte-btn" onclick="PharmacyAdmin.execRte('formatBlock', 'H3')">Tiêu đề H3</button>
                <button type="button" class="rte-btn" onclick="PharmacyAdmin.execRte('removeFormat')">Xóa định dạng</button>
              </div>
              <div id="pfRichEditor" class="rte-editor-area" contenteditable="true" role="textbox" aria-multiline="true" aria-label="Nội dung mô tả sản phẩm">
                ${med.descriptionHtml || ''}
              </div>
            </div>
          </div>
        </form>
      `,
      footerHtml: `
        <button type="button" class="btn-ph btn-ph-secondary" onclick="PharmacyComponents.closeModal()">Hủy thao tác</button>
        <button type="submit" form="adminProductModalForm" class="btn-ph btn-ph-primary">${C.icon('check', 16)} Lưu sản phẩm</button>
      `,
      onMount: () => {
        renderProductImagesPreviewGrid();
      }
    });
  }

  function renderProductImagesPreviewGrid() {
    const grid = document.getElementById('pfImagesPreviewGrid');
    if (!grid) return;
    if (editingProductImages.length === 0) {
      grid.innerHTML = `<div class="col-12 small text-muted">Chưa có ảnh riêng; hệ thống sẽ tự dùng ảnh hộp thuốc chuẩn SVG theo tên thuốc.</div>`;
      return;
    }
    grid.innerHTML = editingProductImages.map((img, idx) => `
      <div class="col-6 col-sm-3">
        <div class="p-2 rounded border ${img.isPrimary ? 'border-teal bg-light' : 'bg-white'}">
          <img src="${img.url}" alt="${esc(img.name)}" class="w-100 rounded mb-1" style="height:76px;object-fit:contain;" />
          <div class="small text-truncate mb-1">${esc(img.name)}</div>
          <div class="d-flex justify-content-between align-items-center">
            ${img.isPrimary
              ? `<span class="status-badge status-badge-success" style="font-size:10px;padding:1px 6px;">Ảnh chính</span>`
              : `<button type="button" class="btn btn-sm btn-link p-0 text-decoration-none" style="font-size:11px;" onclick="PharmacyAdmin.setPrimaryProductImage(${idx})">Đặt làm chính</button>`}
            <button type="button" class="btn btn-sm btn-link text-danger p-0 text-decoration-none" style="font-size:11px;" onclick="PharmacyAdmin.removeProductImage(${idx})">Xóa</button>
          </div>
        </div>
      </div>
    `).join('');
  }

  function handleProductImagesSelected(input) {
    const errEl = document.getElementById('err_pfImageInput');
    if (errEl) errEl.classList.add('d-none');
    const files = Array.from(input.files || []);
    files.forEach(file => {
      if (!file.type.startsWith('image/')) {
        if (errEl) {
          errEl.textContent = `File "${file.name}" không đúng định dạng ảnh (JPG/PNG/WEBP).`;
          errEl.classList.remove('d-none');
        }
        return;
      }
      if (file.size > 5 * 1024 * 1024) {
        if (errEl) {
          errEl.textContent = `File "${file.name}" vượt quá kích thước 5MB.`;
          errEl.classList.remove('d-none');
        }
        return;
      }
      const reader = new FileReader();
      reader.onload = (ev) => {
        editingProductImages.push({
          id: Date.now() + Math.random(),
          name: file.name,
          url: ev.target.result,
          isPrimary: editingProductImages.length === 0
        });
        renderProductImagesPreviewGrid();
      };
      reader.readAsDataURL(file);
    });
  }

  function generateExtraProductImage() {
    const nameVal = (document.getElementById('pfName')?.value || 'Thuốc mới').trim();
    const unitVal = (document.getElementById('pfUnit')?.value || 'Hộp').trim();
    const isRx = Boolean(document.getElementById('pfIsPrescription')?.checked);
    const num = editingProductImages.length + 1;
    const url = I18n.generateMedicineSvgDataUrl({
      name: `${nameVal} (Mặt ${num})`,
      activeIngredient: document.getElementById('pfIngredient')?.value || 'Ảnh minh họa góc hộp',
      unit: unitVal,
      barCode: document.getElementById('pfBarcode')?.value || '89350018',
      isPrescription: isRx
    });
    editingProductImages.push({
      id: Date.now(),
      name: `anh-hop-goc-${num}.svg`,
      url,
      isPrimary: editingProductImages.length === 0
    });
    renderProductImagesPreviewGrid();
  }

  function setPrimaryProductImage(idx) {
    editingProductImages.forEach((img, i) => { img.isPrimary = (i === idx); });
    renderProductImagesPreviewGrid();
  }

  function removeProductImage(idx) {
    editingProductImages.splice(idx, 1);
    if (editingProductImages.length > 0 && !editingProductImages.some(i => i.isPrimary)) {
      editingProductImages[0].isPrimary = true;
    }
    renderProductImagesPreviewGrid();
  }

  function execRte(cmd, val = null) {
    const editor = document.getElementById('pfRichEditor');
    if (!editor) return;
    editor.focus();
    document.execCommand(cmd, false, val);
  }

  function insertSampleScriptToTestSanitizer() {
    const editor = document.getElementById('pfRichEditor');
    if (!editor) return;
    editor.innerHTML += `<p>Đoạn văn kiểm thử bảo mật (Thẻ script phía dưới sẽ bị xóa sạch khi bấm Lưu):</p><script>alert('XSS');</script>`;
    C.showToast('Đã chèn thử chuỗi chứa <script>. Khi lưu, bộ lọc HTML Sanitizer sẽ tự động loại bỏ script.', 'warning');
  }

  function handleSaveProductForm(e, medId) {
    e.preventDefault();
    const name = document.getElementById('pfName').value.trim();
    const price = Number(document.getElementById('pfPrice').value);
    const errName = document.getElementById('err_pfName');
    const errPrice = document.getElementById('err_pfPrice');
    errName.classList.add('d-none');
    errPrice.classList.add('d-none');

    if (name.length < 2) {
      errName.textContent = 'Vui lòng nhập tên sản phẩm (tối thiểu 2 ký tự).';
      errName.classList.remove('d-none');
      document.getElementById('pfName').focus();
      return;
    }
    if (!price || price < 1000) {
      errPrice.textContent = 'Giá bán phải từ 1.000 ₫ trở lên.';
      errPrice.classList.remove('d-none');
      document.getElementById('pfPrice').focus();
      return;
    }

    const payload = {
      id: medId,
      name,
      barCode: document.getElementById('pfBarcode').value.trim(),
      categoryId: Number(document.getElementById('pfCategory').value),
      supplierId: Number(document.getElementById('pfSupplier').value),
      activeIngredient: document.getElementById('pfIngredient').value.trim(),
      unit: document.getElementById('pfUnit').value.trim() || 'Hộp',
      packagingSpec: document.getElementById('pfSpec').value.trim(),
      price,
      isPrescription: document.getElementById('pfIsPrescription').checked,
      isSelling: document.getElementById('pfIsSelling').checked,
      isActive: document.getElementById('pfIsActive').checked,
      descriptionHtml: document.getElementById('pfRichEditor').innerHTML,
      images: editingProductImages,
      initialBatchStock: document.getElementById('pfInitStock') ? Number(document.getElementById('pfInitStock').value) : 0,
      initialBatchNumber: document.getElementById('pfInitBatch') ? document.getElementById('pfInitBatch').value.trim() : 'PH26-NEW'
    };

    API.saveAdminProduct(payload).then(saved => {
      C.closeModal();
      C.showToast(`Đã lưu sản phẩm "${saved.name}" thành công! Dữ liệu đã đồng bộ lên Storefront.`);
      window.PharmacyApp.refreshCurrentRoute();
    });
  }

  // ==================== 3. NHÓM THUỐC & NHÀ CUNG CẤP (#/admin/categories-suppliers) ====================
  function renderCategoriesSuppliersSection(el) {
    const db = DB.getDb();

    el.innerHTML = `
      <div class="mb-3">
        <h1 class="h4 fw-bold mb-1">Quản lý Nhóm thuốc &amp; Nhà cung cấp</h1>
        <p class="small text-secondary mb-0">Kiểm tra toàn vẹn tham chiếu: Không cho phép xóa cứng Nhóm hoặc Nhà cung cấp khi đang có Thuốc / Phiếu nhập liên kết.</p>
      </div>

      <div class="row g-4">
        <!-- Cột Nhóm thuốc -->
        <div class="col-12 col-lg-6">
          <div class="admin-panel p-4 h-100">
            <div class="d-flex justify-content-between align-items-center mb-3">
              <h2 class="h6 fw-bold mb-0">Danh mục Nhóm thuốc (${db.categories.length})</h2>
              <button type="button" class="btn-ph btn-ph-primary btn-ph-sm" onclick="PharmacyAdmin.openCategoryModal(null)">
                ${C.icon('plus', 14)} Thêm nhóm
              </button>
            </div>
            <div class="admin-table-container">
              <table class="table-ph">
                <thead>
                  <tr><th>Mã</th><th>Tên nhóm thuốc</th><th class="text-end">Số thuốc</th><th class="text-end">Thao tác</th></tr>
                </thead>
                <tbody>
                  ${db.categories.map(cat => {
                    const count = db.medicines.filter(m => m.categoryId === cat.id).length;
                    return `
                      <tr>
                        <td class="font-mono-code">#${cat.id}</td>
                        <td>
                          <div class="fw-semibold">${esc(cat.name)}</div>
                          <div class="small text-muted">${esc(cat.description || '')}</div>
                        </td>
                        <td class="text-end tabular-nums fw-semibold">${count}</td>
                        <td class="text-end">
                          <button type="button" class="btn-ph btn-ph-secondary btn-ph-sm" onclick="PharmacyAdmin.openCategoryModal(${cat.id})">Sửa</button>
                          <button type="button" class="btn-ph btn-ph-danger-outline btn-ph-sm" onclick="PharmacyAdmin.confirmDeleteCategory(${cat.id})">Xóa</button>
                        </td>
                      </tr>
                    `;
                  }).join('')}
                </tbody>
              </table>
            </div>
          </div>
        </div>

        <!-- Cột Nhà cung cấp -->
        <div class="col-12 col-lg-6">
          <div class="admin-panel p-4 h-100">
            <div class="d-flex justify-content-between align-items-center mb-3">
              <h2 class="h6 fw-bold mb-0">Danh sách Nhà cung cấp (${db.suppliers.length})</h2>
              <button type="button" class="btn-ph btn-ph-primary btn-ph-sm" onclick="PharmacyAdmin.openSupplierModal(null)">
                ${C.icon('plus', 14)} Thêm NCC
              </button>
            </div>
            <div class="admin-table-container">
              <table class="table-ph">
                <thead>
                  <tr><th>Tên nhà cung cấp</th><th>Điện thoại</th><th class="text-end">Số thuốc</th><th class="text-end">Thao tác</th></tr>
                </thead>
                <tbody>
                  ${db.suppliers.map(sup => {
                    const count = db.medicines.filter(m => m.supplierId === sup.id).length;
                    return `
                      <tr>
                        <td>
                          <div class="fw-semibold">${esc(sup.name)}</div>
                          <div class="small text-muted">${esc(sup.address || '')}</div>
                        </td>
                        <td class="tabular-nums">${esc(sup.phone)}</td>
                        <td class="text-end tabular-nums fw-semibold">${count}</td>
                        <td class="text-end">
                          <button type="button" class="btn-ph btn-ph-secondary btn-ph-sm" onclick="PharmacyAdmin.openSupplierModal(${sup.id})">Sửa</button>
                          <button type="button" class="btn-ph btn-ph-danger-outline btn-ph-sm" onclick="PharmacyAdmin.confirmDeleteSupplier(${sup.id})">Xóa</button>
                        </td>
                      </tr>
                    `;
                  }).join('')}
                </tbody>
              </table>
            </div>
          </div>
        </div>
      </div>
    `;
  }

  function openCategoryModal(catId) {
    const db = DB.getDb();
    const cat = catId ? db.categories.find(c => c.id === Number(catId)) : { id: null, name: '', description: '' };
    C.openModal({
      title: cat.id ? `Sửa nhóm thuốc #${cat.id}` : 'Thêm nhóm thuốc mới',
      size: 'sm',
      bodyHtml: `
        <form id="catForm" onsubmit="PharmacyAdmin.saveCategory(event, ${cat.id || 'null'})">
          <div class="mb-3">
            <label for="catNameInp" class="form-label-ph">Tên nhóm thuốc <span class="text-danger">*</span></label>
            <input id="catNameInp" type="text" class="form-control-ph" required value="${esc(cat.name)}" />
          </div>
          <div class="mb-2">
            <label for="catDescInp" class="form-label-ph">Mô tả ngắn</label>
            <input id="catDescInp" type="text" class="form-control-ph" value="${esc(cat.description || '')}" />
          </div>
        </form>
      `,
      footerHtml: `
        <button type="button" class="btn-ph btn-ph-secondary" onclick="PharmacyComponents.closeModal()">Hủy</button>
        <button type="submit" form="catForm" class="btn-ph btn-ph-primary">Lưu nhóm</button>
      `
    });
  }

  function saveCategory(e, catId) {
    e.preventDefault();
    const name = document.getElementById('catNameInp').value.trim();
    const description = document.getElementById('catDescInp').value.trim();
    if (!name) return;
    const db = DB.getDb();
    if (catId) {
      const cat = db.categories.find(c => c.id === Number(catId));
      if (cat) { cat.name = name; cat.description = description; }
    } else {
      const nextId = db.categories.reduce((m, c) => Math.max(m, c.id), 6) + 1;
      db.categories.push({ id: nextId, name, slug: 'nhom-' + nextId, description });
    }
    DB.saveDb();
    C.closeModal();
    C.showToast(`Đã lưu nhóm thuốc "${name}".`);
    window.PharmacyApp.refreshCurrentRoute();
  }

  function confirmDeleteCategory(catId) {
    const db = DB.getDb();
    const cat = db.categories.find(c => c.id === Number(catId));
    if (!cat) return;
    const linkedMeds = db.medicines.filter(m => m.categoryId === cat.id);
    if (linkedMeds.length > 0) {
      C.openModal({
        title: 'Không thể xóa nhóm thuốc đang sử dụng',
        size: 'sm',
        bodyHtml: `
          <div class="p-3 rounded-2 mb-2" style="background:var(--color-danger-bg);color:var(--color-danger);border:1px solid var(--color-danger-border);">
            <strong>Ràng buộc dữ liệu liên quan:</strong> Nhóm <strong>"${esc(cat.name)}"</strong> hiện đang có <strong>${linkedMeds.length} sản phẩm</strong> trực thuộc (ví dụ: ${linkedMeds.slice(0, 2).map(m => esc(m.name)).join(', ')}).
          </div>
          <p class="small text-secondary mb-0">Vui lòng chuyển các thuốc sang nhóm khác hoặc chỉ đổi tên nhóm. Hệ thống không xóa cứng nhóm đang có dữ liệu tham chiếu.</p>
        `,
        footerHtml: `<button type="button" class="btn-ph btn-ph-primary" onclick="PharmacyComponents.closeModal()">Đã hiểu</button>`
      });
      return;
    }

    C.openConfirmModal({
      title: 'Xác nhận xóa nhóm thuốc',
      message: `Nhóm "${cat.name}" chưa có sản phẩm nào liên kết. Bạn có chắc chắn muốn xóa?`,
      confirmText: 'Xóa nhóm',
      danger: true,
      onConfirm: () => {
        db.categories = db.categories.filter(c => c.id !== cat.id);
        DB.saveDb();
        C.showToast(`Đã xóa nhóm "${cat.name}".`);
        window.PharmacyApp.refreshCurrentRoute();
      }
    });
  }

  function openSupplierModal(supId) {
    const db = DB.getDb();
    const sup = supId ? db.suppliers.find(s => s.id === Number(supId)) : { id: null, name: '', phone: '', address: '' };
    C.openModal({
      title: sup.id ? `Sửa nhà cung cấp #${sup.id}` : 'Thêm nhà cung cấp mới',
      size: 'sm',
      bodyHtml: `
        <form id="supForm" onsubmit="PharmacyAdmin.saveSupplier(event, ${sup.id || 'null'})">
          <div class="mb-3">
            <label for="supNameInp" class="form-label-ph">Tên nhà cung cấp <span class="text-danger">*</span></label>
            <input id="supNameInp" type="text" class="form-control-ph" required value="${esc(sup.name)}" />
          </div>
          <div class="mb-3">
            <label for="supPhoneInp" class="form-label-ph">Số điện thoại <span class="text-danger">*</span></label>
            <input id="supPhoneInp" type="tel" class="form-control-ph" required value="${esc(sup.phone)}" />
          </div>
          <div class="mb-2">
            <label for="supAddrInp" class="form-label-ph">Địa chỉ</label>
            <input id="supAddrInp" type="text" class="form-control-ph" value="${esc(sup.address || '')}" />
          </div>
        </form>
      `,
      footerHtml: `
        <button type="button" class="btn-ph btn-ph-secondary" onclick="PharmacyComponents.closeModal()">Hủy</button>
        <button type="submit" form="supForm" class="btn-ph btn-ph-primary">Lưu nhà cung cấp</button>
      `
    });
  }

  function saveSupplier(e, supId) {
    e.preventDefault();
    const name = document.getElementById('supNameInp').value.trim();
    const phone = document.getElementById('supPhoneInp').value.trim();
    const address = document.getElementById('supAddrInp').value.trim();
    if (!name || !phone) return;
    const db = DB.getDb();
    if (supId) {
      const sup = db.suppliers.find(s => s.id === Number(supId));
      if (sup) { sup.name = name; sup.phone = phone; sup.address = address; }
    } else {
      const nextId = db.suppliers.reduce((m, s) => Math.max(m, s.id), 5) + 1;
      db.suppliers.push({ id: nextId, name, phone, address });
    }
    DB.saveDb();
    C.closeModal();
    C.showToast(`Đã lưu nhà cung cấp "${name}".`);
    window.PharmacyApp.refreshCurrentRoute();
  }

  function confirmDeleteSupplier(supId) {
    const db = DB.getDb();
    const sup = db.suppliers.find(s => s.id === Number(supId));
    if (!sup) return;
    const linkedMeds = db.medicines.filter(m => m.supplierId === sup.id);
    const linkedStockIns = db.stockIns.filter(s => s.supplierId === sup.id);

    if (linkedMeds.length > 0 || linkedStockIns.length > 0) {
      C.openModal({
        title: 'Không thể xóa Nhà cung cấp đang có dữ liệu liên kết',
        size: 'sm',
        bodyHtml: `
          <div class="p-3 rounded-2 mb-2" style="background:var(--color-danger-bg);color:var(--color-danger);border:1px solid var(--color-danger-border);">
            Nhà cung cấp <strong>"${esc(sup.name)}"</strong> đang gắn với <strong>${linkedMeds.length} thuốc</strong> và <strong>${linkedStockIns.length} phiếu nhập kho</strong>.
          </div>
          <p class="small text-secondary mb-0">Để bảo toàn lịch sử chứng từ nhập kho và nguồn gốc lô thuốc, hệ thống chặn thao tác xóa nhà cung cấp này.</p>
        `,
        footerHtml: `<button type="button" class="btn-ph btn-ph-primary" onclick="PharmacyComponents.closeModal()">Đã hiểu</button>`
      });
      return;
    }

    C.openConfirmModal({
      title: 'Xác nhận xóa Nhà cung cấp',
      message: `Xóa nhà cung cấp "${sup.name}"?`,
      confirmText: 'Xóa',
      danger: true,
      onConfirm: () => {
        db.suppliers = db.suppliers.filter(s => s.id !== sup.id);
        DB.saveDb();
        C.showToast(`Đã xóa nhà cung cấp "${sup.name}".`);
        window.PharmacyApp.refreshCurrentRoute();
      }
    });
  }

  // ==================== 4. KHO / LÔ HÀNG & PHIẾU NHẬP (#/admin/inventory) ====================
  function renderInventorySection(el) {
    const db = DB.getDb();

    const batchRows = db.batches.map(b => {
      const rawMed = db.medicines.find(m => m.id === b.medicineId);
      const med = DB.enrichMedicine(rawMed);
      const st = DB.computeBatchStatus(b, rawMed);
      return { batch: b, med, st, sellableOfBatch: st.canSell ? b.stock : 0 };
    });

    let filteredBatches = batchRows;
    if (batchFilter.q) {
      const q = batchFilter.q.toLowerCase();
      filteredBatches = filteredBatches.filter(r =>
        (r.med && r.med.name.toLowerCase().includes(q)) ||
        r.batch.batchNumber.toLowerCase().includes(q)
      );
    }
    if (batchFilter.state === 'SELLABLE') filteredBatches = filteredBatches.filter(r => r.st.canSell);
    else if (batchFilter.state === 'NEAR_EXPIRY') filteredBatches = filteredBatches.filter(r => r.st.isNearExpiry);
    else if (batchFilter.state === 'EXPIRED') filteredBatches = filteredBatches.filter(r => r.st.isExpired);
    else if (batchFilter.state === 'BLOCKED') filteredBatches = filteredBatches.filter(r => r.batch.isBlocked);

    const draftLines = db.stockInDraft || [];
    const draftTotalCost = draftLines.reduce((s, l) => s + (Number(l.quantity) || 0) * (Number(l.unitCost) || 0), 0);

    el.innerHTML = `
      <div class="d-flex flex-wrap align-items-center justify-content-between gap-2 mb-3">
        <div>
          <h1 class="h4 fw-bold mb-1">Quản lý Tồn kho theo Lô &amp; Phiếu nhập kho</h1>
          <p class="small text-secondary mb-0">
            Tồn có thể bán chỉ cộng các lô chưa khóa, còn hạn của thuốc Đang bán. Quy tắc xuất kho: <strong>FEFO (Lô hết hạn trước xuất trước)</strong>.
          </p>
        </div>
        <div class="d-flex gap-2">
          <button type="button" class="btn-ph ${invTab === 'batches' ? 'btn-ph-primary' : 'btn-ph-secondary'}" onclick="PharmacyAdmin.switchInvTab('batches')">
            1. Danh sách Lô hàng (${db.batches.length})
          </button>
          <button type="button" class="btn-ph ${invTab === 'stockin' ? 'btn-ph-primary' : 'btn-ph-secondary'}" onclick="PharmacyAdmin.switchInvTab('stockin')">
            2. Phiếu nhập kho &amp; Nhập nháp (${db.stockIns.length})
          </button>
        </div>
      </div>

      ${invTab === 'batches' ? `
        <div class="admin-panel p-3 mb-3">
          <div class="row g-2 align-items-end">
            <div class="col-12 col-md-5">
              <label for="batchQ" class="form-label-ph">Tìm tên thuốc hoặc số lô</label>
              <input id="batchQ" type="search" class="form-control-ph" placeholder="VD: Panadol, LOT-PND-2024A, PH26..." value="${esc(batchFilter.q)}" oninput="PharmacyAdmin.updateBatchFilter('q', this.value)" />
            </div>
            <div class="col-12 col-md-5">
              <label for="batchStSel" class="form-label-ph">Lọc theo tình trạng lô</label>
              <select id="batchStSel" class="form-select-ph" onchange="PharmacyAdmin.updateBatchFilter('state', this.value)">
                <option value="ALL" ${batchFilter.state === 'ALL' ? 'selected' : ''}>Tất cả lô (${batchRows.length})</option>
                <option value="SELLABLE" ${batchFilter.state === 'SELLABLE' ? 'selected' : ''}>Được xuất bán</option>
                <option value="NEAR_EXPIRY" ${batchFilter.state === 'NEAR_EXPIRY' ? 'selected' : ''}>Sắp hết hạn (&le; 90 ngày)</option>
                <option value="EXPIRED" ${batchFilter.state === 'EXPIRED' ? 'selected' : ''}>Đã hết hạn</option>
                <option value="BLOCKED" ${batchFilter.state === 'BLOCKED' ? 'selected' : ''}>Đang tạm khóa (IsBlocked=1)</option>
              </select>
            </div>
            <div class="col-12 col-md-2">
              <button type="button" class="btn-ph btn-ph-secondary w-100" onclick="PharmacyAdmin.updateBatchFilter('state', 'ALL');PharmacyAdmin.updateBatchFilter('q', '')">Xóa lọc</button>
            </div>
          </div>
        </div>

        <div class="admin-panel p-3">
          <div class="admin-table-container">
            <table class="table-ph">
              <thead>
                <tr>
                  <th>Tên thuốc</th>
                  <th>Số lô (BatchNumber)</th>
                  <th>Hạn dùng</th>
                  <th class="text-end">Tồn lô thực tế</th>
                  <th class="text-end">Tồn có thể bán</th>
                  <th>Trạng thái thực tế</th>
                  <th class="text-end">Khóa / Mở khóa lô</th>
                </tr>
              </thead>
              <tbody>
                ${filteredBatches.map(r => `
                  <tr>
                    <td>
                      <div class="fw-semibold">${esc(r.med ? r.med.name : '')}</div>
                      <div class="small text-muted">Tổng tồn thuốc: ${r.med ? r.med.totalStock : 0} • Có thể bán: <strong>${r.med ? r.med.availableStock : 0}</strong></div>
                    </td>
                    <td class="font-mono-code fw-bold">${esc(r.batch.batchNumber)}</td>
                    <td class="tabular-nums">${I18n.formatDate(r.batch.expiryDate)}</td>
                    <td class="text-end tabular-nums">${r.batch.stock}</td>
                    <td class="text-end tabular-nums fw-bold ${r.sellableOfBatch > 0 ? 'text-success' : 'text-danger'}">${r.sellableOfBatch}</td>
                    <td><span class="status-badge status-badge-${r.st.badgeType}">${esc(r.st.label)}</span></td>
                    <td class="text-end">
                      <button type="button" class="btn-ph ${r.batch.isBlocked ? 'btn-ph-mint' : 'btn-ph-danger-outline'} btn-ph-sm" onclick="PharmacyAdmin.confirmToggleBatchLock(${r.batch.id})">
                        ${r.batch.isBlocked ? `${C.icon('unlock', 13)} Mở khóa lô` : `${C.icon('lock', 13)} Tạm khóa lô`}
                      </button>
                    </td>
                  </tr>
                `).join('')}
              </tbody>
            </table>
          </div>
        </div>
      ` : `
        <div class="row g-4">
          <!-- Lập phiếu nhập kho nháp nhiều dòng -->
          <div class="col-12 col-xl-7">
            <div class="admin-panel p-4">
              <h2 class="h6 fw-bold mb-1">Lập Phiếu nhập kho mới (Nhiều dòng hàng)</h2>
              <p class="small text-secondary mb-3">Khi bấm Ghi sổ Phiếu nhập, hệ thống sẽ cộng tồn vào lô tương ứng và cập nhật ngay Tồn có thể bán.</p>

              <!-- Form thêm dòng vào phiếu nháp -->
              <form onsubmit="PharmacyAdmin.addDraftStockInLine(event)" class="p-3 rounded-3 border bg-light mb-3">
                <div class="row g-2 align-items-end">
                  <div class="col-12 col-md-5">
                    <label for="siMedId" class="form-label-ph">Chọn thuốc nhập</label>
                    <select id="siMedId" class="form-select-ph" required>
                      ${db.medicines.map(m => `<option value="${m.id}">${esc(m.name)} (${esc(m.unit)})</option>`).join('')}
                    </select>
                  </div>
                  <div class="col-6 col-md-4">
                    <label for="siBatchNo" class="form-label-ph">Số lô</label>
                    <input id="siBatchNo" type="text" class="form-control-ph font-mono-code" required value="PH26-IMP-01" />
                  </div>
                  <div class="col-6 col-md-3">
                    <label for="siExpiry" class="form-label-ph">Hạn dùng</label>
                    <input id="siExpiry" type="date" class="form-control-ph" required value="2028-06-30" />
                  </div>
                  <div class="col-6 col-md-4">
                    <label for="siQty" class="form-label-ph">Số lượng nhập</label>
                    <input id="siQty" type="number" class="form-control-ph tabular-nums" required min="1" value="25" />
                  </div>
                  <div class="col-6 col-md-4">
                    <label for="siCost" class="form-label-ph">Giá vốn / đơn vị (₫)</label>
                    <input id="siCost" type="number" class="form-control-ph tabular-nums" required min="500" step="500" value="32000" />
                  </div>
                  <div class="col-12 col-md-4">
                    <button type="submit" class="btn-ph btn-ph-mint w-100">${C.icon('plus', 15)} Thêm dòng</button>
                  </div>
                </div>
              </form>

              <!-- Bảng các dòng trong phiếu nháp -->
              <div class="admin-table-container mb-3">
                <table class="table-ph">
                  <thead>
                    <tr><th>Thuốc</th><th>Số lô</th><th>Hạn dùng</th><th class="text-end">SL</th><th class="text-end">Giá vốn</th><th class="text-end">Thành tiền</th><th></th></tr>
                  </thead>
                  <tbody>
                    ${draftLines.length === 0 ? `
                      <tr><td colspan="7" class="text-center py-3 text-muted">Phiếu nhập nháp đang trống. Hãy thêm dòng thuốc ở trên.</td></tr>
                    ` : draftLines.map((l, idx) => {
                      const m = db.medicines.find(x => x.id === Number(l.medicineId));
                      return `
                        <tr>
                          <td class="fw-medium">${esc(m ? m.name : '')}</td>
                          <td class="font-mono-code">${esc(l.batchNumber)}</td>
                          <td class="tabular-nums">${I18n.formatDate(l.expiryDate)}</td>
                          <td class="text-end tabular-nums">${l.quantity}</td>
                          <td class="text-end tabular-nums">${I18n.formatCurrency(l.unitCost)}</td>
                          <td class="text-end tabular-nums fw-bold">${I18n.formatCurrency(l.quantity * l.unitCost)}</td>
                          <td class="text-end">
                            <button type="button" class="btn btn-sm btn-link text-danger p-0" onclick="PharmacyAdmin.removeDraftStockInLine(${idx})">Xóa</button>
                          </td>
                        </tr>
                      `;
                    }).join('')}
                  </tbody>
                </table>
              </div>

              <div class="row g-3 align-items-end">
                <div class="col-12 col-md-5">
                  <label for="siSupplierSel" class="form-label-ph">Nhà cung cấp phiếu nhập</label>
                  <select id="siSupplierSel" class="form-select-ph">
                    ${db.suppliers.map(s => `<option value="${s.id}">${esc(s.name)}</option>`).join('')}
                  </select>
                </div>
                <div class="col-12 col-md-4">
                  <label class="form-label-ph">Tổng tiền vốn (Tự động tính - Chỉ đọc)</label>
                  <input type="text" class="form-control-ph tabular-nums fw-bold" readonly value="${I18n.formatCurrency(draftTotalCost)}" />
                </div>
                <div class="col-12 col-md-3">
                  <button type="button" class="btn-ph btn-ph-primary w-100" ${draftLines.length === 0 ? 'disabled' : ''} onclick="PharmacyAdmin.saveStockInReceiptNow()">
                    ${C.icon('check', 15)} Lưu phiếu nhập
                  </button>
                </div>
              </div>
            </div>
          </div>

          <!-- Lịch sử Phiếu nhập đã lưu (Không cho xóa cứng phiếu đã ghi kho) -->
          <div class="col-12 col-xl-5">
            <div class="admin-panel p-4 h-100">
              <h2 class="h6 fw-bold mb-1">Danh sách Phiếu nhập kho đã lưu (${db.stockIns.length})</h2>
              <p class="small text-secondary mb-3">Phiếu đã ghi sổ kho được bảo toàn lịch sử (chế độ chỉ đọc, không xóa cứng).</p>
              <div class="d-flex flex-column gap-3">
                ${db.stockIns.map(si => `
                  <div class="p-3 rounded-3 border">
                    <div class="d-flex justify-content-between align-items-center mb-1">
                      <span class="font-mono-code fw-bold">${esc(si.code)}</span>
                      <span class="fw-bold tabular-nums" style="color:var(--color-primary-active);">${I18n.formatCurrency(si.totalCost)}</span>
                    </div>
                    <div class="small text-secondary mb-2">
                      NCC: <strong>${esc(si.supplierName)}</strong> • Ngày: ${I18n.formatDate(si.importDate, true)}
                    </div>
                    <button type="button" class="btn-ph btn-ph-secondary btn-ph-sm" onclick="PharmacyAdmin.openStockInDetailModal(${si.id})">
                      ${C.icon('eye', 14)} Xem chi tiết ${si.lines.length} dòng nhập
                    </button>
                  </div>
                `).join('')}
              </div>
            </div>
          </div>
        </div>
      `}
    `;
  }

  function switchInvTab(tab) {
    invTab = tab;
    window.PharmacyApp.refreshCurrentRoute();
  }

  function updateBatchFilter(k, v) {
    batchFilter[k] = v;
    window.PharmacyApp.refreshCurrentRoute();
  }

  function confirmToggleBatchLock(batchId) {
    const db = DB.getDb();
    const b = db.batches.find(x => x.id === Number(batchId));
    if (!b) return;
    const med = db.medicines.find(m => m.id === b.medicineId);
    const nextLock = !b.isBlocked;

    C.openConfirmModal({
      title: `${nextLock ? 'Tạm khóa' : 'Mở khóa'} lô hàng ${b.batchNumber}`,
      message: nextLock
        ? `Tạm khóa lô "${b.batchNumber}" (${b.stock} đơn vị của ${med ? med.name : ''})? Số lượng của lô này sẽ bị loại khỏi Tồn có thể bán ngay lập tức.`
        : `Mở khóa lô "${b.batchNumber}"? Lưu ý: Mở khóa chỉ cho phép xuất bán nếu lô còn hạn và thuốc đang mở bán.`,
      confirmText: nextLock ? 'Tạm khóa lô' : 'Mở khóa lô',
      danger: nextLock,
      onConfirm: () => {
        API.toggleBatchLock(batchId, nextLock).then(() => {
          C.showToast(`Đã ${nextLock ? 'tạm khóa' : 'mở khóa'} lô ${b.batchNumber}.`);
          window.PharmacyApp.refreshCurrentRoute();
        });
      }
    });
  }

  function addDraftStockInLine(e) {
    e.preventDefault();
    const db = DB.getDb();
    db.stockInDraft = db.stockInDraft || [];
    db.stockInDraft.push({
      medicineId: Number(document.getElementById('siMedId').value),
      batchNumber: document.getElementById('siBatchNo').value.trim(),
      expiryDate: document.getElementById('siExpiry').value,
      quantity: Number(document.getElementById('siQty').value),
      unitCost: Number(document.getElementById('siCost').value)
    });
    DB.saveDb();
    window.PharmacyApp.refreshCurrentRoute();
  }

  function removeDraftStockInLine(idx) {
    const db = DB.getDb();
    db.stockInDraft.splice(idx, 1);
    DB.saveDb();
    window.PharmacyApp.refreshCurrentRoute();
  }

  function saveStockInReceiptNow() {
    const db = DB.getDb();
    const supplierId = Number(document.getElementById('siSupplierSel').value);
    API.createStockInReceipt({
      supplierId,
      note: 'Nhập kho từ giao diện Web Admin Prototype',
      lines: db.stockInDraft
    }).then(rec => {
      C.showToast(`Đã ghi sổ phiếu nhập ${rec.code}! Tồn có thể bán đã được cập nhật.`);
      window.PharmacyApp.refreshCurrentRoute();
    });
  }

  function openStockInDetailModal(id) {
    const si = DB.getDb().stockIns.find(s => s.id === Number(id));
    if (!si) return;
    C.openModal({
      title: `Chi tiết Phiếu nhập kho ${si.code} (Chỉ đọc)`,
      size: 'lg',
      bodyHtml: `
        <div class="row g-2 small mb-3">
          <div class="col-md-6"><strong>Nhà cung cấp:</strong> ${esc(si.supplierName)}</div>
          <div class="col-md-6"><strong>Người lập:</strong> ${esc(si.employeeName)}</div>
          <div class="col-md-6"><strong>Thời gian:</strong> ${I18n.formatDate(si.importDate, true)}</div>
          <div class="col-md-6"><strong>Ghi chú:</strong> ${esc(si.note || '—')}</div>
        </div>
        <div class="table-responsive">
          <table class="table-ph">
            <thead>
              <tr><th>Thuốc</th><th>Đơn vị</th><th>Số lô</th><th>Hạn dùng</th><th class="text-end">Số lượng</th><th class="text-end">Giá vốn</th><th class="text-end">Thành tiền</th></tr>
            </thead>
            <tbody>
              ${si.lines.map(l => `
                <tr>
                  <td class="fw-medium">${esc(l.medicineName)}</td>
                  <td>${esc(l.unit)}</td>
                  <td class="font-mono-code">${esc(l.batchNumber)}</td>
                  <td class="tabular-nums">${I18n.formatDate(l.expiryDate)}</td>
                  <td class="text-end tabular-nums">${l.quantity}</td>
                  <td class="text-end tabular-nums">${I18n.formatCurrency(l.unitCost)}</td>
                  <td class="text-end tabular-nums fw-bold">${I18n.formatCurrency(l.quantity * l.unitCost)}</td>
                </tr>
              `).join('')}
            </tbody>
          </table>
        </div>
        <div class="d-flex justify-content-end pt-3 border-top mt-3">
          <span class="fw-bold">Tổng tiền vốn phiếu nhập: <span class="text-teal ms-2">${I18n.formatCurrency(si.totalCost)}</span></span>
        </div>
      `,
      footerHtml: `<button type="button" class="btn-ph btn-ph-primary" onclick="PharmacyComponents.closeModal()">Đóng</button>`
    });
  }

  // ==================== 5. QUẢN LÝ ĐƠN HÀNG (#/admin/orders) ====================
  function renderOrdersSection(el, queryParams = {}) {
    if (queryParams.status) orderFilter.orderStatus = queryParams.status;
    const db = DB.getDb();

    let list = db.orders.slice();
    if (orderFilter.q) {
      const q = orderFilter.q.toLowerCase();
      list = list.filter(o =>
        o.code.toLowerCase().includes(q) ||
        o.customerName.toLowerCase().includes(q) ||
        o.customerPhone.includes(q)
      );
    }
    if (orderFilter.orderStatus !== 'ALL') list = list.filter(o => o.orderStatus === orderFilter.orderStatus);
    if (orderFilter.paymentStatus !== 'ALL') list = list.filter(o => o.paymentStatus === orderFilter.paymentStatus);

    const totalItems = list.length;
    const totalPages = Math.max(1, Math.ceil(totalItems / orderFilter.pageSize));
    const safePage = Math.min(orderFilter.page, totalPages);
    const paged = list.slice((safePage - 1) * orderFilter.pageSize, safePage * orderFilter.pageSize);

    el.innerHTML = `
      <div class="mb-3">
        <h1 class="h4 fw-bold mb-1">Quản lý Đơn hàng &amp; Đối soát Thanh toán</h1>
        <p class="small text-secondary mb-0">
          Tách biệt trạng thái Đơn hàng, Thanh toán và Giao hàng. Giữ nguyên lịch sử đơn đã hoàn tất (không xóa cứng). Kiểm tra phiên bản đơn (<code>OrderVersion</code>) chống ghi đè.
        </p>
      </div>

      <div class="admin-panel p-3 mb-3">
        <div class="row g-2 align-items-end">
          <div class="col-12 col-md-4">
            <label for="admOrdQ" class="form-label-ph">Tìm mã đơn, tên khách, SĐT</label>
            <input id="admOrdQ" type="search" class="form-control-ph" placeholder="VD: HD00929, Trần Văn Tuấn..." value="${esc(orderFilter.q)}" oninput="PharmacyAdmin.updateOrdFilter('q', this.value)" />
          </div>
          <div class="col-6 col-md-3">
            <label for="admOrdSt" class="form-label-ph">Trạng thái đơn</label>
            <select id="admOrdSt" class="form-select-ph" onchange="PharmacyAdmin.updateOrdFilter('orderStatus', this.value)">
              <option value="ALL" ${orderFilter.orderStatus === 'ALL' ? 'selected' : ''}>Tất cả trạng thái</option>
              <option value="NEW" ${orderFilter.orderStatus === 'NEW' ? 'selected' : ''}>Mới tạo (NEW)</option>
              <option value="CONFIRMED" ${orderFilter.orderStatus === 'CONFIRMED' ? 'selected' : ''}>Đã xác nhận</option>
              <option value="PROCESSING" ${orderFilter.orderStatus === 'PROCESSING' ? 'selected' : ''}>Đang chuẩn bị / Giao</option>
              <option value="COMPLETED" ${orderFilter.orderStatus === 'COMPLETED' ? 'selected' : ''}>Hoàn tất</option>
              <option value="CANCELLED" ${orderFilter.orderStatus === 'CANCELLED' ? 'selected' : ''}>Đã hủy</option>
            </select>
          </div>
          <div class="col-6 col-md-3">
            <label for="admPaySt" class="form-label-ph">Trạng thái thanh toán</label>
            <select id="admPaySt" class="form-select-ph" onchange="PharmacyAdmin.updateOrdFilter('paymentStatus', this.value)">
              <option value="ALL" ${orderFilter.paymentStatus === 'ALL' ? 'selected' : ''}>Tất cả thanh toán</option>
              <option value="UNPAID" ${orderFilter.paymentStatus === 'UNPAID' ? 'selected' : ''}>Chưa thanh toán (COD)</option>
              <option value="PENDING_TRANSFER" ${orderFilter.paymentStatus === 'PENDING_TRANSFER' ? 'selected' : ''}>Chờ xác nhận CK</option>
              <option value="PAID" ${orderFilter.paymentStatus === 'PAID' ? 'selected' : ''}>Đã thu tiền (PAID)</option>
            </select>
          </div>
          <div class="col-12 col-md-2">
            <button type="button" class="btn-ph btn-ph-secondary w-100" onclick="PharmacyAdmin.resetOrdFilter()">Xóa lọc</button>
          </div>
        </div>
      </div>

      <div class="admin-panel p-3">
        <div class="admin-table-container">
          <table class="table-ph">
            <thead>
              <tr>
                <th>Mã đơn</th>
                <th>Khách hàng</th>
                <th>Ngày đặt</th>
                <th>Trạng thái đơn</th>
                <th>Thanh toán</th>
                <th>Giao hàng</th>
                <th class="text-end">Tổng tiền</th>
                <th class="text-center">Phiên bản</th>
                <th class="text-end">Thao tác</th>
              </tr>
            </thead>
            <tbody>
              ${paged.map(o => `
                <tr>
                  <td class="font-mono-code fw-bold">${esc(o.code)}</td>
                  <td>
                    <div class="fw-semibold">${esc(o.customerName)}</div>
                    <div class="small text-muted tabular-nums">${esc(o.customerPhone)} • ${esc(o.channel)}</div>
                  </td>
                  <td class="tabular-nums small">${I18n.formatDate(o.orderDate, true)}</td>
                  <td>${C.renderStatusBadge('order', o.orderStatus)}</td>
                  <td>${C.renderStatusBadge('payment', o.paymentStatus)}</td>
                  <td>${C.renderStatusBadge('shipping', o.shippingStatus)}</td>
                  <td class="text-end tabular-nums fw-bold">${I18n.formatCurrency(o.totalAmount)}</td>
                  <td class="text-center font-mono-code">v${o.orderVersion}</td>
                  <td class="text-end">
                    <button type="button" class="btn-ph btn-ph-primary btn-ph-sm" onclick="PharmacyAdmin.openAdminOrderModal(${o.id})">
                      Chi tiết / Cập nhật
                    </button>
                  </td>
                </tr>
              `).join('')}
            </tbody>
          </table>
        </div>
        ${C.renderPagination({ page: safePage, totalPages, totalItems }, 'PharmacyAdmin.changeOrdPage')}
      </div>
    `;
  }

  function updateOrdFilter(k, v) {
    orderFilter[k] = v;
    orderFilter.page = 1;
    window.PharmacyApp.refreshCurrentRoute();
  }

  function resetOrdFilter() {
    orderFilter = { q: '', orderStatus: 'ALL', paymentStatus: 'ALL', page: 1, pageSize: 6 };
    window.PharmacyApp.refreshCurrentRoute();
  }

  function changeOrdPage(p) {
    orderFilter.page = Number(p) || 1;
    window.PharmacyApp.refreshCurrentRoute();
  }

  function openAdminOrderModal(orderId) {
    const ord = DB.getDb().orders.find(o => o.id === Number(orderId));
    if (!ord) return;
    const isCancelled = ord.orderStatus === 'CANCELLED';

    C.openModal({
      title: `Xử lý Đơn hàng ${ord.code} (Phiên bản hiện tại: v${ord.orderVersion})`,
      size: 'lg',
      bodyHtml: `
        <div id="adminOrderConflictBox"></div>

        <div class="row g-3 small mb-3">
          <div class="col-md-6">
            <div><strong>Khách hàng:</strong> ${esc(ord.customerName)} (${esc(ord.customerPhone)})</div>
            <div><strong>Địa chỉ nhận:</strong> ${esc(ord.shippingAddress)}</div>
          </div>
          <div class="col-md-6">
            <div><strong>Ngày tạo:</strong> ${I18n.formatDate(ord.orderDate, true)}</div>
            <div><strong>Hình thức TT:</strong> ${esc(ord.paymentMethod)} • Voucher: ${esc(ord.voucherCode || 'Không')}</div>
          </div>
        </div>

        <div class="table-responsive mb-3">
          <table class="table-ph">
            <thead>
              <tr><th>Thuốc</th><th>Đơn vị</th><th class="text-end">Số lượng</th><th class="text-end">Đơn giá</th><th class="text-end">Thành tiền</th></tr>
            </thead>
            <tbody>
              ${ord.items.map(i => `
                <tr>
                  <td class="fw-medium">${esc(i.medicineName)}</td>
                  <td>${esc(i.unit)}</td>
                  <td class="text-end tabular-nums">${i.quantity}</td>
                  <td class="text-end tabular-nums">${I18n.formatCurrency(i.unitPrice)}</td>
                  <td class="text-end tabular-nums fw-bold">${I18n.formatCurrency(i.unitPrice * i.quantity)}</td>
                </tr>
              `).join('')}
            </tbody>
          </table>
        </div>

        <!-- Form chuyển trạng thái hợp lệ -->
        <form id="adminOrderUpdateForm" onsubmit="PharmacyAdmin.handleSaveAdminOrder(event, ${ord.id})" class="p-3 rounded-3 border bg-light mb-3">
          <input type="hidden" id="admOrdExpectedVer" value="${ord.orderVersion}" />
          <div class="d-flex justify-content-between align-items-center mb-2">
            <strong class="small">Cập nhật luồng trạng thái (Tách biệt Đơn — Thanh toán — Giao hàng)</strong>
            <button type="button" class="btn-ph btn-ph-secondary btn-ph-sm" onclick="PharmacyAdmin.simulateConcurrentOrderEdit(${ord.id})">
              Mô phỏng đơn vừa bị sửa từ tab khác (Test xung đột v${ord.orderVersion})
            </button>
          </div>
          <div class="row g-2">
            <div class="col-12 col-md-4">
              <label for="admSelOrderSt" class="form-label-ph">1. Trạng thái đơn hàng</label>
              <select id="admSelOrderSt" class="form-select-ph" ${isCancelled ? 'disabled' : ''}>
                <option value="NEW" ${ord.orderStatus === 'NEW' ? 'selected' : ''}>Mới tạo (NEW)</option>
                <option value="CONFIRMED" ${ord.orderStatus === 'CONFIRMED' ? 'selected' : ''}>Đã xác nhận (CONFIRMED)</option>
                <option value="PROCESSING" ${ord.orderStatus === 'PROCESSING' ? 'selected' : ''}>Đang xử lý (PROCESSING)</option>
                <option value="COMPLETED" ${ord.orderStatus === 'COMPLETED' ? 'selected' : ''}>Hoàn tất (COMPLETED)</option>
                <option value="CANCELLED" ${ord.orderStatus === 'CANCELLED' ? 'selected' : ''}>Đã hủy (CANCELLED)</option>
              </select>
            </div>
            <div class="col-12 col-md-4">
              <label for="admSelPaySt" class="form-label-ph">2. Trạng thái thanh toán</label>
              <select id="admSelPaySt" class="form-select-ph" ${isCancelled ? 'disabled' : ''}>
                <option value="UNPAID" ${ord.paymentStatus === 'UNPAID' ? 'selected' : ''}>Chưa thanh toán (COD)</option>
                <option value="PENDING_TRANSFER" ${ord.paymentStatus === 'PENDING_TRANSFER' ? 'selected' : ''}>Chờ xác nhận chuyển khoản</option>
                <option value="PAID" ${ord.paymentStatus === 'PAID' ? 'selected' : ''}>Đã thu tiền (PAID)</option>
                <option value="REFUNDED" ${ord.paymentStatus === 'REFUNDED' ? 'selected' : ''}>Đã hoàn tiền</option>
              </select>
            </div>
            <div class="col-12 col-md-4">
              <label for="admSelShipSt" class="form-label-ph">3. Trạng thái giao hàng</label>
              <select id="admSelShipSt" class="form-select-ph" ${isCancelled ? 'disabled' : ''}>
                <option value="NOT_SHIPPED" ${ord.shippingStatus === 'NOT_SHIPPED' ? 'selected' : ''}>Chưa giao</option>
                <option value="PACKING" ${ord.shippingStatus === 'PACKING' ? 'selected' : ''}>Đang đóng gói</option>
                <option value="SHIPPING" ${ord.shippingStatus === 'SHIPPING' ? 'selected' : ''}>Đang giao hàng</option>
                <option value="DELIVERED" ${ord.shippingStatus === 'DELIVERED' ? 'selected' : ''}>Đã giao thành công</option>
                <option value="RETURNED" ${ord.shippingStatus === 'RETURNED' ? 'selected' : ''}>Giao thất bại / Hoàn kho</option>
              </select>
            </div>
          </div>
        </form>

        <h3 class="h6 fw-bold mb-2">Nhật ký lịch sử đơn hàng</h3>
        <ul class="list-group list-group-flush small">
          ${(ord.history || []).map(h => `
            <li class="list-group-item px-0 d-flex justify-content-between">
              <span><strong>${esc(h.actor)}:</strong> ${esc(h.text)}</span>
              <span class="text-muted tabular-nums">${I18n.formatDate(h.time, true)}</span>
            </li>
          `).join('')}
        </ul>
      `,
      footerHtml: `
        <button type="button" class="btn-ph btn-ph-secondary" onclick="PharmacyComponents.closeModal()">Đóng</button>
        ${!isCancelled ? `<button type="submit" form="adminOrderUpdateForm" class="btn-ph btn-ph-primary">${C.icon('check', 16)} Lưu cập nhật trạng thái</button>` : ''}
      `
    });
  }

  function simulateConcurrentOrderEdit(orderId) {
    const ord = DB.getDb().orders.find(o => o.id === Number(orderId));
    if (!ord) return;
    ord.orderVersion += 1;
    ord.history.push({
      time: new Date().toISOString(),
      actor: 'Dược sĩ ca trực khác (Mô phỏng)',
      text: `Đã cập nhật đơn lên phiên bản v${ord.orderVersion} từ một cửa sổ khác`
    });
    DB.saveDb();
    C.showToast(`Đã tăng phiên bản đơn trong bộ nhớ lên v${ord.orderVersion}. Hãy bấm "Lưu cập nhật trạng thái" để thấy cảnh báo xung đột!`, 'warning');
  }

  function handleSaveAdminOrder(e, orderId) {
    e.preventDefault();
    const expectedVersion = Number(document.getElementById('admOrdExpectedVer').value);
    const orderStatus = document.getElementById('admSelOrderSt').value;
    const paymentStatus = document.getElementById('admSelPaySt').value;
    const shippingStatus = document.getElementById('admSelShipSt').value;

    API.updateAdminOrderStatus(orderId, { orderStatus, paymentStatus, shippingStatus, expectedVersion })
      .then(updated => {
        C.closeModal();
        C.showToast(`Đã cập nhật đơn hàng ${updated.code} (v${updated.orderVersion}). Dashboard & Báo cáo đã được đồng bộ.`);
        window.PharmacyApp.refreshCurrentRoute();
      })
      .catch(err => {
        const box = document.getElementById('adminOrderConflictBox');
        if (box) {
          box.innerHTML = `
            <div class="p-3 rounded-3 mb-3" role="alert" style="background:var(--color-danger-bg);color:var(--color-danger);border:1px solid var(--color-danger-border);">
              <div class="fw-bold mb-1">${C.icon('alert', 16)} Phát hiện xung đột cập nhật (HTTP 409 Conflict):</div>
              <div class="small mb-2">${esc(err.message)}</div>
              <button type="button" class="btn-ph btn-ph-danger btn-ph-sm" onclick="PharmacyAdmin.openAdminOrderModal(${orderId})">
                ${C.icon('refresh', 14)} Tải lại dữ liệu mới nhất của đơn
              </button>
            </div>
          `;
        }
      });
  }

  // ==================== 6. YÊU CẦU ĐƠN THUỐC (#/admin/prescriptions) ====================
  function renderPrescriptionsSection(el) {
    const db = DB.getDb();
    let list = db.prescriptionRequests.slice();
    if (rxAdminFilter !== 'ALL') {
      list = list.filter(r => r.status === rxAdminFilter);
    }

    el.innerHTML = `
      <div class="d-flex flex-wrap align-items-center justify-content-between gap-2 mb-3">
        <div>
          <h1 class="h4 fw-bold mb-1">Tiếp nhận &amp; Phản hồi Yêu cầu Đơn thuốc (Rx)</h1>
          <p class="small text-secondary mb-0">
            Kiểm tra hình ảnh đơn thuốc khách tải lên, yêu cầu bổ sung ảnh nếu mờ/thiếu góc, hoặc gửi phản hồi hướng dẫn nhận thuốc. Không tự đưa ra chẩn đoán y khoa.
          </p>
        </div>
        <div class="d-flex gap-1">
          ${['ALL', 'PENDING_REVIEW', 'NEED_MORE_INFO', 'RESPONDED'].map(st => {
            const labels = { ALL: 'Tất cả', PENDING_REVIEW: 'Chờ kiểm tra', NEED_MORE_INFO: 'Cần bổ sung', RESPONDED: 'Đã phản hồi' };
            return `<button type="button" class="btn-ph ${rxAdminFilter === st ? 'btn-ph-primary' : 'btn-ph-secondary'} btn-ph-sm" onclick="PharmacyAdmin.setRxAdminFilter('${st}')">${labels[st]}</button>`;
          }).join('')}
        </div>
      </div>

      <div class="row g-3">
        ${list.map(req => `
          <div class="col-12 col-lg-6">
            <div class="admin-panel p-4 h-100 d-flex flex-column justify-content-between">
              <div>
                <div class="d-flex justify-content-between align-items-center mb-2">
                  <div>
                    <span class="font-mono-code fw-bold fs-6">${esc(req.code)}</span>
                    <span class="small text-muted ms-2">${I18n.formatDate(req.createdAt, true)}</span>
                  </div>
                  ${C.renderStatusBadge('rxRequest', req.status)}
                </div>

                <div class="small mb-2">
                  <strong>Người gửi:</strong> ${esc(req.patientName)} — <strong>SĐT:</strong> <span class="tabular-nums">${esc(req.phone)}</span>
                </div>
                <div class="small mb-2">
                  <strong>Thuốc khách quan tâm:</strong> ${(req.requestedMedicines || []).map(m => esc(m.name)).join(', ') || 'Theo ảnh đơn đính kèm'}
                </div>
                <div class="small text-secondary mb-3">
                  <strong>Ghi chú của khách:</strong> ${esc(req.customerNote || 'Không có')}
                </div>

                <!-- Ảnh đơn có thể bấm phóng to -->
                <div class="d-flex flex-wrap gap-2 mb-3">
                  ${(req.images || []).map((img, i) => `
                    <div class="p-2 border rounded bg-light d-flex align-items-center gap-2">
                      <img src="${img.dataUrl}" alt="" width="54" height="54" class="rounded border bg-white" style="object-fit:cover;cursor:pointer;" onclick="PharmacyStorefront.openImageZoomModal('${img.dataUrl}', '${esc(req.code)}')" />
                      <div>
                        <div class="small fw-medium">${esc(img.name)}</div>
                        <button type="button" class="btn btn-sm btn-link p-0 text-decoration-none" onclick="PharmacyStorefront.openImageZoomModal('${img.dataUrl}', '${esc(req.code)}')">
                          ${C.icon('eye', 13)} Phóng to kiểm tra
                        </button>
                      </div>
                    </div>
                  `).join('')}
                </div>

                ${req.pharmacistNote ? `
                  <div class="p-2 rounded small mb-3" style="background:var(--color-primary-soft-bg);color:var(--color-primary-soft-fg);border:1px solid #99F6E4;">
                    <strong>Phản hồi hiện tại:</strong> ${esc(req.pharmacistNote)}
                  </div>
                ` : ''}
              </div>

              <div class="pt-3 border-top d-flex justify-content-end gap-2">
                <button type="button" class="btn-ph btn-ph-primary btn-ph-sm" onclick="PharmacyAdmin.openRxResponseModal(${req.id})">
                  ${C.icon('edit', 14)} Cập nhật trạng thái &amp; Phản hồi mẫu
                </button>
              </div>
            </div>
          </div>
        `).join('')}
      </div>
    `;
  }

  function setRxAdminFilter(st) {
    rxAdminFilter = st;
    window.PharmacyApp.refreshCurrentRoute();
  }

  function openRxResponseModal(reqId) {
    const req = DB.getDb().prescriptionRequests.find(r => r.id === Number(reqId));
    if (!req) return;

    C.openModal({
      title: `Phản hồi Yêu cầu Đơn thuốc ${req.code} (${req.patientName})`,
      size: 'md',
      bodyHtml: `
        <form id="rxRespForm" onsubmit="PharmacyAdmin.saveRxResponse(event, ${req.id})">
          <div class="mb-3">
            <label for="rxStatusSel" class="form-label-ph">Trạng thái xử lý <span class="text-danger">*</span></label>
            <select id="rxStatusSel" class="form-select-ph">
              <option value="PENDING_REVIEW" ${req.status === 'PENDING_REVIEW' ? 'selected' : ''}>Chờ kiểm tra</option>
              <option value="NEED_MORE_INFO" ${req.status === 'NEED_MORE_INFO' ? 'selected' : ''}>Cần bổ sung (Ảnh mờ / thiếu thông tin)</option>
              <option value="RESPONDED" ${req.status === 'RESPONDED' ? 'selected' : ''}>Đã phản hồi hướng dẫn</option>
            </select>
          </div>
          <div class="mb-2">
            <span class="small text-muted d-block mb-1">Chọn nhanh mẫu phản hồi chuẩn (Không tự kết luận y khoa):</span>
            <div class="d-flex flex-wrap gap-1 mb-2">
              <button type="button" class="btn-ph btn-ph-secondary btn-ph-sm" onclick="PharmacyAdmin.fillRxTemplate('MORE_INFO')">Mẫu 1: Yêu cầu chụp lại ảnh rõ nét</button>
              <button type="button" class="btn-ph btn-ph-mint btn-ph-sm" onclick="PharmacyAdmin.fillRxTemplate('AVAILABLE')">Mẫu 2: Xác nhận còn hàng &amp; hướng dẫn nhận tại quầy</button>
            </div>
          </div>
          <div class="mb-2">
            <label for="rxNoteTxt" class="form-label-ph">Nội dung phản hồi gửi khách hàng <span class="text-danger">*</span></label>
            <textarea id="rxNoteTxt" rows="4" class="form-control-ph" required>${esc(req.pharmacistNote || '')}</textarea>
          </div>
        </form>
      `,
      footerHtml: `
        <button type="button" class="btn-ph btn-ph-secondary" onclick="PharmacyComponents.closeModal()">Hủy</button>
        <button type="submit" form="rxRespForm" class="btn-ph btn-ph-primary">Lưu phản hồi</button>
      `
    });
  }

  function fillRxTemplate(type) {
    const stSel = document.getElementById('rxStatusSel');
    const txt = document.getElementById('rxNoteTxt');
    if (type === 'MORE_INFO') {
      stSel.value = 'NEED_MORE_INFO';
      txt.value = 'Ảnh chụp đơn thuốc hiện bị mờ phần ngày kê đơn và chữ ký người kê đơn. Quý khách vui lòng chụp bổ sung toàn bộ mặt tờ đơn thuốc rõ nét để dược sĩ đối chiếu.';
    } else {
      stSel.value = 'RESPONDED';
      txt.value = 'Nhà thuốc đã tiếp nhận thông tin đơn thuốc mẫu. Sản phẩm trên đơn hiện còn tồn tại quầy. Quý khách vui lòng mang bản gốc đơn thuốc tới quầy để dược sĩ kiểm tra trực tiếp và hướng dẫn.';
    }
  }

  function saveRxResponse(e, reqId) {
    e.preventDefault();
    const status = document.getElementById('rxStatusSel').value;
    const pharmacistNote = document.getElementById('rxNoteTxt').value.trim();
    API.respondPrescriptionRequest(reqId, { status, pharmacistNote }).then(updated => {
      C.closeModal();
      C.showToast(`Đã cập nhật phản hồi cho yêu cầu ${updated.code}!`);
      window.PharmacyApp.refreshCurrentRoute();
    });
  }

  // ==================== 7. KHÁCH HÀNG & TÀI KHOẢN WEB (#/admin/customers) ====================
  function renderCustomersSection(el) {
    const db = DB.getDb();
    let list = db.customers.slice();
    if (custFilter.q) {
      const q = custFilter.q.toLowerCase();
      list = list.filter(c => c.fullName.toLowerCase().includes(q) || c.phone.includes(q) || (c.email && c.email.toLowerCase().includes(q)));
    }
    if (custFilter.status === 'ACTIVE') list = list.filter(c => !c.isLocked);
    else if (custFilter.status === 'LOCKED') list = list.filter(c => c.isLocked);

    el.innerHTML = `
      <div class="mb-3">
        <h1 class="h4 fw-bold mb-1">Quản lý Hồ sơ Khách hàng &amp; Tài khoản Website</h1>
        <p class="small text-secondary mb-0">
          Tra cứu lịch sử mua hàng, điểm tích lũy tại quầy (chỉ đọc) và khóa/mở khóa tài khoản web. <strong>Bảo mật:</strong> Tuyệt đối không hiển thị mật khẩu và không có thao tác đọc mật khẩu cũ.
        </p>
      </div>

      <div class="admin-panel p-3 mb-3">
        <div class="row g-2 align-items-end">
          <div class="col-12 col-md-6">
            <label for="custQ" class="form-label-ph">Tìm theo họ tên, số điện thoại, email</label>
            <input id="custQ" type="search" class="form-control-ph" placeholder="VD: Trần Văn Tuấn, 0912345678..." value="${esc(custFilter.q)}" oninput="PharmacyAdmin.updateCustFilter('q', this.value)" />
          </div>
          <div class="col-12 col-md-4">
            <label for="custStSel" class="form-label-ph">Trạng thái tài khoản</label>
            <select id="custStSel" class="form-select-ph" onchange="PharmacyAdmin.updateCustFilter('status', this.value)">
              <option value="ALL" ${custFilter.status === 'ALL' ? 'selected' : ''}>Tất cả (${db.customers.length})</option>
              <option value="ACTIVE" ${custFilter.status === 'ACTIVE' ? 'selected' : ''}>Đang hoạt động</option>
              <option value="LOCKED" ${custFilter.status === 'LOCKED' ? 'selected' : ''}>Đang tạm khóa</option>
            </select>
          </div>
        </div>
      </div>

      <div class="admin-panel p-3">
        <div class="admin-table-container">
          <table class="table-ph">
            <thead>
              <tr>
                <th>Mã KH</th>
                <th>Họ và tên</th>
                <th>Số điện thoại</th>
                <th>Email</th>
                <th class="text-end">Điểm POS (Chỉ đọc)</th>
                <th class="text-end">Số đơn hàng</th>
                <th>Trạng thái TK Web</th>
                <th class="text-end">Thao tác</th>
              </tr>
            </thead>
            <tbody>
              ${list.map(c => {
                const ordCount = db.orders.filter(o => o.customerId === c.id).length;
                return `
                  <tr>
                    <td class="font-mono-code">#${c.id}</td>
                    <td class="fw-semibold">${esc(c.fullName)}</td>
                    <td class="tabular-nums">${esc(c.phone)}</td>
                    <td>${esc(c.email || '—')}</td>
                    <td class="text-end tabular-nums">${c.points} điểm</td>
                    <td class="text-end tabular-nums fw-bold">${ordCount} đơn</td>
                    <td>
                      ${c.isLocked
                        ? `<span class="status-badge status-badge-danger">${C.icon('lock', 12)} Tạm khóa</span>`
                        : `<span class="status-badge status-badge-success">${C.icon('check', 12)} Hoạt động</span>`}
                    </td>
                    <td class="text-end">
                      <button type="button" class="btn-ph btn-ph-secondary btn-ph-sm" onclick="PharmacyAdmin.openCustomerOrdersModal(${c.id})">
                        Lịch sử đơn (${ordCount})
                      </button>
                      <button type="button" class="btn-ph ${c.isLocked ? 'btn-ph-mint' : 'btn-ph-danger-outline'} btn-ph-sm" onclick="PharmacyAdmin.confirmToggleCustomerLock(${c.id})">
                        ${c.isLocked ? 'Mở khóa TK' : 'Khóa TK'}
                      </button>
                    </td>
                  </tr>
                `;
              }).join('')}
            </tbody>
          </table>
        </div>
      </div>
    `;
  }

  function updateCustFilter(k, v) {
    custFilter[k] = v;
    window.PharmacyApp.refreshCurrentRoute();
  }

  function confirmToggleCustomerLock(custId) {
    const c = DB.getDb().customers.find(x => x.id === Number(custId));
    if (!c) return;
    const nextLock = !c.isLocked;
    C.openConfirmModal({
      title: `${nextLock ? 'Tạm khóa' : 'Mở khóa'} tài khoản khách hàng`,
      message: nextLock
        ? `Tạm khóa tài khoản web của "${c.fullName}" (${c.phone})? Khách hàng này sẽ không thể đăng nhập hoặc đặt đơn mới trên website.`
        : `Mở khóa tài khoản web cho "${c.fullName}"?`,
      confirmText: nextLock ? 'Khóa tài khoản' : 'Mở khóa',
      danger: nextLock,
      onConfirm: () => {
        c.isLocked = nextLock;
        DB.saveDb();
        C.showToast(`Đã ${nextLock ? 'tạm khóa' : 'mở khóa'} tài khoản khách hàng ${c.fullName}.`);
        window.PharmacyApp.refreshCurrentRoute();
      }
    });
  }

  function openCustomerOrdersModal(custId) {
    const db = DB.getDb();
    const c = db.customers.find(x => x.id === Number(custId));
    if (!c) return;
    const ords = db.orders.filter(o => o.customerId === c.id);

    C.openModal({
      title: `Hồ sơ & Lịch sử đơn hàng: ${c.fullName} (${c.phone})`,
      size: 'lg',
      bodyHtml: `
        <div class="p-3 rounded-2 bg-light border small mb-3 d-flex flex-wrap justify-content-between gap-2">
          <span><strong>Email:</strong> ${esc(c.email || '—')}</span>
          <span><strong>Điểm tích lũy POS (Chỉ đọc):</strong> ${c.points} điểm</span>
          <span><strong>Mật khẩu:</strong> <em>[Đã băm một chiều — Không hiển thị]</em></span>
        </div>
        ${ords.length === 0 ? `<p class="text-secondary small">Khách hàng này chưa có đơn hàng nào.</p>` : `
          <div class="table-responsive">
            <table class="table-ph">
              <thead>
                <tr><th>Mã đơn</th><th>Ngày đặt</th><th>Kênh</th><th>Trạng thái đơn</th><th>Thanh toán</th><th class="text-end">Tổng tiền</th></tr>
              </thead>
              <tbody>
                ${ords.map(o => `
                  <tr>
                    <td class="font-mono-code fw-bold">${esc(o.code)}</td>
                    <td class="tabular-nums">${I18n.formatDate(o.orderDate, true)}</td>
                    <td>${esc(o.channel)}</td>
                    <td>${C.renderStatusBadge('order', o.orderStatus)}</td>
                    <td>${C.renderStatusBadge('payment', o.paymentStatus)}</td>
                    <td class="text-end tabular-nums fw-bold">${I18n.formatCurrency(o.totalAmount)}</td>
                  </tr>
                `).join('')}
              </tbody>
            </table>
          </div>
        `}
      `,
      footerHtml: `<button type="button" class="btn-ph btn-ph-primary" onclick="PharmacyComponents.closeModal()">Đóng</button>`
    });
  }

  // ==================== 8. QUẢN LÝ VOUCHER (#/admin/vouchers) ====================
  function renderVouchersSection(el) {
    const db = DB.getDb();

    el.innerHTML = `
      <div class="d-flex flex-wrap align-items-center justify-content-between gap-2 mb-3">
        <div>
          <h1 class="h4 fw-bold mb-1">Quản lý Mã giảm giá (Vouchers)</h1>
          <p class="small text-secondary mb-0">
            Hỗ trợ hai loại <code>FIXED</code> (tiền cố định) và <code>PERCENT</code> (phần trăm), kiểm soát số lượt còn lại và trạng thái hoạt động/tạm khóa.
          </p>
        </div>
        <button type="button" class="btn-ph btn-ph-primary" onclick="PharmacyAdmin.openVoucherModal(null)">
          ${C.icon('plus', 16)} Thêm mã Voucher mới
        </button>
      </div>

      <div class="admin-panel p-3">
        <div class="admin-table-container">
          <table class="table-ph">
            <thead>
              <tr>
                <th>Mã Voucher</th>
                <th>Loại giảm</th>
                <th class="text-end">Giá trị giảm</th>
                <th class="text-end">Đơn tối thiểu</th>
                <th class="text-end">Số lượt còn</th>
                <th>Trạng thái</th>
                <th>Mô tả</th>
                <th class="text-end">Thao tác</th>
              </tr>
            </thead>
            <tbody>
              ${db.vouchers.map(v => `
                <tr>
                  <td class="font-mono-code fw-bold">${esc(v.code)}</td>
                  <td><span class="status-badge status-badge-neutral">${esc(v.discountType)}</span></td>
                  <td class="text-end tabular-nums fw-bold">
                    ${v.discountType === 'FIXED' ? I18n.formatCurrency(v.discountValue) : `${v.discountValue}%`}
                  </td>
                  <td class="text-end tabular-nums">${I18n.formatCurrency(v.minOrderValue || 0)}</td>
                  <td class="text-end tabular-nums fw-bold ${v.quantity <= 0 ? 'text-danger' : ''}">${v.quantity} lượt</td>
                  <td>
                    ${!v.isActive
                      ? `<span class="status-badge status-badge-danger">Đang tạm khóa</span>`
                      : (v.quantity <= 0
                          ? `<span class="status-badge status-badge-warning">Hết lượt (0)</span>`
                          : `<span class="status-badge status-badge-success">Hoạt động</span>`)}
                  </td>
                  <td class="small text-secondary">${esc(v.description || '')}</td>
                  <td class="text-end">
                    <button type="button" class="btn-ph btn-ph-secondary btn-ph-sm" onclick="PharmacyAdmin.openVoucherModal(${v.id})">Sửa</button>
                    <button type="button" class="btn-ph ${v.isActive ? 'btn-ph-danger-outline' : 'btn-ph-mint'} btn-ph-sm" onclick="PharmacyAdmin.confirmToggleVoucher(${v.id})">
                      ${v.isActive ? 'Khóa' : 'Mở khóa'}
                    </button>
                  </td>
                </tr>
              `).join('')}
            </tbody>
          </table>
        </div>
      </div>
    `;
  }

  function openVoucherModal(vId) {
    const db = DB.getDb();
    const v = vId ? db.vouchers.find(x => x.id === Number(vId)) : {
      id: null, code: 'PHNEW15', discountType: 'FIXED', discountValue: 15000, minOrderValue: 60000, maxDiscountCap: null, quantity: 25, isActive: true, description: 'Giảm 15.000 ₫ cho đơn từ 60.000 ₫'
    };

    C.openModal({
      title: v.id ? `Chỉnh sửa Voucher ${v.code}` : 'Thêm mã Voucher mới',
      size: 'sm',
      bodyHtml: `
        <form id="voucherModalForm" onsubmit="PharmacyAdmin.saveVoucherForm(event, ${v.id || 'null'})">
          <div class="mb-3">
            <label for="vcCode" class="form-label-ph">Mã giảm giá (Code) <span class="text-danger">*</span></label>
            <input id="vcCode" type="text" class="form-control-ph font-mono-code text-uppercase" required maxlength="20" value="${esc(v.code)}" />
          </div>
          <div class="row g-2 mb-3">
            <div class="col-6">
              <label for="vcType" class="form-label-ph">Loại giảm</label>
              <select id="vcType" class="form-select-ph">
                <option value="FIXED" ${v.discountType === 'FIXED' ? 'selected' : ''}>FIXED (Tiền mặt ₫)</option>
                <option value="PERCENT" ${v.discountType === 'PERCENT' ? 'selected' : ''}>PERCENT (Phần trăm %)</option>
              </select>
            </div>
            <div class="col-6">
              <label for="vcVal" class="form-label-ph">Giá trị giảm <span class="text-danger">*</span></label>
              <input id="vcVal" type="number" class="form-control-ph tabular-nums" required min="1" value="${v.discountValue}" />
            </div>
          </div>
          <div class="row g-2 mb-3">
            <div class="col-6">
              <label for="vcMin" class="form-label-ph">Đơn tối thiểu (₫)</label>
              <input id="vcMin" type="number" class="form-control-ph tabular-nums" min="0" value="${v.minOrderValue || 0}" />
            </div>
            <div class="col-6">
              <label for="vcQty" class="form-label-ph">Số lượt còn lại <span class="text-danger">*</span></label>
              <input id="vcQty" type="number" class="form-control-ph tabular-nums" required min="0" value="${v.quantity}" />
            </div>
          </div>
          <div class="mb-2">
            <label for="vcDesc" class="form-label-ph">Mô tả điều kiện</label>
            <input id="vcDesc" type="text" class="form-control-ph" value="${esc(v.description || '')}" />
          </div>
        </form>
      `,
      footerHtml: `
        <button type="button" class="btn-ph btn-ph-secondary" onclick="PharmacyComponents.closeModal()">Hủy</button>
        <button type="submit" form="voucherModalForm" class="btn-ph btn-ph-primary">Lưu Voucher</button>
      `
    });
  }

  function saveVoucherForm(e, vId) {
    e.preventDefault();
    const db = DB.getDb();
    const code = document.getElementById('vcCode').value.trim().toUpperCase();
    const discountType = document.getElementById('vcType').value;
    const discountValue = Number(document.getElementById('vcVal').value);
    const minOrderValue = Number(document.getElementById('vcMin').value) || 0;
    const quantity = Number(document.getElementById('vcQty').value) || 0;
    const description = document.getElementById('vcDesc').value.trim();

    if (vId) {
      const v = db.vouchers.find(x => x.id === Number(vId));
      if (v) {
        Object.assign(v, { code, discountType, discountValue, minOrderValue, quantity, description });
      }
    } else {
      const nextId = db.vouchers.reduce((m, x) => Math.max(m, x.id), 6) + 1;
      db.vouchers.push({ id: nextId, code, discountType, discountValue, minOrderValue, maxDiscountCap: null, quantity, isActive: true, description });
    }
    DB.saveDb();
    C.closeModal();
    C.showToast(`Đã lưu thông tin mã giảm giá ${code}.`);
    window.PharmacyApp.refreshCurrentRoute();
  }

  function confirmToggleVoucher(vId) {
    const v = DB.getDb().vouchers.find(x => x.id === Number(vId));
    if (!v) return;
    const nextActive = !v.isActive;
    C.openConfirmModal({
      title: `${nextActive ? 'Mở khóa' : 'Tạm khóa'} Voucher ${v.code}`,
      message: nextActive ? `Kích hoạt lại mã giảm giá ${v.code}?` : `Tạm khóa mã giảm giá ${v.code}? Khách hàng sẽ không thể áp dụng mã này khi thanh toán.`,
      confirmText: nextActive ? 'Mở khóa' : 'Tạm khóa',
      danger: !nextActive,
      onConfirm: () => {
        v.isActive = nextActive;
        DB.saveDb();
        C.showToast(`Đã ${nextActive ? 'mở khóa' : 'tạm khóa'} mã ${v.code}.`);
        window.PharmacyApp.refreshCurrentRoute();
      }
    });
  }

  // ==================== 9. BÁO CÁO DOANH THU (#/admin/reports) ====================
  function renderReportsSection(el) {
    const db = DB.getDb();
    const fromStr = reportFilter.from || '2026-10-01';
    const toStr = reportFilter.to || '2026-11-03';

    const inRangeOrders = db.orders.filter(o => {
      const d = (o.orderDate || '').slice(0, 10);
      return d >= fromStr && d <= toStr;
    });

    const paidOrders = inRangeOrders.filter(o => o.paymentStatus === 'PAID' && o.orderStatus !== 'CANCELLED');
    const pendingOrders = inRangeOrders.filter(o => o.paymentStatus !== 'PAID' && o.orderStatus !== 'CANCELLED');
    const totalCollected = paidOrders.reduce((s, o) => s + Number(o.totalAmount || 0), 0);
    const totalDiscount = paidOrders.reduce((s, o) => s + Number(o.discountAmount || 0), 0);
    const totalPending = pendingOrders.reduce((s, o) => s + Number(o.totalAmount || 0), 0);

    el.innerHTML = `
      <div class="d-flex flex-wrap align-items-center justify-content-between gap-2 mb-3">
        <div>
          <h1 class="h4 fw-bold mb-1">Báo cáo Doanh thu &amp; Tổng hợp Hóa đơn</h1>
          <p class="small text-secondary mb-0">
            Chỉ tính các đơn đã thu tiền (<code>PAID</code>) vào Doanh thu thực thu. Nút xuất dữ liệu đang ở trạng thái Prototype (tải file CSV mẫu; bộ xuất Excel/PDF <code>ClosedXML</code> sẽ gắn ở Backend).
          </p>
        </div>
        <button type="button" class="btn-ph btn-ph-primary" onclick="PharmacyAdmin.exportReportPrototypeCsv()">
          ${C.icon('download', 16)} Xuất dữ liệu CSV (Prototype)
        </button>
      </div>

      <!-- Bộ lọc khoảng ngày -->
      <div class="admin-panel p-3 mb-4">
        <div class="row g-3 align-items-end">
          <div class="col-6 col-md-4">
            <label for="repFromDate" class="form-label-ph">Từ ngày</label>
            <input id="repFromDate" type="date" class="form-control-ph" value="${esc(fromStr)}" onchange="PharmacyAdmin.updateReportDate('from', this.value)" />
          </div>
          <div class="col-6 col-md-4">
            <label for="repToDate" class="form-label-ph">Đến ngày</label>
            <input id="repToDate" type="date" class="form-control-ph" value="${esc(toStr)}" onchange="PharmacyAdmin.updateReportDate('to', this.value)" />
          </div>
          <div class="col-12 col-md-4">
            <button type="button" class="btn-ph btn-ph-secondary w-100" onclick="PharmacyAdmin.resetReportDates()">Khôi phục kỳ mặc định (10/2026 – 11/2026)</button>
          </div>
        </div>
      </div>

      <!-- 4 Cột số tổng hợp bằng nhau (kế thừa bố cục DESIGNER.md) -->
      <div class="row g-3 mb-4">
        <div class="col-12 col-sm-6 col-xl-3">
          <div class="kpi-card">
            <span class="kpi-card-title">DOANH THU ĐÃ THU (PAID)</span>
            <div class="kpi-card-value tabular-nums" style="color:var(--color-primary-active);">${I18n.formatCurrency(totalCollected)}</div>
            <span class="small text-muted">Từ ${paidOrders.length} hóa đơn đã thu tiền</span>
          </div>
        </div>
        <div class="col-12 col-sm-6 col-xl-3">
          <div class="kpi-card">
            <span class="kpi-card-title">SỐ ĐƠN TRONG KỲ LỌC</span>
            <div class="kpi-card-value tabular-nums">${inRangeOrders.length} đơn</div>
            <span class="small text-muted">${paidOrders.length} đã thu • ${pendingOrders.length} chờ thu</span>
          </div>
        </div>
        <div class="col-12 col-sm-6 col-xl-3">
          <div class="kpi-card">
            <span class="kpi-card-title">TỔNG GIẢM GIÁ VOUCHER</span>
            <div class="kpi-card-value tabular-nums text-success">${I18n.formatCurrency(totalDiscount)}</div>
            <span class="small text-muted">Trên các đơn đã thanh toán</span>
          </div>
        </div>
        <div class="col-12 col-sm-6 col-xl-3">
          <div class="kpi-card">
            <span class="kpi-card-title">GIÁ TRỊ ĐƠN CHỜ THU</span>
            <div class="kpi-card-value tabular-nums text-warning">${I18n.formatCurrency(totalPending)}</div>
            <span class="small text-muted">Chưa cộng vào doanh thu đã thu</span>
          </div>
        </div>
      </div>

      <!-- Bảng danh sách hóa đơn đã thu trong kỳ -->
      <div class="admin-panel p-4">
        <h2 class="h6 fw-bold mb-3">Bảng kê Hóa đơn trong khoảng ngày (${inRangeOrders.length} đơn)</h2>
        <div class="admin-table-container">
          <table class="table-ph">
            <thead>
              <tr>
                <th>Mã đơn</th>
                <th>Ngày lập</th>
                <th>Khách hàng</th>
                <th>Kênh</th>
                <th>Thanh toán</th>
                <th class="text-end">Tiền hàng</th>
                <th class="text-end">Giảm giá</th>
                <th class="text-end">Thực thu (PAID)</th>
              </tr>
            </thead>
            <tbody>
              ${inRangeOrders.map(o => {
                const collected = (o.paymentStatus === 'PAID' && o.orderStatus !== 'CANCELLED') ? o.totalAmount : 0;
                return `
                  <tr>
                    <td class="font-mono-code fw-bold">${esc(o.code)}</td>
                    <td class="tabular-nums small">${I18n.formatDate(o.orderDate, true)}</td>
                    <td>${esc(o.customerName)}</td>
                    <td>${esc(o.channel)}</td>
                    <td>${C.renderStatusBadge('payment', o.paymentStatus)}</td>
                    <td class="text-end tabular-nums">${I18n.formatCurrency(o.subtotalAmount)}</td>
                    <td class="text-end tabular-nums text-success">−${I18n.formatCurrency(o.discountAmount)}</td>
                    <td class="text-end tabular-nums fw-bold ${collected > 0 ? 'text-teal' : 'text-muted'}">${I18n.formatCurrency(collected)}</td>
                  </tr>
                `;
              }).join('')}
            </tbody>
          </table>
        </div>
      </div>
    `;
  }

  function updateReportDate(key, val) {
    reportFilter[key] = val;
    window.PharmacyApp.refreshCurrentRoute();
  }

  function resetReportDates() {
    reportFilter = { from: '2026-10-01', to: '2026-11-03' };
    window.PharmacyApp.refreshCurrentRoute();
  }

  function exportReportPrototypeCsv() {
    const db = DB.getDb();
    const rows = [
      ['MaDon', 'NgayLap', 'KhachHang', 'Kenh', 'TrangThaiDon', 'TrangThaiThanhToan', 'TamTinh', 'GiamGia', 'ThucThu_PAID']
    ];
    db.orders.forEach(o => {
      const collected = (o.paymentStatus === 'PAID' && o.orderStatus !== 'CANCELLED') ? o.totalAmount : 0;
      rows.push([
        o.code,
        o.orderDate,
        `"${o.customerName}"`,
        o.channel,
        o.orderStatus,
        o.paymentStatus,
        o.subtotalAmount,
        o.discountAmount,
        collected
      ]);
    });
    const csvContent = '\uFEFF' + rows.map(r => r.join(',')).join('\n');
    const blob = new Blob([csvContent], { type: 'text/csv;charset=utf-8;' });
    const url = URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = `BaoCao_DoanhThu_Pharmacy_Prototype_${DB.TODAY_STR}.csv`;
    document.body.appendChild(a);
    a.click();
    a.remove();
    URL.revokeObjectURL(url);
    C.showToast('Đã tải xuống file CSV mẫu (Trạng thái Prototype — khi nối Backend sẽ dùng ClosedXML xuất Excel).');
  }

  return {
    renderAdminShell,
    toggleSidebarCollapse,
    toggleMobileSidebar,
    changeDashPeriod,
    updateProdFilter,
    resetProdFilter,
    changeProdPage,
    confirmToggleSelling,
    confirmToggleActive,
    openProductFormModal,
    handleProductImagesSelected,
    generateExtraProductImage,
    setPrimaryProductImage,
    removeProductImage,
    execRte,
    insertSampleScriptToTestSanitizer,
    handleSaveProductForm,
    openCategoryModal,
    saveCategory,
    confirmDeleteCategory,
    openSupplierModal,
    saveSupplier,
    confirmDeleteSupplier,
    switchInvTab,
    updateBatchFilter,
    confirmToggleBatchLock,
    addDraftStockInLine,
    removeDraftStockInLine,
    saveStockInReceiptNow,
    openStockInDetailModal,
    updateOrdFilter,
    resetOrdFilter,
    changeOrdPage,
    openAdminOrderModal,
    simulateConcurrentOrderEdit,
    handleSaveAdminOrder,
    setRxAdminFilter,
    openRxResponseModal,
    fillRxTemplate,
    saveRxResponse,
    updateCustFilter,
    confirmToggleCustomerLock,
    openCustomerOrdersModal,
    openVoucherModal,
    saveVoucherForm,
    confirmToggleVoucher,
    updateReportDate,
    resetReportDates,
    exportReportPrototypeCsv
  };
})();
