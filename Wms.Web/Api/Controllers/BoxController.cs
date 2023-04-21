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
    
    [HttpGet(Name = "GetAllBoxesFromDb")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyCollection<BoxResponse>>> GetAllAsync()
    {
        var boxesDto = await _boxService.GetAllAsync();

        if (boxesDto is null)
        {
            return NotFound();
        }
        
        var boxResponse = _mapper.Map<IReadOnlyCollection<BoxResponse>>(boxesDto);

        return Ok(boxResponse);
    }
    
    [HttpGet("{boxId}", Name = "GetBoxById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BoxResponse>> GetAsync([FromRoute] Guid boxId)
    {
        var boxDto = await _boxService.GetByIdAsync(boxId);

        if (boxDto is null)
        {
            return NotFound();
        }

        var boxResponse = _mapper.Map<BoxResponse>(boxDto);
        return Ok(boxResponse);
    }

    [HttpPost("{id:guid}", Name = "CreateBox")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<BoxResponse>> CreateAsync([FromRoute] Guid id, [FromBody] CreateBoxRequest request)
    {
        if (await _boxService.GetByIdAsync(id) != null)
        {
            return Conflict("Box with the same id already exist.");
        }

        var boxDto = _mapper.Map<BoxDto>(request);
    
        boxDto.Id = id;
        
        if (await _paletteService.GetByIdAsync(boxDto.PaletteId) is null)
        {
            return NotFound($"Palette with id={boxDto.PaletteId} does not exist");
        }
    
        await _boxService.CreateAsync(boxDto);
    
        var response = _mapper.Map<BoxResponse>(boxDto);
        
        return Created("Box created", response);
    }
    
    [HttpDelete("{id:guid}", Name = "DeleteBox")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        await _boxService.DeleteAsync(id);
    
        return Ok();
    }
}