# Unreleased

## Features

- `Business` namespace now provides `Validate` object to perform business
  validations
- `FlagsEnumCodingStyle` is now available that supports enums with `[Flags]`

## Breaking Changes

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
    becomes an extension for that locatablae
  - This might result unintended classes to become an extension causing their
    API endpoint routes to change, removing `LocatableExtensionAttribute` from
    unwanted classes, or adding another parameter to the initializer, will
    resolve the issue
