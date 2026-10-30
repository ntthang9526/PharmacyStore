using System.Data;
using Store.BLL;
using Store.POS.Common;
namespace Store.POS;
public partial class frmQuanLyThuoc
{
    private void XuatExcel()
    {
        try {if(dgvThuoc.DataSource is DataTable data) ExcelHelper.XuatDanhSachThuoc(data);}
        catch(Exception ex) {MessageBox.Show(this,ex.Message,"Xuất Excel");}
    }
    private async void NhapExcel()
    {
        string? path=ExcelHelper.ChonFileExcelDeDoc();if(path==null) return;
        btnNhapExcel.Enabled=false;
        try
        {
            var result=await Task.Run(()=> {var rows=ExcelHelper.DocDanhSachThuocTuExcel(path,out var errors);return(rows,errors);});
            if(IsDisposed) return;
            var groups=new NhomThuocBLL().GetAllNhomThuoc().Rows.Cast<DataRow>().GroupBy(r=>((string)r["TenNhom"]).Trim(),StringComparer.OrdinalIgnoreCase).ToDictionary(g=>g.Key,g=>g.ToList(),StringComparer.OrdinalIgnoreCase);
            var suppliers=new NhaCungCapBLL().GetAllNhaCungCap().Rows.Cast<DataRow>().GroupBy(r=>((string)r["TenNCC"]).Trim(),StringComparer.OrdinalIgnoreCase).ToDictionary(g=>g.Key,g=>g.ToList(),StringComparer.OrdinalIgnoreCase);
            var valid=new List<MedicineImportRow>();var preview=new DataTable();
            foreach(string name in new[]{"Thuốc","Mã vạch","Nhóm","NCC","Đơn vị","Giá bán","Kê đơn","Hoạt chất"}) preview.Columns.Add(name);
            var seen=new HashSet<(string Name,int Supplier,string Unit)>();
            var seenBarcodes=new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var existing=bll.GetAllThuoc().Rows.Cast<DataRow>().ToArray();
            var existingNames=existing.Select(r=>(Convert.ToString(r["TenThuoc"])!.Trim().ToUpperInvariant(),(int)r["MaNCC"],Convert.ToString(r["DonViTinh"])!.Trim().ToUpperInvariant())).ToHashSet();
            var existingBarcodes=existing.Where(r=>r["MaVach"]!=DBNull.Value).Select(r=>Convert.ToString(r["MaVach"])!).ToHashSet(StringComparer.OrdinalIgnoreCase);
            foreach(var row in result.rows)
            {
                string? error=DanhMucValidation.NameError(row.TenThuoc,150)??DanhMucValidation.NameError(row.DonViTinh,30);
                if(error==null && (!groups.TryGetValue(row.TenNhom,out var g) || g.Count!=1)) error="Nhóm thuốc chưa có hoặc trùng tên. Hãy bổ sung danh mục trước.";
                if(error==null && (!suppliers.TryGetValue(row.TenNCC,out var s) || s.Count!=1)) error="NCC chưa có hoặc trùng tên. Hãy bổ sung danh mục trước.";
                if(error==null && (row.GiaBan>9999999999999999.99m || decimal.Round(row.GiaBan,2)!=row.GiaBan)) error="Giá bán không hợp lệ.";
                if(error==null && row.BarCode!=null && (row.BarCode.Length>50 || row.BarCode.Any(char.IsControl))) error="Mã vạch không hợp lệ.";
                if(error==null && row.HoatChat!=null && (row.HoatChat.Length>150 || row.HoatChat.Any(char.IsControl))) error="Hoạt chất tối đa 150 ký tự, không chứa ký tự điều khiển.";
                int supplierId=error==null?(int)suppliers[row.TenNCC][0]["MaNCC"]:0;
                var key=(row.TenThuoc.Trim().ToUpperInvariant(),supplierId,row.DonViTinh.Trim().ToUpperInvariant());
                if(error==null && seen.Contains(key)) error="Thuốc trùng tên, nhà cung cấp và đơn vị trong file.";
                if(error==null && row.BarCode!=null && seenBarcodes.Contains(row.BarCode)) error="Mã vạch trùng trong file.";
                if(error==null && existingNames.Contains(key)) error="Thuốc đã có trong danh sách của nhà cung cấp.";
                if(error==null && row.BarCode!=null && existingBarcodes.Contains(row.BarCode)) error="Mã vạch đã được dùng cho một thuốc khác.";
                if(error!=null) {result.errors.Add($"Dòng {row.RowIndex}: {error}");continue;}
                seen.Add(key);if(row.BarCode!=null) seenBarcodes.Add(row.BarCode);
                valid.Add(row);preview.Rows.Add(row.TenThuoc,row.BarCode,row.TenNhom,row.TenNCC,row.DonViTinh,Store.Shared.TienTe.LamTron(row.GiaBan).ToString("N0"),row.KeDon?"Có":"Không",row.HoatChat);
            }
            using var dialog=new frmXemTruocExcel(preview,result.errors);if(dialog.ShowDialog(this)!=DialogResult.OK) return;
            int saved=0;var errors=new List<string>();
            foreach(var row in valid)
                if(bll.ThemNhanhThuoc(row.TenThuoc,(int)groups[row.TenNhom][0]["MaNhom"],(int)suppliers[row.TenNCC][0]["MaNCC"],row.DonViTinh,row.GiaBan,out _,out string error,row.BarCode,row.KeDon,row.HoatChat)) saved++;
                else errors.Add($"Dòng {row.RowIndex}: {error}");
            LamMoiDuLieu();MessageBox.Show(this,$"Đã thêm {saved}/{valid.Count} thuốc.\n"+string.Join("\n",errors.Take(8)),"Kết quả nhập Excel");
        }
        catch(Exception ex) {if(!IsDisposed) MessageBox.Show(this,ex.Message,"Nhập Excel");}
        finally {if(!IsDisposed) btnNhapExcel.Enabled=true;}
    }
}
