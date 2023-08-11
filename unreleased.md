# Unreleased

## Features

- Beta features are available in `do-blueprints-service` package;
  - `Documentation` feature is added with `Default` implementation.

## Improvements

- Swagger schema ID conflict that occurred when two different controllers had a
  nested class with the same name, fixed.
- `Searcher` `GetMe` is added to `Spec`, `Searcher` extensions have been added
  to `ServiceSpecExtensions`.
- `Url Extensions` and `Guid Extensions` are added to `ServiceSpecExtensions`
  for generating new `Uri` and `Guid`.
