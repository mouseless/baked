# Logging

Implementations of this feature provides predefined logging behaviour.

Add this feature implementations using `AddLogging()` extension;

```csharp
app.Features.AddLogging(...);
```

## Request

This implementation creates a log scope for business method endpoints and logs
begin and end with response status.

```csharp
c => c.Request(singleLine: false)
```
