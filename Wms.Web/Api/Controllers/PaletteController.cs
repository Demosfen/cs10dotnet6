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
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyCollection<PaletteResponse>>> GetAll()
    {
        var palettes = await _paletteService.GetAllAsync();
        var paletteResponse = _mapper.Map<IReadOnlyCollection<PaletteResponse>>(palettes);

        return Ok(paletteResponse);
    }
    
    [HttpGet("{paletteId}", Name = "GetPaletteById")]
    public async Task<IActionResult> Get([FromRoute] Guid paletteId)
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
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PaletteDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(PaletteDto))]
    public async Task<IActionResult> CreateAsync([FromRoute] Guid id, [FromBody] CreatePaletteRequest request)
    {
        if (await _paletteService.GetByIdAsync(id) != null)
        {
            return Conflict("Palette with the same id already exist.");
        }
        
        var paletteDto = _mapper.Map<PaletteDto>(request);

        paletteDto.Id = id;

        await _paletteService.CreateAsync(paletteDto);

        var response = _mapper.Map<PaletteResponse>(paletteDto);
        
        return Created("Palette created:", paletteDto);
    }
    
    // [HttpPut("{id:guid}", Name = "UpdateWarehouse")]
    // [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WarehouseDto))]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(UpdateWarehouseRequest))]
    // public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] CreateWarehouseRequest request)
    // {
    //     var warehouseDto = _mapper.Map<WarehouseDto>(request);
    //
    //     warehouseDto.Id = id;
    //
    //     await _paletteService.UpdateAsync(warehouseDto);
    //
    //     var response = _mapper.Map<WarehouseResponse>(warehouseDto);
    //     return Ok(response);
    // }
    //
    // [HttpDelete("{id:guid}", Name = "DeleteWarehouse")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // public async Task<ActionResult> DeleteAsync(Guid id)
    // {
    //     await _paletteService.DeleteAsync(id);
    //
    //     return Ok();
    // }
}