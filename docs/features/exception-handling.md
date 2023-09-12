# Exception Handling

This feature allows adding custom _Exception Handlers_.

```csharp
app.Features.AddExceptionHandling(...);
```

## Default

Adds default exception handler.

```csharp
c => c.Default()
```
