using Baked.Business;
using Baked.RestApi.Configuration;
using Humanizer;

namespace Baked.CodingStyle.RichTransient;

public class RichTransientInitializerIsGetResourceConvention : IApiModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (!context.Controller.MappedType.TryGetMetadata(out var metadata)) { return; }
        if (!metadata.Has<RichTransientAttribute>()) { return; }
        if (context.Action.MappedMethod is null) { return; }
        if (!context.Action.MappedMethod.Has<InitializerAttribute>()) { return; }
        if (context.Parameter.FromServices) { return; }

        context.Parameter.IsInvokeMethodParameter = true;
        context.Parameter.From = RestApi.Model.ParameterModelFrom.Route;
        context.Parameter.RoutePosition = 1;

        context.Action.Method = HttpMethod.Get;
        context.Action.RouteParts = [context.Controller.MappedType.Name.Pluralize()];
        context.Action.FindTargetStatement = "newTarget()";
    }
}