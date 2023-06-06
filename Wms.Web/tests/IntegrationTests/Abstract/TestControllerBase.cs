using AutoMapper;
using Wms.Web.Business.Infrastructure.Mapping;
using Wms.Web.Client.Custom.Abstract;
using Wms.Web.Store.Common.Interfaces;
using Xunit;

namespace Wms.Web.IntegrationTests.Abstract;

[Collection(IntegrationTestCollection.Name)]
public abstract partial class TestControllerBase
{
    protected readonly IWmsClient Sut;
    
    protected IWarehouseDbContext DbContext { get; }

    protected TestControllerBase(TestApplication apiFactory)
    {
        Sut = apiFactory.WmsClient;
        DbContext = apiFactory.CreateDbContext();
        
        var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(DtoEntitiesMappingProfile));
            });

        Mapper = configurationProvider.CreateMapper();
    }
}