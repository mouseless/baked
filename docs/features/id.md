# Id

This feature provides id configuration for persistent entities

Add this feature using `AddId()` extension;

```csharp
app.Features.AddId(...);
```

## Guid

Marks properties named `Id` with `System.Guid` type with `IdAttribute` and 
setups as primary key by configuring `AutoMapping` and `AutoPersistenceModel`
configuration targets

```csharp
c => c.Guid()
```

