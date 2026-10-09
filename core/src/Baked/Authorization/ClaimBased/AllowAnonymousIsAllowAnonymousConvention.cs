using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.Authorization.ClaimBased;

public class AllowAnonymousIsAllowAnonymousConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(TypeModelContext context)
    {
        if (!context.Type.TryGetMembers(out var members)) { return; }
        if (!members.TryGet<ApiController>(out var controller)) { return; }
        if (!members.Has<AllowAnonymous>()) { return; }

        foreach (var (key, action) in controller.Action)
        {
            if (members.Methods.Contains(key)) { continue; }

            action.AdditionalAttributes.Add("AllowAnonymous");
        }
    }

    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGet<ApiAction>(out var action)) { return; }
        if (!context.Method.Has<AllowAnonymous>()) { return; }

        action.AdditionalAttributes.Add("AllowAnonymous");
    }
}