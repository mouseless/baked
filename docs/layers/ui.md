# UI

Baked introduces a metadata generation template which can be used to transform
the business domain to UI metadata to be used in dynamic UI applications.

```csharp
app.Layers.AddUi();
```

`UiLayer` provides `IComponentDescriptor`, `IComponentSchema`, `IData` types for
building UI page metadata. A page metadata will have a component descriptor
(represented by a `ComponentDescriptorAttribute` instance) at the top in the
hierarchy containing `Type`, `Schema`, `Name` and `Data` properties.

> [!NOTE]
>
> The generated UI metadata files will be saved to `Ui` folder at `$(OutDir)` of
> your project, you can add below properties to your `.csproj` file to copy
> generated files to given directory
>
>```xml
> <PropertyGroup>
>   <CopyComponentDescriptors>true</CopyComponentDescriptors>
>   <UiAppDir>$(ProjectDir)..\admin</UiAppDir>
> </PropertyGroup>
>```

## Configuration Targets

This layer provides `AppDescriptor`, `ComponentExports`, `LayerDescriptors`
and `PageDescriptors` configuration target for registering pages using
`ComponentDescriptor` instances.

### `AppDescriptor`

This target is provided in `GenerateCode` phase. To configure it in a feature;

```csharp
configurator.ConfigureAppDescriptor(app =>
{
    ...
});
```

### `ComponentExports`

This target is provided in `GenerateCode` phase. To configure it in a feature;

```csharp
configurator.ConfigureComponentExports(exports =>
{
    ...
});
```

### `LayoutDescriptors`

This target is provided in `GenerateCode` phase. To configure it in a feature;

```csharp
configurator.ConfigureLayoutDescriptors(layouts =>
{
    ...
});
```

### `PageDescriptors`

This target is provided in `GenerateCode` phase. To configure it in a feature;

```csharp
configurator.ConfigurePageDescriptors(pages =>
{
    ...
});
```

> [!TIP]
>
> To access the localization function from a feature use below extension method.
>
> ```csharp
> configurator.UsingLocalization(l =>
> {
>     // use this function to add that key to UI project;
>     // l("A localized message")
>     ...
> });
> ```
>
> Each call to this function will result `UiLayer` to bring that value of given
> key for every language to the UI project under `.baked/` folder, e.g.,
> `.baked/locale.en.json` file.

> [!TIP]
>
> To access which keys are going to be brought to UI project from a feature, you
> can use `UsingLocaleTemplate` function.
>
> ```csharp
> configurator.UsingLocaleTemplate(localeTemplate =>
> {
>     ...
> });
> ```

## Descriptor Builder System

To generate a page descriptor from a domain model, we provide a descriptor
builder system that is added through domain model conventions. There are two
builder attributes for this purpose;

1. `ComponentDescriptorBuilderAttribute<TComponentSchema>`
2. `DescriptorBuilderAttribute<TSchema>`

The page generator starts with a `Page` component path to render a domain model
into a `ComponentDescriptor<TComponentSchema>` instance. To add a component to a
domain model, usually to a type, use the `AddTypeComponent` extension of
`IDomainModelConventionCollection`;

```csharp
configurator.ConfigureDomainModelBuilder(builder =>
{
    builder.Conventions.AddTypeComponent(
        component: (c, cc) => ...,
        whenType: c => c.Type...,
        whenComponent: cc => cc.Path.EndsWith("Page")
    );
});
```

- `component:` is a builder function that takes `TypeModelMetadataContext c` and
  `ComponentContext cc` as parameters, and is expected to return a
  `ComponentDescriptor<TComponentSchema>` instance, such as
  `ComponentDescriptor<ReportPage>`
- `whenType:` is a predicate function that takes `TypeModelMetadataContext c` as
  a parameter and is used as a filter to choose which types this convention
  applies to
- `whenComponent:` is a predicate funtion that takes `ComponentContext cc` as a
  parameter and is used as a filter to determine at which component path this
  convention should be applied

`AddPropertyComponent`, `AddMethodComponent` and `AddParameterComponent`
extensions works the same way, except instead of `whenType:` they accept
`whenProperty:`, `whenMethod:` and `whenParameter:` respectively.

Once you add a component for a domain model at a specific component path, it
will return that component instance when asked at that component path. You may
use `GetComponent` extension to ask for a component during a component build.
For example, below call asks for a component at `../title` path for the `type`;

```csharp
type.GetComponent(cc.Drill("Title"))
```

> [!NOTE]
>
> `ComponentContext` has a `Drill(...)` method that enables you to navigate
> deeper into component paths.

> [!TIP]
>
> `GetRequiredComponent` is also provided to ensure that a component exists at
> the speficied path for the domain model. Otherwise, it will cause a generation
> error during post-build phase of `dotnet build`.

For non-component schemas, similar extensions are provided for domain models;

- `AddTypeSchema`
- `AddPropertySchema`
- `AddMethodSchema`
- `AddParameterSchema`

Use these extensions to associate domain models with non-component schemas such
as `ReportPage.Tab` or `Parameter`. Once you add a schema for a domain model,
you can access it using `GetSchema<TSchema>` or `GetSchemas<TSchema>` extension
methods.

> [!TIP]
>
> `GetRequiredSchema<TSchema>` is also provided to ensure that a schema exists
> at that path for that domain model. Otherwise, it will cause a generation
> error during post-build phase of `dotnet build`.

### Configuring Existing Schemas

To add a convention that configures an existing schema, there are
`Add...ComponentConfiguration<TComponentSchema>` and
`Add...SchemaConfiguration<TSchema>` helpers.
