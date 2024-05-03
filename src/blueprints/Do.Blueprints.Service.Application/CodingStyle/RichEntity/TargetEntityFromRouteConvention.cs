using Do.Business;
using Do.Domain.Model;
using Do.RestApi.Configuration;
using Do.RestApi.Model;
using Humanizer;

namespace Do.CodingStyle.RichEntity;

public class TargetEntityFromRouteConvention(DomainModel _domain)
    : IApiModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (context.Action.MethodModel is null) { return; }
        if (context.Action.MethodModel.Has<InitializerAttribute>()) { return; }
        if (context.Parameter.IsInvokeMethodParameter) { return; }

        var entityType = context.Parameter.TypeModel;
        if (!entityType.TryGetQueryContextType(_domain, out var queryContextType)) { return; }

        var queryContextParameter = context.Action.AddQueryContextAsService(queryContextType);

        context.Parameter.ConvertToId(name: "id");
        context.Parameter.From = ParameterModelFrom.Route;
        context.Parameter.RoutePosition = 1;
        context.Action.Route = $"{entityType.Name.Pluralize()}/{context.Action.Name}";
        context.Action.FindTargetStatement = queryContextParameter.BuildSingleBy(context.Parameter.Name, fromRoute: true);
    }
}