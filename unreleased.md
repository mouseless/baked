# Unreleased

## Features

- __Metadata Exports:__ `DomainLayer` can now output attribute export files for
  each specified attribute type combination in domain model
  - `RestBindingFeature` now exports API metadata into `.baked/rest-api` folder
    in application project
  - `AutoMapOrmFeature` now exports entity metadata into `.baked/data-access`
    folder in application project
    - `ColumnAttribute` and `ForeignKeyAttribute` classes are added to have a
      more clear data access metadata contents
- __Diagnostics:__ Baked now has `Diagnostics` class to improve error and
  warning messages during `Generate` phase
  - `Generate` task now doesn't break as soon as it encounters an exception,
    instead it collects all errors and breaks the build with multiple errors to
    provide a better DX
  - All existing build errors are now documented under [Errors](../errors.md)
- __Inspection:__ Baked now supports inspecting a specific UI schema property
  where it lists all conventions and value change records for the specified
  property

## Breaking Changes

- `Conditional` component and its helper methods are removed
  - It's recommended to implement a custom component per use case, when there is
    a need to switch between components based on its data
  - One of the `DataTableColumn()` overloads was using `Conditional`, removed

## Improvements

- `DefaultThemeFeature(debugComponentPaths:)` now prints a pretty formatted tree
  instead of a list of component paths
  - To enable
    ```csharp
    debugComponentPaths: true
    ```
  - To filter paths
    ```csharp
    debugComponentPaths: new() { Filter = path => path.EndsWith(...) }
    ```
  - To include full paths
    ```csharp
    debugComponentPaths: new() { IncludeFullPaths = true }
    ```

## Library Upgrades

| NuGet Package           | Old Version | New Version |
| ---                     | ---         | ---         |
| Spectre.Console         | new         | 0.52.0      |
| Spectre.Console.Testing | new         | 0.52.0      |
