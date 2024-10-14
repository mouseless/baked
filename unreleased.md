# Unreleased

## Features

- Beta features are available in do-blueprints-service package;
  - `RuntimeLayer`is added which merges the following layers:
      - `Configuration`
      - `DependencyInjection`
      - `Monitoring`

## Improvements

- `MockConfiguration` feature now clears `FakeSettings` list on teardown
- `MocMe.TheClient` helper now provides optional parameter to clear previous 
  invocations
- `ConfigureAction` and `OverrideAction` helpers are now added to configure 
  `RestApi.ActionModel` before and after conventions
- `Enum<T>` helper class is added to use enum values within `ValueSource`
  attribute
- `DataAccess` layer now exposes `FluentConfiguration` as configuration target
- An embedded resource reader component, `IEmbeddedResourceReader`, is now provided 
  through `DotnetCore` and `MockCore` features
