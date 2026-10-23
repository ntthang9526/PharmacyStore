using System.ComponentModel;
namespace Store.POS;

internal static class PosFormStyle
{
    internal static bool IsInDesigner(Control control) =>
        LicenseManager.UsageMode == LicenseUsageMode.Designtime || control.Site?.DesignMode == true;

    internal static void ConfigureGridSelection(Form form, Control root, Action? resetDetails)
    {
        if (IsInDesigner(form)) return;
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

}
