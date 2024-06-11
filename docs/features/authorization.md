# Authorization

Add this feature using `AddAuthorization()` extension;

```csharp
app.Features.AddAuthorization();
```

## Claim Based

This feature adds `AuthorizationMiddleware` to the request pipeline and adds
authorization policies with given claim requirements.

```csharp
 c => c.ClaimBased(claims: ["User", "Admin"], baseClaims: ["User"]);
```

Feature gets claim requirement for methods by using `RequireUserAttribute`
metadata and adds appropriate `AuthorizeAttribute` to the specified controller
actions by with `ApiModel` conventions.

```csharp
[RequireUser(["Admin"])]
public string AdminMethod()
{
    ...
}
```

When base claim is set, authorization will be enabled for all actions with base
claim requirement if not specified. `AllowAnonymous` attribute will override
this convention and allow anonymous access to the specified methods.

```csharp
[AllowAnonymous]
public void AnonymousMethod()
{
    ...
}
```

This feature also registers a custom `IAuthorizationMiddlewareResultHandler`
and this handler directly throws;

- `AuthenticationException` when challenged
- `UnauthorizedAccessExcetpin` when forbidden

instead of using `IAuthenticationHandler` fallback methods.

## Disabled

You can disable this feature by calling `Disabled()` method;

```csharp
c => c.Disabled()
```
