using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.Authorization.ClaimBased;

public class AddBaseClaimsAsAuthorizePolicyConvention(IEnumerable<string> _baseClaims)
    : IDomainModelConvention<TypeModelContext>, IDomainModelConvention<MethodModelContext>
{
    public void Apply(TypeModelContext context)
    {
        if (!context.Type.TryGetMembers(out var members)) { return; }
        if (!members.TryGet<ControllerModelAttribute>(out var controller)) { return; }
        if (members.Has<AllowAnonymousAttribute>()) { return; }
        if (members.TryGet<RequireUserAttribute>(out var requireUser) && requireUser.Override) { return; }

        foreach (var (key, action) in controller.Action)
        {
            if (members.Methods.Contains(key)) { continue; }

            action.AdditionalAttributes.AddRange(_baseClaims.Select(claim => $"Authorize(Policy = \"{claim}\")"));
        }
    }

    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGet<ActionModelAttribute>(out var action)) { return; }
        if (context.Method.Has<AllowAnonymousAttribute>()) { return; }
        if (context.Method.TryGet<RequireUserAttribute>(out var requireUser) && requireUser.Override) { return; }

        action.AdditionalAttributes.AddRange(_baseClaims.Select(claim => $"Authorize(Policy = \"{claim}\")"));
    }
}