# Core

This feature registers `TimeProvider.System` to services and adds file
providers for given entry assembly.

Add this feature using `AddCore()` extension;

```csharp
app.Features.AddCore(...);
```

## .NET

Adds a local implementation of `TimeProvider` to services to be used throughout
your application.

```csharp
c => c.Dotnet()
```

## Mock

Adds a mock implementation to be used in testing.

```csharp
c => c.Mock()
```
