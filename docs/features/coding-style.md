# Coding Style

Add this feature using `AddCodingStyles()` extension;

```csharp
app.Features.AddCodingStyles([...]);
```

## Add/Remove Child as Sub Resource

Configures method routes in `AddChild` and `RemoveChild(Child)` signature to
have a resource route `POST /../children` and `DELETE /../children/{childId}`
respectively.

```csharp
c => c.AddRemoveChildAsSubResource()
```

## Command via Method Name

Uses class names as route and removes configured method names from route.

```csharp
c => c.CommandViaMethodName(methodNames: [...])
```

> [!NOTE]
>
> Default value of `methodNames` is `["Execute", "Process"]`.

## Extension via Locatable Initializer

Allows classes to extend locatables via composition. This marks a transient
class as a locatable extension when it has a property with `IdProperty`
attribute and an initializer with only one parameter that is a locatable.
Methods of these extension classes are rendered under locatable group.

```csharp
c => c.ExtensionViaLocatableInitializer()
```

## Flags Enum

Adds support for enums marked with `[Flags]`. Configures data access to map
flags enum properties, including nullable ones, using the enum type itself, and
configures api serialization to represent them as an array of flag names in
`camelCase`.

```csharp
c => c.FlagsEnum()
```

To create a flags enum, mark it with `[Flags]` and give each member a distinct
bit;

```csharp
[Flags]
public enum Permissions
{
    Read = 1 << 0,
    Write = 1 << 1,
    Delete = 1 << 2
}
```

A property of this type is serialized as below, and is deserialized from the
same representation;

```json
["read", "write"]
```

## Initializable via Method Name

Adds `Transient` attribute to the services that has an `Initializer` method.
This coding style makes usages like `_newEntity().With(name)` possible.
`Transient` type's initializer parameters are added to query string and
initalizer is invoked with given parameters when constructing target.

```csharp
c => c.InitializableViaMethodName(initializerNames: [...])
```

> [!NOTE]
>
> Default value of `initializerNames` is `["With"]`.

## Locate via ID

Manages binding of `Locatable` targets and api inputs. For `Locatable` types,
this feature adds id parameter to route, configures finding target and parameter
lookup expressions by using `Locatable` attribute.

```csharp
c => c.LocateViaId()
```

> [!NOTE]
>
> Parameter lookup is only supported for `Locatable` types

## Name based Label

Marks selected string properties as labels by giving `Label` to properties with
matching names.

```csharp
c => c.NameBasedLabel(propertyNames: [...])
```

> [!NOTE]
>
> Default value of `propertyNames` is `["Display", "Label", "Name", "Title"]`.

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

## Primitive via Parsable

Allows creating custom primitives via `IParsable<T>` interface. It marks these
types as `Primitive` attribute and maps them using `PrimitiveUserType` in data
access layer using `NHibernateUtil.String`. Allows serializing and deserializing
to and from `string` in json and API endpoints.

```csharp
c => c.PrimitiveViaParsable()
```

To create a primitive implement `IParsable<>` and override `ToString()`. Below
is an example implementation;

```csharp
public readonly record struct MyValue : IParsable<MyValue>
{
    public static MyValue Parse(string s, IFormatProvider? provider)
    {
        if (!TryParse(s, provider, out var result))
        {
            throw new FormatException($"'{s}' is not in an expected format");
        }

        return result;
    }

    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        IFormatProvider? provider,
        [MaybeNullWhen(false)] out MyValue result
    )
    {
        // Add your custom validation and parse logic here
        result = new(s ?? string.Empty);

        return true;
    }

    readonly string _data;

    MyValue(string data)
    {
        _data = data;
    }

    public override string ToString() =>
        _data;
}
```

## Query via Plural Name

Adds `QueryClass` attribute to the classes that has plural name of a locatable
class, e.g. assuming `MyLocatable` is a locatable, `MyLocatables` becomes a
query.

Removes `FirstBy`, `SingleBy` and `By` names from API routes and configure them
as `GET` endpoints.

Adds `QueryMethod` to the methods having given name of types with `QueryClass`
and marks parameters with `Sorting` and `Paging` attributes.

