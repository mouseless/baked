# Unreleased

## Improvements

- `DomainLayer` can now output attribute export files for each specified
  attribute type combination in domain model
- `CodeGenerationLayer` now has improved error and warning messages
  - You can now add error or warning from domain model conventions to break the
    build with an error message
  - `Generate` task now doesn't break as soon as it encounters an error, instead
    it collects all errors and breaks the build with multiple errors to provide
    a better DX
