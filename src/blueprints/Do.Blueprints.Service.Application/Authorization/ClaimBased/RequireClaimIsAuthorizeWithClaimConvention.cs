using Do.RestApi.Configuration;

namespace Do.Authorization.ClaimBased;

public class RequireClaimIsAuthorizeWithClaimConvention : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (context.Action.MappedMethod is null) { return; }
        if (!context.Action.MappedMethod.CustomAttributes.Contains<RequireClaimAttribute>()) { return; }

        foreach (var attribute in context.Action.MappedMethod.CustomAttributes.Get<RequireClaimAttribute>())
        {
            var attributeSyntax = $"Authorize(Policy = \"{attribute.Claim}\")";

            context.Action.AdditionalAttributes.Add(attributeSyntax);
        }
    }
}