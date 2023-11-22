# Unreleased

## Improvements

- Added `GetRequiredValue<T>` extension to IConfiguration with default value 
  option which returns given value when configuration value is null

## Bugfixes

- `ObjectUserType` was causing its data to be corrupted when it contains special
  characters, fixed.
