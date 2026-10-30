using System.Data;
using Microsoft.Data.SqlClient;
namespace Store.DAL;
public partial class VoucherDAL
{
    public DataTable TimKiem(string keyword) => new DBConnection().DocBang(@"
        SELECT Id, Code, DiscountType,
               CASE DiscountType WHEN 'PERCENT' THEN N'Phần trăm (%)' ELSE N'Số tiền (đ)' END AS LoaiGiam,
               DiscountValue, Quantity, IsActive
        FROM Vouchers WHERE Code LIKE @kw ORDER BY Id DESC",
        new SqlParameter("@kw", SqlDbType.NVarChar, -1) { Value = DBConnection.LikePattern(keyword) });

    public bool Luu(int? id, string code, string type, decimal value, int quantity, bool active, int? originalQuantity)
    {
        using var conn = DBConnection.GetDbConnection();
        using var transaction=conn.BeginTransaction();
        PhanQuyenDAL.YeuCau(Quyen.QuanTri,conn,transaction);
        using var cmd = new SqlCommand(id.HasValue ? @"
            UPDATE Vouchers SET Code=@code, DiscountType=@type, DiscountValue=@value,
                   Quantity=@qty, IsActive=@active
            WHERE Id=@id AND Quantity=@oldQty" : @"
            INSERT INTO Vouchers(Code,DiscountType,DiscountValue,Quantity,IsActive)
            VALUES(@code,@type,@value,@qty,@active)", conn,transaction);
        cmd.Parameters.Add("@id", SqlDbType.Int).Value = (object?)id ?? DBNull.Value;
        cmd.Parameters.Add("@code", SqlDbType.VarChar, 20).Value = code;
        cmd.Parameters.Add("@type", SqlDbType.VarChar, 10).Value = type;
        cmd.Parameters.Add("@value", SqlDbType.Decimal).Value = value;
        cmd.Parameters["@value"].Precision = 18; cmd.Parameters["@value"].Scale = 2;
        cmd.Parameters.Add("@qty", SqlDbType.Int).Value = quantity;
        cmd.Parameters.Add("@active", SqlDbType.Bit).Value = active;
        cmd.Parameters.Add("@oldQty", SqlDbType.Int).Value = (object?)originalQuantity ?? DBNull.Value;
        bool changed = cmd.ExecuteNonQuery() == 1;
        transaction.Commit();
        if (changed) DBConnection.NotifyDataChanged();
        return changed;
    }

    public bool Xoa(int id) => new DBConnection().CapNhatDuLieu(
        "DELETE FROM Vouchers WHERE Id=@id", new SqlParameter("@id", SqlDbType.Int) { Value = id });
}
