/* ==========================================================================
   app.js — Bộ định tuyến (Hash Router), Điều phối Vai trò, Giỏ hàng & Công cụ Kiểm thử
   ========================================================================== */

window.PharmacyApp = (function () {
  const DB = window.PharmacyMockDB;
  const API = window.PharmacyApiService;
  const C = window.PharmacyComponents;
  const Store = window.PharmacyStorefront;
  const Admin = window.PharmacyAdmin;
  const I18n = window.PharmacyI18n;
  const esc = C.escapeHtml;

  function parseHash() {
    const raw = window.location.hash || '#/home';
    const clean = raw.startsWith('#') ? raw.slice(1) : raw;
    const [pathPart, queryPart] = clean.split('?');
    const segments = pathPart.split('/').filter(Boolean);
    const query = {};
    if (queryPart) {
      const sp = new URLSearchParams(queryPart);
      sp.forEach((v, k) => { query[k] = v; });
    }
    return { raw, pathPart, segments, query };
  }

  function renderApp() {
    const { raw, segments, query } = parseHash();
    const session = DB.getSession();

    const previewRoot = document.getElementById('appPreviewToolbar');
    const headerRoot = document.getElementById('appStoreHeader');
    const mainRoot = document.getElementById('appMainView');
    const footerRoot = document.getElementById('appStoreFooter');

    if (previewRoot) {
      previewRoot.innerHTML = C.renderPreviewToolbar();
    }

    const firstSeg = segments[0] || 'home';

    // Nếu truy cập nhánh /admin/*
    if (firstSeg === 'admin') {
      if (session.accessRole !== 'ADMIN') {
        // Không cho khách truy cập màn quản trị trong luồng sản phẩm -> hiện màn 403
        if (headerRoot) headerRoot.innerHTML = C.renderStoreHeader('403', query);
        if (footerRoot) footerRoot.innerHTML = C.renderStoreFooter();
        Store.renderForbidden403(mainRoot, raw);
        I18n.applyDomTranslation(document.body);
        return;
      }

      // Admin Layout có Sidebar & Topbar riêng, ẩn Storefront Header/Footer
      if (headerRoot) headerRoot.innerHTML = '';
      if (footerRoot) footerRoot.innerHTML = '';
      const adminSec = segments[1] || 'dashboard';
      Admin.renderAdminShell(mainRoot, adminSec, query);
      I18n.applyDomTranslation(document.body);
      return;
    }

    // Các trang thuộc Storefront
    if (headerRoot) headerRoot.innerHTML = C.renderStoreHeader(firstSeg, query);
    if (footerRoot) footerRoot.innerHTML = C.renderStoreFooter();

    switch (firstSeg) {
      case 'home':
        Store.renderHome(mainRoot);
        break;
      case 'products':
        Store.renderCatalog(mainRoot, query);
        break;
      case 'product':
        Store.renderProductDetail(mainRoot, segments[1] || 1);
        break;
      case 'cart':
        Store.renderCart(mainRoot);
        break;
      case 'checkout':
        Store.renderCheckout(mainRoot);
        break;
      case 'order-result':
        Store.renderOrderResult(mainRoot, segments[1] || 929);
        break;
      case 'login':
        Store.renderAuthView(mainRoot, 'login', query);
        break;
      case 'register':
        Store.renderAuthView(mainRoot, 'register', query);
        break;
      case 'forgot-password':
        Store.renderAuthView(mainRoot, 'forgot', query);
        break;
      case 'account':
        Store.renderAccount(mainRoot);
        break;
      case 'my-orders':
        Store.renderMyOrders(mainRoot);
        break;
      case 'prescription-upload':
        Store.renderPrescriptionUpload(mainRoot, query);
        break;
      case 'about-policy':
        Store.renderAboutPolicy(mainRoot);
        break;
      case '403':
        Store.renderForbidden403(mainRoot, query.from || '#/admin/dashboard');
        break;
      case '404':
      default:
        Store.renderNotFound404(mainRoot, raw);
        break;
    }
    I18n.applyDomTranslation(document.body);
  }

  function refreshCurrentRoute() {
    renderApp();
  }

  function switchRole(role) {
    const session = DB.getSession();
    session.accessRole = role;
    if (role === 'CUSTOMER' && !session.customerId) {
      session.customerId = 1;
    }
    DB.saveSession();

    const { segments } = parseHash();
    if (role === 'ADMIN' && segments[0] !== 'admin') {
      window.location.hash = '#/admin/dashboard';
      C.showToast('Đã chuyển sang chế độ Quản trị viên (ADMIN).');
      return;
    }
    if (role !== 'ADMIN' && segments[0] === 'admin') {
      window.location.hash = '#/home';
      C.showToast(`Đã chuyển sang vai trò ${role === 'GUEST' ? 'Khách chưa đăng nhập' : 'CUSTOMER'} (Quay về Storefront).`);
      return;
    }
    C.showToast(`Đã chuyển trạng thái truy cập sang: ${role}`);
    renderApp();
  }

  function toggleLanguage() {
    const next = I18n.getLang() === 'vi' ? 'en' : 'vi';
    I18n.setLang(next);
    C.showToast(next === 'en' ? 'Switched interface language to English (Prices remain in VND ₫).' : 'Đã chuyển ngôn ngữ giao diện sang Tiếng Việt.');
    renderApp();
  }

  function toggleSimulateApiError() {
    const session = DB.getSession();
    session.simulateApiError = !session.simulateApiError;
    DB.saveSession();
    C.showToast(
      session.simulateApiError
        ? 'Đã BẬT mô phỏng lỗi mạng 503. Hãy thử lọc sản phẩm để xem trạng thái báo lỗi AJAX.'
        : 'Đã TẮT mô phỏng lỗi mạng API.',
      session.simulateApiError ? 'warning' : 'success'
    );
    renderApp();
  }

  function handleHeaderSearch(e) {
    e.preventDefault();
    const inp = document.getElementById('globalSearchInput');
    const q = inp ? inp.value.trim() : '';
    window.location.hash = `#/products?q=${encodeURIComponent(q)}`;
  }

  function addToCart(medicineId, qty = 1) {
    const db = DB.getDb();
    const rawMed = db.medicines.find(m => m.id === Number(medicineId));
    const med = DB.enrichMedicine(rawMed);
    if (!med) return;

    if (med.isPrescription) {
      C.showToast(`"${med.name}" là thuốc kê đơn. Vui lòng gửi ảnh đơn thuốc để được tư vấn.`, 'warning');
      window.location.hash = `#/prescription-upload?medicineId=${med.id}`;
      return;
    }
    if (!med.isSelling || !med.isActive || med.availableStock <= 0) {
      C.showToast(`Sản phẩm "${med.name}" hiện tạm hết tồn có thể bán.`, 'error');
      return;
    }

    const session = DB.getSession();
    session.cart = session.cart || [];
    const existing = session.cart.find(i => i.medicineId === med.id);
    if (existing) {
      existing.quantity = Math.min(med.availableStock, existing.quantity + Number(qty));
      existing.addedPrice = med.price;
    } else {
      session.cart.push({
        medicineId: med.id,
        quantity: Math.min(med.availableStock, Number(qty)),
        addedPrice: med.price
      });
    }
    DB.saveSession();
    const badge = document.getElementById('headerCartCount');
    if (badge) {
      badge.textContent = session.cart.reduce((s, i) => s + Number(i.quantity || 0), 0);
    }
    C.showToast(`Đã thêm ${qty} ${med.unit} "${med.name}" vào giỏ hàng.`);
  }

  function openApiInspectorModal() {
    const logs = API.getLogs();
    C.openModal({
      title: 'Nhật ký Hợp đồng REST API Mock (Async Promise Calls)',
      size: 'lg',
      bodyHtml: `
        <p class="small text-secondary mb-3">
          Mọi thao tác tìm kiếm, lọc, phân trang, tạo đơn và CRUD trong prototype đều đi qua <code>PharmacyApiService</code> với độ trễ mạng giả lập. Khi triển khai ASP.NET MVC, các lời gọi này sẽ ánh xạ sang <code>ApiController</code> hoặc <code>Controller Action</code> thực tế.
        </p>
        <div class="table-responsive">
          <table class="table-ph">
            <thead>
              <tr><th>Thời gian</th><th>Method</th><th>Endpoint</th><th>HTTP Status</th><th class="text-end">Độ trễ</th><th>Tóm tắt phản hồi</th></tr>
            </thead>
            <tbody>
              ${logs.length === 0 ? `<tr><td colspan="6" class="text-center py-3 text-muted">Chưa có lời gọi API nào.</td></tr>` : logs.map(l => `
                <tr>
                  <td class="tabular-nums small">${esc(l.time)}</td>
                  <td><span class="font-mono-code fw-bold">${esc(l.method)}</span></td>
                  <td class="font-mono-code">${esc(l.endpoint)}</td>
                  <td>
                    <span class="status-badge status-badge-${l.status >= 400 ? 'danger' : 'success'}">${l.status}</span>
                  </td>
                  <td class="text-end tabular-nums">${l.durationMs} ms</td>
                  <td class="small">${esc(l.summary)}</td>
                </tr>
              `).join('')}
            </tbody>
          </table>
        </div>
      `,
      footerHtml: `<button type="button" class="btn-ph btn-ph-primary" onclick="PharmacyComponents.closeModal()">Đóng</button>`
    });
  }

  function openSessionCookieModal() {
    const session = DB.getSession();
    const lang = I18n.getLang();
    C.openModal({
      title: 'Trạng thái Mô phỏng Session & Cookies (Yêu cầu 3.4)',
      size: 'md',
      bodyHtml: `
        <div class="p-3 rounded-3 border bg-light small mb-3">
          <strong>Lưu ý kiến trúc:</strong> Trong bản thiết kế Prototype chạy trình duyệt, trạng thái phiên và giỏ hàng được mô phỏng qua bộ nhớ trình duyệt. Khi triển khai Backend ASP.NET MVC, phiên đăng nhập và quyền truy cập bắt buộc xác thực qua Server-side Session / Cookie <code>HttpOnly</code>.
        </div>
        <h3 class="h6 fw-bold mb-2">1. Dữ liệu mô phỏng Server Session</h3>
        <ul class="small text-secondary mb-3">
          <li><strong>Trạng thái truy cập (Access Role):</strong> <code>${esc(session.accessRole)}</code></li>
          <li><strong>Khách hàng phiên hiện tại (CustomerId):</strong> <code>${esc(session.customerId)}</code></li>
          <li><strong>Số mặt hàng giữ trong giỏ (Session Cart):</strong> <code>${(session.cart || []).length} mặt hàng</code> (Được bảo toàn khi chuyển qua màn Đăng nhập)</li>
          <li><strong>Mã giảm giá đang gắn phiên:</strong> <code>${esc(session.appliedVoucherCode || 'Chưa gắn')}</code></li>
        </ul>
        <h3 class="h6 fw-bold mb-2">2. Tùy chọn Cookies trình duyệt</h3>
        <ul class="small text-secondary mb-3">
          <li><strong>Cookie ngôn ngữ (<code>pharmacy_proto_lang</code>):</strong> <code>${esc(lang)}</code></li>
          <li><strong>Cookie Consent (<code>pharmacy_cookie_consent</code>):</strong> <code>${esc(session.cookieConsent)}</code></li>
        </ul>
        <div class="d-flex gap-2">
          <button type="button" class="btn-ph btn-ph-mint btn-ph-sm" onclick="PharmacyApp.setCookieConsent('ACCEPTED')">Chấp nhận tất cả Cookie</button>
          <button type="button" class="btn-ph btn-ph-secondary btn-ph-sm" onclick="PharmacyApp.setCookieConsent('ESSENTIAL_ONLY')">Chỉ dùng Cookie thiết yếu</button>
        </div>
      `,
      footerHtml: `<button type="button" class="btn-ph btn-ph-primary" onclick="PharmacyComponents.closeModal()">Đóng</button>`
    });
  }

  function setCookieConsent(val) {
    const session = DB.getSession();
    session.cookieConsent = val;
    DB.saveSession();
    C.closeModal();
    C.showToast(`Đã lưu tùy chọn Cookie: ${val}`);
    renderApp();
  }

  function confirmResetData() {
    C.openConfirmModal({
      title: 'Khôi phục dữ liệu mẫu gốc (Reset Prototype Data)',
      message: 'Đặt lại toàn bộ 20 sản phẩm, 24 lô hàng, 12 đơn hàng, phiếu nhập, yêu cầu đơn thuốc và giỏ hàng về trạng thái chuẩn ban đầu?',
      confirmText: 'Khôi phục dữ liệu gốc',
      danger: true,
      onConfirm: () => {
        DB.resetAll();
        C.showToast('Đã khôi phục toàn bộ dữ liệu mẫu về trạng thái gốc ban đầu.');
        renderApp();
      }
    });
  }

  function init() {
    window.addEventListener('hashchange', renderApp);
    window.addEventListener('pharmacy:apilog', () => {
      const badge = document.getElementById('apiLogCountBadge');
      if (badge) badge.textContent = API.getLogs().length;
    });
    window.addEventListener('keydown', (e) => {
      if (e.key === 'Escape') {
        const modalBackdrop = document.getElementById('activeModalBackdrop');
        if (modalBackdrop) {
          C.closeModal();
        }
        Store.closeMobileFilterDrawer();
      }
    });

    if (!window.location.hash) {
      window.location.hash = '#/home';
    } else {
      renderApp();
    }
  }

  return {
    init,
    refreshCurrentRoute,
    switchRole,
    toggleLanguage,
    toggleSimulateApiError,
    handleHeaderSearch,
    addToCart,
    openApiInspectorModal,
    openSessionCookieModal,
    setCookieConsent,
    confirmResetData
  };
})();

document.addEventListener('DOMContentLoaded', () => {
  window.PharmacyApp.init();
});
