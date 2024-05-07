using Do.RestApi.Configuration;

namespace Do.Authorization.ClaimBased;

public class RequireNoClaimIsAllowAnonymousConvention : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (context.Action.MappedMethod is null) { return; }
        if (!context.Action.MappedMethod.Has<RequireNoClaim>()) { return; }

        context.Action.AdditionalAttributes.Add("AllowAnonymous");
    }
}