using AutoMapper;
using Wms.Web.Api.Contracts.Responses;
using Wms.Web.Services.Abstract;
using Wms.Web.Api.Contracts.Requests;
using Wms.Web.Services.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Wms.Web.Api.Controllers;

[ApiController]
[Route("api/v1/warehouses/")]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public sealed class PaletteController : ControllerBase
{
    // private readonly IWarehouseService _warehouseService;
    private readonly IPaletteService _paletteService;
    private readonly IMapper _mapper;

    public PaletteController(
        IWarehouseService warehouseService,
        IPaletteService paletteService, 
        IMapper mapper)
    {
        //_warehouseService = warehouseService;
        _paletteService = paletteService;
        _mapper = mapper;
    }
    
    [HttpGet("{warehouseId:guid}/palettes/", Name = "GetNotDeletedPalettesFromDb")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyCollection<PaletteResponse>>> GetAllNotDeleted(
        [FromRoute] Guid warehouseId, int offset = 0, int limit = 100)
    {
        var palettesDto = await _paletteService
            .GetAllAsync(warehouseId, offset, limit);
        
        var paletteResponse = _mapper.Map<IReadOnlyCollection<PaletteResponse>>(palettesDto);

        return Ok(paletteResponse);
    }
    
    [HttpGet("{warehouseId:guid}/palettes/archive/", Name = "GetDeletedPalettesFromDb")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyCollection<PaletteResponse>>> GetAllDeleted(
        [FromRoute] Guid warehouseId, int offset = 0, int limit = 100)
    {
        var palettesDto = await _paletteService
            .GetAllAsync(warehouseId, offset, limit, true);
        
        var paletteResponse = _mapper.Map<IReadOnlyCollection<PaletteResponse>>(palettesDto);

        return Ok(paletteResponse);
    }
    
    [HttpGet("palettes/{paletteId}", Name = "GetPaletteById")]
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

    [HttpPost("{warehouseId:guid}/palettes/{paletteId:guid}", Name = "CreatePalette")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<PaletteResponse>> CreateAsync(
        [FromRoute] Guid paletteId, 
        [FromRoute] Guid warehouseId, 
        [FromBody] CreatePaletteRequest request)
    {
        if (await _paletteService.GetByIdAsync(paletteId) != null)
        {
            return Conflict("Palette with the same id already exist.");
        }
        
        var paletteDto = _mapper.Map<PaletteDto>(request);

        paletteDto.Id = paletteId;
        paletteDto.WarehouseId = warehouseId;

        await _paletteService.CreateAsync(paletteDto);

        var response = _mapper.Map<PaletteResponse>(paletteDto);
        
        return Created("Palette created:", response);
    }
    
    [HttpDelete("palettes/{paletteId}", Name = "DeletePalette")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _paletteService.DeleteAsync(id);
    
        return Ok();
    }
}