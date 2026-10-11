using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.Authorization.ClaimBased;

public class AddRequireUserClaimsAsAuthorizePolicyConvention : IDomainModelConvention<TypeModelContext>, IDomainModelConvention<MethodModelContext>
{
    public void Apply(TypeModelContext context)
    {
        if (!context.Type.TryGetMembers(out var members)) { return; }
        if (!members.TryGet<ApiController>(out var controller)) { return; }
        if (!members.TryGet<RequireUser>(out var requireUser)) { return; }

        foreach (var (key, action) in controller.Action)
        {
            if (members.Methods.Contains(key)) { continue; }

            action.AdditionalAttributes.AddRange(requireUser.Claims.Select(claim => $"Authorize(Policy = \"{claim}\")"));
        }
    }

    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGet<ApiAction>(out var action)) { return; }
        if (!context.Method.TryGet<RequireUser>(out var requireUser)) { return; }

        action.AdditionalAttributes.AddRange(requireUser.Claims.Select(claim => $"Authorize(Policy = \"{claim}\")"));
    }
}