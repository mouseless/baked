# Orm

This feature setups _NHibernate_ which registers `IEntityContext` and
`IQueryContext` implementations and adds `AutoPersistenceModel` and
`AutoMapping` conventions along with an `Instantiator` interceptor
for _NHibernate_.

Add this feature using `AddOrm()` extension;

```csharp
app.Features.AddOrm(...);
```

## Default

Adds default opinionated orm feature

```csharp
c => c.Default()
```
