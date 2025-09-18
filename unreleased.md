# Unreleased

## Features

- Beta features are available in `Baked.Recipe.Service.Application`;
  - Introducing UI Conventions: `UiLayer` and `ThemeFeature` now provides
    several conventions and extension methods to allow you to build your page
    descriptors via conventions instead of configuring them one by one

## Improvements

- `AdminThemeFeature` is renamed as `DefaultThemeFeature` to indicate that it
  serves as a base for other themes
- Component initializers now have a `schema:` and optional `data:` function
  parameter, minimizing the need to duplicate schema properties in component
  initializers
- `ComponentDescriptor` is removed, all components must have a schema now
  - `Rate`, `Money`, `Number` now have a component schema even if the schemas
    don't have any property
- `ComponentDescriptorAttribute<TSchema>` is renamed as
  `ComponentDescriptor<TSchema>` and is not an attribute any more
  - To add metadata to a domain model use `ComponentDescriptorBuilderAttribute`
    and for schema parts use `DescriptorBuilderAttribute`
- `IPageSchema` and `PageSchemaBase` are introduced to allow custom pages
- `ClientCache` is now automatically mapped to `RemoteData` attributes
- Add/remove metadata extensions are now available property, method and
  parameter as well
- Domain model now respects `AllowMultiple` property of an attribute, allowing
  an attribute to be overriden when it is single instance attribute
  - `Add{..}Metadata` now only works for the attributes that have
    `AllowMultiple` set to `true`
  - `Set{..}Metadata` is now introduced to setting (or overriding) a single
    instance for the attributes that have `AllowMultiple` set to `false`
- `.baked/components.js` now ensures components are sorted by name to make it
  easier to spot unexpected changes
- `AttributeCollection` was not keeping the of attributes in the order they were
  added fixed
