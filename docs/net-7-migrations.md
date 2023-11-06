# .NET 6 👉 .NET 7

Upgrades flowed in the following order.

```markdown
- [ ] Upgrade .NET and C# versions
  - [ ] in projects
  - [ ] in docker files
  - [ ] in GitHub workflows
- [ ] Migrate to central package management
- [ ] Utilize regex source generator
```

## Upgrade .NET and C# versions

### in projects

- Change the C# version to `11.0` in `<LangVersion>` tag in
  `Directory.Build.props`.
- Change the .NET version to `net7.0` in `<TargetFramework>` tag in
  `Directory.Build.props`.

### in docker files

Update .NET version in `Dockerfile`

- replace `.../dotnet/aspnet:6.0-focal` to `.../dotnet/aspnet:7.0`
- replace `.../dotnet/sdk:6.0-focal` to `.../dotnet/sdk:7.0`
- again docker compose up ✅

### in GitHub workflows

Update `dotnet-version` to `7`

```yml
- name: Setup .NET
  uses: actions/setup-dotnet@v3
  with:
    dotnet-version: 7
```

## Migrate to central package management

- Add `Directory.Packages.props` root
- Set `true` to `<ManagePackageVersionsCentrally>`
- Go through all `.csproj` and collect all packages into
  `Directory.Packages.props` and remove versions from all `.csproj`.

Visit [Central Package Management][] for more details.

## Utilize regex source generator

Use regex source generator where possible.

Visit [Regular expressions][] for more details.

[Central Package Management]: https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-7#central-package-management
[Regular expressions]: https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-7#regular-expressions
