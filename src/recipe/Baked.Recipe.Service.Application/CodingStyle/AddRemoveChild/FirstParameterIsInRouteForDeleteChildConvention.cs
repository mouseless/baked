using Baked.RestApi.Configuration;
using Baked.RestApi.Model;

namespace Baked.CodingStyle.AddRemoveChild;

public class FirstParameterIsInRouteForDeleteChildConvention : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (context.Action.Method != HttpMethod.Delete) { return; }
        if (context.Action.Name == string.Empty) { return; }
        if (!context.Action.InvokedMethodParameters.Any()) { return; }

        var firstParameter = context.Action.InvokedMethodParameters.First();
        firstParameter.From = ParameterModelFrom.Route;
        firstParameter.RoutePosition = 3;
    }
}