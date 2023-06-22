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

Notice that DO does not allow you to use `build.Services.Add` `app.Use` methods
directly. We made this design decision because the order of `Add`/`Use` calls
depends on the features you need to use. Since DO comes with a set of features
already added, it may cause unexpected behaviour. To add a new feature to a DO
application, you need to make use of feature/layer system.
