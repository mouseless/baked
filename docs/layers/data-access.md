# Data Access

DO uses _NHibernate_ and _FluentNHibernate_ library when providing data access
layer.

```csharp
app.AddDataAccess();
```

## Configuration Target

This layer provides `PersistenceConfiguration`, `AutomappingConfiguration`
`AutoPersistenceModel`, `InterceptorConfiguration` for configuring
NHibernate behaviour.

### `PersistenceConfiguration`

```csharp
configurator.ConfigurePersistence(logging =>
{
    ...
});
```

### `AutomappingConfiguration`

```csharp
configurator.ConfigureAutomapping(logging =>
{
    ...
});
```

### `AutoPersistenceModel`

```csharp
configurator.ConfigureAutoPersistenceModel(logging =>
{
    ...
});
```

### `InterceptorConfiguration`

```csharp
configurator.ConfigureNHibernateInterceptor(logging =>
{
    ...
});
```
