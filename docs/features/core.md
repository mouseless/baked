# Core

This feature registers `ISystem` to services.

Add this feature using `AddCore()` extension;

```csharp
app.Features.AddCore(...);
```

## Dotnet

Adds a local implementation of `ISystem` interface to services to be used
throughout your application.

```csharp
c => c.Dotnet()
```