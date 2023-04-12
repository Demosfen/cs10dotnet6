using Wms.Web.Services.Dto;

namespace Wms.Web.Services.Abstract;

public interface IPaletteService : IBusinessService
{
    Task CreateAsync(PaletteDto paletteDto, CancellationToken ct = default);
    
    Task<IReadOnlyCollection<PaletteDto>?> GetAllAsync(CancellationToken ct = default);

    Task<PaletteDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    
    Task UpdateAsync(PaletteDto paletteDto, CancellationToken ct = default);
    
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}