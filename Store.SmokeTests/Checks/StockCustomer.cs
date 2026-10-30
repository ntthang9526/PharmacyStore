using System.Data;
using System.Text;
using Store.BLL;
using Store.DAL;
using Store.POS;
using Store.Shared.Models;
internal static partial class Program
{
    static void AnswerTestDialogs(Action action,int answer)
    {
        uint thread=GetCurrentThreadId();
        using var timer=new System.Windows.Forms.Timer {Interval=30};
        timer.Tick+=(_,_)=>EnumThreadWindows(thread,(window,_)=>
        {
            var name=new StringBuilder(64);GetClassName(window,name,name.Capacity);
            if(name.ToString()=="#32770") PostMessage(window,0x111,(IntPtr)answer,IntPtr.Zero);
            return true;
        },IntPtr.Zero);
        timer.Start();try{action();Application.DoEvents();}finally{timer.Stop();}
    }
    static void TestStockCustomer()
    {
        string tag="SC"+Guid.NewGuid().ToString("N")[..10];int admin=PhanQuyenDAL.NhanVienId,group=0,supplier=0;
        string NewPhone(){string p;do{p="09"+System.Security.Cryptography.RandomNumberGenerator.GetInt32(10000000,99999999);}while(Count("SELECT COUNT(*) FROM Customers WHERE Phone=@p",("@p",p))>0);return p;}
        string phone=NewPhone(),changedPhone=NewPhone();while(changedPhone==phone)changedPhone=NewPhone();
        int staff=Count("SELECT TOP(1) Id FROM Employees WHERE Role='STAFF' AND IsActive=1 ORDER BY Id");
        Directory.CreateDirectory("artifacts/stock-customer-20261030");
        try
        {
            Check(new NhomThuocBLL().ThemNhanhNhom(tag,out group,out var error),"stock/customer category fixture");
            Check(new NhaCungCapBLL().ThemNhanhNCC(tag,"0901234567","Test",out supplier,out error),"stock/customer supplier fixture");
            Check(new ThuocBLL().ThemNhanhThuoc(tag,group,supplier,"Hộp",1000,out int med,out error),"stock/customer medicine fixture");
            int stockBefore=Count("SELECT COALESCE(SUM(Stock),0) FROM Batches WHERE MedicineId=@m",("@m",med));
            SetTestSession(staff);
            using(var form=new frmNhapKho())
            {
                Offscreen(form);UiField<ComboBox>(form,"cboNhaCungCap").SelectedValue=supplier;UiField<ComboBox>(form,"cboThuoc").SelectedValue=med;
                UiField<TextBox>(form,"txtSoLo").Text="DRAFT";UiField<NumericUpDown>(form,"nudSoLuong").Value=2;UiField<NumericUpDown>(form,"nudGiaNhap").Value=1000;
                UiField<Button>(form,"btnThemThuoc").PerformClick();
                var data=UiField<DataTable>(form,"dtPhieu");var grid=UiField<DataGridView>(form,"dgvPhieu");
                Check(data.Rows.Count==1 && (decimal)data.Rows[0]["ThanhTien"]==2000,"add creates exactly one draft line");
                void Select(int index)=>typeof(DataGridView).GetMethod("OnCellClick",PrivateInstance)!.Invoke(grid,new object[]{new DataGridViewCellEventArgs(0,index)});
                Select(0);Check(UiField<TextBox>(form,"txtSoLo").Text=="DRAFT" && UiField<NumericUpDown>(form,"nudSoLuong").Value==2 && UiField<NumericUpDown>(form,"nudGiaNhap").Value==1000,"select loads the draft row into inputs");
                UiField<NumericUpDown>(form,"nudSoLuong").Focus();Application.DoEvents();
                Check(UiField<Button>(form,"btnCapNhatDong").Enabled,"moving focus to inputs keeps Update usable");
                UiField<TextBox>(form,"txtSoLo").Text="CORRECTED";UiField<NumericUpDown>(form,"nudSoLuong").Value=5;UiField<NumericUpDown>(form,"nudGiaNhap").Value=4500.25m;
                UiField<Button>(form,"btnCapNhatDong").PerformClick();
                Check(data.Rows.Count==1 && (int)data.Rows[0]["SoLuong"]==5 && (decimal)data.Rows[0]["GiaNhap"]==4500 && (decimal)data.Rows[0]["ThanhTien"]==22500 && (string)data.Rows[0]["SoLo"]=="CORRECTED","update replaces selected line, rounds money and recalculates amount without duplicates");
                Check(!UiField<Button>(form,"btnCapNhatDong").Enabled,"update clears the editing target");
                Select(0);UiField<NumericUpDown>(form,"nudGiaNhap").Value=0;
                AnswerTestDialogs(()=>UiField<Button>(form,"btnCapNhatDong").PerformClick(),1);
                Check((decimal)data.Rows[0]["GiaNhap"]==4500 && data.Rows.Count==1,"invalid update keeps the original row unchanged");
                UiField<NumericUpDown>(form,"nudGiaNhap").Value=5000;form.LamMoiDuLieu();
                Check(UiField<NumericUpDown>(form,"nudGiaNhap").Value==5000 && UiField<Button>(form,"btnCapNhatDong").Enabled,"automatic refresh preserves draft changes and selected target");
                UiField<Button>(form,"btnXoaDong").PerformClick();
                Check(data.Rows.Count==0 && !UiField<Button>(form,"btnCapNhatDong").Enabled,"deleting a line clears the update target");
                Check(Count("SELECT COALESCE(SUM(Stock),0) FROM Batches WHERE MedicineId=@m",("@m",med))==stockBefore,"editing draft does not change stock before saving receipt");
            }
            using(var main=new frmMain())
            {
                Offscreen(main);typeof(frmMain).GetMethod("MoForm",PrivateInstance)!.MakeGenericMethod(typeof(frmNhapKho)).Invoke(main,null);
                var form=(frmNhapKho)UiField<Dictionary<Type,Form>>(main,"cuaSoDangMo")[typeof(frmNhapKho)];
                foreach(var size in new[]{new Size(1680,900),new Size(1100,700)})
                {
                    main.ClientSize=size;Application.DoEvents();
                    foreach(string field in new[]{"btnThemThuoc","btnCapNhatDong"})
                    {
                        var button=UiField<Button>(form,field);
                        Check(button.Visible && button.Width>130 && button.Right<=button.Parent!.ClientSize.Width && button.Bottom<=button.Parent.ClientSize.Height,"stock action fits "+field+" "+size.Width);
                        Check(TextRenderer.MeasureText(button.Text,button.Font).Width<=button.Width,"stock action caption fits "+field+" "+size.Width);
                    }
                }
                using var image=new Bitmap(main.Width,main.Height);main.DrawToBitmap(image,new Rectangle(Point.Empty,image.Size));image.Save("artifacts/stock-customer-20261030/stock.png");main.Dispose();
            }
            using(var customer=new frmKhachHang(phone))
            {
                Offscreen(customer);
                Check(UiField<TextBox>(customer,"txtDienThoai").Text==phone && UiField<TextBox>(customer,"txtTenKH").Enabled,"quick-add opens in Add mode with phone preserved after Load");
                customer.LamMoiDuLieu();Check(UiField<TextBox>(customer,"txtDienThoai").Text==phone,"automatic customer refresh preserves prefilled phone");
                UiField<TextBox>(customer,"txtTenKH").Text=tag;UiField<TextBox>(customer,"txtDienThoai").Text="123";
                AnswerTestDialogs(()=>UiField<Button>(customer,"btnThem").PerformClick(),1);
                Check(customer.CreatedPhone==null && !customer.IsDisposed && Count("SELECT COUNT(*) FROM Customers WHERE FullName=@n",("@n",tag))==0,"invalid quick-add phone does not close dialog or create customer");
            }
            using(var sales=new frmBanHang())
            {
                Offscreen(sales);UiField<TextBox>(sales,"boxMaKhachHang").Text=phone;
                bool handled=false;uint thread=GetCurrentThreadId();
                using var timer=new System.Windows.Forms.Timer{Interval=30};
                timer.Tick+=(_,_)=>
                {
                    var dialog=Application.OpenForms.OfType<frmKhachHang>().FirstOrDefault(f=>f.Visible);
                    if(dialog!=null && !handled)
                    {
                        handled=true;
                        Check(UiField<TextBox>(dialog,"txtDienThoai").Text==phone && UiField<TextBox>(dialog,"txtTenKH").Enabled,"sales passes the looked-up phone to quick-add");
                        UiField<TextBox>(dialog,"txtTenKH").Text=tag;UiField<TextBox>(dialog,"txtDienThoai").Text=changedPhone;
                        UiField<Button>(dialog,"btnThem").PerformClick();
                    }
                    EnumThreadWindows(thread,(window,_)=> {var n=new StringBuilder(64);GetClassName(window,n,n.Capacity);if(n.ToString()=="#32770")PostMessage(window,0x111,(IntPtr)6,IntPtr.Zero);return true;},IntPtr.Zero);
                };
                timer.Start();try{InvokePrivate(sales,"ThemKhachHang");}finally{timer.Stop();}
                var selected=UiField<Customer?>(sales,"khachHang");
                Check(handled && selected?.Phone==changedPhone && selected.FullName==tag,"sales automatically selects the saved customer even after phone correction");
                Check(UiField<TextBox>(sales,"boxMaKhachHang").Text==changedPhone && UiField<Label>(sales,"labelMaKhachHang").Text==tag,"sales updates phone and customer name after quick-add");
            }
            using(var manager=new frmKhachHang()) {Offscreen(manager);Check(!UiField<TextBox>(manager,"txtTenKH").Enabled && UiField<TextBox>(manager,"txtDienThoai").Text=="","normal customer management still starts in browse mode");}
        }
        finally
        {
            SetTestSession(admin);
            Scalar("DELETE FROM Customers WHERE FullName=@n AND Phone IN(@a,@b);DELETE FROM Medicines WHERE CategoryId=@g AND SupplierId=@s;DELETE FROM Suppliers WHERE Id=@s;DELETE FROM Categories WHERE Id=@g",("@n",tag),("@a",phone),("@b",changedPhone),("@g",group),("@s",supplier));DBConnection.NotifyDataChanged();
        }
    }
}
