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

## Coding Standards

- Use `_` prefix for private fields
  - ⛔ Wrong => `int id;`
  - ✅ Correct => `int _id;`
- Don't use access modifier when they are default
  - ⛔ Wrong => `private int _id;`
  - ✅ Correct => `int _id;`
- Don't use `[TestFixture]` attribute, nunit runs tests without it anyway
- Refer to [Stylecop.Analyzers](https://github.com/mouseless/learn-dotnet/tree/main/analyzers/README.md)
  for coding standards we enforce using `Stylecop Analyzers`.

## Naming Conventions

- Use documentation heading names for text fixtures
  - ⛔ Wrong => `AddExtensionTest`
  - ✅ Correct => `AddingExtensions`
- Use `Extensions` suffix for static extension classes
