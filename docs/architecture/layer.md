---
position: 2
---

# Layer

Each `ILayer` implementation represents an internal system component of the
software you develop. A layer provides application features with configuration
of this system component so that the features can bind domain objects and
system components together.

Let's take relational database as an example. A relational database is treated
as an internal system component and is introduced by the `DatabaseLayer` along
with a configuration API. This API enables a feature to map your domain
entities to database tables.

Another good example is the `WebLayer`. This layer introduces the ASP.NET Core
framework into your application. It provides three phases, as mentioned in
[Application](./application.md), along with `IApplicationBuilder` and
`IEndpointRouteBuilder` objects as its configuration API. This way, any feature
has the ability to use a middleware or add routes to the application.

> :bulb:
>
> You can directly implement provided interfaces `ILayer` and `IPhase`, however
> we've created some base classes to make it easier for you to create layers
> and phases. All examples below, demonstrate usage of these base classes.

## Adding Phases

By default `LayerBase` returns no phases. To add one or more phases into the
application, you need to override `GetPhases()` method as shown below;

```csharp
public class SampleLayer : LayerBase
{
    protected override IEnumerable<IPhase> GetPhases()
    {
        yield return new DoA();
        yield return new DoB();
    }

    public class DoA : PhaseBase { ... }
    public class DoB : PhaseBase { ... }
}
```

Here `SampleLayer` adds two phases, `DoA` and `DoB`, to the application.

> :bulb:
>
> By convention place the phase implementations as a nested class in the layer
> it is added.

### Initializing a Phase

`PhaseBase` class has `Initialize()` method to override so that it can add an
object to the application context via `Context` property. Below, you can see
how `CreateBuilder` phase adds `WebApplicationBuilder` to the context;

```csharp
public class CreateBuilder : PhaseBase
{
    protected override void Initialize()
    {
        var build = WebApplication.CreateBuilder();

        Context.Add(build);
    }
}
```

### Readiness via Dependencies

You can define context dependencies via generic `PhaseBase<>` classes to make
the phase wait until context is ready to provide that dependency. For example,
`BuildApp` depends on the `WebApplicationBuilder` instance as shown below;

```csharp
public class BuildApp : PhaseBase<WebApplicationBuilder>
{
    protected override void Initialize(WebApplicationBuilder build)
    {
        var app = build.Build();

        Context.Add(app);
    }
}
```

`PhaseBase<T>` class requires `Initialize(T t)` method to be implemented.

> :information_source:
>
> You can provide more than one dependency for a phase. E.g., `Phase<X, Y>`
> will require `Initialize(X x, Y y)` method to be implemented.

### Order of a Phase

`PhaseBase` classes has `order` parameter in their constructors. Default order
is `PhaseOrder.Normal`. If you need to change the order, pass the desired order
to this parameter as shown below;

```csharp
public class DoThisEarlyOn : PhaseBase
{
    public DoThisEarlyOn() : base(PhaseOrder.Early) { }
}
```

## Providing Configuration

> TBD

## Conventions

> TBD
