using WMS.Repositories.Concrete;
using WMS.WarehouseDbContext;
using WMS.WarehouseDbContext.Entities;
using WMS.Services;
using WMS.Services.Concrete;

using static System.Console;

internal class Program
{
    public static async Task Main(string[] args)
    {
        WarehouseDbContext context = new WarehouseDbContext();

        var warehouseRepository = new WarehouseRepository(context);

        await warehouseRepository.CreateAsync(new Warehouse());

        await context.SaveChangesAsync();
    }
}
