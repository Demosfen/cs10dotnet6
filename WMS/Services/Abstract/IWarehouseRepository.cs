using WMS.Data;

namespace WMS.Services.Abstract;

public interface IWarehouseRepository
{
    Warehouse Read(string fileName);

    void Save(Warehouse warehouse, string fileName);
}