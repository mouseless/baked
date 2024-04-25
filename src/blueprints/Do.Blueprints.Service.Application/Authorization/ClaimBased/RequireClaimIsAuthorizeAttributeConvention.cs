using Do.RestApi.Configuration;

namespace Do.Authorization.ClaimBased;

public class RequireClaimIsAuthorizeAttributeConvention : IApiModelConvention<ActionModelContext>, IApiModelConvention<ApiModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (context.Action.MethodModel is null) { return; }
        // TODO CustomAttribute access is changed, use helpers Has
        if (!context.Action.MethodModel.CustomAttributes.Contains<RequireClaimAttribute>()) { return; }

        foreach (var require in context.Action.MethodModel.CustomAttributes.Get<RequireClaimAttribute>())
        {
            var attributeSyntax = $"Authorize(Policy = \"{require.Claim}\")";

            context.Action.AdditionalAttributes.Add(attributeSyntax);
        }
    }

    public void Apply(ApiModelContext context)
    {
        context.Api.Usings.Add("Microsoft.AspNetCore.Authorization");
    }
}