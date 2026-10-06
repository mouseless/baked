# Unreleased

## Features

- `Business` namespace now provides `Validate` object to perform business
  validations
- `ValidatableObjectCodingStyle` is introduced to handle complex record
  validation via `IValidatableObject` interface
- `EnumFlagsCodingStyle` is now available that supports enums with `[Flags]`

## Breaking Changes

- Domain components are removed, all conventions now come from
  `DefaultThemeFeature` by default
  - To migrate, just use components directly instead of through domain
    components, e.g., `TypeFormPage` -> `B.FormPage()`
- `EntitySubclassCodingStyle` is removed completely
- `ICasts` interface and `Caster.Cast()` extension are removed
