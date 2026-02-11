# king-price-c-sharp

This is the **King Price C# Web API**, built with **ASP.NET Core** and **Entity Framework Core**.  
It manages users, groups, and permissions with **role-based access control (RBAC)**.

---

## Project Structure

# King Price C# Web API

This is the **King Price C# Web API**, built with **ASP.NET Core** and **Entity Framework Core**.  
It manages users, groups, and permissions with **role-based access control (RBAC)**.

---

## Project Structure

# King Price C# Web API

This is the **King Price C# Web API**, built with **ASP.NET Core** and **Entity Framework Core**.  
It manages users, groups, and permissions with **role-based access control (RBAC)**.

---


> **Note:** All `dotnet` commands must be run **inside the `kingPriceApi` folder** where `kingPriceApi.csproj` is located.

---

## Prerequisites

- [.NET SDK 9.0](https://dotnet.microsoft.com/download/dotnet/9.0) or higher  
- [PostgreSQL](https://www.postgresql.org/) (or your configured database)  
- Optional: Visual Studio 2022 / VS Code  

---

## Common Commands

### Build the Project

```bash
dotnet build
```

### Run the Project
```bash
dotnet run
```

## Swagger available at
- http://localhost:5132/swagger

### Clean the Project

```bash
dotnet clean
```

### Restore the Project
```bash
dotnet restore
```

### Add a Migration
```bash
dotnet ef migrations add <MigrationName>
```

### Apply Migrations to the Database
```bash
dotnet ef database update
```

### Remove Last Migration
```bash
dotnet ef migrations remove
```

### Run the unit tests
```bash
dotnet test
```