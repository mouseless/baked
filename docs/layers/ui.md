# Ui

Baked introduces a metadata generation template which can be used to transform 
the business domain to ui metadata to be used in dynamic ui applications.

```csharp
app.Layers.AddDomain();
```

`UiLayer` provides `ComponentDescriptor`, `IComponentSchema`, `IData` 
types for building ui metadata. A metadata will have a `ComponentDescriptor`
at the top in the hierarchy containing `Type`, `Schema` and `Data` properties.

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
  "data": {
    "type": "Remote",
    "path": "/parents/{0}"
  }
}
```

> [!NOTE]
>
> The generated ui metadata files will be saved to `Ui` folder at $(OutDir)
> of your project, you can add below properties to your .csproj file to
> copy generated files to given directory
>
>```xml
> <PropertyGroup>
>   <CopyComponentDescriptors>true</CopyComponentDescriptors>  
>   <ComponentDescriptorsDir>$(SolutionDir)\test\recipe\admin\.baked</ComponentDescriptorsDir>
> </PropertyGroup>
>```

## Configuration Targets

This layer provides `ComponentDescriptors` configuration target 
for registering `ComponentDescriptor` instances.

### `ComponentDescriptors`

This target is provided in `GenerateCode` phase. To configure it in a feature;

```csharp
configurator.ConfigureComponentDescriptors(components =>
{
    ...
});
```
