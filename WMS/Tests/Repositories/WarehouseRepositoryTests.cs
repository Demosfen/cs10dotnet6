using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WMS.Repositories.Abstract;
using WMS.Repositories.Concrete;
using WMS.Tests.Abstract;
using WMS.Tests.Infrastructure;
using WMS.WarehouseDbContext.Entities;

namespace WMS.Tests.Repositories;

public class WarehouseRepositoryTests: WarehouseTestsBase
{
    private readonly IGenericRepository<Warehouse> _sut;

    public WarehouseRepositoryTests(TestDatabaseFixture fixture) 
        : base(fixture)
    {
        _sut = new GenericRepository<Warehouse>(DbContext);
    }

    [Fact(DisplayName = "Check if UnitOfWork successfully inserts and saves entities")]
    public async Task RepositoryInsert_ShouldInsertWarehouse()
    {
        // Arrange, Act
        await _sut.InsertAsync(new Warehouse("Warehouse1"));
        await _sut.UnitOfWork.SaveChangesAsync();

        // Assert
        var result = await DbContext.Warehouses
            .FirstOrDefaultAsync(x => x.Name == "Warehouse1");
        result.Should().NotBeNull();
    }
}