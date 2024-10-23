# Cors

Implementations of this feature adds and configures `AspNetCore` CORS services
and middleware

Add this feature using `AddCors()` extension;

```csharp
app.Features.AddCors();
```

## Allow Origin

This feature registers a single CORS policy that specifies certain origins 
while allowing any headers and methods.

```csharp
c => c.AllowOrigin([...])
}
```

## Disabled

You can disable this feature by calling `Disabled()` method;

```csharp
c => c.Disabled()
```