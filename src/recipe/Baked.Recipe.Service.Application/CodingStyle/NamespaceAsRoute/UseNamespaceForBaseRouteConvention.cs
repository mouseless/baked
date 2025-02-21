namespace Baked.CodingStyle.NamespaceAsRoute;

public class UseNamespaceForBaseRouteConvention
  : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (!context.Controller.MappedType.TryGetNamespace(out var @namespace)) { return; }

        var baseRoute = @namespace.Split(".");

        context.Action.RouteParts.InsertRange(0, baseRoute);
        foreach (var routeParameter in context.Action.Parameters.Where(pm => pm.FromRoute))
        {
            routeParameter.RoutePosition += baseRoute.Length;
        }
    }
}