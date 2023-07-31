# Dependency Injection

DO uses the default dependency injection library for now.

```csharp
app.AddDependencyInjection();
```

## Configuration Targets

Dependency injection layer provides `IServiceCollection` as the only
configuration target.

### `IServiceCollection`

This target is provided in `AddServices` phase. To configure it in a feature;

```csharp
configurator.ConfigureServiceCollection(services =>
{
    ...
});
```

## Phases

This layer introduces following phases to the application it is added;

- `AddServices`: This phase creates a `IServiceCollection` instance and places
  it in the application context
