using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.RateLimiter.Concurrency;

public class AddRequireConcurrencyLimiterConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGetSingle<ActionModelAttribute>(out var action)) { return; }

        action.AdditionalAttributes.Add("""EnableRateLimiting("Concurrency")""");
    }
}