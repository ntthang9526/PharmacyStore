using Microsoft.EntityFrameworkCore;
using PharmacyWeb.Data;
using PharmacyWeb.Data.Entities;

namespace PharmacyWeb.Tests;

public class SharedPosModelTests
{
    [Fact]
    public void SharedCatalogAndWebTransactionsMustNotUseTheSameOrderTablesAsPos()
    {
        using var db = new PharmacyWebDbContext(new DbContextOptionsBuilder<PharmacyWebDbContext>()
            .UseSqlServer("Server=unused;Database=PharmacyStoreDB;Integrated Security=True").Options);
        Assert.Equal("Customers", db.Model.FindEntityType(typeof(CustomerProfile))!.GetTableName());
        Assert.Equal("Medicines", db.Model.FindEntityType(typeof(Medicine))!.GetTableName());
        Assert.Equal("Batches", db.Model.FindEntityType(typeof(Batch))!.GetTableName());
        Assert.Equal("WebOrders", db.Model.FindEntityType(typeof(Order))!.GetTableName());
        Assert.Equal("WebUsers", db.Model.FindEntityType(typeof(ApplicationUser))!.GetTableName());
        Assert.True(db.Model.FindEntityType(typeof(Batch))!.FindProperty(nameof(Batch.RowVersion))!.IsConcurrencyToken);
    }
}
