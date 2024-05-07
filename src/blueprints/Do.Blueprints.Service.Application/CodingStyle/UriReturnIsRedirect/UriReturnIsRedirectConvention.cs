using Do.RestApi.Configuration;
using System.Net;

namespace Do.CodingStyle.UriReturnIsRedirect;

public class UriReturnIsRedirectConvention : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (context.Action.MappedMethod is null) { return; }
        if (!context.Action.Return.TypeModel.Is<Uri>(allowAsync: true)) { return; }

        context.Action.AdditionalAttributes.Add($"ProducesResponseType((int){typeof(HttpStatusCode).FullName}.Redirect)");
        context.Action.Return.Type = context.Action.Return.IsAsync ? "Task<RedirectResult>" : "RedirectResult";
        context.Action.Return.ResultRenderer = resultExpression => $"new RedirectResult($\"{{{resultExpression}}}\", permanent: false)";
    }
}