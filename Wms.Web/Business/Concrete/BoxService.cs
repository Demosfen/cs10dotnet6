using AutoMapper;
using Wms.Web.Business.Abstract;
using Wms.Web.Business.Dto;
using Wms.Web.Business.Extensions;
using Wms.Web.Common.Exceptions;
using Wms.Web.Repositories.Interfaces.Interfaces;
using Wms.Web.Store.Entities.Concrete;
using Wms.Web.Store.Entities.Specifications;

namespace Wms.Web.Business.Concrete;

internal sealed class BoxService : IBoxService
{
    private const int ExpiryDays = 100;         //TODO Finish business logic

    private readonly IPaletteService _paletteService;
    
    //TODO implement ILogger https://github.com/Demosfen/cs10dotnet6/pull/6#discussion_r1173487786
    
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
        int offset, int size,
        bool deleted,
        CancellationToken cancellationToken)
    {
        IEnumerable<Box?> entities;

        switch (deleted)
        {
            case false:
                entities = await _boxRepository
                    .GetAllAsync(
                        x => x.PaletteId == id,
                        q => q.NotDeleted().Skip(offset).Take(size).OrderBy(p => p.CreatedAt),
                        cancellationToken: cancellationToken);
                break;
            
            case true:
                entities = await _boxRepository
                    .GetAllAsync(
                        x => x.PaletteId == id,
                        q => q.Deleted().Skip(offset).Take(size).OrderBy(p => p.CreatedAt),
                        cancellationToken: cancellationToken);
                break;
        }
        
        return _mapper.Map<IReadOnlyCollection<BoxDto>>(entities);
    }

    /// <inheritdoc />
    public async Task CreateAsync(BoxDto boxDto, CancellationToken cancellationToken)
    {
        var box = await _boxRepository.GetByIdAsync(boxDto.Id, cancellationToken);
        
        if (box != null)
        {
            throw new EntityAlreadyExistException(boxDto.Id);
        }
        
        var palette = await _paletteRepository
                          .GetByIdAsync(boxDto.PaletteId, cancellationToken)
                      ?? throw new EntityNotFoundException(boxDto.PaletteId);

        if (palette.DeletedAt is not null)
        {
            throw new EntityWasDeletedException(palette.Id);
        }
        
        BoxValidations.BoxSizeValidation(palette, boxDto);
        BoxValidations.BoxExpiryValidation(palette, boxDto);

        boxDto.Volume = boxDto.Width * boxDto.Height * boxDto.Depth;   

        box = _mapper.Map<Box>(boxDto);

        await _boxRepository.CreateAsync(box, cancellationToken);

        await _paletteService.RefreshAsync(box.PaletteId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<BoxDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var box = await _boxRepository.GetByIdAsync(id, cancellationToken) 
            ?? throw new EntityNotFoundException(id);

        return _mapper.Map<BoxDto>(box);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(BoxDto boxDto, CancellationToken cancellationToken)
    {
        var box = await _boxRepository.GetByIdAsync(boxDto.Id, cancellationToken)
                  ?? throw new EntityNotFoundException(boxDto.Id);

        if (box.DeletedAt is not null)
        {
            throw new EntityWasDeletedException(box.Id);
        }

        var oldPalette = box.PaletteId;
        
        var palette = await _paletteRepository.GetByIdAsync(boxDto.PaletteId, cancellationToken)
                      ?? throw new EntityNotFoundException(boxDto.PaletteId);
        
        if (palette.DeletedAt is not null)
        {
            throw new EntityWasDeletedException(palette.Id);
        }
        
        BoxValidations.BoxSizeValidation(palette, boxDto);
        BoxValidations.BoxExpiryValidation(palette, boxDto);
        
        boxDto.Volume = boxDto.Width * boxDto.Height * boxDto.Depth;
        
        _mapper.Map(boxDto, box);
        
        await _boxRepository.UpdateAsync(box, cancellationToken);

        await _paletteService.RefreshAsync(palette.Id, cancellationToken);

        await _paletteService.RefreshAsync(oldPalette, cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var box = await _boxRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new EntityNotFoundException(id);
        
        if (box.DeletedAt is not null) return;

        await _boxRepository.DeleteAsync(id, cancellationToken);

        await _paletteService.RefreshAsync(box.PaletteId, cancellationToken);
    }
}