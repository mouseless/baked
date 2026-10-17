# Features

Features configure layers. Each feature is an abstraction with one or more
implementations, and an application picks the implementation that fits, e.g.,
SQLite or PostgreSQL for database.

::columns

#add

### Adding Features

Add an implementation of a feature to an application;

```csharp
app.Features.AddDatabase(c => c.PostgreSql());
app.Features.AddCodingStyles([
    c => c.LocateViaId(),
    c => c.NameBasedLabel()
]);
```

#recipe

### In a Recipe

Recipes come with a default for each feature, use `options:` to change them;

```csharp
Bake.New
    .Monolith(
        business: c => c.DomainAssemblies([...]),
        options: o => o.Database(c => c.PostgreSql())
    )
    .Run();
```

::

To learn how features work and how to create one, see
[Feature](../architecture/feature.md).
