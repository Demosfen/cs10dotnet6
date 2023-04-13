using AutoMapper;
using Wms.Web.Common.Exceptions;
using Wms.Web.Repositories.Abstract;
using Wms.Web.Services.Abstract;
using Wms.Web.Store.Entities;
using Wms.Web.Services.Dto;

namespace Wms.Web.Services.Concrete;

internal sealed class PaletteService : IPaletteService
{
    private const int DefaultWeight = 30;
    
    private readonly IWarehouseService _warehouseService;
    private readonly IGenericRepository<Palette> _paletteRepository;
    private readonly IMapper _mapper;

    public PaletteService(
        IWarehouseService warehouseService,
        IPaletteRepository paletteRepository, 
        IMapper mapper)
    {
        _warehouseService = warehouseService;
        _paletteRepository = paletteRepository;
        _mapper = mapper;
    }

    public async Task CreateAsync(PaletteDto paletteDto, CancellationToken ct = default)
    {
        paletteDto.Weight = DefaultWeight;
        paletteDto.Volume = paletteDto.Width * paletteDto.Height * paletteDto.Depth;
        
        var entity = _mapper.Map<Palette>(paletteDto);

        await _paletteRepository.CreateAsync(entity, ct);
    }

    public async Task<IReadOnlyCollection<PaletteDto>> GetAllAsync(CancellationToken ct = default)
    {
        var entities = await _paletteRepository.GetAllAsync(cancellationToken: ct);
        
        return _mapper.Map<IReadOnlyCollection<PaletteDto>>(entities);
    }

    public async Task<PaletteDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var entities = await _paletteRepository.GetByIdAsync(id, ct);

        return _mapper.Map<PaletteDto?>(entities);
    }

    public Task UpdateAsync(PaletteDto paletteDto, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}