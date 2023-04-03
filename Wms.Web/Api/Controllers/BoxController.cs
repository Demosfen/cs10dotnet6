using Wms.Web;
using Microsoft.AspNetCore.Mvc;

namespace Wms.Web.Api.Controllers;

[ApiController]
[Route("boxes/")]
public sealed class BoxController : ControllerBase
{
    private readonly ILogger<BoxController> _logger;

    public BoxController(ILogger<BoxController> logger)
    {
        _logger = logger;
    }
    
    
}