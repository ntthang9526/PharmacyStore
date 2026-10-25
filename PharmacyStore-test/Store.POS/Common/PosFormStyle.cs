using System.ComponentModel;

namespace Store.POS;

internal static class PosFormStyle
{
    // Bảng màu hiện đại chuẩn Y tế & Dược phẩm (Modern Medical/Pharmacy Theme)
    public static readonly Color Primary = Color.FromArgb(15, 118, 110);         // Teal 700 - Xanh ngọc dược phẩm sang trọng
    public static readonly Color PrimaryHover = Color.FromArgb(13, 148, 136);    // Teal 600
    public static readonly Color Success = Color.FromArgb(22, 101, 52);          // Emerald 800 - Xanh lá an tâm
    public static readonly Color SuccessHover = Color.FromArgb(21, 128, 61);     // Emerald 700
    public static readonly Color Danger = Color.FromArgb(225, 29, 72);           // Rose 600 - Đỏ hiện đại
    public static readonly Color DangerHover = Color.FromArgb(190, 18, 60);      // Rose 700
    public static readonly Color Warning = Color.FromArgb(217, 119, 6);          // Amber 600 - Cam cảnh báo
    public static readonly Color WarningBg = Color.FromArgb(254, 243, 199);      // Amber 100
    public static readonly Color Info = Color.FromArgb(2, 132, 199);             // Sky 600
    public static readonly Color InfoBg = Color.FromArgb(224, 242, 254);         // Sky 100
    public static readonly Color DarkBg = Color.FromArgb(15, 23, 42);            // Slate 900 - Nền Sidebar cao cấp
    public static readonly Color DarkCard = Color.FromArgb(30, 41, 59);          // Slate 800
    public static readonly Color SurfaceBg = Color.FromArgb(248, 250, 252);      // Slate 50 - Nền giao diện dịu mắt
    public static readonly Color CardBg = Color.White;
    public static readonly Color BorderColor = Color.FromArgb(226, 232, 240);    // Slate 200 - Viền mảnh tinh tế
    public static readonly Color TextPrimary = Color.FromArgb(15, 23, 42);       // Slate 900 - Đen chữ sắc nét
    public static readonly Color TextMuted = Color.FromArgb(100, 116, 139);      // Slate 500
    public static readonly Color SelectionRowBg = Color.FromArgb(204, 251, 241); // Teal 100 - Hàng đang chọn dịu mắt

    internal static bool IsInDesigner(Control control) =>
        LicenseManager.UsageMode == LicenseUsageMode.Designtime || control.Site?.DesignMode == true;

    /// <summary>
    /// Áp dụng toàn bộ Theme hiện đại cho Form và các điều khiển con
    /// </summary>
    internal static void ApplyTheme(Form form)
    {
        if (IsInDesigner(form)) return;

        form.BackColor = SurfaceBg;
        form.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

        StyleAllControls(form);
    }

    private static void StyleAllControls(Control parent)
    {
        foreach (Control c in parent.Controls)
        {
            if (c is DataGridView grid)
            {
                StyleGrid(grid);
            }
            else if (c is Button btn)
            {
                StyleButton(btn);
            }
            else if (c is GroupBox gb)
            {
                gb.ForeColor = TextPrimary;
                gb.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
            }
            else if (c is TextBox tb)
            {
                if (tb.BorderStyle == BorderStyle.FixedSingle)
                    tb.BackColor = Color.White;
            }

            if (c.HasChildren)
            {
                StyleAllControls(c);
            }
        }
    }

    /// <summary>
    /// Làm đẹp DataGridView chuẩn bảng dữ liệu hiện đại
    /// </summary>
    internal static void StyleGrid(DataGridView grid)
    {
        grid.BackgroundColor = Color.White;
        grid.BorderStyle = BorderStyle.None;
        grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        grid.GridColor = BorderColor;
        grid.EnableHeadersVisualStyles = false;
        grid.RowHeadersVisible = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.MultiSelect = false;

        // Header
        grid.ColumnHeadersHeight = 38;
        grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        grid.ColumnHeadersDefaultCellStyle.BackColor = Primary;
        grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
        grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        grid.ColumnHeadersDefaultCellStyle.Padding = new Padding(6, 0, 6, 0);

        // Hàng dữ liệu
        grid.RowTemplate.Height = 34;
        grid.DefaultCellStyle.BackColor = Color.White;
        grid.DefaultCellStyle.ForeColor = TextPrimary;
        grid.DefaultCellStyle.Font = new Font("Segoe UI", 9.8F, FontStyle.Regular);
        grid.DefaultCellStyle.SelectionBackColor = SelectionRowBg;
        grid.DefaultCellStyle.SelectionForeColor = TextPrimary;
        grid.DefaultCellStyle.Padding = new Padding(6, 0, 6, 0);

        // Hàng xen kẽ
        grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
        grid.AlternatingRowsDefaultCellStyle.ForeColor = TextPrimary;
        grid.AlternatingRowsDefaultCellStyle.SelectionBackColor = SelectionRowBg;
        grid.AlternatingRowsDefaultCellStyle.SelectionForeColor = TextPrimary;
    }

