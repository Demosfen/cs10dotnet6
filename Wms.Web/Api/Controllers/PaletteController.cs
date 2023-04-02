using Api;
using Microsoft.AspNetCore.Mvc;

namespace Wms.Web.Api.Controllers;

[ApiController]
[Route("palettes/")]
public sealed class PaletteController : ControllerBase
{
    private readonly ILogger<PaletteController> _logger;

    public PaletteController(ILogger<PaletteController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet(Name = "GetAllPalettes")]
    public IEnumerable<Warehouse> Get()
    {
        
        return Enumerable.Range(1, 5).Select(index => new Warehouse
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
            })
            .ToArray();
    }
}