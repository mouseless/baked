# Recipes

Baked consists of many features and layers. You are free to add the features
and layers you need to your application and bake an application that fits your
needs. However, there are also architectural recipes that include a set of
layers and features with a default configuration to allow you to reuse what is
commonly needed for an application.

::columns

#monolith

### Monolith

The default recipe for a backend application with a database, REST API and an
optional UI.

```csharp
Bake.New
    .Monolith(...)
    .Run();
```

[See more](monolith.md)

#data-source

### Data Source

A recipe for a backend application that serves data from a configured database
using report queries in `.sql` files.

```csharp
Bake.New
    .DataSource(...)
    .Run();
```

[See more](data-source.md)

::

> [!TIP]
>
> To override a built-in configuration see
> [Overriding A Configuration](../architecture/application.md#overriding-a-configuration)

All recipes, features and layers are in the `Baked` package, and your domain
projects only need to reference `Baked.Abstractions`.
