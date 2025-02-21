namespace Baked.CodingStyle.UriReturnIsRedirect;

public class UriReturnWithParameterIsFormPostConvention : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (context.Action.MappedMethod is null) { return; }
        if (!context.Action.Return.TypeModel.Is<Uri>(allowAsync: true)) { return; }
        if (!context.Action.InvokedMethodParameters.Any()) { return; }

        context.Action.Method = HttpMethod.Post;
        context.Action.UseForm = true;
    }
}