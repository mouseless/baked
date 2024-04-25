using Do.RestApi.Configuration;

namespace Do.Authorization.ClaimBased;

public class AllRequestsShouldRequireBaseClaimConvention(string _baseClaim)
    : IApiModelConvention<ActionModelContext>, IApiModelConvention<ApiModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (context.Action.MethodModel is null) { return; }
        if (context.Action.MethodModel.CustomAttributes.Contains<RequireNoClaim>()) { return; }
        if (context.Action.MethodModel.CustomAttributes.Contains<RequireClaimAttribute>()) { return; }

        var attributeSyntax = $"Authorize(Policy = \"{_baseClaim}\")";

        context.Action.AdditionalAttributes.Add(attributeSyntax);
    }

    public void Apply(ApiModelContext context)
    {
        context.Api.Usings.Add("Microsoft.AspNetCore.Authorization");
    }
}