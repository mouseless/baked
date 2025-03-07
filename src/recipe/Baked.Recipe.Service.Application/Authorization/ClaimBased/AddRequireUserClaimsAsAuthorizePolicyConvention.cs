using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.Authorization.ClaimBased;

public class AddRequireUserClaimsAsAuthorizePolicyConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGetSingle<ActionModelAttribute>(out var action)) { return; }
        if (!context.Method.TryGetSingle<RequireUserAttribute>(out var requireUser)) { return; }

        action.AdditionalAttributes.AddRange(requireUser.Claims.Select(claim => $"Authorize(Policy = \"{claim}\")"));
    }
}