# Rest API

Baked uses ASP.NET Core for configuring and building a REST API application.

```csharp
app.Layers.AddRestApi();
```

## Configuration Targets

This layer provides `ApiModel` to generate controllers from domain objects,
`IApplicationPartCollection` for registering necessary application parts,
`MvcNewtonsoftJsonOptions` `SwaggerGenOptions`, `SwaggerOptions` and
`SwaggerUIOptions` for configuring `Swagger` behavior.

### `ApiModel`

This target is provided in `GenerateCode` phase. To configure it in a feature;

```csharp
configurator.ConfigureApiModel(api =>
{
    ...
});
```

### `IApplicationPartCollection`

This target is provided in `AddServices` phase. To configure it in a feature;

```csharp
configurator.ConfigureApplicationParts(applicationParts =>
{
    ...
});
```

### `MvcNewtonsoftJsonOptions`

This target is provided in `AddServices` phase. To configure it in a feature;

```csharp
 configurator.ConfigureMvcNewtonsoftJsonOptions(options =>
 {
     ...
 });
```

> [!TIP]
>
> This layer creates and sets an `ExtendedContractResolver` instance as the
> default contract resolver under `options.SerializerSettings.ContractResolver`.
> It contains a `IServiceProvider` instance to help you serialize json objects
> into rich domain objects.
>
> You may use this resolver to;
>
> - Set a proxy type via `ProxyType` property to ignore the proxy objects and
>   serialize them using the actual base type
> - Change a type's json contract via `.SetType(...)` method
> - Change a property's json contract via `.SetProperty(...)` method

### `SwaggerGenOptions`

This target is provided in `AddServices` phase right after
`IApplicationPartCollection`. To configure it in a feature;

```csharp
configurator.ConfigureSwaggerGenOptions(swaggerGenOptions =>
{
    ...
});
```

### `SwaggerOptions`

This target is provided in `AddServices` phase right after
`SwaggerGenOptions`. To configure it in a feature;

```csharp
configurator.ConfigureSwaggerOptions(swaggerOptions =>
{
    ...
});
```

### `SwaggerUIOptions`

This target is provided in `AddServices` phase right after
`SwaggerOptions`. To configure it in a feature;

```csharp
configurator.ConfigureSwaggerUIOptions(swaggerUIOptions =>
{
    ...
});
```
