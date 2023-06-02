# Warehouse Management System (WMS)

## Migrations

<details>
<summary>Setup / Add / Update</summary>


### Setup
```
dotnet tool install --global dotnet-ef
```

### Add new migrations
```
dotnet ef migrations add InitialCreate --startup-project Api/Api.csproj --project Store/Store.csproj --context WarehouseDbContext
```

### Update database
```
dotnet ef database update
```
</details>

## Project structure
<details>
  <summary>Click to expand</summary>

  <div>
    <h3>Api</h3>
    <img src="../docs/diagrams/Wms.Api.png" alt="Wms.Api.png">
  </div>
</details>