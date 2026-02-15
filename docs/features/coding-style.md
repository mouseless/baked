# Coding Style

Add this feature using `AddCodingStyles()` extension;

```csharp
app.Features.AddCodingStyles([...]);
```

## Add/Remove Child

Configures method routes in `AddChild` and `RemoveChild(Child)` signature to
have a resource route `POST /../children` and `DELETE /../children/{childId}`
respectively.

```csharp
c => c.AddRemoveChild()
```

## Command Pattern

Uses class names as route and removes `Execute` and `Process` names from route.

```csharp
c => c.CommandPattern()
```

## Entity Subclass

Allows classes to be subclasses of entities via composition. This marks a
transient class as an entity subclass when it implements explicit casting to an
entity. Methods of these extension classes are rendered under entity group. It
uses the first unique property to discriminate entity records.

> [!WARNING]
>
> First unique property is expected to be `enum` or `string`. Otherwise
> subclass routing won't work.

```csharp
c => c.EntitySubclass()
```

## Id

This feature provides `Id` configuration for transient and entity classes.

```csharp
c => c.Id()
```

Single property of type `Baked.Business.Id` is marked with `IdAttribute`. For
entities, `Id` properties are mapped with `IdGuidUserType` and generated with
`IdGuidGenerator` using `DbType.Guid`.

```csharp
public class Entity(IEntityContext<Parent> _context)
{
    public Id Id { get; private set; } = default!;
    ...
}
```

> [!TIP]
>
> To override ID mapping of an entity, add a property attribute configuration on
> `IdAttribute` as below,
>
> ```csharp
> builder.Conventions.AddPropertyAttributeConfiguration<IdAttribute>(
>     when: c => c.Type.Is<MyEntity>(),
>     attribute: id => id.Assigned() // or id.AutoIncrement()
> );
> ```

## Initializable

Adds `TransientAttribute` to the services that has an `Initializer` method.
This coding style makes usages like `_newEntity().With(name)` possible.
`Transient` type's initializer parameters are added to query string and
initalizer is invoked with given parameters when constructing target.

> [!NOTE]
>
> Default initializer method name is set to `With`

```csharp
c => c.Initializable()
```

## Label

Marks selected string properties as labels by giving `LabelAttribute` to
properties with matching names.

```csharp
c => c.Label(propertyNames: [...])
```

> [!NOTE]
>
> Default value of `propertyNames` is `["Display", "Label", "Name", "Title"]`.

## Locatable

Manages binding of `Locatable` targets and api inputs. For `Locatable` types,
this feature adds id parameter to route, configures finding target and parameter
lookup expressions by using `Locatable` attribute.

> [!NOTE]
>
> Parameter lookup is only supported for `Locatable` types

```csharp
c => c.Locatable()
```

## Locatable Extension

Allows classes to extend locatables via composition. This marks a transient
class as a locatable extension when it implements implicit casting to a
locatable. Methods of these extension classes are rendered under locatable
group.

```csharp
c => c.LocatableExtension()
```

## Namespace as Route

Reflects namespace of a domain class as base route for its endpoints.

```csharp
c => c.NamespaceAsRoute()
```

## Object as JSON

Configures all `object` parameters, return types and properties to be treated as
`JSON` content.

```csharp
c => c.ObjectAsJson()
```

## Query

Adds `QueryAttribute` to the classes that has plural name of a locatable class,
e.g. assuming `MyLocatable` is a locatable, `MyLocatables` becomes a query.

Removes `FirstBy`, `SingleBy` and `By` names from API routes and configure them
as `GET` endpoints.

> [!WARNING]
>
> A class that injects `IQueryContext` is not considered as a query class unless
> it satisfies the plural naming convention.

## Records are DTOs

Configures domain type records as valid input parameters. Methods containing
record parameters render as api endpoints.

```csharp
c => c.RecordsAreDtos()
```

## Remaining Services are Singleton

Adds `SingletonAttribute` to the services that has no `TransientAttribute` or
`ScopedAttribute`.

```csharp
c => c.RemainingServicesAreSingleton()
```

## Rich Entity

Adds `EntityAttribute` to classes that inject `IEntityContext<TEntity>`.

Configures `NHibernate` to initialize entities using dependency injection,
making them rich entities.

Configures routes and swagger docs to use entity methods as resource actions.

```csharp
c => c.RichEntity()
```

## Rich Transient

Configures transient services as api services. This coding style marks a type
having a public initializer with a single `Business.Id` parameter which will
render from route, as `RichTransient`, configures `Locatable` attribure and
generates locators.

Rich transients can be method parameters and located using their locators.

Configures routes and swagger docs to use entity methods as resource actions.

```csharp
c => c.RichTransient()
```

## Scoped by Suffix

Adds `ScopedAttribute` to the services that has name with any of the given
suffixes.

```csharp
c => c.ScopedBySuffix(suffixes: ["Context", "Scope"])
```

> [!NOTE]
>
> Default suffix is `Context`.

## `Uri` Return is Redirect

Adds redirect support to your api endpoints. It configures an endpoint to use
redirect result when its corresponding method returns `Uri`. Combined with
`CommandPattern`, it allows you to create callback `GET` endpoints when method
doesn't have any parameters. For actions that have parameters, it configures
its corresponding endpoint to accept form instead of a `json` body.

```csharp
c => c.UriReturnIsRedirect()
```

## Use Built-in Types

Configures built-in .NET types to be used as entity properties and service
parameters. Uses `IParsable` interface to configure primitives. Additionally
configures `string`, enums, `Uri` and `IEnumerable<>` types.

It also allows for string properties to use `TEXT` column type instead of
`VARCHAR` by suffixes.

```csharp
c => c.UseBuiltInTypes(textPropertySuffixes: ["Data", "Description"])
```

> [!TIP]
>
> Default text property suffix is `Data`.

## Use Nullable Types

Adds support for nullable value and reference types. Configures api model to
forbid sending null or empty values to not-null parameters.

```csharp
c => c.UseNullableTypes()
```
