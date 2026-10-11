using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.Authorization.ClaimBased;

public class RequireUserIsAuthorizeConvention : IDomainModelConvention<TypeModelContext>, IDomainModelConvention<MethodModelContext>
{
    public void Apply(TypeModelContext context)
    {
        if (!context.Type.TryGetMembers(out var members)) { return; }
        if (!members.TryGet<ApiController>(out var controller)) { return; }
        if (!members.Has<RequireUser>()) { return; }

        foreach (var (key, action) in controller.Action)
        {
            if (members.Methods.Contains(key)) { continue; }

            action.AdditionalAttributes.Add("Authorize");
        }
    }

    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGet<ApiAction>(out var action)) { return; }
        if (!context.Method.Has<RequireUser>()) { return; }

        action.AdditionalAttributes.Add("Authorize");
    }
}