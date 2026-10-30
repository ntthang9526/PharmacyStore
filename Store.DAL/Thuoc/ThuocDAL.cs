using System.Data;
using Microsoft.Data.SqlClient;
namespace Store.DAL;
public partial class ThuocDAL
{
    private readonly DBConnection db = new();
    private const string SelectSql = @"
        SELECT m.BarCode AS MaVach, m.Id AS MaThuoc, m.Name AS TenThuoc, m.ActiveIngredient AS HoatChat, m.CategoryId AS MaNhom,
               m.SupplierId AS MaNCC, c.Name AS TenNhom, s.Name AS TenNCC, m.Unit AS DonViTinh, ROUND(m.Price, 0) AS DonGia,
               ISNULL(SUM(b.Stock), 0) AS SoLuongTon, gia.UnitCost AS GiaNhapGanNhat, m.IsSelling AS DangBan,m.IsPrescription AS KeDon, CAST(CASE WHEN COUNT(b.Id)>0 THEN 1 ELSE 0 END AS bit) AS KhoaNCC,
               CASE WHEN m.IsSelling=1 THEN N'Đang bán' ELSE N'Ngừng bán' END AS TrangThaiBan,
               ISNULL(SUM(CASE WHEN m.IsSelling=1 AND b.IsBlocked=0 AND b.ExpiryDate>=CAST(GETDATE() AS date) THEN b.Stock ELSE 0 END),0) AS TonCoTheBan
        FROM Medicines m
        JOIN Categories c ON c.Id = m.CategoryId
        JOIN Suppliers s ON s.Id = m.SupplierId
        LEFT JOIN Batches b ON b.MedicineId = m.Id
        OUTER APPLY (SELECT TOP(1) d.UnitCost FROM StockInDetails d
                     JOIN StockIns i ON i.Id=d.StockInId JOIN Batches ib ON ib.Id=d.BatchId
                     WHERE ib.MedicineId=m.Id ORDER BY i.ImportDate DESC,i.Id DESC,d.Id DESC) gia
        WHERE m.IsActive = 1 {0}
        GROUP BY m.BarCode, m.Id, m.Name, m.ActiveIngredient, m.CategoryId, m.SupplierId, c.Name, s.Name, m.Unit, m.Price, m.IsSelling, m.IsPrescription, gia.UnitCost ORDER BY m.Id";
    public DataTable GetAllThuoc() => db.DocBang(string.Format(SelectSql, ""));
    public DataTable TimKiemThuoc(string keyword) => db.DocBang(
        string.Format(SelectSql, "AND (CAST(m.Id AS NVARCHAR(20)) LIKE @kw OR m.Name LIKE @kw OR m.ActiveIngredient LIKE @kw OR c.Name LIKE @kw OR s.Name LIKE @kw OR m.BarCode LIKE @kw)"),
        new SqlParameter("@kw", SqlDbType.NVarChar, -1) { Value = DBConnection.LikePattern(keyword) });

    public bool InsertThuoc(string id, string name, string category, string unit, decimal price,
                            int stock, int supplierId, out string error, bool dangBan=true,bool? keDon=null,string? barcode=null,string? hoatChat=null)
        => Save(true, id, name, category, unit, price, supplierId, out error, dangBan,keDon,barcode,hoatChat);
    public bool UpdateThuoc(string id, string name, string category, string unit, decimal price,
                            int stock, int supplierId, out string error, bool dangBan=true,bool? keDon=null,string? barcode=null,string? hoatChat=null)
        => Save(false, id, name, category, unit, price, supplierId, out error, dangBan,keDon,barcode,hoatChat);

