using Baked.Business;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using Humanizer;

namespace Baked.CodingStyle.EntityExtensionViaComposition;

public class TargetEntityExtensionFromRouteConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGet<ActionModelAttribute>(out var action)) { return; }
        if (context.Method.Has<InitializerAttribute>()) { return; }
        if (!action.Parameter.TryGetValue(ParameterModelAttribute.TargetParameterName, out var parameter)) { return; }

        var entityExtensionType = context.Type;
        if (!entityExtensionType.TryGetEntityTypeFromExtension(context.Domain, out var entityType)) { return; }
        if (!entityType.TryGetQueryContextType(context.Domain, out var queryContextType)) { return; }

        var queryContextParameter = action.AddQueryContextAsService(queryContextType);

        if (!entityType.TryGetIdInfo(out var identifier)) { return; }

        parameter.ConvertToId(identifier.Type, identifier.Name, name: identifier.RouteName, dontAddRequired: true);

        parameter.From = ParameterModelFrom.Route;
        parameter.RoutePosition = 1;
        action.RouteParts = [entityType.Name.Pluralize(), action.Name];
        action.FindTargetStatement = queryContextParameter.BuildSingleBy(parameter.Name, identifier.Name,
            fromRoute: true,
            castTo: entityExtensionType
        );
    }
}