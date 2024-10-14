# Data Access

Baked uses _NHibernate_ and _FluentNHibernate_ library when providing data
access layer.

```csharp
app.Layers.AddDataAccess();
```

## Configuration Targets

This layer provides `PersistenceConfiguration`, `AutomappingConfiguration`
`AutoPersistenceModel`, `InterceptorConfiguration` and `FluentConfiguration` 
for configuring _NHibernate_ behavior.

### `PersistenceConfiguration`

This target is provided in `AddServices` phase. To configure it in a
feature;

```csharp
configurator.ConfigurePersistence(persistence =>
{
    ...
});
```

### `InterceptorConfiguration`

This target is provided in `AddServices` phase right after
`PersistenceConfiguration`. To configure it in a feature;

```csharp
configurator.ConfigureNHibernateInterceptor(interceptor =>
{
    ...
});
```

### `AutomappingConfiguration`

This target is provided in `AddServices` phase right after
`InterceptorConfiguration`. To configure it in a feature;

```csharp
configurator.ConfigureAutomapping(automapping =>
{
    ...
});
```

### `AutoPersistenceModel`

This target is provided in `AddServices` phase right after
`AutomappingConfiguration`. To configure it in a feature;

```csharp
configurator.ConfigureAutoPersistenceModel(autoPersistenceModel =>
{
    ...
});
```

### `FluentConfiguration`

This target is provided in `AddServices` phase. To configure it in a
feature;

```csharp
configurator.ConfigureFluentConfiguration(fluentConfiguration =>
{
    ...
});
```