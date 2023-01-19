using AutoMapper;
using System.Text.Json;
using WMS.Data;
using WMS.Services.Abstract;
using WMS.Services.Models.Serialization;
using WMS.Services.Infrastructure.Mapping.Profiles;

namespace WMS.Services.Concrete;

public class WarehouseRepository: IWarehouseRepository
{
    private static readonly JsonSerializerOptions _options = new()
    {
        IncludeFields = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
        WriteIndented = true
    };

    private static IMapper Mapper { get; set; } = null!;

    public static void CreateMapper()
    {
        var mapperConfiguration = new MapperConfiguration(x =>
            x.AddProfile<WarehouseMapperConfiguration>());
        
        mapperConfiguration.AssertConfigurationIsValid();

        Mapper = mapperConfiguration.CreateMapper();
    }

    public async Task<Warehouse> Read(string fileName)
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, fileName);

        using FileStream openStream = File.OpenRead(filePath);

        var modelResult = await JsonSerializer.DeserializeAsync<WarehouseModel>(openStream, _options)
                     ?? throw new Exception("There is nothing to deserialize...");
        
        /*var config = new MapperConfiguration(
            cfg => cfg.CreateMap<WarehouseModel, Warehouse>());        

        var mapper = new Mapper(config);

        Warehouse result = mapper.Map<Warehouse>(modelResult);*/

        var result = Mapper.Map<Warehouse>(modelResult);

        return result;
    }

    /// <summary>
    /// Saving current warehouse state
    /// </summary>
    /// <param name="warehouse">Storung palettes</param>
    /// <param name="fileName">File name of json file</param>
    public async Task Save(Warehouse warehouse, string fileName)
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, fileName);

        using FileStream fileStream = File.Create(filePath);
        
        await JsonSerializer.SerializeAsync<Warehouse>(
            utf8Json: fileStream, value: warehouse, _options);
        await fileStream.DisposeAsync();
    }
}