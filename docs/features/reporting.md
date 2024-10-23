# Reporting

Implementations of this feature registers a singleton `IReportContext` with
which you can read raw data from database. Add this feature using
`AddReporting()` extension;

```csharp
app.Features.AddReporting(...);
```

## Fake

Adds a fake report context that allows you to return data directly from `.json`
resources.

```csharp
c => c.Fake(basePath: "Fake")
```

## Mock

Adds a mock instance of report context to be used during spec tests.

```csharp
c => c.Mock()
```

## Native SQL

Adds a report context instance that uses a `IStatelessSession` instance to
execute native SQL queries read from `.sql` resources in your project.

```csharp
c => c.NativeSql(basePath: "Queries/MySql")
```

> [!TIP]
>
> You may group your RDBMS specific queries in different folders, and use
> setting to specify which folder to use depending on environment.
>
> ```csharp
> c => c.NativeSql(basePath: Settings.Required("Reporting:NativeSql:BasePath"))
> ```
