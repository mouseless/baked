# Lifetime

Add this feature using `AddLifetimes()` extension;

```csharp
app.Features.AddLifetimes([...]);
```

## Application

Adds services with `Singleton` metadata to `IServiceCollection` as singleton.

```csharp
c => c.Application()
```

## Instance

Adds services with `Transient` metadata to `IServiceCollection` as
transient.

```csharp
c => c.Instance()
```

## Scope

Adds services with `Scoped` metadata to `IServiceCollection` as scoped.

```csharp
c => c.Scope()
```
