# Feature

Features consist of two parts; abstraction (port) and implementation (adapter).
Each adapter exposes a class that implements `IFeature` interface.

## Conventions

For a consistent developer experience, follow below conventions when
implementing a new feature;

### Abstraction

1. Place all abstraction classes under a folder named after feature, e.g.,
   `Greeting/`.
1. Provide a configurator and an extension class for abstraction part, e.g.,
   `Greeting/GreetingConfigurator.cs`, `Greeting/GreetingExtensions.cs`.
1. Provide an `Add` method to add feature to an application, e.g.,
   `AddGreeting()`.
   1. If feature allows multiple implementations, indicate this with a plural
      add method, e.g., `AddCodingStyles()`. Also accept a list of features
      instead of a single feature.

### Implementation

1. Place all implementation classes under their own folder in the abstraction
   folder, e.g., `Greeting/WelcomePage/`.
1. Provide an extension method with the implementation name to allow adding
   that implementation, e.g., `WelcomePage()`.
   1. This method should be in an extension class under `Baked` namespace, e.g.,
      `Greeting/WelcomePage/WelcomePageGreetingExtensions.cs`.
1. Name feature class after implementation name with abstraction name as a
   suffix, e.g., `WelcomePageGreetingFeature`.
1. Features depend on other features through their abstraction parts. Direct
   dependency between feature implementations is forbidden.
1. To create a configuration overrider, add an extension and feature class
   directly under the feature folder, e.g.,
   `ConfigurationOverrider/ConfigurationOverriderExtensions.cs` and
   `ConfigurationOverrider/ConfigurationOverriderFeature.cs`.
    1. Unlike regular features, provide `AddConfigurationOverrider()` extension
       method directly to allow `app.Features.AddConfigurationOverrider()`
       usage.
    1. Implement `IFeature` in `ConfigurationOverriderFeature` where you add
       all your configuration overrides.

Please refer to existing features in [github.com/mouseless/baked][] for
examples.

## Creating A Feature

To create a feature implementation, create a class using above conventions and
implement `IFeature<TConfigurator>` where `TConfigurator` is the configurator
class of your feature abstraction.

`WelcomePageGreetingFeature.cs`
```csharp
public class WelcomePageGreetingFeature : IFeature<GreetingConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        ...
    }
}
```

### `Id` of a Feature

`IFeature` has an `Id` property which should be unique per `Application`
instance. By default, value of this property is name of the implementing
feature.

You can override its value by implementing `IFeature.Id` property in feature
implementation class as shown below;

```csharp
public class WelcomePageGreetingFeature : IFeature<GreetingConfigurator>
{
    public string Id => "CustomFeatureId";
    ...
}
```

### Disabling a Feature

To allow a feature to be disabled, you can provide a `Disable` method in your
configurator which returns `Feature.Empty<TConfigurator>()`, this feautre does
not configure any layers.

`GreetingConfigurator.cs`
```csharp
public class GreetingConfigurator
{
    public IFeature<GreetingConfigurator> Disabled() =>
        Feature.Empty<GreetingConfigurator>();
}
```

## Configuring Layers

To configure layers, a `LayerCofigurator` instance is passed to the
`Configure()` method of the `IFeature` interface. Using extension methods on
the given configurator, a feature accesses configuration targets of layers.

`WelcomePageGreetingFeature.cs`
```csharp
public class WelcomePageGreetingFeature : IFeature<GreetingConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add(app => app.UseWelcomePage());
        });
    }
}
```

> [!NOTE]
>
> Layers allow up to three configuration targets per configuration action.

`Configure` method will be called multiple times, each time to configure a
different part of the application. `LayerConfigurator` ensures that given
configuration action is only applied to the related target, e.g.,
`IMiddlewareCollection` in the above code.

A layer might provide the same object in different configurators. For example,
`WebApplication` implements `IEndpointRouteBuilder` but `HttpServerLayer`
provides it with its interface not its concrete type.

> [!WARNING]
>
> Do __NOT__ cast given configuration objects to their other interfaces. A
> layer provides a separate extension method, e.g.,
> `ConfigureEndpointRouteBuilder()`.

> [!TIP]
>
> The order of the configuration calls does not have an effect in the outcome.
> Feel free to organize these calls in the way you like.

### Using Phase Artifacts

To access and use objects stored in application context in a feature,
`LayerConfigurator` provides `Use<T>` helper which will invoke given
action with context phase artifact.

```csharp
configurator.Configure<TTarget>(target =>
{
    configurator.Use<TContextTarget>(contextTarget =>{
        ...
    });
});
```

> [!WARNING]
>
> Unlike configuration targets, phase artifacts may or may not exists in the
> application context or not configured properly at the moment
> `LayerConfigurator` applies configurations. Phase execution orders and
> configurations should be taken into consideration when using phase artifacts.

### Including an Option

To include an option in a feature, take the option as a parameter in
configurator extension and pass it to the feature implementation as shown
below;

`WelcomePageGreetingFeature.cs`
```csharp
public class WelcomePageGreetingFeature(string _path)
    : IFeature<GreetingConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureApplicationBuilder(app =>
        {
            app.UseWelcomePage(_path);
        });
    }
}
```

`WelcomePageGreetingExtensions.cs`
```csharp
public static class WelcomePageGreetingExtensions
{
    public static WelcomePageGreetingFeature WelcomePage(
        this GreetingConfigurator source,
        string? path = default
    ) => new(path ?? "/");
}
```

[github.com/mouseless/baked]:https://github.com/mouseless/baked
