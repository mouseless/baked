# Unreleased

## Features

- Beta features are available in `do-blueprints-service` package;
  - `Core` feature is added with `Dotnet` and `Mock` implementations
  - `DataAccess` layer is added, you can now configure `NHibernate`
  - `Monitoring` layer is added, you can now configure `ILoggerBuilder`
  - `HttpServer` layer is added, you can now configure
    `IMiddlewareCollection` along with `IEndpointRouteBuilder`
    - `Web` layer is merged into this new layer and `IApplicationBuilder` is
      removed from configuration
  - `Configuration` layer is added, you can now configure
    `IConfigurationBuilder`
  - `RestApi` layer is added, you can now configure `SwaggerGenOptions`,
    `SwaggerOptions` and `SwaggerUIOptions` along with
    `IApplicationPartCollection`
  - `Testing` layer is added, you can now configure `TestConfiguration`
  - `Swagger` implementation is added for greeting feature
  - `Logging` feature is added with `Request` implementation
  - `MockOverrider`feature is added with `FirstInterface` implementation
  - `Business` features is added with no implementation.
  - `HelloWorld` implementation of `Greeting` feature is now removed.