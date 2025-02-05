# Exception Handling

This feature allows adding custom _Exception Handlers_.

```csharp
app.Features.AddExceptionHandling(...);
```

## Problem Details

This feature implementation adds .NET `ProblemDetails` services with a custom
`IExceptionHandler` implementation that loops through `Baked.IExceptionHandler` 
services and uses first matching handler for current exception. It also adds a 
custom middleware that logs exceptions.

```csharp
c => c.ProblemDetails(typeUrlFormat: "https://my-service.com/errors/{0}")
```

> [!NOTE]
>
> More to find at [RFC 7807][rfc] and [Handle errors in ASP.NET Core][dotnet].

By default this feature handles following exceptions;

- `AuthenticationException` which returns 401 status code
- `UnauthorizedAccessExceptionHandler` which returns 403 status code
- `HandledException` which returns 400 status code

`HandledException` is an opinionated abstraction which helps providing
necessary data for a handled exception result. Below is a sample for creating 
a user defined exception;

```csharp
public class CustomHandledException(string message)
    : HandledException(message)
{ }
```

All remaining exceptions will be handled by the `UnhandledExceptionHandler` 
fallback, which returns a `500` result with exception type as title and a 
simple detail message.

```json
{
  "type": "https://baked.mouseless.codes/errors/invalid-operation",
  "title": "Invalid Operation",
  "status": 500,
  "detail": "An unexpected error has occured. Please contact the administrator."
}
```

### Adding Custom Exception Handlers

To add a custom exception handler, you can create a  
`Baked.IExceptionHandler` implementation and register it in
`ConfigurationOverriderFeature`. 

Below is a sample for how to create and register an exception handler

```csharp
//
// Create a custom exception handler implementation
//
public class SampleExceptionHandler : IExceptionHandler
{
    public bool CanHandle(Exception ex) => ex is InvalidOperationException;
    public ExceptionInfo Handle(Exception ex) => new(ex, (int)HttpStatusCode.BadRequest, ex.Message);
}

//
// Add to service collection in ConfigurationOverriderFeature
//
public class ConfigurationOverriderFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddSingleton<IExceptionHandler, SampleExceptionHandler>();
        });
    }
}
```

[rfc]: https://www.rfc-editor.org/rfc/rfc7807.html
[dotnet]: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/error-handling?view=aspnetcore-9.0&source=recommendations#problem-details
