---
position: 1
---

# Application

`Application` acts as a container object for all the domain objects, features
and layers of the software you develop. This object is built through a bunch of
extensible builder classes and methods.

## Building an Application

DO provides a fluent interface to build an application. You can access it
through `Build.Application` under `Do` namespace. Below is a sample
`Program.cs` that builds and runs a DO application;

```csharp
Build.Application
    .As(app =>
    {
        ...
    });
    .Run();
```

> :information_source:
>
> `Do` is automatically added as a global using so that you can directly make a
> call to `Build.Application`.

## Adding Extensions

To add a new extension to an application, you need to make use of feature /
layer system.

DO does not allow you to use `build.Services.Add` or `app.Use` methods
directly. This is a design decision to avoid any unexpected behaviour because
the order of extensions may require a special attention as in [Enabling
Cors][].

`As` method provides an `Application` instance with `Layers` and `Features`
properties. Using these properties you may add needed extensions into your
application.

> :information_source:
>
> Layers and features come with extension methods exposed directly in `Do`
> namespace so that you can see options without adding an extra `using`.

### Layers

Layers can be added without any options to configure;

```csharp
app.Layers.AddDomain();
app.Layers.AddHttp();
app.Layers.AddDatabase();
```

To configure a layer, you need to add a feature mentioned in the next section.
More on layers can be found at [Layer](layer.md).

### Features

Features are added using one of the implementations available in given
configurator;

```csharp
app.Features.AddApi(c => c.Rest());
app.Features.AddSql(c => c.EfCore());
```

An implementation may ask for additional options within its configurator
method;

```csharp
app.Features.AddSql(c => c.EfCore(primaryKeyPrefix: "PK_"))
```

## Running an Application

> TBD

[Enabling Cors]:https://learn.microsoft.com/en-us/aspnet/core/security/cors#enable-cors
