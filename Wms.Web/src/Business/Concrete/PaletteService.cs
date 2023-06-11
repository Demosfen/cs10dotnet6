using AutoMapper;
using Microsoft.Extensions.Logging;
using Serilog;
using Wms.Web.Business.Abstract;
using Wms.Web.Business.Dto;
using Wms.Web.Common.Exceptions;
using Wms.Web.Repositories.Interfaces;
using Wms.Web.Store.Entities.Concrete;
using Wms.Web.Store.Entities.Specifications;

namespace Wms.Web.Business.Concrete;

internal sealed class PaletteService : IPaletteService
{
    private const int DefaultWeight = 30;

    private readonly IGenericRepository<Warehouse> _warehouseRepository;
    private readonly IGenericRepository<Palette> _paletteRepository;
    private readonly IGenericRepository<Box> _boxRepository;
    private readonly IMapper _mapper;

    public PaletteService(
        IGenericRepository<Warehouse> warehouseRepository,
        IGenericRepository<Palette> paletteRepository,  
        IGenericRepository<Box> boxRepository,
        IMapper mapper)
    {
        _warehouseRepository = warehouseRepository;
        _paletteRepository = paletteRepository;
        _boxRepository = boxRepository;
        _mapper = mapper;
    }

    public async Task CreateAsync(PaletteDto paletteDto, CancellationToken cancellationToken)
    {
        var warehouse = await _warehouseRepository.GetByIdAsync(paletteDto.WarehouseId, cancellationToken); 
        
        if (warehouse == null)
        {
            Log.Logger.Error("Warehouse does not exist: id={WarehouseId}", paletteDto.WarehouseId);
            
            throw new EntityNotFoundException(paletteDto.WarehouseId);
        }

        if (warehouse.DeletedAt is not null)
        {
            Log.Logger.Error("Warehouse was deleted: id={WarehouseId}", paletteDto.WarehouseId);
            
            throw new EntityWasDeletedException(paletteDto.WarehouseId);
        }

        if (await _paletteRepository.GetByIdAsync(paletteDto.Id, cancellationToken) is not null)
        {
            Log.Logger.Error("Palette already exist: id={Palette}", paletteDto.Id);

            throw new EntityAlreadyExistException(paletteDto.Id);
        }
        
        paletteDto.Weight = DefaultWeight;
        paletteDto.Volume = paletteDto.Width * paletteDto.Height * paletteDto.Depth;
        
        var palette = _mapper.Map<Palette>(paletteDto);

        await _paletteRepository.CreateAsync(palette, cancellationToken);
        
        Log.Logger.Information("Palette was created: \n{Palette}", 
            palette.ToString());
    }

    public async Task<IReadOnlyCollection<PaletteDto>> GetAllAsync(
        Guid id, 
        int offset, int size,
        bool deleted,
        CancellationToken cancellationToken)
    {
        IEnumerable<Palette?> entities;

        if (deleted)
        {
            entities = await _paletteRepository
                .GetAllAsync(
                    x => x.WarehouseId == id,
                    q => q.Deleted().Skip(offset).Take(size).OrderBy(p => p.CreatedAt),
                    cancellationToken: cancellationToken);
        }
        else
        {
            entities = await _paletteRepository
                .GetAllAsync(
                    x => x.WarehouseId == id,
                    q => q.NotDeleted().Skip(offset).Take(size).OrderBy(p => p.CreatedAt),
                    cancellationToken: cancellationToken);
        }
        
        var palettes = _mapper.Map<IReadOnlyCollection<PaletteDto>>(entities);
        
        Log.Logger.Information("Got {Count} palettes", palettes.Count);

        return palettes;
    }

    public async Task<PaletteDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _paletteRepository.GetByIdAsync(id, cancellationToken);
            
            if (entity == null)
            {
                Log.Logger.Error("Palette wasn't found: id={Id}", id);
                
                throw new EntityNotFoundException(id);
            }

