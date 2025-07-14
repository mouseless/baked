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

Also this layer provides `ILocaleDictionary` and `NewLocale` configuration
targets to generate localization data for generated page descriptors

### `AppDescriptor`

This target is provided in `GenerateCode` phase. To configure it in a feature;

```csharp
configurator.ConfigureAppDescriptor(app =>
{
    ...
});
```

### `AppDescriptor`

This target is provided in `GenerateCode` phase. To configure it in a feature;

```csharp
configurator.ConfigureComponentExports(exports =>
{
    ...
});
```

### `ILocaleDictionary`

This target is provided in `GenerateCode` phase. To configure it in a feature;

```csharp
configurator.ConfigurePageDescriptors(pages =>
{
    configurator.UsingLocaleDictionay(locales =>
    {
        ...
    });
});
```

### `NewLocale`

This target is provided in `GenerateCode` phase. To configure it in a feature;

```csharp
configurator.ConfigurePageDescriptors(pages =>
{
    configurator.UsingNewLocale(l =>
    {
        ...
    });
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
