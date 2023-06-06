using AutoMapper;
using Moq;
using Wms.Web.Business.Abstract;
using Wms.Web.Business.Concrete;
using Wms.Web.Repositories.Interfaces;
using Wms.Web.Store.Entities.Concrete;
using Xunit;

namespace Wms.Web.UnitTests.Business;

public class WarehouseServiceTests
{
    private readonly IWarehouseService _sut;
    private readonly Mock<IGenericRepository<Warehouse>> _warehouseRepositoryMock;
    private readonly Mock<IGenericRepository<Palette>> _paletteRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;

    public WarehouseServiceTests()
    {
        _warehouseRepositoryMock = new Mock<IGenericRepository<Warehouse>>();
        _paletteRepositoryMock = new Mock<IGenericRepository<Palette>>();
        _mapperMock = new Mock<IMapper>();
        
        _sut = new WarehouseService(
            _warehouseRepositoryMock.Object, 
            _paletteRepositoryMock.Object, 
            _mapperMock.Object);
    }
}