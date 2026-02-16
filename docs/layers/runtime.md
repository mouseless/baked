# Runtime

This layer serves as foundation and provides common libraries for an
application. Baked uses .NET's standard libraries for providing an environment
based configuration mechanism, dependency injection container for inversion of
control and logging mechanism for monitoring and diagnostic purposes.

```csharp
app.Layers.AddRuntime();
```

## Configuration Targets

`Runtime` layer provides `IConfigurationBuilder`, `ILoggingBuilder`,
`IServiceCollection`, `IServiceProvider` and `ThreadOptions` as configuration
targets.

### `IConfigurationBuilder`

This target is provided in `BuildConfiguration` phase. To configure it in a
feature;

```csharp
configurator.ConfigureConfigurationBuilder(configuration =>
{
    ...
});
```

### `ILoggingBuilder`

This target is provided in `AddServices` phase. To configure it in a feature;

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

### `ThreadOptions`

This target is provided in add `AddServices` phase. When configured, min and
max `workerthreads` and `completionPortThreads` values are set from
`MinThreadCount` and `MaxThreadCount` value as `1x` and `2x` respectively

## Phases

This layer introduces following `Start` phases to the application it is added;

- `BuildConfiguration`: This phase runs in the earliest stage to allow the usage
  of `Settings` API from features
- `AddServices`: This phase creates a `IServiceCollection` instance and places
  it in the application context
- `PostBuild`: This phase depends on a `IServiceProvider` instance to be added
  to application context so that it can be provided from this layer
