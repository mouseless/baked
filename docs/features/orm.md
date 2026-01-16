# ORM

This features basically configures `DataAccessLayer` so that it interprets
certain domain types as entities and queries.

Add this feature using `AddOrm()` extension;

```csharp
app.Features.AddOrm(...);
```

## Auto Map

Auto maps domain types with `Entity` attribute using default opinions of
`FluentNHibernate` along with `Id` properties as primary key and configure
foreign key references between entities.

Also adds api model conventions that enables;

- Getting entity types directly from api inputs
- Hiding method name from route for `By` methods, exposing them under `GET
  /entities` route

```csharp
c => c.AutoMap()
```
