# Http Server

DO uses ASP.NET Core's `WebApplication` to build and host a web application.

```csharp
app.AddHttpServer();
```

## Configuration Targets

`HttpServer` layer provides `IMiddlewareCollection` and `IEndpointRouteBuilder`
targets to configure a web application.

### `IMiddlewareCollection`

This target is provided in `Build` phase as the first target. To configure it in
a feature;

```csharp
configurator.ConfigureMiddlewareCollection(middlewares =>
{
    ...
});
```

### `IEndpointRouteBuilder`

This target is provided in `Build` phase right after `IMiddlewareCollection`. To
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
  creates and adds a `WebApplicationBuilder` instance to the application context
  along with a `ConfigurationManager` instance.
- `Build`: This phase adds all services from dependency injection layer to the
  services in `WebApplicationBuilder` instance. Then builds the app and adds
  the `WebApplication` instance to the application context along with the
  `IServiceProvider`.
- `Run`: This phase is added internally and is the latest phase in an
  application. It is not allowed to provide a configuration at this phase since
  it runs the `WebApplication` instance during phase initialization.
