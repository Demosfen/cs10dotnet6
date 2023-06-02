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
### Api
![Coupling analysis](/home/alexander/C#/cs10dotnet6/docs/diagrams/Wms.Api.png "Api structure")