# Unreleased

## Features

- `Domain` layer now introduces a level system to improve managing convention
  execution orders.  

## Breaking Changes

- `requireIndex` convention flag is renamed to `beforeBuildingIndex`
- `IDomainModelConventionCollection` is now provided as a configuration target
  ```csharp
  // old usage
  configurator.Domain.ConfigureDomainModelBuilder(builder =>
  {
    builder.Conventions.Add(...);
  });

  // current usage
  configurator.Domain.ConfigureConventions(conventions =>
  {
    conventions.Add(...);
  });
  ```

## Bugfixes

- Adding locate action by conventions was missing claim requirements, fixed