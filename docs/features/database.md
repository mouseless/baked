# Database

This feature provides a database setup to be used both in development and
production setups.

Add this feature using `AddDatabase()` extension;

```csharp
app.Features.AddDatabase(...);
```

## InMemory

Adds in-memory database setup.

```csharp
c => c.InMemory()
```

## Sqlite

Adds local Sqlite database setup which creates local sqlite database in
documents folder with given name.

```csharp
c => c.AddSqlite(fileName: "test.db")
```

## MySql

Adds MySql database setup which gets connection parameters from `app.settings`.

```csharp
c => c.MySql()
```
