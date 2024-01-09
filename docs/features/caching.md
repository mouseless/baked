# Caching

Implementations of this feature provides predefined caching behaviour.

Add this feature using `AddCaching()` extension;

```csharp
app.Features.AddCaching(...);
```

## Scoped Memory

This feature implementation registers `Func<IMemoryCache>` factory with scoped
`MemoryCache` implementation to provide request in-memory caching.

```csharp
c => c.ScopedMemory()
```
