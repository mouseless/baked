namespace Baked.RestApi.Conventions;

public class ProducesJsonConvention(
    Func<ActionModelContext, bool>? _when = default
) : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (_when is not null && !_when(context)) { return; }

        context.Action.AdditionalAttributes.Add("Produces(\"application/json\")");
    }
}