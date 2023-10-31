# Domain

DO introduces a model generation mechanism to reflect the business domain of
a project. The generated model instance can be used in directly in layers or 
in features while configuring configuration targets.

```csharp
app.Layers.AddDomain();
```

## Configuration Targets

`Domain` layer only exposes `DomainDescriptor` for configuration target.

### `DomainDescriptor`

This target serves as a configuration for when building the `DomainModel`. 
To configure it in a feature;

```csharp
configurator.ConfigureDomainDescriptor(domainDescriptor =>
{
    ...
});
```

## Phases

This layer introduces following phases to the application it is added;

- `BuildDomain`: This phase adds an `DomainModel` instance to the application
  context and runs before `AddServices` to allow the access to `DomainModel`.
