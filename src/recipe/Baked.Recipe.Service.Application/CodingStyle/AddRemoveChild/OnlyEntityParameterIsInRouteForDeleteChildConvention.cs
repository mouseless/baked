using Baked.RestApi.Configuration;
using Baked.RestApi.Model;

namespace Baked.CodingStyle.AddRemoveChild;

public class OnlyEntityParameterIsInRouteForDeleteChildConvention : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (context.Action.Method != HttpMethod.Delete) { return; }
        if (context.Action.Name == string.Empty) { return; }
        if (context.Action.InvokedMethodParameters.Count() != 1) { return; }

        var onlyParameter = context.Action.InvokedMethodParameters.Single();
        if (onlyParameter.MappedParameter is null) { return; }
        if (!onlyParameter.MappedParameter.ParameterType.TryGetEntityAttribute(out var _)) { return; }

        onlyParameter.From = ParameterModelFrom.Route;
        onlyParameter.RoutePosition = 3;
    }
}