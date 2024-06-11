# Monitoring

Baked uses the default logging library for now.

```csharp
app.Layers.AddMonitoring();
```

## Configuration Targets

`Monitoring` layer only exposes `ILoggingBuilder` for configuration target.

### `ILoggingBuilder`

This target is provided in `CreateBuilder` phase. To configure it in a feature;

```csharp
configurator.ConfigureLoggingBuilder(logging =>
{
    ...
});
```
