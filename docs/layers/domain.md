# Domain

DO introduces a model generation mechanism to reflect the business domain of
a project. The generated model instance can be used in directly in layers or 
in features while configuring configuration targets.

```csharp
app.Layers.AddDomain();
```

## Configuration Targets

`Domain` layer only exposes `DomainDescriptor` for configuration
target.

### `DomainDescriptor`

This target serves as a configuration for `DomainModelBuilder` when generating
the `DomainModel` of a project and is provided in `ConfigureDomain` phase. To 
configure it in a feature;

```csharp
configurator.ConfigureDomainDescriptor(domainDescriptor =>
{
    ...
});
```

## Phases

This layer introduces following phases to the application it is added;

- `ConfigureDomain`: This phase runs before `BuildDomain` to allow the usage
  of `Settins` API and `DomainDescriptor` when generating `DomainModel`.
- `BuildDomain`: This phase adds an `DomainModel` instance to the application
  context and runs before `AddServices` to allow the access to `DomainModel`.
