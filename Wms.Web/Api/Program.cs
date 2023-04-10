using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Features.OwnedInstances;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Wms.Web.Api.Infrastructure.Mapping;
using Wms.Web.Repositories.Infrastructure.DI;
using Wms.Web.Services.Infrastructure.DI;
using Wms.Web.Services.Infrastructure.Mapping;
using Wms.Web.Store;
using Wms.Web.Store.Infrastructure.DI;
using Wms.Web.Store.Interfaces;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = Directory.GetCurrentDirectory()
});

builder.Configuration.AddEnvironmentVariables();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

var config = builder.Configuration;
config.AddEnvironmentVariables("WarehouseApi_");

builder.Services
    .AddControllers()
    .AddControllersAsServices();
    // .AddMvcOptions();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
        { Title = "My Warehouse Service API", Version = "v1" });
});

builder.Services.AddAutoMapper(typeof(ApiContractToDtoMappingProfile));
builder.Services.AddAutoMapper(typeof(DtoEntitiesMappingProfile));

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule<WarehouseDbContextModule>();
    containerBuilder.RegisterModule<ServiceModule>();
    containerBuilder.RegisterModule<RepositoriesModule>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.MapControllers();

var mapper = app.Services.GetRequiredService<IMapper>();
mapper.ConfigurationProvider.AssertConfigurationIsValid();

await using var dbContext = app.Services.GetRequiredService<Func<Owned<IWarehouseDbContext>>>()();
await dbContext.Value.Database.MigrateAsync();

app.Run();