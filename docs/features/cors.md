# Cors

Implementations of this feature adds and configures services and middlewares
to enable Cross-Origin Resource Sharing (CORS)

Add this feature using `AddCors()` extension;

```csharp
app.Features.AddCors();
```

## ASP.NET Core

This feature adds `AspNetCore` cors services and middleware.

```csharp
c => c.AspNetCore([...])
```

## Disabled

You can disable this feature by calling `Disabled()` method;

```csharp
c => c.Disabled()
```
