using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WMS.Repositories.Abstract;
using WMS.Repositories.Concrete;
using WMS.Store.Entities;
using WMS.Tests.Abstract;
using WMS.Tests.Infrastructure;

namespace WMS.Tests.Repositories;

public class WarehouseRepositoryTests: WarehouseTestsBase
{
    private readonly IGenericRepository<Warehouse> _sut;

    public WarehouseRepositoryTests(TestDatabaseFixture fixture) 
        : base(fixture)
    {
        _sut = new GenericRepository<Warehouse>(DbContext);
    }

    [Fact(DisplayName = "Check if repository successfully inserts and saves entities")]
    public async Task RepositoryInsert_ShouldInsertWarehouse()
    {
        // Arrange, Act
        await _sut.AddAsync(new Warehouse("Warehouse1"));
        await _sut.UnitOfWork.SaveChangesAsync();

        // Assert
        var result = await DbContext.Warehouses
            .FirstOrDefaultAsync(x => x.Name == "Warehouse1");
        result.Should().NotBeNull();
    }
    
    [Fact(DisplayName = "Check if repository successfully inserts and saves entities")]
    public async Task RepositoryDelete_ShouldSoftDeleteWarehouse()
    {
        // Arrange
        var warehouse = new Warehouse("Warehouse2");
        
        // Act
        await _sut.AddAsync(warehouse);
        await _sut.UnitOfWork.SaveChangesAsync();

        await _sut.DeleteAsync(warehouse.Id);
        await _sut.UnitOfWork.SaveChangesAsync();

        // Assert
        var result = await DbContext.Warehouses
            .FindAsync(warehouse.Id);
        result?.IsDeleted.Should().Be(true);
    }
}