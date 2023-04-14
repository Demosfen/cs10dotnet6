using Wms.Web.Services.Dto;

namespace Wms.Web.Services.Abstract;

public interface IBoxService : IBusinessService
{
    Task CreateAsync(BoxDto boxDto, CancellationToken ct = default);
    
    Task<IReadOnlyCollection<BoxDto>?> GetAllAsync(CancellationToken ct = default);

    Task<BoxDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    
    Task UpdateAsync(BoxDto boxDto, CancellationToken ct = default);
    
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}