# .Net 6 👉 .Net 7

Upgrades flowed in the following order.

```markdown
- [ ] `c#` version upgrade
- [ ] `.Net` version upgrade
- [ ] Version upgrades in `Do.Blueprints.Service`
  - [ ] Microsoft.Extensions.Configuration.Abstractions
  - [ ] Microsoft.Extensions.Configuration.Binder
- [ ] Version upgrades in `Do.Blueprints.Service.Application`
  - [ ] Microsoft.AspNetCore.Mvc.NewtonsoftJson
- [ ] Other version upgrades in `Do.Architecture`
  - [ ] Moq
- [ ] Other version upgrades in `Do.Blueprints.Service.Application`
  - [ ] FluentNHibernate
  - [ ] MySql.Data
- [ ] Docker .Net update
  - [ ] Update version in Dockerfile
```

## C# version upgrade

Change the `C#` version between `<LangVersion>` tags in `Directory.Build.props`.

## .Net version upgrade

Change the `.Net` version between `<TargetFramework>` tags in
`Directory.Build.props`.

## Do.Architecture

After the upgrade related to `.Net7` was successful in the whole project, the
following upgrade was done.

- `Moq` 4.20.69
  - Build ✅
  - Test ✅

## Do.Blueprints.Service

- `Microsoft.Extensions.Configuration.Abstractions` 7.0.0
  - Build ✅
  - Test ✅
- `Microsoft.Extensions.Configuration.Binder` 7.0.4
  - Build ✅
  - Test ✅

## Do.Blueprints.Service.Application

The following upgrade is the part related to the `.Net` 7 upgrade.

- `Microsoft.AspNetCore.Mvc.NewtonsoftJson` 7.0.13
  - Build ✅
  - Test ✅

After the upgrade related to `.Net7` was successful in the whole project, the
following upgrade was done.

- `FluentNHibernate` 3.3.0
  - Build ✅
  - Test ✅
- `MySql.Data` 8.2.0
  - Build ✅
  - Test ✅

## Docker

- Update `.Net` version in `Dockerfile`
  - replace `.../dotnet/aspnet:6.0-focal` to `.../dotnet/aspnet:7.0-focal`
  - docker compose up ❌ (`load metadata for mcr.microsoft.com/dotnet/aspnet:7.0-focal`)
  - replace `.../dotnet/aspnet:7.0-focal` to `.../dotnet/aspnet:7.0`
  - replace `.../dotnet/sdk:7.0-focal` to `.../dotnet/sdk:7.0`
  - again docker compose up ✅
