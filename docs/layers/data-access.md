# Data Access

Baked uses _NHibernate_ and _FluentNHibernate_ library when providing data
access layer.

```csharp
app.Layers.AddDataAccess();
```

## Configuration Targets

This layer provides `PersistenceConfiguration`, `AutomappingConfiguration`
`AutoPersistenceModel`, `InterceptorConfiguration`, `FluentConfiguration`
for configuring _NHibernate_ behavior and `IDatabaseInitializationCollection`
for configuring database initialization actions.

### `PersistenceConfiguration`

This target is provided in `AddServices` phase. To configure it in a
feature;

```csharp
configurator.DataAccess.ConfigurePersistence(persistence =>
{
    ...
});
```

### `InterceptorConfiguration`

This target is provided in `AddServices` phase right after
`PersistenceConfiguration`. To configure it in a feature;

```csharp
configurator.DataAccess.ConfigureNHibernateInterceptor(interceptor =>
{
    ...
});
```

### `AutomappingConfiguration`

This target is provided in `AddServices` phase right after
`InterceptorConfiguration`. To configure it in a feature;

```csharp
configurator.DataAccess.ConfigureAutomapping(automapping =>
{
    ...
});
```

### `AutoPersistenceModel`

This target is provided in `AddServices` phase right after
`AutomappingConfiguration`. To configure it in a feature;

```csharp
configurator.DataAccess.ConfigureAutoPersistenceModel(autoPersistenceModel =>
{
    ...
});
```

### `FluentConfiguration`

This target is provided in `AddServices` phase. To configure it in a
feature;

```csharp
configurator.DataAccess.ConfigureFluentConfiguration(fluentConfiguration =>
{
    ...
});
```

### `IDatabaseInitializationCollection`

This target is provided in `PostBuild` phase. To configure it in a
feature;

```csharp
configurator.DataAccess.ConfigureDatabaseInitializationCollection(initializations =>
{
    ...
});
```