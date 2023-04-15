using AutoMapper;
using Wms.Web.Common.Exceptions;
using Wms.Web.Repositories.Abstract;
using Wms.Web.Services.Abstract;
using Wms.Web.Services.Dto;
using Wms.Web.Store.Entities;

namespace Wms.Web.Services.Concrete;

public class BoxService : IBoxService
{
    private const int ExpiryDays = 100;
    
    private readonly IGenericRepository<Box> _boxRepository;
    private readonly IGenericRepository<Palette> _paletteRepository;
    private readonly IMapper _mapper;

    public BoxService(
        IGenericRepository<Box> boxRepository, 
        IGenericRepository<Palette> paletteRepository, 
        IMapper mapper)
    {
        _boxRepository = boxRepository;
        _paletteRepository = paletteRepository;
        _mapper = mapper;
    }

    public async Task CreateAsync(BoxDto boxDto, CancellationToken ct = default)
    {
        var paletteDto = await _paletteRepository.GetByIdAsync(boxDto.PaletteId, ct)
                         ?? throw new EntityNotFoundException(boxDto.PaletteId);
        
        if (boxDto.Width > paletteDto.Width 
            | boxDto.Height > paletteDto.Height 
            | boxDto.Depth > paletteDto.Depth)
        {
            throw new UnitOversizeException(boxDto.Id);
        }
        
        if (boxDto.ProductionDate != null)
        {
            boxDto.ExpiryDate ??= boxDto.ProductionDate.Value.AddDays(ExpiryDays);
        }
        else
        {
            if (boxDto.ExpiryDate == null)
            {
                throw new InvalidOperationException("Both Production and Expiry date should not be null");
            }
        }
        
        if (boxDto.ExpiryDate <= boxDto.ProductionDate)
        {
            throw new ArgumentException(
                "Expiry date cannot be lower than Production date!");
        }

        boxDto.Volume = boxDto.Width * boxDto.Height * boxDto.Depth;   

        var box = _mapper.Map<Box>(boxDto);

        await _boxRepository.CreateAsync(box, ct);
        
        paletteDto.Weight += boxDto.Weight;
        paletteDto.Volume += boxDto.Volume;
        paletteDto.Boxes?.Add(box);
        paletteDto.ExpiryDate = paletteDto.Boxes?.Min(x => x.ExpiryDate);

        var palette = _mapper.Map<Palette>(paletteDto);

        await _paletteRepository.UpdateAsync(palette, ct);
    }

    public async Task<IReadOnlyCollection<BoxDto>?> GetAllAsync(CancellationToken ct = default)
    {
        var boxes = await _boxRepository.GetAllAsync(cancellationToken: ct);

        return _mapper.Map<IReadOnlyCollection<BoxDto>>(boxes);
    }

    public async Task<BoxDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var box = await _boxRepository.GetByIdAsync(id, ct);

        var boxDto = _mapper.Map<BoxDto>(box);

        return boxDto;
    }

    public async Task UpdateAsync(BoxDto boxDto, CancellationToken ct = default)
    {
        var box = _mapper.Map<Box>(boxDto);
        
        await _boxRepository.UpdateAsync(box, ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var boxDto = await _boxRepository.GetByIdAsync(id, ct);

        if (boxDto != null)
        {
            var paletteDto = await _paletteRepository.GetByIdAsync(boxDto.PaletteId, ct)
                             ?? throw new EntityNotFoundException(boxDto.PaletteId);
        
            await _boxRepository.DeleteAsync(id, ct);
        
            paletteDto.Weight -= boxDto.Weight;
            paletteDto.Volume -= boxDto.Volume;
            paletteDto.Boxes?.Remove(boxDto);
            paletteDto.ExpiryDate = paletteDto.Boxes?.Min(x => x.ExpiryDate);

            var palette = _mapper.Map<Palette>(paletteDto);

            await _paletteRepository.UpdateAsync(palette, ct);
        }
    }
}