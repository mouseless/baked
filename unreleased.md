# Unreleased

## Features

- Beta features are available in `do-blueprints-service` package;
  - `DomainLayer` is added which creates `DomainModel` phase artifact to be 
    used in features
  - Added `IScoped` marker interface  
  - Added a default `BusinessFeature` which uses `DomainModel` to register
    services using following conventions:
    - Types which have _With_ method with return type same as declering type
      are registered as `transient` services with `singleton` factories
    - Types which implements `IScoped` interface are registered as 
      `scoped` services with `singleton` factories
    - Remaining types are registered as `singleton` services
    - Implemented interfaces are registered for types with same service 
      lifetimes, if interfaces belongs to any assembly in the added to 
      assembly descriptor list

## Improvements    

- Features can now access `ApplicationContext` through `LayerConfigurator`