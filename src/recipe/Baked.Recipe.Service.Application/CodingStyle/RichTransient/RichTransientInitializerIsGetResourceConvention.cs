using Baked.Business;
using Baked.Domain.Model;
using Baked.RestApi.Configuration;
using Humanizer;

namespace Baked.CodingStyle.RichTransient;

public class RichTransientInitializerIsGetResourceConvention(DomainModel _domain)
    : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (!context.Controller.MappedType.TryGetMetadata(out var metadata)) { return; }
        if (!metadata.Has<LocatableAttribute>()) { return; }
        if (context.Controller.MappedType.TryGetQueryType(_domain, out var _)) { return; }
        if (!metadata.Has<HasPublicDataAttribute>()) { return; }
        if (context.Action.MappedMethod is null) { return; }
        if (!context.Action.MappedMethod.Has<InitializerAttribute>()) { return; }
        if (!context.Action.Parameter.TryGetValue("id", out var parameter)) { return; }

        parameter.IsInvokeMethodParameter = true;
        parameter.From = RestApi.Model.ParameterModelFrom.Route;
        parameter.RoutePosition = 1;

        context.Action.Method = HttpMethod.Get;
        context.Action.RouteParts = [context.Controller.MappedType.Name.Pluralize()];
        context.Action.FindTargetStatement = "newTarget()";
    }
}