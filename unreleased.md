# Unreleased

## .NET 8 Update

DO now supports .NET 8! Below you can find a task list to upgrade your projects.

```markdown
- [ ] Upgrade .NET and C# versions
  - [ ] in projects
  - [ ] in docker files
  - [ ] in GitHub workflows
- [ ] [Library upgrades](#library-upgrades)
- [ ] Syntax improvement
  - [ ] Use primary constructors
    - Parameter name start with underscore
    - Use primary c. where dependency injection and record exist
  - [ ] Use collection expressions
  - [ ] Use default lambda parameters
- [ ] Use/Test `DO` source link
- [ ] Switch to `IExceptionHandling`.
- [ ] `TimeProvider` to be injected.
- [ ] `[FromKeyedServices]` and `[FromServices]` using in controller
- [ ] Null, white space checks in the parameter are provided by
  `ThrowIfNullOrWhiteSpace` etc. extensions of `ArgumentException`.
```

### Upgrade .NET and C# versions

- Upgraded the project's `C#` language to `12`.
- Framework version upgraded to `net8.0` in the projects.
- Framework and sdk version upgraded to `8` in `Dockerfile`.
- Upgraded dotnet version `8` in Github actions.

### Syntax improvement

#### Use primary constructors

In projects, we use primary constructor when there is a dependency that we get
from the constructor and when we get it without logic. We start parameter names
with underscore.

#### Use collection expressions

Where we can use collection expressions in projects, we tried to use them for
readability and more comfortable writing.

### New Exception Handling

In Exception handling, we switched to `IExceptionHandling` that `Dotnet`
brought with `.NET 8`. We switched to `ProblemDetails` format in exception
responses.

`IExceptionHandling` usage

```csharp
services.AddExceptionHandler<ExceptionHandlingMiddleware>();

...

middleware.Add(app => app.UseExceptionHandler());
```

Using `ProblemDetails`

```csharp
services.AddProblemDetails();

...

public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
{
  ...
  var problemDetails = new ProblemDetails
  {
    ...
  };

  ...

  await httpContext.Response
    .WriteAsJsonAsync(problemDetails, cancellationToken);

  return true;
}
```

### New TimeProvider

We switched to `TimeProvider` in time management. We use it by registering
`TimeProvider.System` with dependency injection.

We use `FakeTimeProvider` with wrapping in tests. Because it does not allow
going back in time, we use it by resetting the time.

### `[FromServices]` using in controller

Instead of getting dependency services from the constructor in controllers, we
switched to getting them with `[FromServices]` attributes.

```csharp
public Entity Get([FromServices] IQueryContext<Entity> service) { }
```

## Library Upgrades

| Package                                         | Old Version | New Version |
| ----------------------------------------------- | ----------- | ----------- |
| Microsoft.AspNetCore.Mvc.NewtonsoftJson         | 7.0.13      | 8.0.0       |
| Microsoft.Extensions.Configuration.Abstractions | 7.0.0       | 8.0.0       |
| Microsoft.Extensions.Configuration.Binder       | 7.0.0       | 8.0.0       |
| Microsoft.Extensions.Logging.Abstractions       | 7.0.0       | 8.0.0       |
| Microsoft.NET.Test.Sdk                          | 7.0.0       | 8.0.0       |
| Microsoft.Extensions.TimeProvider.Testing       | -           | new*        |
| Moq                                             | 4.20.69     | 4.20.70     |
| NHibernate                                      | 5.4.6       | 5.5.0       |
| NUnit                                           | 3.14.0      | 4.0.1       |
| StyleCop.Analyzers.Unstable                     | 1.2.0.507   | 1.2.0.556   |
