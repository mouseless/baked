# Greeting

This feature provides a greeting message to show that application is up and
running.

Add this feature using `AddGreeting()` extension;

```csharp
app.Features.AddGreeting(...);
```

## Swagger

Redirects root path `/` to `SwaggerUI` for `RestApi` documentation.

```csharp
c => c.Swagger()
```
