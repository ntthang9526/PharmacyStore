using Store.POS;
internal static partial class Program
{
    static void TestRequestedUiFixes()
    {
        Directory.CreateDirectory("artifacts/ui-fixes-20261029");
        using var main=new frmMain();Offscreen(main);main.ClientSize=new Size(1500,820);
        Form Open(Type type)
        {
            typeof(frmMain).GetMethod("MoForm",PrivateInstance)!.MakeGenericMethod(type).Invoke(main,null);Application.DoEvents();
            return UiField<Dictionary<Type,Form>>(main,"cuaSoDangMo")[type];
        }
        void Capture(string name)
        {
            using var picture=new Bitmap(main.Width,main.Height);main.DrawToBitmap(picture,new Rectangle(Point.Empty,picture.Size));
            picture.Save("artifacts/ui-fixes-20261029/"+name+".png");
        }
        foreach(var type in new[]{typeof(frmNhomThuoc),typeof(frmQuanLyThuoc),typeof(frmNhanVien),typeof(frmNhapKho),typeof(frmLoHang),typeof(frmNhaCungCap),typeof(frmKhachHang),typeof(frmVoucher),typeof(frmBaoCao),typeof(frmCanhBao),typeof(frmBanHang)})
        {
            var form=Open(type);
            foreach(var grid in UiGrids(form))
            {
                Check(grid.AutoSizeColumnsMode==DataGridViewAutoSizeColumnsMode.None && grid.Columns.Cast<DataGridViewColumn>().All(c=>c.AutoSizeMode==DataGridViewAutoSizeColumnMode.None),type.Name+"/"+grid.Name+" keeps column sizing based on Designer widths");
                if(!grid.ColumnHeadersVisible || type==typeof(frmBaoCao) && grid.Name=="dgvHoaDon")continue;
                foreach(DataGridViewColumn column in grid.Columns)
                {
                    if(!column.Visible || string.IsNullOrWhiteSpace(column.HeaderText))continue;
                    var font=column.HeaderCell.InheritedStyle.Font??grid.Font;
                    int required=TextRenderer.MeasureText(column.HeaderText,font).Width+column.HeaderCell.InheritedStyle.Padding.Horizontal+24;
                    Check(column.Width>=required,type.Name+"/"+column.DataPropertyName+" header fits its column");
                }
            }
            if(form is frmBanHang)
            {
                foreach(string field in new[]{"labelTienTamTinh","labelTienGiamGia","labelTienTongTien"})
                    Check(UiField<Label>(form,field).Text=="0 đ","first sales open initializes "+field+" to zero");
                Check(!UiField<Button>(form,"btnThanhToan").Enabled && !UiField<Button>(form,"btnLuuCho").Enabled,"empty cart starts with checkout and pending-save disabled");
            }
            if(type==typeof(frmNhomThuoc) || type==typeof(frmVoucher) || type==typeof(frmNhapKho) || type==typeof(frmQuanLyThuoc) || type==typeof(frmBanHang)) Capture(type.Name+"-after-1500");
        }
        foreach(var size in new[]{new Size(1500,820),new Size(1382,753),new Size(1100,700)})
        {
            main.ClientSize=size;var groups=Open(typeof(frmNhomThuoc));
            foreach(string field in new[]{"btnThem","btnSua","btnXoa","btnLamMoi"})
            {
                var button=UiField<Button>(groups,field);var text=TextRenderer.MeasureText(button.Text,button.Font);
                Check(button.Visible && button.Height>=text.Height+button.Padding.Vertical+4,"group action text fits "+field+" at "+size.Width);
            }
            Capture("groups-after-"+size.Width);
        }
        main.Dispose();
    }
}
