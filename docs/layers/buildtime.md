# Buildtime

Allows meta-programming-like experience to reduce repetitive code.

```csharp
app.Layers.AddBuildtime();
```

## Configuration Targets

Buildtime layer provides `IGeneratedAssemblyCollection`,
`IGeneratedFileCollection` and `GeneratedContext` configuration targets.

### `IGeneratedAssemblyCollection`

This target is provided in `Generate` phase. To configure it in a feature;

```csharp
configurator.Buildtime.ConfigureGeneratedAssemblyCollection(assemblies =>
{
    ...
});
```

### `IGeneratedFileCollection`

This target is provided in `Compile` phase. To configure it in a feature;

```csharp
configurator.Buildtime.ConfigureGeneratedFileCollection(files =>
{
    ...
});
```

## Phases

This layer introduces following `Generate` phases to the application it is added;

- `Prepare`: This is the first phase of generate, layers use it to provide
  their configuration targets before domain model is built
- `Generate`: This phase creates a `IGeneratedAssemblyCollection` instance and
  places it in the application context
- `Compile`: This phase compiles generated code during above phase, saves
  generated assemblies and files to entry assembly location with
  `ASPNETCORE_ENVIRONMENT` subfolder

> [!TIP]
>
> To access to a generated assembly or file from a feature use below extension
> method;
>
> ```csharp
> configurator.Buildtime.UsingGeneratedContext(generatedContext =>
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

## Diagnostics

To have a better error output during generation we provide a diagnostics
mechanism.

To define a known build error add an extension to `DiagnosticCode`;

```csharp
extension(DiagnosticCode)
{
    public static DiagnosticCode MyCustomCode => new(1);
}
```

And throw it from within conventions;

```csharp
throw DiagnosticCode.MyCustomCode.Exception(
    "Some custom message..."
);
```

This will print a single error line in build output during `Generate` task
instead of printing a full stack trace;

```bash
error C0001: Some custom message...
```

### Reporting Custom Information

It is also possible to report any build output in `error`, `warning` and `info`
levels. To report a custom message simply call the corresponding report method
of `Diagnostics.Current`;

```csharp
Diagnostics.Current.ReportError(myCode, myMessage);
Diagnostics.Current.ReportWarning(myCode, myMessage);
Diagnostics.Current.ReportInfo(myMessage);
```

To group together info messages that occur at different times you may use
`group:` optional parameter;

```csharp
Diagnostics.Current.ReportInfo(myMessage, group: "my-group");
```
