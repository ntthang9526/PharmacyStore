(() => {
    let adding = false;
    const isEnglish = document.documentElement.lang.startsWith('en');

    function notify(message, success) {
        const container = document.querySelector('.toast-container');
        if (!container) return;
        const toast = document.createElement('div');
        toast.className = `toast align-items-center border-0 show text-bg-${success ? 'success' : 'danger'}`;
        toast.setAttribute('role', 'status');
        toast.setAttribute('aria-live', 'polite');
        const row = document.createElement('div');
        row.className = 'd-flex';
        const body = document.createElement('div');
        body.className = 'toast-body';
        body.textContent = message;
        const close = document.createElement('button');
        close.type = 'button';
        close.className = 'btn-close btn-close-white me-2 m-auto';
        close.setAttribute('aria-label', isEnglish ? 'Close' : 'Đóng');
        close.addEventListener('click', () => toast.remove());
        row.append(body, close);
        toast.append(row);
        container.append(toast);
        setTimeout(() => toast.remove(), 4000);
    }

    // Delegation also handles product cards replaced by AJAX filtering.
    document.addEventListener('submit', async event => {
        const form = event.target;
        if (!(form instanceof HTMLFormElement) || !form.matches('[data-add-to-cart]')) return;
        event.preventDefault();
        // Session cart updates must not overlap within this page.
        if (adding) return;
        adding = true;
        const button = form.querySelector('button[type="submit"]');
        if (button) button.disabled = true;
        form.setAttribute('aria-busy', 'true');
        try {
            const response = await fetch(form.action, {
                method: 'POST',
                headers: { 'X-Requested-With': 'XMLHttpRequest' },
                body: new FormData(form)
            });
            if (!response.ok) throw new Error('Add to cart failed');
            const result = await response.json();
            const count = document.getElementById('headerCartCount');
            if (count && Number.isInteger(result.cartCount)) count.textContent = result.cartCount;
            notify(result.message, result.success);
        } catch {
            notify(isEnglish
                ? 'No response. Check your cart before trying again.'
                : 'Chưa nhận được kết quả. Kiểm tra giỏ trước khi thử lại.', false);
        } finally {
            adding = false;
            if (button) button.disabled = false;
            form.removeAttribute('aria-busy');
        }
    });
})();
