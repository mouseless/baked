using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.Authorization.ClaimBased;

public class RequireUserIsAuthorizeConvention : IDomainModelConvention<TypeModelContext>, IDomainModelConvention<MethodModelContext>
{
    public void Apply(TypeModelContext context)
    {
        if (!context.Type.TryGetMembers(out var members)) { return; }
        if (!members.TryGet<ControllerModelAttribute>(out var controller)) { return; }
        if (!members.Has<RequireUserAttribute>()) { return; }

        foreach (var (key, action) in controller.Action)
        {
            if (members.Methods.Contains(key)) { continue; }

            action.AdditionalAttributes.Add("Authorize");
        }
    }

    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGet<ActionModelAttribute>(out var action)) { return; }
        if (!context.Method.Has<RequireUserAttribute>()) { return; }

        action.AdditionalAttributes.Add("Authorize");
    }
}