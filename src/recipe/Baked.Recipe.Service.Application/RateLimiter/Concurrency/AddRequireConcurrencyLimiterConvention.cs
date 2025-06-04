using Baked.Authorization;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.RateLimiter.Concurrency;

public class AddRequireConcurrencyLimiterConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGetSingle<ActionModelAttribute>(out var action)) { return; }
        if (context.Method.Has<AllowAnonymousAttribute>()) { return; }
        if (context.Method.TryGetSingle<RequireUserAttribute>(out var requireUser) && requireUser.Override) { return; }

        action.AdditionalAttributes.Add("EnableRateLimiting(\"Concurrency\")");
    }
}