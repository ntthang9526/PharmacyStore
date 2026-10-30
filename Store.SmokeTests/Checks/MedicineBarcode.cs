using Microsoft.Data.SqlClient;
using Store.DAL;
using Store.BLL;
using Store.POS;
internal static partial class Program
{
    static void TestMedicineBarcode()
    {
        string tag="BARCODE_"+Guid.NewGuid().ToString("N")[..10],name=tag+" Medicine",code="00"+Guid.NewGuid().ToString("N")[..18];
        int admin=PhanQuyenDAL.NhanVienId,category=0,supplier=0,med=0;
        var catalog=new ThuocBLL();var dal=new ThuocDAL();
        string Code()=>Convert.ToString(Scalar("SELECT BarCode FROM Medicines WHERE Id=@m",("@m",med)))??"";
        bool Update(string? barcode,out string error)=>catalog.SuaThuoc(med.ToString(),name,category.ToString(),"Hộp","1000","0",supplier,out error,barcode:barcode);
        try
        {
            Check(new NhomThuocBLL().ThemNhanhNhom(tag,out category,out var error),"barcode category created");
            Check(new NhaCungCapBLL().ThemNhanhNCC(tag,"0901234567","Test",out supplier,out error),"barcode supplier created");
            Check(catalog.ThemMoiThuoc("",name,category.ToString(),"Hộp","1000","0",supplier,out error,barcode:"  "+code+"  ",hoatChat:"  Paracetamol + cafein  "),"normal Add stores a barcode: "+error);
            med=Count("SELECT Id FROM Medicines WHERE Name=@n",("@n",name));
            Check(Code()==code && new MedicineDAL().GetMedicines()[med].BarCode==code,"barcode is trimmed and preserves leading zeroes in database and sales lookup");
            Check(Update(code,out error),"same barcode remains valid when editing its own medicine");
            Check(!catalog.ThemMoiThuoc("",tag+" Duplicate",category.ToString(),"Hộp","1000","0",supplier,out error,barcode:code) && error.Contains("Mã vạch"),"normal Add rejects duplicate barcode");
            Check(!catalog.ThemNhanhThuoc(tag+" Quick duplicate",category,supplier,"Hộp",1000,out _,out error,code),"quick Add cannot duplicate a barcode from normal Add");
            string otherCode=code+"-B";
            Check(catalog.ThemNhanhThuoc(tag+" Other",category,supplier,"Hộp",1000,out int other,out error,otherCode),"quick Add fixture has another barcode");
            Check(!Update(otherCode,out error) && Code()==code,"editing rejects another medicine's barcode without changing stored code");
            foreach(string invalid in new[]{new string('1',51),"ABC DEF","ABC\tDEF".Replace("\t","	"),"MÃ123"})
                Check(!Update(invalid,out error) && Code()==code,"invalid barcode rejected without changes: "+invalid.Length+" chars");
            Check(catalog.SuaThuoc(med.ToString(),name,category.ToString(),"Hộp","1000","0",supplier,out error) && Code()==code,"existing callers that omit barcode preserve it");
            Check(Update("",out error) && Scalar("SELECT BarCode FROM Medicines WHERE Id=@m",("@m",med))==DBNull.Value,"clearing barcode explicitly stores NULL");
            Check(Update(code,out error),"barcode can be restored after clearing");
            Check(!dal.UpdateThuoc(other.ToString(),tag+" Other",category.ToString(),"Hộp",1000,0,supplier,out error,barcode:code),"direct DAL update cannot bypass duplicate validation");
            string raceCode=code+"-RACE";
            var first=Task.Run(()=>dal.InsertThuoc("",tag+" Race 1",category.ToString(),"Hộp",1000,0,supplier,out _,barcode:raceCode));
            var second=Task.Run(()=>new ThuocDAL().InsertThuoc("",tag+" Race 2",category.ToString(),"Hộp",1000,0,supplier,out _,barcode:raceCode));
            Task.WaitAll(first,second);
            Check((first.Result?1:0)+(second.Result?1:0)==1 && Count("SELECT COUNT(*) FROM Medicines WHERE BarCode=@b",("@b",raceCode))==1,"simultaneous saves cannot create duplicate barcodes");
            string Ingredient()=>Convert.ToString(Scalar("SELECT ActiveIngredient FROM Medicines WHERE Id=@m",("@m",med)))??"";
            Check(Ingredient()=="Paracetamol + cafein","normal Add trims and stores active ingredient");
            Check(!catalog.ThemMoiThuoc("",name.ToUpperInvariant()+"  ",category.ToString()," HỘP ","1000","0",supplier,out error) && error.Contains("đã có"),"normal Add rejects same name/supplier/unit ignoring case and outer spaces");
            Check(!dal.InsertThuoc("",name,category.ToString(),"Hộp",1000,0,supplier,out error),"direct DAL cannot bypass name/supplier/unit duplicate protection");
            Check(!catalog.ThemNhanhThuoc(name,category,supplier,"Hộp",1000,out _,out error),"quick Add agrees with normal duplicate protection");
            Check(catalog.ThemMoiThuoc("",name,category.ToString(),"Viên","1000","0",supplier,out error),"same name with a different unit remains allowed");
            string raceName=tag+" Same Name";
            var nameFirst=Task.Run(()=>new ThuocDAL().InsertThuoc("",raceName,category.ToString(),"Hộp",1000,0,supplier,out _));
            var nameSecond=Task.Run(()=>new ThuocDAL().InsertThuoc("",raceName,category.ToString(),"Hộp",1000,0,supplier,out _));
            Task.WaitAll(nameFirst,nameSecond);
            Check((nameFirst.Result?1:0)+(nameSecond.Result?1:0)==1 && Count("SELECT COUNT(*) FROM Medicines WHERE Name=@n",("@n",raceName))==1,"simultaneous normal Adds cannot duplicate a name/supplier/unit");
            Check(catalog.SuaThuoc(med.ToString(),name,category.ToString(),"Hộp","1000","0",supplier,out error) && Ingredient()=="Paracetamol + cafein","old callers preserve active ingredient when omitted");
            Check(catalog.SuaThuoc(med.ToString(),name,category.ToString(),"Hộp","1000","0",supplier,out error,hoatChat:"  Acetylcystein  ") && Ingredient()=="Acetylcystein","Edit trims and updates active ingredient");
            Check(catalog.TimKiemThuoc("Acetylcystein").Rows.Cast<System.Data.DataRow>().Any(r=>(int)r["MaThuoc"]==med && (string)r["HoatChat"]=="Acetylcystein"),"catalog can search and display active ingredient");
            foreach(string invalid in new[]{new string('A',151),"Bad\tingredient"})
                Check(!dal.UpdateThuoc(med.ToString(),name,category.ToString(),"Hộp",1000,0,supplier,out error,hoatChat:invalid) && Ingredient()=="Acetylcystein","invalid ingredient rejected atomically through direct DAL");
            Check(catalog.SuaThuoc(med.ToString(),name,category.ToString(),"Hộp","1000","0",supplier,out error,hoatChat:" ") && Scalar("SELECT ActiveIngredient FROM Medicines WHERE Id=@m",("@m",med))==DBNull.Value,"explicitly clearing ingredient stores NULL");
            Check(catalog.SuaThuoc(med.ToString(),name,category.ToString(),"Hộp","1000","0",supplier,out error,hoatChat:"Paracetamol + cafein"),"ingredient restored for UI selection check");
            using(var form=new frmQuanLyThuoc())
            {
                Offscreen(form);var grid=UiField<DataGridView>(form,"dgvThuoc");var row=grid.Rows.Cast<DataGridViewRow>().Single(r=>(int)r.Cells["MaThuoc"].Value==med);
                InvokePrivate(form,"dgvThuoc_CellClick",grid,new DataGridViewCellEventArgs(0,row.Index));var barcode=UiField<TextBox>(form,"txtMaVach");
                Check(UiField<TextBox>(form,"txtHoatChat").Text=="Paracetamol + cafein" && !UiField<TextBox>(form,"txtHoatChat").Enabled,"selection displays ingredient read-only");
                Check(barcode.Text==code && !barcode.Enabled && barcode.MaxLength==50,"selected medicine displays its barcode read-only with length limit");
                UiField<Button>(form,"btnSua").PerformClick();Check(barcode.Enabled,"barcode becomes editable in Edit mode");
                UiField<TextBox>(form,"txtHoatChat").Text="Draft ingredient";
                barcode.Text="DRAFT-"+code;form.LamMoiDuLieu();Check(barcode.Text=="DRAFT-"+code,"automatic refresh preserves barcode being edited");
                Check(UiField<TextBox>(form,"txtHoatChat").Enabled && UiField<TextBox>(form,"txtHoatChat").Text=="Draft ingredient","automatic refresh preserves ingredient while editing");
                var key=new KeyEventArgs(Keys.Enter);typeof(Control).GetMethod("OnKeyDown",PrivateInstance)!.Invoke(barcode,new object[]{key});Check(key.SuppressKeyPress,"scanner Enter does not submit an incomplete medicine");
                UiField<Button>(form,"btnBoQua").PerformClick();Check(barcode.Text=="" && !barcode.Enabled,"Cancel clears and locks barcode input");
                Check(!UiField<TextBox>(form,"txtHoatChat").Enabled && UiField<TextBox>(form,"txtHoatChat").Text=="","Cancel clears and locks ingredient");
                UiField<Button>(form,"btnThem").PerformClick();Check(barcode.Enabled && barcode.Text=="","Add starts with empty editable barcode");
                Check(UiField<TextBox>(form,"txtHoatChat").Enabled && UiField<TextBox>(form,"txtHoatChat").Text=="" && UiField<TextBox>(form,"txtHoatChat").MaxLength==150,"Add starts with empty ingredient and length limit");
            }
            int staff=Count("SELECT TOP(1) Id FROM Employees WHERE Role='STAFF' AND IsActive=1 ORDER BY Id");SetTestSession(staff);
            using(var staffForm=new frmQuanLyThuoc())
            {
                Offscreen(staffForm);Check(!UiField<TextBox>(staffForm,"txtHoatChat").Enabled,"staff can only view ingredient");Check(!UiField<TextBox>(staffForm,"txtMaVach").Enabled && !UiField<Button>(staffForm,"btnSua").Visible,"staff may view barcodes but cannot edit them");
            }
            Check(!Update("STAFF-CHANGE",out error) && Code()==code,"staff cannot bypass barcode edit permissions through BLL");
            Check(!catalog.SuaThuoc(med.ToString(),name,category.ToString(),"Hộp","1000","0",supplier,out error,hoatChat:"Staff change") && Ingredient()=="Paracetamol + cafein","staff cannot edit ingredient through BLL");
            SetTestSession(admin);
            Directory.CreateDirectory("artifacts/medicine-barcode-20261030");
            using var main=new frmMain();Offscreen(main);
            typeof(frmMain).GetMethod("MoForm",PrivateInstance)!.MakeGenericMethod(typeof(frmQuanLyThuoc)).Invoke(main,null);
            var medicineForm=UiField<Dictionary<Type,Form>>(main,"cuaSoDangMo")[typeof(frmQuanLyThuoc)];
            foreach(var size in new[]{new Size(1680,900),new Size(1500,820),new Size(1100,700)})
            {
                main.ClientSize=size;Application.DoEvents();
                IEnumerable<Control> Walk(Control parent){foreach(Control child in parent.Controls){yield return child;foreach(var item in Walk(child))yield return item;}}
                foreach(var control in Walk(medicineForm).Where(c=>c.Visible && c is Button or TextBoxBase or ComboBox or Label))
                {
                    var rect=control.RectangleToScreen(control.ClientRectangle);var visible=rect;
                    for(Control? parent=control.Parent;parent!=null;parent=parent.Parent)visible=Rectangle.Intersect(visible,parent.RectangleToScreen(parent.ClientRectangle));
                    Check(visible.Width>=rect.Width-2 && visible.Height>=rect.Height-2,"medicine "+size.Width+" control fits: "+control.Name);
                }
                using var image=new Bitmap(main.Width,main.Height);main.DrawToBitmap(image,new Rectangle(Point.Empty,image.Size));image.Save("artifacts/medicine-barcode-20261030/medicine-"+size.Width+".png");
            }
            main.Dispose();
        }
        finally
        {
            SetTestSession(admin);using var connection=DBConnection.GetDbConnection();
            using var command=new SqlCommand("DELETE FROM Medicines WHERE CategoryId=@g AND SupplierId=@s;DELETE FROM Suppliers WHERE Id=@s;DELETE FROM Categories WHERE Id=@g",connection);
            command.Parameters.AddWithValue("@g",category);command.Parameters.AddWithValue("@s",supplier);command.ExecuteNonQuery();DBConnection.NotifyDataChanged();
        }
    }
}
