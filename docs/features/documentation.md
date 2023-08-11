# Documentation

This feature configures swagger schema ids and adds operation filters.

Add this feature using `AddDocumentation()` extension;

```csharp
app.Features.AddDocumentation(...);
```

## Default

Adds default opinionated documentation feature

```csharp
c => c.Default()
```
