# Unreleased

## Features

- Beta features are available in Baked.Recipe.Service package;
  - `RuntimeLayer`is added which merges the following layers:
      - `Configuration`
      - `DependencyInjection`
      - `Monitoring`
  - `Oracle` implementation of `Database` feature is now added
  - `Cors` feature is now added with `AspNetCore` implementation
  - `Reporting` feature is introduced with three implenmentations `NativeSql`
    for production, `Mock` and `Fake` for development
  - `DataSource` recipe is available which minimal features for a data source
    service recipe

## Improvements

- `MockConfiguration` feature now clears `FakeSettings` list on teardown
- `MockMe.TheClient` helper now provides optional parameter to clear previous
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
- `DomainAssemblies` feature now have options to auto add embedded file
  providers for give assemblies
- `Dotnet` feature now adds embedded and physical file providers for given
  entry assembly
- Async overloads for `ShouldPass` and `ShouldFail` are now available

### Library Upgrades

| Package                                   | Old Version | New Version |
| ----------------------------------------- | ----------- | ----------- |
| Oracle.ManagedDataAccess.Core             | new         | 23.5.1      |
| Microsoft.AspNetCore.Mvc.NewtonsoftJson   | 8.0.8       | 8.0.10      |
| Microsoft.AspNetCore.Mvc.Testing          | 8.0.8       | 8.0.10      |
| Microsoft.Data.Sqlite.Core                | 8.0.8       | 8.0.10      |
| Microsoft.Extensions.Logging.Abstractions | 8.0.1       | 8.0.2       |
| Microsoft.Extensions.TimeProvider.Testing | 8.9.1       | 8.10.0      |
| MySql.Data                                | 9.0.0       | 9.1.0       |
| NHibernate.Extensions.Sqlite              | 8.0.13      | 8.0.14      |
| Npgsql                                    | 8.0.4       | 8.0.5       |
| Swashbuckle.AspNetCore                    | 6.8.0       | 6.9.0       |
| Swashbuckle.AspNetCore.Annotations        | 6.8.0       | 6.9.0       |
