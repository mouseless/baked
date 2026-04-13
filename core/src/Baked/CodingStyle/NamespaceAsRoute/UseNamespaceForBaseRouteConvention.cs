using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.CodingStyle.NamespaceAsRoute;

public class UseNamespaceForBaseRouteConvention : IDomainModelConvention<TypeModelMetadataContext>
{
    public void Apply(TypeModelMetadataContext context)
    {
        if (!context.Type.TryGetNamespace(out var @namespace)) { return; }
        if (!context.Type.TryGet<ControllerModelAttribute>(out var controller)) { return; }

        var baseRoute = @namespace.Split(".");
        foreach (var action in controller.Actions)
        {
            action.RouteParts.InsertRange(0, baseRoute);
            foreach (var routeParameter in action.Parameters.Where(pm => pm.FromRoute))
            {
                routeParameter.RoutePosition += baseRoute.Length;
            }
        }
    }
}