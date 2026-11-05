/* ==========================================================================
   storefront.js — Toàn bộ 11 Trang Khu vực Khách hàng (Storefront Views)
   1. Trang chủ (#/home)
   2. Danh sách & Tìm kiếm (#/products)
   3. Chi tiết sản phẩm (#/product/:id)
   4. Giỏ hàng (#/cart)
   5. Checkout (#/checkout)
   6. Kết quả đặt hàng (#/order-result/:id)
   7. Đăng nhập / Đăng ký / Quên mật khẩu (#/login, #/register, #/forgot-password)
   8. Tài khoản & Sổ địa chỉ (#/account)
   9. Đơn của tôi (#/my-orders)
   10. Gửi đơn thuốc tư vấn (#/prescription-upload)
   11. Chính sách/Liên hệ, 403, 404 (#/about-policy, #/403, #/404)
   ========================================================================== */

window.PharmacyStorefront = (function () {
  const DB = window.PharmacyMockDB;
  const API = window.PharmacyApiService;
  const C = window.PharmacyComponents;
  const I18n = window.PharmacyI18n;
  const esc = C.escapeHtml;

  // Trạng thái bộ lọc trang Danh sách sản phẩm
  let catalogFilterState = {
    q: '',
    categoryId: '',
    supplierId: '',
    rxMode: 'ALL',
    priceRange: 'ALL',
    inStockOnly: false,
    sort: 'featured',
    page: 1,
    pageSize: 8
  };

  // Trạng thái trang Đơn của tôi
  let myOrdersFilter = {
    status: 'ALL',
    page: 1,
    pageSize: 5
  };

  // Trạng thái ảnh đơn thuốc đang soạn
  let draftRxImages = [];

  // ==================== 1. TRANG CHỦ (#/home) — Phong cách Nhà thuốc FPT Long Châu ====================
  function renderHome(container) {
    const db = DB.getDb();
    const activeMeds = db.medicines.filter(m => m.isActive).map(m => DB.enrichMedicine(m));
    const featuredMeds = activeMeds.filter(m => m.featured && !m.isPrescription).slice(0, 4);
    const rxMeds = activeMeds.filter(m => m.isPrescription).slice(0, 4);
    const healthcareMeds = activeMeds.filter(m => [3, 4, 5, 6].includes(m.categoryId) && !m.isPrescription).slice(0, 4);

    container.innerHTML = `
      <div class="container-xxl px-3 py-4">
        <!-- Khu vực Hero Đôi (Banner Chính thức + Khối Gửi đơn thuốc tư vấn) -->
        <section class="mb-4" aria-label="Tìm nhanh thuốc và gửi đơn tư vấn">
          <div class="row g-3 align-items-stretch">
            <div class="col-12 col-lg-8">
              <div class="store-hero-banner h-100 d-flex flex-column justify-content-between">
                <div>
                  <div class="d-flex flex-wrap gap-2 mb-2">
                    <span class="status-badge status-badge-info">${C.icon('shield', 13)} Nhà thuốc chuẩn GPP • Chính hãng 100%</span>
                  </div>
                  <h1 class="h3 fw-bold mb-2" style="color:var(--color-primary-active);">
                    Chăm sóc Sức khỏe Toàn diện cho Cả Gia đình
                  </h1>
                  <p class="text-secondary mb-3" style="max-width:60ch;">
                    Đặt mua nhanh thuốc thông thường, vitamin và thiết bị y tế giao tận nơi hoặc gửi ảnh đơn thuốc để dược sĩ tư vấn chuyên môn.
                  </p>
                </div>
                <div class="d-flex flex-wrap align-items-center justify-content-between gap-3 pt-2 border-top" style="border-color:#99F6E4!important;">
                  <div class="d-flex flex-wrap gap-2">
                    <a href="#/products?rxMode=OTC" class="btn-ph btn-ph-primary">
                      ${C.icon('cart', 16)} <span>Mua thuốc trực tuyến</span>
                    </a>
                    <a href="#/prescription-upload" class="btn-ph btn-ph-rx">
                      ${C.icon('rx', 16)} <span>Gửi đơn thuốc tư vấn</span>
                    </a>
                  </div>
                  <div class="small text-secondary">
                    Mã ưu đãi: <code class="font-mono-code fw-bold" style="color:var(--color-primary-active);">GIAM10K</code> • <code class="font-mono-code fw-bold" style="color:var(--color-primary-active);">PH10</code>
                  </div>
                </div>
              </div>
            </div>
            <div class="col-12 col-lg-4">
              <div class="rx-callout-box h-100 d-flex flex-column justify-content-between">
                <div>
                  <div class="d-flex align-items-center justify-content-between mb-2">
                    <strong class="d-flex align-items-center gap-2" style="color:var(--color-primary);">
                      ${C.icon('rx', 18)} Mua thuốc theo đơn bác sĩ
                    </strong>
                    <span class="status-badge status-badge-warning">Dược sĩ tư vấn</span>
                  </div>
                  <ol class="small mb-3 ps-3 text-secondary">
                    <li class="mb-1"><strong>Bước 1:</strong> Tải lên ảnh chụp đơn thuốc rõ nét.</li>
                    <li class="mb-1"><strong>Bước 2:</strong> Nhập họ tên &amp; số điện thoại liên hệ.</li>
                    <li><strong>Bước 3:</strong> Dược sĩ liên hệ tư vấn &amp; hướng dẫn nhận thuốc.</li>
                  </ol>
                </div>
                <a href="#/prescription-upload" class="btn-ph btn-ph-primary w-100">
                  ${C.icon('upload', 16)} <span>Gửi ảnh đơn thuốc ngay</span>
                </a>
              </div>
            </div>
          </div>
        </section>

        <!-- 6 Ô Tiện ích Nhanh (Bố cục kiểu Long Châu) -->
        <section class="mb-4" aria-label="Tiện ích tra cứu nhanh">
          <div class="row g-2 g-md-3">
            <div class="col-6 col-md-4 col-xl-2">
              <a href="#/prescription-upload" class="lc-quick-tile">
                <span class="lc-quick-tile-icon orange">${C.icon('rx', 20)}</span>
                <div>
                  <div class="fw-bold small">Cần mua thuốc</div>
                  <div class="text-muted" style="font-size:11px;">Gửi đơn tư vấn nhanh</div>
                </div>
              </a>
            </div>
            <div class="col-6 col-md-4 col-xl-2">
              <a href="#/products?rxMode=OTC" class="lc-quick-tile">
                <span class="lc-quick-tile-icon">${C.icon('cart', 20)}</span>
                <div>
                  <div class="fw-bold small">Mua trực tiếp</div>
                  <div class="text-muted" style="font-size:11px;">Không cần đơn bác sĩ</div>
                </div>
              </a>
            </div>
            <div class="col-6 col-md-4 col-xl-2">
              <a href="#/products?rxMode=RX" class="lc-quick-tile">
                <span class="lc-quick-tile-icon orange">${C.icon('shield', 20)}</span>
                <div>
                  <div class="fw-bold small">Thuốc theo đơn</div>
                  <div class="text-muted" style="font-size:11px;">Cần đơn bác sĩ</div>
                </div>
              </a>
            </div>
            <div class="col-6 col-md-4 col-xl-2">
              <a href="#/my-orders" class="lc-quick-tile">
                <span class="lc-quick-tile-icon green">${C.icon('box', 20)}</span>
                <div>
                  <div class="fw-bold small">Đơn hàng của tôi</div>
                  <div class="text-muted" style="font-size:11px;">Tra cứu trạng thái</div>
                </div>
              </a>
            </div>
            <div class="col-6 col-md-4 col-xl-2">
              <a href="#/products?categoryId=5" class="lc-quick-tile">
                <span class="lc-quick-tile-icon">${C.icon('capsule', 20)}</span>
                <div>
                  <div class="fw-bold small">Vitamin &amp; Đề kháng</div>
                  <div class="text-muted" style="font-size:11px;">Bổ sung khoáng chất</div>
                </div>
              </a>
            </div>
            <div class="col-6 col-md-4 col-xl-2">
              <a href="#/products?categoryId=6" class="lc-quick-tile">
                <span class="lc-quick-tile-icon green">${C.icon('check', 20)}</span>
                <div>
                  <div class="fw-bold small">Vật tư y tế</div>
                  <div class="text-muted" style="font-size:11px;">Tủ thuốc gia đình</div>
                </div>
              </a>
            </div>
          </div>
        </section>

        <!-- Sản phẩm Mua trực tiếp Nổi bật với Dải Ribbon Teal POS -->
        <section class="lc-section-card mb-4" aria-labelledby="homeFeaturedOtcHeading">
          <div class="lc-section-ribbon">
            <div>
              <h2 class="h5 fw-bold mb-0 text-white" id="homeFeaturedOtcHeading">
                Sản phẩm bán chạy nổi bật
              </h2>
            </div>
            <a href="#/products?rxMode=OTC" class="btn-ph btn-ph-secondary btn-ph-sm">Xem tất cả</a>
          </div>
          <div class="row g-3">
            ${featuredMeds.map(med => `<div class="col-6 col-md-4 col-lg-3">${C.renderProductCard(med)}</div>`).join('')}
          </div>
        </section>

        <!-- Nhóm Thuốc cần đơn bác sĩ -->
        <section class="lc-section-card mb-4" aria-labelledby="homeRxSectionHeading">
          <div class="d-flex flex-wrap align-items-center justify-content-between gap-2 mb-3 pb-2 border-bottom">
            <div>
              <h2 class="h5 fw-bold mb-0" id="homeRxSectionHeading" style="color:var(--color-primary-active);">
                Thuốc theo đơn bác sĩ
              </h2>
              <p class="small text-secondary mb-0">Dược sĩ chuyên môn kiểm tra đơn thuốc và tư vấn trực tiếp</p>
            </div>
            <a href="#/products?rxMode=RX" class="btn-ph btn-ph-rx btn-ph-sm">Xem tất cả</a>
          </div>
          <div class="row g-3">
            ${rxMeds.map(med => `<div class="col-6 col-md-4 col-lg-3">${C.renderProductCard(med)}</div>`).join('')}
          </div>
        </section>

        <!-- Nhóm Chăm sóc sức khỏe, Tiêu hóa & Vật tư y tế -->
        <section class="lc-section-card mb-2" aria-labelledby="homeHealthcareHeading">
          <div class="d-flex flex-wrap align-items-center justify-content-between gap-2 mb-3 pb-2 border-bottom">
            <div>
              <h2 class="h5 fw-bold mb-0" id="homeHealthcareHeading" style="color:var(--color-primary-active);">
                Tủ thuốc gia đình &amp; Chăm sóc sức khỏe
              </h2>
              <p class="small text-secondary mb-0">Bổ sung điện giải, vệ sinh mũi họng và chăm sóc vết thương hàng ngày</p>
            </div>
            <a href="#/products" class="btn-ph btn-ph-secondary btn-ph-sm">Khám phá thêm</a>
          </div>
          <div class="row g-3">
            ${healthcareMeds.map(med => `<div class="col-6 col-md-4 col-lg-3">${C.renderProductCard(med)}</div>`).join('')}
          </div>
        </section>
      </div>
    `;
  }

  // ==================== 2. DANH SÁCH / TÌM KIẾM SẢN PHẨM (#/products) ====================
  function renderCatalog(container, queryParams = {}) {
    if (queryParams.q !== undefined) catalogFilterState.q = queryParams.q;
    if (queryParams.categoryId !== undefined) catalogFilterState.categoryId = queryParams.categoryId;
    if (queryParams.rxMode !== undefined) catalogFilterState.rxMode = queryParams.rxMode;
    if (queryParams.page !== undefined) catalogFilterState.page = Number(queryParams.page) || 1;

    const db = DB.getDb();

    const filterControlsHtml = (idPrefix) => `
      <div class="mb-3">
        <label for="${idPrefix}_q" class="form-label-ph">Từ khóa tìm kiếm</label>
        <input id="${idPrefix}_q" type="search" class="form-control-ph form-control-sm" placeholder="Tên thuốc, hoạt chất, mã vạch..." value="${esc(catalogFilterState.q)}" oninput="PharmacyStorefront.updateFilter('q', this.value)" />
      </div>
      <div class="mb-3">
        <label for="${idPrefix}_cat" class="form-label-ph">Nhóm thuốc</label>
        <select id="${idPrefix}_cat" class="form-select-ph" onchange="PharmacyStorefront.updateFilter('categoryId', this.value)">
          <option value="">Tất cả nhóm (${db.categories.length})</option>
          ${db.categories.map(c => `<option value="${c.id}" ${String(catalogFilterState.categoryId) === String(c.id) ? 'selected' : ''}>${esc(c.name)}</option>`).join('')}
        </select>
      </div>
      <div class="mb-3">
        <label for="${idPrefix}_rx" class="form-label-ph">Phân loại mua hàng</label>
        <select id="${idPrefix}_rx" class="form-select-ph" onchange="PharmacyStorefront.updateFilter('rxMode', this.value)">
          <option value="ALL" ${catalogFilterState.rxMode === 'ALL' ? 'selected' : ''}>Tất cả (Mua trực tiếp + Tư vấn)</option>
          <option value="OTC" ${catalogFilterState.rxMode === 'OTC' ? 'selected' : ''}>Thuốc mua trực tiếp</option>
          <option value="RX" ${catalogFilterState.rxMode === 'RX' ? 'selected' : ''}>Thuốc cần đơn bác sĩ</option>
        </select>
      </div>
      <div class="mb-3">
        <label for="${idPrefix}_price" class="form-label-ph">Khoảng giá theo đơn vị</label>
        <select id="${idPrefix}_price" class="form-select-ph" onchange="PharmacyStorefront.updateFilter('priceRange', this.value)">
          <option value="ALL" ${catalogFilterState.priceRange === 'ALL' ? 'selected' : ''}>Mọi mức giá</option>
          <option value="UNDER_50K" ${catalogFilterState.priceRange === 'UNDER_50K' ? 'selected' : ''}>Dưới 50.000 ₫</option>
          <option value="50K_100K" ${catalogFilterState.priceRange === '50K_100K' ? 'selected' : ''}>Từ 50.000 ₫ – 100.000 ₫</option>
          <option value="OVER_100K" ${catalogFilterState.priceRange === 'OVER_100K' ? 'selected' : ''}>Trên 100.000 ₫</option>
        </select>
      </div>
      <div class="mb-3">
        <label for="${idPrefix}_sup" class="form-label-ph">Nhà cung cấp</label>
        <select id="${idPrefix}_sup" class="form-select-ph" onchange="PharmacyStorefront.updateFilter('supplierId', this.value)">
          <option value="">Tất cả nhà cung cấp</option>
          ${db.suppliers.map(s => `<option value="${s.id}" ${String(catalogFilterState.supplierId) === String(s.id) ? 'selected' : ''}>${esc(s.name)}</option>`).join('')}
        </select>
      </div>
      <div class="form-check mb-3">
        <input class="form-check-input" type="checkbox" id="${idPrefix}_stock" ${catalogFilterState.inStockOnly ? 'checked' : ''} onchange="PharmacyStorefront.updateFilter('inStockOnly', this.checked)" />
        <label class="form-check-label small fw-medium" for="${idPrefix}_stock">
          Chỉ hiện thuốc đang còn tồn bán
        </label>
      </div>
      <button type="button" class="btn-ph btn-ph-secondary w-100" onclick="PharmacyStorefront.resetCatalogFilters()">
        ${C.icon('refresh', 14)} Xóa toàn bộ lọc
      </button>
    `;

    container.innerHTML = `
      <div class="container-xxl px-3 py-4">
        <nav aria-label="breadcrumb" class="mb-3">
          <ol class="breadcrumb small mb-0">
            <li class="breadcrumb-item"><a href="#/home" class="text-decoration-none">Trang chủ</a></li>
            <li class="breadcrumb-item active" aria-current="page">Danh sách &amp; Tìm kiếm sản phẩm</li>
          </ol>
        </nav>

        <div class="row g-4">
          <!-- Bộ lọc Desktop -->
          <aside class="col-lg-3 d-none d-lg-block" aria-label="Bộ lọc sản phẩm">
            <div class="bg-white p-3 rounded-3 border" style="border-color:var(--color-border-subtle);">
              <h2 class="h6 fw-bold mb-3 d-flex align-items-center gap-2">
                ${C.icon('filter', 16)} Bộ lọc tìm kiếm
              </h2>
              ${filterControlsHtml('deskFilter')}
            </div>
          </aside>

          <!-- Kết quả Lưới sản phẩm -->
          <div class="col-12 col-lg-9">
            <div class="bg-white p-3 rounded-3 border mb-3 d-flex flex-wrap align-items-center justify-content-between gap-2" style="border-color:var(--color-border-subtle);">
              <div class="d-flex align-items-center gap-2">
                <button type="button" class="btn-ph btn-ph-secondary btn-ph-sm d-lg-none" onclick="PharmacyStorefront.openMobileFilterDrawer()">
                  ${C.icon('filter', 15)} Bộ lọc
                </button>
                <h1 class="h6 fw-bold mb-0" id="catalogResultCountText">Đang tải danh sách thuốc...</h1>
              </div>
              <div class="d-flex align-items-center gap-2">
                <label for="catalogSortSelect" class="small text-secondary text-nowrap mb-0">Sắp xếp:</label>
                <select id="catalogSortSelect" class="form-select-ph py-1 px-2" style="min-height:36px;width:auto;font-size:14px;" onchange="PharmacyStorefront.updateFilter('sort', this.value)">
                  <option value="featured" ${catalogFilterState.sort === 'featured' ? 'selected' : ''}>Nổi bật / Đề xuất</option>
                  <option value="price_asc" ${catalogFilterState.sort === 'price_asc' ? 'selected' : ''}>Giá tăng dần</option>
                  <option value="price_desc" ${catalogFilterState.sort === 'price_desc' ? 'selected' : ''}>Giá giảm dần</option>
                  <option value="name_asc" ${catalogFilterState.sort === 'name_asc' ? 'selected' : ''}>Tên A → Z</option>
                </select>
              </div>
            </div>

            <!-- Thanh Chip bộ lọc đang áp dụng -->
            <div id="activeFilterChipsBar" class="mb-3"></div>

            <!-- Vùng hiển thị kết quả AJAX -->
            <div id="catalogGridContainer" aria-live="polite">
              ${C.renderSkeletonCards(8)}
            </div>
          </div>
        </div>
      </div>

      <!-- Mobile Filter Drawer -->
      <div id="mobileFilterDrawerRoot"></div>
    `;

    loadCatalogResults();
  }

  function renderActiveFilterChips() {
    const bar = document.getElementById('activeFilterChipsBar');
    if (!bar) return;
    const db = DB.getDb();
    const chips = [];

    if (catalogFilterState.q) {
      chips.push({ label: `Từ khóa: "${catalogFilterState.q}"`, key: 'q', val: '' });
    }
    if (catalogFilterState.categoryId) {
      const cat = db.categories.find(c => String(c.id) === String(catalogFilterState.categoryId));
      chips.push({ label: `Nhóm: ${cat ? cat.name : catalogFilterState.categoryId}`, key: 'categoryId', val: '' });
    }
    if (catalogFilterState.rxMode !== 'ALL') {
      chips.push({ label: catalogFilterState.rxMode === 'OTC' ? 'Chỉ thuốc mua trực tiếp' : 'Chỉ thuốc cần đơn bác sĩ', key: 'rxMode', val: 'ALL' });
    }
    if (catalogFilterState.priceRange !== 'ALL') {
      const map = { UNDER_50K: '< 50.000 ₫', '50K_100K': '50.000 – 100.000 ₫', OVER_100K: '> 100.000 ₫' };
      chips.push({ label: `Giá: ${map[catalogFilterState.priceRange]}`, key: 'priceRange', val: 'ALL' });
    }
    if (catalogFilterState.supplierId) {
      const sup = db.suppliers.find(s => String(s.id) === String(catalogFilterState.supplierId));
      chips.push({ label: `NCC: ${sup ? sup.name : catalogFilterState.supplierId}`, key: 'supplierId', val: '' });
    }
    if (catalogFilterState.inStockOnly) {
      chips.push({ label: 'Còn tồn có thể bán', key: 'inStockOnly', val: false });
    }

    if (chips.length === 0) {
      bar.innerHTML = '';
      return;
    }

    bar.innerHTML = `
      <div class="d-flex flex-wrap align-items-center gap-2">
        <span class="small text-secondary">Đang lọc theo:</span>
        ${chips.map(ch => `
          <button type="button" class="status-badge status-badge-info border-0" style="cursor:pointer;" onclick="PharmacyStorefront.updateFilter('${ch.key}', ${typeof ch.val === 'boolean' ? ch.val : `'${ch.val}'`})">
            <span>${esc(ch.label)}</span> <span aria-hidden="true">×</span>
          </button>
        `).join('')}
        <button type="button" class="btn btn-link btn-sm text-danger text-decoration-none p-0 ms-1" onclick="PharmacyStorefront.resetCatalogFilters()">
          Xóa tất cả
        </button>
      </div>
    `;
  }

  function loadCatalogResults() {
    const grid = document.getElementById('catalogGridContainer');
    const countEl = document.getElementById('catalogResultCountText');
    if (!grid) return;

    renderActiveFilterChips();
    grid.innerHTML = C.renderSkeletonCards(4);

    API.getProducts(catalogFilterState)
      .then(res => {
        if (countEl) {
          countEl.innerHTML = `Tìm thấy <span class="text-teal fw-bold tabular-nums">${res.totalItems}</span> sản phẩm phù hợp`;
        }
        if (res.items.length === 0) {
          grid.innerHTML = `
            <div class="bg-white p-5 rounded-3 border text-center">
              <div class="mb-2 text-secondary">${C.icon('search', 36)}</div>
              <h2 class="h6 fw-bold">Không tìm thấy sản phẩm khớp với bộ lọc hiện tại</h2>
              <p class="small text-secondary mb-3">Thử xóa bớt điều kiện lọc hoặc tìm theo tên hoạt chất (ví dụ: Paracetamol, Vitamin C).</p>
              <button type="button" class="btn-ph btn-ph-primary" onclick="PharmacyStorefront.resetCatalogFilters()">
                Xóa bộ lọc &amp; Xem lại tất cả
              </button>
            </div>
          `;
          return;
        }

        grid.innerHTML = `
          <div class="row g-3">
            ${res.items.map(med => `<div class="col-6 col-md-4 col-xl-3">${C.renderProductCard(med)}</div>`).join('')}
          </div>
          ${C.renderPagination(res, 'PharmacyStorefront.changeCatalogPage')}
        `;
      })
      .catch(err => {
        grid.innerHTML = `
          <div class="bg-white p-4 rounded-3 border border-danger text-center">
            <div class="text-danger mb-2">${C.icon('alert', 32)}</div>
            <h2 class="h6 fw-bold text-danger">Không thể tải danh sách sản phẩm</h2>
            <p class="small text-secondary mb-3">${esc(err.message || 'Lỗi kết nối dịch vụ.')}</p>
            <button type="button" class="btn-ph btn-ph-primary" onclick="PharmacyStorefront.loadCatalogResults()">
              ${C.icon('refresh', 15)} Thử tải lại
            </button>
          </div>
        `;
      });
  }

  function updateFilter(key, value) {
    catalogFilterState[key] = value;
    if (key !== 'page') catalogFilterState.page = 1;
    loadCatalogResults();
  }

  function changeCatalogPage(page) {
    catalogFilterState.page = Number(page) || 1;
    loadCatalogResults();
    window.scrollTo({ top: 120, behavior: 'smooth' });
  }

  function resetCatalogFilters() {
    catalogFilterState = {
      q: '',
      categoryId: '',
      supplierId: '',
      rxMode: 'ALL',
      priceRange: 'ALL',
      inStockOnly: false,
      sort: 'featured',
      page: 1,
      pageSize: 8
    };
    const root = document.getElementById('appMainView');
    if (root) renderCatalog(root, {});
  }

  function openMobileFilterDrawer() {
    const db = DB.getDb();
    const drawerRoot = document.getElementById('mobileFilterDrawerRoot');
    if (!drawerRoot) return;
    drawerRoot.innerHTML = `
      <div class="mobile-drawer-backdrop" onclick="PharmacyStorefront.closeMobileFilterDrawer()"></div>
      <div class="mobile-drawer-panel" role="dialog" aria-modal="true" aria-label="Bộ lọc sản phẩm">
        <div class="p-3 border-bottom d-flex align-items-center justify-content-between">
          <strong class="d-flex align-items-center gap-2">${C.icon('filter', 16)} Bộ lọc tìm kiếm</strong>
          <button type="button" class="btn-close" aria-label="Đóng bộ lọc" onclick="PharmacyStorefront.closeMobileFilterDrawer()"></button>
        </div>
        <div class="p-3 overflow-auto flex-grow-1">
          <div class="mb-3">
            <label for="mob_cat" class="form-label-ph">Nhóm thuốc</label>
            <select id="mob_cat" class="form-select-ph" onchange="PharmacyStorefront.updateFilter('categoryId', this.value)">
              <option value="">Tất cả nhóm</option>
              ${db.categories.map(c => `<option value="${c.id}" ${String(catalogFilterState.categoryId) === String(c.id) ? 'selected' : ''}>${esc(c.name)}</option>`).join('')}
            </select>
          </div>
          <div class="mb-3">
            <label for="mob_rx" class="form-label-ph">Phân loại</label>
            <select id="mob_rx" class="form-select-ph" onchange="PharmacyStorefront.updateFilter('rxMode', this.value)">
              <option value="ALL" ${catalogFilterState.rxMode === 'ALL' ? 'selected' : ''}>Tất cả</option>
              <option value="OTC" ${catalogFilterState.rxMode === 'OTC' ? 'selected' : ''}>Thuốc mua trực tiếp</option>
              <option value="RX" ${catalogFilterState.rxMode === 'RX' ? 'selected' : ''}>Thuốc cần đơn bác sĩ</option>
            </select>
          </div>
          <div class="mb-3">
            <label for="mob_price" class="form-label-ph">Khoảng giá</label>
            <select id="mob_price" class="form-select-ph" onchange="PharmacyStorefront.updateFilter('priceRange', this.value)">
              <option value="ALL" ${catalogFilterState.priceRange === 'ALL' ? 'selected' : ''}>Mọi mức giá</option>
              <option value="UNDER_50K" ${catalogFilterState.priceRange === 'UNDER_50K' ? 'selected' : ''}>Dưới 50.000 ₫</option>
              <option value="50K_100K" ${catalogFilterState.priceRange === '50K_100K' ? 'selected' : ''}>50.000 – 100.000 ₫</option>
              <option value="OVER_100K" ${catalogFilterState.priceRange === 'OVER_100K' ? 'selected' : ''}>Trên 100.000 ₫</option>
            </select>
          </div>
          <div class="form-check mb-3">
            <input class="form-check-input" type="checkbox" id="mob_stock" ${catalogFilterState.inStockOnly ? 'checked' : ''} onchange="PharmacyStorefront.updateFilter('inStockOnly', this.checked)" />
            <label class="form-check-label small" for="mob_stock">Chỉ hiện thuốc còn tồn bán</label>
          </div>
        </div>
        <div class="p-3 border-top d-flex gap-2">
          <button type="button" class="btn-ph btn-ph-secondary flex-grow-1" onclick="PharmacyStorefront.resetCatalogFilters()">Xóa lọc</button>
          <button type="button" class="btn-ph btn-ph-primary flex-grow-1" onclick="PharmacyStorefront.closeMobileFilterDrawer()">Áp dụng</button>
        </div>
      </div>
    `;
  }

  function closeMobileFilterDrawer() {
    const drawerRoot = document.getElementById('mobileFilterDrawerRoot');
    if (drawerRoot) drawerRoot.innerHTML = '';
  }

  // ==================== 3. CHI TIẾT SẢN PHẨM (#/product/:id) ====================
  function renderProductDetail(container, productId) {
    container.innerHTML = `<div class="container-xxl px-3 py-4">${C.renderSkeletonCards(4)}</div>`;

    API.getProductById(productId)
      .then(({ product, relatedProducts }) => {
        const isRx = Boolean(product.isPrescription);
        const isSelling = Boolean(product.isSelling);
        const avail = Number(product.availableStock) || 0;

        let stockStatusHtml = '';
        if (!isSelling) {
          stockStatusHtml = `<span class="status-badge status-badge-danger">${C.icon('xCircle', 14)} Ngừng kinh doanh tạm thời</span>`;
        } else if (avail <= 0) {
          stockStatusHtml = `<span class="status-badge status-badge-neutral">${C.icon('alert', 14)} Tạm hết hàng có thể bán</span>`;
        } else if (avail <= 5) {
          stockStatusHtml = `<span class="status-badge status-badge-warning">${C.icon('clock', 14)} Sắp hết hàng (Còn ${avail} ${esc(product.unit)})</span>`;
        } else {
          stockStatusHtml = `<span class="status-badge status-badge-success">${C.icon('check', 14)} Còn hàng (${avail} ${esc(product.unit)} có thể bán)</span>`;
        }

        let actionBoxHtml = '';
        if (!isSelling) {
          actionBoxHtml = `
            <div class="p-3 rounded-3 mb-3" style="background:var(--color-danger-bg);color:var(--color-danger);border:1px solid var(--color-danger-border);">
              <strong>Sản phẩm đang ngừng bán:</strong> Thuốc này hiện đang tạm ngừng xuất bán trên hệ thống. Không thể thêm vào giỏ hàng.
            </div>
            <button type="button" class="btn-ph w-100" disabled>${C.icon('xCircle', 16)} Ngừng kinh doanh</button>
          `;
        } else if (isRx) {
          actionBoxHtml = `
            <div class="rx-callout-box mb-3">
              <div class="fw-bold mb-1 d-flex align-items-center gap-2">
                ${C.icon('rx', 18)} Thuốc kê đơn — Cần đơn thuốc &amp; tư vấn dược sĩ
              </div>
              <p class="small mb-2">
                Theo quy tắc nghiệp vụ, thuốc kê đơn không tự động thanh toán chung trong giỏ hàng mua trực tiếp. Vui lòng gửi ảnh chụp đơn thuốc để dược sĩ đối chiếu và liên hệ hướng dẫn.
              </p>
              <a href="#/prescription-upload?medicineId=${product.id}" class="btn-ph btn-ph-primary w-100">
                ${C.icon('rx', 16)} Gửi đơn thuốc / Yêu cầu tư vấn cho sản phẩm này
              </a>
            </div>
          `;
        } else if (avail <= 0) {
          actionBoxHtml = `
            <div class="p-3 rounded-3 mb-3" style="background:var(--color-warning-bg);color:var(--color-warning);border:1px solid var(--color-warning-border);">
              <strong>Tạm hết tồn có thể bán:</strong> Các lô hàng hiện tại của sản phẩm đã hết hoặc đang tạm khóa kiểm tra.
            </div>
            <button type="button" class="btn-ph w-100" disabled>Tạm hết hàng</button>
          `;
        } else {
          actionBoxHtml = `
            <div class="d-flex flex-wrap align-items-end gap-3 mb-3">
              <div>
                <label for="detailQtyInput" class="form-label-ph">Số lượng (${esc(product.unit)})</label>
                <div class="input-group" style="width:148px;">
                  <button class="btn btn-outline-secondary" type="button" aria-label="Giảm số lượng" onclick="PharmacyStorefront.stepDetailQty(-1, ${avail})">−</button>
                  <input id="detailQtyInput" type="number" class="form-control text-center tabular-nums" value="1" min="1" max="${avail}" aria-label="Số lượng mua" />
                  <button class="btn btn-outline-secondary" type="button" aria-label="Tăng số lượng" onclick="PharmacyStorefront.stepDetailQty(1, ${avail})">+</button>
                </div>
              </div>
              <div class="flex-grow-1 d-flex flex-wrap gap-2">
                <button type="button" class="btn-ph btn-ph-primary flex-grow-1" onclick="PharmacyStorefront.addDetailToCart(${product.id}, false)">
                  ${C.icon('cart', 16)} Thêm vào giỏ hàng
                </button>
                <button type="button" class="btn-ph btn-ph-mint" onclick="PharmacyStorefront.addDetailToCart(${product.id}, true)">
                  Đặt mua ngay
                </button>
              </div>
            </div>
          `;
        }

        container.innerHTML = `
          <div class="container-xxl px-3 py-4">
            <nav aria-label="breadcrumb" class="mb-3">
              <ol class="breadcrumb small mb-0">
                <li class="breadcrumb-item"><a href="#/home" class="text-decoration-none">Trang chủ</a></li>
                <li class="breadcrumb-item"><a href="#/products?categoryId=${product.categoryId}" class="text-decoration-none">${esc(product.categoryName)}</a></li>
                <li class="breadcrumb-item active" aria-current="page">${esc(product.name)}</li>
              </ol>
            </nav>

            <div class="bg-white rounded-3 border p-4 mb-4" style="border-color:var(--color-border-subtle);">
              <div class="row g-4">
                <!-- Cột Ảnh sản phẩm -->
                <div class="col-12 col-md-5">
                  <div class="p-4 rounded-3 border text-center mb-3" style="background:#F8FAFC;border-color:var(--color-border-subtle);">
                    <img id="detailMainImage" src="${product.primaryImage}" alt="${esc(product.name)}" class="img-fluid" style="max-height:260px;object-fit:contain;" />
                  </div>
                  <div class="d-flex gap-2 overflow-auto pb-1">
                    ${product.images.map((img, i) => `
                      <button type="button" class="p-1 rounded border bg-white" style="width:68px;height:54px;" onclick="document.getElementById('detailMainImage').src='${img.url}'" aria-label="Xem ảnh ${i + 1}">
                        <img src="${img.url}" alt="" class="w-100 h-100" style="object-fit:contain;" />
                      </button>
                    `).join('')}
                  </div>
                </div>

                <!-- Cột Thông tin & CTA -->
                <div class="col-12 col-md-7">
                  <div class="d-flex flex-wrap align-items-center gap-2 mb-2">
                    ${isRx
                      ? `<span class="status-badge status-badge-info">${C.icon('rx', 13)} Thuốc cần đơn bác sĩ</span>`
                      : `<span class="status-badge status-badge-success">${C.icon('check', 13)} Mua trực tiếp</span>`}
                    ${stockStatusHtml}
                    <span class="font-mono-code text-muted ms-auto">Mã vạch: ${esc(product.barCode || 'N/A')}</span>
                  </div>

                  <h1 class="h3 fw-bold mb-2">${esc(product.name)}</h1>

                  <div class="p-3 rounded-3 mb-3" style="background:var(--color-primary-soft-bg);border:1px solid #CCFBF1;">
                    <div class="d-flex align-items-baseline gap-2">
                      <span class="h3 fw-bold mb-0 tabular-nums" style="color:var(--color-primary-active);">${I18n.formatCurrency(product.price)}</span>
                      <span class="fw-semibold text-secondary">/ ${esc(product.unit)}</span>
                    </div>
                  </div>

                  <dl class="row small mb-3 g-2">
                    <dt class="col-sm-4 text-secondary">Thành phần chính:</dt>
                    <dd class="col-sm-8 fw-semibold mb-0">${esc(product.activeIngredient || 'Đang cập nhật')}</dd>
                    <dt class="col-sm-4 text-secondary">Nhóm danh mục:</dt>
                    <dd class="col-sm-8 mb-0">${esc(product.categoryName)}</dd>
                    <dt class="col-sm-4 text-secondary">Nhà cung cấp:</dt>
                    <dd class="col-sm-8 mb-0">${esc(product.supplierName)}</dd>
                    <dt class="col-sm-4 text-secondary">Quy cách đóng gói:</dt>
                    <dd class="col-sm-8 mb-0">${esc(product.packagingSpec || product.unit)}</dd>
                  </dl>

                  ${actionBoxHtml}
                </div>
              </div>
            </div>

            <!-- Mô tả chi tiết -->
            <div class="bg-white rounded-3 border p-4 mb-4" style="border-color:var(--color-border-subtle);">
              <h2 class="h5 fw-bold mb-3">Thông tin mô tả &amp; Hướng dẫn bảo quản</h2>
              <div class="text-secondary" style="max-width:72ch;line-height:1.7;">
                ${API.sanitizeRichHtml(product.descriptionHtml || '<p>Chưa có mô tả chi tiết cho sản phẩm này.</p>')}
              </div>
              <div class="mt-3 p-3 rounded-2 small" style="background:var(--color-bg-subtle);color:var(--color-text-secondary);">
                <strong>Lưu ý:</strong> Thông tin sản phẩm trên website chỉ mang tính chất tham khảo, không thay thế chẩn đoán hoặc chỉ định của bác sĩ. Vui lòng đọc kỹ hướng dẫn sử dụng trước khi dùng.
              </div>
            </div>

            <!-- Sản phẩm liên quan cùng nhóm -->
            <section aria-labelledby="relatedProductsHeading">
              <h2 class="h5 fw-bold mb-3" id="relatedProductsHeading">Sản phẩm liên quan cùng nhóm</h2>
              <div class="row g-3">
                ${relatedProducts.length
                  ? relatedProducts.map(m => `<div class="col-6 col-md-3">${C.renderProductCard(m)}</div>`).join('')
                  : `<div class="col-12 text-muted small">Không có sản phẩm liên quan khác trong nhóm này.</div>`}
              </div>
            </section>
          </div>
        `;
      })
      .catch(err => {
        container.innerHTML = `
          <div class="container-xxl px-3 py-5 text-center">
            <div class="bg-white p-5 rounded-3 border mx-auto" style="max-width:520px;">
              <div class="text-danger mb-2">${C.icon('alert', 36)}</div>
              <h1 class="h5 fw-bold">Không tìm thấy sản phẩm</h1>
              <p class="small text-secondary mb-3">${esc(err.message)}</p>
              <a href="#/products" class="btn-ph btn-ph-primary">Quay lại danh sách sản phẩm</a>
            </div>
          </div>
        `;
      });
  }

  function stepDetailQty(delta, maxAvail) {
    const inp = document.getElementById('detailQtyInput');
    if (!inp) return;
    let val = (Number(inp.value) || 1) + delta;
    if (val < 1) val = 1;
    if (val > maxAvail) val = maxAvail;
    inp.value = val;
  }

  function addDetailToCart(medId, goToCart) {
    const inp = document.getElementById('detailQtyInput');
    const qty = inp ? Math.max(1, Number(inp.value) || 1) : 1;
    window.PharmacyApp.addToCart(medId, qty);
    if (goToCart) {
      window.location.hash = '#/cart';
    }
  }

  // ==================== 4. GIỎ HÀNG (#/cart) ====================
  function renderCart(container) {
    const session = DB.getSession();
    const db = DB.getDb();
    const cartItems = session.cart || [];

    let subtotal = 0;
    const stockPriceNotices = [];

    const enrichedCart = cartItems.map(item => {
      const rawMed = db.medicines.find(m => m.id === item.medicineId);
      const med = DB.enrichMedicine(rawMed);
      if (!med) return null;

      let effectiveAvail = med.availableStock;
      if (session.cartSimulationMode === 'STOCK_SHORTAGE') {
        effectiveAvail = Math.max(0, item.quantity - 1);
      }

      let effectivePrice = med.price;
      if (session.cartSimulationMode === 'PRICE_CHANGED') {
        effectivePrice = med.price + 4000;
      }

      if (item.addedPrice && item.addedPrice !== effectivePrice) {
        stockPriceNotices.push(`Giá của "${med.name}" đã thay đổi từ ${I18n.formatCurrency(item.addedPrice)} sang ${I18n.formatCurrency(effectivePrice)} / ${med.unit}.`);
      }
      if (!med.isSelling || !med.isActive) {
        stockPriceNotices.push(`Sản phẩm "${med.name}" hiện đã ngừng bán.`);
      } else if (item.quantity > effectiveAvail) {
        stockPriceNotices.push(`Sản phẩm "${med.name}" chỉ còn ${effectiveAvail} ${med.unit} có thể bán (trong giỏ đang có ${item.quantity}).`);
      }

      const lineTotal = effectivePrice * item.quantity;
      subtotal += lineTotal;

      return {
        medicineId: med.id,
        med,
        quantity: item.quantity,
        effectivePrice,
        effectiveAvail,
        lineTotal
      };
    }).filter(Boolean);

    let discountAmount = 0;
    let voucherFeedbackHtml = '';
    if (session.appliedVoucherCode && enrichedCart.length > 0) {
      const vEval = API.evaluateVoucher(session.appliedVoucherCode, subtotal);
      if (vEval.valid) {
        discountAmount = vEval.discountAmount;
        voucherFeedbackHtml = `
          <div class="p-2 rounded-2 small mt-2 d-flex align-items-center justify-content-between gap-2" style="background:var(--color-success-bg);color:var(--color-success);border:1px solid var(--color-success-border);">
            <span>${C.icon('check', 14)} ${esc(vEval.message)} ${vEval.cappedWarning ? `<br/><em>(${esc(vEval.cappedWarning)})</em>` : ''}</span>
            <button type="button" class="btn btn-sm btn-link text-danger text-decoration-none p-0 text-nowrap flex-shrink-0" onclick="PharmacyStorefront.removeVoucher()">Bỏ mã</button>
          </div>
        `;
      } else {
        voucherFeedbackHtml = `
          <div class="p-2 rounded-2 small mt-2 d-flex align-items-center justify-content-between gap-2" style="background:var(--color-danger-bg);color:var(--color-danger);border:1px solid var(--color-danger-border);">
            <span>${C.icon('alert', 14)} ${esc(vEval.message)}</span>
            <button type="button" class="btn btn-sm btn-link text-danger text-decoration-none p-0 text-nowrap flex-shrink-0" onclick="PharmacyStorefront.removeVoucher()">Xóa</button>
          </div>
        `;
      }
    }

    const shippingFee = enrichedCart.length === 0 ? 0 : (subtotal - discountAmount >= 150000 ? 0 : 15000);
    const finalTotal = Math.max(0, subtotal - discountAmount + shippingFee);

    container.innerHTML = `
      <div class="container-xxl px-3 py-4">
        <div class="d-flex flex-wrap align-items-center justify-content-between gap-2 mb-3">
          <div>
            <h1 class="h4 fw-bold mb-0">${I18n.t('cart_title')}</h1>
          </div>
          <a href="#/products" class="small fw-semibold text-decoration-none" style="color:var(--color-primary);">← Tiếp tục mua thuốc</a>
        </div>

        ${stockPriceNotices.length > 0 ? `
          <div class="p-3 rounded-3 mb-3" role="alert" style="background:var(--color-warning-bg);color:var(--color-warning);border:1px solid var(--color-warning-border);">
            <div class="fw-bold mb-1 d-flex align-items-center gap-2">
              ${C.icon('alert', 18)} Thông báo cập nhật giỏ hàng:
            </div>
            <ul class="small mb-0 ps-3">
              ${stockPriceNotices.map(msg => `<li>${esc(msg)}</li>`).join('')}
            </ul>
          </div>
        ` : ''}

        ${enrichedCart.length === 0 ? `
          <div class="bg-white rounded-3 border p-5 text-center my-2">
            <div class="mb-3 text-muted">${C.icon('cart', 44)}</div>
            <h2 class="h5 fw-bold mb-2">${I18n.t('cart_empty')}</h2>
            <p class="small text-secondary mb-4">Bạn chưa có sản phẩm nào trong giỏ hàng.</p>
            <div class="d-flex flex-wrap justify-content-center gap-2">
              <a href="#/products" class="btn-ph btn-ph-primary">Khám phá sản phẩm</a>
            </div>
          </div>
        ` : `
          <div class="row g-4">
            <!-- Danh sách dòng hàng -->
            <div class="col-12 col-lg-8">
              <div class="bg-white rounded-3 border overflow-hidden" style="border-color:var(--color-border-subtle);">
                <div class="table-responsive">
                  <table class="table-ph">
                    <thead>
                      <tr>
                        <th>Sản phẩm &amp; Đơn vị</th>
                        <th class="text-end">Đơn giá</th>
                        <th class="text-center" style="width:150px;">Số lượng</th>
                        <th class="text-end">Thành tiền</th>
                        <th class="text-center">Xóa</th>
                      </tr>
                    </thead>
                    <tbody>
                      ${enrichedCart.map(row => `
                        <tr>
                          <td>
                            <div class="d-flex align-items-center gap-3">
                              <img src="${row.med.primaryImage}" alt="" width="56" height="42" class="rounded border bg-light" style="object-fit:contain;" />
                              <div>
                                <a href="#/product/${row.med.id}" class="fw-semibold text-dark text-decoration-none d-block">${esc(row.med.name)}</a>
                                <span class="small text-muted">Đơn vị tính: <strong>${esc(row.med.unit)}</strong></span>
                              </div>
                            </div>
                          </td>
                          <td class="text-end tabular-nums fw-medium">${I18n.formatCurrency(row.effectivePrice)}</td>
                          <td class="text-center">
                            <div class="input-group input-group-sm mx-auto" style="width:120px;">
                              <button type="button" class="btn btn-outline-secondary" aria-label="Giảm số lượng ${esc(row.med.name)}" onclick="PharmacyStorefront.updateCartItemQty(${row.medicineId}, ${row.quantity - 1})">−</button>
                              <input type="number" class="form-control text-center tabular-nums" value="${row.quantity}" min="1" max="99" aria-label="Số lượng ${esc(row.med.name)}" onchange="PharmacyStorefront.updateCartItemQty(${row.medicineId}, this.value)" />
                              <button type="button" class="btn btn-outline-secondary" aria-label="Tăng số lượng ${esc(row.med.name)}" onclick="PharmacyStorefront.updateCartItemQty(${row.medicineId}, ${row.quantity + 1})">+</button>
                            </div>
                          </td>
                          <td class="text-end tabular-nums fw-bold" style="color:var(--color-primary-active);">${I18n.formatCurrency(row.lineTotal)}</td>
                          <td class="text-center">
                            <button type="button" class="btn-ph btn-ph-danger-outline btn-ph-sm" aria-label="Xóa ${esc(row.med.name)} khỏi giỏ" onclick="PharmacyStorefront.confirmRemoveCartItem(${row.medicineId}, '${esc(row.med.name)}')">
                              ${C.icon('trash', 14)}
                            </button>
                          </td>
                        </tr>
                      `).join('')}
                    </tbody>
                  </table>
                </div>
              </div>
            </div>

            <!-- Cột Voucher & Tổng tiền -->
            <div class="col-12 col-lg-4">
              <div class="bg-white rounded-3 border p-4" style="border-color:var(--color-border-subtle);">
                <h2 class="h6 fw-bold mb-3">Mã ưu đãi / Giảm giá</h2>
                <form onsubmit="PharmacyStorefront.handleApplyVoucherForm(event)" class="d-flex gap-2">
                  <label for="cartVoucherInput" class="visually-hidden">Nhập mã voucher</label>
                  <input id="cartVoucherInput" type="text" class="form-control-ph font-mono-code text-uppercase" placeholder="Nhập mã ưu đãi" value="${esc(session.appliedVoucherCode || '')}" />
                  <button type="submit" class="btn-ph btn-ph-secondary">Áp dụng</button>
                </form>
                ${voucherFeedbackHtml}

                <div class="mt-2 pt-2 border-top small">
                  <div class="text-muted mb-1">Mã giảm giá hiện có:</div>
                  <div class="d-flex flex-wrap gap-1">
                    <button type="button" class="badge bg-light text-dark border font-mono-code" style="cursor:pointer;" onclick="PharmacyStorefront.applyVoucherQuick('GIAM10K')">GIAM10K (-10K)</button>
                    <button type="button" class="badge bg-light text-dark border font-mono-code" style="cursor:pointer;" onclick="PharmacyStorefront.applyVoucherQuick('PH10')">PH10 (-10%)</button>
                    <button type="button" class="badge bg-light text-dark border font-mono-code" style="cursor:pointer;" onclick="PharmacyStorefront.applyVoucherQuick('PH25K')">PH25K (-25K)</button>
                  </div>
                </div>

                <hr class="my-3" />
                <h2 class="h6 fw-bold mb-3">Tóm tắt đơn hàng</h2>
                <div class="d-flex justify-content-between small mb-2">
                  <span class="text-secondary">Tạm tính (${enrichedCart.reduce((s, i) => s + i.quantity, 0)} sản phẩm):</span>
                  <span class="fw-semibold tabular-nums">${I18n.formatCurrency(subtotal)}</span>
                </div>
                <div class="d-flex justify-content-between small mb-2">
                  <span class="text-secondary">Giảm giá voucher:</span>
                  <span class="fw-semibold text-success tabular-nums">−${I18n.formatCurrency(discountAmount)}</span>
                </div>
                <div class="d-flex justify-content-between small mb-3">
                  <span class="text-secondary">Phí giao hàng dự kiến:</span>
                  <span class="fw-semibold tabular-nums">${shippingFee === 0 ? 'Miễn phí (0 ₫)' : I18n.formatCurrency(shippingFee)}</span>
                </div>
                <div class="d-flex justify-content-between align-items-baseline pt-3 border-top mb-3">
                  <span class="fw-bold">Tổng thanh toán:</span>
                  <span class="h4 fw-bold mb-0 tabular-nums" style="color:var(--color-primary-active);">${I18n.formatCurrency(finalTotal)}</span>
                </div>

                <a href="#/checkout" class="btn-ph btn-ph-primary w-100">
                  <span>${I18n.t('btn_checkout')}</span>
                </a>
              </div>
            </div>
          </div>
        `}
      </div>
    `;
  }

  function updateCartItemQty(medId, newQty) {
    const session = DB.getSession();
    const qty = Number(newQty);
    if (qty <= 0) {
      const med = DB.getDb().medicines.find(m => m.id === Number(medId));
      confirmRemoveCartItem(medId, med ? med.name : 'sản phẩm');
      return;
    }
    const item = session.cart.find(i => i.medicineId === Number(medId));
    if (item) {
      item.quantity = Math.min(99, qty);
      DB.saveSession();
      window.PharmacyApp.refreshCurrentRoute();
    }
  }

  function confirmRemoveCartItem(medId, name) {
    C.openConfirmModal({
      title: 'Xóa sản phẩm khỏi giỏ hàng',
      message: `Bạn có chắc muốn xóa "${name}" khỏi giỏ hàng hiện tại không?`,
      confirmText: 'Xóa khỏi giỏ',
      danger: true,
      onConfirm: () => {
        const session = DB.getSession();
        session.cart = session.cart.filter(i => i.medicineId !== Number(medId));
        DB.saveSession();
        C.showToast(`Đã xóa ${name} khỏi giỏ hàng.`);
        window.PharmacyApp.refreshCurrentRoute();
      }
    });
  }

  function handleApplyVoucherForm(e) {
    e.preventDefault();
    const inp = document.getElementById('cartVoucherInput');
    if (!inp) return;
    applyVoucherQuick(inp.value);
  }

  function applyVoucherQuick(code) {
    const session = DB.getSession();
    session.appliedVoucherCode = (code || '').trim().toUpperCase();
    DB.saveSession();
    const db = DB.getDb();
    const subtotal = (session.cart || []).reduce((sum, i) => {
      const m = db.medicines.find(x => x.id === i.medicineId);
      return sum + (m ? m.price * i.quantity : 0);
    }, 0);
    const res = API.evaluateVoucher(session.appliedVoucherCode, subtotal);
    if (res.valid) {
      C.showToast(res.message, 'success');
    } else {
      C.showToast(res.message, 'warning');
    }
    window.PharmacyApp.refreshCurrentRoute();
  }

  function removeVoucher() {
    const session = DB.getSession();
    session.appliedVoucherCode = null;
    DB.saveSession();
    C.showToast('Đã gỡ mã giảm giá khỏi giỏ hàng.');
    window.PharmacyApp.refreshCurrentRoute();
  }

  function setCartSimulation(mode) {
    const session = DB.getSession();
    session.cartSimulationMode = mode;
    DB.saveSession();
    C.showToast(`Đã chuyển mô phỏng giỏ hàng sang: ${mode}`);
    window.PharmacyApp.refreshCurrentRoute();
  }

  function populateSampleCart() {
    const session = DB.getSession();
    session.cart = [
      { medicineId: 1, quantity: 2, addedPrice: 45000 },
      { medicineId: 4, quantity: 1, addedPrice: 35000 }
    ];
    session.appliedVoucherCode = 'GIAM10K';
    session.cartSimulationMode = 'NORMAL';
    DB.saveSession();
    C.showToast('Đã nạp lại 2 sản phẩm mẫu vào giỏ hàng.');
    window.PharmacyApp.refreshCurrentRoute();
  }

  // ==================== 5. CHECKOUT (#/checkout) ====================
  function renderCheckout(container) {
    const session = DB.getSession();
    const db = DB.getDb();

    // Yêu cầu đăng nhập trước khi đặt hàng, giữ nguyên giỏ
    if (session.accessRole === 'GUEST') {
      C.showToast('Vui lòng đăng nhập để tiến hành đặt hàng. Giỏ hàng của bạn được giữ nguyên.', 'warning');
      window.location.hash = '#/login?returnUrl=/checkout';
      return;
    }

    if (!session.cart || session.cart.length === 0) {
      window.location.hash = '#/cart';
      return;
    }

    const customer = db.customers.find(c => c.id === session.customerId) || db.customers[0];
    const defaultAddr = (customer.addresses && customer.addresses.find(a => a.isDefault)) || (customer.addresses && customer.addresses[0]) || {
      recipientName: customer.fullName,
      phone: customer.phone,
      address: 'Số 42 Đường 3/2, P. Xuân Khánh, Q. Ninh Kiều, Cần Thơ'
    };

    let subtotal = 0;
    const rows = session.cart.map(item => {
      const med = DB.enrichMedicine(db.medicines.find(m => m.id === item.medicineId));
      const lineTotal = med ? med.price * item.quantity : 0;
      subtotal += lineTotal;
      return { item, med, lineTotal };
    }).filter(r => r.med);

    const vEval = session.appliedVoucherCode ? API.evaluateVoucher(session.appliedVoucherCode, subtotal) : { valid: false, discountAmount: 0 };
    const discountAmount = vEval.valid ? vEval.discountAmount : 0;
    const shippingFee = subtotal - discountAmount >= 150000 ? 0 : 15000;
    const totalAmount = Math.max(0, subtotal - discountAmount + shippingFee);

    container.innerHTML = `
      <div class="container-xxl px-3 py-4">
        <nav aria-label="breadcrumb" class="mb-3">
          <ol class="breadcrumb small mb-0">
            <li class="breadcrumb-item"><a href="#/cart" class="text-decoration-none">Giỏ hàng</a></li>
            <li class="breadcrumb-item active" aria-current="page">Thanh toán &amp; Đặt hàng</li>
          </ol>
        </nav>

        <h1 class="h4 fw-bold mb-3">${I18n.t('checkout_title')}</h1>

        <div id="checkoutConflictAlertBox"></div>

        <form id="checkoutForm" novalidate onsubmit="PharmacyStorefront.handleCheckoutSubmit(event)">
          <div class="row g-4">
            <div class="col-12 col-lg-7">
              <div class="bg-white rounded-3 border p-4 mb-4" style="border-color:var(--color-border-subtle);">
                <div class="d-flex flex-wrap align-items-center justify-content-between gap-2 mb-3">
                  <h2 class="h6 fw-bold mb-0">1. Thông tin người nhận hàng</h2>
                  ${customer.addresses && customer.addresses.length > 1 ? `
                    <select class="form-select form-select-sm w-auto" aria-label="Chọn từ sổ địa chỉ" onchange="PharmacyStorefront.fillCheckoutAddress(this.value)">
                      ${customer.addresses.map(a => `<option value="${a.id}" ${a.isDefault ? 'selected' : ''}>Sổ địa chỉ: ${esc(a.label)}</option>`).join('')}
                    </select>
                  ` : ''}
                </div>

                <div class="row g-3">
                  <div class="col-12 col-md-6">
                    <label for="chkRecipientName" class="form-label-ph">Họ và tên người nhận <span class="text-danger">*</span></label>
                    <input id="chkRecipientName" name="recipientName" type="text" class="form-control-ph" required minlength="2" maxlength="100" value="${esc(defaultAddr.recipientName)}" />
                    <div class="field-error-msg d-none" id="err_chkRecipientName"></div>
                  </div>
                  <div class="col-12 col-md-6">
                    <label for="chkRecipientPhone" class="form-label-ph">Số điện thoại liên hệ <span class="text-danger">*</span></label>
                    <input id="chkRecipientPhone" name="recipientPhone" type="tel" class="form-control-ph tabular-nums" required placeholder="0912345678" value="${esc(defaultAddr.phone)}" />
                    <div class="field-error-msg d-none" id="err_chkRecipientPhone"></div>
                  </div>
                  <div class="col-12">
                    <label for="chkShippingAddress" class="form-label-ph">Địa chỉ nhận hàng chi tiết <span class="text-danger">*</span></label>
                    <input id="chkShippingAddress" name="shippingAddress" type="text" class="form-control-ph" required minlength="10" maxlength="250" value="${esc(defaultAddr.address)}" />
                    <div class="field-error-msg d-none" id="err_chkShippingAddress"></div>
                  </div>
                  <div class="col-12">
                    <label for="chkNote" class="form-label-ph">Ghi chú giao hàng (Tùy chọn)</label>
                    <textarea id="chkNote" name="note" rows="2" class="form-control-ph" maxlength="300" placeholder="Ví dụ: Giao trong giờ hành chính, gọi trước khi giao..."></textarea>
                  </div>
                </div>
              </div>

              <div class="bg-white rounded-3 border p-4" style="border-color:var(--color-border-subtle);">
                <h2 class="h6 fw-bold mb-3">2. Hình thức giao nhận &amp; Phương thức thanh toán</h2>
                <div class="row g-3 mb-3">
                  <div class="col-12 col-md-6">
                    <label class="d-flex align-items-start gap-2 p-3 rounded-3 border h-100" style="cursor:pointer;">
                      <input type="radio" name="deliveryMethod" value="STANDARD" checked class="mt-1" />
                      <div>
                        <div class="fw-semibold small">Giao hàng tận nơi tiêu chuẩn</div>
                        <div class="text-muted" style="font-size:12px;">Miễn phí cho đơn từ 150.000 ₫ (dưới 150k phí 15.000 ₫)</div>
                      </div>
                    </label>
                  </div>
                  <div class="col-12 col-md-6">
                    <label class="d-flex align-items-start gap-2 p-3 rounded-3 border h-100" style="cursor:pointer;">
                      <input type="radio" name="deliveryMethod" value="PICKUP" class="mt-1" />
                      <div>
                        <div class="fw-semibold small">Nhận tại quầy Nhà thuốc Pharmacy</div>
                        <div class="text-muted" style="font-size:12px;">Giữ đơn chờ tại quầy • Phí vận chuyển 0 ₫</div>
                      </div>
                    </label>
                  </div>
                </div>

                <div class="row g-3">
                  <div class="col-12 col-md-6">
                    <label class="d-flex align-items-start gap-2 p-3 rounded-3 border h-100" style="cursor:pointer;">
                      <input type="radio" name="paymentMethod" value="COD" checked class="mt-1" />
                      <div>
                        <div class="fw-semibold small">Thanh toán khi nhận hàng (COD)</div>
                        <div class="text-muted" style="font-size:12px;">Kiểm tra hàng và thanh toán tiền mặt cho nhân viên giao hàng</div>
                      </div>
                    </label>
                  </div>
                  <div class="col-12 col-md-6">
                    <label class="d-flex align-items-start gap-2 p-3 rounded-3 border h-100" style="cursor:pointer;">
                      <input type="radio" name="paymentMethod" value="BANK_TRANSFER" class="mt-1" />
                      <div>
                        <div class="fw-semibold small">Chuyển khoản ngân hàng</div>
                        <div class="text-muted" style="font-size:12px;">Chuyển khoản nhanh qua mã VietQR hoặc số tài khoản nhà thuốc</div>
                      </div>
                    </label>
                  </div>
                </div>
              </div>
            </div>

            <!-- Bảng Tổng tiền & Nút Đặt hàng -->
            <div class="col-12 col-lg-5">
              <div class="bg-white rounded-3 border p-4" style="border-color:var(--color-border-subtle);">
                <h2 class="h6 fw-bold mb-3">3. Kiểm tra sản phẩm &amp; Tổng tiền</h2>
                <div class="mb-3">
                  ${rows.map(r => `
                    <div class="d-flex justify-content-between align-items-center py-2 border-bottom small">
                      <div>
                        <div class="fw-semibold">${esc(r.med.name)}</div>
                        <div class="text-muted">${r.item.quantity} ${esc(r.med.unit)} × ${I18n.formatCurrency(r.med.price)}</div>
                      </div>
                      <div class="fw-semibold tabular-nums">${I18n.formatCurrency(r.lineTotal)}</div>
                    </div>
                  `).join('')}
                </div>

                <div class="d-flex justify-content-between small mb-2">
                  <span class="text-secondary">Tạm tính:</span>
                  <span class="fw-semibold tabular-nums">${I18n.formatCurrency(subtotal)}</span>
                </div>
                <div class="d-flex justify-content-between small mb-2">
                  <span class="text-secondary">Voucher (${esc(session.appliedVoucherCode || 'Không dùng')}):</span>
                  <span class="fw-semibold text-success tabular-nums">−${I18n.formatCurrency(discountAmount)}</span>
                </div>
                <div class="d-flex justify-content-between small mb-3">
                  <span class="text-secondary">Phí giao hàng:</span>
                  <span class="fw-semibold tabular-nums">${I18n.formatCurrency(shippingFee)}</span>
                </div>
                <div class="d-flex justify-content-between align-items-baseline pt-3 border-top mb-3">
                  <span class="fw-bold">Tổng cộng cần thanh toán:</span>
                  <span class="h4 fw-bold mb-0 tabular-nums" style="color:var(--color-primary-active);">${I18n.formatCurrency(totalAmount)}</span>
                </div>

                <button type="submit" id="btnSubmitCheckout" class="btn-ph btn-ph-primary w-100 mb-2">
                  ${C.icon('check', 16)} <span>${I18n.t('btn_place_order')}</span>
                </button>
                <a href="#/cart" class="btn-ph btn-ph-secondary w-100">Quay lại chỉnh sửa giỏ hàng</a>
              </div>
            </div>
          </div>
        </form>
      </div>
    `;
  }

  function fillCheckoutAddress(addrId) {
    const session = DB.getSession();
    const customer = DB.getDb().customers.find(c => c.id === session.customerId);
    if (!customer || !customer.addresses) return;
    const addr = customer.addresses.find(a => String(a.id) === String(addrId));
    if (!addr) return;
    document.getElementById('chkRecipientName').value = addr.recipientName;
    document.getElementById('chkRecipientPhone').value = addr.phone;
    document.getElementById('chkShippingAddress').value = addr.address;
  }

  function setFieldError(inputId, msg) {
    const inp = document.getElementById(inputId);
    const err = document.getElementById('err_' + inputId);
    if (!inp || !err) return;
    if (msg) {
      inp.classList.add('is-invalid');
      inp.setAttribute('aria-invalid', 'true');
      err.textContent = msg;
      err.classList.remove('d-none');
    } else {
      inp.classList.remove('is-invalid');
      inp.removeAttribute('aria-invalid');
      err.textContent = '';
      err.classList.add('d-none');
    }
  }

  function handleCheckoutSubmit(e) {
    e.preventDefault();
    const form = e.target;
    const name = form.recipientName.value.trim();
    const phone = form.recipientPhone.value.trim();
    const address = form.shippingAddress.value.trim();
    const deliveryMethod = form.deliveryMethod.value;
    const paymentMethod = form.paymentMethod.value;
    const note = form.note.value.trim();

    let firstInvalid = null;
    setFieldError('chkRecipientName', null);
    setFieldError('chkRecipientPhone', null);
    setFieldError('chkShippingAddress', null);

    if (name.length < 2) {
      setFieldError('chkRecipientName', 'Vui lòng nhập họ tên người nhận (tối thiểu 2 ký tự).');
      firstInvalid = firstInvalid || document.getElementById('chkRecipientName');
    }
    if (!/^0[35789]\d{8}$/.test(phone)) {
      setFieldError('chkRecipientPhone', 'Vui lòng nhập số điện thoại hợp lệ (10 chữ số, bắt đầu bằng 03, 05, 07, 08, 09).');
      firstInvalid = firstInvalid || document.getElementById('chkRecipientPhone');
    }
    if (deliveryMethod !== 'PICKUP' && address.length < 10) {
      setFieldError('chkShippingAddress', 'Vui lòng nhập địa chỉ nhận hàng cụ thể (tối thiểu 10 ký tự).');
      firstInvalid = firstInvalid || document.getElementById('chkShippingAddress');
    }

    if (firstInvalid) {
      firstInvalid.focus();
      return;
    }

    const btn = document.getElementById('btnSubmitCheckout');
    if (btn) {
      btn.disabled = true;
      btn.innerHTML = `<span class="spinner-border spinner-border-sm"></span> <span>Đang kiểm tra lại tồn kho &amp; tạo đơn...</span>`;
    }

    API.createOrder({ recipientName: name, recipientPhone: phone, shippingAddress: address, deliveryMethod, paymentMethod, note })
      .then(order => {
        C.showToast(`Đã tiếp nhận đơn hàng ${order.code}!`, 'success');
        window.location.hash = `#/order-result/${order.id}`;
      })
      .catch(err => {
        if (btn) {
          btn.disabled = false;
          btn.innerHTML = `${C.icon('check', 16)} <span>${I18n.t('btn_place_order')}</span>`;
        }
        const alertBox = document.getElementById('checkoutConflictAlertBox');
        if (alertBox) {
          alertBox.innerHTML = `
            <div class="p-3 rounded-3 mb-3" role="alert" style="background:var(--color-danger-bg);color:var(--color-danger);border:1px solid var(--color-danger-border);">
              <div class="fw-bold mb-1 d-flex align-items-center gap-2">
                ${C.icon('alert', 18)} ${esc(err.message)}
              </div>
              ${err.warnings ? `<ul class="small mb-2 ps-3">${err.warnings.map(w => `<li>${esc(w)}</li>`).join('')}</ul>` : ''}
              <a href="#/cart" class="btn-ph btn-ph-danger btn-ph-sm">Quay lại Giỏ hàng để điều chỉnh</a>
            </div>
          `;
        }
      });
  }

  // ==================== 6. KẾT QUẢ ĐẶT HÀNG (#/order-result/:id) ====================
  function renderOrderResult(container, orderId) {
    const db = DB.getDb();
    const order = db.orders.find(o => String(o.id) === String(orderId) || o.code === String(orderId)) || db.orders[0];

    container.innerHTML = `
      <div class="container-xxl px-3 py-4">
        <div class="bg-white rounded-3 border p-4 mx-auto" style="max-width:740px;border-color:var(--color-border-subtle);">
          <div class="d-flex flex-wrap align-items-center justify-content-between gap-2 pb-3 mb-3 border-bottom">
            <div class="d-flex align-items-center gap-2">
              <span class="brand-icon-box">${C.icon('check', 22)}</span>
              <div>
                <h1 class="h5 fw-bold mb-0">Đã tiếp nhận yêu cầu đặt hàng</h1>
                <div class="small text-secondary">Mã đơn hàng: <strong class="font-mono-code">${esc(order.code)}</strong> • Ngày đặt: ${I18n.formatDate(order.orderDate, true)}</div>
              </div>
            </div>
            ${C.renderStatusBadge('order', order.orderStatus)}
          </div>

          <div class="p-3 rounded-3 mb-3" style="background:var(--color-primary-soft-bg);color:var(--color-primary-soft-fg);border:1px solid #99F6E4;">
            <div class="small">
              Cảm ơn bạn đã đặt hàng tại <strong>Nhà thuốc Pharmacy</strong>. Dược sĩ đang kiểm tra đơn hàng và sẽ sớm liên hệ xác nhận giao hàng cho bạn.
            </div>
          </div>

          <div class="row g-3 mb-3 small">
            <div class="col-12 col-sm-4">
              <div class="p-3 rounded-2 border bg-light h-100">
                <div class="text-muted mb-1">Trạng thái đơn hàng</div>
                ${C.renderStatusBadge('order', order.orderStatus)}
              </div>
            </div>
            <div class="col-12 col-sm-4">
              <div class="p-3 rounded-2 border bg-light h-100">
                <div class="text-muted mb-1">Trạng thái thanh toán</div>
                ${C.renderStatusBadge('payment', order.paymentStatus)}
              </div>
            </div>
            <div class="col-12 col-sm-4">
              <div class="p-3 rounded-2 border bg-light h-100">
                <div class="text-muted mb-1">Trạng thái giao hàng</div>
                ${C.renderStatusBadge('shipping', order.shippingStatus)}
              </div>
            </div>
          </div>

          <div class="mb-3 small">
            <div><strong>Người nhận:</strong> ${esc(order.customerName)} (${esc(order.customerPhone)})</div>
            <div><strong>Địa chỉ nhận:</strong> ${esc(order.shippingAddress)}</div>
          </div>

          <div class="table-responsive mb-3">
            <table class="table-ph">
              <thead>
                <tr>
                  <th>Sản phẩm</th>
                  <th class="text-center">Đơn vị</th>
                  <th class="text-end">Số lượng</th>
                  <th class="text-end">Đơn giá</th>
                  <th class="text-end">Thành tiền</th>
                </tr>
              </thead>
              <tbody>
                ${order.items.map(it => `
                  <tr>
                    <td class="fw-medium">${esc(it.medicineName)}</td>
                    <td class="text-center">${esc(it.unit)}</td>
                    <td class="text-end tabular-nums">${it.quantity}</td>
                    <td class="text-end tabular-nums">${I18n.formatCurrency(it.unitPrice)}</td>
                    <td class="text-end tabular-nums fw-semibold">${I18n.formatCurrency(it.unitPrice * it.quantity)}</td>
                  </tr>
                `).join('')}
              </tbody>
            </table>
          </div>

          <div class="d-flex justify-content-between align-items-center pt-2 border-top mb-4">
            <span class="fw-bold">Tổng số tiền đơn hàng:</span>
            <span class="h4 fw-bold mb-0 tabular-nums" style="color:var(--color-primary-active);">${I18n.formatCurrency(order.totalAmount)}</span>
          </div>

          <div class="d-flex flex-wrap gap-2 justify-content-end">
            <a href="#/my-orders" class="btn-ph btn-ph-primary">${C.icon('box', 16)} Xem trong Đơn của tôi</a>
            <a href="#/products" class="btn-ph btn-ph-secondary">Tiếp tục mua sắm</a>
          </div>
        </div>
      </div>
    `;
  }

  // ==================== 7. ĐĂNG NHẬP / ĐĂNG KÝ / QUÊN MẬT KHẨU ====================
  function renderAuthView(container, mode, queryParams = {}) {
    const returnUrl = queryParams.returnUrl || '/home';

    if (mode === 'login') {
      container.innerHTML = `
        <div class="container-xxl px-3 py-5">
          <div class="bg-white rounded-3 border p-4 mx-auto" style="max-width:460px;border-color:var(--color-border-subtle);">
            <h1 class="h5 fw-bold mb-1">Đăng nhập tài khoản</h1>
            <p class="small text-secondary mb-3">Đăng nhập để đặt hàng, quản lý sổ địa chỉ và theo dõi đơn thuốc.</p>

            <div id="authFormAlert"></div>

            <form onsubmit="PharmacyStorefront.handleLoginSubmit(event, '${esc(returnUrl)}')" novalidate>
              <div class="mb-3">
                <label for="loginIdentifier" class="form-label-ph">Số điện thoại hoặc Tên đăng nhập <span class="text-danger">*</span></label>
                <input id="loginIdentifier" type="text" class="form-control-ph" required value="0912345678" placeholder="0912345678 hoặc admin" />
                <div class="field-error-msg d-none" id="err_loginIdentifier"></div>
              </div>
              <div class="mb-3">
                <div class="d-flex justify-content-between">
                  <label for="loginPassword" class="form-label-ph">Mật khẩu <span class="text-danger">*</span></label>
                  <a href="#/forgot-password" class="small text-decoration-none" style="color:var(--color-primary);">Quên mật khẩu?</a>
                </div>
                <div class="input-group">
                  <input id="loginPassword" type="password" class="form-control-ph" style="border-top-right-radius:0;border-bottom-right-radius:0;width:calc(100% - 72px);" required value="Demo@123" />
                  <button type="button" class="btn btn-outline-secondary" style="width:72px;" onclick="PharmacyStorefront.togglePasswordVisibility('loginPassword', this)">Hiện</button>
                </div>
                <div class="field-error-msg d-none" id="err_loginPassword"></div>
              </div>
              <button type="submit" class="btn-ph btn-ph-primary w-100 mb-3">Đăng nhập</button>
            </form>

            <div class="p-3 rounded-2 small mb-3" style="background:var(--color-bg-subtle);">
              <div class="fw-semibold mb-1">Tài khoản mẫu để bấm thử nhanh:</div>
              <div class="d-flex flex-wrap gap-1">
                <button type="button" class="btn-ph btn-ph-secondary btn-ph-sm" onclick="PharmacyStorefront.quickFillLogin('0912345678')">Khách: Trần Văn Tuấn</button>
                <button type="button" class="btn-ph btn-ph-secondary btn-ph-sm" onclick="PharmacyStorefront.quickFillLogin('0907010005')">Khách bị khóa: Bùi Quốc Bảo</button>
                <button type="button" class="btn-ph btn-ph-mint btn-ph-sm" onclick="PharmacyStorefront.quickFillLogin('admin')">Quản trị: admin</button>
              </div>
            </div>

            <div class="text-center small text-secondary">
              Chưa có tài khoản khách hàng? <a href="#/register" class="fw-semibold text-decoration-none" style="color:var(--color-primary);">Đăng ký ngay</a>
            </div>
          </div>
        </div>
      `;
    } else if (mode === 'register') {
      // Tuyệt đối không đưa hộp chọn vai trò vào form đăng ký khách hàng
      container.innerHTML = `
        <div class="container-xxl px-3 py-5">
          <div class="bg-white rounded-3 border p-4 mx-auto" style="max-width:480px;border-color:var(--color-border-subtle);">
            <h1 class="h5 fw-bold mb-1">Đăng ký tài khoản Khách hàng</h1>
            <p class="small text-secondary mb-3">Tạo tài khoản mua hàng trực tuyến (Vai trò mặc định: <code class="font-mono-code">CUSTOMER</code>, liên kết hồ sơ khách hàng theo SĐT).</p>

            <form onsubmit="PharmacyStorefront.handleRegisterSubmit(event)" novalidate>
              <div class="mb-3">
                <label for="regFullName" class="form-label-ph">Họ và tên <span class="text-danger">*</span></label>
                <input id="regFullName" type="text" class="form-control-ph" required placeholder="Nguyễn Văn A" />
                <div class="field-error-msg d-none" id="err_regFullName"></div>
              </div>
              <div class="mb-3">
                <label for="regPhone" class="form-label-ph">Số điện thoại <span class="text-danger">*</span></label>
                <input id="regPhone" type="tel" class="form-control-ph tabular-nums" required placeholder="0912345678" />
                <div class="field-error-msg d-none" id="err_regPhone"></div>
              </div>
              <div class="mb-3">
                <label for="regEmail" class="form-label-ph">Địa chỉ Email <span class="text-danger">*</span></label>
                <input id="regEmail" type="email" class="form-control-ph" required placeholder="khachhang@example.com" />
                <div class="field-error-msg d-none" id="err_regEmail"></div>
              </div>
              <div class="mb-3">
                <label for="regPassword" class="form-label-ph">Mật khẩu (tối thiểu 6 ký tự) <span class="text-danger">*</span></label>
                <div class="input-group">
                  <input id="regPassword" type="password" class="form-control-ph" style="width:calc(100% - 72px);" required />
                  <button type="button" class="btn btn-outline-secondary" style="width:72px;" onclick="PharmacyStorefront.togglePasswordVisibility('regPassword', this)">Hiện</button>
                </div>
                <div class="field-error-msg d-none" id="err_regPassword"></div>
              </div>
              <button type="submit" class="btn-ph btn-ph-primary w-100 mb-3">Tạo tài khoản khách hàng</button>
            </form>

            <div class="text-center small text-secondary">
              Đã có tài khoản? <a href="#/login" class="fw-semibold text-decoration-none" style="color:var(--color-primary);">Đăng nhập</a>
            </div>
          </div>
        </div>
      `;
    } else {
      container.innerHTML = `
        <div class="container-xxl px-3 py-5">
          <div class="bg-white rounded-3 border p-4 mx-auto" style="max-width:460px;border-color:var(--color-border-subtle);">
            <h1 class="h5 fw-bold mb-1">Khôi phục mật khẩu (Mô phỏng)</h1>
            <p class="small text-secondary mb-3">Nhập email hoặc số điện thoại đã đăng ký. Lưu ý: Prototype chỉ mô phỏng giao diện yêu cầu, chưa gửi email/SMS thật.</p>
            <div id="forgotResultBox"></div>
            <form onsubmit="PharmacyStorefront.handleForgotSubmit(event)" novalidate>
              <div class="mb-3">
                <label for="forgotContact" class="form-label-ph">Số điện thoại hoặc Email đã đăng ký <span class="text-danger">*</span></label>
                <input id="forgotContact" type="text" class="form-control-ph" required placeholder="0912345678 hoặc email@example.com" />
                <div class="field-error-msg d-none" id="err_forgotContact"></div>
              </div>
              <button type="submit" class="btn-ph btn-ph-primary w-100 mb-3">Gửi yêu cầu đặt lại mật khẩu</button>
            </form>
            <div class="text-center small">
              <a href="#/login" class="text-decoration-none" style="color:var(--color-primary);">← Quay lại màn hình đăng nhập</a>
            </div>
          </div>
        </div>
      `;
    }
  }

  function togglePasswordVisibility(inputId, btn) {
    const inp = document.getElementById(inputId);
    if (!inp) return;
    if (inp.type === 'password') {
      inp.type = 'text';
      btn.textContent = 'Ẩn';
    } else {
      inp.type = 'password';
      btn.textContent = 'Hiện';
    }
  }

  function quickFillLogin(val) {
    const inp = document.getElementById('loginIdentifier');
    if (inp) inp.value = val;
  }

  function handleLoginSubmit(e, returnUrl) {
    e.preventDefault();
    const idVal = document.getElementById('loginIdentifier').value.trim();
    const pwVal = document.getElementById('loginPassword').value;
    setFieldError('loginIdentifier', null);
    setFieldError('loginPassword', null);

    if (!idVal) {
      setFieldError('loginIdentifier', 'Vui lòng nhập số điện thoại hoặc tên đăng nhập.');
      return;
    }
    if (!pwVal || pwVal.length < 4) {
      setFieldError('loginPassword', 'Vui lòng nhập mật khẩu.');
      return;
    }

    const db = DB.getDb();
    const session = DB.getSession();

    if (idVal.toLowerCase() === 'admin') {
      session.accessRole = 'ADMIN';
      DB.saveSession();
      C.showToast('Đã đăng nhập với vai trò Quản trị viên (ADMIN).', 'success');
      window.location.hash = '#/admin/dashboard';
      return;
    }

    const cust = db.customers.find(c => c.phone === idVal || (c.email && c.email.toLowerCase() === idVal.toLowerCase()));
    if (!cust) {
      // Lỗi đăng nhập chung, không tiết lộ tài khoản nào tồn tại
      const alertBox = document.getElementById('authFormAlert');
      if (alertBox) {
        alertBox.innerHTML = `<div class="p-2 rounded small mb-3" style="background:var(--color-danger-bg);color:var(--color-danger);border:1px solid var(--color-danger-border);">Thông tin đăng nhập hoặc mật khẩu không chính xác.</div>`;
      }
      return;
    }

    if (cust.isLocked) {
      const alertBox = document.getElementById('authFormAlert');
      if (alertBox) {
        alertBox.innerHTML = `<div class="p-2 rounded small mb-3" style="background:var(--color-danger-bg);color:var(--color-danger);border:1px solid var(--color-danger-border);">Tài khoản khách hàng (${esc(cust.fullName)}) hiện đang tạm khóa. Vui lòng liên hệ nhà thuốc.</div>`;
      }
      return;
    }

    session.accessRole = 'CUSTOMER';
    session.customerId = cust.id;
    DB.saveSession();
    C.showToast(`Xin chào ${cust.fullName}! Giỏ hàng của bạn đã được đồng bộ.`, 'success');
    window.location.hash = '#' + (returnUrl || '/home');
  }

  function handleRegisterSubmit(e) {
    e.preventDefault();
    const fullName = document.getElementById('regFullName').value.trim();
    const phone = document.getElementById('regPhone').value.trim();
    const email = document.getElementById('regEmail').value.trim();
    const pw = document.getElementById('regPassword').value;

    setFieldError('regFullName', null);
    setFieldError('regPhone', null);
    setFieldError('regEmail', null);
    setFieldError('regPassword', null);

    if (fullName.length < 2) {
      setFieldError('regFullName', 'Vui lòng nhập họ và tên hợp lệ.');
      return;
    }
    if (!/^0[35789]\d{8}$/.test(phone)) {
      setFieldError('regPhone', 'Vui lòng nhập số điện thoại 10 số hợp lệ.');
      return;
    }
    if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email)) {
      setFieldError('regEmail', 'Vui lòng nhập địa chỉ email hợp lệ.');
      return;
    }
    if (!pw || pw.length < 6) {
      setFieldError('regPassword', 'Mật khẩu cần tối thiểu 6 ký tự.');
      return;
    }

    const db = DB.getDb();
    let cust = db.customers.find(c => c.phone === phone);
    if (!cust) {
      const nextId = db.customers.reduce((m, c) => Math.max(m, c.id), 8) + 1;
      cust = {
        id: nextId,
        fullName,
        phone,
        email,
        points: 0,
        webAccountActive: true,
        isLocked: false,
        createdAt: DB.TODAY_STR,
        addresses: []
      };
      db.customers.push(cust);
    } else {
      cust.fullName = fullName;
      cust.email = email;
      cust.webAccountActive = true;
    }
    const session = DB.getSession();
    session.accessRole = 'CUSTOMER';
    session.customerId = cust.id;
    DB.saveDb();
    DB.saveSession();
    C.showToast(`Đăng ký thành công tài khoản khách hàng cho ${fullName}.`, 'success');
    window.location.hash = '#/account';
  }

  function handleForgotSubmit(e) {
    e.preventDefault();
    const val = document.getElementById('forgotContact').value.trim();
    setFieldError('forgotContact', null);
    if (val.length < 5) {
      setFieldError('forgotContact', 'Vui lòng nhập số điện thoại hoặc địa chỉ email hợp lệ.');
      return;
    }
    const box = document.getElementById('forgotResultBox');
    if (box) {
      box.innerHTML = `
        <div class="p-3 rounded-2 small mb-3" style="background:var(--color-success-bg);color:var(--color-success);border:1px solid var(--color-success-border);">
          <strong>Đã ghi nhận yêu cầu mô phỏng:</strong> Nếu thông tin <code>${esc(val)}</code> khớp với hồ sơ đăng ký, hướng dẫn đặt lại mật khẩu sẽ được gửi khi triển khai dịch vụ Email/SMS ở giai đoạn Backend.
        </div>
      `;
    }
  }

  // ==================== 8. TÀI KHOẢN & SỔ ĐỊA CHỈ (#/account) ====================
  function renderAccount(container) {
    const session = DB.getSession();
    if (session.accessRole === 'GUEST') {
      window.location.hash = '#/login?returnUrl=/account';
      return;
    }
    const db = DB.getDb();
    const customer = db.customers.find(c => c.id === session.customerId) || db.customers[0];

    container.innerHTML = `
      <div class="container-xxl px-3 py-4">
        <div class="d-flex flex-wrap align-items-center justify-content-between gap-2 mb-3">
          <div>
            <h1 class="h4 fw-bold mb-1">Hồ sơ tài khoản &amp; Sổ địa chỉ</h1>
            <p class="small text-secondary mb-0">Quản lý thông tin liên hệ và địa chỉ giao hàng của bạn</p>
          </div>
          <div class="d-flex flex-wrap gap-2">
            <a href="#/my-orders" class="btn-ph btn-ph-mint btn-ph-sm">${C.icon('box', 15)} Đơn hàng của tôi</a>
            <button type="button" class="btn-ph btn-ph-secondary btn-ph-sm" onclick="PharmacyApp.switchRole('GUEST')">Đăng xuất</button>
          </div>
        </div>

        <div class="row g-4">
          <!-- Thông tin cá nhân -->
          <div class="col-12 col-lg-6">
            <div class="bg-white rounded-3 border p-4 mb-4" style="border-color:var(--color-border-subtle);">
              <h2 class="h6 fw-bold mb-3">Thông tin cá nhân</h2>
              <form onsubmit="PharmacyStorefront.handleUpdateProfile(event)">
                <div class="mb-3">
                  <label for="profName" class="form-label-ph">Họ và tên <span class="text-danger">*</span></label>
                  <input id="profName" type="text" class="form-control-ph" required value="${esc(customer.fullName)}" />
                </div>
                <div class="row g-3 mb-3">
                  <div class="col-12 col-sm-6">
                    <label for="profPhone" class="form-label-ph">Số điện thoại</label>
                    <input id="profPhone" type="text" class="form-control-ph tabular-nums" readonly value="${esc(customer.phone)}" />
                  </div>
                  <div class="col-12 col-sm-6">
                    <label for="profPoints" class="form-label-ph">Điểm tích lũy thành viên</label>
                    <input id="profPoints" type="text" class="form-control-ph tabular-nums" readonly value="${customer.points} điểm" />
                  </div>
                </div>
                <div class="mb-3">
                  <label for="profEmail" class="form-label-ph">Email nhận thông báo</label>
                  <input id="profEmail" type="email" class="form-control-ph" value="${esc(customer.email || '')}" />
                </div>
                <button type="submit" class="btn-ph btn-ph-primary">Lưu thay đổi</button>
              </form>
            </div>

            <!-- Đổi mật khẩu -->
            <div class="bg-white rounded-3 border p-4" style="border-color:var(--color-border-subtle);">
              <h2 class="h6 fw-bold mb-3">Đổi mật khẩu đăng nhập</h2>
              <form onsubmit="PharmacyStorefront.handleChangePassword(event)">
                <div class="row g-3 mb-3">
                  <div class="col-12 col-sm-6">
                    <label for="pwCurrent" class="form-label-ph">Mật khẩu hiện tại <span class="text-danger">*</span></label>
                    <input id="pwCurrent" type="password" class="form-control-ph" required />
                  </div>
                  <div class="col-12 col-sm-6">
                    <label for="pwNew" class="form-label-ph">Mật khẩu mới (≥ 6 ký tự) <span class="text-danger">*</span></label>
                    <input id="pwNew" type="password" class="form-control-ph" required minlength="6" />
                  </div>
                </div>
                <button type="submit" class="btn-ph btn-ph-secondary">Cập nhật mật khẩu</button>
              </form>
            </div>
          </div>

          <!-- Sổ địa chỉ nhận hàng -->
          <div class="col-12 col-lg-6">
            <div class="bg-white rounded-3 border p-4 h-100" style="border-color:var(--color-border-subtle);">
              <div class="d-flex align-items-center justify-content-between mb-3">
                <h2 class="h6 fw-bold mb-0">Sổ địa chỉ nhận hàng (${(customer.addresses || []).length})</h2>
              </div>

              <div class="d-flex flex-column gap-2 mb-4">
                ${(customer.addresses || []).map(addr => `
                  <div class="p-3 rounded-3 border ${addr.isDefault ? 'border-teal bg-light' : ''}">
                    <div class="d-flex justify-content-between align-items-center mb-1">
                      <strong>${esc(addr.label)} — ${esc(addr.recipientName)} (${esc(addr.phone)})</strong>
                      ${addr.isDefault
                        ? `<span class="status-badge status-badge-success">Mặc định</span>`
                        : `<button type="button" class="btn-ph btn-ph-secondary btn-ph-sm" onclick="PharmacyStorefront.setDefaultAddress(${addr.id})">Đặt mặc định</button>`}
                    </div>
                    <div class="small text-secondary">${esc(addr.address)}</div>
                  </div>
                `).join('')}
              </div>

              <hr />
              <h3 class="h6 fw-bold mb-3">Thêm địa chỉ nhận hàng mới</h3>
              <form onsubmit="PharmacyStorefront.handleAddAddress(event)">
                <div class="row g-2 mb-2">
                  <div class="col-4">
                    <label for="newAddrLabel" class="form-label-ph">Nhãn</label>
                    <input id="newAddrLabel" type="text" class="form-control-ph" placeholder="Nhà riêng" required />
                  </div>
                  <div class="col-8">
                    <label for="newAddrPhone" class="form-label-ph">SĐT nhận hàng</label>
                    <input id="newAddrPhone" type="tel" class="form-control-ph" value="${esc(customer.phone)}" required />
                  </div>
                </div>
                <div class="mb-3">
                  <label for="newAddrText" class="form-label-ph">Địa chỉ cụ thể <span class="text-danger">*</span></label>
                  <input id="newAddrText" type="text" class="form-control-ph" placeholder="Số nhà, tên đường, phường/xã, quận/huyện..." required minlength="10" />
                </div>
                <button type="submit" class="btn-ph btn-ph-mint">${C.icon('plus', 15)} Thêm vào sổ địa chỉ</button>
              </form>
            </div>
          </div>
        </div>
      </div>
    `;
  }

  function handleUpdateProfile(e) {
    e.preventDefault();
    const session = DB.getSession();
    const cust = DB.getDb().customers.find(c => c.id === session.customerId);
    if (!cust) return;
    cust.fullName = document.getElementById('profName').value.trim();
    cust.email = document.getElementById('profEmail').value.trim();
    DB.saveDb();
    C.showToast('Đã lưu thông tin hồ sơ khách hàng.');
    window.PharmacyApp.refreshCurrentRoute();
  }

  function handleChangePassword(e) {
    e.preventDefault();
    const pwNew = document.getElementById('pwNew').value;
    if (!pwNew || pwNew.length < 6) {
      C.showToast('Mật khẩu mới phải có ít nhất 6 ký tự.', 'error');
      return;
    }
    e.target.reset();
    C.showToast('Đã cập nhật mật khẩu mới thành công (Mô phỏng).');
  }

  function setDefaultAddress(addrId) {
    const session = DB.getSession();
    const cust = DB.getDb().customers.find(c => c.id === session.customerId);
    if (!cust || !cust.addresses) return;
    cust.addresses.forEach(a => { a.isDefault = (a.id === Number(addrId)); });
    DB.saveDb();
    C.showToast('Đã đổi địa chỉ nhận hàng mặc định.');
    window.PharmacyApp.refreshCurrentRoute();
  }

  function handleAddAddress(e) {
    e.preventDefault();
    const session = DB.getSession();
    const cust = DB.getDb().customers.find(c => c.id === session.customerId);
    if (!cust) return;
    cust.addresses = cust.addresses || [];
    cust.addresses.push({
      id: Date.now(),
      label: document.getElementById('newAddrLabel').value.trim(),
      recipientName: cust.fullName,
      phone: document.getElementById('newAddrPhone').value.trim(),
      address: document.getElementById('newAddrText').value.trim(),
      isDefault: cust.addresses.length === 0
    });
    DB.saveDb();
    C.showToast('Đã thêm địa chỉ nhận hàng mới.');
    window.PharmacyApp.refreshCurrentRoute();
  }

  // ==================== 9. ĐƠN CỦA TÔI (#/my-orders) ====================
  function renderMyOrders(container) {
    const session = DB.getSession();
    if (session.accessRole === 'GUEST') {
      window.location.hash = '#/login?returnUrl=/my-orders';
      return;
    }
    const db = DB.getDb();
    const customer = db.customers.find(c => c.id === session.customerId) || db.customers[0];

    // Chỉ hiển thị đơn của đúng người dùng mẫu đang đăng nhập
    let myOrders = db.orders.filter(o => o.customerId === customer.id);
    if (myOrdersFilter.status !== 'ALL') {
      myOrders = myOrders.filter(o => o.orderStatus === myOrdersFilter.status);
    }

    const totalItems = myOrders.length;
    const totalPages = Math.max(1, Math.ceil(totalItems / myOrdersFilter.pageSize));
    const safePage = Math.min(myOrdersFilter.page, totalPages);
    const pagedOrders = myOrders.slice((safePage - 1) * myOrdersFilter.pageSize, safePage * myOrdersFilter.pageSize);

    container.innerHTML = `
      <div class="container-xxl px-3 py-4">
        <div class="d-flex flex-wrap align-items-center justify-content-between gap-2 mb-3">
          <div>
            <h1 class="h4 fw-bold mb-1">Đơn hàng của tôi</h1>
            <p class="small text-secondary mb-0">Theo dõi tiến độ giao hàng và lịch sử mua sắm của bạn</p>
          </div>
          <div class="d-flex flex-wrap gap-1">
            ${['ALL', 'NEW', 'PROCESSING', 'COMPLETED', 'CANCELLED'].map(st => {
              const labels = { ALL: 'Tất cả', NEW: 'Mới tạo', PROCESSING: 'Đang giao', COMPLETED: 'Hoàn tất', CANCELLED: 'Đã hủy' };
              return `<button type="button" class="btn-ph ${myOrdersFilter.status === st ? 'btn-ph-primary' : 'btn-ph-secondary'} btn-ph-sm" onclick="PharmacyStorefront.filterMyOrders('${st}')">${labels[st]}</button>`;
            }).join('')}
          </div>
        </div>

        ${pagedOrders.length === 0 ? `
          <div class="bg-white rounded-3 border p-5 text-center">
            <p class="text-secondary mb-3">Không có đơn hàng nào ở trạng thái này.</p>
            <a href="#/products" class="btn-ph btn-ph-primary">Đặt mua sản phẩm</a>
          </div>
        ` : `
          <div class="d-flex flex-column gap-3">
            ${pagedOrders.map(ord => {
              const canCancel = ['NEW', 'CONFIRMED'].includes(ord.orderStatus) && ord.paymentStatus !== 'PAID';
              return `
                <article class="bg-white rounded-3 border p-4" style="border-color:var(--color-border-subtle);">
                  <div class="d-flex flex-wrap align-items-center justify-content-between gap-2 pb-3 mb-3 border-bottom">
                    <div>
                      <span class="font-mono-code fw-bold fs-6 me-2">${esc(ord.code)}</span>
                      <span class="small text-secondary">Ngày đặt: ${I18n.formatDate(ord.orderDate, true)} • Kênh: ${esc(ord.channel)}</span>
                    </div>
                    <div class="d-flex flex-wrap gap-2">
                      ${C.renderStatusBadge('order', ord.orderStatus)}
                      ${C.renderStatusBadge('payment', ord.paymentStatus)}
                      ${C.renderStatusBadge('shipping', ord.shippingStatus)}
                    </div>
                  </div>

                  <div class="row g-3 align-items-center">
                    <div class="col-12 col-md-8">
                      <ul class="list-unstyled small mb-2">
                        ${ord.items.map(it => `<li class="mb-1">• <strong>${esc(it.medicineName)}</strong> — ${it.quantity} ${esc(it.unit)} × ${I18n.formatCurrency(it.unitPrice)}</li>`).join('')}
                      </ul>
                      <div class="small text-muted">Địa chỉ: ${esc(ord.shippingAddress)}</div>
                      ${ord.cancelReason ? `<div class="small text-danger mt-1">Lý do hủy: ${esc(ord.cancelReason)}</div>` : ''}
                    </div>
                    <div class="col-12 col-md-4 text-md-end">
                      <div class="small text-secondary">Tổng thanh toán:</div>
                      <div class="h5 fw-bold tabular-nums mb-2" style="color:var(--color-primary-active);">${I18n.formatCurrency(ord.totalAmount)}</div>
                      <div class="d-flex flex-wrap gap-2 justify-content-md-end">
                        <button type="button" class="btn-ph btn-ph-secondary btn-ph-sm" onclick="PharmacyStorefront.openMyOrderDetailModal(${ord.id})">
                          ${C.icon('eye', 14)} Chi tiết &amp; Lịch sử
                        </button>
                        ${canCancel ? `
                          <button type="button" class="btn-ph btn-ph-danger-outline btn-ph-sm" onclick="PharmacyStorefront.promptCancelMyOrder(${ord.id}, '${esc(ord.code)}')">
                            Yêu cầu hủy đơn
                          </button>
                        ` : ''}
                      </div>
                    </div>
                  </div>
                </article>
              `;
            }).join('')}
          </div>
          ${C.renderPagination({ page: safePage, totalPages, totalItems }, 'PharmacyStorefront.changeMyOrdersPage')}
        `}
      </div>
    `;
  }

  function filterMyOrders(status) {
    myOrdersFilter.status = status;
    myOrdersFilter.page = 1;
    window.PharmacyApp.refreshCurrentRoute();
  }

  function changeMyOrdersPage(p) {
    myOrdersFilter.page = Number(p) || 1;
    window.PharmacyApp.refreshCurrentRoute();
  }

  function openMyOrderDetailModal(orderId) {
    const ord = DB.getDb().orders.find(o => o.id === Number(orderId));
    if (!ord) return;
    C.openModal({
      title: `Chi tiết đơn hàng ${ord.code}`,
      size: 'lg',
      bodyHtml: `
        <div class="d-flex flex-wrap gap-2 mb-3">
          ${C.renderStatusBadge('order', ord.orderStatus)}
          ${C.renderStatusBadge('payment', ord.paymentStatus)}
          ${C.renderStatusBadge('shipping', ord.shippingStatus)}
        </div>
        <div class="row g-3 small mb-3">
          <div class="col-md-6">
            <div><strong>Người nhận:</strong> ${esc(ord.customerName)} (${esc(ord.customerPhone)})</div>
            <div><strong>Địa chỉ:</strong> ${esc(ord.shippingAddress)}</div>
          </div>
          <div class="col-md-6">
            <div><strong>Phương thức TT:</strong> ${esc(ord.paymentMethod)}</div>
            <div><strong>Mã giảm giá:</strong> ${esc(ord.voucherCode || 'Không áp dụng')}</div>
          </div>
        </div>
        <div class="table-responsive mb-3">
          <table class="table-ph">
            <thead>
              <tr><th>Tên thuốc</th><th>Đơn vị</th><th class="text-end">SL</th><th class="text-end">Đơn giá</th><th class="text-end">Thành tiền</th></tr>
            </thead>
            <tbody>
              ${ord.items.map(i => `
                <tr>
                  <td>${esc(i.medicineName)}</td>
                  <td>${esc(i.unit)}</td>
                  <td class="text-end tabular-nums">${i.quantity}</td>
                  <td class="text-end tabular-nums">${I18n.formatCurrency(i.unitPrice)}</td>
                  <td class="text-end tabular-nums fw-bold">${I18n.formatCurrency(i.unitPrice * i.quantity)}</td>
                </tr>
              `).join('')}
            </tbody>
          </table>
        </div>
        <h3 class="h6 fw-bold mb-2">Lịch sử cập nhật trạng thái</h3>
        <ul class="list-group list-group-flush small">
          ${(ord.history || []).map(h => `
            <li class="list-group-item px-0 d-flex justify-content-between">
              <span><strong>${esc(h.actor)}:</strong> ${esc(h.text)}</span>
              <span class="text-muted tabular-nums">${I18n.formatDate(h.time, true)}</span>
            </li>
          `).join('')}
        </ul>
      `,
      footerHtml: `<button type="button" class="btn-ph btn-ph-secondary" onclick="PharmacyComponents.closeModal()">Đóng</button>`
    });
  }

  function promptCancelMyOrder(orderId, code) {
    C.openModal({
      title: `Yêu cầu hủy đơn hàng ${code}`,
      size: 'sm',
      bodyHtml: `
        <p class="small text-secondary">Đơn hàng đang ở trạng thái cho phép hủy. Vui lòng nhập lý do hủy đơn:</p>
        <label for="cancelOrderReasonInp" class="form-label-ph">Lý do hủy đơn <span class="text-danger">*</span></label>
        <input id="cancelOrderReasonInp" type="text" class="form-control-ph" value="Đặt nhầm số lượng, muốn đặt lại đơn mới" />
      `,
      footerHtml: `
        <button type="button" class="btn-ph btn-ph-secondary" onclick="PharmacyComponents.closeModal()">Không hủy</button>
        <button type="button" class="btn-ph btn-ph-danger" onclick="PharmacyStorefront.confirmCancelMyOrder(${orderId})">Xác nhận hủy đơn</button>
      `
    });
  }

  function confirmCancelMyOrder(orderId) {
    const inp = document.getElementById('cancelOrderReasonInp');
    const reason = inp ? inp.value.trim() : 'Khách yêu cầu hủy';
    C.closeModal();
    API.cancelCustomerOrder(orderId, reason)
      .then(ord => {
        C.showToast(`Đã hủy đơn hàng ${ord.code}.`, 'success');
        window.PharmacyApp.refreshCurrentRoute();
      })
      .catch(err => C.showToast(err.message, 'error'));
  }

  // ==================== 10. GỬI ĐƠN THUỐC TƯ VẤN (#/prescription-upload) ====================
  function renderPrescriptionUpload(container, queryParams = {}) {
    const db = DB.getDb();
    const session = DB.getSession();
    const customer = db.customers.find(c => c.id === session.customerId) || db.customers[0];
    const rxMedicines = db.medicines.filter(m => m.isActive && m.isPrescription);
    const preselectedMedId = queryParams.medicineId ? Number(queryParams.medicineId) : null;

    if (draftRxImages.length === 0) {
      draftRxImages = [
        {
          name: 'don-thuoc-mau-01.svg',
          sizeKb: 128,
          dataUrl: I18n.generatePrescriptionSampleSvg('RX-DRAFT-01', customer.fullName, 'BS. Nguyễn Văn Minh (Mẫu)', '03/11/2026', 'Augmentin 1g — 01 Hộp')
        }
      ];
    }

    container.innerHTML = `
      <div class="container-xxl px-3 py-4">
        <div class="mb-3">
          <h1 class="h4 fw-bold mb-1">Gửi đơn thuốc &amp; Yêu cầu dược sĩ tư vấn</h1>
          <p class="small text-secondary mb-0">
            Luồng dành riêng cho các thuốc cần đơn của bác sĩ. Không tự động thanh toán trực tiếp khi chưa có đối chiếu đơn thuốc.
          </p>
        </div>

        <div class="row g-4">
          <!-- Form Gửi đơn thuốc -->
          <div class="col-12 col-lg-6">
            <div class="bg-white rounded-3 border p-4" style="border-color:var(--color-border-subtle);">
              <h2 class="h6 fw-bold mb-3">1. Tải ảnh chụp đơn thuốc &amp; Thông tin liên hệ</h2>

              <form id="rxUploadForm" onsubmit="PharmacyStorefront.handleRxSubmit(event)" novalidate>
                <div class="mb-3">
                  <label for="rxFileInput" class="form-label-ph">Tải lên ảnh chụp đơn thuốc (JPG, PNG, WEBP ≤ 5MB) <span class="text-danger">*</span></label>
                  <input id="rxFileInput" type="file" accept="image/jpeg,image/png,image/webp,image/svg+xml" multiple class="form-control-ph" onchange="PharmacyStorefront.handleRxFilesSelected(this)" />
                  <div class="field-error-msg d-none" id="err_rxFileInput"></div>

                  <!-- Danh sách ảnh Preview -->
                  <div id="rxPreviewList" class="row g-2 mt-2"></div>
                </div>

                <div class="row g-3 mb-3">
                  <div class="col-12 col-sm-6">
                    <label for="rxPatientName" class="form-label-ph">Họ tên người bệnh / người liên hệ <span class="text-danger">*</span></label>
                    <input id="rxPatientName" type="text" class="form-control-ph" required value="${esc(customer.fullName)}" />
                    <div class="field-error-msg d-none" id="err_rxPatientName"></div>
                  </div>
                  <div class="col-12 col-sm-6">
                    <label for="rxPhone" class="form-label-ph">Số điện thoại nhận tư vấn <span class="text-danger">*</span></label>
                    <input id="rxPhone" type="tel" class="form-control-ph tabular-nums" required value="${esc(customer.phone)}" />
                    <div class="field-error-msg d-none" id="err_rxPhone"></div>
                  </div>
                </div>

                <div class="mb-3">
                  <label class="form-label-ph">Thuốc kê đơn cần hỏi (Tùy chọn đánh dấu)</label>
                  <div class="row g-2">
                    ${rxMedicines.map(m => `
                      <div class="col-12 col-sm-6">
                        <label class="d-flex align-items-center gap-2 p-2 rounded border small" style="cursor:pointer;">
                          <input type="checkbox" name="rxMedIds" value="${m.id}" ${preselectedMedId === m.id ? 'checked' : ''} />
                          <span>${esc(m.name)} (${I18n.formatCurrency(m.price)}/${esc(m.unit)})</span>
                        </label>
                      </div>
                    `).join('')}
                  </div>
                </div>

                <div class="mb-3">
                  <label for="rxNote" class="form-label-ph">Ghi chú cho dược sĩ</label>
                  <textarea id="rxNote" rows="2" class="form-control-ph" placeholder="Nhập câu hỏi về tình trạng còn hàng hoặc thời gian ghé nhà thuốc..."></textarea>
                </div>

                <button type="submit" class="btn-ph btn-ph-primary w-100">
                  ${C.icon('upload', 16)} Gửi yêu cầu tư vấn đơn thuốc
                </button>
              </form>
            </div>
          </div>

          <!-- Danh sách Yêu cầu đã gửi & Trạng thái phản hồi -->
          <div class="col-12 col-lg-6">
            <div class="bg-white rounded-3 border p-4 h-100" style="border-color:var(--color-border-subtle);">
              <div class="d-flex align-items-center justify-content-between mb-3">
                <h2 class="h6 fw-bold mb-0">2. Lịch sử đơn thuốc đã gửi</h2>
              </div>

              <div class="d-flex flex-column gap-3">
                ${db.prescriptionRequests.map(req => `
                  <article class="p-3 rounded-3 border">
                    <div class="d-flex flex-wrap justify-content-between align-items-center gap-2 mb-2">
                      <div>
                        <span class="font-mono-code fw-bold">${esc(req.code)}</span>
                        <span class="small text-muted ms-1">• ${I18n.formatDate(req.createdAt, true)}</span>
                      </div>
                      ${C.renderStatusBadge('rxRequest', req.status)}
                    </div>
                    <div class="small mb-1"><strong>Người gửi:</strong> ${esc(req.patientName)} (${esc(req.phone)})</div>
                    ${req.requestedMedicines && req.requestedMedicines.length ? `
                      <div class="small mb-1"><strong>Thuốc hỏi:</strong> ${req.requestedMedicines.map(m => esc(m.name)).join(', ')}</div>
                    ` : ''}
                    <div class="small text-secondary mb-2"><strong>Ghi chú khách:</strong> ${esc(req.customerNote || 'Không có')}</div>

                    ${req.pharmacistNote ? `
                      <div class="p-2 rounded-2 small mb-2" style="background:var(--color-primary-soft-bg);color:var(--color-primary-soft-fg);border:1px solid #99F6E4;">
                        <strong>Phản hồi từ nhà thuốc:</strong> ${esc(req.pharmacistNote)}
                      </div>
                    ` : `
                      <div class="small text-muted fst-italic mb-2">Đang chờ quản trị viên / dược sĩ kiểm tra ảnh đơn...</div>
                    `}

                    <div class="d-flex gap-2 align-items-center">
                      ${(req.images || []).map((img, idx) => `
                        <button type="button" class="btn-ph btn-ph-secondary btn-ph-sm" onclick="PharmacyStorefront.openImageZoomModal('${img.dataUrl}', '${esc(req.code)} - Ảnh ${idx + 1}')">
                          ${C.icon('eye', 14)} Xem ảnh đơn (${img.sizeKb || 120} KB)
                        </button>
                      `).join('')}
                    </div>
                  </article>
                `).join('')}
              </div>
            </div>
          </div>
        </div>
      </div>
    `;

    renderRxPreviewThumbnails();
  }

  function renderRxPreviewThumbnails() {
    const listEl = document.getElementById('rxPreviewList');
    if (!listEl) return;
    if (draftRxImages.length === 0) {
      listEl.innerHTML = `<div class="col-12 small text-muted">Chưa có ảnh đơn thuốc nào được chọn.</div>`;
      return;
    }
    listEl.innerHTML = draftRxImages.map((img, idx) => `
      <div class="col-6 col-sm-4">
        <div class="p-2 border rounded-2 bg-light position-relative">
          <img src="${img.dataUrl}" alt="${esc(img.name)}" class="w-100 rounded mb-1" style="height:96px;object-fit:cover;cursor:pointer;" onclick="PharmacyStorefront.openImageZoomModal('${img.dataUrl}', '${esc(img.name)}')" />
          <div class="small text-truncate">${esc(img.name)}</div>
          <div class="d-flex justify-content-between align-items-center mt-1">
            <span class="text-muted" style="font-size:11px;">${img.sizeKb} KB</span>
            <button type="button" class="btn btn-sm btn-link text-danger p-0 text-decoration-none" onclick="PharmacyStorefront.removeDraftRxImage(${idx})">Xóa</button>
          </div>
        </div>
      </div>
    `).join('');
  }

  function handleRxFilesSelected(input) {
    setFieldError('rxFileInput', null);
    const files = Array.from(input.files || []);
    files.forEach(file => {
      if (!file.type.startsWith('image/')) {
        setFieldError('rxFileInput', `File "${file.name}" không phải là định dạng ảnh hợp lệ (Chỉ nhận JPG, PNG, WEBP).`);
        return;
      }
      if (file.size > 5 * 1024 * 1024) {
        setFieldError('rxFileInput', `File "${file.name}" vượt quá giới hạn dung lượng 5MB.`);
        return;
      }
      const reader = new FileReader();
      reader.onload = (ev) => {
        draftRxImages.push({
          name: file.name,
          sizeKb: Math.max(1, Math.round(file.size / 1024)),
          dataUrl: ev.target.result
        });
        renderRxPreviewThumbnails();
      };
      reader.readAsDataURL(file);
    });
  }

  function addSampleRxImage() {
    setFieldError('rxFileInput', null);
    const idx = draftRxImages.length + 1;
    draftRxImages.push({
      name: `don-thuoc-mau-0${idx}.svg`,
      sizeKb: 135,
      dataUrl: I18n.generatePrescriptionSampleSvg(`RX-SAMPLE-0${idx}`, 'Trần Văn Tuấn', 'BS. Lê Hoàng Quân (Mẫu)', '03/11/2026', 'Amoxicillin 500mg — 01 Hộp')
    });
    renderRxPreviewThumbnails();
  }

  function simulateInvalidRxFile() {
    setFieldError('rxFileInput', 'Lỗi kiểm tra file: File "ho-so-benh-an.exe" (8.4 MB) sai định dạng ảnh cho phép (JPG/PNG/WEBP) và vượt quá dung lượng tối đa 5MB.');
  }

  function removeDraftRxImage(idx) {
    draftRxImages.splice(idx, 1);
    renderRxPreviewThumbnails();
  }

  function openImageZoomModal(dataUrl, title) {
    C.openModal({
      title: `Xem ảnh đơn thuốc: ${title}`,
      size: 'lg',
      bodyHtml: `
        <div class="text-center bg-light p-3 rounded border overflow-auto" style="max-height:70vh;">
          <img id="zoomableRxImg" src="${dataUrl}" alt="${esc(title)}" class="img-fluid rounded shadow-sm" style="transition:transform 150ms;" />
        </div>
      `,
      footerHtml: `
        <button type="button" class="btn-ph btn-ph-secondary btn-ph-sm" onclick="document.getElementById('zoomableRxImg').style.transform='scale(1.35)'">Phóng to 135%</button>
        <button type="button" class="btn-ph btn-ph-secondary btn-ph-sm" onclick="document.getElementById('zoomableRxImg').style.transform='scale(1)'">Kích thước gốc</button>
        <button type="button" class="btn-ph btn-ph-primary btn-ph-sm" onclick="PharmacyComponents.closeModal()">Đóng</button>
      `
    });
  }

  function handleRxSubmit(e) {
    e.preventDefault();
    const patientName = document.getElementById('rxPatientName').value.trim();
    const phone = document.getElementById('rxPhone').value.trim();
    const note = document.getElementById('rxNote').value.trim();

    setFieldError('rxFileInput', null);
    setFieldError('rxPatientName', null);
    setFieldError('rxPhone', null);

    if (draftRxImages.length === 0) {
      setFieldError('rxFileInput', 'Vui lòng tải lên ít nhất 1 ảnh chụp đơn thuốc.');
      return;
    }
    if (patientName.length < 2) {
      setFieldError('rxPatientName', 'Vui lòng nhập họ tên người bệnh/người liên hệ.');
      return;
    }
    if (!/^0[35789]\d{8}$/.test(phone)) {
      setFieldError('rxPhone', 'Vui lòng nhập số điện thoại 10 số hợp lệ.');
      return;
    }

    const db = DB.getDb();
    const checkedBoxes = Array.from(document.querySelectorAll('input[name="rxMedIds"]:checked'));
    const requestedMedicines = checkedBoxes.map(cb => {
      const m = db.medicines.find(x => x.id === Number(cb.value));
      return m ? { medicineId: m.id, name: m.name, unit: m.unit } : null;
    }).filter(Boolean);

    API.submitPrescriptionRequest({
      patientName,
      phone,
      customerNote: note,
      requestedMedicines,
      images: draftRxImages.slice()
    }).then(req => {
      draftRxImages = [];
      C.showToast(`Đã gửi yêu cầu tư vấn đơn thuốc ${req.code} (Trạng thái: Chờ kiểm tra).`, 'success');
      window.PharmacyApp.refreshCurrentRoute();
    });
  }

  // ==================== 11. CHÍNH SÁCH / LIÊN HỆ, 403, 404 ====================
  function renderAboutPolicy(container) {
    container.innerHTML = `
      <div class="container-xxl px-3 py-4">
        <div class="bg-white rounded-3 border p-4 mb-4" style="border-color:var(--color-border-subtle);">
          <h1 class="h4 fw-bold mb-2">Giới thiệu &amp; Chính sách Nhà thuốc Pharmacy</h1>
          <p class="text-secondary" style="max-width:72ch;">
            Hệ thống Nhà thuốc Pharmacy cam kết cung cấp thuốc chính hãng, niêm yết giá minh bạch và đồng hành chăm sóc sức khỏe cùng mọi gia đình.
          </p>
          <div class="row g-4 mt-1">
            <div class="col-md-6">
              <div class="p-3 rounded-3 border h-100">
                <h2 class="h6 fw-bold">1. Cam kết chất lượng &amp; Giá niêm yết</h2>
                <p class="small text-secondary mb-0">
                  100% sản phẩm đều có nguồn gốc xuất xứ rõ ràng, kiểm soát chặt chẽ số lô và hạn sử dụng, niêm yết giá chuẩn theo từng đơn vị bán (Hộp, Tuýp, Chai, Gói).
                </p>
              </div>
            </div>
            <div class="col-md-6">
              <div class="p-3 rounded-3 border h-100">
                <h2 class="h6 fw-bold">2. Quy định tư vấn Thuốc theo đơn bác sĩ</h2>
                <p class="small text-secondary mb-0">
                  Các thuốc kê đơn luôn được dược sĩ chuyên môn kiểm tra đơn thuốc hợp lệ và tư vấn hướng dẫn sử dụng chi tiết trước khi cấp phát.
                </p>
              </div>
            </div>
            <div class="col-md-6">
              <div class="p-3 rounded-3 border h-100">
                <h2 class="h6 fw-bold">3. Chính sách giao nhận &amp; Đổi trả</h2>
                <p class="small text-secondary mb-0">
                  Miễn phí giao hàng tiêu chuẩn cho đơn từ 150.000 ₫ hoặc hỗ trợ giữ hàng nhận trực tiếp tại quầy nhà thuốc gần nhất.
                </p>
              </div>
            </div>
            <div class="col-md-6">
              <div class="p-3 rounded-3 border h-100">
                <h2 class="h6 fw-bold">4. Thông tin liên hệ hỗ trợ</h2>
                <ul class="small text-secondary mb-0">
                  <li>Tổng đài tư vấn miễn phí: <strong>1800 6928</strong> (07:00 – 22:00)</li>
                  <li>Email chăm sóc khách hàng: <strong>cskh@pharmacy.vn</strong></li>
                  <li>Trụ sở chính: Số 42 Đường 3/2, Q. Ninh Kiều, TP. Cần Thơ</li>
                </ul>
              </div>
            </div>
          </div>
        </div>
      </div>
    `;
  }

  function renderForbidden403(container, attemptedRoute) {
    container.innerHTML = `
      <div class="container-xxl px-3 py-5">
        <div class="bg-white rounded-3 border p-5 mx-auto text-center" style="max-width:560px;border-color:var(--color-danger-border);">
          <div class="text-danger mb-3">${C.icon('shield', 44)}</div>
          <span class="status-badge status-badge-danger mb-2">HTTP 403 • Forbidden</span>
          <h1 class="h4 fw-bold mb-2">Không đủ quyền truy cập khu vực Quản trị</h1>
          <p class="small text-secondary mb-4">
            Đường dẫn <code class="font-mono-code">${esc(attemptedRoute || '#/admin')}</code> chỉ dành cho tài khoản có vai trò <strong>ADMIN</strong>. Tài khoản khách hàng (<code>CUSTOMER</code>) hoặc khách chưa đăng nhập không được phép truy cập màn hình này.
          </p>
          <div class="d-flex flex-wrap justify-content-center gap-2">
            <a href="#/home" class="btn-ph btn-ph-secondary">Về Trang chủ khách hàng</a>
            <button type="button" class="btn-ph btn-ph-primary" onclick="PharmacyApp.switchRole('ADMIN')">
              ${C.icon('shield', 15)} Chuyển sang vai trò ADMIN (Preview) để xem
            </button>
          </div>
        </div>
      </div>
    `;
  }

  function renderNotFound404(container, badRoute) {
    container.innerHTML = `
      <div class="container-xxl px-3 py-5">
        <div class="bg-white rounded-3 border p-5 mx-auto text-center" style="max-width:540px;">
          <div class="text-secondary mb-3">${C.icon('alert', 44)}</div>
          <span class="status-badge status-badge-neutral mb-2">HTTP 404 • Not Found</span>
          <h1 class="h4 fw-bold mb-2">Không tìm thấy trang yêu cầu</h1>
          <p class="small text-secondary mb-4">
            Đường dẫn <code class="font-mono-code">${esc(badRoute || window.location.hash)}</code> không tồn tại hoặc đã thay đổi.
          </p>
          <div class="d-flex justify-content-center gap-2">
            <a href="#/home" class="btn-ph btn-ph-primary">Về Trang chủ</a>
            <a href="#/products" class="btn-ph btn-ph-secondary">Xem Danh mục thuốc</a>
          </div>
        </div>
      </div>
    `;
  }

  return {
    renderHome,
    renderCatalog,
    loadCatalogResults,
    updateFilter,
    changeCatalogPage,
    resetCatalogFilters,
    openMobileFilterDrawer,
    closeMobileFilterDrawer,
    renderProductDetail,
    stepDetailQty,
    addDetailToCart,
    renderCart,
    updateCartItemQty,
    confirmRemoveCartItem,
    handleApplyVoucherForm,
    applyVoucherQuick,
    removeVoucher,
    setCartSimulation,
    populateSampleCart,
    renderCheckout,
    fillCheckoutAddress,
    handleCheckoutSubmit,
    renderOrderResult,
    renderAuthView,
    togglePasswordVisibility,
    quickFillLogin,
    handleLoginSubmit,
    handleRegisterSubmit,
    handleForgotSubmit,
    renderAccount,
    handleUpdateProfile,
    handleChangePassword,
    setDefaultAddress,
    handleAddAddress,
    renderMyOrders,
    filterMyOrders,
    changeMyOrdersPage,
    openMyOrderDetailModal,
    promptCancelMyOrder,
    confirmCancelMyOrder,
    renderPrescriptionUpload,
    handleRxFilesSelected,
    addSampleRxImage,
    simulateInvalidRxFile,
    removeDraftRxImage,
    openImageZoomModal,
    handleRxSubmit,
    renderAboutPolicy,
    renderForbidden403,
    renderNotFound404
  };
})();
