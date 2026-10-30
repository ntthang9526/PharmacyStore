using System.Data;
using ClosedXML.Excel;
using Store.BLL;
using Store.DAL;
using Store.POS;
using Store.POS.Common;
internal static partial class Program
{
    static void TestMedicineExcel()
    {
        string tag="ME"+Guid.NewGuid().ToString("N")[..10];
        string folder=Path.Combine("artifacts","medicine-excel-"+tag);Directory.CreateDirectory(folder);
        int category=0,supplier=0,admin=PhanQuyenDAL.NhanVienId;
        var catalog=new ThuocBLL();
        try
        {
            Check(new NhomThuocBLL().ThemNhanhNhom(tag,out category,out var error),"Excel category fixture");
            Check(new NhaCungCapBLL().ThemNhanhNCC(tag,"0901234567","Test",out supplier,out error),"Excel supplier fixture");
            string template=Path.Combine(folder,"template.xlsx");ExcelHelper.XuatFileMauNhapThuoc(template,false);
            using(var book=new XLWorkbook(template))
            {
                var ws=book.Worksheet(1);
                Check(ws.Cell(1,8).GetString().StartsWith("Hoạt chất") && ws.Column(2).Style.NumberFormat.Format=="@","template includes optional ingredient and text barcode column");
                ws.Cell(2,1).Value=tag;ws.Cell(2,2).Value="00"+tag;ws.Cell(2,3).Value=tag;ws.Cell(2,4).Value=tag;ws.Cell(2,5).Value="Hộp";
                ws.Cell(2,7).Value=1;ws.Cell(2,8).Value="  Hoạt chất thử nghiệm  ";ws.Row(3).Delete();book.Save();
            }
            var rows=ExcelHelper.DocDanhSachThuocTuExcel(template,out var errors);
            Check(rows.Count==1 && errors.Count==0 && rows[0].HoatChat=="Hoạt chất thử nghiệm" && rows[0].KeDon && rows[0].BarCode=="00"+tag,"parser trims Unicode ingredient, preserves leading zeroes and prescription flag");
            var row=rows.Single();
            Check(catalog.ThemNhanhThuoc(row.TenThuoc,category,supplier,row.DonViTinh,row.GiaBan,out int med,out error,row.BarCode,row.KeDon,row.HoatChat),"import save accepts ingredient through validated business layer: "+error);
            Check((string)Scalar("SELECT ActiveIngredient FROM Medicines WHERE Id=@m",("@m",med))==row.HoatChat && Count("SELECT CAST(IsPrescription AS int) FROM Medicines WHERE Id=@m",("@m",med))==1,"import persists ingredient and prescription flag together");
            Check(!catalog.ThemNhanhThuoc(row.TenThuoc,category,supplier,row.DonViTinh,row.GiaBan,out _,out error,null,false,"Different ingredient") && error.Contains("đã có"),"changing ingredient does not bypass duplicate name/supplier/unit check");
            Check(!catalog.ThemNhanhThuoc(tag+" Barcode duplicate",category,supplier,"Hộp",1000,out _,out error,row.BarCode,false,row.HoatChat) && error.Contains("Mã vạch"),"import retains duplicate barcode protection");
            string exported=Path.Combine(folder,"export.xlsx");ExcelHelper.XuatDanhSachThuoc(catalog.TimKiemThuoc(tag),exported,false);
            var roundtrip=ExcelHelper.DocDanhSachThuocTuExcel(exported,out errors);
            Check(errors.Count==0 && roundtrip.Count==1 && roundtrip[0].HoatChat==row.HoatChat && roundtrip[0].KeDon && roundtrip[0].BarCode==row.BarCode,"export/import roundtrip retains ingredient, prescription and barcode");
            var data=new DataTable();foreach(string header in new[]{"Thuốc","Mã vạch","Nhóm","NCC","Đơn vị","Giá bán","Kê đơn","Hoạt chất"}) data.Columns.Add(header);
            data.Rows.Add(row.TenThuoc,row.BarCode,row.TenNhom,row.TenNCC,row.DonViTinh,row.GiaBan.ToString(),"Có",row.HoatChat);
            using(var preview=new frmXemTruocExcel(data,new List<string>()))
            {
                Offscreen(preview);var grid=UiField<DataGridView>(preview,"dgvDuLieu");
                Check(grid.Columns["Hoạt chất"]!.Visible && (string)grid.Rows[0].Cells["Hoạt chất"].Value==row.HoatChat,"preview displays ingredient column");
                Check(grid.Columns["Kê đơn"]!.Visible && (string)grid.Rows[0].Cells["Kê đơn"].Value=="Có","preview displays prescription flag");
                using var picture=new Bitmap(preview.Width,preview.Height);preview.DrawToBitmap(picture,new Rectangle(Point.Empty,picture.Size));picture.Save(Path.Combine(folder,"preview.png"));
            }
            using(var stockPreview=new frmXemTruocExcel(new DataTable(),new List<string>()))
            {Offscreen(stockPreview);Check(!UiField<Button>(stockPreview,"btnNhap").Enabled,"empty preview still cannot import");}
            foreach(int columns in new[]{6,7})
            {
                string legacy=Path.Combine(folder,"legacy-"+columns+".xlsx");
                using(var book=new XLWorkbook(template)) {var ws=book.Worksheet(1);ws.Columns(columns+1,8).Delete();book.SaveAs(legacy);}
                var oldRows=ExcelHelper.DocDanhSachThuocTuExcel(legacy,out errors);
                Check(errors.Count==0 && oldRows.Count==1 && oldRows[0].HoatChat==null && oldRows[0].KeDon==(columns==7),"legacy "+columns+"-column template remains compatible");
            }
            foreach(string ingredient in new[]{new string('A',151),"Invalid\tingredient"})
            {
                string path=Path.Combine(folder,"invalid.xlsx");
                using(var book=new XLWorkbook(template)) {book.Worksheet(1).Cell(2,8).Value=ingredient;book.SaveAs(path);}
                Check(ExcelHelper.DocDanhSachThuocTuExcel(path,out errors).Count==0 && errors.Count==1 && errors[0].StartsWith("Dòng 2: Hoạt chất"),"invalid ingredient reports its Excel row");
                Check(!catalog.ThemNhanhThuoc(tag+" Invalid",category,supplier,"Hộp",1000,out _,out error,hoatChat:ingredient),"business save also rejects invalid ingredient");
            }
            bool directRejected=false;
            try {new ThuocDAL().ThemNhanh(tag+" Direct",category,supplier,"Hộp",1000,hoatChat:new string('X',151));}
            catch(ArgumentException){directRejected=true;}
            Check(directRejected,"direct DAL cannot silently truncate ingredient");
            foreach(string code in new[]{new string('0',51),"ABC DEF","MÃ123"})
            {
                string path=Path.Combine(folder,"invalid-code.xlsx");
                using(var book=new XLWorkbook(template)) {book.Worksheet(1).Cell(2,2).Value=code;book.SaveAs(path);}
                Check(ExcelHelper.DocDanhSachThuocTuExcel(path,out errors).Count==0 && errors.Count==1 && errors[0].Contains("Mã vạch"),"invalid barcode rejected before preview");
            }
            string reordered=Path.Combine(folder,"optional-reordered.xlsx");
            using(var book=new XLWorkbook(template))
            {var ws=book.Worksheet(1);ws.Cell(1,7).Value="Hoạt chất";ws.Cell(2,7).Value="";ws.Cell(1,8).Value="Kê đơn";ws.Cell(2,8).Value=1;book.SaveAs(reordered);}
            var optionalRows=ExcelHelper.DocDanhSachThuocTuExcel(reordered,out errors);
            Check(errors.Count==0 && optionalRows.Single().HoatChat==null && optionalRows.Single().KeDon,"optional columns are found by header and blank ingredient is valid");
            int staff=Count("SELECT TOP(1) Id FROM Employees WHERE Role='STAFF' AND IsActive=1 ORDER BY Id");SetTestSession(staff);
            Check(!catalog.ThemNhanhThuoc(tag+" Staff",category,supplier,"Hộp",1000,out _,out error,hoatChat:row.HoatChat),"staff cannot bypass Excel import permissions");
        }
        finally
        {
            SetTestSession(admin);
            Scalar("DELETE FROM Medicines WHERE CategoryId=@g AND SupplierId=@s;DELETE FROM Suppliers WHERE Id=@s;DELETE FROM Categories WHERE Id=@g",("@g",category),("@s",supplier));DBConnection.NotifyDataChanged();
        }
    }
}
