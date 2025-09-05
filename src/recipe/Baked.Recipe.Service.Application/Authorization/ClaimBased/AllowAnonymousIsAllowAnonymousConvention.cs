using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.Authorization.ClaimBased;

public class AllowAnonymousIsAllowAnonymousConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGet<ActionModelAttribute>(out var action)) { return; }
        if (!context.Method.Has<AllowAnonymousAttribute>()) { return; }

        action.AdditionalAttributes.Add("AllowAnonymous");
    }
}