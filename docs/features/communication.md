# Communication

Add this feature using `AddCommunication()` extension;

```csharp
app.Features.AddCommunication(...);
```

## Http

This feature provides an `IClient<>` implementation and adds descriptors for 
configured named clients from _app.settings_

```csharp
c => c.Http()
```
```json
"Communication": {
    "Http": {
        "MyService": {
            "BaseAddress": "http://api.backend.com",
            "DefaultHeaders": {
                "User-Agent": ".NET Http Client"
            }
        }
    }
}
```

## Mock

Adds a mock implementation to be used in testing with `MockClientConfiguration`

```csharp
c => c.Mock(configuration =>
{
    configuration.AddClientSetup<MyService>([
        new(r => r.UrlOrPath.Equals("path1"), "path1 response"),
        new(r => r.UrlOrPath.Equals("path2"), "path2 response")
    ]);
}),
```