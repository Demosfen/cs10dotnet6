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
    private readonly ILogger<WarehouseController> _logger;
    private readonly IMapper _mapper;

    public WarehouseController(
        ILogger<WarehouseController> logger, 
        IWarehouseService warehouseService, 
        IMapper mapper)
    {
        _logger = logger;
        _warehouseService = warehouseService;
        _mapper = mapper;
    }

    [HttpPost("warehouses/{}")]
    public async Task<IActionResult> Create([FromQuery] CreateWarehouseRequest request)
    {
        var warehouseDto = _mapper.Map<WarehouseDto>(request);

        await _warehouseService.CreateAsync(warehouseDto);

        var response = _mapper.Map<WarehouseResponse>(warehouseDto);

        return CreatedAtAction("Get", new { response.Id }, response);
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

    [HttpGet("warehouses/")]
    public async Task<IActionResult> GetAll()
    {
        var warehouses = await _warehouseService.GetAllAsync();
        var warehouseResponse = _mapper.Map<IReadOnlyCollection<WarehouseResponse>>(warehouses);

        return Ok(warehouseResponse);
    }
}