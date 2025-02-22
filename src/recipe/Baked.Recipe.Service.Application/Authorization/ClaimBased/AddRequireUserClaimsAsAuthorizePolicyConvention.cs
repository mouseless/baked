using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.Authorization.ClaimBased;

public class AddRequireUserClaimsAsAuthorizePolicyConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGetSingle<RequireUserAttribute>(out var requireUser)) { return; }
        if (!context.Method.TryGetSingle<ActionModel>(out var action)) { return; }

        action.AdditionalAttributes.AddRange(requireUser.Claims.Select(claim => $"Authorize(Policy = \"{claim}\")"));
    }
}