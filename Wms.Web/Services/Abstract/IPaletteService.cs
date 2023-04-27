using Wms.Web.Services.Dto;
using Wms.Web.Store.Entities;

namespace Wms.Web.Services.Abstract;

public interface IPaletteService : IBusinessService
{
    Task CreateAsync(PaletteDto paletteDto, CancellationToken ct = default);
    
    Task<IReadOnlyCollection<PaletteDto>> GetAllAsync(
        Guid id, 
        int offset, int size,
        bool deleted = false,
        CancellationToken ct = default);

    Task<PaletteDto?> GetByIdAsync(Guid id, int offset = 0, int size = 10, CancellationToken ct = default);
    
    Task UpdateAsync(PaletteDto paletteDto, CancellationToken ct = default);
    
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}