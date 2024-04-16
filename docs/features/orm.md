# ORM

This features basically configures `DataAccessLayer` so that it interprets
certain domain types as entities and queries.

Add this feature using `AddOrm()` extension;

```csharp
app.Features.AddOrm(...);
```

## Auto Map

Auto maps domain types with `Entity` attribute using default opinions of
`FluentNHibernate` along with `Guid Id` properties as primary key and configure
foreign key references between entities.

```csharp
c => c.AutoMap()
```
