# Feature

Features consist of two parts; abstraction (port) and implementation (adapter).
Each adapter exposes a class that implements `IFeature` interface.

## Conventions

For a consistent developer experience, follow below conventions when
implementing a new feature;

### Abstraction

1. Place all abstraction classes under a folder named after feature, e.g.,
   `Greeting/`.
1. Provide a configurator and an extension class for abstraction part, e.g.
   `Greeting/GreetingConfigurator.cs`, `Greeting/GreetingExtensions.cs`.
1. Provide an `Add` method to add feature to an application, e.g.,
   `AddGreeting()`.

### Implementation

1. Place all implementation classes under their own folder in the abstraction
   folder, e.g., `Greeting/HelloWorld/`, `Greeting/WelcomePage/`.
1. Provide an extension method with the implementation name to allow adding
   that implementation, e.g., `HelloWorld()`, `WelcomePage()`.
   1. This method should be in an extension class under `Do` namespace, e.g.,
      `Greeting/HelloWorldGreetingExtensions.cs`,
      `Greeting/WelcomePageGreetingExtensions.cs`.
1. Name feature class after implementation name with abstraction name as a
   suffix, e.g., `HelloWorldGreetingFeature`, `WelcomePageGreetingFeature`.
1. Features depend on other features through their abstraction parts. Direct
   dependency between feature implementations is forbidden.
1. To create configuration overrider add an extension and feature class under
   its folder, e.g., `ConfigurationOverriderExtensions.cs` and
   `ConfigurationOverriderFeature.cs`.
    1. Unlike regular features, provide `AddConfigurationOverrider()` extension method
       directly to allow `app.Features.AddConfigurationOverrider()` usage.
    1. Implement `IFeature` in `ConfigurationOverriderFeature` where you add all your
       configuration overrides.

Please refer to existing features in [github.com/mouseless/do][] for examples.

## Configuring Layers

To configure layers, a `LayerCofigurator` instance is passed to `Configure()`
method of `IFeature` interface. Using extension methods on the given
configurator, a feature accesses configuration targets of layers.

```csharp
public class WelcomePageGreetingFeature : IFeature
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

> :information_source:
>
> Layers allow up to three configuration targets per configuration action.

`Configure` method will be called multiple times, each time to configure a
different part of the application. `LayerConfigurator` ensures that given
configuration action is only applied to the related target, e.g.,
`IMiddlewareCollection` in the above code.

A layer might provide the same object in different configurators. For example,
`WebApplication` implements `IEndpointRouteBuilder` but `HttpServerLayer`
provides it with its interface not its concrete type.

> :warning:
>
> Do __NOT__ cast given configuration objects to their other interfaces. A
> layer provides a separate extension method, e.g.,
> `ConfigureEndpointRouteBuilder()`.

> :bulb:
>
> The order of the configuration calls does not have an effect in the outcome.
> Feel free to organize these calls in the way you like.

### Including an Option

To include an option in a feature, take the option as a parameter in
configurator extension and pass it to the feature implementation as shown
below;

`WelcomePageGreetingFeature.cs`
```csharp
public class WelcomePageGreetingFeature : IFeature
{
    readonly string _path;

    public WelcomePageGreetingFeature(string path) => _path = path;

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

### Disabling a Feature

To allow disabling a feature, provide a `Disabled()` method which returns an
empty feature.

`GreetingConfigurator.cs`
```csharp
public class GreetingConfigurator
{
    public IFeature Disabled() => Feature.Empty;
}
```

[github.com/mouseless/do]:https://github.com/mouseless/do
