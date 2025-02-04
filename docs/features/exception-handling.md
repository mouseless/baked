# Exception Handling

This feature allows adding custom _Exception Handlers_.

```csharp
app.Features.AddExceptionHandling(...);
```

## Problem Details

This feature implementation adds `NET` `ProblemDetails` services with a custom
`IExceptionHandler` implementation that loops through `Baked.IExceptionHandler` 
services and uses first matching handler for current exception. It also adds a 
custom middleware that logs exceptions.

```csharp
c => c.ProblemDetails(typeUrlFormat: "https://my-service.com/errors/{0}")
```

> [!NOTE]
>
> More to find at [RFC 7807][rfc] and [Handle errors in ASP.NET Core][dotnet].

By default this feature registers following oppinionated `Baked.IExceptionHandler` 
implementations with `UnhandledExceptionHandler` fallback;

- `AuthenticationExceptionHandler` for `AuthenticationException`
- `UnauthorizedAccessExceptionHandler` for `UnauthorizedAccessException`
- `HandledExceptionHandler` for `HandledException` types

All exceptions which are not matched by any registered handlers will then be 
processed by the `UnhandledExceptionHandler` fallback, which returns a `500`
result with exception type as title and a simple detail message.

```json
{
  "type": "https://baked.mouseless.codes/errors/invalid-operation",
  "title": "Invalid Operation",
  "status": 500,
  "detail": "An unexpected error has occured. Please contact the administrator."
}
```

### HandledExceptionHandler

`HandledExceptionHandler` is desinged as a generic exception handler 
implementation for user defined exceptions which are expected to be
derived from `HandledException`. 

```csharp
public class HandledExceptionHandler : IExceptionHandler
{
    public bool CanHandle(Exception ex) => ex is HandledException;
    public ExceptionInfo Handle(Exception ex) => new(ex, (int)((HandledException)ex).StatusCode, ex.Message, ((HandledException)ex).ExtraData);
}
```

#### HandledException

`HandledException` is an opinionated abstraction which helps to provide 
necessary data for an handled exception result.

```csharp
//
// User defined exception
//
public class CustomHandledException(string message)
    : HandledException(message)
{ }
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
