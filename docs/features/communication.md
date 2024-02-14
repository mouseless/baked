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
communication: c => c.Mock(defaultResponses: response =>
{
    response.ForClient<MyService>("""{ "value": "test result" }""");
    response.ForClient<MyOtherService>(new { value = "path1 response" }, when: r => r.UrlOrPath.Equals("path1"));
})
```