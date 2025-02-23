# Code Generation

Allows meta-programming-like experience to reduce repetitive code.

```csharp
app.Layers.AddCodeGeneration();
```

## Configuration Targets

Code generation layer provides `IGeneratedAssemblyCollection`,
`IGeneratedFileCollection` and `GeneratedContext` configuration targets.

### `IGeneratedAssemblyCollection`

This target is provided in `GenerateCode` phase. To configure it in a feature;

```csharp
configurator.ConfigureGeneratedAssemblyCollection(assemblies =>
{
    ...
});
```

### `IGeneratedFileCollection`

This target is provided in `Compile` phase. To configure it in a feature;

```csharp
configurator.ConfigureGeneratedFileCollection(files =>
{
    ...
});
```

## Phases

This layer introduces following `Generate` phases to the application it is added;

- `GenerateCode`: This phase creates a `IGeneratedAssemblyCollection` instance
  and places it in the application context
- `Compile`: This phase compiles generated code during above phase, saves
  generated assemblies and files to entry assembly location with
  `ASPNETCORE_ENVIRONMENT` subfolder

> [!TIP]
>
> To access to a generated assembly or file from a feature use below extension
> method;
>
> ```csharp
> configurator.UsingGeneratedContext(generatedContext =>
> {
>     // generated assembly
>     var assembly = generatedContext.Assemblies[...];
>
>     // generated file helpers
>     var path = generatedContext.Files[...];
>     var contentString = generatedContext.ReadFile(...);
>     var data = generatedContext.ReadFileAsJson<...>();
> });
> ```
