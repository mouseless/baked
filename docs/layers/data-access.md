# Data Access

DO uses NHibernate and FluentNHibernate library when providing data access
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

## Phases

This layer uses following phases;

- AddServices: DataAccess Layer registers `NHConfiguration`, `ISession` 
  `ISessionFactory` to `IServiceCollection` for setting up NHibernate