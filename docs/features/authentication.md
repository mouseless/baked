# Authentication

Implementations of this feature adds an `AuthenticationScheme` with an
`IAuthenticationHandler` implementation. Features can also configure
`AuthenticationSchemeOptions.ForwardDefaultSelector` which enables multiple
authentication handlers for a single endpoint by selecting first matching
handler available.

Add this feature using `AddAuthentications()` extension;

```csharp
app.Features.AddAuthentications([...]);
```

## Fixed Bearer Token

This feature uses the token provided in `Authorization` header. Handler tests
the token against the value configured in settings and setups `Claims` for
current `HttpContext.User`. Multiple tokens can be used by providing key names
when adding the feature and `Authentication:FixedBearerToken:Default` value from
settings will be used if no token key specified

```csharp
c => c.FixedBearerToken(tokens =>
{
    tokens.Add("ServiceA", claims: ["ClaimX"]);
    tokens.Add("ServiceB", claims: ["ClaimX", "ClaimY"]);
})
```

```json
"Authentication": {
  "FixedBearerToken": {
    "ServiceA": "SERVICE_A_TOKEN",
    "ServiceB": "SERVICE_B_TOKEN"
  }
}
```

The feature also provides a form post authentication mechanism. The handler
looks for a form parameter named `hash` then validates the request using other
form parameters and token. The expected hash is generated as below;

1. Sort all form parameters except `hash` by their keys
2. URL encode each key and value, and join them as `key=value`
3. Join all pairs with `&`
4. Sign the result using `HMAC-SHA256` with token value as the key
5. Convert the signature to a `Base64` string

For example, form parameters `fee=3` and `amount=12` signed with
`SERVICE_A_TOKEN` gives the following;

```
message: amount=12&fee=3
hash:    bfegZ/mTBlu4zHtPGwUVYdO9szVG1ii+5VQZR9l/yHM=
```

> [!NOTE]
>
> Form post authentication will only work if there is no authorization header
> exists in the request.

## Jwt

This feature registers jwt authentication services from `.NET` along with
`ITokenBuilder` implementation for generating jwt tokens. This feature also
enables authorized routing and fetch requests in `@mouseless/baked` by adding an
auth plugin and custom fetch service.

```csharp
c => c.Jwt(
  configureOptions: options => { ... },
  configurePlugin: plugin => { ... }
)
