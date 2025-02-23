using Baked.Business;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using Humanizer;

namespace Baked.CodingStyle.RichTransient;

public class AddIdParameterToRouteConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGetSingle<ActionModelAttribute>(out var action)) { return; }
        if (!context.Type.TryGetMembers(out var members)) { return; }
        if (!members.Methods.Having<InitializerAttribute>().Any()) { return; }
        if (!members.Has<LocatableAttribute>()) { return; }
        if (context.Method.Has<InitializerAttribute>()) { return; }

        var initializer = members.Methods.Having<InitializerAttribute>().Single();
        if (!initializer.DefaultOverload.Parameters.TryGetValue("id", out var parameter)) { return; }

        action.Parameter["id"] =
            new("id", parameter.ParameterType.CSharpFriendlyFullName, ParameterModelFrom.Route)
            {
                IsOptional = parameter.IsOptional,
                DefaultValue = parameter.DefaultValue,
                IsInvokeMethodParameter = false,
                RoutePosition = 1
            };
        action.Parameter["id"].AdditionalAttributes.Add($"SwaggerSchema(\"Unique value to find {context.Type.Name.Humanize().ToLowerInvariant()} resource\")");
        action.RouteParts = [context.Type.Name.Pluralize(), action.Name];
    }
}