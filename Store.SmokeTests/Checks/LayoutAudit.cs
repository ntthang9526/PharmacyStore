using Store.POS;
internal static partial class Program
{
    static void AuditEmbeddedLayouts()
    {
        Directory.CreateDirectory("artifacts/previews/layout-audit");
        var problems=new List<string>();
        using var main=new frmMain();Offscreen(main);
        var types=new[]{typeof(frmQuanLyThuoc),typeof(frmNhanVien),typeof(frmNhapKho),typeof(frmLoHang),typeof(frmPhieuNhap),typeof(frmHoaDon),typeof(frmNhomThuoc),typeof(frmNhaCungCap),typeof(frmKhachHang),typeof(frmVoucher),typeof(frmBaoCao),typeof(frmCanhBao),typeof(frmDoiMatKhau),typeof(frmBanHang)};
        foreach(var size in new[]{new Size(1500,820),new Size(1382,753),new Size(1100,700)})
        {
            main.ClientSize=size;Application.DoEvents();WaitForLoads();
            foreach(var type in types)
            {
                typeof(frmMain).GetMethod("MoForm",PrivateInstance)!.MakeGenericMethod(type).Invoke(main,null);Application.DoEvents();WaitForLoads();
                var form=UiField<Dictionary<Type,Form>>(main,"cuaSoDangMo")[type];
                Console.WriteLine("FORM "+type.Name+" "+size+" viewport "+form.ClientSize);
                void Scan(Control parent)
                {
                    foreach(Control c in parent.Controls)
                    {
                        if(!c.Visible) continue;
                        if(c is ButtonBase or TextBoxBase or DateTimePicker or Label or ComboBox or NumericUpDown or DataGridView)
                        {
                            var rect=c.RectangleToScreen(c.ClientRectangle);var visible=rect;
                            for(Control? a=c.Parent;a!=null;a=a.Parent)
                                visible=Rectangle.Intersect(visible,a.RectangleToScreen(a.ClientRectangle));
                            if(visible.Height<rect.Height-2 || visible.Width<rect.Width-2)
                                problems.Add(type.Name+" "+size+" CLIP "+c.Name+" "+c.GetType().Name+" "+rect.Size+" visible "+visible.Size+" text="+c.Text.Replace("\n"," "));
                            if(c is Label l && !l.AutoSize && !l.AutoEllipsis && l.Text.Length>0)
                            {
                                var needed=TextRenderer.MeasureText(l.Text,l.Font,new Size(Math.Max(1,l.Width-l.Padding.Horizontal),int.MaxValue),TextFormatFlags.WordBreak|TextFormatFlags.TextBoxControl);
                                if(needed.Height>l.Height+2) problems.Add(type.Name+" "+size+" TEXT "+l.Name+" height="+l.Height+" needed="+needed.Height+" text="+l.Text.Replace("\n"," "));
                            }
                        }
                        if(c is not (DataGridView or ComboBox or NumericUpDown)) Scan(c);
                    }
                }
                Scan(form);
                if(form is frmBaoCao)
                {
                    var summary=UiField<TableLayoutPanel>(form,"tableLayoutPanel7");
                    int[] widths=summary.GetColumnWidths();
                    Check(widths.Max()-widths.Min()<=2,"report summary gives each total equal readable space at "+size.Width);
                    foreach(string name in new[]{"lblSoHD","lblTongTien","lblGiamGia","lblDoanhThu"})
                    {
                        var label=UiField<Label>(form,name);
                        Check(TextRenderer.MeasureText(label.Text,label.Font).Width<=label.Width,"report total fits its summary card: "+name+" at "+size.Width);
                    }
                }
                if(form is frmBanHang)
                {
                    var cart=UiField<DataGridView>(form,"tableGioHang");
                    foreach(string name in new[]{"Quantity","Price","Total"})
                    {
                        var col=cart.Columns[name];
                        var visible=cart.GetColumnDisplayRectangle(col.Index,true);
                        Check(visible.Width>=col.Width-2,"cashier can read "+name+" without horizontal scrolling at "+size.Width);
                    }
                }
                using var image=new Bitmap(main.Width,main.Height);main.DrawToBitmap(image,new Rectangle(0,0,image.Width,image.Height));
                image.Save("artifacts/previews/layout-audit/"+type.Name+"-"+size.Width+".png");
            }
        }
        foreach(string problem in problems) Console.WriteLine(problem);
        Check(problems.Count==0,"all 14 embedded forms keep controls and label text visible at default and reduced sizes");
        main.Dispose();
    }
}
