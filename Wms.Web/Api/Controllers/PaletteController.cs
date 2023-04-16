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
public sealed class PaletteController : ControllerBase
{
    private readonly IPaletteService _paletteService;
    private readonly IMapper _mapper;

    public PaletteController(
        IPaletteService paletteService, 
        IMapper mapper)
    {
        _paletteService = paletteService;
        _mapper = mapper;
    }
    
    [HttpGet(Name = "GetAllPalettesFromDb")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyCollection<PaletteResponse>>> GetAll()
    {
        var palettesDto = await _paletteService.GetAllAsync();
        var paletteResponse = _mapper.Map<IReadOnlyCollection<PaletteResponse>>(palettesDto);

        return Ok(paletteResponse);
    }
    
    [HttpGet("{paletteId}", Name = "GetPaletteById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PaletteResponse>> Get([FromRoute] Guid paletteId)
    {
        var paletteDto = await _paletteService.GetByIdAsync(paletteId);

        if (paletteDto is null)
        {
            return NotFound();
        }

        var paletteResponse = _mapper.Map<PaletteResponse>(paletteDto);
        return Ok(paletteResponse);
    }

    [HttpPost("{id:guid}", Name = "CreatePalette")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<PaletteResponse>> CreateAsync([FromRoute] Guid id, [FromBody] CreatePaletteRequest request)
    {
        if (await _paletteService.GetByIdAsync(id) != null)
        {
            return Conflict("Palette with the same id already exist.");
        }
        
        var paletteDto = _mapper.Map<PaletteDto>(request);

        paletteDto.Id = id;

        await _paletteService.CreateAsync(paletteDto);

        var response = _mapper.Map<PaletteResponse>(paletteDto);
        
        return Created("Palette created:", response);
    }
    
    [HttpDelete("{id:guid}", Name = "DeletePalette")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _paletteService.DeleteAsync(id);
    
        return Ok();
    }
}