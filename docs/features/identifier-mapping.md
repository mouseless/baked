# Identifier Mapping

This feature provides id configuration for persistent entities

Add this feature using `AddIdentifierMapping()` extension;

```csharp
app.Features.AddIdentifierMapping(...);
```

## Guid

Setups `AutoPersistenceModel.Id` configuration with `GuidIdentifierUserType` and
`GuidIdentifierGenerator` to map `Id` column as `Guid`

```csharp
c => c.Guid()
```

