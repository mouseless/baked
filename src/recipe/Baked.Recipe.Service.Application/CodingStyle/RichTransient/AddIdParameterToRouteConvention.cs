using Baked.Business;
using Humanizer;

namespace Baked.CodingStyle.RichTransient;

public class AddIdParameterToRouteConvention : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (!context.Controller.MappedType.TryGetMembers(out var members)) { return; }
        if (!members.Methods.Having<InitializerAttribute>().Any()) { return; }
        if (!members.Has<LocatableAttribute>()) { return; }
        if (context.Action.MappedMethod is null) { return; }
        if (context.Action.MappedMethod.Has<InitializerAttribute>()) { return; }

        var initializer = members.Methods.Having<InitializerAttribute>().Single();
        if (!initializer.DefaultOverload.Parameters.TryGetValue("id", out var parameter)) { return; }

        context.Action.Parameter["id"] =
            new(parameter.ParameterType, ParameterModelFrom.Route, parameter.Name, MappedParameter: parameter)
            {
                IsOptional = parameter.IsOptional,
                DefaultValue = parameter.DefaultValue,
                IsInvokeMethodParameter = false,
                RoutePosition = 1,
                AdditionalAttributes = [$"SwaggerSchema(\"Unique value to find {context.Controller.MappedType.Name.Humanize().ToLowerInvariant()} resource\")"]
            };
        context.Action.RouteParts = [context.Controller.MappedType.Name.Pluralize(), context.Action.Name];
    }
}