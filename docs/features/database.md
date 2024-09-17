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

Adds MySQL database setup which gets connection parameters from `app.settings`.

```csharp
c => c.MySql()
```

## PostgreSQL

Adds PostgreSQL database setup which gets connection parameters from
`app.settings`.

```csharp
c => c.PostgreSql()
```

## SQLite

Adds local SQLite database setup which creates local sqlite database in
documents folder with given name.

```csharp
c => c.Sqlite(fileName: "test.db", autoExportSchema: false)
```
