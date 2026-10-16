# Database

This feature provides a database setup to be used both in development and
production setups.

Add this feature using `AddDatabase()` extension;

```csharp
app.Features.AddDatabase(...);
```

## In Memory

Adds in-memory database setup.

```csharp
c => c.InMemory()
```

## MySQL

Adds MySQL database setup.

```csharp
c => c.MySql()
```

Connection string is required, schema update is optional and disabled by
default. When they are not given as arguments, they are read from
`appsettings.json`;

```json
"Database": {
  "MySql": {
    "ConnectionString": "...",
    "AutoUpdateSchema": false
  }
}
```

## Oracle

Adds Oracle database setup.

```csharp
c => c.Oracle()
```

Settings are the same as MySQL, under `Database:Oracle` section.

## PostgreSQL

Adds PostgreSQL database setup.

```csharp
c => c.PostgreSql()
```

Settings are the same as MySQL, under `Database:PostgreSql` section.

## SQLite

Adds local SQLite database setup which creates a database file with given name
in the documents folder of the user.

```csharp
c => c.Sqlite(fileName: "test.db", autoExportSchema: false)
```

File name is required, schema export is optional and enabled by default. When
they are not given as arguments, they are read from `appsettings.json`;

```json
"Database": {
  "Sqlite": {
    "FileName": "test.db",
    "AutoExportSchema": true
  }
}
```

> [!NOTE]
>
> When the user does not have a documents folder, e.g., in a container, the
> file is created in the working directory
