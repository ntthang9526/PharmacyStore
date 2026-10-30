using Store.POS;
using System.Reflection;
internal static partial class Program
{
    static void TestDisabledButtons()
    {
        var style=typeof(frmMain).Assembly.GetType("Store.POS.PosFormStyle")!;
        var configure=style.GetMethod("ConfigureDisabledButtons",PrivateStatic)!;
        var colorMethod=style.GetMethod("DisabledButtonTextColor",PrivateStatic)!;
        static double Lum(Color c)
        {
            double Channel(byte v){double x=v/255d;return x<=0.04045?x/12.92:Math.Pow((x+0.055)/1.055,2.4);}
            return .2126*Channel(c.R)+.7152*Channel(c.G)+.0722*Channel(c.B);
        }
        Directory.CreateDirectory("artifacts/disabled-buttons-20261029");
        using var host=new Form {ClientSize=new Size(460,510),BackColor=Color.FromArgb(248,250,252)};
        var palette=new[]{Color.FromArgb(22,101,52),Color.FromArgb(15,118,110),Color.FromArgb(225,29,72),Color.FromArgb(100,116,139),Color.FromArgb(30,41,59),Color.FromArgb(204,251,241),Color.White,Color.Yellow};
        for(int i=0;i<palette.Length;i++)
        {
            Color back=palette[i],fore=Color.FromArgb(240,240,240);
            var button=new Button {Name="button"+i,Text="Chưa thể thanh toán",Location=new Point(20,10+i*60),Size=new Size(410,46),FlatStyle=FlatStyle.Flat,UseVisualStyleBackColor=false,BackColor=back,ForeColor=fore,Enabled=false,Font=new Font("Segoe UI",11)};
            host.Controls.Add(button);
        }
        configure.Invoke(null,new object[]{host});configure.Invoke(null,new object[]{host});Offscreen(host);
        foreach(Button button in host.Controls)
        {
            Color expected=(Color)colorMethod.Invoke(null,new object[]{button.BackColor})!;
            double ratio=(Math.Max(Lum(expected),Lum(button.BackColor))+.05)/(Math.Min(Lum(expected),Lum(button.BackColor))+.05);
            Check(ratio>=4.5,"disabled text is readable on "+button.BackColor+" contrast="+ratio.ToString("F2"));
            using var image=new Bitmap(button.Width,button.Height);button.DrawToBitmap(image,button.ClientRectangle);
            int textPixels=0;
            for(int y=8;y<image.Height-8;y++)for(int x=12;x<image.Width-12;x++)if(image.GetPixel(x,y).ToArgb()==expected.ToArgb())textPixels++;
            Check(textPixels>20,"disabled button actually paints contrasting text: "+button.Name);
            bool clicked=false;button.Click+=(_,_)=>clicked=true;button.PerformClick();
            Check(!clicked && !button.Enabled,"disabled button remains non-interactive: "+button.Name);
            var back=button.BackColor;var fore=button.ForeColor;var bounds=button.Bounds;
            button.Enabled=true;Application.DoEvents();button.Enabled=false;Application.DoEvents();
            Check(button.BackColor==back && button.ForeColor==fore && button.Bounds==bounds,"state changes preserve Designer colors and bounds: "+button.Name);
        }
        using var preview=new Bitmap(host.Width,host.Height);host.DrawToBitmap(preview,new Rectangle(Point.Empty,preview.Size));preview.Save("artifacts/disabled-buttons-20261029/palette.png");
        foreach(var type in new[]{typeof(frmBanHang),typeof(frmQuanLyThuoc),typeof(frmNhapKho),typeof(frmHoaDon)})
        {
            using var form=(Form)Activator.CreateInstance(type)!;form.Site=new FormDesignSite(form);Offscreen(form);form.Site=null;
            IEnumerable<Control> Walk(Control root){foreach(Control child in root.Controls){yield return child;foreach(var nested in Walk(child))yield return nested;}}
            if(form is frmHoaDon) UiField<Button>(form,"btnXuatChiTietExcel").Enabled=false;
            var disabled=Walk(form).OfType<Button>().Where(b=>b.Visible&&!b.Enabled).ToArray();
            Check(disabled.Length>0,type.Name+" has disabled buttons to inspect");
            foreach(var button in disabled)
            {
                using var image=new Bitmap(button.Width,button.Height);button.DrawToBitmap(image,button.ClientRectangle);
                var color=(Color)colorMethod.Invoke(null,new object[]{button.BackColor})!;
                int pixels=0;for(int y=5;y<image.Height-5;y++)for(int x=5;x<image.Width-5;x++)if(image.GetPixel(x,y).ToArgb()==color.ToArgb())pixels++;
                Check(pixels>0,type.Name+"/"+button.Name+" uses the disabled-text renderer");
            }
        }
    }
}
