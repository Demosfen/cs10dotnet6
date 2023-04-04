using AutoMapper;
using Wms.Web.Api.Contracts.Responses;
using Wms.Web.Store.Entities;
using Wms.Web.Repositories.Abstract;
using Wms.Web.Services.Abstract;
using Wms.Web.Services.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Wms.Web.Api.Controllers;

[ApiController]
[Route("api/v1/")]
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
    
    [HttpGet("warehouses/")]
    public async Task<IActionResult> GetAll()
    {
        var warehouses = await _warehouseService.GetAllAsync();
        var warehouseResponse = _mapper.Map<IReadOnlyCollection<WarehouseResponse>>(warehouses);

        return Ok(warehouseResponse);
    }
}