using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.Authorization.ClaimBased;

public class RequireUserIsAuthorizeConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGetSingle<ActionModelAttribute>(out var action)) { return; }
        if (!context.Method.Has<RequireUserAttribute>()) { return; }

        action.AdditionalAttributes.Add("Authorize");
    }
}