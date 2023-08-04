# Testing

DO uses _NUnit_ for unit testing, _Moq_ for mocking and _Shouldly_ for
assertion.

```csharp
app.AddTesting();
```

## Configuration Targets

`Testing` layer only exposes `TestConfiguration` for configuration target.

### `TestConfiguration`

This target is provided in `AddServices` phase. To configure it in a feature;

```csharp
configurator.ConfigureTestConfiguration(test =>
{
    ...
});
```

## Phases

This layer introduces following phases to the application it is added;

- `CreateConfigurationManager`: This phase runs as the earliest stage of a test
  run to add an empty `ConfigurationManager` to the application context.
