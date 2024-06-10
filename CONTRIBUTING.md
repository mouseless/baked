# Contributing

This project is developed and maintained by Mouseless Software Development
Collective. It is, and will always be, free and open source.

## Project Structure

- `/docs`: Documentation site. It is a single website that documents every
  package
- `/samples`: sample projects are here. Each project should be in its own folder
- `/src`: all source code that we ship as nuget packages
  - `/tools`: CLI tools
  - `/extensions`: features and/or layers
  - `/recipe`: recipe packages
  - `/core`: core packages that every type of project will have a reference to
- `/test`: test automation projects
  - `/recipe`: e2e test projects per recipe package
  - `/core`: unit test projects per package

## Feature project conventions;

- When there is a single implementation, it becomes `Baked.{Feature}` and
  `Baked.{Feature}.{Implementation}`, and the configurator is embedded in the
  single implementation project.
- When there are multiple implementations of a feature, the configurator class
  is moved to `Baked.{Feature}.Configuration` (or `.Base` :thinking:), and all
  implementations will depend on this config project.
- `Baked.csproj` contains ports such as `Logging`, `Setting`, etc. that will be
  used in all projects.
- `Baked.Configuration.csproj` contains configurator classes and feature
  interfaces when a second implementation of these ports are introduced.
- Sometimes a layer and feature will provide meaningful functionality when they
  coexist, such as `HttpClient` and `Communication`. In this case, a layer can
  be bundled with the feature and can be published in a single package like
  `Baked.Communication`, meaning a layer can come from a package not named after
  itself.
  - However, a feature implementation must always be published with the package
    name matching its own port.

## Naming Conventions

- Use [Layer.Conventions][] when adding a new layer
- Use [Feature.Conventions][] when adding a new feature or feature
  implementation
- Use documentation heading names for text fixtures
  - ⛔ Wrong => `AddExtensionTest`
  - ✅ Correct => `AddingExtensions`
- Use `Extensions` suffix for static extension classes

## Coding Standards

- Use file scoped namespaces
- Use `_` prefix for private fields
  - ⛔ Wrong => `int id;`
  - ✅ Correct => `int _id;`
- Don't use access modifier when they are default
  - ⛔ Wrong => `private int _id;`
  - ✅ Correct => `int _id;`
- Use named arguments when calling methods with optional parameters
  ```csharp
  public void Method(string required
      string? optional = default
  )
  // ⛔ Wrong
  service.Method("Required", "Optional");
  // ✅ Correct
  service.Method("Required", optional: "Optional");
  ```
- Don't use `[TestFixture]` attribute, nunit runs tests without it anyway
- Place static factory or helper methods at the top of before any instance
  members
- Refer to [PrimaryConstructos][] for coding standards we follow when using
  `PrimaryConstructors`.
- Refer to [NullableUsage][] for coding standards we follow when using
  `nullable` value and reference types.
- Refer to [Stylecop.Analyzers][] for coding standards we enforce using
  `Stylecop Analyzers`.

[Layer.Conventions]: https://github.com/mouseless/baked/tree/main/docs/architecture/layer.md
[Feature.Conventions]: https://github.com/mouseless/baked/tree/main/docs/architecture/feature.md
[PrimaryConstructors]: https://github.com/mouseless/learn-dotnet/tree/main/primary-constructor/README.md
[NullableUsage]: https://github.com/mouseless/learn-dotnet/tree/main/nullable-usage/README.md
[Stylecop.Analyzers]: https://github.com/mouseless/learn-dotnet/tree/main/analyzers/README.md
