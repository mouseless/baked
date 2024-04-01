# Unreleased

## Features

- Beta features are available in do-blueprints-service package;
  - `CodeGenerationLayer` is introduced, now it is possible to generate code
    during initialization of a service
  - `DomainLayer` now provides an inherited factory mechanism to individually
    configure level of reflected details for registered types. Models for types
    are now built as following inherited levels;
    - `TypeModel`
    - `TypeModelGenerics`
    - `TypeModelInheritance`
    - `TypeModelMetadata`
    - `TypeModelMembers`
  - `DomainLayer` now provides a metadata mechanism that allows you to add
    custom attributes to domain model
  - `DomainLayer` now provides an indexer mechanism that allows you to index
    models based on their attributes
  - `RestApiLayer` now provides api model to generate controller code from
    domain model
  - `RestApiLayer` now provides model binder configuration to allow custom
    parameter types from action parameters
  - `DefaultBusinessFeature` now generates api model from domain model
  - `DefaultBusinessFeature` now generates model binders for entity parameters

## Improvements

- `MvcNewtonsoftJsonOptions` is added to `RestApiLayer` as configuration target

## Bugfixes

- `TypeModel`'s which are not business types were throwing null reference
  exception for collection properties, fixed
