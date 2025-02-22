using Baked.Business;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using Humanizer;

namespace Baked.CodingStyle.EntityExtensionViaComposition;

public class TargetEntityExtensionFromRouteConvention : IDomainModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (!context.Method.TryGetSingle<ActionModel>(out var action)) { return; }
        if (context.Method.Has<InitializerAttribute>()) { return; }
        if (!context.Parameter.TryGetSingle<ParameterModel>(out var parameter)) { return; }
        if (!parameter.IsTarget()) { return; }

        var entityExtensionType = context.Parameter.ParameterType;
        if (!entityExtensionType.TryGetEntityTypeFromExtension(context.Domain, out var entityType)) { return; }
        if (!entityType.TryGetQueryContextType(context.Domain, out var queryContextType)) { return; }

        var queryContextParameter = action.AddQueryContextAsService(queryContextType);

        parameter.ConvertToId(name: "id", dontAddRequired: true);
        parameter.From = ParameterModelFrom.Route;
        parameter.RoutePosition = 1;
        action.RouteParts = [entityType.Name.Pluralize(), action.Name];
        action.FindTargetStatement = queryContextParameter.BuildSingleBy(context.Parameter.Name, fromRoute: true, castTo: entityExtensionType);
    }
}