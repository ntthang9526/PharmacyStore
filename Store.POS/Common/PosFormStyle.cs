using System.ComponentModel;
namespace Store.POS;

internal static class PosFormStyle
{
    internal static bool IsInDesigner(Control control) =>
        LicenseManager.UsageMode == LicenseUsageMode.Designtime || control.Site?.DesignMode == true;

    internal static void ApplyModernButton(Button btn)
    {
        if (btn == null) return;
        btn.FlatStyle = FlatStyle.Flat;
        btn.Cursor = Cursors.Hand;
        btn.UseVisualStyleBackColor = false;

        btn.Paint -= Button_Paint;
        btn.Paint += Button_Paint;
        btn.EnabledChanged -= Button_EnabledChanged;
        btn.EnabledChanged += Button_EnabledChanged;

        if (btn.Font != null && btn.Font.FontFamily.Name.Equals("Segoe UI Semibold", StringComparison.OrdinalIgnoreCase) && btn.Font.Bold)
        {
            try
            {
                btn.Font = new Font(btn.Font.FontFamily, btn.Font.Size, FontStyle.Regular, btn.Font.Unit);
            }
            catch { }
        }

        Color c = btn.BackColor;
        if (c == Color.FromArgb(22, 101, 52))
        {
            btn.ForeColor = Color.White;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(21, 128, 61);
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(18, 83, 43);
        }
        else if (c == Color.FromArgb(15, 118, 110))
        {
            btn.ForeColor = Color.White;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(13, 148, 136);
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(17, 94, 89);
        }
        else if (c == Color.FromArgb(225, 29, 72))
        {
            btn.ForeColor = Color.White;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(244, 63, 94);
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(190, 18, 60);
        }
        else if (c == Color.FromArgb(100, 116, 139))
        {
            btn.ForeColor = Color.White;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(148, 163, 184);
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(71, 85, 105);
        }
        else if (c == Color.FromArgb(30, 41, 59))
        {
            btn.ForeColor = Color.FromArgb(241, 245, 249);
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(51, 65, 85);
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 23, 42);
        }
        else if (c == Color.FromArgb(204, 251, 241))
        {
            btn.ForeColor = Color.FromArgb(19, 78, 74);
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(153, 246, 228);
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(94, 234, 212);
        }
        else if (c == Color.FromArgb(192, 255, 192))
        {
            btn.ForeColor = Color.FromArgb(20, 83, 45);
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(167, 243, 167);
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(134, 220, 134);
        }
        else if (c == Color.FromArgb(255, 192, 192))
        {
            btn.ForeColor = Color.FromArgb(136, 19, 55);
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(254, 168, 168);
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(248, 140, 140);
        }
        else
        {
            float brightness = c.GetBrightness();
            if (brightness > 0.5f)
            {
                btn.ForeColor = Color.FromArgb(30, 41, 59);
                btn.FlatAppearance.BorderSize = 1;
                btn.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(
                    Math.Max(0, (int)(c.R * 0.94f)),
                    Math.Max(0, (int)(c.G * 0.94f)),
                    Math.Max(0, (int)(c.B * 0.94f)));
                btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(
                    Math.Max(0, (int)(c.R * 0.86f)),
                    Math.Max(0, (int)(c.G * 0.86f)),
                    Math.Max(0, (int)(c.B * 0.86f)));
            }
            else
            {
                btn.ForeColor = Color.White;
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(
                    Math.Min(255, c.R + (int)((255 - c.R) * 0.15f)),
                    Math.Min(255, c.G + (int)((255 - c.G) * 0.15f)),
                    Math.Min(255, c.B + (int)((255 - c.B) * 0.15f)));
                btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(
                    Math.Max(0, (int)(c.R * 0.82f)),
                    Math.Max(0, (int)(c.G * 0.82f)),
                    Math.Max(0, (int)(c.B * 0.82f)));
            }
        }
    }

    internal static void ApplyTableStyle(DataGridView grid)
    {
        if (grid == null) return;
        // Designer does not persist AutoGenerateColumns and uses component names for columns.
        grid.AutoGenerateColumns = false;
        foreach (DataGridViewColumn column in grid.Columns)
            if (!string.IsNullOrEmpty(column.DataPropertyName)) column.Name = column.DataPropertyName;
        grid.ShowCellToolTips = true;
        grid.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        ConfigureConditionalFill(grid);
    }

    private static readonly System.Runtime.CompilerServices.ConditionalWeakTable<DataGridView, object> configuredGrids = new();

    private static void ConfigureConditionalFill(DataGridView grid)
    {
        if (configuredGrids.TryGetValue(grid, out _)) return;
        configuredGrids.Add(grid, new object());
        var widths = grid.Columns.Cast<DataGridViewColumn>().ToDictionary(c => c, c => c.Width);
        var flexible = grid.Columns.Cast<DataGridViewColumn>()
            .FirstOrDefault(c => c.AutoSizeMode == DataGridViewAutoSizeColumnMode.Fill);
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        foreach (var column in widths.Keys) column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        bool adjusting = false;
        void ResizeColumns()
        {
            if (adjusting || grid.IsDisposed) return;
            adjusting = true;
            try
            {
                var visible = widths.Keys.Where(c => c.DataGridView == grid && c.Visible).ToArray();
                if (visible.Length == 0) return;
                foreach (var column in visible) column.Width = widths[column];
                int available = grid.ClientSize.Width - (grid.RowHeadersVisible ? grid.RowHeadersWidth : 0)
                    - (grid.BorderStyle == BorderStyle.None ? 0 : 2)
                    - grid.Controls.OfType<VScrollBar>().Where(b => b.Visible).Sum(b => b.Width);
                int extra = available - visible.Sum(c => c.Width);
                if (extra <= 0) return;
                var stretch = flexible != null && flexible.DataGridView == grid && flexible.Visible ? flexible
                    : visible.Where(c => c.DataPropertyName.StartsWith("Ten") || c.DataPropertyName.Contains("Name") || c.DataPropertyName == "HoTen")
                        .OrderByDescending(c => widths[c]).FirstOrDefault()
                      ?? visible.OrderByDescending(c => widths[c]).First();
                stretch.Width += extra;
            }
            finally { adjusting = false; }
        }
        grid.ColumnWidthChanged += (_, e) =>
        {
            if (adjusting || !widths.ContainsKey(e.Column)) return;
            widths[e.Column] = e.Column.Width;
            ResizeColumns();
        };
        grid.ColumnAdded += (_, e) => { widths[e.Column] = e.Column.Width; ResizeColumns(); };
        grid.ColumnRemoved += (_, e) => { widths.Remove(e.Column); ResizeColumns(); };
        grid.ClientSizeChanged += (_, _) => ResizeColumns();
        grid.DataBindingComplete += (_, _) => ResizeColumns();
        grid.ColumnStateChanged += (_, e) => { if (e.StateChanged == DataGridViewElementStates.Visible) ResizeColumns(); };
        ResizeColumns();
    }

    internal static void ConfigureGridSelection(Form form, Control root, Action? resetDetails, bool clearSelectionOnLeave = true)
    {
        if (IsInDesigner(form)) return;
        


        var grids = FindGrids(form).Distinct().ToArray();
        foreach (var grid in grids)
            ApplyTableStyle(grid);

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
            control.MouseDown += (_, e) =>
            {
                Point screenPt = control.PointToScreen(e.Location);
                bool interactive = false;

                for (Control? current = control; current != null; current = current.Parent)
                {
                    if (current is ButtonBase or LinkLabel or TextBoxBase or ComboBox or NumericUpDown or DateTimePicker or ScrollBar)
                    {
                        interactive = true;
                        break;
                    }
                }

                if (!interactive)
                {
                    Control? hit = FindDeepestControlAtPoint(form, screenPt);
                    for (Control? c = hit; c != null && c != form; c = c.Parent)
                    {
                        if (c is ButtonBase or LinkLabel or TextBoxBase or ComboBox or NumericUpDown or DateTimePicker or ScrollBar)
                        {
                            interactive = true;
                            break;
                        }
                    }
                }

                if (!interactive)
                {
                    ClearAll();
                    resetDetails?.Invoke();
                }
            };
            foreach (Control child in control.Controls) WireOutside(child);
        }
        WireOutside(form);
        foreach (var grid in grids)
        {
            if (clearSelectionOnLeave) grid.Leave += (_, _) => Clear(grid);
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

    internal static void ConfigureRowNavigation(DataGridView grid, Action<DataGridViewRow> showRow)
    {
        bool navigationPending=false;
        void ShowCurrent()
        {
            if(grid.IsDisposed || grid.Disposing || !grid.Enabled || !grid.Visible) return;
            if(grid.CurrentRow is {IsNewRow:false,Selected:true} row && row.DataBoundItem!=null) showRow(row);
        }
        // Leave also covers pressing a button while a navigation key is still held.
        grid.KeyDown+=(_,e)=>navigationPending|=e.KeyCode is Keys.Up or Keys.Down or Keys.Left or Keys.Right or Keys.Home or Keys.End or Keys.PageUp or Keys.PageDown or Keys.Enter or Keys.Tab;
        grid.KeyUp+=(_,_)=> {ShowCurrent();navigationPending=false;};
        grid.Sorted+=(_,_)=>ShowCurrent();
        grid.Leave+=(_,_)=> {if(navigationPending) ShowCurrent();navigationPending=false;};
    }

    internal static void RebindCombo(ComboBox combo, System.Data.DataTable data, int? id)
    {
        combo.DataSource=data;combo.SelectedIndex=-1;
        if(id.HasValue)
        {
            combo.SelectedValue=id.Value;
            if(combo.SelectedValue is not int selected || selected!=id.Value) combo.SelectedIndex=-1;
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

    private static IEnumerable<Button> FindButtons(Control root)
    {
        foreach (Control child in root.Controls)
        {
            if (child is Button btn) yield return btn;
            else foreach (var nested in FindButtons(child)) yield return nested;
        }
    }

    internal static void ConfigureDisabledButtons(Form form)
    {
        if (IsInDesigner(form)) return;
        foreach (var button in FindButtons(form))
        {
            button.Paint -= Button_Paint;
            button.Paint += Button_Paint;
            button.EnabledChanged -= Button_EnabledChanged;
            button.EnabledChanged += Button_EnabledChanged;
        }
    }

    internal static Color DisabledButtonTextColor(Color background)
    {
        static double Channel(byte value)
        {
            double channel = value / 255d;
            return channel <= 0.04045 ? channel / 12.92 : Math.Pow((channel + 0.055) / 1.055, 2.4);
        }
        static double Luminance(Color color) => 0.2126 * Channel(color.R) + 0.7152 * Channel(color.G) + 0.0722 * Channel(color.B);
        double luminance = Luminance(background);
        var slate = Color.FromArgb(71, 85, 105);
        if ((luminance + 0.05) / (Luminance(slate) + 0.05) >= 4.5) return slate;
        if (1.05 / (luminance + 0.05) >= 4.5) return Color.White;
        return Color.Black;
    }

    private static void Button_EnabledChanged(object? sender, EventArgs e)
    {
        if (sender is Button btn)
            btn.Invalidate();
    }

    private static void Button_Paint(object? sender, PaintEventArgs e)
    {
        if (sender is not Button btn || btn.Enabled || btn.FindForm() is Form form && IsInDesigner(form)) return;

        Color back = btn.BackColor;
        using var brush = new SolidBrush(back);
        e.Graphics.FillRectangle(brush, btn.ClientRectangle);

        if (btn.FlatAppearance.BorderSize > 0)
        {
            using var pen = new Pen(btn.FlatAppearance.BorderColor, btn.FlatAppearance.BorderSize);
            e.Graphics.DrawRectangle(pen, 0, 0, btn.Width - 1, btn.Height - 1);
        }

        // WinForms ignores ForeColor for disabled buttons; paint readable text without changing Designer colors.
        Color disabledText = DisabledButtonTextColor(back);

        TextFormatFlags flags = TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak;
        switch (btn.TextAlign)
        {
            case ContentAlignment.MiddleLeft:
            case ContentAlignment.TopLeft:
            case ContentAlignment.BottomLeft:
                flags |= TextFormatFlags.Left;
                break;
            case ContentAlignment.MiddleRight:
            case ContentAlignment.TopRight:
            case ContentAlignment.BottomRight:
                flags |= TextFormatFlags.Right;
                break;
            default:
                flags |= TextFormatFlags.HorizontalCenter;
                break;
        }

        Rectangle textRect = btn.ClientRectangle;
        if (btn.Padding != Padding.Empty)
        {
            textRect = new Rectangle(
                btn.Padding.Left,
                btn.Padding.Top,
                Math.Max(1, btn.Width - btn.Padding.Horizontal),
                Math.Max(1, btn.Height - btn.Padding.Vertical));
        }

        TextRenderer.DrawText(e.Graphics, btn.Text, btn.Font, textRect, disabledText, flags);
    }

    private static Control? FindDeepestControlAtPoint(Control parent, Point screenPt)
    {
        for (int i = parent.Controls.Count - 1; i >= 0; i--)
        {
            Control child = parent.Controls[i];
            if (!child.Visible) continue;
            Rectangle rect = child.RectangleToScreen(child.ClientRectangle);
            if (rect.Contains(screenPt))
            {
                Control? deeper = FindDeepestControlAtPoint(child, screenPt);
                return deeper ?? child;
            }
        }
        return null;
    }
}
