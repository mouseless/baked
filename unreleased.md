# Unreleased

## Features

- To support `Publicize.Fody` weaving, domain model now treats members with
  `EditorBrowsable(State=Advanced)` as private
  - `IsOriginallyPublic()` extension is introduced to check if attribute is
    present on a member info
- [Layers / Domain](../layers/domain.md#proxifying-entities) is updated to
  contain a guide to enable proxifying in domain assemblies
- `IdCodingStyle` feature is now added which configures primary key and foreign
  key references for entities
  - A property named `Id` with `Baked.Business.Id` user type is required for a
    property to be configured as `Id`
  - `Id` user type is mapped to `Guid` db type with autogenerate support

## Improvements

- Locatable domain objects were only supported in API parameters, now they are
  rendered as `{ id: "..." }` in API record inputs as well
- Relations of a locatable is rendered as a ref object `{ id: "...", label:
  "..." }`
  - It includes id and all label properties
  - It works only for locatable relations, e.g., `Child.Parent` property will
    render only id and label properties of the parent
  - Any locatable under a record will include all of their properties, e.g.,
    `Child.ParentWrapper.Parent` will render all properties of the parent

## Breaking Changes

- `AutoMapOrmFeature` no longer configures `Id` properties and foreign keys
- `Id` property type is now changed from `System.Guid` to `Baked.Business.Id`
  ```csharp
  // not supported
  public Guid Id { get; set; }

  // use 'Baked.Business.Id'
  public Id Id { get; set; }
  ```
- `LabelAttribute` is moved to `Baked.Business` namespace
