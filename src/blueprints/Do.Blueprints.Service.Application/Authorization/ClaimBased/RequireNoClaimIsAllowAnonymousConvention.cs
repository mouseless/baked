using Do.RestApi.Configuration;

namespace Do.Authorization.ClaimBased;

public class RequireNoClaimIsAllowAnonymousConvention : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (context.Action.MethodModel is null) { return; }
        if (!context.Action.MethodModel.CustomAttributes.Contains<RequireNoClaim>()) { return; }

        var attributeSyntax = "AllowAnonymous";

        context.Action.AdditionalAttributes.Add(attributeSyntax);
    }
}