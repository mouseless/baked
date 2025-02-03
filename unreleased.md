# Unreleased

## Features

- `Application` now provies `Bake` and `Start` modes which can be run both 
  together or individually with distinct `ApplicationContext`'s.
  - `RunFlags` is introduced for configuring application mode
- `LayerBase` now provies `GetBakePhases()` method to enable registering
  specific phases to run at `Bake` mode 
- `Service` and `Data Source` recipies now triggers `Bake` mode run at post 
  build
- `Domain` layer's `AddDomainTypes` and `BuildDomainModel` phases now only runs
  in `Bake` mode
- `CodeGeneration` layer's `GenerateCode` and `Compile` phases now only runs
  in `Bake` mode
- `CodeGeneration` layer now introduces `IGeneratedFileCollection` which enables
  generating data files in `Bake` mode
- `CodeGeneration` layer now introduces `GeneratedContext` at `BuildConfiguration`
  phase which provides access to generated assemblies and files in `Start` mode
- `Domain` layer now provides a `DomainServicesModel` configuration target in 
  `Bake` mode which will then as be used to generate `IServiceAdder` implementation 

## Improvements

- `CodeGeneration` layer now compiles and saves generated assemblies and files 
  to entry assembly location with `ASPNETCORE_ENVIRONMENT` subfolder
- `DomainAssemblies` feature now generates
  - `ICasterConfigurer`
  implementations and 
  - `TagDescriptor`
  - `RequestResponseExample`
  json files in `Bake` mode   
- Following features now generates `IServiceAdder` implementations from 
  `DomainModel` in `Bake` mode   
  - `AutoMapOrm`
- `GiveMe.PropertyOf<T>` helper is renamed to `ThePropertyOf<T>`  
- `GiveMe.MethodOf<T>` helper is renamed to `TheMethodOf<T>`  
- Following features now use `DomainServicesModel` target to register services
  - `Transient`
  - `Scoped`
  - `Singleton`
- `ProblemDetails` feature now no longer searches domain assemblies and
  `IExceptionHandler` implementations are required to be added in 
  `ConfigurationOverrider` feature
  
  