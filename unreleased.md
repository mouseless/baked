# Unreleased

## Features

- Introducing model metadata generation: `DomainLayer` can now output attribute
  export files for each specified attribute type combination in domain model
- Introducing diagnostics: Baked now has `Diagnostics` class to improve error
  and warning messages during `Generate` phase
  - `Generate` task now doesn't break as soon as it encounters an exception,
    instead it collects all errors and breaks the build with multiple errors to
    provide a better DX
  - You may report an error using `Diagnostics.ReportError` during the generate
    phase that outputs a well formatted error message
  - All existing build errors are now documented under [Errors](../errors.md)

## Improvements

- `DefaultThemeFeature(debugComponentPaths:)` now prints a pretty formatted tree
  instead of a list of component paths
