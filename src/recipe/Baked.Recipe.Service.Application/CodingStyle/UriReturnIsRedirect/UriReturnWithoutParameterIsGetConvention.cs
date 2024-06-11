using Baked.RestApi.Configuration;

namespace Baked.CodingStyle.UriReturnIsRedirect;

public class UriReturnWithoutParameterIsGetConvention : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (context.Action.MappedMethod is null) { return; }
        if (!context.Action.Return.TypeModel.Is<Uri>(allowAsync: true)) { return; }
        if (context.Action.InvokedMethodParameters.Any()) { return; }

        context.Action.Method = HttpMethod.Get;
    }
}