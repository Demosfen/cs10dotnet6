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
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(WarehouseResponse))]
    public async Task<IActionResult> GetAll()
    {
        var warehouses = await _warehouseService.GetAllAsync();
        var warehouseResponse = _mapper.Map<IReadOnlyCollection<WarehouseResponse>>(warehouses);

        return Ok(warehouseResponse);
    }

    [HttpPost("warehouses/{name}/create", Name = "Create warehouse")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(WarehouseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(WarehouseDto))]
    // public async Task<IActionResult> Create([FromBody] CreateWarehouseRequest request)  //TODO: create own attributes: https://nabeelvalley.co.za/blog/2020/17-12/csharp-webapi-custom-attributes/
    // public async Task<IActionResult> Create([FromBody] WarehouseRequest request)         //TODO: Parameters from route doesn't work. Default model only
    public async Task<IActionResult> Create([FromRoute] string name)
    {
        var request = new WarehouseRequest
        {
            Id = Guid.NewGuid(),
            Name = name
        };

        var warehouseDto = _mapper.Map<WarehouseDto>(request);

        if (warehouseDto.Name.Equals(name))
        {
            return Conflict("Warehouse with the same name already exist.");
        }

        await _warehouseService.CreateAsync(warehouseDto);

        var response = _mapper.Map<WarehouseResponse>(warehouseDto);

        // return CreatedAtAction("Get", new { response.Id }, response);
        return Created("Warehouse created:", warehouseDto);
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
    
    
}