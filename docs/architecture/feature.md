# Feature

Features consist of two parts; abstraction and implementation. Each
implementation is an extension method that defines an `IFeature` using the
configurator of its abstraction.

## Defining A Feature

To define a feature implementation, create an extension method to the
configurator of its abstraction and call `Define()` in it. See [Guide](#guide)
for naming and placing it.

```csharp
public static IFeature<GreetingFeature.Configurator> Welcome(this GreetingFeature.Configurator x) =>
    x.Define(configurator =>
    {
        ...
    });
```

Then use it when adding the feature to an application;

```csharp
app.Features.AddGreeting(c => c.Welcome());
```

> [!NOTE]
>
> `Define()` returns an `IFeature<TConfigurator>`, so all feature
> implementations implement `IFeature` behind the scenes.

### `Id` of a Feature

`IFeature` has an `Id` property which should be unique per `Application`
instance. By default, value of this property is the name of the method defining
it, prefixed with its abstraction name, e.g., `Greeting.Welcome`.

You can give an id explicitly when defining a feature inline;

```csharp
app.Features.AddGreeting(c => c.Define("MyGreeting", configurator =>
{
    ...
}));
```

This feature will have `Greeting.MyGreeting` as its id.

> [!NOTE]
>
> Since ids are prefixed with abstraction names, features defined with the same
> name in different abstractions have different ids, e.g., `Core.Mock` and
> `Communication.Mock`.

### Disabling a Feature

Every feature configurator provides a `Disabled()` method that defines a feature
which does not configure any layers.

```csharp
app.Features.AddGreeting(c => c.Disabled());
```

This feature will have `Greeting.Disabled` as its id.

## Configuring Layers

To configure layers, a `LayerConfigurator` instance is passed to the action
given to `Define()`. Using extension methods on the given configurator, a
feature accesses configuration targets of layers.

`WelcomeGreetingFeature.cs`
```csharp
public static IFeature<GreetingFeature.Configurator> Welcome(this GreetingFeature.Configurator x) =>
    x.Define(configurator =>
    {
        configurator.HttpServer.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add(app => app.UseWelcomePage());
        });
    });
```

> [!NOTE]
>
> Layers allow up to three configuration targets per configuration action.

Given action will be called multiple times, each time to configure a different
part of the application. `LayerConfigurator` ensures that given configuration
action is only applied to the related target, e.g., `IMiddlewareCollection` in
the above code.

A layer might provide the same object in different configurators. For example,
`WebApplication` implements `IEndpointRouteBuilder` but `HttpServerLayer`
provides it with its interface not its concrete type.

> [!WARNING]
>
> Do __NOT__ cast given configuration objects to their other interfaces. A layer
> provides a separate extension method, e.g., `ConfigureEndpointRouteBuilder()`.

> [!TIP]
>
> The order of the configuration calls does not have an effect in the outcome.
> Feel free to organize these calls in the way you like.

### Using Phase Artifacts

To access and use objects stored in application context in a feature,
`LayerConfigurator` provides `Use<T>` helper which will invoke given action with
context phase artifact.

```csharp
configurator.RestApi.ConfigureApiModel(api =>
{
    configurator.Domain.UsingDomainModel(domain =>
    {
        ...
    });
});
```

> [!WARNING]
>
> Unlike configuration targets, phase artifacts may or may not exist in the
> application context or not configured properly at the moment
> `LayerConfigurator` applies configurations. Phase execution orders and
> configurations should be taken into consideration when using phase artifacts.

> [!TIP]
>
> Use phase artifacts within a configuration action as shown above, not directly
> in the action given to `Define()`.

### Keeping a State

Since the action given to `Define()` is called multiple times, a variable
declared in it does not survive between calls. To keep a state across
configurations, give it to `Define()` as an argument. Arguments are evaluated
once and given to every call;

```csharp
public static IFeature<GreetingFeature.Configurator> Welcome(this GreetingFeature.Configurator x) =>
    x.Define(new VisitCounter(), (configurator, visits) =>
    {
        configurator.HttpServer.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add(app => app.UseWelcomePage(visits));
        });

        configurator.Runtime.ConfigureServiceCollection(services =>
        {
            services.AddSingleton(visits);
        });
    });
```

When the state is assigned in a configuration action, declare it before
`Define()` instead;

```csharp
public static IFeature<GreetingFeature.Configurator> Welcome(this GreetingFeature.Configurator x)
{
    var greeting = string.Empty;

    return x.Define(configurator =>
    {
        configurator.Configure((GreetingOptions options) => greeting = options.Text);
        ...
    });
}
```

> [!CAUTION]
>
> Do __NOT__ use static fields to keep a state. They are shared by all
> applications in the same process, e.g., in tests.

### Including an Option

To include an option in a feature, take the option as a parameter in the
extension method and give it to `Define()` with its default value. The action
receives it with the same name;

`WelcomeGreetingFeature.cs`
```csharp
public static IFeature<GreetingFeature.Configurator> Welcome(this GreetingFeature.Configurator x,
    string? path = default
) => x.Define(path ?? "/", (configurator, path) =>
{
    configurator.HttpServer.ConfigureMiddlewareCollection(middlewares =>
    {
        middlewares.Add(app => app.UseWelcomePage(path));
    });
});
```

For more than one option, give them as a tuple and deconstruct it in the action;

```csharp
public static IFeature<GreetingFeature.Configurator> Welcome(this GreetingFeature.Configurator x,
    string? path = default,
    string? title = default
) => x.Define((
    path ?? "/",
    title ?? "Welcome"
), (configurator, args) =>
{
    var (path, title) = args;

    ...
});
```

> [!WARNING]
>
> Do __NOT__ apply defaults in the action given to `Define()`, e.g., `path ??=
> "/"`. The action is called multiple times and this assigns a variable that is
> shared by all calls.

## Guide

For a consistent developer experience, follow below conventions when
implementing a new feature;

```
Greeting/
├─ GreetingFeature.cs            // abstraction, in Baked namespace
├─ IGreetingService.cs           // in Baked.Greeting namespace
└─ Welcome/
   ├─ WelcomeGreetingFeature.cs  // implementation, in Baked namespace
   └─ WelcomeMiddleware.cs       // in Baked.Greeting.Welcome namespace
```

`GreetingFeature.cs`
```csharp
namespace Baked;

public static class GreetingFeature
{
    public static void AddGreeting(this List<IFeature> features,
        FeatureFunc<Configurator> configure
    ) => features.Add(configure(new()));

    public class Configurator : FeatureConfigurator<Configurator>;
}
```

`CodingStyleFeature.cs`
```csharp
namespace Baked;

public static class CodingStyleFeature
{
    // plural add method when multiple implementations are allowed
    public static void AddCodingStyles(this List<IFeature> features,
        IEnumerable<FeatureFunc<Configurator>> configures
    ) => features.AddRange(configures.Select(configure => configure(new())));

    public class Configurator : FeatureConfigurator<Configurator>;
}
```

`WelcomeGreetingFeature.cs`
```csharp
namespace Baked;

public static class WelcomeGreetingFeature
{
    public static IFeature<GreetingFeature.Configurator> Welcome(this GreetingFeature.Configurator x) =>
        x.Define(configurator =>
        {
            ...
        });
}
```

> [!WARNING]
>
> Features depend on other features through their abstraction parts. Direct
> dependency between feature implementations is forbidden.

> [!TIP]
>
> If nesting gets too deep, move inner lambdas to private static methods in the
> same class.

### Overrides

```
Override/
├─ OverrideFeature.cs                    // AddOverrides(), in Baked namespace
└─ Runtime/
   └─ ServicesRuntimeOverrideFeature.cs  // one folder per layer to configure
```

`OverrideFeature.cs`
```csharp
namespace Baked;

public static class OverrideFeature
{
    public static void AddOverrides(this List<IFeature> features)
    {
        features.Add(new ServicesRuntimeOverrideFeature());
        ...
    }
}
```
