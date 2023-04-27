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
    public async Task<ActionResult<IReadOnlyCollection<WarehouseResponse>>> 
        GetNotDeletedAsync(int offset = 0, int size = 10)
    {
        var warehousesDto = await _warehouseService
            .GetAllAsync(offset, size);
        
        var warehouseResponse = _mapper.Map<IReadOnlyCollection<WarehouseResponse>>(warehousesDto);

        return Ok(warehouseResponse);
    }
    
    [HttpGet("archive/",Name = "GetDeletedWarehouses")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<WarehouseResponse>>> 
        GetDeletedAsync(int offset = 0, int limit = 10)
    {
        var warehousesDto = await _warehouseService
            .GetAllAsync(offset, limit, true);
        
        var warehouseResponse = _mapper.Map<IReadOnlyCollection<WarehouseResponse>>(warehousesDto);

        return Ok(warehouseResponse);
    }
    
    [HttpGet("{warehouseId:guid}", Name = "GetWarehouseByIdWithPalettes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WarehouseResponse>> GetByIdAsync(
        [FromRoute] Guid warehouseId,
        int offset = 0,
        int size = 10)
    {
        var warehouseDto = await _warehouseService.GetByIdAsync(warehouseId, offset, size);

        if (warehouseDto is null)
        {
            return NotFound();
        }

        var warehouseResponse = _mapper.Map<WarehouseResponse>(warehouseDto);
        return Ok(warehouseResponse);
    }

    [HttpPost("{warehouseId:guid}", Name = "CreateWarehouse")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<WarehouseResponse>> CreateAsync([FromRoute] Guid warehouseId, [FromBody] CreateWarehouseRequest request)
    {
        if (await _warehouseService.GetByIdAsync(warehouseId) != null)
        {
            return Conflict("Warehouse with the same id already exist.");
        }
        
        var warehouseDto = _mapper.Map<WarehouseDto>(request);

        warehouseDto.Id = warehouseId;

        await _warehouseService.CreateAsync(warehouseDto);

        var warehouseResponse = _mapper.Map<WarehouseResponse>(warehouseDto);
        
        return Created("Warehouse created:", warehouseResponse);
    }
    
    [HttpPut("{warehouseId:guid}", Name = "UpdateWarehouse")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WarehouseResponse>> UpdateAsync(
        [FromRoute] Guid warehouseId, 
        [FromBody] UpdateWarehouseRequest request)
    {
        var warehouseDto = _mapper.Map<WarehouseDto>(request);

        warehouseDto.Id = warehouseId;

        await _warehouseService.UpdateAsync(warehouseDto);

        var warehouseResponse = _mapper.Map<WarehouseResponse>(warehouseDto);
        return Ok(warehouseResponse);
    }

    [HttpDelete("{warehouseId:guid}", Name = "DeleteWarehouse")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteAsync(Guid warehouseId)
    {
        var warehouseDto = await _warehouseService.GetByIdAsync(warehouseId);

        if (warehouseDto?.Palettes?.Count != 0)
        {
            return BadRequest("Warehouse is not empty! Remove palettes first. Return.");
        } 
        
        await _warehouseService.DeleteAsync(warehouseId);

        return Ok();
    }
}