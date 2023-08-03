# Core

This feature registers ISystem to ServiceCollection.

Add this feature using `AddCore()` extension;

```csharp
app.Features.AddCore(...);
```

## Dotnet

Adds ISystem interface to ServiceCollection to be used throughout your
application.

```csharp
c => c.Dotnet()
```