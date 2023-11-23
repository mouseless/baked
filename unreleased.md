# Unreleased

## Improvements

- Added `GetRequiredValue<T>` extension to `IConfiguration` with default value
  option
- Mock `IConfiguration` return values for not defined settings can now be 
  configured by overriding `ServiceSpec.GetDefaultSettingsValue()`

## Bugfixes

- `ObjectUserType` was causing its data to be corrupted when it contains special
  characters, fixed.
