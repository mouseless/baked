# Unreleased

## .NET 8 Upgrade

DO now supports .NET 8! Below you can find a task list to upgrade your projects.

```markdown
- [ ] Upgrade .NET and C# versions
  - [ ] in projects
  - [ ] in docker files
  - [ ] in GitHub workflows
- [ ] Upgrade DO version
- [ ] Syntax improvement
  - [ ] Use primary constructors
    - Parameter name start with underscore
    - Use primary c. where dependency injection and record exist
  - [ ] Use collection expressions
- [ ] `[FromKeyedServices]` and `[FromServices]` using in controller
```

### Upgrade .NET and C# versions

- Upgrade the project's `C#` language to `12`.
- Framework version upgrade to `net8.0` in the projects.
- Framework and sdk version upgrade to `8` in `Dockerfile`.
- Upgrade dotnet version `8` in Github actions.

### Syntax improvement

#### Use primary constructors

In projects, use primary constructor when there is a dependency that needs to be
injected at the constructor without any logic. Parameter names start with
underscore.

```csharp
public class Entity(IEntityContext<Entity> _context)
{
  ...
}
```

#### Collection expressions

Use new, simplified collection expressions where possible.

Visit [Collection expression][] for more details.

### `[FromServices]` using in controller

Instead of getting dependency services from the constructor in controllers, get
them using `[FromServices]` attributes.

```csharp
public Entity Get([FromServices] IQueryContext<Entity> service) { }
```

## Improvements

- `ExceptionHandling` now uses
  `Microsoft.AspNetCore.Diagnostics.IExceptionHandling`
- Exceptions now return `ProblemDetails` as response.
- Now using `TimeProvider.System` instead of `ISystem`.
  - Use `FakeTimeProvider` for tests instead of mocking `ISystem`.
- `TheSystem` renamed to `TheTime`
- Internal Server Error response included extra details in message, removed.
  - Extra details can be reached from the logs.

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

[Collection expression]: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/collection-expressions
