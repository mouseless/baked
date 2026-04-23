# Unreleased

## Features

- __Metadata Exports:__ `DomainLayer` can now output attribute export files for
  each specified attribute type combination in domain model
  - `RestBindingFeature` now exports API metadata into `.baked/rest-api` folder
    in application project
- __Diagnostics:__ Baked now has `Diagnostics` class to improve error and
  warning messages during `Generate` phase
  - `Generate` task now doesn't break as soon as it encounters an exception,
    instead it collects all errors and breaks the build with multiple errors to
    provide a better DX
  - You may report an error using `Diagnostics.ReportError` during the generate
    phase that outputs a well formatted error message
  - All existing build errors are now documented under [Errors](../errors.md)

## Breaking Changes

- `Conditional` component and its helper methods are removed
  - It's recommended to implement a custom component per use case, when there is
    a need to switch between components based on its data

## Improvements

- `DefaultThemeFeature(debugComponentPaths:)` now prints a pretty formatted tree
  instead of a list of component paths
