﻿using Wms.Web.Api.Console.Clients;

using var httpClient = new HttpClient
{
    BaseAddress = new Uri("http://localhost:5003")
};

var client = new WmsClient(httpClient);

var result = await client.PostAsync(Guid.NewGuid(), CancellationToken.None);

Console.ReadKey();