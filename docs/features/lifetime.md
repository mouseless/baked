# Lifetime

Add this feature using `AddLifetimes()` extension;

```csharp
app.Features.AddLifetimes([...]);
```

## Singleton

Adds services with `SingletonAttribute` metadata to `IServiceCollection` as
singleton.

```csharp
c => c.Singleton()
```

## Scoped

Adds services with `ScopedAttribute` metadata to `IServiceCollection` as scoped.

```csharp
c => c.Scoped()
```

## Transient

Adds services with `TransientAttribute` metadata to `IServiceCollection` as
transient.

```csharp
c => c.Transient()
```
