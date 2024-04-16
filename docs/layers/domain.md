# Domain

DO introduces a model generation mechanism to reflect the business domain of
a project. The generated model instance can be used in directly in layers or
in features while configuring configuration targets.

```csharp
app.Layers.AddDomain();
```

## Configuration Targets

This layer provides `IDomainTypeCollection` and `DomainModelBuilderOptions` as
configuration targets for building `DomainModel`.

### `IDomainTypeCollection`

This target is provided in `AddDomainTypes` phase. To configure it in a feature;

```csharp
configurator.ConfigureDomainTypeCollection(types =>
{
    ...
});
```

### `DomainModelBuilderOptions`

This target exposes options for configuring built-in `DomainModelBuilder` and is
provided in `AddDomainTypes` phase. To configure it in a feature;

```csharp
configurator.ConfigureDomainBuilderOptions(options =>
{
    ...
});
```

## Phases

This layer introduces following phases to the application it is added;

- `AddDomainTypes`: This phase adds an `IDomainTypeCollection` instance to the
  application context.
- `BuildDomainModel`: This phase uses domain types to build and add a
  `DomainModel` instance to the application context.
