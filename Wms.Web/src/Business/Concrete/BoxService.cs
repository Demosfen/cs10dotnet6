using AutoMapper;
using Serilog;
using Wms.Web.Business.Abstract;
using Wms.Web.Business.Dto;
using Wms.Web.Business.Extensions;
using Wms.Web.Common.Exceptions;
using Wms.Web.Repositories.Interfaces;
using Wms.Web.Store.Entities.Concrete;
using Wms.Web.Store.Entities.Specifications;

namespace Wms.Web.Business.Concrete;

internal sealed class BoxService : IBoxService
{
    private readonly IPaletteService _paletteService;
    
    private readonly IGenericRepository<Box> _boxRepository;
    private readonly IGenericRepository<Palette> _paletteRepository;
    
    private readonly IMapper _mapper;

    public BoxService(
        IPaletteService paletteService,
        IGenericRepository<Box> boxRepository, 
        IGenericRepository<Palette> paletteRepository, 
        IMapper mapper)
    {
        _paletteService = paletteService;
        _boxRepository = boxRepository;
        _paletteRepository = paletteRepository;
        _mapper = mapper;
    }
    
    /// <inheritdoc />
    public async Task<IReadOnlyCollection<BoxDto>> GetAllAsync(
        Guid id,
        int offset, 
        int size,
        bool deleted,
        CancellationToken cancellationToken)
    {
        IEnumerable<Box?> entities;

        if (deleted)
        {
            entities = await _boxRepository
                .GetAllAsync(
                    x => x.PaletteId == id,
                    q => q.Deleted().Skip(offset).Take(size).OrderBy(p => p.CreatedAt),
                    cancellationToken: cancellationToken);
        }
        else
        {
            entities = await _boxRepository
                .GetAllAsync(
                    x => x.PaletteId == id,
                    q => q.NotDeleted().Skip(offset).Take(size).OrderBy(p => p.CreatedAt),
                    cancellationToken: cancellationToken);
        }
        
        var boxDto = _mapper.Map<IReadOnlyCollection<BoxDto>>(entities);
        
        Log.Logger.Information("Got {Count} boxes", boxDto.Count);

        return boxDto;
    }

    /// <inheritdoc />
    public async Task CreateAsync(
        BoxDto boxDto, 
        CancellationToken cancellationToken)
    {
        var box = await _boxRepository.GetByIdAsync(boxDto.Id, cancellationToken);
        
        if (box != null)
        {
            Log.Logger.Error("Box already exist: id={Id}", boxDto.Id);
            throw new EntityAlreadyExistException(boxDto.Id);
        }

        var palette = await _paletteRepository
            .GetByIdAsync(boxDto.PaletteId, cancellationToken);
        
        if (palette == null)
        {
            Log.Logger.Error("Palette not found: id={Id}", boxDto.PaletteId);
            
            throw new EntityNotFoundException(boxDto.PaletteId);
        }

        if (palette.DeletedAt is not null)
        {
            Log.Logger.Error("Palette was deleted: id={Id}", boxDto.PaletteId);
            
            throw new EntityWasDeletedException(palette.Id);
        }

        boxDto.Validate(palette);

        boxDto.Volume = boxDto.Width * boxDto.Height * boxDto.Depth;   

        box = _mapper.Map<Box>(boxDto);

        await _boxRepository.CreateAsync(box, cancellationToken);

        await _paletteService.RefreshAsync(box.PaletteId, cancellationToken);
        
        Log.Logger.Information("Box created: \n{Box}", box.ToString());
    }

    /// <inheritdoc />
    public async Task<BoxDto?> GetByIdAsync(
        Guid id, 
        CancellationToken cancellationToken)
    {
        var box = await _boxRepository.GetByIdAsync(id, cancellationToken) 
            ?? throw new EntityNotFoundException(id);

        return _mapper.Map<BoxDto>(box);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(
        BoxDto boxDto, 
        CancellationToken cancellationToken)
    {
        var box = await _boxRepository.GetByIdAsync(boxDto.Id, cancellationToken);
        
        if (box == null)
        {
            Log.Logger.Error("Box wasn't found. ID={Id}", boxDto.Id);
            throw new EntityNotFoundException(boxDto.Id);
        }
        
        if (box.DeletedAt is not null)
        {
            Log.Logger.Error("Box was deleted. ID={BoxId}", box.Id);
            
            throw new EntityWasDeletedException(box.Id);
        }

        var oldPalette = box.PaletteId;
        
        var palette = await _paletteRepository.GetByIdAsync(boxDto.PaletteId, cancellationToken);
            
        if (palette == null)
        {
            Log.Logger.Error("Palette wasn't found. ID={PaletteId}", boxDto.PaletteId);
                
            throw new EntityNotFoundException(boxDto.PaletteId);
        }
        
        if (palette.DeletedAt is not null)
        {
            Log.Logger.Error("Palette was deleted. ID={PaletteId}", palette.Id);
            
            throw new EntityWasDeletedException(palette.Id);
        }
        
        boxDto.Validate(palette);
        
        boxDto.Volume = boxDto.Width * boxDto.Height * boxDto.Depth;
        
        _mapper.Map(boxDto, box);
        
        await _boxRepository.UpdateAsync(box, cancellationToken);

        await _paletteService.RefreshAsync(palette.Id, cancellationToken);

        await _paletteService.RefreshAsync(oldPalette, cancellationToken);
        
        Log.Logger.Information("Box updated: \n {Box}", box.ToString());
    }

    /// <inheritdoc />
    public async Task DeleteAsync(
        Guid id, 
        CancellationToken cancellationToken)
    {
        var box = await _boxRepository.GetByIdAsync(id, cancellationToken);
        
        if (box == null)
        {
            Log.Logger.Error("Box wasn't found. ID={Id}", id);
            throw new EntityNotFoundException(id);
        }
        
        if (box.DeletedAt is not null)
        {
            Log.Logger.Warning("Box id={Id} already deleted. Return", id);
            
            return;
        }

        await _boxRepository.DeleteAsync(id, cancellationToken);

        await _paletteService.RefreshAsync(box.PaletteId, cancellationToken);
        
        Log.Logger.Information("Box id={Id} deleted", id);
    }
}