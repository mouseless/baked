# Caching

Implementations of this feature provides predefined caching behaviour.

Add this feature using `AddCachings()` extension;

```csharp
app.Features.AddCachings([...]);
```

## In-Memory

This feature implementation uses `AddMemoryCache` to provide application-wide
in-memory caching.

```csharp
c => c.InMemory()
```

## Scoped Memory

This feature implementation registers `Func<IMemoryCache>` factory with scoped
`MemoryCache` implementation to provide request in-memory caching.

```csharp
c => c.ScopedMemory()
```
