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
  locatable transients
- `ILocator<>` generic interface is now introduced for configuring locators for
  `RichTransient` and `Entity` types and their extensions

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
- `ExtendedContractResolver` is added as a default contract resolver to allow
  customization of json serialization through `RestApiLayer`

## Breaking Changes

- Entity and rich transient domain objects are now rendered ID objects instead
  of ID strings in API endpoints
  - E.g., for a method like `public void Sample(Entity entity)`;
    - Request object was `{ "entityId": "..." }`,
    - Now it has become `{ "entity": { "id": "..." } }`
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
- `SingleById` and `ByIds` are now removed from `IQueryContext`, inject
  `ILocator<>` to get entities by id/ids
  ```csharp
  // not supported
  IQueryContext<Entity>.SingleById(id);

  // add 'Baked.Business.Id' property
  ILocator<Entity>.Locate(id);
  ```
- `SingleById` convention is renamed to `AddLocateAction`
- `AddLocateAction` (former: `SingleById`) convention now requires `Locatable`
  type instead of `Query` type
  ```csharp
  // previous usage
  builder.Conventions.AddSingleById<Entities>();

  // use locatable type instead
  builder.Conventions.AddLocateAction<Entity>();
  ```
- `WithMethodCodingStyleFeature` is now renamed to
  `InitializableCodingStyleFeature`
- `EntitySubclassViaCompositionCodingStyleFeature` is renamed to
  `EntitySubclassCodingStyleFeature`
- `DesignatedStringPropertiesAreLabelUxFeature` is now split into two,
  `LabelCodingStyleFeature` and `LabelsAreFrozenUxFeature`
- `LabelAttribute` is moved to `Baked.Business` namespace
