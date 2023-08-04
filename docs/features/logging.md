# Logging

Implementations of this feature provides predefined logging behaviour.

Add this feature implementations using `AddLogging()` extension;

```csharp
app.Features.AddLogging(...);
```

## Request Logging

This implementation logs incoming request and related response information 
along with exception logging.

```csharp
c => c.RequestLogging()
```

By default it adds a logging middeware for printing log messages to console.

```csharp
configurator.ConfigureMiddlewareCollection(middlewares =>
{
    middlewares.Add<RequestLogMiddleware>(order: ...);
});
```