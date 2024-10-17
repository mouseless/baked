# Business

Add this feature implementations using `AddBusiness()` extension;

```csharp
app.Features.AddBusiness(...);
```

## Domain Assemblies

Adds domain types from given assemblies, configures domain model builder with
standard behavior, registers embedded and physical file providers for given 
assemblies and builds api model out of domain model.

All types from domain assemblies are treated as domain types except exceptions,
attributes, delegates and static classes. It also marks some domain types as
service via adding `ServiceAttribute` metadata. Service domain types are public
classes that are not an enumerable or a record. It also skips generic type
definitions.

> [!NOTE]
>
> Methods that are _NOT_ defined under service domain types are marked with
> `ExternalAttribute`. This is to avoid `ToString` and similar methods to be
> treated as non-business logic, while allowing you to define business logic in
> your own base classes.

Additionally, it registers types that implement `ICasts<,>` interface under
`Caster` to allow static casting under `implicit` and `explicit` operators.

```csharp
c => c.DomainAssemblies([typeof(MyClass).Assembly])
```
