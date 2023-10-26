# Business

Implementations of this feature will be customized for your own project needs.
A built-in default implementation is provided which uses generated 
a `DomainModel` instance to register components to `IServiceCollection`.

Add this feature implementations using `AddBusiness()` extension;

```csharp
app.Features.AddBusiness(...);
```

## Default

Adds default opinionated business feature

```csharp
c => c.Default()
```
