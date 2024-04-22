using Do.RestApi.Configuration;
using Microsoft.AspNetCore.Authorization;
using System.Text;

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
            var attributeSyntaxBuilder = new StringBuilder();
            attributeSyntaxBuilder.Append("Authorize");

            var args = new List<string>();
            if (!string.IsNullOrEmpty(attribute.AuthenticationSchemes))
            {
                args.Add($"AuthenticationSchemes = \"{attribute.AuthenticationSchemes}\"");
            }

            if (!string.IsNullOrEmpty(attribute.Policy))
            {
                args.Add($"Policy = \"{attribute.Policy}\"");
            }

            if (!string.IsNullOrEmpty(attribute.Roles))
            {
                args.Add($"Roles = \"{attribute.Roles}\"");
            }

            if (args.Any())
            {
                attributeSyntaxBuilder.Append($"({string.Join(',', args)})");
            }

            context.Action.AdditionalAttributes.Add(attributeSyntaxBuilder.ToString());
        }
    }

    public void Apply(ApiModelContext context)
    {
        context.Api.Usings.Add("Microsoft.AspNetCore.Authorization");
    }
}