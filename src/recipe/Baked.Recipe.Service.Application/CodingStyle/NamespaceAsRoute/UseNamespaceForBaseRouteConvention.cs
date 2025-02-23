using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.CodingStyle.NamespaceAsRoute;

public class UseNamespaceForBaseRouteConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Type.TryGetNamespace(out var @namespace)) { return; }
        if (!context.Method.TryGetSingle<ActionModelAttribute>(out var action)) { return; }

        var baseRoute = @namespace.Split(".");

        action.RouteParts.InsertRange(0, baseRoute);
        foreach (var routeParameter in action.Parameters.Where(pm => pm.FromRoute))
        {
            routeParameter.RoutePosition += baseRoute.Length;
        }
    }
}