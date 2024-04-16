# Business

Add this feature implementations using `AddBusiness()` extension;

```csharp
app.Features.AddBusiness(...);
```

## Domain Assemblies

Adds domain types from given assemblies, configures domain model builder with
standard behavior and builds api model out of domain model.

All types from domain assemblies are treated as domain types except exceptions,
attributes, delegates and static classes. It also marks some domain types as
service via adding `ServiceAttribute` metadata. Service domain types are public
classes that are not an enumerable or a record. It also skips generic type
definitions.

```csharp
c => c.DomainAssemblies([typeof(MyClass).Assembly])
```
