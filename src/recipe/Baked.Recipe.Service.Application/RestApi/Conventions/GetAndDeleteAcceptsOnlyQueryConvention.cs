namespace Baked.RestApi.Conventions;

public class GetAndDeleteAcceptsOnlyQueryConvention : IApiModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (context.Action.Method != HttpMethod.Get && context.Action.Method != HttpMethod.Delete) { return; }
        if (context.Parameter.FromServices || context.Parameter.FromRoute) { return; }

        context.Parameter.From = ParameterModelFrom.Query;
    }
}