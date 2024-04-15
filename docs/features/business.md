# Business

Implementations of this feature will be customized for your own project needs.
A built-in implementation is provided which configures and uses the generated a
`DomainModel` instance to register components to `IServiceCollection`.

Add this feature implementations using `AddBusiness()` extension;

```csharp
app.Features.AddBusiness(...);
```

## Domain Assemblies

Adds domain types from given assemblies, configures domain model builder with
standard behavior and builds api model out of domain model.

```csharp
c => c.DomainAssemblies([typeof(MyEntity).Assembly])
```
