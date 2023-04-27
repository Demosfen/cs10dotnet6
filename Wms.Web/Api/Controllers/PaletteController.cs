using System.Text;
using AutoMapper;
using Wms.Web.Api.Contracts.Responses;
using Wms.Web.Services.Abstract;
using Wms.Web.Api.Contracts.Requests;
using Wms.Web.Services.Dto;
using Microsoft.AspNetCore.Mvc;
using Wms.Web.Common.Exceptions;
using Wms.Web.Repositories.Abstract;
using Wms.Web.Store.Entities;

namespace Wms.Web.Api.Controllers;

[ApiController]
[Route("api/v1/warehouses/")]
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
    
    [HttpGet("{warehouseId:guid}/palettes/", Name = "GetNotDeletedPalettes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyCollection<PaletteResponse>>> GetAllNotDeleted(
        [FromRoute] Guid warehouseId, int offset = 0, int limit = 10)
    {
        var palettesDto = await _paletteService
            .GetAllAsync(warehouseId, offset, limit);
        
        var paletteResponse = _mapper.Map<IReadOnlyCollection<PaletteResponse>>(palettesDto);

        return Ok(paletteResponse);
    }
    
    [HttpGet("{warehouseId:guid}/palettes/archive/", Name = "GetDeletedPalettes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyCollection<PaletteResponse>>> GetAllDeleted(
        [FromRoute] Guid warehouseId, int offset = 0, int limit = 10)
    {
        var palettesDto = await _paletteService
            .GetAllAsync(warehouseId, offset, limit, true);
        
        var paletteResponse = _mapper.Map<IReadOnlyCollection<PaletteResponse>>(palettesDto);

        return Ok(paletteResponse);
    }
    
    [HttpGet("palettes/{paletteId}", Name = "GetPaletteById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PaletteResponse>> GetByIdAsync(
        [FromRoute] Guid paletteId, int boxListoffset = 0, int boxListSize = 10)
    {
        var paletteDto = await _paletteService.GetByIdAsync(
            paletteId, boxListoffset, boxListSize);
    
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
    
    [HttpPut("palettes/{paletteId:guid}", Name = "UpdatePalette")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WarehouseResponse>> UpdateAsync(
        [FromRoute] Guid paletteId, [FromBody] UpdatePaletteRequest request)
    {
        var paletteDto = _mapper.Map<PaletteDto>(request);

        paletteDto.Id = paletteId;

        await _paletteService.UpdateAsync(paletteDto);

        var paletteResponse = _mapper.Map<PaletteResponse>(paletteDto);
        return Ok(paletteResponse);
    }
    
    [HttpDelete("palettes/{paletteId}", Name = "DeletePalette")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(Guid paletteId)
    {
        var paletteDto = await _paletteService.GetByIdAsync(paletteId);

        if (paletteDto?.Boxes?.Count != 0)
        {
            return BadRequest("Warehouse is not empty! Remove palettes first. Return.");
        } 
        
        await _paletteService.DeleteAsync(paletteId);

        return Ok("Palette deleted");
    }
}