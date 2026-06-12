# Business

Add this feature implementations using `AddBusiness()` extension;

```csharp
app.Features.AddBusiness(...);
```

This feature abstraction provides following extensions to
`DomainModelConventionCollection`;

- Executes before building index and `Order` is defaulted to
  `Business.Custom.Add`
  ```csharp
  conventions.SetTypeAttribute(...);
  conventions.SetPropertyAttribute(...);
  conventions.SetMethodAttribute(...);
  conventions.SetParametereAttribute(...);

  conventions.AddTypeAttribute(...);
  conventions.AddPropertyAttribute(...);
  conventions.AddMethodAttribute(...);
  conventions.AddParametereAttribute(...);

  conventions.RemoveTypeAttribute(...);
  conventions.RemovePropertyAttribute(...);
  conventions.RemoveMethodAttribute(...);
  conventions.RemoveParametereAttribute(...);
  ```
- Executes after building index and `Order` is defaulted to
  `Business.Custom.Configure`
  ```csharp
  conventions.AddTypeAttributeConfiguration(...);
  conventions.AddPropertyAttributeConfiguration(...);
  conventions.AddMethodAttributeConfiguration(...);
  conventions.AddParametereAttributeConfiguration(...);
  ```

> [!TIP]
>
> See [Layers / Domain / Ordering Conventions](../layers/domain.md#ordering-conventions)
> for more information on convention order mechanism

Below you can find sample for adding convention using extensions;

```csharp
configurator.Domain.ConfigureConventions(conventions =>
{
    conventions.SetPropertyAttribute(
        when: c => c.Property.Name == "Id"
        attribute: () => new IdAttribute()
    );
}
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
