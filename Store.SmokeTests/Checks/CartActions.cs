using System.ComponentModel;
using Store.DAL;
using Store.POS;
using Store.POS.FormBanHang;
using Store.Shared.Models;

internal static partial class Program
{
    static void TestCartActions()
    {
        int stockBefore=Stock();
        using var form=new frmBanHang();Offscreen(form);
        var grid=UiField<DataGridView>(form,"tableGioHang");var panel=UiField<Panel>(form,"panelAction");
        var layout=UiField<TableLayoutPanel>(form,"salesLayout");var cartBox=UiField<GroupBox>(form,"groupGioHang");var payment=UiField<GroupBox>(form,"groupThanhToan");
        var numeric=UiField<NumericUpDown>(form,"numericSoLuong");
        Check(panel.Parent==form && layout.ColumnCount==2 && layout.GetColumn(cartBox)==0 && layout.GetColumn(payment)==1,
            "cart keeps full width; action panel is floating without a separate column");
        Check(!panel.Visible,"cart actions start hidden without a target row");
        UiField<TextBox>(form,"boxNhapSanPham").Text=medicineId.ToString();UiField<Button>(form,"btnNhapSanPham").PerformClick();
        var cart=UiField<BindingList<RowData>>(form,"gioHang");
        var other=new MedicineDAL().GetMedicines().Values.First(m=>m.Id!=medicineId && new MedicineDAL().SoLuongConHan(m.Id)>0);
        UiField<TextBox>(form,"boxNhapSanPham").Text=other.Id.ToString();UiField<Button>(form,"btnNhapSanPham").PerformClick();
        Check(cart.Count==2,"action test has two independent cart rows");
        int RowIndex(int id)=>grid.Rows.Cast<DataGridViewRow>().Single(r=>((RowData)r.DataBoundItem).Id==id).Index;
        void ActionCell(int id)=>InvokePrivate(form,"tableGioHang_CellMouseEnter",grid,new DataGridViewCellEventArgs(grid.Columns["btnAction"].Index,RowIndex(id)));
        ActionCell(medicineId);
        Check(panel.Visible && numeric.Value==1 && ReferenceEquals(UiField<RowData>(form,"dongDangThaoTac"),cart[0]),"hover action button binds the correct cart row");
        var hideTimer=UiField<System.Windows.Forms.Timer>(form,"timerAnPanelAction");
        InvokePrivate(form,"AnActionNeuChuotRaNgoai",panel,EventArgs.Empty);
        Check(hideTimer.Enabled,"leaving action panel schedules automatic close");
        typeof(Control).GetMethod("OnMouseEnter",PrivateInstance)!.Invoke(numeric,new object[]{EventArgs.Empty});
        Check(!hideTimer.Enabled && panel.Visible,"entering panel input cancels close while crossing from plus button");
        InvokePrivate(form,"KiemTraAnPanelAction",panel.PointToScreen(new Point(10,10)));
        Check(panel.Visible,"mouse inside action panel keeps it open");
        var plus=grid.GetCellDisplayRectangle(grid.Columns["btnAction"].Index,RowIndex(medicineId),true);
        InvokePrivate(form,"KiemTraAnPanelAction",grid.PointToScreen(new Point(plus.Left+plus.Width/2,plus.Top+plus.Height/2)));
        Check(panel.Visible,"mouse over active plus button keeps panel open");
        InvokePrivate(form,"KiemTraAnPanelAction",form.PointToScreen(new Point(-20,-20)));
        Check(!panel.Visible && typeof(frmBanHang).GetField("dongDangThaoTac",PrivateInstance)!.GetValue(form)==null,"mouse outside plus and panel closes actions and clears target");
        ActionCell(medicineId);
        foreach(var size in new[]{new Size(1182,653),new Size(1120,650),new Size(900,570)})
        {
            form.ClientSize=size;Application.DoEvents();
            Check(!cartBox.Bounds.IntersectsWith(panel.Bounds) && payment.Bounds.IntersectsWith(panel.Bounds) &&
                panel.Left>=payment.Left && panel.Right<=payment.Right && panel.Top>=payment.Top && panel.Bottom<=payment.Bottom &&
                form.Controls.GetChildIndex(panel)<form.Controls.GetChildIndex(layout),
                "action panel overlays payment, stays above it and never covers cart at "+size);
            Check(numeric.Left>=0 && numeric.Right<=panel.ClientSize.Width && UiField<Button>(form,"btnXoa").Bottom<=panel.ClientSize.Height,
                "action inputs stay inside floating panel at "+size);
        }
        numeric.Value=3;
        InvokePrivate(form,"tableGioHang_CellMouseEnter",grid,new DataGridViewCellEventArgs(grid.Columns.Cast<DataGridViewColumn>().Single(c=>c.DataPropertyName=="Name").Index,RowIndex(other.Id)));
        Check(panel.Visible && hideTimer.Enabled && numeric.Value==3 && UiField<RowData>(form,"dongDangThaoTac").Id==medicineId,"moving outside schedules close without retargeting product");
        typeof(Control).GetMethod("OnMouseEnter",PrivateInstance)!.Invoke(numeric,new object[]{EventArgs.Empty});
        grid.ClearSelection();form.LamMoiDuLieu();
        Check(panel.Visible && numeric.Value==3 && UiField<RowData>(form,"dongDangThaoTac").Id==medicineId,"data reload preserves action target and draft quantity");
        grid.ClearSelection();UiField<Button>(form,"btnSua").PerformClick();
        Check(cart[0].Quantity==3 && cart[1].Quantity==1 && !panel.Visible,"quantity edit updates target even after selection clears");
        ActionCell(other.Id);grid.ClearSelection();UiField<Button>(form,"btnXoa").PerformClick();
        Check(cart.Count==1 && cart[0].Id==medicineId && cart[0].No==1 && !panel.Visible,"delete removes exact target and renumbers cart");
        ActionCell(medicineId);numeric.Value=4;
        var enter=new KeyEventArgs(Keys.Enter);InvokePrivate(form,"numericSoLuong_KeyDown",numeric,enter);
        Check(enter.SuppressKeyPress && cart[0].Quantity==4 && !panel.Visible,"Enter applies quantity once and closes actions");
        ActionCell(medicineId);numeric.Value=0;UiField<Button>(form,"btnSua").PerformClick();
        Check(cart.Count==0 && !panel.Visible && !UiField<Button>(form,"btnThanhToan").Enabled,"zero quantity removes row and disables checkout for empty cart");
        UiField<TextBox>(form,"boxNhapSanPham").Text=medicineId.ToString();UiField<Button>(form,"btnNhapSanPham").PerformClick();
        cart[0].Quantity=1001;cart.ResetItem(0);ActionCell(medicineId);
        Check(numeric.Value==1001,"action editor can display quantities above former 1000 limit without throwing");
        cart[0].Quantity=1;cart.ResetItem(0);form.CapNhatGiaTien();
        InvokePrivate(form,"AnThaoTacGioHang");ActionCell(medicineId);
        form.ClientSize=new Size(1182,653);Application.DoEvents();
        Directory.CreateDirectory("artifacts/previews");
        using(var picture=new Bitmap(form.Width,form.Height))
        {
            form.DrawToBitmap(picture,new Rectangle(0,0,picture.Width,picture.Height));panel.DrawToBitmap(picture,panel.Bounds);picture.Save("artifacts/previews/cart-actions-payment-overlay.png");
        }
        cart.Clear();form.CapNhatGiaTien();
        Check(!panel.Visible && typeof(frmBanHang).GetField("dongDangThaoTac",PrivateInstance)!.GetValue(form)==null,"clearing cart resets action target");
        Check(Stock()==stockBefore,"cart quantity/edit/delete actions do not deduct database stock before checkout");
    }
}
