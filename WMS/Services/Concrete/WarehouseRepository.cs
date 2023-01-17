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
    
    public Warehouse Read(string fileName)
    {
        var json = File.ReadAllText(fileName, Encoding.UTF8);
        var result = JsonSerializer.Deserialize<Warehouse>(json, _options)
                     ?? throw new Exception("TODO");

        return result;
    }

    public void Save(Warehouse warehouse, string fileName)
    {
        string filePath = System.IO.Path.Combine(System.Environment.CurrentDirectory, fileName);

        using (Stream fileStream = File.Create(filePath))
        {
            JsonSerializer.Serialize<Warehouse>(
                utf8Json: fileStream, value: warehouse, _options);
        }
        /*var json = JsonSerializer.Serialize(warehouse, _options);
        File.WriteAllText(fileName, json, Encoding.UTF8);*/
    }
}