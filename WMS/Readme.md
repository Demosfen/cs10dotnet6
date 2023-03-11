# Warehouse Management System (WMS)

## Projects

### Store
EF Core project with box / palette / warehouse entities and migrations

## Migrations
### Setup
```
dotnet tool install --global dotnet-ef
```

### Add new migrations
```
dotnet ef migrations add InitialCreate --startup-project ..\WarehouseApp\WarehouseApp.csproj --project .\Store.csproj --context WarehouseDbContext
```

### Update database
```
dotnet ef database update
```