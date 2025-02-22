using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.Authorization.ClaimBased;

public class AddBaseClaimsAsAuthorizePolicyConvention(IEnumerable<string> _baseClaims)
    : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGetSingle<ActionModel>(out var action)) { return; }
        if (context.Method.Has<AllowAnonymousAttribute>()) { return; }
        if (context.Method.TryGetSingle<RequireUserAttribute>(out var requireUser) && requireUser.Override) { return; }

        action.AdditionalAttributes.AddRange(_baseClaims.Select(claim => $"Authorize(Policy = \"{claim}\")"));
    }
}