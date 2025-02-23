# Unreleased

## Features

- Beta features are available;
  - `@baked/recipe-admin` npm package is released
  - `UiLayer` is added to `Service` recipe
  - `Binding` feature is added with the default `Rest` implementation where all
    the rest api bindings are made for domain objects

## Improvements

- All API conventions are migrated to be domain model conventions making it
  possible to access final api model in domain model builder
- `CodeGenerationLayer` now outputs the generated code next to the generated
  assembly to allow further investigation in case code is not generated as
  expected
