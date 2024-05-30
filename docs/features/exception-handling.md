# Exception Handling

This feature allows adding custom _Exception Handlers_.

```csharp
app.Features.AddExceptionHandling(...);
```

## Default

Adds default exception handler as well as middleware to log exceptions.

```csharp
c => c.Default(typeUrlFormat: "https://my-service.com/errors/{0}")
```
