using System.Text;
using WMS.Data;
using WMS.Services.Abstract;
using System.Text.Json;
using System.Text.Json.Serialization;

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
    
    public async Task<Warehouse> Read(string fileName)
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, fileName);

        using FileStream openStream = File.OpenRead(filePath);
        
        //var json = await File.ReadAllTextAsync(filePath, Encoding.UTF8);
        var result = await JsonSerializer.DeserializeAsync<Warehouse>(openStream, _options)
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
        /*var json = JsonSerializer.Serialize(warehouse, _options);
        File.WriteAllText(fileName, json, Encoding.UTF8);*/
    }
}