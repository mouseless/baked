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

Configures properties with `UniqueAttribute` to have unique constraint, unless
the properties are overridden explicitly in another feature.

```csharp
c => c.AutoMap()
```

> [!WARNING]
>
> A unique constraint will get removed even if you change another configuration
> of that property such as column name. You are expected to call `.Unique()`
> explicitly for the properties that have a mapping override.
