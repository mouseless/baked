using Baked.Business;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using Humanizer;

namespace Baked.CodingStyle.RichTransient;

public class RichTransientInitializerIsGetResourceConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGet<ActionModelAttribute>(out var action)) { return; }
        if (!context.Type.Has<RichTransientAttribute>()) { return; }
        if (!context.Method.Has<InitializerAttribute>()) { return; }
        if (!context.Type.TryGetIdInfo(out var idInfo)) { return; }
        if (!action.Parameter.TryGetValue(idInfo.RouteName, out var parameter)) { return; }

        parameter.From = ParameterModelFrom.Route;
        parameter.RoutePosition = 1;
        parameter.IsInvokeMethodParameter = true;

        action.Method = HttpMethod.Get;
        action.RouteParts = [context.Type.Name.Pluralize()];
    }
}