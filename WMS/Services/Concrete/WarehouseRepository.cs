using System.Text;
using WMS.Data;
using WMS.Services.Abstract;
using WMS.Services.Models.Serialization;
using System.Text.Json;
using System.Text.Json.Nodes;

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
    
    public async Task<RootObject> Read(string fileName)
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, fileName);

        using FileStream openStream = File.OpenRead(filePath);

        var json = await File.ReadAllTextAsync(filePath, Encoding.UTF8);

        //JsonNode? result1 = JsonObject.Parse(json);
        
        var result = await JsonSerializer.DeserializeAsync<RootObject>(openStream, _options)
                     ?? throw new Exception("There is nothing to deserialize...");

        return result;
    }

    public async Task Save(Warehouse warehouse, string fileName)
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, fileName);

        using FileStream fileStream = File.Create(filePath);
        
        await JsonSerializer.SerializeAsync<Warehouse>(
            utf8Json: fileStream, value: warehouse, _options);
        await fileStream.DisposeAsync();
    }
}