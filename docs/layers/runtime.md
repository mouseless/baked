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
`IServiceCollection`, `ServiceCollectionWrapper`, `IServiceProvider` and
`ThreadOptions` as configuration targets.

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

### `ServiceCollectionWrapper`

This target is provided in `ConfigureServices` phase. To configure it in a
feature;

```csharp
configurator.ConfigureServiceCollection(configuration =>
{
    ...
}, afterAddServices: true);
```

> [!NOTE]
>
> This is just a wrapper target that provides `IServiceCollection` in a phase
> after `AddServices` so that you can make extra `services.Configure...` calls
> after `services.Add...` calls.

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
- `ConfigureServices`: This phase depends on a `IServiceCollection` instance to be added
  so that there is an extra phase after `AddServices` is done and before
  `IServiceProvider` is built
- `PostBuild`: This phase depends on a `IServiceProvider` instance to be added
  to application context so that it can be provided from this layer