```csharp
c => c.QueryViaPluralName(
    queryMethodNames: [...],
    primaryParameterNames: [...],
    takeParameterNames: [...],
    skipParameterNames: [...],
    sortParameterNames: [...]
)
```

> [!NOTE]
>
> Default values for parameters are listed below;
>
> - `queryMethodNames`: `["By"]`
> - `primaryParameterNames`: `["searchText"]`
> - `takeParameterNames`: `["take"]`
> - `skipParameterNames`: `["skip"]`
> - `sortParameterNames`: `["sort"]`

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

Adds `Singleton` attribute to the services that has no `Transient` or `Scoped`
attributes.

```csharp
c => c.RemainingServicesAreSingleton()
```

## Resource via ID Initializer

Configures transient services as api services. This coding style marks a type
having a public initializer with a single `Business.Id` parameter which will
render from route, as `Resource`, configures `Locatable` attribute and generates
locators.

Resources can be method parameters and located using their locators.

Configures routes and swagger docs to use entity methods as resource actions.

```csharp
c => c.ResourceViaIdInitializer()
```

## Rich Entity

Adds `Entity` to classes that inject `IEntityContext<TEntity>`.

Configures `NHibernate` to initialize entities using dependency injection,
making them rich entities.

Configures routes and swagger docs to use entity methods as resource actions.

```csharp
c => c.RichEntity()
```

## Scoped via Suffix

Adds `Scoped` attribute to the services that has name with any of the given
suffixes.

```csharp
c => c.ScopedViaSuffix(suffixes: [...])
```

> [!NOTE]
>
> Default value of `suffixes` is `["Context"]`.

## Suffix based Client

Configures `IXxxClient` interfaces as outgoing clients and removes rest binding
for their implementations. Also, adds singleton mock override for the interface
to inject mock instances to domain objects that use client interfaces.

```csharp
c => c.SuffixBasedClient()
```

## Type based ID

This feature provides `Id` configuration for transient and entity classes.

```csharp
c => c.TypeBasedId()
```

Single property of type `Baked.Business.Id` is marked with `IdProperty`
attribute. For entities, `Id` properties are mapped with `IdGuidUserType` and
generated with `IdGuidGenerator` using `DbType.Guid`.

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
> `IdProperty` as below,
>
> ```csharp
> conventions.EditPropertyAttribute<IdProperty>(
>     when: c => c.Type.Is<MyEntity>(),
>     attribute: id => id.Assigned() // or id.AutoIncrement()
> );
> ```

## Unique via SingleBy

Adds `Unique` attribute to entity properties of which corresponding query class
has a `SingleBy...` query method, e.g., `User.Username` property would be
treated as unique if `Users.SingleByUsername` exists.

```csharp
c => c.UniqueViaSingleBy()
```

> [!NOTE]
>
> Having `Unique` attribute on a property tells `AutoMapOrmFeature` to configure
> that column to have a unique constraint.

## `Uri` Return is Redirect

Adds redirect support to your api endpoints. It configures an endpoint to use
redirect result when its corresponding method returns `Uri`. Combined with
`CommandViaMethodName`, it allows you to create callback `GET` endpoints when
method doesn't have any parameters. For actions that have parameters, it
configures its corresponding endpoint to accept form instead of a `json` body.

```csharp
c => c.UriReturnIsRedirect()
```

## Use Built-in Types

Configures built-in .NET types to be used as entity properties and service
parameters. Uses `IParsable<>` interface to configure primitives. Additionally
configures `string`, enums, `Uri` and `IEnumerable<>` types.

It also allows for string properties to use `TEXT` column type instead of
`VARCHAR` by suffixes.

```csharp
c => c.UseBuiltInTypes(textPropertySuffixes: [...])
```

> [!TIP]
>
> Default value of `textPropertySuffixes` is `["Data", "Description"]`.

## Use Nullable Types

Adds support for nullable value and reference types. Configures api model to
forbid sending null or empty values to not-null parameters.

```csharp
c => c.UseNullableTypes()
```
