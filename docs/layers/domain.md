# Domain

Baked introduces a model generation mechanism to reflect the business domain of
a project. The generated model instance can be used directly in layers or in
features while configuring configuration targets.

```csharp
app.Layers.AddDomain();
```

> [!NOTE]
>
> The generated domain metadata files will be saved to `.baked` folder at
> `$(ProjectDir)` of your application project.
>
>```xml
> <Target Name="SetCopyComponentDescriptors" BeforeTargets="Generate">
>   <PropertyGroup>
>     <CopyExportFiles>true</CopyExportFiles>
>     ...
>   </PropertyGroup>
> </Target>
>```

## Configuration Targets

This layer provides `IDomainTypeCollection` and `DomainModelBuilderOptions`
configuration targets for building `DomainModel`, `AttributeDatas` and
`ExportConfigurations` for exporting attribute metadata in `Generate` mode. It
also provides `DomainServiceCollection` configuration target for features to add
`DomainServiceDescriptor` for domain types which then be used to generate an
`IServiceAdder` implementation. The generated `IServiceAdder` is then used in
`Start` mode for auto registering domain types to service collection. Domain
layer also provides an `Inspect` object to inspect on metadata while
`DomainModelBuilder` builds the domain model through conventions.

### `Inspect`

> [!WARNING]
>
> This feature is still in experimentation and might print false-negative
> output, meaning it might not capture every change of the inspected attribute.

This target is provided in `AddDomainTypes` phase. To configure it in a feature;

```csharp
configurator.Domain.ConfigureInspect(inspect =>
{
    // To inspect an attribute on types
    inspect.TypeAttribute<MyAttribute>(
        when: c => c.Type..., // optional to inspect specific type models
        attribute: ma => ma.Value // optional to inspect just this value
    );

    // To inspect an attribute properties
    inspect.PropertyAttribute<MyAttribute>(
        when: c => c.Property..., // optional to inspect specific property models
        attribute: ma => ma.Value // optional to inspect just this value
    );

    // To inspect an attribute methods
    inspect.MethodAttribute<MyAttribute>(
        when: c => c.Method..., // optional to inspect specific method models
        attribute: ma => ma.Value // optional to inspect just this value
    );

    // To inspect an attribute parameters
    inspect.ParameterAttribute<MyAttribute>(
        when: c => c.Parameter..., // optional to inspect specific parameter models
        attribute: ma => ma.Value // optional to inspect just this value
    );

    // To inspect an attribute any member
    inspect.Attribute<MyAttribute>(
        when: c => c..., // optional to inspect specific members
        attribute: ma => ma.Value // optional to inspect just this value
    );
});
```

> [!NOTE]
>
> Only one inspect is allowed. If you configure more than one,
> `InvalidOperationException` will be thrown

### `IDomainTypeCollection`

This target is provided in `AddDomainTypes` phase. To configure it in a feature;

```csharp
configurator.Domain.ConfigureDomainTypeCollection(types =>
{
    ...
});
```

### `DomainModelBuilderOptions`

This target exposes options for configuring built-in `DomainModelBuilder` and is
provided in `AddDomainTypes` phase. To configure it in a feature;

```csharp
configurator.Domain.ConfigureDomainModelBuilder(builder =>
{
    ...
});
```

### `DomainServiceCollection`

This target is provided in `Generate` phase and it is used to generate
`IServiceAdder` to add domain services during `AddService` phase in `Start`
mode. To configure it in a feature;

```csharp
configurator.Domain.ConfigureDomainServiceCollection((services, domain) =>
{
    // use domain metadata to register services at generate time
    ...
});
```

### `AttributeProperties`

This target is provided in `Generate` phase and it is used to configure exported
properties for attributes;

```csharp
configurator.Domain.ConfigureAttributeProperties(properties =>
{
    // configure to output desired attribute properties
    ...
});
```

### `ExportConfigurations`

This target is provided in `Generate` phase and it is used to export attribute
data of matching types and their members. To configure it in a feature;

