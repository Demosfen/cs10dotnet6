# Warehouse Management System (WMS)
Warehouse management system operates (CRUD) with array of warehouses containing palettes and boxes on them.
The functional diagram of the project presented [here](https://github.com/Demosfen/cs10dotnet6/blob/Wms.CleanArchitecture/Wms.Web/Structure.md) 

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
- Microsoft.EntityFrameworkCore
- Dependency Injection + Autofac
- Swashbuckle.AspNetCore

## Tech Stack
- .Net7
- Central package management - [Directory.Packages.props](https://github.com/Demosfen/cs10dotnet6/blob/Wms.CleanArchitecture/Wms.Web/Directory.Packages.props)
- Build customization - [Directory.Build.props](https://github.com/Demosfen/cs10dotnet6/blob/Wms.CleanArchitecture/Wms.Web/Directory.Build.props)
- Nullable reference types

## Testing
- FluentAssertions
- FluentValidations
- [TestContainers + Docker](https://github.com/Demosfen/cs10dotnet6/tree/Wms.CleanArchitecture/Wms.Web/tests/IntegrationTests)
- [UnitTests](https://github.com/Demosfen/cs10dotnet6/tree/Wms.CleanArchitecture/Wms.Web/tests/UnitTests)
- [Moq (under construction)]()

## IDE
- JetBrains Rider Professional