using Microsoft.AspNetCore.Mvc; //TODO kill this nuget here. How? https://github.com/alex1ozr/DockerTests/blob/main/src/Api/Contracts/Requests/CreatePersonRequest.cs

namespace Wms.Web.Api.Contracts.Requests;

public sealed class CreateBoxRequest
{
    [FromRoute(Name = "boxId")]
    public required Guid Id { get; init; }
    
    [FromRoute(Name = "paletteId")]
    public required Guid PaletteId { get; init; }
    
    [FromBody]
    public required BoxRequest BoxRequest { get; init; }
}