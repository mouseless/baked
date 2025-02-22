using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.Authorization.ClaimBased;

public class RequireUserIsAuthorizeConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.Has<RequireUserAttribute>()) { return; }
        if (!context.Method.TryGetSingle<ActionModel>(out var action)) { return; }

        action.AdditionalAttributes.Add("Authorize");
    }
}