using Microsoft.AspNetCore.Mvc;
using Wms.Web.Api.Contracts.Requests;

//TODO kill this nuget here. How? https://github.com/alex1ozr/DockerTests/blob/main/src/Api/Contracts/Requests/CreatePersonRequest.cs

namespace Wms.Web.Api.Contracts;

public sealed class CreateBoxRequest
{
    [FromRoute(Name = "paletteId")]
    public required Guid PaletteId { get; init; }
    
    [FromRoute(Name = "boxId")]
    public required Guid Id { get; init; }

    [FromBody]
    public required BoxRequest BoxRequest { get; init; }
}