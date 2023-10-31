# Unreleased

## Features

- Beta features are available in `do-blueprints-service` package;
  - `DomainLayer` is added which creates `DomainModel` phase artifact to be 
    used in features
  - Added a default `BusinessFeature` which uses `DomainModel` to register
    services

## Improvements    

- Features can now access `ApplicationContext` through `LayerConfigurator`