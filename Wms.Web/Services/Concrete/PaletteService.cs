using System.Text;
using AutoMapper;
using Wms.Web.Common.Exceptions;
using Wms.Web.Repositories.Abstract;
using Wms.Web.Services.Abstract;
using Wms.Web.Store.Entities;
using Wms.Web.Services.Dto;
using Wms.Web.Store.Specifications;

namespace Wms.Web.Services.Concrete;

internal sealed class PaletteService : IPaletteService
{
    private const int DefaultWeight = 30;

    private readonly IGenericRepository<Warehouse> _warehouseRepository;
    private readonly IGenericRepository<Palette> _paletteRepository;
    private readonly IMapper _mapper;

    public PaletteService(
        IGenericRepository<Warehouse> warehouseRepository,
        IGenericRepository<Palette> paletteRepository, 
        IMapper mapper)
    {
        _warehouseRepository = warehouseRepository;
        _paletteRepository = paletteRepository;
        _mapper = mapper;
    }

    public async Task CreateAsync(PaletteDto paletteDto, CancellationToken ct)
    {
        var warehouse = await _warehouseRepository.GetByIdAsync(paletteDto.WarehouseId, ct);

        if (warehouse is { DeletedAt: not null })
        {
            throw new EntityWasDeletedException(paletteDto.WarehouseId);
        }
        
        paletteDto.Weight = DefaultWeight;
        paletteDto.Volume = paletteDto.Width * paletteDto.Height * paletteDto.Depth;
        
        var entity = _mapper.Map<Palette>(paletteDto);

        await _paletteRepository.CreateAsync(entity, ct);
    }

    public async Task<IReadOnlyCollection<PaletteDto>> GetAllAsync(
        Guid id, 
        int offset, int size,
        bool deleted,
        CancellationToken ct)
    {
        IEnumerable<Palette?> entities;

        switch (deleted)
        {
            case false:
                entities = await _paletteRepository
                    .GetAllAsync(
                        x => x.WarehouseId == id,
                        q => q.NotDeleted().Skip(offset).Take(size).OrderBy(p => p.CreatedAt),
                        cancellationToken: ct);
                break;
            
            case true:
                entities = await _paletteRepository
                    .GetAllAsync(
                        x => x.WarehouseId == id,
                        q => q.Deleted().Skip(offset).Take(size).OrderBy(p => p.CreatedAt),
                        cancellationToken: ct);
                break;
        }
        
        return _mapper.Map<IReadOnlyCollection<PaletteDto>>(entities);
    }

    public async Task<PaletteDto?> GetByIdAsync(Guid id, int boxListOffset, int boxListSize, CancellationToken ct)
    {
        var entity = await _paletteRepository
            .GetByIdAsync(id, boxListOffset, boxListSize, nameof(Palette.Boxes), ct);

        return _mapper.Map<PaletteDto?>(entity);
    }

    public async Task RefreshAsync(Guid id, CancellationToken cancellationToken)
    {
        var palette = await _paletteRepository.GetByIdAsync(
            id,
            includeProperties: nameof(Palette.Boxes),
            cancellationToken: cancellationToken);

        if (palette != null)
        {
            palette.Weight = 30;
            palette.Volume = palette.Width * palette.Height * palette.Depth;
            
            foreach (var box in palette.Boxes)
            {
                palette.Weight += box.Weight;
                palette.Volume += box.Volume;
            }
            
            palette.ExpiryDate = palette.Boxes.Where(b => b.ExpiryDate.)
        } 
    }

    public async Task UpdateAsync(PaletteDto paletteDto, CancellationToken cancellationToken)
    {
        var palette = await _paletteRepository.GetByIdAsync(
                         paletteDto.Id,
                         includeProperties: nameof(paletteDto.Boxes),
                             cancellationToken: cancellationToken)
                     ?? throw new EntityNotFoundException(paletteDto.Id);

        var warehouse = await _warehouseRepository.GetByIdAsync(palette.WarehouseId, cancellationToken)
            ?? throw new EntityNotFoundException(palette.WarehouseId);

        if (warehouse.DeletedAt is not null)
        {
            throw new EntityWasDeletedException(warehouse.Id);
        }
        
        if (palette.DeletedAt is not null)
        {
            throw new EntityWasDeletedException(palette.Id);
        }

        if (palette.Boxes.Count != 0)
        {
            throw new EntityNotEmptyException(palette.Id);
        }
        
        paletteDto.Weight = DefaultWeight;
        paletteDto.Volume = paletteDto.Width * paletteDto.Height * paletteDto.Depth;
        
        _mapper.Map(paletteDto, palette);

        await _paletteRepository.UpdateAsync(palette, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var palette = await _paletteRepository.GetByIdAsync(id, ct);

        if (palette != null)
        {
            var warehouse = await _warehouseRepository.GetByIdAsync(palette.WarehouseId, ct);

            if (warehouse is { DeletedAt: not null })
            {
                throw new EntityWasDeletedException(palette.WarehouseId);
            }
        }

        await _paletteRepository.DeleteAsync(id, ct);
    }
}