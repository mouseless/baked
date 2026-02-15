# ORM

This features basically configures `DataAccessLayer` so that it interprets
certain domain types as entities and queries.

Add this feature using `AddOrm()` extension;

```csharp
app.Features.AddOrm(...);
```

## Auto Map

Auto maps domain types with `Entity` attribute using default opinions of
`FluentNHibernate` and registers `IEntityContext<>`, `IQueryContext<>` and
`ILocator<>` services for entities.

```csharp
c => c.AutoMap()
```
