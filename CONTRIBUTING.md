# Contributing

This project is developed and maintained by Mouseless Software Development
Collective. It is, and will always be, free and open source.

## Project Structure

- `/docs`: Documentation site. It is a single website that documents every
  package
- `/samples`: sample projects are here. Each project should be in its own
  folder
- `/src`: all source code that we ship as nuget packages
  - `/blueprints`: blueprint packages
  - `/core`: core packages that every type of project will have a reference to
- `/test`: test automation projects
  - `/blueprints`: e2e test projects per blueprint package
  - `/core`: unit test projects per package

## Naming Conventions

- Use [Layer.Conventions](https://github.com/mouseless/do/tree/main/docs/architecture/layer.md) 
  when adding a new layer
- Use [Feature.Conventions](https://github.com/mouseless/do/tree/main/docs/architecture/feature.md) 
  when adding a new feature or feature implementation
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
- Refer to [PrimaryConstructos](https://github.com/mouseless/learn-dotnet/tree/main/primary-constructor/README.md)
  for coding standards we follow when using `PrimaryConstructors`.
- Refer to [NullableUsage](https://github.com/mouseless/learn-dotnet/tree/main/nullable-usage/README.md)
  for coding standards we follow when using `nullable` value and reference 
  types.
- Refer to [Stylecop.Analyzers](https://github.com/mouseless/learn-dotnet/tree/main/analyzers/README.md)
  for coding standards we enforce using `Stylecop Analyzers`.
- Use named arguments when calling methods with optional parameters
  ```csharp
  public void Method(string required
      string? optional = default
  )
  { 
    ... 
  }

  // ⛔ Wrong
  service.Method("Required", "Optional");
  // ✅ Correct
  service.Method("Required", optional: "Optional");
  ``` 
- Don't use `[TestFixture]` attribute, nunit runs tests without it anyway

## Feature project conventions;

- When there is a single implementation, it becomes `Do.{Feature}` and
  `Do.{Feature}.{Implementation}`, and the configurator is embedded in the
  single implementation project.
- When there are multiple implementations of a feature, the configurator class
  is moved to `Do.{Feature}.Configuration` (or `.Base` :thinking:), and all
  implementations will depend on this config project.
- `Do.csproj` contains ports such as `Logging`, `Setting`, etc. that will be
  used in all projects (but `do` cannot be released, maybe it could be
  `do-core` :thinking:).
- `Do.Configuration.csproj` contains configurator classes and feature
  interfaces when a second implementation of these ports are introduced.
- Sometimes a layer and feature will provide meaningful functionality when they
  coexist, such as `HttpClient` and `Communication`. In this case, a layer can
  be bundled with the feature and can be published in a single package like 
  `Do.Communication`, meaning a layer can come from a package not named after 
  itself.
  - However, a feature implementation must always be published with the package
    name matching its own port.