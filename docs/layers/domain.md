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
configurator.ConfigureDomainModelBuilder(builder =>
{
    ...
});
```

### `DomainServiceCollection`

This target is provided in `GenerateCode` phase and it is used to generated
`IServiceAdder` to add domain services during `AddService` phase in `Start`
mode. To configure it in a feature;

```csharp
configurator.ConfigureDomainServiceCollection((services, domain) =>
{
    // use domain metadata to register services at generate time
    ...
});
```

## Phases

This layer introduces following `Generate` phases to the application it is added;

- `AddDomainTypes`: This phase adds an `IDomainTypeCollection` instance to the
  application context
- `BuildDomainModel`: This phase uses domain types to build and add a
  `DomainModel` instance to the application context

> [!TIP]
>
> To access the domain model from a feature use below extension method;
>
> ```csharp
> configurator.UsingDomainModel(domain =>
> {
>     // use domain metadata to configure any configuration target
>     ...
> });
> ```

## Proxifying Entities

It is possible to avoid adding `protected virtual` and default constructors to
classes (such as entity classes) to enable lazy loading and dynamic proxy.

Please add below references to your projects that contain your domain objects
(projects that depend only to `Baked.Abstractions`).

```xml
<ItemGroup>
  <PackageReference Include="EmptyConstructor.Fody" PrivateAssets="All" />
  <PackageReference Include="Fody" PrivateAssets="All" />
  <PackageReference Include="Publicize.Fody" PrivateAssets="All" />
  <PackageReference Include="Virtuosity.Fody" PrivateAssets="All" />
</ItemGroup>
```

Add versions to `Directory.Packages.props`;

```xml
<PackageVersion Include="EmptyConstructor.Fody" Version="..." />
<PackageVersion Include="Fody" Version="..." />
<PackageVersion Include="Publicize.Fody" Version="..." />
<PackageVersion Include="Virtuosity.Fody" Version="..." />
```

> [!WARNING]
>
> Build your project now. Expect a build fail on your first build after you add
> fody. This fail adds `FodyWeavers.xml` to your project. Following builds will
> success.

> [!TIP]
> You can use `GenerateXsd="false"` property in your `FodyWeavers.xml` to remove
> the extra `.xsd` file
>
> ```xml
> <?xml version="1.0" encoding="utf-8"?>
> <Weavers GenerateXsd="false">
>   <EmptyConstructor />
>   <Publicize />
>   <Virtuosity />
> </Weavers>
> ```
