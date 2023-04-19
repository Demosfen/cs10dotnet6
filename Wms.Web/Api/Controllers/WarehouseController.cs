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
public sealed class WarehouseController : ControllerBase
{
    private readonly IWarehouseService _warehouseService;
    private readonly IMapper _mapper;

    public WarehouseController(
        IWarehouseService warehouseService, 
        IMapper mapper)
    {
        _warehouseService = warehouseService;
        _mapper = mapper;
    }
    
    [HttpGet(Name = "GetNotDeletedWarehouses")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<WarehouseResponse>>> GetNotDeleted(int offset = 0, int limit = 100)
    {
        var warehousesDto = await _warehouseService
            .GetAllAsync(offset, limit);
        
        var warehouseResponse = _mapper.Map<IReadOnlyCollection<WarehouseResponse>>(warehousesDto);

        return Ok(warehouseResponse);
    }
    
    [HttpGet("archive/",Name = "GetDeletedWarehouses")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<WarehouseResponse>>> GetDeleted(int offset = 0, int limit = 100)
    {
        var warehousesDto = await _warehouseService
            .GetAllAsync(offset, limit, true);
        
        var warehouseResponse = _mapper.Map<IReadOnlyCollection<WarehouseResponse>>(warehousesDto);

        return Ok(warehouseResponse);
    }
    
    [HttpGet("{warehouseId}", Name = "GetWarehouseById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WarehouseResponse>> Get([FromRoute] Guid warehouseId)
    {
        var warehouseDto = await _warehouseService.GetByIdAsync(warehouseId);

        if (warehouseDto is null)
        {
            return NotFound();
        }

        var warehouseResponse = _mapper.Map<WarehouseResponse>(warehouseDto);
        return Ok(warehouseResponse);
    }

    [HttpPost("{id:guid}", Name = "CreateWarehouse")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<WarehouseResponse>> CreateAsync([FromRoute] Guid id, [FromBody] CreateWarehouseRequest request)
    {
        if (await _warehouseService.GetByIdAsync(id) != null)
        {
            return Conflict("Warehouse with the same id already exist.");
        }
        
        var warehouseDto = _mapper.Map<WarehouseDto>(request);

        warehouseDto.Id = id;

        await _warehouseService.CreateAsync(warehouseDto);

        var warehouseResponse = _mapper.Map<WarehouseResponse>(warehouseDto);
        
        return Created("Warehouse created:", warehouseResponse);
    }
    
    [HttpPut("{id:guid}", Name = "UpdateWarehouse")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WarehouseResponse>> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateWarehouseRequest request)
    {
        var warehouseDto = _mapper.Map<WarehouseDto>(request);

        warehouseDto.Id = id;

        await _warehouseService.UpdateAsync(warehouseDto);

        var warehouseResponse = _mapper.Map<WarehouseResponse>(warehouseDto);
        return Ok(warehouseResponse);
    }

    [HttpDelete("{id:guid}", Name = "DeleteWarehouse")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        var warehouseDto = await _warehouseService.GetByIdAsync(id);

        if (warehouseDto?.Palettes?.Count != 0)
        {
            return BadRequest("Warehouse is not empty! Remove palettes first. Return.");
        } 
        
        await _warehouseService.DeleteAsync(id);

        return Ok();
    }
}