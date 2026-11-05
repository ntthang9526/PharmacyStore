/* ==========================================================================
   api-service.js — Tầng Mô phỏng REST API Bất đồng bộ (Async Promise Service)
   Mô phỏng độ trễ mạng, kiểm tra quyền, kiểm tra lại tồn kho/giá/voucher,
   kiểm tra xung đột phiên bản đơn (OrderVersion) và làm sạch HTML (XSS Sanitizer)
   ========================================================================== */

window.PharmacyApiService = (function () {
  const DB = window.PharmacyMockDB;
  const apiLogs = [];

  function logCall(method, endpoint, status, durationMs, summary) {
    apiLogs.unshift({
      id: Date.now() + Math.random(),
      time: new Date().toLocaleTimeString('vi-VN'),
      method,
      endpoint,
      status,
      durationMs,
      summary
    });
    if (apiLogs.length > 40) apiLogs.pop();
    window.dispatchEvent(new CustomEvent('pharmacy:apilog'));
  }

  function simulateNetwork(method, endpoint, handler, options = {}) {
    const session = DB.getSession();
    const delay = typeof options.delay === 'number' ? options.delay : (Number(session.simulateLatencyMs) || 180);
    const start = performance.now();

    return new Promise((resolve, reject) => {
      setTimeout(() => {
        const elapsed = Math.round(performance.now() - start);
        if (session.simulateApiError && !options.ignoreSimulatedError) {
          logCall(method, endpoint, 503, elapsed, 'Mô phỏng lỗi kết nối máy chủ (Simulated 503 Service Unavailable)');
          return reject({
            status: 503,
            code: 'SIMULATED_NETWORK_ERROR',
            message: 'Không thể kết nối tới dịch vụ máy chủ (Đang bật chế độ Mô phỏng lỗi API trên thanh công cụ). Vui lòng tắt mô phỏng lỗi hoặc bấm Thử lại.'
          });
        }
        try {
          const result = handler();
          logCall(method, endpoint, result.status || 200, elapsed, result.logSummary || 'OK');
          resolve(result.data !== undefined ? result.data : result);
        } catch (err) {
          const status = err.status || 400;
          logCall(method, endpoint, status, elapsed, err.message || 'Bad Request');
          reject(err);
        }
      }, delay);
    });
  }

  // Làm sạch HTML mô tả sản phẩm (Loại bỏ <script>, <iframe>, thuộc tính on* và javascript:)
  function sanitizeRichHtml(rawHtml) {
    if (!rawHtml) return '';
    const parser = new DOMParser();
    const doc = parser.parseFromString(String(rawHtml), 'text/html');
    const forbiddenTags = ['script', 'iframe', 'object', 'embed', 'style', 'link', 'meta', 'form'];
    forbiddenTags.forEach(tag => {
      doc.querySelectorAll(tag).forEach(el => el.remove());
    });
    doc.body.querySelectorAll('*').forEach(el => {
      Array.from(el.attributes).forEach(attr => {
        const name = attr.name.toLowerCase();
        const val = attr.value.toLowerCase().trim();
        if (name.startsWith('on') || val.startsWith('javascript:') || val.startsWith('data:text/html')) {
          el.removeAttribute(attr.name);
        }
      });
    });
    return doc.body.innerHTML;
  }

  // Kiểm tra voucher
  function evaluateVoucher(code, subtotal) {
    if (!code || !code.trim()) {
      return { valid: false, discountAmount: 0, message: 'Vui lòng nhập mã giảm giá.' };
    }
    const cleanCode = code.trim().toUpperCase();
    const voucher = DB.getDb().vouchers.find(v => v.code.toUpperCase() === cleanCode);
    if (!voucher) {
      return { valid: false, code: cleanCode, discountAmount: 0, message: `Mã giảm giá "${cleanCode}" không tồn tại.` };
    }
    if (!voucher.isActive) {
      return { valid: false, code: cleanCode, discountAmount: 0, message: `Mã giảm giá "${cleanCode}" hiện đang tạm khóa.` };
    }
    if (voucher.quantity <= 0) {
      return { valid: false, code: cleanCode, discountAmount: 0, message: `Voucher "${cleanCode}" không còn lượt sử dụng.` };
    }
    if (voucher.minOrderValue && subtotal < voucher.minOrderValue) {
      return {
        valid: false,
        code: cleanCode,
        discountAmount: 0,
        message: `Mã "${cleanCode}" yêu cầu đơn hàng tối thiểu ${window.PharmacyI18n.formatCurrency(voucher.minOrderValue)}.`
      };
    }

    let discount = 0;
    let cappedWarning = null;
    if (voucher.discountType === 'FIXED') {
      discount = voucher.discountValue;
      if (discount > subtotal) {
        discount = subtotal;
        cappedWarning = `Giá trị voucher (${window.PharmacyI18n.formatCurrency(voucher.discountValue)}) vượt tổng tiền hàng nên được giới hạn bằng ${window.PharmacyI18n.formatCurrency(subtotal)}.`;
      }
    } else {
      discount = Math.round((subtotal * voucher.discountValue) / 100);
      if (voucher.maxDiscountCap && discount > voucher.maxDiscountCap) {
        discount = voucher.maxDiscountCap;
        cappedWarning = `Giảm tối đa ${window.PharmacyI18n.formatCurrency(voucher.maxDiscountCap)} theo điều kiện của mã ${cleanCode}.`;
      }
    }

    return {
      valid: true,
      voucher,
      code: voucher.code,
      discountAmount: discount,
      cappedWarning,
      message: `Đã áp dụng mã ${voucher.code}: giảm ${window.PharmacyI18n.formatCurrency(discount)}.`
    };
  }

  // ==================== PUBLIC / CUSTOMER ENDPOINTS ====================

  function getProducts(params = {}) {
    const q = (params.q || '').trim().toLowerCase();
    const categoryId = params.categoryId ? Number(params.categoryId) : null;
    const supplierId = params.supplierId ? Number(params.supplierId) : null;
    const rxMode = params.rxMode || 'ALL'; // 'ALL' | 'OTC' | 'RX'
    const priceRange = params.priceRange || 'ALL'; // 'ALL' | 'UNDER_50K' | '50K_100K' | 'OVER_100K'
    const inStockOnly = Boolean(params.inStockOnly);
    const sort = params.sort || 'featured';
    const page = Math.max(1, Number(params.page) || 1);
    const pageSize = Math.max(1, Number(params.pageSize) || 8);

    return simulateNetwork('GET', `/api/products?page=${page}&q=${encodeURIComponent(q)}`, () => {
      const db = DB.getDb();
      let list = db.medicines
        .filter(m => m.isActive) // Khách hàng chỉ xem thuốc chưa bị ẩn (IsActive = true)
        .map(m => DB.enrichMedicine(m));

      if (q) {
        list = list.filter(m =>
          m.name.toLowerCase().includes(q) ||
          (m.activeIngredient && m.activeIngredient.toLowerCase().includes(q)) ||
          (m.barCode && m.barCode.toLowerCase().includes(q)) ||
          m.categoryName.toLowerCase().includes(q)
        );
      }
      if (categoryId) {
        list = list.filter(m => m.categoryId === categoryId);
      }
      if (supplierId) {
        list = list.filter(m => m.supplierId === supplierId);
      }
      if (rxMode === 'OTC') {
        list = list.filter(m => !m.isPrescription);
      } else if (rxMode === 'RX') {
        list = list.filter(m => m.isPrescription);
      }
      if (priceRange === 'UNDER_50K') {
        list = list.filter(m => m.price < 50000);
      } else if (priceRange === '50K_100K') {
        list = list.filter(m => m.price >= 50000 && m.price <= 100000);
      } else if (priceRange === 'OVER_100K') {
        list = list.filter(m => m.price > 100000);
      }
      if (inStockOnly) {
        list = list.filter(m => m.isSelling && m.availableStock > 0);
      }

      if (sort === 'price_asc') {
        list.sort((a, b) => a.price - b.price);
      } else if (sort === 'price_desc') {
        list.sort((a, b) => b.price - a.price);
      } else if (sort === 'name_asc') {
        list.sort((a, b) => a.name.localeCompare(b.name, 'vi'));
      } else {
        list.sort((a, b) => (b.featured ? 1 : 0) - (a.featured ? 1 : 0) || a.id - b.id);
      }

      const totalItems = list.length;
      const totalPages = Math.max(1, Math.ceil(totalItems / pageSize));
      const safePage = Math.min(page, totalPages);
      const items = list.slice((safePage - 1) * pageSize, safePage * pageSize);

      return {
        status: 200,
        logSummary: `Trả về ${items.length}/${totalItems} sản phẩm`,
        data: { items, totalItems, page: safePage, pageSize, totalPages }
      };
    });
  }

  function getProductById(id) {
    return simulateNetwork('GET', `/api/products/${id}`, () => {
      const db = DB.getDb();
      const raw = db.medicines.find(m => m.id === Number(id));
      if (!raw || !raw.isActive) {
        throw { status: 404, message: 'Không tìm thấy sản phẩm hoặc sản phẩm đã ngừng hiển thị.' };
      }
      const product = DB.enrichMedicine(raw);
      const relatedProducts = db.medicines
        .filter(m => m.isActive && m.categoryId === product.categoryId && m.id !== product.id)
        .slice(0, 4)
        .map(m => DB.enrichMedicine(m));

      return {
        status: 200,
        logSummary: `Chi tiết thuốc: ${product.name}`,
        data: { product, relatedProducts }
      };
    });
  }

  function validateVoucherApi(code, subtotal) {
    return simulateNetwork('POST', '/api/vouchers/validate', () => {
      const res = evaluateVoucher(code, subtotal);
      if (!res.valid) {
        throw { status: 400, message: res.message };
      }
      return {
        status: 200,
        logSummary: `Voucher ${res.code} hợp lệ (-${res.discountAmount}đ)`,
        data: res
      };
    });
  }

  // Đặt hàng Checkout có kiểm tra lại tồn có thể bán, giá bán, trạng thái thuốc và lượt voucher
  function createOrder(payload) {
    return simulateNetwork('POST', '/api/orders', () => {
      const session = DB.getSession();
      const db = DB.getDb();

      if (session.accessRole === 'GUEST') {
        throw { status: 401, message: 'Vui lòng đăng nhập tài khoản trước khi xác nhận đặt hàng.' };
      }

      const customer = db.customers.find(c => c.id === session.customerId) || db.customers[0];
      if (customer && customer.isLocked) {
        throw { status: 403, message: 'Tài khoản khách hàng của bạn hiện đang tạm khóa. Vui lòng liên hệ nhà thuốc.' };
      }

      if (!session.cart || session.cart.length === 0) {
        throw { status: 400, message: 'Giỏ hàng đang trống, không thể tạo đơn hàng.' };
      }

      const warnings = [];
      let subtotal = 0;
      const orderItems = [];

      for (const cartItem of session.cart) {
        const rawMed = db.medicines.find(m => m.id === cartItem.medicineId);
        const med = DB.enrichMedicine(rawMed);

        if (!med || !med.isActive || !med.isSelling) {
          warnings.push(`Sản phẩm "${med ? med.name : '#' + cartItem.medicineId}" vừa chuyển sang trạng thái ngừng kinh doanh.`);
          continue;
        }
        if (med.isPrescription) {
          warnings.push(`Thuốc kê đơn "${med.name}" cần gửi đơn tư vấn, không thể đặt mua trực tiếp.`);
          continue;
        }

        // Hỗ trợ chế độ mô phỏng thiếu tồn hoặc đổi giá ngay tại Checkout
        let effectiveAvailable = med.availableStock;
        if (session.cartSimulationMode === 'STOCK_SHORTAGE') {
          effectiveAvailable = Math.max(0, cartItem.quantity - 1);
        }

        if (cartItem.quantity > effectiveAvailable) {
          warnings.push(`Sản phẩm "${med.name}" chỉ còn ${effectiveAvailable} ${med.unit} có thể bán (bạn đang chọn ${cartItem.quantity} ${med.unit}).`);
        }

        let effectivePrice = med.price;
        if (session.cartSimulationMode === 'PRICE_CHANGED' && cartItem.addedPrice === med.price) {
          effectivePrice = med.price + 4000;
        }

        if (cartItem.addedPrice && cartItem.addedPrice !== effectivePrice) {
          warnings.push(`Giá niêm yết của "${med.name}" đã cập nhật từ ${window.PharmacyI18n.formatCurrency(cartItem.addedPrice)} sang ${window.PharmacyI18n.formatCurrency(effectivePrice)} / ${med.unit}.`);
          cartItem.addedPrice = effectivePrice;
        }

        subtotal += effectivePrice * cartItem.quantity;
        orderItems.push({
          medicineId: med.id,
          medicineName: med.name,
          unit: med.unit,
          quantity: cartItem.quantity,
          unitPrice: effectivePrice
        });
      }

      let discountAmount = 0;
      let appliedVoucher = null;
      if (session.appliedVoucherCode) {
        const vRes = evaluateVoucher(session.appliedVoucherCode, subtotal);
        if (!vRes.valid) {
          warnings.push(vRes.message);
        } else {
          discountAmount = vRes.discountAmount;
          appliedVoucher = vRes.voucher;
        }
      }

      if (warnings.length > 0) {
        // Reset simulation flag after surfacing the conflict once so user can proceed on next confirm if adjusted
        if (session.cartSimulationMode === 'PRICE_CHANGED') {
          session.cartSimulationMode = 'NORMAL';
          DB.saveSession();
        }
        throw {
          status: 409,
          code: 'CHECKOUT_REVALIDATION_CONFLICT',
          message: 'Thông tin tồn kho, giá bán hoặc mã giảm giá đã thay đổi tại thời điểm xác nhận. Vui lòng kiểm tra lại trước khi đặt hàng.',
          warnings
        };
      }

      const shippingFee = payload.deliveryMethod === 'PICKUP' ? 0 : (subtotal - discountAmount >= 150000 ? 0 : 15000);
      const totalAmount = Math.max(0, subtotal - discountAmount + shippingFee);

      const nextId = db.orders.reduce((max, o) => Math.max(max, o.id), 934) + 1;
      const orderCode = 'HD' + String(nextId).padStart(5, '0');
      const nowIso = new Date().toISOString();

      // Đơn vừa đặt KHÔNG tự động hiện Đã thanh toán
      const paymentStatus = payload.paymentMethod === 'BANK_TRANSFER' ? 'PENDING_TRANSFER' : 'UNPAID';

      const newOrder = {
        id: nextId,
        code: orderCode,
        customerId: customer.id,
        customerName: payload.recipientName.trim(),
        customerPhone: payload.recipientPhone.trim(),
        shippingAddress: payload.deliveryMethod === 'PICKUP'
          ? 'Nhận trực tiếp tại quầy Nhà thuốc Pharmacy [Dữ liệu mẫu]'
          : payload.shippingAddress.trim(),
        deliveryMethod: payload.deliveryMethod,
        channel: 'WEB',
        orderDate: nowIso,
        orderStatus: 'NEW',
        paymentMethod: payload.paymentMethod,
        paymentStatus,
        shippingStatus: 'NOT_SHIPPED',
        voucherCode: appliedVoucher ? appliedVoucher.code : null,
        subtotalAmount: subtotal,
        discountAmount,
        shippingFee,
        totalAmount,
        orderVersion: 1,
        note: (payload.note || '').trim(),
        items: orderItems,
        history: [
          {
            time: nowIso,
            actor: customer.fullName,
            text: `Tạo đơn hàng mới trên website (${payload.paymentMethod === 'BANK_TRANSFER' ? 'Chuyển khoản — Chờ xác nhận' : 'Thanh toán khi nhận hàng COD — Chưa thu tiền'})`
          }
        ]
      };

      if (appliedVoucher) {
        appliedVoucher.quantity = Math.max(0, appliedVoucher.quantity - 1);
      }

      db.orders.unshift(newOrder);
      session.cart = [];
      session.appliedVoucherCode = null;
      session.cartSimulationMode = 'NORMAL';
      DB.saveDb();
      DB.saveSession();

      return {
        status: 201,
        logSummary: `Tạo đơn ${newOrder.code} (${window.PharmacyI18n.formatCurrency(newOrder.totalAmount)})`,
        data: newOrder
      };
    });
  }

  function cancelCustomerOrder(orderId, reason) {
    return simulateNetwork('POST', `/api/me/orders/${orderId}/cancel`, () => {
      const db = DB.getDb();
      const session = DB.getSession();
      const order = db.orders.find(o => o.id === Number(orderId));
      if (!order || order.customerId !== session.customerId) {
        throw { status: 404, message: 'Không tìm thấy đơn hàng thuộc tài khoản của bạn.' };
      }
      if (!['NEW', 'CONFIRMED'].includes(order.orderStatus) || order.paymentStatus === 'PAID') {
        throw { status: 400, message: 'Chỉ được phép hủy đơn khi đơn mới tạo/đã xác nhận và chưa thu tiền.' };
      }
      order.orderStatus = 'CANCELLED';
      order.cancelReason = reason || 'Khách hàng yêu cầu hủy trên website';
      order.orderVersion += 1;
      order.history.push({
        time: new Date().toISOString(),
        actor: 'Khách hàng',
        text: `Yêu cầu hủy đơn: ${order.cancelReason}`
      });
      DB.saveDb();
      return {
        status: 200,
        logSummary: `Khách hủy đơn ${order.code}`,
        data: order
      };
    });
  }

  function submitPrescriptionRequest(payload) {
    return simulateNetwork('POST', '/api/prescription-requests', () => {
      const db = DB.getDb();
      const session = DB.getSession();
      const nextNum = db.prescriptionRequests.length + 1;
      const code = `RX-2026-${String(nextNum).padStart(3, '0')}`;
      const nowIso = new Date().toISOString();

      const newReq = {
        id: 300 + nextNum,
        code,
        customerId: session.accessRole === 'CUSTOMER' ? session.customerId : 1,
        patientName: payload.patientName.trim(),
        phone: payload.phone.trim(),
        createdAt: nowIso,
        updatedAt: nowIso,
        status: 'PENDING_REVIEW',
        requestedMedicines: payload.requestedMedicines || [],
        customerNote: (payload.customerNote || '').trim(),
        pharmacistNote: '',
        images: payload.images || []
      };

      db.prescriptionRequests.unshift(newReq);
      DB.saveDb();

      return {
        status: 201,
        logSummary: `Gửi đơn thuốc tư vấn ${code}`,
        data: newReq
      };
    });
  }

  // ==================== ADMIN ENDPOINTS ====================

  function saveAdminProduct(payload) {
    const isEdit = Boolean(payload.id);
    const endpoint = isEdit ? `/api/admin/products/${payload.id}` : '/api/admin/products';
    const method = isEdit ? 'PUT' : 'POST';

    return simulateNetwork(method, endpoint, () => {
      const db = DB.getDb();
      const cleanHtml = sanitizeRichHtml(payload.descriptionHtml || '');

      if (isEdit) {
        const idx = db.medicines.findIndex(m => m.id === Number(payload.id));
        if (idx === -1) throw { status: 404, message: 'Không tìm thấy sản phẩm để cập nhật.' };
        const updated = Object.assign({}, db.medicines[idx], {
          name: payload.name.trim(),
          categoryId: Number(payload.categoryId),
          supplierId: Number(payload.supplierId),
          barCode: (payload.barCode || '').trim(),
          activeIngredient: (payload.activeIngredient || '').trim(),
          unit: payload.unit.trim(),
          packagingSpec: (payload.packagingSpec || '').trim(),
          price: Number(payload.price),
          isPrescription: Boolean(payload.isPrescription),
          isSelling: Boolean(payload.isSelling),
          isActive: Boolean(payload.isActive),
          descriptionHtml: cleanHtml,
          images: payload.images && payload.images.length ? payload.images : db.medicines[idx].images
        });
        db.medicines[idx] = updated;
        DB.saveDb();
        return {
          status: 200,
          logSummary: `Cập nhật thuốc #${updated.id}: ${updated.name}`,
          data: DB.enrichMedicine(updated)
        };
      } else {
        const nextId = db.medicines.reduce((max, m) => Math.max(max, m.id), 20) + 1;
        const slug = payload.name.trim().toLowerCase().replace(/[^a-z0-9]+/g, '-');
        const created = {
          id: nextId,
          name: payload.name.trim(),
          slug,
          categoryId: Number(payload.categoryId),
          supplierId: Number(payload.supplierId),
          barCode: (payload.barCode || `8935001800${nextId}`).trim(),
          activeIngredient: (payload.activeIngredient || '').trim(),
          unit: payload.unit.trim(),
          packagingSpec: (payload.packagingSpec || '').trim(),
          price: Number(payload.price),
          isPrescription: Boolean(payload.isPrescription),
          isSelling: Boolean(payload.isSelling),
          isActive: Boolean(payload.isActive),
          featured: true,
          descriptionHtml: cleanHtml,
          images: payload.images && payload.images.length ? payload.images : []
        };
        db.medicines.unshift(created);
        // Tạo lô khởi tạo mặc định nếu quản trị viên nhập số lượng khởi tạo
        if (Number(payload.initialBatchStock) > 0) {
          const nextBatchId = db.batches.reduce((max, b) => Math.max(max, b.id), 24) + 1;
          db.batches.push({
            id: nextBatchId,
            medicineId: created.id,
            batchNumber: payload.initialBatchNumber || `PH26-NEW-${nextId}`,
            expiryDate: payload.initialBatchExpiry || '2028-06-30',
            stock: Number(payload.initialBatchStock),
            isBlocked: false
          });
        }
        DB.saveDb();
        return {
          status: 201,
          logSummary: `Thêm thuốc mới #${created.id}: ${created.name}`,
          data: DB.enrichMedicine(created)
        };
      }
    });
  }

  function toggleBatchLock(batchId, isBlocked) {
    return simulateNetwork('PATCH', `/api/admin/batches/${batchId}/lock`, () => {
      const db = DB.getDb();
      const batch = db.batches.find(b => b.id === Number(batchId));
      if (!batch) throw { status: 404, message: 'Không tìm thấy lô hàng.' };
      batch.isBlocked = Boolean(isBlocked);
      DB.saveDb();
      return {
        status: 200,
        logSummary: `${batch.isBlocked ? 'Tạm khóa' : 'Mở khóa'} lô ${batch.batchNumber}`,
        data: batch
      };
    });
  }

  function createStockInReceipt(payload) {
    return simulateNetwork('POST', '/api/admin/stock-ins', () => {
      const db = DB.getDb();
      const sup = db.suppliers.find(s => s.id === Number(payload.supplierId));
      if (!sup) throw { status: 400, message: 'Vui lòng chọn nhà cung cấp hợp lệ.' };
      if (!payload.lines || payload.lines.length === 0) {
        throw { status: 400, message: 'Phiếu nhập kho phải có ít nhất 1 dòng thuốc.' };
      }

      let totalCost = 0;
      const savedLines = [];

      payload.lines.forEach(line => {
        const med = db.medicines.find(m => m.id === Number(line.medicineId));
        if (!med) throw { status: 400, message: 'Thuốc trong dòng nhập không hợp lệ.' };
        const qty = Number(line.quantity);
        const cost = Number(line.unitCost);
        if (qty <= 0 || cost <= 0) throw { status: 400, message: 'Số lượng và giá vốn phải lớn hơn 0.' };

        totalCost += qty * cost;
        savedLines.push({
          medicineId: med.id,
          medicineName: med.name,
          unit: med.unit,
          batchNumber: line.batchNumber.trim(),
          expiryDate: line.expiryDate,
          quantity: qty,
          unitCost: cost
        });

        // Cộng tồn vào lô đã có hoặc tạo lô mới (nhập bổ sung vào lô bị khóa không tự mở khóa lô — đúng quy tắc SELLING-STATUS.md)
        const existingBatch = db.batches.find(
          b => b.medicineId === med.id && b.batchNumber.toLowerCase() === line.batchNumber.trim().toLowerCase()
        );
        if (existingBatch) {
          existingBatch.stock += qty;
        } else {
          const nextBatchId = db.batches.reduce((max, b) => Math.max(max, b.id), 24) + 1;
          db.batches.push({
            id: nextBatchId,
            medicineId: med.id,
            batchNumber: line.batchNumber.trim(),
            expiryDate: line.expiryDate,
            stock: qty,
            isBlocked: false
          });
        }
      });

      const nextId = db.stockIns.reduce((max, s) => Math.max(max, s.id), 503) + 1;
      const receipt = {
        id: nextId,
        code: `PN2026-0${nextId - 483}`,
        supplierId: sup.id,
        supplierName: sup.name,
        employeeName: 'Quản lý hệ thống (admin)',
        importDate: new Date().toISOString(),
        totalCost,
        note: (payload.note || '').trim(),
        lines: savedLines
      };

      db.stockIns.unshift(receipt);
      db.stockInDraft = [];
      DB.saveDb();

      return {
        status: 201,
        logSummary: `Lưu phiếu nhập ${receipt.code} (${window.PharmacyI18n.formatCurrency(totalCost)})`,
        data: receipt
      };
    });
  }

  function updateAdminOrderStatus(orderId, payload) {
    return simulateNetwork('PATCH', `/api/admin/orders/${orderId}/status`, () => {
      const db = DB.getDb();
      const order = db.orders.find(o => o.id === Number(orderId));
      if (!order) throw { status: 404, message: 'Không tìm thấy đơn hàng.' };

      // Kiểm tra Optimistic Concurrency (OrderVersion)
      if (payload.expectedVersion !== undefined && Number(payload.expectedVersion) !== order.orderVersion) {
        throw {
          status: 409,
          code: 'ORDER_VERSION_CONFLICT',
          message: `Đơn hàng ${order.code} đã được thay đổi từ một phiên làm việc khác (Phiên bản hiện tại: v${order.orderVersion}, phiên bản form của bạn: v${payload.expectedVersion}). Vui lòng tải lại chi tiết đơn trước khi lưu.`
        };
      }

      if (order.orderStatus === 'CANCELLED' && payload.orderStatus !== 'CANCELLED') {
        throw { status: 400, message: 'Đơn hàng đã hủy không thể chuyển ngược lại trạng thái đang xử lý.' };
      }

      const changes = [];
      if (payload.orderStatus && payload.orderStatus !== order.orderStatus) {
        changes.push(`Trạng thái đơn: ${order.orderStatus} → ${payload.orderStatus}`);
        order.orderStatus = payload.orderStatus;
      }
      if (payload.paymentStatus && payload.paymentStatus !== order.paymentStatus) {
        changes.push(`Thanh toán: ${order.paymentStatus} → ${payload.paymentStatus}`);
        order.paymentStatus = payload.paymentStatus;
      }
      if (payload.shippingStatus && payload.shippingStatus !== order.shippingStatus) {
        changes.push(`Giao hàng: ${order.shippingStatus} → ${payload.shippingStatus}`);
        order.shippingStatus = payload.shippingStatus;
      }
      if (payload.cancelReason) {
        order.cancelReason = payload.cancelReason;
      }

      order.orderVersion += 1;
      order.history.push({
        time: new Date().toISOString(),
        actor: 'Quản trị viên (admin)',
        text: changes.length ? changes.join(' • ') : 'Cập nhật ghi chú xử lý đơn hàng'
      });

      DB.saveDb();
      return {
        status: 200,
        logSummary: `Cập nhật đơn ${order.code} (v${order.orderVersion})`,
        data: order
      };
    });
  }

  function respondPrescriptionRequest(reqId, payload) {
    return simulateNetwork('PATCH', `/api/admin/prescription-requests/${reqId}`, () => {
      const db = DB.getDb();
      const req = db.prescriptionRequests.find(r => r.id === Number(reqId));
      if (!req) throw { status: 404, message: 'Không tìm thấy yêu cầu đơn thuốc.' };
      req.status = payload.status;
      req.pharmacistNote = (payload.pharmacistNote || '').trim();
      req.updatedAt = new Date().toISOString();
      DB.saveDb();
      return {
        status: 200,
        logSummary: `Phản hồi đơn thuốc ${req.code} (${req.status})`,
        data: req
      };
    });
  }

  return {
    getLogs: () => apiLogs,
    sanitizeRichHtml,
    evaluateVoucher,
    getProducts,
    getProductById,
    validateVoucherApi,
    createOrder,
    cancelCustomerOrder,
    submitPrescriptionRequest,
    saveAdminProduct,
    toggleBatchLock,
    createStockInReceipt,
    updateAdminOrderStatus,
    respondPrescriptionRequest,
    simulateNetwork
  };
})();
