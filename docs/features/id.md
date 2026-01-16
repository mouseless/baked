# Id

This feature provides id configuration for persistent entities

Add this feature using `AddId()` extension;

```csharp
app.Features.AddId(...);
```

## Guid

Setups `AutoPersistenceModel.Id` configuration with `GuidIdUserType` and
`GuidIdGenerator` to map `Id` column as `Guid`

```csharp
c => c.Guid()
```

