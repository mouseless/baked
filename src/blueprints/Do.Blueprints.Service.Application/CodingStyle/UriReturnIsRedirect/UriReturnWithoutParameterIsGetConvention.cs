using Do.RestApi.Configuration;

namespace Do.CodingStyle.UriReturnIsRedirect;

public class UriReturnWithoutParameterIsGetConvention : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (context.Action.MappedMethod is null) { return; }
        if (!context.Action.MappedMethod.DefaultOverload.ReturnType.Is<Uri>()) { return; }
        if (context.Action.MappedMethod.DefaultOverload.Parameters.Any()) { return; }

        context.Action.Method = HttpMethod.Get;
    }
}