using Baked.Business;
using Baked.Orm;
using Baked.RestApi.Configuration;
using Baked.RestApi.Model;

namespace Baked.CodingStyle.RichTransient;

public class TargetRichTransientFromRouteConvention
    : IApiModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (!context.Controller.MappedType.TryGetMetadata(out var metadata)) { return; }
        if (metadata.Has<EntityAttribute>()) { return; }
        if (!metadata.Has<LocatableAttribute>()) { return; }
        if (!context.Controller.MappedType.TryGetMembers(out var members)) { return; }
        if (context.Action.MappedMethod is null) { return; }
        if (context.Action.MappedMethod.Has<InitializerAttribute>()) { return; }
        if (context.Parameter.Name != "id") { return; }

        var initializer = members.Methods.Having<InitializerAttribute>().Single();

        foreach (var parameter in initializer.DefaultOverload.Parameters)
        {
            context.Action.Parameter[parameter.Name] =
                new(parameter.ParameterType, ParameterModelFrom.Route, parameter.Name, MappedParameter: parameter)
                {
                    IsOptional = parameter.IsOptional,
                    DefaultValue = parameter.DefaultValue,
                    IsInvokeMethodParameter = false,
                    RoutePosition = 1
                };
        }

        context.Action.FindTargetStatement = $"newTarget().{initializer.Name}({initializer.DefaultOverload.Parameters.Select(p => $"@{p.Name}").Join(", ")})";
    }
}