# Unreleased

## Features

- Beta features are available in do-blueprints-service package;
  - `CodeGenerationLayer` is introduced, now it is possible to generate code
    during initialization of a service
  - `DomainLayer` now provides;
    - an inherited factory mechanism to individually configure level of
      reflected details for registered types. Models for types are now built as
      following inherited levels;
      - `TypeModel`
      - `TypeModelGenerics`
      - `TypeModelInheritance`
      - `TypeModelMetadata`
      - `TypeModelMembers`
    - a metadata mechanism that allows you to add custom attributes to domain
      model
    - an indexer mechanism that allows you to index models based on their
      attributes
  - `RestApiLayer` now provides;
    - api model to generate controller code from domain model
    - model binder configuration to allow custom parameter types from action
      parameters
  - `Forge.New.Service` now automatically generates;
    - api controllers and actions from domain model
    - entity lookup calls for entity parameters
    - all types of parameters and return types
    - stylized routes with default conventions
    - additional attributes for controller actions
  - `Business` and `Orm` is split into two features
    - `LifetimeFeature` is introduced with three implementations `Singleton`,
      `Scoped` and `Transient`
    - `CodingStyleFeature` is introduced with existing coding styles to separate
      them from business feature

## Improvements

- `MvcNewtonsoftJsonOptions` is added to `RestApiLayer` as configuration target
- `IScoped` marker interface is removed, `[Name]Context` convention is
  introduced to configure scoped lifetime by convention
- `FixedToken` authentication is now the default in `Service` blueprint
- `Default` business feature is renamed as `DomainAssemblies`
- `Default` orm feature is renamed as `AutoMap`
- `Documentation` feature is refactored into coding styles and removed
  completely

## Bugfixes

- `TypeModel`'s which are not business types were throwing null reference
  exception for collection properties, fixed
