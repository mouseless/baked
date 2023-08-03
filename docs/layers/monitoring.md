# Monitoring

DO uses the default logging library for now.

```csharp
app.AddMonitoring();
```

## Configuration Targets

Monitoring Layer only exposes ILoggingBuilder for configuration target.

### `ILoggingBuilder`

```csharp
configurator.ConfigureLoggingBuilder(logging =>
{
    ...
});
```

## Phases

This layer uses following phases;

- `CreateBuilder`