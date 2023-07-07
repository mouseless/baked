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

Layers provide configuration objects phase by phase. This means a layer can
provide two different configuration objects for two different phases. For this
reason, a layer returns `PhaseContext` instance per phase it provides
configuration to.

```csharp
public class LayerX : LayerBase<AddServices>
{
    LayerXConfiguration _configuration = new();

    protected override PhaseContext GetContext(AddServices phase) =>
        phase.CreateContext(_configuration);
}
```

In this example, you see a layer named `LayerX` providing a
`LayerXConfiguration` instance during `AddServices` phase.

> :information_source:
>
> By default, a layer returns `PhaseContext.Empty` instance for the phases it
> does not provide a configuration. This means `Application` skips that layer
> for the phases it doesn't have anything to configure.

### Using non-generic `LayerBase`

`LayerBase<>` classes allow up to three generic arguments. If you need to
implement a layer that has things to configure during more than three phases,
use non-generic `LayerBase` class, override `GetContext(IPhase phase)` method
and switch given phase according to its type;

```csharp
public class MyLayer : LayerBase
{
    protected override PhaseContext GetContext(IPhase phase) =>
        phase switch
        {
            PhaseA => phase.CreateContext(...),
            PhaseB => phase.CreateContext(...),
            _ => base.GetContext(phase)
        };
}
```

### Before and After a Phase

Layers might need to do some standard stuff before and after a phase is applied
to all features for all layers. Use `GetContext()` method to do stuff before,
and `onDispose:` delegate to do stuff after.

```csharp
public class LayerX : LayerBase<AddServices>
{
    LayerXConfiguration _configuration = new();

    protected override PhaseContext GetContext(AddServices phase)
    {
        var services = Context.Get<IServiceCollection>();

        services.AddStandardStuffBefore();

        phase.CreateContext(_configuration,
            onDispose: () => services.AddStandardStuffAfter()
        );
    }
}
```

> :information_source:
>
> Notice that a layer has access to the `ApplicationContext` instance through
> `Context` property provided in `LayerBase` base class.

> :warning:
>
> `GetContext()` method is called for every layer before applying any of them
> to features. And `onDispose:` actions are called after applying these
> contexts to all features.

### Providing Multiple Targets

It is possible to provide up to three configuration objects for the same phase.
There are two ways of providing multiple targets; at once or one by one.

#### At Once

Below code demonstrates providing two configuration objects at once;

```csharp
public class LayerX : LayerBase<AddServices>
{
    Configuration1 _configuration1 = new();
    Configuration2 _configuration2 = new();

    protected override PhaseContext GetContext(AddServices phase) =>
        phase.CreateContext(_configuration1, _configuration2);
}
```

This phase context will require an action with two parameter in a feature;

```csharp
...
public void Configure(LayerConfigurator configurator)
{
    configurator.Configure((Configuration1 configuration1, Configuration2 configuration2) =>
    {
        // use both objects to configure
    });
}
...
```

See [Feature](./feature.md) for more information on using layer configurators
for features.

#### One by One

Below code demonstrates providing configuration objects one by one;

```csharp
public class LayerX : LayerBase<AddServices>
{
    Configuration1 _configuration1 = new();
    Configuration2 _configuration2 = new();

    protected override PhaseContext GetContext(AddServices phase) =>
        phase.CreateContextBuilder()
            .Add(_configuration1)
            .Add(_configuration2)
            .Build()
        ;
}
```

This phase context will require two different actions in a feature;

```csharp
...
public void Configure(LayerConfigurator configurator)
{
    configurator.Configure((Configuration1 configuration1) =>
    {
        // configure one by one
    });

    configurator.Configure((Configuration2 configuration2) =>
    {
        // configure one by one
    });
}
...
```

> :bulb:
>
> `phase.CreateContext()` is a helper method that utilizes
> `phase.CreateContextBuilder()` behind the scenes.

#### Using Two Ways In Combination

You may combine these two ways to provide configuration;

```csharp
public class LayerX : LayerBase<AddServices>
{
    Configuration1 _configuration1 = new();
    Configuration2 _configuration2 = new();
    Configuration3 _configuration3 = new();

    protected override PhaseContext GetContext(AddServices phase) =>
        phase.CreateContextBuilder()
            .Add(_configuration1, _configuration2)
            .Add(_configuration3)
            .Build()
        ;
}
```

This phase context will require two different actions;

1. For the first two configuration objects: `(Configuration1 configuration1,
   Configuration2 configuration2)`
1. For the last configuration object: `(Configuration3 configuration3)`.

## Conventions

> TBD
