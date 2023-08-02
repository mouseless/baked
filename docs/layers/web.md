# Web

DO uses ASP.NET Core's `WebApplication` to build and host a web application.

```csharp
app.AddWeb();
```

## Configuration Targets

Web layer provides `IApplicationBuilder` and `IEndpointRouteBuilder` targets to
configure a web application.

### `IApplicationBuilder`

This target is provided in `Build` phase. To configure it in a feature;

```csharp
configurator.ConfigureApplicationBuilder(app =>
{
    ...
});
```

### `IEndpointRouteBuilder`

This target is provided in `Build` phase right after `IApplicationBuilder`. To
configure it in a feature;

```csharp
configurator.ConfigureEndpointRouteBuilder(routes =>
{
    ...
});
```

## Phases

This layer introduces following phases to the application it is added;

- `CreateBuilder`: This phase is the earliest phase in an application which
  creates a `WebApplicationBuilder` instance to the application context.
- `Build`: This phase adds all services from dependency injection layer to the
  services in `WebApplicationBuilder` instance. Then builds the app and adds
  `WebApplication` instance to the application context.
- `Run`: This phase is added internally and is the latest phase in an
  application. It is not allowed to provide a configuration at this phase since
  it only runs the `WebApplication` instance in the application context.
