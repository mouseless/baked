# Rest API

DO uses ASP.NET Core for configuring and building a REST API application.

```csharp
app.AddRestApi();
```

## Configuration Targets

This layer provides `IApplicationPartCollection` for registering necessary
application parts, `SwaggerGenOptions`, `SwaggerOptions` and `SwaggerUIOptions`
for configuring `Swagger` behaviour.

### `IApplicationPartCollection`

```csharp
configurator.ConfigureApplicationParts(applicationParts =>
{
    ...
});
```

### `SwaggerGenOptions`

```csharp
configurator.ConfigureSwaggerGenOptions(swaggerGenOptions =>
{
    ...
});
```

### `SwaggerOptions`

```csharp
configurator.ConfigureSwaggerOptions(swaggerOptions =>
{
    ...
});
```

### `SwaggerUIOptions`

```csharp
configurator.ConfigureSwaggerUIOptions(swaggerUIOptions =>
{
    ...
});
```
