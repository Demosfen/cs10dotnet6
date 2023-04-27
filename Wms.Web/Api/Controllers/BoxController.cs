using AutoMapper;
using Wms.Web.Api.Contracts.Responses;
using Wms.Web.Services.Abstract;
using Wms.Web.Api.Contracts.Requests;
using Wms.Web.Services.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Wms.Web.Api.Controllers;

[ApiController]
[Route("api/v1/warehouses/palettes/")]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public sealed class BoxController : ControllerBase
{
    private readonly IPaletteService _paletteService;
    private readonly IBoxService _boxService;
    private readonly IMapper _mapper;

    public BoxController(
        IPaletteService paletteService, 
        IBoxService boxService, 
        IMapper mapper)
    {
        _paletteService = paletteService;
        _boxService = boxService;
        _mapper = mapper;
    }
    
    [HttpGet("{paletteId}/boxes", Name = "GetAllBoxes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<BoxResponse>>> GetNotDeletedAsync(
        [FromRoute] Guid paletteId,
        int offset = 0, 
        int size = 10,
        CancellationToken cancellationToken = default)
    {
        var boxesDto = await _boxService
            .GetAllAsync(paletteId, offset, size, false, cancellationToken);
        
        var boxResponse = _mapper.Map<IReadOnlyCollection<BoxResponse>>(boxesDto);

        return Ok(boxResponse);
    }

    [HttpGet("{paletteId}/boxes/archive", Name = "GetDeletedBoxes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<BoxResponse>>> GetDeletedAsync(
        [FromRoute] Guid paletteId,
        int offset = 0,
        int size = 10,
        CancellationToken cancellationToken = default)
    {
        var boxDto = await _boxService
            .GetAllAsync(paletteId, offset, size, true, cancellationToken);
       
        var boxResponse = _mapper.Map<IReadOnlyCollection<BoxResponse>>(boxDto);
    
        return Ok(boxResponse);
    }
    
    [HttpGet("boxes/{boxId}", Name = "GetBoxById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BoxResponse>> GetByIdAsync(
        [FromRoute] Guid boxId, 
        CancellationToken cancellationToken = default)
    {
        var boxDto = await _boxService.GetByIdAsync(boxId, cancellationToken);
    
        var boxResponse = _mapper.Map<BoxResponse>(boxDto);
        
        return Ok(boxResponse);
    }

    [HttpPost("{paletteId:guid}/boxes/{boxId:guid}", Name = "CreateBox")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<BoxResponse>> CreateAsync(
        [FromRoute] Guid boxId,
        [FromRoute] Guid paletteId,
        [FromBody] BoxRequest request,
        CancellationToken cancellationToken = default)
    {
        if (await _boxService.GetByIdAsync(boxId, cancellationToken) != null)
        {
            return Conflict("Box with the same id already exist.");
        }
        
        if (await _paletteService.GetByIdAsync(paletteId, ct: cancellationToken) is null)
        {
            return NotFound($"Palette with id={paletteId} does not exist");
        }
    
        var createBox = new CreateBoxRequest
        {
            Id = boxId,
            PaletteId = paletteId,
            BoxRequest = request
        };

        var boxDto = _mapper.Map<BoxDto>(createBox);
    
        await _boxService.CreateAsync(boxDto, cancellationToken);
    
        var response = _mapper.Map<BoxResponse>(boxDto);
        
        return Created("Box created!", response);
    }
    
    // [HttpDelete("{boxId:guid}", Name = "DeleteBox")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // public async Task<IActionResult> DeleteAsync([FromRoute] Guid boxId)
    // {
    //     await _boxService.DeleteAsync(boxId);
    //
    //     return Ok();
    // }
}