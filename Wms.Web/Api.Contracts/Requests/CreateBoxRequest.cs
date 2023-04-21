using Microsoft.AspNetCore.Mvc;

namespace Wms.Web.Api.Contracts.Requests;

public class CreateBoxRequest
{
    [FromRoute(Name = "boxId")]
    public required Guid Id { get; init; }
    
    [FromRoute(Name = "paletteId")]
    public required Guid PaletteId { get; init; }
    
    [FromBody]
    public required BoxRequest BoxRequest { get; init; }
}