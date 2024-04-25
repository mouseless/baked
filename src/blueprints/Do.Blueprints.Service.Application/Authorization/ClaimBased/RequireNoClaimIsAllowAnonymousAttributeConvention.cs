using Do.RestApi.Configuration;

namespace Do.Authorization.ClaimBased;

public class RequireNoClaimIsAllowAnonymousAttributeConvention : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (context.Action.MethodModel is null) { return; }
        if (!context.Action.MethodModel.CustomAttributes.Contains<RequireNoClaim>()) { return; }

        context.Action.AdditionalAttributes.Add("AllowAnonymous");
    }
}