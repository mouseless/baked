# Greeting

This feature provides a greeting message to show that application is up and
running.

Add this feature using `AddGreeting()` extension;

```csharp
app.Features.AddGreeting(...);
```

## Hello World

Prints hello world at the root path `/`.

```csharp
c => c.HelloWorldFeature()
```

## Welcome Page

Adds Microsoft's `WelcomePageFeature` extension to the `WebApplication`.

```csharp
c => c.WelcomePageFeature()
```

By default it adds the welcome page to root path `/`. You can provide another
path to keep the welcome page in;

```csharp
c => c.WelcomePageFeature("/hi")
```

## Disabled

You can disable this feature by calling `Disabled()` method;

```csharp
c => c.Disabled()
```
