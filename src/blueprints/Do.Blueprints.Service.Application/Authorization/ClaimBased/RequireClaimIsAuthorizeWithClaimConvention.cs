using Do.RestApi.Configuration;

namespace Do.Authorization.ClaimBased;

public class RequireClaimIsAuthorizeWithClaimConvention : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (context.Action.MethodModel is null) { return; }
        if (!context.Action.MethodModel.CustomAttributes.Contains<RequireClaimAttribute>()) { return; }

        foreach (var attribute in context.Action.MethodModel.CustomAttributes.Get<RequireClaimAttribute>())
        {
            var attributeSyntax = $"Authorize(Policy = \"{attribute.Claim}\")";

            context.Action.AdditionalAttributes.Add(attributeSyntax);
        }
    }
}