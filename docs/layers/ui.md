# Ui

Baked introduces a metadata generation template which can be used to transform
the business domain to ui metadata to be used in dynamic ui applications.

```csharp
app.Layers.AddUi();
```

`UiLayer` provides `IComponentDescriptor`, `IComponentSchema`, `IData` types for
building ui page metadata. A page metadata will have a component descriptor
(represented by a `ComponentDescriptorAttribtue` instance) at the top in the
hierarchy containing `Type`, `Schema`, `Name` and `Data` properties.

Below is a sample metadata output which can be created;

```json
{
  "type": "Detail",
  "schema": {
    "title": "Parent",
    "header": null,
    "props": [
      {
        "key": "id",
        "title": "Id",
        "component": {
          "type": "String",
          "data": null
        }
      },
      {
        "key": "name",
        "title": "Name",
        "component": {
          "type": "String",
          "data": null
        }
      }
    ]
  },
  "name": "parents",
  "data": {
    "type": "Remote",
    "path": "/parents/{0}"
  }
}
```

> [!NOTE]
>
> The generated ui metadata files will be saved to `Ui` folder at $(OutDir) of
> your project, you can add below properties to your `.csproj` file to copy
> generated files to given directory
>
>```xml
> <PropertyGroup>
>   <CopyComponentDescriptors>true</CopyComponentDescriptors>
>   <ComponentDescriptorsDir>$(ProjectDir)..\admin\.baked</ComponentDescriptorsDir>
> </PropertyGroup>
>```

## Configuration Targets

This layer provides `LayerDescriptors` and `PageDescriptors` configuration
target for registering pages using `ComponentDescriptor` instances.

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
