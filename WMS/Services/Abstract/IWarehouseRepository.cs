using WMS.Data;

namespace WMS.Services.Abstract;

public interface IWarehouseRepository
{
    Task<Warehouse> Read(string fileName);

    Task Save(Warehouse warehouse, string fileName);
}