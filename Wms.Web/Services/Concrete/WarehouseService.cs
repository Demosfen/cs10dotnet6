using AutoMapper;
using Wms.Web.Common.Exceptions;
using Wms.Web.Repositories.Abstract;
using Wms.Web.Services.Abstract;
using Wms.Web.Store.Entities;
using Wms.Web.Services.Dto;

namespace Wms.Web.Services.Concrete;

internal sealed class WarehouseService : IWarehouseService
{
    private readonly IGenericRepository<Warehouse> _warehouseRepository;
    private readonly IMapper _mapper;

    public WarehouseService(
        IWarehouseRepository warehouseRepository, 
        IMapper mapper)
    {
        _warehouseRepository = warehouseRepository;
        _mapper = mapper;
    }

    public Task CreateAsync(WarehouseDto warehouseDto, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyCollection<WarehouseDto>?> GetAllAsync(CancellationToken ct)
    {
        var entities = await _warehouseRepository.GetAllAsync(cancellationToken: ct);
        return _mapper.Map<IReadOnlyCollection<WarehouseDto>>(entities);
    }

    public async Task<WarehouseDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _warehouseRepository.GetByIdAsync(id, ct);

        return _mapper.Map<WarehouseDto>(entity);
    }

    public Task UpdateAsync(WarehouseDto warehouseDto, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}