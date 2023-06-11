using AutoMapper;
using Serilog;
using Wms.Web.Business.Abstract;
using Wms.Web.Business.Dto;
using Wms.Web.Common.Exceptions;
using Wms.Web.Repositories.Interfaces;
using Wms.Web.Store.Entities.Concrete;
using Wms.Web.Store.Entities.Specifications;

namespace Wms.Web.Business.Concrete;

internal sealed class WarehouseService : IWarehouseService
{
    private readonly IGenericRepository<Warehouse> _warehouseRepository;
    private readonly IGenericRepository<Palette> _paletteRepository;
    private readonly IMapper _mapper;

    public WarehouseService(
        IGenericRepository<Warehouse> warehouseRepository, 
        IGenericRepository<Palette> paletteRepository, 
        IMapper mapper)
    {
        _warehouseRepository = warehouseRepository;
        _paletteRepository = paletteRepository;
        _mapper = mapper;
    }

    public async Task CreateAsync(
        WarehouseDto warehouseDto, 
        CancellationToken cancellationToken)
    {
        if (await _warehouseRepository.GetByIdAsync(warehouseDto.Id, cancellationToken) != null)
        {
            throw new EntityAlreadyExistException(warehouseDto.Id);
        }

        await _warehouseRepository.CreateAsync(_mapper.Map<Warehouse>(warehouseDto), cancellationToken);
                
        Log.Logger.Information("Warehouse created: \n {Warehouse}", warehouseDto.ToString());
    }

    public async Task<IReadOnlyCollection<WarehouseDto>?> GetAllAsync(
        int offset, 
        int size, 
        bool deleted, 
        CancellationToken cancellationToken)
    {
        IEnumerable<Warehouse?> entities;

        if (deleted)
        {
            entities = await _warehouseRepository
                .GetAllAsync(null,
                    q => q.Deleted().Skip(offset).Take(size).OrderBy(x => x.CreatedAt),
                    cancellationToken: cancellationToken);
        }
        else
        {
            entities = await _warehouseRepository
                                .GetAllAsync(null,
                                    q => q.NotDeleted().Skip(offset).Take(size).OrderBy(x => x.CreatedAt),
                                    cancellationToken: cancellationToken);
        }
        
        var warehouses = _mapper.Map<IReadOnlyCollection<WarehouseDto>>(entities);
        
        Log.Logger.Information("Got {Count} palettes", warehouses.Count);

        return warehouses;
    }

    public async Task<WarehouseDto?> GetByIdAsync(
        Guid id, 
        CancellationToken cancellationToken)
    {
        var entity = await _warehouseRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new EntityNotFoundException(id);

        return _mapper.Map<WarehouseDto>(entity);
    }

    public async Task UpdateAsync(
        WarehouseDto warehouseDto, 
        CancellationToken cancellationToken)
    {
        var warehouse = await _warehouseRepository.GetByIdAsync(warehouseDto.Id, cancellationToken)
                     ?? throw new EntityNotFoundException(warehouseDto.Id);

        if (warehouse.DeletedAt is not null)
        {
            Log.Logger.Error("Warehouse was deleted: id={Id}", warehouse.Id);
            
            throw new EntityWasDeletedException(warehouse.Id);
        }
        
        _mapper.Map(warehouseDto, warehouse);

        await _warehouseRepository.UpdateAsync(warehouse, cancellationToken);
        
        Log.Logger.Information(
            "Warehouse was updated: \n {Palette}", 
            warehouse.ToString());
    }

    public async Task DeleteAsync(
        Guid id, 
        CancellationToken cancellationToken)
    {
        var warehouse = await _warehouseRepository.GetByIdAsync(id, cancellationToken);
            
        if(warehouse is null)
        {
            Log.Logger.Error("Warehouse wasn't found: id={Id}", id);
            
            throw new EntityNotFoundException(id);
        }
        
        if (warehouse.DeletedAt is not null)
        {
            Log.Logger.Warning("Warehouse already deleted: id={Id}. Return", id);
            
            return;
        }

        var palette = await _paletteRepository.GetAllAsync(
            f => f.WarehouseId == warehouse.Id,
            q => q.NotDeleted().Take(1).OrderBy(x => x.CreatedAt),
            cancellationToken: cancellationToken);

        if (palette.Any())
        {
            Log.Logger.Error("Warehouse not empty: id={Id}", id);
            
            throw new EntityNotEmptyException(id);
        } 
        
        await _warehouseRepository.DeleteAsync(id, cancellationToken);
        Log.Logger.Information("Success. Warehouse was deleted: id={Id}", id);
    }
}