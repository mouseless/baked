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
- `DataAccess` layer now introduces `IDatabaseInitializerCollection` 
  configuration target for registering database initialization actions
- `IServiceProvider` now has `UseCurrentScope` extensions to resolve services 
  using the scope provided by `IServiceProviderAccessor` implementations      
- `TestRun` now creates and disposes a scope for each test run to  
- `Runtime` layer now provides `IFileProvider`component with 
  `CompositeFileProvider` implementation
- `ReadAsString` and `ReadAsStringAsync` helper extensions are now added for
  `IFileProvider`  
- `DomainAssemblies` feature now have options to auto add embedded and physical
  file providers for give assemblies  
