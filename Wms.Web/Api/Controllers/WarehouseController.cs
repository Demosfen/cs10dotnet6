using AutoMapper;
using Wms.Web.Api.Contracts.Responses;
using Wms.Web.Services.Abstract;
using Wms.Web.Api.Contracts.Requests;
using Wms.Web.Services.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Wms.Web.Api.Controllers;

[ApiController]
[Route("api/v1/")]
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
    
    [HttpGet("warehouses/")]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(WarehouseDto))]
    public async Task<IActionResult> GetAll()
    {
        var warehouses = await _warehouseService.GetAllAsync();
        var warehouseResponse = _mapper.Map<IReadOnlyCollection<WarehouseResponse>>(warehouses);

        return Ok(warehouseResponse);
    }
    
    [HttpGet("warehouses/{warehouseId}", Name = "GetWarehouseById")]
    public async Task<IActionResult> Get([FromRoute] Guid warehouseId)
    {
        var warehouseDto = await _warehouseService.GetByIdAsync(warehouseId);

        if (warehouseDto is null)
        {
            return NotFound();
        }

        var warehouseResponse = _mapper.Map<WarehouseResponse>(warehouseDto);
        return Ok(warehouseResponse);
    }

    [HttpPost("warehouses/{id:guid}", Name = "CreateWarehouse")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(WarehouseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(WarehouseDto))]
    public async Task<IActionResult> CreateAsync([FromRoute] Guid id, [FromBody] CreateWarehouseRequest request)
    {
        if (await _warehouseService.GetByIdAsync(id) != null)
        {
            return Conflict("Warehouse with the same id already exist.");
        }
        
        var warehouseDto = _mapper.Map<WarehouseDto>(request);

        warehouseDto.Id = id;

        await _warehouseService.CreateAsync(warehouseDto);

        var response = _mapper.Map<WarehouseResponse>(warehouseDto);
        
        return Created("Warehouse created:", warehouseDto);
    }
    
    [HttpPut("warehouses/{id:guid}", Name = "UpdateWarehouse")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WarehouseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(UpdateWarehouseRequest))]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] CreateWarehouseRequest request)
    {
        var warehouseDto = _mapper.Map<WarehouseDto>(request);

        warehouseDto.Id = id;

        await _warehouseService.UpdateAsync(warehouseDto);

        var response = _mapper.Map<WarehouseResponse>(warehouseDto);
        return Ok(response);
    }
}