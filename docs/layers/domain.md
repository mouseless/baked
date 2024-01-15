# Domain

DO introduces a model generation mechanism to reflect the business domain of
a project. The generated model instance can be used in directly in layers or 
in features while configuring configuration targets.

```csharp
app.Layers.AddDomain();
```

## Configuration Targets

This layer provides `IAssemblyCollection`, `ITypeCollection` and 
`DomainOptions` as configuration targets for building `DomainModel`.

### `IAssemblyCollection`

This target is provided in `BuildConfiguration` phase. To configure it in a 
feature;

```csharp
configurator.ConfigureAssemblyCollection(assemblies =>
{
    ...
});
```

### `ITypeCollection`

This target is provided in `BuildConfiguration` phase. To configure it in a 
feature;

```csharp
configurator.ConfigureTypeCollection(types =>
{
    ...
});
```

### `DomainOptions`

This target is provided in `BuildConfiguration` phase. To configure it in a 
feature;

```csharp
configurator.ConfigureDomainOptions(types =>
{
    ...
});
```

## Phases

This layer introduces following phases to the application it is added;

- `BuildDomain`: This phase adds an `DomainModel` instance to the application
  context and runs before `AddServices` to allow the access to `DomainModel`.
