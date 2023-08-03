# Monitoring

DO uses the default logging library for now.

```csharp
app.AddMonitoring();
```

## Configuration Targets

`Monitoring` layer only exposes `ILoggingBuilder` for configuration target.

### `ILoggingBuilder`

```csharp
configurator.ConfigureLoggingBuilder(logging =>
{
    ...
});
```
