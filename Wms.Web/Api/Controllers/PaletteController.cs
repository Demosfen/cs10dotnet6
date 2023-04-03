using Wms.Web.Api;
using Microsoft.AspNetCore.Mvc;
using Wms.Web.Store.Entities;

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
    
    // [HttpGet(Name = "GetAllPalettes")]
    // public IEnumerable<Warehouse> Get()
    // {
    //
    //     return Enumerable.Range(1, 5);
    // }
}