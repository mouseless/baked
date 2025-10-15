# Contributing

This project is developed and maintained by Mouseless Software Development
Collective. It is, and will always be, free and open source.

## Project Structure

- `/core`: Core project in .NET that contains both backend runtime and buildtime
  - `/src`: All source code that we ship as nuget packages
  - `/test`: Contains test projects (test domain, test app, specs, load test)
- `/docs`: Documentation site. It is a static website that uses nuxt
- `/samples`: Sample projects are here. each project should be in its own folder
- `/ui`: UI project in Nuxt that contains all UI components as a nuxt module
  - `/playground`: Test app that uses the nuxt module
  - `/src`: Nuxt module source files
  - `/test`: Playwright specs that tests playground spec pages

## Naming Conventions

- Use [Layer.Conventions][] when adding a new layer
- Use [Feature.Conventions][] when adding a new feature or feature
  implementation
- Use documentation heading names for text fixtures
  - ⛔ Wrong => `AddExtensionTest`
  - ✅ Correct => `AddingExtensions`
- Use `Extensions` suffix for static extension classes

## Packaging

We aim to release a single package per platform named `Baked` or `baked` to keep
things simple. For now, Baked is on NuGet and npm.

- We follow Microsoft's conventions, so we created implementation and
  abstraction packages named `Baked` and `Baked.Abstractions`, respectively
- Unfortunately, npm package `baked` is taken, so we used `@mouseless/baked` for
  this
  - If we need to release another npm package we'll use `@mouseless/baked-xx`
    format

If we release a package in another runtime, we'll try to keep things as simple
and consistent as possible.

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
  public void Method(string required,
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
