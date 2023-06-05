# Warehouse Management System (WMS)
Warehouse management system operates (CRUD) with array of warehouses containing palettes and boxes on them.

## Migrations
<details>
<summary>Setup / Add / Update</summary>

### Setup
```bash
dotnet tool install --global dotnet-ef
```

### Add new migrations in src/ (Sqlite)
```bash
dotnet ef migrations add InitialCreate --startup-project src/Api/Api.csproj --project src/Store.Sqlite/Store.Sqlite.csproj --context Wms.Web.Store.Sqlite.WarehouseDbContext
```

### Add new migrations in src/ (Postgres)
```bash
dotnet ef migrations add InitialMigration --startup-project src/Api/Api.csproj --project src/Store.Postgres/Store.Postgres.csproj --context Wms.Web.Store.Postgres.WarehouseDbContext
```

### Update database
```bash
dotnet ef database update
```
</details>

## Db Providers
- Postgresql
- Sqlite

## Key Technologies
- 