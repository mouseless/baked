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

> :warning:
>
> Notice that it does not allow you to use `build.Services.Add` or `app.Use`
> methods directly. We made this design decision because the order of
> `Add`/`Use` calls depends on the features you need to use. Since DO comes
> with a set of features already added, it may cause unexpected behaviour.

`As` method provides an `Application` instance with `Layers` and `Features`
properties. Using these properties you may add needed extensions to your
application.

### Layers

Layers introduce new technologies into your application without any options to
configure;

```csharp
Build.Application
    .As(app =>
    {
        app.Layers.AddDomain();
        app.Layers.AddHttp();
        app.Layers.AddDatabase();
    });
```

> :information_source:
>
> Layers come with extension methods exposed directly in `Do` namespace so that
> you can see your layer options without adding an extra `using`.

To configure a layer, you need to add a feature mentioned in the next section.
More on layers can be found at [Layer](layer.md).

### Features

> TBD

## Running an Application

> TBD
