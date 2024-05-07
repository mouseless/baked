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
    - entity lookup calls for entity and entity extension parameters
    - all types of parameters and return types
    - stylized routes with default conventions
    - additional attributes for controller actions
    - multi unique property instead of just id route
    - commands as actions
    - entity extension services under entity route
    - entity subclass services under entity route via a discriminator unique
      property
    - add/remove child routes
    - null check for not-null parameters
    - redirect result for uri returning methods
  - `Business` and `Orm` is split into two features
    - `LifetimeFeature` is introduced with three implementations `Singleton`,
      `Scoped` and `Transient`
    - `CodingStyleFeature` is introduced with existing coding styles to separate
      them from business feature
  - `Business` now allows service casting, `service.Cast().To<AnotherService>()`
  - `Authorization` feature is now added with `ClaimBasedAuthorization`
    implementation
  - `HttpServerLayer` now registers authentication services and adds
    authentication middleware
    - provides `SchemeConfigurationCollection` configuration target which
      supports forwarding request to appropriate handlers

## Improvements

- `MvcNewtonsoftJsonOptions` is added to `RestApiLayer` as configuration target
- `IScoped` marker interface is removed, `[Name]Context` convention is
  introduced to configure scoped lifetime by convention
- `FixedToken` authentication is now the default in `Service` blueprint
- `Default` business feature is renamed as `DomainAssemblies`
- `Default` orm feature is renamed as `AutoMap`
- `Documentation` feature is refactored into coding styles and removed
  completely
- `Authentication` feature is now a multi-feature
- Not null parameters throw bad request when null value is received
- `FixedToken` authentication feature is now renamed to `FixedBearerToken`
  - Feature now have `TokenOptions` parameter to configure token names and
    associated claims with given tokens

## Bugfixes

- `TypeModel`'s which are not business types were throwing null reference
  exception for collection properties, fixed
