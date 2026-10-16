# Baked

Baked is an opinionated framework for .NET and Vue.

You write domain objects in C#. Baked uses conventions to turn them into a REST
API with Swagger documentation and, optionally, a generated admin UI built with
Nuxt.

## Getting Started

Create a solution with an application and a domain project:

```bash
dotnet new sln -n Sample
dotnet new web -n Sample.Application
dotnet new classlib -n Sample
dotnet sln add Sample.Application/Sample.Application.csproj Sample/Sample.csproj
dotnet add Sample.Application reference Sample
```

Add Baked packages:

```bash
dotnet add Sample.Application package Baked
dotnet add Sample package Baked.Abstractions
```

Replace `Sample.Application/Program.cs` with:

```csharp
using Sample;

Bake.New
    .Monolith(
        business: c => c.DomainAssemblies(typeof(HelloWorld).Assembly),
        options: o => o.Database(c => c.Sqlite("Sample.db"))
    )
    .Run();
```

Add a domain class to `Sample/HelloWorld.cs`:

```csharp
namespace Sample;

public class HelloWorld
{
    public string GetGreeting(string name) =>
        $"Hello {name}";
}
```

Run the application:

```bash
dotnet run --project Sample.Application
```

The root URL opens Swagger, and `GET /hello-world/greeting?name=Baked` returns
`"Hello Baked"`.

## Documentation

See [baked.mouseless.codes][] for recipes, features, layers and architecture.

## Status

Baked is under active development and its API may change between minor versions.
Breaking changes are listed in the [release notes][].

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md).

## License

Baked is licensed under the [MIT License](LICENSE).

[baked.mouseless.codes]: https://baked.mouseless.codes
[release notes]: https://baked.mouseless.codes/release-notes
