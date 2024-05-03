using Do.RestApi.Configuration;
using System.Net;

namespace Do.CodingStyle.UriReturnIsRedirect;

public class UriReturnIsRedirectConvention : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (context.Action.MethodModel is null) { return; }
        if (!context.Action.MethodModel.DefaultOverload.ReturnType.Is<Uri>()) { return; }

        context.Action.AdditionalAttributes.Add($"ProducesResponseType((int){typeof(HttpStatusCode).FullName}.Redirect)");
        context.Action.Return.Type = "RedirectResult";
        context.Action.Return.ResultRenderer = resultExpression => $"new RedirectResult($\"{{{resultExpression}}}\", permanent: false)";
    }
}