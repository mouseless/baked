# Unreleased

## Features

- Beta features are available in `do-blueprints-service` package;
  - `Core` feature is added with `Dotnet` implementation
  - `HttpServer` layer is added, you can now configure
    `IMiddlewareCollection` along with `IEndpointRouteBuilder`
    - `Web` layer is merged into this new layer and `IApplicationBuilder` is
      removed from configuration
