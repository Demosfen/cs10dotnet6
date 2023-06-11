using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Features.OwnedInstances;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Wms.Web.Api;
using Wms.Web.Api.Infrastructure.Filters;
using Wms.Web.Api.Infrastructure.Mapping;
using Wms.Web.Api.Validators.Warehouse;
using Wms.Web.Business.Infrastructure.DI;
using Wms.Web.Business.Infrastructure.Mapping;
using Wms.Web.Store.Common.Interfaces;
using Wms.Web.Store.Postgres.DI;
using Wms.Web.Store.Sqlite.DI;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = Directory.GetCurrentDirectory()
});

builder.Logging.ClearProviders().AddConsole();

builder.Configuration.AddEnvironmentVariables();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

var config = builder.Configuration;
config.AddEnvironmentVariables("WarehouseApi_");

builder.Services
    .AddControllers()
    .AddControllersAsServices()
    .AddMvcOptions(opt =>
    {
        opt.Filters.Add<DefaultExceptionFilter>();
    });

builder.Services.AddFluentValidationAutoValidation(cfg =>
    cfg.DisableDataAnnotationsValidation = true);
builder.Services.AddValidatorsFromAssemblyContaining<CreateWarehouseRequestValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
        { Title = "My Warehouse Service API", Version = "v1" });
});

builder.Services.AddAutoMapper(
    typeof(ApiContractToDtoMappingProfile), 
    typeof(DtoEntitiesMappingProfile));

var wmsOptions = config.GetRequiredSection(WmsOptions.SectionName)
    .Get<WmsOptions>()
    ?? throw new InvalidOperationException("Provide DbProvider options please");

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    switch (wmsOptions.DbProvider)
    {
        case WmsOptions.DbProviderEnum.Postgres:
            containerBuilder.RegisterModule<PostgresDbContextModule>();
            break;
        case WmsOptions.DbProviderEnum.Sqlite:
            containerBuilder.RegisterModule<SqliteDbContextModule>();
            break;
        default:
            throw new ArgumentException($"Invalid data source: {wmsOptions.DbProvider}");
    }

    containerBuilder.RegisterModule<BusinessModule>();
});

var app = builder.Build();

app.Logger.LogInformation("Configure the HTTP request pipeline");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Logger.LogInformation("Add routing");
app.UseRouting();

app.Logger.LogInformation("Map controllers");
app.MapControllers();

var mapper = app.Services.GetRequiredService<IMapper>();
mapper.ConfigurationProvider.AssertConfigurationIsValid();

app.Logger.LogInformation("Get DbContext from Services");
await using var dbContext = app.Services.GetRequiredService<Func<Owned<IWarehouseDbContext>>>()();

app.Logger.LogInformation("Migrations call");
await dbContext.Value.Database.MigrateAsync();

app.Run();