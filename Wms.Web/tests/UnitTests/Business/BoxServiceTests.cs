using AutoMapper;
using FluentAssertions;
using Moq;
using Wms.Web.Business.Abstract;
using Wms.Web.Business.Concrete;
using Wms.Web.Business.Dto;
using Wms.Web.Repositories.Interfaces;
using Wms.Web.Store.Entities.Concrete;
using Xunit;

namespace Wms.Web.UnitTests.Business;

public class BoxServiceTests
{
    private readonly IBoxService _sut;
    private readonly Mock<IPaletteService> _paletteServiceMock;
    private readonly Mock<IGenericRepository<Palette>> _paletteRepositoryMock;
    private readonly Mock<IGenericRepository<Box>> _boxRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    
    public BoxServiceTests()
    {
        _paletteServiceMock = new Mock<IPaletteService>();
        _paletteRepositoryMock = new Mock<IGenericRepository<Palette>>();
        _boxRepositoryMock = new Mock<IGenericRepository<Box>>();
        _mapperMock = new Mock<IMapper>();
        
        _sut = new BoxService(
            _paletteServiceMock.Object,
            _boxRepositoryMock.Object,
            _paletteRepositoryMock.Object,
            _mapperMock.Object);
    }

    [Fact]
    public void Create_ShouldCreateBox_WithCalculatedVolume()
    {
        // Arrange
        var boxId = Guid.NewGuid();
        var paletteId = Guid.NewGuid();
        
        var boxDto = new BoxDto
        {
            Id = boxId,
            PaletteId = paletteId,
            Width = 5,
            Height = 5,
            Depth = 5,
            Weight = 1,
            ProductionDate = new DateTime(2007, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
            ExpiryDate = new DateTime(2008, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
        };
        
        var box = new Box
        {
            Id = boxId,
            PaletteId = paletteId,
            Width = 5,
            Height = 5,
            Depth = 5,
            Weight = 1,
            ProductionDate = new DateTime(2007, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
            ExpiryDate = new DateTime(2008, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
        };
        
        var palette = new Palette
        {
            WarehouseId = Guid.NewGuid(),
            Id = paletteId,
            Width = 10,
            Height = 10,
            Depth = 10,
            Weight = 10,
            DeletedAt = null
        };

        _boxRepositoryMock
            .Setup(x => x.GetByIdAsync(boxId, default))
            .ReturnsAsync((Box?)null);

        _boxRepositoryMock
            .Setup(x => x.CreateAsync(box, default))
            .Returns(Task.CompletedTask);

        _paletteRepositoryMock
            .Setup(x => x.GetByIdAsync(paletteId, default))
            .ReturnsAsync(palette);

        _paletteServiceMock
            .Setup(x => x.RefreshAsync(box.PaletteId, default))
            .Returns(Task.CompletedTask);
        
        _mapperMock
            .Setup(x => x.Map<Box>(boxDto))
            .Returns(box);
        
        // Act
        _sut.CreateAsync(boxDto);
        
        // Assert
        boxDto.Volume.Should().Be(125);
    }
}