    /// <summary>
    /// Tự động làm đẹp nút bấm theo vai trò ngữ cảnh
    /// </summary>
    internal static void StyleButton(Button btn)
    {
        btn.FlatStyle = FlatStyle.Flat;
        btn.Cursor = Cursors.Hand;
        btn.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);

        string txt = btn.Text.ToLowerInvariant();
        string name = btn.Name.ToLowerInvariant();

        // 1. Nút Nguy hiểm (Xóa, Hủy)
        if (txt.Contains("xóa") || txt.Contains("hủy") || name.Contains("xoa") || name.Contains("huy"))
        {
            btn.BackColor = Danger;
            btn.ForeColor = Color.White;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = DangerHover;
        }
        // 2. Nút Thành công / Lưu / Thanh toán / Bán hàng / Excel
        else if (txt.Contains("thanh toán") || txt.Contains("lưu") || txt.Contains("bán") || txt.Contains("excel") ||
                 name.Contains("thanhtoan") || name.Contains("luu") || name.Contains("excel"))
        {
            btn.BackColor = Success;
            btn.ForeColor = Color.White;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = SuccessHover;
        }
        // 3. Nút Thêm mới / Xem / Tìm kiếm
        else if (txt.Contains("thêm") || txt.Contains("xem") || txt.Contains("tìm") || txt == "+" ||
                 name.Contains("them") || name.Contains("xem") || name.Contains("tim"))
        {
            btn.BackColor = Primary;
            btn.ForeColor = Color.White;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = PrimaryHover;
        }
        // 4. Nút Phụ (Làm mới, Bỏ qua, Tạm ẩn, Thoát)
        else
        {
            btn.BackColor = Color.White;
            btn.ForeColor = TextPrimary;
            btn.FlatAppearance.BorderColor = BorderColor;
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(241, 245, 249);
        }
    }

    internal static void ConfigureGridSelection(Form form, Control root, Action? resetDetails)
    {
        if (IsInDesigner(form)) return;
        ApplyTheme(form); // Áp dụng theme tự động khi cấu hình form

        var grids = FindGrids(root).ToArray();
        void Clear(DataGridView grid)
        {
            if (grid.IsDisposed || grid.Disposing) return;
            grid.ClearSelection();
            grid.CurrentCell = null;
        }
        void ClearAll() { foreach (var grid in grids) Clear(grid); }
        void WireOutside(Control control)
        {
            if (control is DataGridView) return;
            control.MouseDown += (_, _) =>
            {
                ClearAll();
                bool interactive = false;
                for (Control? current = control; current != null; current = current.Parent)
                    if (current is ButtonBase or TextBoxBase or ComboBox or NumericUpDown or DateTimePicker or ScrollBar)
                        { interactive = true; break; }
                if (!interactive) resetDetails?.Invoke();
            };
            foreach (Control child in control.Controls) WireOutside(child);
        }
        WireOutside(form);
        foreach (var grid in grids)
        {
            grid.Leave += (_, _) => Clear(grid);
            grid.MouseDown += (_, e) =>
            {
                if (grid.HitTest(e.X, e.Y).Type == DataGridViewHitTestType.None) { Clear(grid); resetDetails?.Invoke(); }
            };
            grid.DataBindingComplete += (_, _) =>
            {
                Clear(grid);
                if (grid.IsHandleCreated)
                    grid.BeginInvoke((Action)(() => Clear(grid)));
            };
        }
        form.Shown += (_, _) => form.BeginInvoke((Action)ClearAll);
        form.Deactivate += (_, _) => ClearAll();
    }

    internal static void RebindCombo(ComboBox combo, System.Data.DataTable data, int? id)
    {
        combo.DataSource = data;
        combo.SelectedIndex = -1;
        if (id.HasValue)
        {
            combo.SelectedValue = id.Value;
            if (combo.SelectedValue is not int selected || selected != id.Value) combo.SelectedIndex = -1;
        }
    }

    private static IEnumerable<DataGridView> FindGrids(Control root)
    {
        foreach (Control child in root.Controls)
        {
            if (child is DataGridView grid) yield return grid;
            else foreach (var nested in FindGrids(child)) yield return nested;
        }
    }
}
