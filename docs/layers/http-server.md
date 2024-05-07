# HTTP Server

DO uses ASP.NET Core's `WebApplication` to build and host a web application.

```csharp
app.Layers.AddHttpServer();
```

## Configuration Targets

`HttpServer` layer provides `IAuthenticationCollection`, 
`IMiddlewareCollection` and `IEndpointRouteBuilder` targets to configure a web 
application.

### `IAuthenticationCollection`

This target is provided in `AddServices` phase. To configure it in a feature;

```csharp
configurator.ConfigureAuthenticationCollection(authentcation =>
{
    ...
});
```

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
  along with a `ConfigurationManager` instance
- `Build`: This phase adds all services from dependency injection layer to the
  services in `WebApplicationBuilder` instance, then builds the app and adds
  `WebApplication` and `IServiceProvider` instances to the application context
- `Run`: This phase is added to the application internally as the latest phase
  to run the `WebApplication`
