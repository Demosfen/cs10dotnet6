using Wms.Web.Services.Dto;

namespace Wms.Web.Services.Abstract;

public interface IPaletteService : IBusinessService
{
    Task CreateAsync(PaletteDto paletteDto, CancellationToken cancellationToken = default);
    
    Task<IReadOnlyCollection<PaletteDto>> GetAllAsync(
        Guid id, 
        int offset, int size,
        bool deleted = false,
        CancellationToken ct = default);

    Task<PaletteDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task RefreshAsync(Guid id, CancellationToken cancellationToken);
    
    Task UpdateAsync(PaletteDto paletteDto, CancellationToken cancellationToken = default);
    
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}