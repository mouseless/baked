using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.CodingStyle.UriReturnIsRedirect;

public class UriReturnWithoutParameterIsGetConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.DefaultOverload.ReturnType.Is<Uri>(allowAsync: true)) { return; }
        if (!context.Method.TryGetSingle<ActionModel>(out var action)) { return; }
        if (action.InvokedMethodParameters.Any()) { return; }

        action.Method = HttpMethod.Get;
    }
}