using Do.RestApi.Configuration;

namespace Do.CodingStyle.UriReturnIsRedirect;

public class UriReturnWithParameterIsFormPostConvention : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (context.Action.MethodModel is null) { return; }
        if (!context.Action.MethodModel.DefaultOverload.ReturnType.Is<Uri>()) { return; }
        if (!context.Action.MethodModel.DefaultOverload.Parameters.Any()) { return; }

        context.Action.Method = HttpMethod.Post;
        context.Action.UseForm = true;
    }
}