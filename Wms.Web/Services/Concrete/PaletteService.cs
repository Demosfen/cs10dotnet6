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
    
    //TODO implement ILogger https://github.com/Demosfen/cs10dotnet6/pull/6#discussion_r1173487786

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
        var warehouse = await _warehouseRepository.GetByIdAsync(paletteDto.WarehouseId, cancellationToken)
            ?? throw new EntityNotFoundException(paletteDto.WarehouseId);

        if (warehouse.DeletedAt is not null)
        {
            throw new EntityWasDeletedException(paletteDto.WarehouseId);
        }

        if (await _paletteRepository.GetByIdAsync(paletteDto.Id, cancellationToken) is not null)
        {
            throw new EntityAlreadyExistException(paletteDto.Id);
        }
        
        paletteDto.Weight = DefaultWeight;
        paletteDto.Volume = paletteDto.Width * paletteDto.Height * paletteDto.Depth;
        
        var entity = _mapper.Map<Palette>(paletteDto);

        await _paletteRepository.CreateAsync(entity, cancellationToken);
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

    public async Task<PaletteDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _paletteRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new EntityNotFoundException(id);

        return _mapper.Map<PaletteDto>(entity);
    }

    public async Task RefreshAsync(Guid id, CancellationToken cancellationToken)
    {
        var boxDto = await _boxRepository.GetAllAsync(
            b => b.PaletteId == id,
            q => q.NotDeleted().OrderBy(b => b.CreatedAt),
            cancellationToken: cancellationToken);

        var paletteDto = await _paletteRepository.GetByIdAsync(id, cancellationToken)
                         ?? throw new EntityNotFoundException(id);

        paletteDto.Weight = 30;
        paletteDto.Volume = paletteDto.Width * paletteDto.Height * paletteDto.Depth;

        var boxesDto = boxDto.ToList();
        
        foreach (var box in boxesDto.ToList())
        {
            paletteDto.Weight += box.Weight;
            paletteDto.Volume += box.Volume;
        }

        paletteDto.ExpiryDate = boxesDto.Min(b => b.ExpiryDate);

        await _paletteRepository.UpdateAsync(_mapper.Map<Palette>(paletteDto), cancellationToken);
    }

    public async Task UpdateAsync(PaletteDto paletteDto, CancellationToken cancellationToken)
    {
        var palette = await _paletteRepository.GetByIdAsync(
                         paletteDto.Id,
                         includeProperties: nameof(paletteDto.Boxes),
                             cancellationToken: cancellationToken)
                     ?? throw new EntityNotFoundException(paletteDto.Id);

        if (palette.DeletedAt is not null)
        {
            throw new EntityWasDeletedException(palette.Id);
        }
        
        if (palette.Boxes.Count != 0)
        {
            throw new EntityNotEmptyException(palette.Id);
        }

        var warehouse = await _warehouseRepository.GetByIdAsync(paletteDto.WarehouseId, cancellationToken)
            ?? throw new EntityNotFoundException(palette.WarehouseId);

        if (warehouse.DeletedAt is not null)
        {
            throw new EntityWasDeletedException(warehouse.Id);
        }

        paletteDto.Weight = DefaultWeight;
        paletteDto.Volume = paletteDto.Width * paletteDto.Height * paletteDto.Depth;
        
        _mapper.Map(paletteDto, palette);

        await _paletteRepository.UpdateAsync(palette, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var palette = await _paletteRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new EntityNotFoundException(id);

        if (palette.DeletedAt is not null) return;

        var box = await _boxRepository.GetAllAsync(
            f => f.PaletteId == id,
            q => q.NotDeleted().Take(1).OrderBy(b => b.CreatedAt),
            cancellationToken: cancellationToken);

        if (!box.Count().Equals(0))
        {
            throw new EntityNotEmptyException(id);
        }

        await _paletteRepository.DeleteAsync(id, cancellationToken);
    }
}