using AutoMapper;
using Wms.Web.Repositories.Abstract;
using Wms.Web.Repositories.Concrete;
using Wms.Web.Services.Abstract;
using Wms.Web.Services.Dto;
using Wms.Web.Store.Entities;

namespace Wms.Web.Services.Concrete;

public class PaletteService : IPaletteService
{
    private readonly PaletteRepository _paletteRepository;
    private readonly IMapper _mapper;

    public PaletteService(PaletteRepository paletteRepository, IMapper mapper)
    {
        _paletteRepository = paletteRepository;
        _mapper = mapper;
    }

    public async Task CreateAsync(PaletteDto paletteDto, CancellationToken ct = default)
    {
        var entity = await _paletteRepository.GetByIdAsync(paletteDto.Id, ct);

        if (entity is not null)
        {
            throw new ArgumentException();
        }

        entity = _mapper.Map<Palette>(paletteDto);

        await _paletteRepository.CreateAsync(entity, ct);
    }

    public Task<IReadOnlyCollection<PaletteDto>?> GetAllAsync(CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<PaletteDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        throw new NotImplementedException();
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