        return _mapper.Map<PaletteDto>(entity);
    }

    public async Task RefreshAsync(Guid id, CancellationToken cancellationToken)
    {
        var boxDto = await _boxRepository.GetAllAsync(
            b => b.PaletteId == id,
            q => q.NotDeleted().OrderBy(b => b.CreatedAt),
            cancellationToken: cancellationToken);

        var paletteDto = await _paletteRepository.GetByIdAsync(id, cancellationToken);
        
        if (paletteDto == null)
        {
            Log.Logger.Error("Refresh: Palette wasn't found: id={Id}", id);
            
            throw new EntityNotFoundException(id);
        }

        paletteDto.Weight = DefaultWeight;
        paletteDto.Volume = paletteDto.Width * paletteDto.Height * paletteDto.Depth;

        var boxesDto = boxDto.ToList();
        
        foreach (var box in boxesDto.ToList())
        {
            paletteDto.Weight += box.Weight;
            paletteDto.Volume += box.Volume;
        }

        paletteDto.ExpiryDate = boxesDto.Min(b => b.ExpiryDate);

        await _paletteRepository.UpdateAsync(_mapper.Map<Palette>(paletteDto), cancellationToken);
        
        Log.Logger.Information(
            "Refresh: Palette was updated: \n {Palette}", 
            paletteDto.ToString());
    }

    public async Task UpdateAsync(PaletteDto paletteDto, CancellationToken cancellationToken)
    {
        var palette = await _paletteRepository.GetByIdAsync(
            paletteDto.Id,
            includeProperties: nameof(paletteDto.Boxes),
            cancellationToken: cancellationToken);
        
        if (palette is null)
        {
            Log.Logger.Error("Palette wasn't found: id={Id}", paletteDto.Id);
            
            throw new EntityNotFoundException(paletteDto.Id);
        }

        if (palette.DeletedAt is not null)
        {
            Log.Logger.Error("Palette was deleted: id={Id}", paletteDto.Id);
            
            throw new EntityWasDeletedException(palette.Id);
        }
        
        if (palette.Boxes.Any())
        {
            Log.Logger.Error("Palette is not empty: id={Id}", palette.Id);
        
            throw new EntityNotEmptyException(palette.Id);
        }

        var warehouse = await _warehouseRepository.GetByIdAsync(paletteDto.WarehouseId, cancellationToken);
            
            if (warehouse is null)
            {
                Log.Logger.Error("Warehouse wasn't found: id={Id}", paletteDto.WarehouseId);
                
                throw new EntityNotFoundException(palette.WarehouseId);
            }

        if (warehouse.DeletedAt is not null)
        {
            Log.Logger.Error("Warehouse was deleted: id={Id}", warehouse.Id);
            
            throw new EntityWasDeletedException(warehouse.Id);
        }

        paletteDto.Weight = DefaultWeight;
        paletteDto.Volume = paletteDto.Width * paletteDto.Height * paletteDto.Depth;
        
        _mapper.Map(paletteDto, palette);

        await _paletteRepository.UpdateAsync(palette, cancellationToken);
        
        Log.Logger.Information(
            "Palette was updated: \n {Palette}", 
            palette.ToString());
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var palette = await _paletteRepository.GetByIdAsync(id, cancellationToken);
            
        if(palette is null)
        {
            Log.Logger.Error("Palette wasn't found: id={Id}", id);
            
            throw new EntityNotFoundException(id);
        }

        if (palette.DeletedAt is not null)
        {
            Log.Logger.Warning("Palette already deleted: id={Id}. Return", id);
            
            return;
        }

        var box = await _boxRepository.GetAllAsync(
            f => f.PaletteId == id,
            q => q.NotDeleted().Take(1).OrderBy(b => b.CreatedAt),
            cancellationToken: cancellationToken);

        if (box.Any())
        {
            Log.Logger.Error("Palette not empty: id={Id}", id);
            
            throw new EntityNotEmptyException(id);
        }

        await _paletteRepository.DeleteAsync(id, cancellationToken);
        
        Log.Logger.Error("Success. Palette was deleted: id={Id}", id);
    }
}