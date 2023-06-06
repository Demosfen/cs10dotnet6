using Moq;
using Wms.Web.Business.Abstract;
using Wms.Web.Repositories.Interfaces;
using Wms.Web.Store.Entities.Concrete;

namespace Wms.Web.UnitTests.Business;

public class WarehouseServiceTests
{
    private readonly IWarehouseService _sut;
    private readonly Mock<IGenericRepository<Warehouse>> _warehouseRepositoryMock;

    public WarehouseServiceTests(
        IWarehouseService sut, 
        Mock<IGenericRepository<Warehouse>> warehouseRepositoryMock)
    {
        _sut = sut;
        _warehouseRepositoryMock = warehouseRepositoryMock;
    }
}