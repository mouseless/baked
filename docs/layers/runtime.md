# Runtime

This layer provides configuration, dependency injection, monitoring components 
which provide a foundation to an application. Baked uses default 
dependency injection and logging libraries
 

```csharp
app.Layers.AddRuntime();
```

## Configuration Targets

Runtime layer provides `ILoggingBuilder`, `IServiceCollection` and `IServiceProvider`
as configuration targets.

### `ILoggingBuilder`

This target is provided in `CreateBuilder` phase. To configure it in a feature;

```csharp
configurator.ConfigureLoggingBuilder(logging =>
{
    ...
});
```

### `IServiceCollection`

This target is provided in `AddServices` phase. To configure it in a feature;

```csharp
configurator.ConfigureServiceCollection(services =>
{
    ...
});
```

### `IServiceProvider`

This target is provided in `PostBuild` phase. To configure it in a feature;

```csharp
configurator.ConfigureServiceProvider(sp =>
{
    ...
});
```

## Phases

This layer introduces following phases to the application it is added;

- `AddServices`: This phase creates a `IServiceCollection` instance and places
  it in the application context
- `PostBuild`: This phase depends on a `IServiceProvider` instance to be added
  to application context so that it can be provided from this layer
