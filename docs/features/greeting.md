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

## Disabled

You can disable this feature by calling `Disabled()` method;

```csharp
c => c.Disabled()
```
