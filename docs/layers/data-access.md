# Data Access

DO uses _NHibernate_ and _FluentNHibernate_ library when providing data access
layer.

```csharp
app.AddDataAccess();
```

## Configuration Target

This layer provides `PersistenceConfiguration`, `AutomappingConfiguration`
`AutoPersistenceModel`, `InterceptorConfiguration` for configuring
_NHibernate_ behaviour.

### `PersistenceConfiguration`

```csharp
configurator.ConfigurePersistence(persistence =>
{
    ...
});
```

### `AutomappingConfiguration`

```csharp
configurator.ConfigureAutomapping(automapping =>
{
    ...
});
```

### `AutoPersistenceModel`

```csharp
configurator.ConfigureAutoPersistenceModel(autoPersistenceModel =>
{
    ...
});
```

### `InterceptorConfiguration`

```csharp
configurator.ConfigureNHibernateInterceptor(interceptor =>
{
    ...
});
```
