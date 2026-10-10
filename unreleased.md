# Unreleased

## Features

- `Business` namespace now provides `Validate` object to perform business
  validations
- `FlagsEnumCodingStyle` is now available that supports enums with `[Flags]`
- `IExportOptions` is introduced so that an attribute can control the name it
  is exported with, e.g., `IdProperty` is exported as `@id`

## Breaking Changes

- Attributes that are used in conventions are renamed
  - `IdAttribute` -> `IdProperty`
  - `ValueTypeAttribute` -> `Primitive`, to avoid clashing with
    `System.ValueType` once the `Attribute` suffix is dropped
  - `RichTransientAttribute` -> `Resource`, since transients are already rich,
    this coding style only makes them locatable by their id
- Coding styles are renamed to express how they detect types
  - `ValueType` -> `PrimitiveViaParsable`, e.g., `c.ValueType()` ->
    `c.PrimitiveViaParsable()`
  - `RichTransient` -> `ResourceViaIdInitializer`, e.g., `c.RichTransient()` ->
    `c.ResourceViaIdInitializer()`
  - `ValueTypeUserType<T>` -> `PrimitiveUserType<T>`
- Domain components are removed, all conventions now come from
  `DefaultThemeFeature` by default
  - To migrate, just use components directly instead of through domain
    components, e.g., `TypeFormPage` -> `B.FormPage()`
- `EntitySubclassCodingStyle` is removed completely
- `ICasts` interface and `Caster.Cast()` extension are removed
- `LocatableExtensionCodingStyle` now does not require extension classes to have
  an implicit operator
  - Any transient with an initializer method that has one parameter that is
    locatable, e.g., `internal MyExtension With(MyLocatable locatable) { ... }`,
    becomes an extension for that locatable
  - This might result unintended classes to become an extension causing their
    API endpoint routes to change, removing `LocatableExtensionAttribute` from
    unwanted classes, or adding another parameter to the initializer, will
    resolve the issue
