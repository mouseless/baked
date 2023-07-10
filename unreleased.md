# Unreleased

## Feature

- `Do.Architecture` package is introduced. This package exposes core _feature_
  and _layer_ system that is mentioned in
  [Architecture](../architecture/README.md) section
- `Greeting` feature is added with two implementations `HelloWorld` and
  `WelcomePage`
- `DependencyInjection` layer is added, you can now configure
  `IServiceCollection` in features
- `Web` layer is added, you can now configure `IApplicationBuilder` and
  `IEndpointRouteBuilder` in features
