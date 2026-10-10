# Unreleased

## Features

- `Business` namespace now provides `Validate` object to perform business
  validations
- `FlagsEnumCodingStyle` is now available that supports enums with `[Flags]`
- `IExportOptions` is introduced in `Baked.Binding` so that an attribute
  can control the name it is exported with, e.g., `IdProperty` attribute is
  exported as `@id`
  - Attributes named after their target do not carry that noise to exports,
    `CommandMethod` is exported as `@command`, `MappedMethod` as `@mapped` and
    `QueryMethod` as `@query`

## Breaking Changes

- Attributes that are used in conventions no longer have the `Attribute` suffix
  - `ClientAttribute` -> `Client`
  - `CommandAttribute` -> `Command`
  - `CommandMethodAttribute` -> `CommandMethod`
  - `ComponentGeneratorAttribute<T>` -> `ComponentGenerator<T>`
  - `ContextBasedComponentAttribute` -> `ContextBasedComponent`
  - `ExternalAttribute` -> `External`
  - `GroupAttribute` -> `Group`
  - `LabelAttribute` -> `Label`
  - `LocatableAttribute` -> `Locatable`
  - `LocatableExtensionAttribute` -> `LocatableExtension`
  - `MappedMethodAttribute` -> `MappedMethod`
  - `NamespaceAttribute` -> `Namespace`
  - `NoTransactionAttribute` -> `NoTransaction`
  - `ObjectWithListAttribute` -> `ObjectWithList`
  - `DataAttribute` -> `UiData`
  - `RouteAttribute` -> `UiRoute`
  - `DescriptionAttribute` -> `UiDescription`
  - `GeneratorAttribute<T>` -> `Generator<T>`, its `Generator` and `Filter`
    properties are renamed as `GeneratorDelegate` and `FilterDelegate`
    - `ContextBasedComponent.Filter` -> `FilterDelegate` as well
  - `TryGetLocatableAttribute()` extension -> `TryGetLocatable()`
  - Names in exported `.kdl` files do not change, the `Attribute` suffix was
    already being stripped during export
- `AllowAnonymous`, `ClientCache` and `NoTransaction` attributes now declare
  `[AttributeUsage]`, so they are no longer included in every export target
- Attributes that are used in conventions are renamed
  - `ApiInputAttribute` -> `Bindable`, and moved from `Baked.RestApi.Model` to
    `Baked.Binding`, since it marks types that can be bound from a request and
    is only used by rest binding
    - `AllParametersAreApiInput()` -> `AllParametersAreBindable()`
    - `IsApiInput` -> `IsBindable`
  - `IdAttribute` -> `IdProperty`
  - `QueryClass` -> `Query`, the coding style no longer occupies that name
  - `ValueTypeAttribute` -> `Primitive`, to avoid clashing with
    `System.ValueType` once the `Attribute` suffix is dropped
  - `RichTransientAttribute` -> `Resource`, since transients are already rich,
    this coding style only makes them locatable by their id
- Coding styles are renamed to express how they detect types, `via` is used when
  the mechanism needs naming and `based` when it reads as a qualifier
  - `AddRemoveChild` -> `AddRemoveChildAsSubResource`
  - `Client` -> `SuffixBasedClient`
  - `CommandPattern` -> `CommandViaMethodName`
  - `Id` -> `TypeBasedId`
  - `Initializable` -> `InitializableViaMethodName`
  - `Label` -> `NameBasedLabel`
  - `Locatable` -> `LocateViaId`
  - `LocatableExtension` -> `ExtensionViaLocatableInitializer`
  - `Query` -> `QueryViaPluralName`
  - `RichTransient` -> `ResourceViaIdInitializer`
  - `ScopedBySuffix` -> `ScopedViaSuffix`
  - `Unique` -> `UniqueViaSingleBy`
  - `ValueType` -> `PrimitiveViaParsable`
  - To migrate, use the new names in `AddCodingStyles()`, e.g.,
    `c => c.ValueType()` -> `c => c.PrimitiveViaParsable()`
  - `RichEntity` and `FlagsEnum` are kept as they are
- `MonolithRecipe` and `DataSourceRecipe` configuration methods follow their
  coding styles, e.g., `CommandPattern(...)` -> `CommandViaMethodName(...)`
- `ValueTypeUserType<T>` -> `PrimitiveUserType<T>`
- `EntityInitializerIsPostResourceConvention` ->
  `EntityInitializerIsPostConvention`
- Domain components are removed, all conventions now come from
  `DefaultThemeFeature` by default
  - To migrate, just use components directly instead of through domain
    components, e.g., `TypeFormPage` -> `B.FormPage()`
- `EntitySubclassCodingStyle` is removed completely
- `ICasts` interface and `Caster.Cast()` extension are removed
- `ExtensionViaLocatableInitializerCodingStyle` now does not require extension
  classes to have an implicit operator
  - Any transient with an initializer method that has one parameter that is
    locatable, e.g., `internal MyExtension With(MyLocatable locatable) { ... }`,
    becomes an extension for that locatable
  - This might result unintended classes to become an extension causing their
    API endpoint routes to change, removing `LocatableExtension` attribute
    from unwanted classes, or adding another parameter to the initializer,
    will resolve the issue
