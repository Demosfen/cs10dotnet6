using WMS.Data;
using WMS.Services.Models.Serialization;

namespace WMS.Services.Abstract;

public interface IWarehouseRepository
{
    Task<RootObject> Read(string fileName);

    Task Save(Warehouse warehouse, string fileName);
}