```csharp
configurator.Domain.ConfigureExportConfigurations(exports =>
{
    // configure exports to output desired attribute export files
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

## DomainModel

`DomainModel` is a reflection cache that stores and reuses type metadata,
properties, methods, parameters, and attribute information. Since baked relies 
on dynamic code generation based on certain set of rules or conventions, 
`DomainModel` serves as the core foundation of the system by providing 
a reusable and extendable reflection metadata.

### Extending DomainModel

Baked utilizes the `Attribute` system to mark or add additional metadata to
reflected types, members, or parameters. All models defined within the 
`DomainModel` has their own attributes collection initialized with dotnet
or user provided attributes, which allows layers and features to define custom 
behaviors, metadata, or runtime behaviours.

In order to create a specific set of rules or behaviors, `DomainLayer` provides 
convention based configuration mechanism which are configured using 
`DomainModeBuilder` configuration target's `Conventions`. 

### Indexing Models

Baked provides indexing mechanism of domain models according to their owned 
or added attributes to improve performance. Indicies of a model in domain can 
be specified from its builder options.

```csharp
configurator.Domain.ConfigureDomainModelBuilder(builder =>
{
    builder.Index.Type.Add<ServiceAttribute>();
    builder.Index.Method.Add<InitializerAttribute>();
    builder.Index.Property.Add<IdAttribute>();
}
```

#### Utilizing Conventions

Attributes can be directly added to types or members as well as using built-in
convetion system of baked. A convention can be used to add/remove or configure
an attribute. Baked provides `IDomainModelConvention<TModel>` to create custom
convention classes and extension methods for `DomainModelConvetionCollection` 
to manage attributes.

```csharp
public class IdConvention : IDomainModelConvention<PropertyModelContext>
{
    public void Apply(PropertModelContext context) 
    {
        if(c.Property.Name != "Id") { continue; }

        ((IMutableAttributeCollection).Property.CustomAttributes).Add(new IdAttribute());
    }
}

configurator.Domain.ConfigureDomainModelBuilder(builder =>
{
    // Adding an implemented convention
    builder.Conventions.Add(new IdConvention());

    // Adding convention via extensions
    builder.Conventions.SetPropertyAttribute(
        when: c => c.Property.Name == "Id"
        attribute: () => new IdAttribute()
    );
}
```

#### Convention Execution Order

By deault a convention is applied in the order which it is added with respect to
its feature order. A global value can be also set when a specific convention is
required to execute at the exact order. 

```csharp
// program.cs
app.Features.Add(new FeatureA());
app.Features.Add(new FeatureB());

public class FeatureB : IFeature 
{
    builder.Conventions.SetPropertyAttribute(
        when: ...,
        attribute: ...
    );
}

public class FeatureB : IFeature 
{
    // This convention will apply first
    builder.Conventions.SetPropertyAttribute(
        when: ...,
        attribute: ...,
        order: int.minValue + 10
    );

    // This convention will apply last
    builder.Conventions.SetPropertyAttribute(
        when: ...,
        attribute: ...
    );
}
```

Another key factor that affects convention execution order is whether a
convention should execute before or after indices are built. Some conventions
may need to modify metadata or add attributes before the indexing stage begins,
while remaining may depend on generated indices.To support this behavior, 
conventions can be marked with the before index flag. These conventions are 
grouped and executed in a separate stage, guaranteeing that they run before 
index generation and all remaining conventions.

```csharp
// This convention will apply after the indicies are built
builder.Conventions.SetPropertyAttribute(
    when: ...,
    attribute: ...,
    order: int.MinValue + 10,
);

// This convention will apply before the indicies are built
builder.Conventions.SetPropertyAttribute(
    when: ...,
    attribute: ...,
    order: int.MaxValue - 10;
    beforeIndex: true
);
```

Baked also provides a level system that allows conventions to be grouped and 
executed within a specific stage. This helps organize convention execution 
accross multiple features and provide a predictable ordering between related 
convention groups.

```csharp
configurator.Domain.ConfigureDomainModelBuilder(builder =>
{
    builder.Levels.Add("Infra");
    builder.Levels.Add("Business");


    builder.Conventions.SetPropertyAttribute(
        when: ...,
        attribute: ...,
        order: Levels["Business"]
    );

    // This convention executes first
    builder.Conventions.SetPropertyAttribute(
        when: ...,
        attribute: ...,
        order: Levels["Infra"]
    );
}
```

A convention with given level order will be added to the median, in other words
will have 0 as its order relative to its level. It is also possible to specify 
min/max values or a specific position within the level.

```csharp
builder.Conventions.SetPropertyAttribute(
    when: ...,
    attribute: ...,
    order: Levels["Infra"].Min + 10
);

builder.Conventions.SetPropertyAttribute(
    when: ...,
    attribute: ...,
    order: Levels["Infra"].At(10)
);
```