using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.Authorization.ClaimBased;

public class AllowAnonymousIsAllowAnonymousConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.Has<AllowAnonymousAttribute>()) { return; }
        if (!context.Method.TryGetSingle<ActionModel>(out var action)) { return; }

        action.AdditionalAttributes.Add("AllowAnonymous");
    }
}