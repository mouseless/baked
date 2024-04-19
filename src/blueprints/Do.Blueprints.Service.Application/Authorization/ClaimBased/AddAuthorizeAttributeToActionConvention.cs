using Do.RestApi.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace Do.Authorization.ClaimBased;

public class AddAuthorizeAttributeToActionConvention : IApiModelConvention<ActionModelContext>, IApiModelConvention<ApiModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (context.Action.MethodModel is null) { return; }
        // TODO CustomAttribute access is changed, use helpers Has
        if (!context.Action.MethodModel.CustomAttributes.Contains<AuthorizeAttribute>()) { return; }

        foreach (var attribute in context.Action.MethodModel.CustomAttributes.Get<AuthorizeAttribute>())
        {
            // Get CSharpFriendlyName from TypeModel
            context.Action.AdditionalAttributes.Add($"""Microsoft.AspNetCore.Authorization.Authorize(Policy = "{attribute.Policy}")""");
        }
    }

    public void Apply(ApiModelContext context)
    {
        //context.Api.usi
    }
}