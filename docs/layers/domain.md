# Domain

Baked introduces a model generation mechanism to reflect the business domain of
a project. The generated model instance can be used directly in layers or in
features while configuring configuration targets.

```csharp
app.Layers.AddDomain();
```

## Configuration Targets

This layer provides `IDomainTypeCollection` and `DomainModelBuilderOptions`
configuration targets for building `DomainModel` in `Generate` mode. It also 
provides `DomainServiceCollection` configuration target for features to add
`DomainServiceDescriptor` for domain types which then be used to generate an
`IServiceAdder` implementation. The generated `IServiceAdder` is then 
used in `Start` mode for auto registering domain types to service collection.

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

### `DomainServiceCollection`

This target is provided in `GenerateCode` phase and it is used to generated 
`IServiceAdder` to add domain services during `AddService` phase in `Start` 
mode. To configure it in a feature;

```csharp
configurator.ConfigureDomainServiceCollection(services =>
{
    ...
});
```

## Phases

This layer introduces following `Generate` phases to the application it is added;

- `AddDomainTypes`: This phase adds an `IDomainTypeCollection` instance to the
  application context
- `BuildDomainModel`: This phase uses domain types to build and add a
  `DomainModel` instance to the application context
