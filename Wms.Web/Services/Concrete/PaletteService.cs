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
    
    private readonly IGenericRepository<Palette> _paletteRepository;
    private readonly IMapper _mapper;

    public PaletteService(
        IGenericRepository<Palette> paletteRepository, 
        IMapper mapper)
    {
        _paletteRepository = paletteRepository;
        _mapper = mapper;
    }

    public async Task CreateAsync(PaletteDto paletteDto, CancellationToken ct = default)
    {
        if (await _paletteRepository.GetByIdAsync(paletteDto.Id, ct) != null)
        {
            throw new EntityAlreadyExistException(paletteDto.Id);
        }
        
        paletteDto.Weight = DefaultWeight;
        paletteDto.Volume = paletteDto.Width * paletteDto.Height * paletteDto.Depth;
        
        var entity = _mapper.Map<Palette>(paletteDto);

        await _paletteRepository.CreateAsync(entity, ct);
    }

    public async Task<IReadOnlyCollection<PaletteDto>> GetAllAsync(CancellationToken ct = default)
    {
        var entities = await _paletteRepository
            .GetAllAsync(includeProperties: nameof(Palette.Boxes), cancellationToken: ct);
        
        return _mapper.Map<IReadOnlyCollection<PaletteDto>>(entities);
    }

    public async Task<PaletteDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _paletteRepository
            .GetByIdAsync(id, nameof(Palette.Boxes), ct);

        return _mapper.Map<PaletteDto?>(entity);
    }

    public Task UpdateAsync(PaletteDto paletteDto, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        await _paletteRepository.DeleteAsync(id, ct);
    }
}