using AutoMapper;
using System.Text.Json;
using WMS.Data;
using WMS.Services.Abstract;
using WMS.Services.Models.Serialization;
using WMS.Services.Infrastructure.Mapping.Profiles;

namespace WMS.Services.Concrete;

public class WarehouseRepository: IWarehouseRepository
{
    private static Lazy<IMapper> Mapper { get; } = new(CreateWarehouseMapper);
    
    private static readonly JsonSerializerOptions _options = new()
    {
        IncludeFields = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
        WriteIndented = true
    };

    private static IMapper CreateWarehouseMapper()
    {
        var mapperConfiguration = new MapperConfiguration(
            x => x.AddProfile<WarehouseMapperConfiguration>());

        mapperConfiguration.AssertConfigurationIsValid();

        var mapper = mapperConfiguration.CreateMapper();

        return mapper;
    }

    /// <summary>
    /// Reading warehouse saved state
    /// </summary>
    /// <param name="fileName">Warehouse JSON saved file</param>
    /// <returns>Instantiated object of Warehouse type</returns>
    /// <exception cref="Exception">If no saved json file</exception>
    public async Task<Warehouse> Read(string fileName)
    {
        var filePath = Path.Combine(Environment.CurrentDirectory, fileName);

        await using FileStream openStream = File.OpenRead(filePath);

        var modelResult = await JsonSerializer.DeserializeAsync<WarehouseModel>(openStream, _options)
                     ?? throw new Exception("There is nothing to deserialize...");
        
        CreateWarehouseMapper();

        var result = Mapper.Value.Map<Warehouse>(modelResult);

        return result;
    }

    /// <summary>
    /// Saving current warehouse state
    /// </summary>
    /// <param name="warehouse">Storage of the palettes</param>
    /// <param name="fileName">File name of json file</param>
    public async Task Save(Warehouse warehouse, string fileName)
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, fileName);

        await using FileStream fileStream = File.Create(filePath);
        
        CreateWarehouseMapper();
        
        await JsonSerializer.SerializeAsync(
            utf8Json: fileStream, 
            value: Mapper.Value.Map<WarehouseModel>(warehouse), 
            _options);
        
        await fileStream.DisposeAsync();
    }
}