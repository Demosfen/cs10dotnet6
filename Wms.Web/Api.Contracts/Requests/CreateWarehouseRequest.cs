﻿namespace Wms.Web.Api.Contracts.Requests;

public sealed class CreateWarehouseRequest
{
    public required string Name { get; init; }

    internal CreateWarehouseRequest(string name)
    {
        Name = name;
    }
}