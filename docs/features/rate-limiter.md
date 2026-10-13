# Rate Limiter

Implementations of this feature provides predefined rate limiter behaviour.
Add this feature using `AddRateLimiter()` extension;

```csharp
app.Features.AddRateLimiter(...);
```

## Concurrency

This feature registers .NET concurrency limiter services and middleware with
`permitLimit` and `queueLimit` options 

```csharp
c => c.Concurrency()
```

> [!TIP]
>
> This feature will set `permitLimit` count as `MinThreadCount`
> and twice the value as `MaxThreadCount`
