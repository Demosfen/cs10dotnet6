using WMS.Repositories.Concrete;
using WMS.WarehouseDbContext;
using WMS.WarehouseDbContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;

await using var context = new WarehouseDbContext();
await context.Database.MigrateAsync();

