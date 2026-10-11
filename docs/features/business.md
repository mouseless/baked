# Business

Add this feature implementations using `AddBusiness()` extension;

```csharp
app.Features.AddBusiness(...);
```

> [!TIP]
>
> See [Extensions](#extensions) for helpers that are available regardless of the
> implementation

## Domain Assemblies

Adds domain types from given assemblies, configures domain model builder with
standard behavior, registers embedded file providers for given assemblies and
builds api model out of domain model.

All types from domain assemblies are treated as domain types except exceptions,
attributes, delegates and static classes. It also marks some domain types as
service via adding `Service` attribute. Service domain types are public classes
that are not an enumerable or a record. It also skips generic type definitions.

> [!NOTE]
>
> Methods that are _NOT_ defined under service domain types are marked with
> `External` attribute. This is to avoid `ToString` and similar methods to be
> treated as non-business logic, while allowing you to define business logic in
> your own base classes.

```csharp
c => c.DomainAssemblies([typeof(MyClass).Assembly])
```

## Extensions

Business abstraction provides below extensions regardless of the
implementation.

### Conventions

This feature abstraction provides following extensions to
`DomainModelConventionCollection`;

- Executes before building index and `Order` is defaulted to
  `Business.Defaults.Add`
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
  `Business.Defaults.Configure`
  ```csharp
  conventions.EditTypeAttribute(...);
  conventions.EditPropertyAttribute(...);
  conventions.EditMethodAttribute(...);
  conventions.EditParametereAttribute(...);
  ```

> [!TIP]
>
> See [Layers / Domain / Ordering
> Conventions](../layers/domain.md#ordering-conventions) for more information on
> convention order mechanism

Below you can find sample for adding convention using extensions;

```csharp
configurator.Domain.ConfigureConventions(conventions =>
{
    conventions.SetPropertyAttribute(
        when: c => c.Property.Name == "Id"
        attribute: () => new IdProperty()
    );
}
```

### Validation

`Validate` is a service for checking business rules inside domain objects.
Inject it and chain `That` / `ThatAsync` calls. Sync and async rules can be
mixed in one chain, which is awaited once at the end. Rules run in order and
stop at the first failure.

Each rule can ask for what it needs through its lambda parameters:

```csharp
_validate.That(() => ...);
_validate.That(sp => ...);
_validate.That(l => ...);
_validate.That((l, sp) => ...);
```

`sp` is the service provider, and `l` localizes field names. `l` clears the
field prefix, titleizes the name using invariant culture, and then looks it up
via `IStringLocalizer`, so `_field.PropertyName` becomes `Property Name` before
localization.

The recommended way to add a rule is to define a handled exception together with
a `Validate` extension:

```csharp
public static class Exceptions
{
    public class RequiredFieldException(string name)
        : HandledException("{0} is required",
            extraData: new() { { nameof(name), name } }
        );

    extension(Validate validate)
    {
        public Validate RequiredField<T>([NotNull] T value,
            [CallerArgumentExpression(nameof(value))] string name = ""
        ) => validate.That(l =>
            {
                if (value is not null) { return; }

                throw new RequiredFieldException(l(name));
            });
    }
}
```

Then use it in domain logic:

```csharp
_validate
    .RequiredField(_field.PropertyName)
    .RequiredField(name)
;
```

> [!TIP]
>
> `CallerArgumentExpression` captures the argument as written, so pass fields
> and parameters directly. `_field.PropertyName` is reported as `Property Name`
> and `name` as `Name`, without passing names by hand.
