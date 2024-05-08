# Authorization

Add this feature using `AddAuthorization()` extension;

```csharp
app.Features.AddAuthorization();
```

## Claim Based

This feature adds `AuthorizationMiddleware` to the request pipeline and adds 
authorization policies with given claim requirements. 

```csharp
 c => c.ClaimBased(claims: ["User", "Admin"], baseClaim: "User");
```

Feature gets claim requirement for methods by using `RequireClaimAttribute` 
metadata and adds appropriate `AuthorizationAttribute` to the specified 
controller actions by with `ApiModel` conventions.

```csharp 
[RequireClaim("Admin")]
public string AdminMethod() 
{ 
  ... 
}
```

When base claim is set, authorization will be enabled for all actions with 
base claim requirement if not specified. `RequireNoClaim` attribute will 
override this convention and allow anonymous access to the specified methods.

```csharp
[RequireNoClaim]
public void AnonymousMethod() { }
```

This feature also registers a custom `IAuthorizationMiddlewareResultHandler`
and this handler directly throws;

- `AuthenticationException` when challenged
- `UnauthorizedAccessExcetpin` when forbidden

instead of using `IAuthenticationHandler` fallback methods. 