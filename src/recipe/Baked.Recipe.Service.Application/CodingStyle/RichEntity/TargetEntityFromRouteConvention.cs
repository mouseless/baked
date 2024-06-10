using Baked.Business;
using Baked.Domain.Model;
using Baked.RestApi.Configuration;
using Baked.RestApi.Model;
using Humanizer;

namespace Baked.CodingStyle.RichEntity;

public class TargetEntityFromRouteConvention(DomainModel _domain)
    : IApiModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (context.Action.MappedMethod is null) { return; }
        if (context.Action.MappedMethod.Has<InitializerAttribute>()) { return; }
        if (context.Parameter.IsInvokeMethodParameter) { return; }

        var entityType = context.Parameter.TypeModel;
        if (!entityType.TryGetQueryContextType(_domain, out var queryContextType)) { return; }

        var queryContextParameter = context.Action.AddQueryContextAsService(queryContextType);

        context.Parameter.ConvertToId(name: "id", dontAddRequired: true);
        context.Parameter.From = ParameterModelFrom.Route;
        context.Parameter.RoutePosition = 1;
        context.Action.RouteParts = [entityType.Name.Pluralize(), context.Action.Name];
        context.Action.FindTargetStatement = queryContextParameter.BuildSingleBy(context.Parameter.Name, fromRoute: true);
    }
}