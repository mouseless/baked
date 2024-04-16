# Coding Style

Add this feature using `AddCodingStyles()` extension;

```csharp
app.Features.AddCodingStyles([...]);
```

## Object as JSON

Configures all `object` parameters, return types and properties to be treated as
`JSON` content.

```csharp
c => c.ObjectAsJson()
```

## Remaining Services are Singleton

Adds `SingletonAttribute` to the services that has no `TransientAttribute` or
`ScopedAttribute`.

```csharp
c => c.RemainingServicesAreSingleton()
```

## Rich Entity

Adds `QueryAttribute` to classes that inject `IQueryContext<TEntity>`. Using
generic argument of `IQueryContext<TEntity>` finds corresponding entity class
and add `EntityAttribute` to it.

Configures `NHibernate` to initialize entities using dependency injection, making
them rich entities.

Configures routes and swagger docs to use entity methods as resource actions.

```csharp
c => c.RichEntity()
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

## Use Built-in Types

Configures built-in .NET types to be used as entity properties and service
parameters. Uses `IParsable` interface to configure primitives. Additionally
configures `string`, enums, `Uri`, `IEnumerable<>` and `Nullable<>` types.

It also allows for string properties to use `TEXT` column type instead of
`VARCHAR` by suffixes.

```csharp
c => c.UseBuiltInTypes(textPropertySuffixes: ["Data", "Description"])
```

> [!TIP]
>
> Default text property suffix is `Data`.

## With Method

Adds `TransientAttribute` to the services that has a `With` method. This coding
style makes usages like `newEntity().With(name)` possible.

```csharp
c => c.WithMethod()
```
