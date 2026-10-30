using System.Data;
using Store.BLL;
using Store.POS;
using Store.POS.FormBanHang;

internal static partial class Program
{
    static void TestGridReceiptReport()
    {
        var reports=new BaoCaoBLL();
        var date=new DateTime(1991,1,2);
        var ids=new List<int>();
        int admin=Count("SELECT Id FROM Employees WHERE Username='admin'");
        int Add(DateTime created,DateTime? paid,string status,decimal total=1000m,decimal discount=100m)
        {
            int id=Count(@"INSERT INTO Orders(EmployeeId,OrderDate,TotalAmount,DiscountAmount,Channel,PaymentStatus,PaymentMethod,PaidAt,PaidEmployeeId)
                VALUES(@e,@created,@total,@discount,'POS',@status,N'Tiền mặt',@paid,@e);SELECT CAST(SCOPE_IDENTITY() AS int)",
                ("@e",admin),("@created",created),("@total",total),("@discount",discount),("@status",status),("@paid",(object?)paid??DBNull.Value));
            ids.Add(id);return id;
        }
        try
        {
            int midnight=Add(date.AddDays(-3),date,"PAID");
            int last=Add(date,date.AddDays(1).AddMilliseconds(-10),"PAID",5000m,250m);
            int legacy=Add(date.AddHours(12),null,"PAID");
            int pending=Add(date,null,"PENDING");
            int next=Add(date,date.AddDays(1),"PAID");
            int previous=Add(date,date.AddMilliseconds(-10),"PAID");
            Check(reports.LayBaoCao(date,date,out var daily,out var bills,out int count,out decimal total,out decimal discount,out decimal revenue,out string error),"revenue boundary query succeeds: "+error);
            var own=bills.AsEnumerable().Where(r=>ids.Contains(r.Field<int>("MaHD"))).ToArray();
            Check(own.Select(r=>r.Field<int>("MaHD")).OrderBy(x=>x).SequenceEqual(new[]{midnight,last,legacy}.OrderBy(x=>x)),"report includes entire payment day and legacy date; excludes pending and adjacent days");
            Check(own.Sum(r=>r.Field<decimal>("TongTien"))==7000m && own.Sum(r=>r.Field<decimal>("GiamGia"))==450m && own.Sum(r=>r.Field<decimal>("ThucThu"))==6550m,"report subtracts discounts once using decimal amounts");
            Check(count==bills.Rows.Count && total==bills.AsEnumerable().Sum(r=>r.Field<decimal>("TongTien")) && discount==bills.AsEnumerable().Sum(r=>r.Field<decimal>("GiamGia")) && revenue==bills.AsEnumerable().Sum(r=>r.Field<decimal>("ThucThu")),"summary exactly matches invoice rows");
            Check(daily.AsEnumerable().Sum(r=>r.Field<int>("SoHoaDon"))==count && daily.AsEnumerable().Sum(r=>r.Field<decimal>("DoanhThu"))==revenue,"daily report exactly matches summary");
            Check(own.All(r=>r.Field<string>("KhachHang")=="Khách lẻ"),"report export has a customer label for walk-in invoices");
            Check(!reports.LayBaoCao(date.AddDays(1),date,out _,out _,out _,out _,out _,out _,out _),"reversed date range rejected");
            Check(!reports.LayBaoCao(date,DateTime.MaxValue,out _,out _,out _,out _,out _,out _,out _),"upper date boundary cannot overflow SQL date calculation");
            using var reportForm=new frmBaoCao();Offscreen(reportForm);
            UiField<DateTimePicker>(reportForm,"dtpTuNgay").Value=date;
            UiField<DateTimePicker>(reportForm,"dtpDenNgay").Value=date;
            var refresh=typeof(frmBaoCao).GetMethod("XemBaoCao",PrivateInstance)!;
            Check((bool)refresh.Invoke(reportForm,new object[]{false})!,"report refresh supports the selected export dates");
            Check(((DataTable)UiField<DataGridView>(reportForm,"dgvHoaDon").DataSource!).Rows.Count==count,"visible invoice list uses refreshed report result");
            Check(UiField<Label>(reportForm,"lblDoanhThu").Text==revenue.ToString("N0",new System.Globalization.CultureInfo("vi-VN"))+" đ","visible revenue matches refreshed invoice result");
        }
        finally {foreach(int id in ids) Scalar("DELETE FROM Orders WHERE Id=@id",("@id",id));}

        using var main=new frmMain();Offscreen(main);
        var types=new[]{typeof(frmQuanLyThuoc),typeof(frmNhanVien),typeof(frmNhapKho),typeof(frmLoHang),typeof(frmPhieuNhap),typeof(frmHoaDon),typeof(frmNhomThuoc),typeof(frmNhaCungCap),typeof(frmKhachHang),typeof(frmVoucher),typeof(frmBaoCao),typeof(frmCanhBao),typeof(frmBanHang)};
        foreach(var size in new[]{new Size(1382,753),new Size(1100,700)})
        {
            main.ClientSize=size;
            foreach(var type in types)
            {
                typeof(frmMain).GetMethod("MoForm",PrivateInstance)!.MakeGenericMethod(type).Invoke(main,null);Application.DoEvents();
                var form=UiField<Dictionary<Type,Form>>(main,"cuaSoDangMo")[type];
                foreach(var grid in UiGrids(form))
                {
                    string context=type.Name+"/"+grid.Name+"/"+size.Width;
                    Check(grid.SelectionMode==DataGridViewSelectionMode.FullRowSelect && !grid.MultiSelect && !grid.AllowUserToResizeColumns,context+" uses row selection with locked column resizing");
                    Check(grid.ColumnHeadersDefaultCellStyle.SelectionBackColor==grid.ColumnHeadersDefaultCellStyle.BackColor && grid.ColumnHeadersDefaultCellStyle.SelectionForeColor==grid.ColumnHeadersDefaultCellStyle.ForeColor,context+" header keeps its normal color when selected");
                    foreach(DataGridViewColumn column in grid.Columns)
                        if(column.Visible)
                            Check(column.Resizable==DataGridViewTriState.False && grid.ColumnHeadersDefaultCellStyle.WrapMode==DataGridViewTriState.False && column.Width>=TextRenderer.MeasureText(column.HeaderText,grid.ColumnHeadersDefaultCellStyle.Font??grid.Font).Width+16,context+" single-line heading fits: "+column.HeaderText);
                }
                if(type==typeof(frmHoaDon) || type==typeof(frmPhieuNhap))
                {
                    var list=UiField<DataGridView>(form,type==typeof(frmHoaDon)?"dgvHoaDon":"dgvPhieuNhap");
                    var detail=UiField<DataGridView>(form,"dgvChiTiet");
                    ClickBackground(form);
                    Check(!detail.Visible && list.Height>200,type.Name+" uses full list space before a receipt is selected at "+size.Width);
                    if(list.Rows.Count>0)
                    {
                        typeof(DataGridView).GetMethod("OnCellClick",PrivateInstance)!.Invoke(list,new object[]{new DataGridViewCellEventArgs(0,0)});
                        Application.DoEvents();
                        var a=list.RectangleToScreen(list.ClientRectangle);var b=detail.RectangleToScreen(detail.ClientRectangle);
                        Check(detail.Visible && a.Bottom<b.Top && Math.Abs(a.Width-b.Width)<8,type.Name+" shows full-width list and details at "+size.Width);
                        var label=UiField<Label>(form,"lblChiTiet");
                        int needed=TextRenderer.MeasureText(label.Text,label.Font,new Size(Math.Max(1,label.Width-label.Padding.Horizontal),int.MaxValue),TextFormatFlags.WordBreak).Height;
                        Check(label.Height-label.Padding.Vertical>=needed-2,type.Name+" selected receipt summary fits at "+size.Width);
                        Check(detail.Rows.Count>0 && detail.GetRowDisplayRectangle(0,true).Height>=detail.Rows[0].Height-2,type.Name+" selected receipt shows complete detail rows at "+size.Width);
                        Directory.CreateDirectory("artifacts/previews/grid-receipt-report");
                        using var picture=new Bitmap(main.Width,main.Height);main.DrawToBitmap(picture,new Rectangle(0,0,picture.Width,picture.Height));
                        picture.Save("artifacts/previews/grid-receipt-report/"+type.Name+"-selected-"+size.Width+".png");
                    }
                }
            }
        }
        main.Dispose();
    }
}
