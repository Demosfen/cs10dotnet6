using Wms.Web.Api.IntegrationTests.Extensions;
using Wms.Web.Store.Entities;
using Wms.Web.Store.Interfaces;
using Xunit;

namespace Wms.Web.Api.IntegrationTests.Abstract;

public abstract partial class TestControllerBase
{
    /*
    protected async Task<Warehouse> CreateWarehouse()
    {
        var entity = DbContext.Warehouses.Add(new Warehouse
        {
            //...
        });
        await DbContext.SaveChangesAsync();

        return entity.Entity;
    }*/
}