# Exception Handling

This feature allows adding custom _Exception Handlers_.

```csharp
app.Features.AddExceptionHandling(...);
```

## Problem Details

This feature implementation adds `NET` `ProblemDetails` services with a custom
`IExceptionHandler` implementation that returns errors in problem details
format. It also adds a custom middleware that logs exceptions.

```csharp
c => c.ProblemDetails(typeUrlFormat: "https://my-service.com/errors/{0}")
```
`IExceptionHandler` implementation loops through `Baked.IExceptionHandler` 
services and uses first handler which can handle current exception.

Below is a sample `Baked.IExceptionHandler` implementation which handles
`InvalidOperationException` types and returs a `Baked.ExceptionInfo` which will
then be converted to `ProblemDetails` in the handler.

```csharp
public class SampleExceptionHandler : IExceptionHandler
{
    public bool CanHandle(Exception ex) => ex is InvalidOperationException;
    public ExceptionInfo Handle(Exception ex) => new(ex, (int)HttpStatusCode.BadRequest, ex.Message);
}
```

`ProblemDetails` feature registers following `Baked.IExceptionHandler` implementations with
`UnhandledExceptionHandler` fallback;

- `AuthenticationExceptionHandler` for `AuthenticationException`
- `UnauthorizedAccessExceptionHandler` for `UnauthorizedAccessException`
- `HandledExceptionHandler` for `HandledException` types

> [!NOTE]
>
> `HandledExceptionHandler` provides basic exception handling mechanism which will handle 
> user exceptions derived from `HandledException` base class

> [!NOTE]
>
> More to find at [RFC 7807][rfc] and [Handle errors in ASP.NET Core][dotnet].

[rfc]: https://www.rfc-editor.org/rfc/rfc7807.html
[dotnet]: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/error-handling?view=aspnetcore-8.0&source=recommendations#problem-details
