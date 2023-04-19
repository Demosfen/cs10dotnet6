using AutoMapper;
using Wms.Web.Common.Exceptions;
using Wms.Web.Repositories.Abstract;
using Wms.Web.Services.Abstract;
using Wms.Web.Store.Entities;
using Wms.Web.Services.Dto;
using Wms.Web.Store.Specifications;

namespace Wms.Web.Services.Concrete;

internal sealed class WarehouseService : IWarehouseService
{
    private readonly IGenericRepository<Warehouse> _warehouseRepository;
    private readonly IMapper _mapper;

    public WarehouseService(
        IGenericRepository<Warehouse> warehouseRepository, 
        IMapper mapper)
    {
        _warehouseRepository = warehouseRepository;
        _mapper = mapper;
    }

    public async Task CreateAsync(WarehouseDto warehouseDto, CancellationToken ct = default)
    {
        var entity = await _warehouseRepository.GetByIdAsync(warehouseDto.Id, ct);

        if (entity is not null)
        {
            throw new EntityAlreadyExistException(entity.Id);
        }

        entity = _mapper.Map<Warehouse>(warehouseDto);
        await _warehouseRepository.CreateAsync(entity, ct);
    }

    public async Task<IReadOnlyCollection<WarehouseDto>?> GetAllAsync(int offset, int size, bool deleted, CancellationToken ct)
    {
        IEnumerable<Warehouse?> entities;

        switch (deleted)
        {
            case true:
                entities = await _warehouseRepository
                    .GetAllAsync(null,
                        q => q.Skip(offset).Deleted().Take(size).OrderBy(x => x.CreatedAt),
                        includeProperties: nameof(Warehouse.Palettes), cancellationToken: ct);
                break;
            
            case false: 
                entities = await _warehouseRepository
                    .GetAllAsync(null,
                        q => q.Skip(offset).NotDeleted().Take(size).OrderBy(x => x.CreatedAt),
                        includeProperties: nameof(Warehouse.Palettes), cancellationToken: ct);
                break;
        }

        return _mapper.Map<IReadOnlyCollection<WarehouseDto>>(entities);
    }

    public async Task<WarehouseDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _warehouseRepository.GetByIdAsync(id, nameof(Warehouse.Palettes), ct);

        return _mapper.Map<WarehouseDto>(entity);
    }

    public async Task UpdateAsync(WarehouseDto warehouseDto, CancellationToken ct = default)
    {
        var entity = await _warehouseRepository.GetByIdAsync(warehouseDto.Id, ct)
                     ?? throw new EntityNotFoundException(warehouseDto.Id);
        
        _mapper.Map(warehouseDto, entity);

        await _warehouseRepository.UpdateAsync(entity, ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        await _warehouseRepository.DeleteAsync(id, ct);
    }
}