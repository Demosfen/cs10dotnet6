using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Wms.Web.Api.Contracts;
using Wms.Web.Business.Abstract;
using Wms.Web.Business.Dto;
using Wms.Web.Contracts.Requests;
using Wms.Web.Contracts.Responses;

namespace Wms.Web.Api.Controllers;

/// <summary>
/// Warehouse controller with CRUD operations
/// </summary>
[ApiController]
[Route("api/v1/warehouses/")]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public sealed class WarehouseController : ControllerBase
{
    private readonly IWarehouseService _warehouseService;
    private readonly IPaletteService _paletteService;
    private readonly IMapper _mapper;

    public WarehouseController(
        IWarehouseService warehouseService,
        IPaletteService paletteService,
        IMapper mapper)
    {
        _warehouseService = warehouseService;
        _paletteService = paletteService;
        _mapper = mapper;
    }
    
    [HttpGet(Name = "GetNotDeletedWarehouses")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<WarehouseResponse>>> GetNotDeletedAsync(
        [FromQuery] int offset = 0,
        [FromQuery] int size = 10,
        CancellationToken cancellationToken = default)
    {
        var warehousesDto = await _warehouseService
            .GetAllAsync(offset, size, cancellationToken: cancellationToken);
        
        var warehouseResponse = _mapper.Map<IReadOnlyCollection<WarehouseResponse>>(warehousesDto);
        
        return Ok(warehouseResponse);
    }
    
    [HttpGet("archive/",Name = "GetDeletedWarehouses")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<WarehouseResponse>>> GetDeletedAsync(
        [FromQuery] int offset = 0, 
        [FromQuery] int limit = 10, 
        CancellationToken cancellationToken = default)
    {
        var warehousesDto = await _warehouseService
            .GetAllAsync(offset, limit, true, cancellationToken);
        
        var warehouseResponse = _mapper.Map<IReadOnlyCollection<WarehouseResponse>>(warehousesDto);
        
        return Ok(warehouseResponse);
    }
    
    [HttpGet("{warehouseId:guid}", Name = "GetWarehouseById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WarehouseResponse>> GetByIdAsync(
        [FromRoute] Guid warehouseId,
        [FromQuery] int palettesOffset = 0,
        [FromQuery] int palettesSize = 10,
        CancellationToken cancellationToken = default)
    {
        var warehouseDto = await _warehouseService.GetByIdAsync(warehouseId, cancellationToken);

        var paletteDto = await _paletteService.GetAllAsync(
            warehouseId,
            palettesOffset, palettesSize, false,
            cancellationToken);
        
        warehouseDto?.Palettes.AddRange(paletteDto);
        
        return Ok(_mapper.Map<WarehouseResponse>(warehouseDto));
    }

    [HttpPost(Name = "CreateWarehouse")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<WarehouseResponse>> CreateAsync(
        [FromQuery][Required] Guid warehouseId, 
        [FromBody] WarehouseRequest request,
        CancellationToken cancellationToken = default)
    {
        var createRequest = new CreateWarehouseRequest
        {
            Id = warehouseId,
            WarehouseRequest = request
        };
        
        var warehouseDto = _mapper.Map<WarehouseDto>(createRequest);

        await _warehouseService.CreateAsync(warehouseDto, cancellationToken);
        
        var locationUri = Url.Link("GetWarehouseById", new { warehouseId });
        
        return Created(locationUri ?? throw new InvalidOperationException(),  
            _mapper.Map<WarehouseResponse>(warehouseDto));
    }
    
    [HttpPut("{warehouseId:guid}", Name = "UpdateWarehouse")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WarehouseResponse>> UpdateAsync(
        [FromRoute] Guid warehouseId, 
        [FromBody] WarehouseRequest request,
        CancellationToken cancellationToken = default)
    {
        var updateRequest = new UpdateWarehouseRequest
        {
            Id = warehouseId,
            WarehouseRequest = request
        };
        
        var warehouseDto = _mapper.Map<WarehouseDto>(updateRequest);

        await _warehouseService.UpdateAsync(warehouseDto, cancellationToken);

        return Ok(_mapper.Map<WarehouseResponse>(warehouseDto));
    }

    [HttpDelete("{warehouseId:guid}", Name = "DeleteWarehouse")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteAsync([FromRoute] Guid warehouseId)
    {
        await _warehouseService.DeleteAsync(warehouseId);

        return NoContent();
    }
}