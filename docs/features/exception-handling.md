# Exception Handling

This feature allows adding custom _Exception Handlers_.

```csharp
app.Features.AddExceptionHandling(...);
```

## Problem Details

Adds an exception handler implementation that returns errors in problem details
format. It also adds a middleware that logs exceptions.

```csharp
c => c.ProblemDetails(typeUrlFormat: "https://my-service.com/errors/{0}")
```

> [!NOTE]
>
> More to find at [RFC 7807][rfc] and [Handle errors in ASP.NET Core][dotnet].

[rfc]: https://www.rfc-editor.org/rfc/rfc7807.html
[dotnet]: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/error-handling?view=aspnetcore-8.0&source=recommendations#problem-details
