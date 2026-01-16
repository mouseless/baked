# Unreleased

## Features

- To support `Publicize.Fody` weaving, domain model now treats members with
  `EditorBrowsable(State=Advanced)` as private
  - `IsOriginallyPublic()` extension is introduced to check if attribute is
    present on a member info
- [Layers / Domain](../layers/domain.md#proxifying-entities) is updated to
  contain a guide to enable proxifying in domain assemblies
- `Id` feature is now intruduced with `Guid` implementation
  - `Guid` implementation configures `Id` to be mapped as `Guid`
  

## Breaking Changes

- `AutoMapOrmFeature` now requires `Id` user type for primary key mapping
  instead of `System.Guid`
  - `Id` type is provided from `Baked.Abstractions.Orm`
```csharp
// not supported
public Guid Id { get; set; }

// use 'Baked.Orm.Id'
public Id Id { get; set; }
```

  