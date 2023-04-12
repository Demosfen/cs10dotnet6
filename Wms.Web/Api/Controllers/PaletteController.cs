using AutoMapper;
using Wms.Web.Api.Contracts.Responses;
using Wms.Web.Services.Abstract;
using Wms.Web.Api.Contracts.Requests;
using Wms.Web.Services.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Wms.Web.Api.Controllers;

[ApiController]
[Route("api/v1/palettes/")]
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
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(WarehouseDto))]
    public async Task<IActionResult> GetAll()
    {
        var palettes = await _paletteService.GetAllAsync();
        var paletteResponse = _mapper.Map<IReadOnlyCollection<WarehouseResponse>>(palettes);

        return Ok(paletteResponse);
    }
    
    [HttpGet("{warehouseId}", Name = "GetWarehouseById")]
    public async Task<IActionResult> Get([FromRoute] Guid warehouseId)
    {
        var warehouseDto = await _paletteService.GetByIdAsync(warehouseId);

        if (warehouseDto is null)
        {
            return NotFound();
        }

        var warehouseResponse = _mapper.Map<WarehouseResponse>(warehouseDto);
        return Ok(warehouseResponse);
    }

    [HttpPost("{id:guid}", Name = "CreateWarehouse")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(WarehouseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(WarehouseDto))]
    public async Task<IActionResult> CreateAsync([FromRoute] Guid id, [FromBody] CreateWarehouseRequest request)
    {
        if (await _paletteService.GetByIdAsync(id) != null)
        {
            return Conflict("Warehouse with the same id already exist.");
        }
        
        var warehouseDto = _mapper.Map<WarehouseDto>(request);

        warehouseDto.Id = id;

        await _paletteService.CreateAsync(palDto);

        var response = _mapper.Map<WarehouseResponse>(warehouseDto);
        
        return Created("Warehouse created:", warehouseDto);
    }
    
    [HttpPut("{id:guid}", Name = "UpdateWarehouse")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WarehouseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(UpdateWarehouseRequest))]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] CreateWarehouseRequest request)
    {
        var warehouseDto = _mapper.Map<WarehouseDto>(request);

        warehouseDto.Id = id;

        await _paletteService.UpdateAsync(warehouseDto);

        var response = _mapper.Map<WarehouseResponse>(warehouseDto);
        return Ok(response);
    }

    [HttpDelete("{id:guid}", Name = "DeleteWarehouse")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        await _paletteService.DeleteAsync(id);

        return Ok();
    }
}