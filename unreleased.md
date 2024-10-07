# Unreleased

## Improvements

- `MockConfiguration` feature now clears `FakeSettings` list on teardown
- `MocMe.TheClient` helper now provides optional parameter to clear previous 
  invocations
- `ConfigureAction` and `OverrideAction` helpers are now added to configure 
  `RestApi.ActionModel` before and after conventions
- `Enum<T>` helper class is added to use enum values within `ValueSource`
  attribute
- The following layers are merged into one single layer;
  - Configuration
  - DependencyInjection
  - Monitoring
