using Do.RestApi.Configuration;

namespace Do.Authorization.ClaimBased;

public class AllActionsRequireBaseClaimConvention(string _baseClaim) : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (context.Action.MappedMethod is null) { return; }
        if (context.Action.MappedMethod.CustomAttributes.Contains<RequireNoClaim>()) { return; }
        if (context.Action.MappedMethod.CustomAttributes.Contains<RequireClaimAttribute>()) { return; }

        var attributeSyntax = $"Authorize(Policy = \"{_baseClaim}\")";

        context.Action.AdditionalAttributes.Add(attributeSyntax);
    }
}