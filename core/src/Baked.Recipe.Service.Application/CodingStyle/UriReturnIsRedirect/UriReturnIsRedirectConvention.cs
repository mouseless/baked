using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using System.Net;

namespace Baked.CodingStyle.UriReturnIsRedirect;

public class UriReturnIsRedirectConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGet<ActionModelAttribute>(out var action)) { return; }
        if (!context.Method.DefaultOverload.ReturnType.Is<Uri>(allowAsync: true)) { return; }

        action.AdditionalAttributes.Add($"ProducesResponseType((int){nameof(HttpStatusCode)}.Redirect)");
        action.ReturnType = action.ReturnIsAsync ? "Task<RedirectResult>" : "RedirectResult";
        action.ReturnResultRenderer = resultExpression => $"new RedirectResult($\"{{{resultExpression}}}\", permanent: false)";
    }
}