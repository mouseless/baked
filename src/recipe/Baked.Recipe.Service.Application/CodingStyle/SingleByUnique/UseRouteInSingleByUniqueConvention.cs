using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using Humanizer;

namespace Baked.CodingStyle.SingleByUnique;

public class UseRouteInSingleByUniqueConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGet<SingleByUniqueAttribute>(out var unique)) { return; }
        if (!context.Method.TryGet<ActionModelAttribute>(out var action)) { return; }
        if (!action.Parameter.TryGetValue(unique.PropertyName.Camelize(), out var uniqueParameter)) { return; }

        uniqueParameter.From = ParameterModelFrom.Route;
        uniqueParameter.RoutePosition = 1;
        action.RouteParts = action.RouteParts.RemoveAll(action.Name);
        action.Name = string.Empty;

        if (action.Parameter.TryGetValue("throwNotFound", out var throwNotFoundParameter))
        {
            throwNotFoundParameter.IsHardCoded = true;
            throwNotFoundParameter.LookupRenderer = _ => "true";
        }
    }
}