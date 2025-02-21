using Humanizer;

namespace Baked.CodingStyle.SingleByUnique;

public class UseRouteInSingleByUniqueConvention : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (context.Action.MappedMethod is null) { return; }
        if (!context.Action.MappedMethod.TryGetSingle<SingleByUniqueAttribute>(out var unique)) { return; }
        if (!context.Action.Parameter.TryGetValue(unique.PropertyName.Camelize(), out var uniqueParameter)) { return; }

        uniqueParameter.From = ParameterModelFrom.Route;
        uniqueParameter.RoutePosition = 1;
        context.Action.RouteParts = context.Action.RouteParts.RemoveAll(context.Action.Name);
        context.Action.Name = string.Empty;

        if (context.Action.Parameter.TryGetValue("throwNotFound", out var throwNotFoundParameter))
        {
            throwNotFoundParameter.IsHardCoded = true;
            throwNotFoundParameter.LookupRenderer = _ => "true";
        }
    }
}