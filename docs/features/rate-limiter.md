# Rate Limiter

Implementations of this feature provides predefined rate limiter behaviour.
Add this feature using `AddRateLimiter()` extension;

```csharp
app.Features.AddRateLimiter(...);
```

## Concurrency

This feature registers .NET concurrency limiter services and middleware with
`permitLimit` and `queueLimit` options.

```csharp
c => c.Concurrency(permitLimit: 20, queueLimit: 1000)
```

When not given, `permitLimit` is `20` and `queueLimit` is `1000`, or `5` and
`100` in development. Requests beyond the queue limit are rejected.

> [!TIP]
>
> This feature sets `MinThreadCount` to twice the `permitLimit` and
> `MaxThreadCount` to four times the `permitLimit`
