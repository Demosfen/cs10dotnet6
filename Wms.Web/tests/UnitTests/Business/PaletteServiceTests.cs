using AutoMapper;
using Moq;
using Wms.Web.Business.Abstract;
using Wms.Web.Business.Concrete;
using Wms.Web.Repositories.Interfaces;
using Wms.Web.Store.Entities.Concrete;

namespace Wms.Web.UnitTests.Business;

public class PaletteServiceTests
{
    private readonly IPaletteService _sut;
    private readonly Mock<IGenericRepository<Warehouse>> _warehouseRepositoryMock;
    private readonly Mock<IGenericRepository<Palette>> _paletteRepositoryMock;
    private readonly Mock<IGenericRepository<Box>> _boxRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    
    public PaletteServiceTests()
    {
        _warehouseRepositoryMock = new Mock<IGenericRepository<Warehouse>>();
        _paletteRepositoryMock = new Mock<IGenericRepository<Palette>>();
        _boxRepositoryMock = new Mock<IGenericRepository<Box>>();
        _mapperMock = new Mock<IMapper>();
        
        _sut = new PaletteService(
            _warehouseRepositoryMock.Object, 
            _paletteRepositoryMock.Object, 
            _boxRepositoryMock.Object,
            _mapperMock.Object);
    }
}