    private bool Save(bool insert, string id, string name, string category, string unit,
                      decimal price, int supplierId, out string error, bool dangBan,bool? keDon,string? barcode,string? hoatChat)
    {
        error = "";
        if (string.IsNullOrWhiteSpace(name) || name.Length > 150 ||
            string.IsNullOrWhiteSpace(unit) || unit.Length > 30 || price < 0 ||
            !int.TryParse(category, out int categoryId) || categoryId <= 0 || supplierId <= 0)
        { error = "Vui lòng nhập tên, nhóm thuốc, nhà cung cấp, đơn vị và đơn giá hợp lệ."; return false; }
        try
        {
            bool changeIngredient=insert || hoatChat!=null;
            hoatChat=ChuanHoaHoatChat(hoatChat);
            bool changeBarcode=insert || barcode!=null;
            barcode=ChuanHoaMaVach(barcode);
            return db.CapNhatDuLieu((insert ? @"IF EXISTS(
                    SELECT 1 FROM Medicines WITH (UPDLOCK,HOLDLOCK)
                    WHERE IsActive=1 AND SupplierId=@sup AND UPPER(LTRIM(RTRIM(Name)))=UPPER(@name)
                      AND UPPER(LTRIM(RTRIM(Unit)))=UPPER(@unit))
                    THROW 50012,N'Thuốc này đã có trong danh sách của nhà cung cấp.',1; " : "") + @"IF @changeBarcode=1 AND @barcode IS NOT NULL AND EXISTS(
                    SELECT 1 FROM Medicines WITH (UPDLOCK,HOLDLOCK) WHERE BarCode=@barcode AND Id<>@id)
                    THROW 50013,N'Mã vạch đã được dùng cho một thuốc khác.',1; " + (insert
                ? "INSERT INTO Medicines(CategoryId,SupplierId,Name,Unit,Price,IsSelling,IsPrescription,BarCode,ActiveIngredient) VALUES(@cat,@sup,@name,@unit,@price,@selling,COALESCE(@rx,0),@barcode,@ingredient)"
                : @"IF EXISTS(SELECT 1 FROM Medicines WITH (UPDLOCK,HOLDLOCK) WHERE Id=@id AND Unit COLLATE Latin1_General_100_BIN2<>@unit COLLATE Latin1_General_100_BIN2)
                      THROW 50014,N'Không được thay đổi đơn vị tính của thuốc đã tạo.',1;
                    IF EXISTS(SELECT 1 FROM Medicines WITH (UPDLOCK,HOLDLOCK) WHERE Id=@id AND SupplierId<>@sup)
                       AND EXISTS(SELECT 1 FROM Batches WITH (HOLDLOCK) WHERE MedicineId=@id)
                      THROW 50015,N'Không được đổi nhà cung cấp của thuốc đã có lô hàng hoặc phiếu nhập.',1;
                    UPDATE Medicines SET CategoryId=@cat,SupplierId=@sup,Name=@name,Price=@price,IsSelling=@selling,IsPrescription=COALESCE(@rx,IsPrescription),BarCode=CASE WHEN @changeBarcode=1 THEN @barcode ELSE BarCode END,ActiveIngredient=CASE WHEN @changeIngredient=1 THEN @ingredient ELSE ActiveIngredient END WHERE Id=@id"),
                new SqlParameter("@ingredient",SqlDbType.NVarChar,150) {Value=string.IsNullOrEmpty(hoatChat)?DBNull.Value:hoatChat},
                new SqlParameter("@changeIngredient",SqlDbType.Bit) {Value=changeIngredient},
                new SqlParameter("@barcode",SqlDbType.VarChar,50) {Value=(object?)barcode??DBNull.Value},
                new SqlParameter("@changeBarcode",SqlDbType.Bit) {Value=changeBarcode},
                new SqlParameter("@rx",SqlDbType.Bit) {Value=(object?)keDon??DBNull.Value},
                new SqlParameter("@selling", SqlDbType.Bit) { Value = dangBan },
                new SqlParameter("@id", SqlDbType.Int) { Value = insert ? 0 : int.Parse(id) },
                new SqlParameter("@cat", SqlDbType.Int) { Value = categoryId },
                new SqlParameter("@sup", SqlDbType.Int) { Value = supplierId },
                new SqlParameter("@name", SqlDbType.NVarChar, 150) { Value = name.Trim() },
                new SqlParameter("@unit", SqlDbType.NVarChar, 30) { Value = unit.Trim() },
                new SqlParameter("@price", SqlDbType.Decimal) { Precision = 18, Scale = 2, Value = Store.Shared.TienTe.LamTron(price) });
        }
        catch (Exception ex) { error = ex.Message; return false; }
    }
    private static string? ChuanHoaHoatChat(string? hoatChat)
    {
        hoatChat=hoatChat?.Trim();
        if(string.IsNullOrEmpty(hoatChat)) return null;
        if(hoatChat.Length>150 || hoatChat.Any(char.IsControl))
            throw new ArgumentException("Hoạt chất tối đa 150 ký tự, không chứa ký tự điều khiển.");
        return hoatChat;
    }
    private static string? ChuanHoaMaVach(string? barcode)
    {
        if(barcode==null) return null;
        barcode=barcode.Trim();
        if(barcode.Length==0) return null;
        if(barcode.Length>50 || barcode.Any(c=>c<'!' || c>'~'))
            throw new ArgumentException("Mã vạch tối đa 50 ký tự không dấu, không chứa khoảng trắng hoặc ký tự điều khiển.");
        return barcode;
    }
    public bool DeleteThuoc(string id, out string error)
    {
        error = "";
        try { return db.CapNhatDuLieu("UPDATE Medicines SET IsActive=0 WHERE Id=@id",
            new SqlParameter("@id", SqlDbType.Int) { Value = int.Parse(id) }); }
        catch (Exception ex) { error = ex.Message; return false; }
    }
}
