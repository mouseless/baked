# Business

Add this feature implementations using `AddBusiness()` extension;

```csharp
app.Features.AddBusiness(...);
```

## Domain Assemblies

Adds domain types from given assemblies, configures domain model builder with
standard behavior, registers embedded file providers for given assemblies and
builds api model out of domain model.

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

## Convention Extensions

Following `DomainModelConventionCollection` in extensions are provided;

```csharp
// Executes `beforeBuildingIndex`
// `Order` parts will be defaulted to `Business.Defaults.Add`
conventions.AddTypeAttribute(...);
conventions.AddPropertyAttribute(...);
conventions.AddMethodAttribute(...);
conventions.AddParametereAttribute(...);

// Executes `beforeBuildingIndex`
// `Order` parts will be defaulted to `Business.Defaults.Add`
conventions.SetTypeAttribute(...);
conventions.SetPropertyAttribute(...);
conventions.SetMethodAttribute(...);
conventions.SetParametereAttribute(...);

// Executes `beforeBuildingIndex`
// `Order` parts will be defaulted to `Business.Defaults.Add`
conventions.RemoveTypeAttribute(...);
conventions.RemovePropertyAttribute(...);
conventions.RemoveMethodAttribute(...);
conventions.RemoveParametereAttribute(...);

// Executes after `beforeBuildingIndex`
// `Order` parts will be defaulted to `Business.Defaults.Configure`
conventions.AddTypeAttributeConfiguration(...);
conventions.AddPropertyAttributeConfiguration(...);
conventions.AddMethodAttributeConfiguration(...);
conventions.AddParametereAttributeConfiguration(...);

// Adding convention using extensions
configurator.Domain.ConfigureConventions(conventions =>
{
    // Adding convention via extensions
    conventions.SetPropertyAttribute(
        when: c => c.Property.Name == "Id"
        attribute: () => new IdAttribute()
    );
}
```