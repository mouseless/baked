# Baked

Baked is an opinionated framework for .NET and Vue.

It proposes well designed conventions to help you express your application
logic with ease. It doesn't reinvent libraries that already exists, but brings
them together with built-in configurations.

## 🚀 Getting Started

This quick start creates a minimal Baked app from scratch using .NET and SQLite.

### 1. Create a Solution and Projects

```bash
dotnet new sln -n Sample
dotnet new web -n Sample.Application
dotnet new classlib -n Sample
dotnet sln add Sample.Application/Sample.Application.csproj Sample/Sample.csproj
dotnet add Sample.Application reference Sample
```

### 2. Add Baked Packages

```bash
dotnet add Sample.Application package Baked
dotnet add Sample package Baked.Abstractions
```

### 3. Configure the Application

Edit `Sample.Application/Program.cs`:

```csharp
Bake.New
    .Monolith(
        business: c => c.DomainAssemblies(typeof(HelloWorld).Assembly),
        database: c => c.Sqlite("Sample.db")
    )
    .Run();
```

### 4. Add a Sample Domain Class

Create `Sample/HelloWorld.cs`:

```csharp
namespace Sample;

public class HelloWorld
{
    public string GetGreeting(string name) =>
        $"Hello {name}";
}
```

### 4. Run the App

```bash
dotnet run --project Sample.Application
```

## 🗺️ Roadmap

Our goal for **Baked** is to reach a **stable and production-ready release**
with complete documentation and tutorials by the end of **2026**.

Throughout this period, we’ll focus on:

- **Stabilizing the core runtime** — finalizing conventions for domain
  assemblies, descriptor generation, and API composition.
- **Improving developer experience** — refining project structure, configuration
  flow, and runtime feedback for a smoother development process.
- **Extending UI generation** — richer component descriptors, better integration
  with Vue/Nuxt, and type-safe schema exports.
- **Documentation and learning materials** — end-to-end guides, real-world
  samples, and architectural walkthroughs for both .NET and frontend developers.
- **Community and collaboration** — inviting contributors, maintaining a clear
  release cadence, and sharing internal design notes openly.

Our aim is for anyone to be able to build **domain-driven web applications**
with **zero boilerplate**, clear conventions, and a transparent design
philosophy by the end of 2026.

---

Check out our documentation site for more information: [baked.mouseless.codes][]

[baked.mouseless.codes]: https://baked.mouseless.codes
