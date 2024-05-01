using Do.RestApi.Configuration;
using Do.RestApi.Model;
using Humanizer;

namespace Do.CodingStyle.SingleByUnique;

public class UseRouteInSingleByUniqueConvention : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (context.Action.MethodModel is null) { return; }
        if (!context.Action.MethodModel.TryGetSingle<SingleByUniqueAttribute>(out var unique)) { return; }
        if (!context.Action.Parameter.TryGetValue(unique.PropertyName.Camelize(), out var uniqueParameter)) { return; }

        uniqueParameter.From = ParameterModelFrom.Route;
        context.Action.Route = context.Action.Route.Replace($"/{context.Action.Name}", $"/{uniqueParameter.GetRouteString()}");

        if (context.Action.Parameter.TryGetValue("throwNotFound", out var throwNotFoundParameter))
        {
            throwNotFoundParameter.IsHardCoded = true;
            throwNotFoundParameter.LookupRenderer = _ => "true";
        }
    }
}