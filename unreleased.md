# Unreleased

## .NET 8 Update

DO now supports .NET 8! Below you can find a task list to upgrade your projects.

```markdown
- [ ] Update language version
- [ ] Update dotnet version
- [ ] Update on worfklow
- [ ] Library upgrades
- [ ] Docker upgrade
- [ ] Syntax improvement
  - [ ] Use primary constructors
    - Parameter name start with underscore
    - Use primary c. where dependency injection and record exist
  - [ ] Use collection expressions
  - [ ] Use default lambda parameters
- [ ] Use/Test source link
- [ ] switch to `IExceptionHandling`.
- [ ] `TimeProvider` to be injected.
- [ ] `[FromKeyedServices]` and `[FromServices]` using in controller
- [ ] Null, white space checks in the parameter are provided by
  `ThrowIfNullOrWhiteSpace` etc. extensions of `ArgumentException`.
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
| NHibernate                                      | 5.4.6       | 5.4.7       |
| NUnit                                           | 3.14.0      | 4.0.1       |
| StyleCop.Analyzers.Unstable                     | 1.2.0.507   | 1.2.0.556   |
