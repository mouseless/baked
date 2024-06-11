# HTTP Client

Baked uses ASP.NET Core's `IHttpClientFactory` and related services for
providing clients for http requests and responses.

```csharp
app.Layers.AddHttpClient();
```

## Configuration Targets

`HttpClient` layer provides `List<HttpClientDescriptor>` which is used to add
named configuration delegates for `IHttpClientBuilder`.

### `List<HttpClientDescriptor>`

This target is provided in `AddServices` phase as the target. To configure it
in a feature;

```csharp
configurator.ConfigureHttpClient(clients =>
{
    ...
});
```

> [!NOTE]
>
> Descriptor with name "Default" is added as a default builder delegate for all
> created http clients
