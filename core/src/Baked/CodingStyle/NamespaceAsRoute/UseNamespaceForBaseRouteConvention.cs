using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.CodingStyle.NamespaceAsRoute;

public class UseNamespaceForBaseRouteConvention : IDomainModelConvention<TypeModelMetadataContext>
{
    public void Apply(TypeModelMetadataContext context)
    {
        if (!context.Type.TryGetNamespace(out var @namespace) || string.IsNullOrWhiteSpace(@namespace.Value)) { return; }
        if (!context.Type.TryGet<ApiController>(out var controller)) { return; }

        var baseRoute = @namespace.Value.Split(".");
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