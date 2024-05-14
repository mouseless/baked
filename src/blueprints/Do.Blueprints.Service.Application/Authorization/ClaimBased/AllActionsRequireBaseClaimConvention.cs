using Do.RestApi.Configuration;

namespace Do.Authorization.ClaimBased;

public class AllActionsRequireBaseClaimConvention(string _baseClaim)
    : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (context.Action.MappedMethod is null) { return; }
        if (context.Action.MappedMethod.Has<RequireNoClaimAttribute>()) { return; }
        if (context.Action.MappedMethod.Has<RequireClaimAttribute>()) { return; }

        var attributeSyntax = $"Authorize(Policy = \"{_baseClaim}\")";

        context.Action.AdditionalAttributes.Add(attributeSyntax);
    }
}