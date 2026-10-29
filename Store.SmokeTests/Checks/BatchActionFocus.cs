using System.Data;
using Store.POS;
internal static partial class Program
{
    static void TestBatchActionFocus()
    {
        using(var form=new frmLoHang())
        {
            Offscreen(form);Application.DoEvents();
            var grid=UiField<DataGridView>(form,"dgvLoHang");var button=UiField<Button>(form,"btnDoiTrangThai");
            Check(grid.Rows.Count>0 && grid.SelectedRows.Count==0 && !button.Enabled,"batch form initially has no target and lock action is disabled");
            grid.Focus();grid.CurrentCell=grid.Rows[0].Cells[0];grid.Rows[0].Selected=true;Application.DoEvents();
            int id=(int)grid.SelectedRows[0].Cells["MaLo"].Value;
            Check(button.Enabled,"selecting a batch enables its action");
            button.Focus();Application.DoEvents();
            Check(button.Focused && button.Enabled,"moving focus to the action does not disable it before Click");
            Check(grid.SelectedRows.Count==1 && (int)grid.SelectedRows[0].Cells["MaLo"].Value==id,"focused action retains the selected batch ID");
            var root=UiField<TableLayoutPanel>(form,"posLayout");
            typeof(Control).GetMethod("OnMouseDown",PrivateInstance)!.Invoke(root,new object[]{new MouseEventArgs(MouseButtons.Left,1,root.Width-2,root.Height-2,0)});
            Check(grid.SelectedRows.Count==0 && grid.CurrentCell==null && !button.Enabled,"clicking blank background still clears batch selection and disables action");
            grid.Focus();grid.CurrentCell=grid.Rows[0].Cells[0];grid.Rows[0].Selected=true;
            typeof(Form).GetMethod("OnDeactivate",PrivateInstance)!.Invoke(form,new object[]{EventArgs.Empty});
            Check(grid.SelectedRows.Count==0 && !button.Enabled,"leaving the form clears its batch selection");
        }
        // Exercise the complete focus -> Click path without opening a native confirmation dialog.
        using var host=new Form {ClientSize=new Size(450,260)};
        var table=new DataGridView {Name="grid",Location=new Point(10,10),Size=new Size(400,150),SelectionMode=DataGridViewSelectionMode.FullRowSelect,MultiSelect=false,AllowUserToAddRows=false};
        table.Columns.Add("Id","Mã lô");table.Rows.Add(new object[]{42});
        var action=new Button {Location=new Point(10,180),Size=new Size(160,38),Text="Tạm khóa",Enabled=false};host.Controls.Add(table);host.Controls.Add(action);
        int clicks=0,target=0;
        table.SelectionChanged+=(_,_)=>action.Enabled=table.SelectedRows.Count==1;
        action.Click+=(_,_)=> {clicks++;target=(int)table.SelectedRows[0].Cells[0].Value;};
        var configure=typeof(frmMain).Assembly.GetType("Store.POS.PosFormStyle")!.GetMethod("ConfigureGridSelection",PrivateStatic)!;
        configure.Invoke(null,new object?[]{host,host,null,false});Offscreen(host);Application.DoEvents();
        for(int repeat=0;repeat<2;repeat++)
        {
            table.Focus();table.CurrentCell=table.Rows[0].Cells[0];table.Rows[0].Selected=true;
            action.Focus();Application.DoEvents();action.PerformClick();
            Check(clicks==repeat+1 && target==42,"action Click is delivered once to the selected batch after focus change "+repeat);
            action.Text="Mở khóa";
        }
    }
}
