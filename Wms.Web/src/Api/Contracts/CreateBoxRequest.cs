using System;
using Wms.Web.Contracts.Requests;

namespace Wms.Web.Api.Contracts;

public sealed class CreateBoxRequest
{
    public required Guid PaletteId { get; init; }
    
    public required Guid Id { get; init; }

    public required BoxRequest BoxRequest { get; init; }
}