using Baked.Business;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using Humanizer;

namespace Baked.CodingStyle.EntityExtensionViaComposition;

public class TargetEntityExtensionFromRouteConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGetSingle<ActionModelAttribute>(out var action)) { return; }
        if (context.Method.Has<InitializerAttribute>()) { return; }
        if (!action.Parameter.TryGetValue("target", out var parameter)) { return; }

        var entityExtensionType = context.Type;
        if (!entityExtensionType.TryGetEntityTypeFromExtension(context.Domain, out var entityType)) { return; }
        if (!entityType.TryGetQueryContextType(context.Domain, out var queryContextType)) { return; }

        var queryContextParameter = action.AddQueryContextAsService(queryContextType);

        parameter.ConvertToId(name: "id", dontAddRequired: true);
        parameter.From = ParameterModelFrom.Route;
        parameter.RoutePosition = 1;
        action.RouteParts = [entityType.Name.Pluralize(), action.Name];
        action.FindTargetStatement = queryContextParameter.BuildSingleBy(parameter.Name, fromRoute: true, castTo: entityExtensionType);
    }
}