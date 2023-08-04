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

## Welcome Page

Adds Microsoft's `WelcomePage` extension to the `WebApplication`.

```csharp
c => c.WelcomePage()
```

By default it adds the welcome page to root path `/`. You can provide another
path to keep the welcome page in;

```csharp
c => c.WelcomePage("/hi")
```

## Disabled

You can disable this feature by calling `Disabled()` method;

```csharp
c => c.Disabled()
```
