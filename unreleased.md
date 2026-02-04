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
  - `Id` user type can be mapped as `Guid`, `AutoIncrement` or `Assigned`
- `LocatableCodingStyle` feature is now added which manages binding of
  transient and locatable transients, adding id or initializer parameters
- `Business.ILocator<>` generic interface is now introduced for configuring locators
  for `RichTransient` and `Entity` types and their extensions

## Breaking Changes

- `AutoMapOrmFeature` no longer configures `Id` properties and foreign keys
- `Id` property type is now changed from `System.Guid` to `Baked.Business.Id`
  ```csharp
  // not supported
  public Guid Id { get; set; }

  // use 'Baked.Business.Id'
  public Id Id { get; set; }
  ```
- `RichTransient` feature now requires initializer to be with single parameter
  of `Business.Id` type and contain property with `Business.Id` type 
  ```csharp
  // not supported
  public RichTransient With(string id) { ... }

  // add 'Baked.Business.Id' property
  public Business.Id Id { get; set; }

  public RichTransient With(Id id) {
    Id = id;
  }
  ```
- `EntityExtensionViaComposition` coding style feature is renamed to 
  `LocatableExtension` coding style feature
- `LocatableExtension` (former: `EntityExtensionViaComposition`) feature now 
  requires a property with `Business.Id` type 
  ```csharp
  // not supported
  Entity _entity = default!;

  internal EntityExtension With(Entity entity) { ... }

  // add 'Baked.Business.Id' property
  Entity _entity = default!;

  internal EntityExtension With(Entity entity) { ... }

  internal Business.Id Id => _entity.Id
  ```
- `SingleByIdConvention` is now moved to `LocatableCodingStyle` feature
- `SingleById` and `ByIds` is now removed from `IQuerContext`, inject 
  `ILocator<>` getting entityes by id/ids
  ```csharp
  // not supported
  IQueryContext<Entity>.SingleById(id);

  // add 'Baked.Business.Id' property
  ILocator<Entity>.Locate(id);
  ```
  - `SingleById` convention now requires `Locatable` type instead of `Query` 
    type
  ```csharp
  // previous usage
  builder.Conventions.AddSingleById<Entities>();

  // use locatable type instead
  builder.Conventions.AddSingleById<Entity>();
  ```
- `WithMethodCodingStyleFeature` is now renamed to 
  `InitializableCodingStyleFeature`