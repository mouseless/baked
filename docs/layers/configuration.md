# Configuration

DO uses .NET's standard configuration system to provide a environment based
configuration mechanism.

```csharp
app.Layers.AddConfiguration();
```

## Configuration Targets

`Configuration` layer only exposes `IConfigurationBuilder` for configuration
target.

### `IConfigurationBuilder`

This target is provided in `BuildConfiguration` phase. To configure it in a
feature;

```csharp
configurator.ConfigureConfigurationBuilder(configurationBuilder =>
{
    ...
});
```

## Phases

This layer introduces following phases to the application it is added;

- `BuildConfiguration`: This phase runs in the earliest stage to allow the usage
  of `Settings` API from features
