using Baked.Business;
using Baked.Domain.Model;
using Baked.Orm;
using Baked.RestApi.Configuration;
using Baked.RestApi.Model;
using Humanizer;

namespace Baked.CodingStyle.RichEntity;

public class AddIdParameterToRouteConvention(DomainModel _domain)
    : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (context.Action.MappedMethod is null) { return; }
        if (context.Action.MappedMethod.Has<InitializerAttribute>()) { return; }
        if (context.Controller.MappedType is null) { return; }
        if (!context.Controller.MappedType.TryGetMetadata(out var metadata)) { return; }
        if (!metadata.Has<EntityAttribute>()) { return; }

        var entityType = context.Controller.MappedType;
        if (!entityType.TryGetQueryContextType(_domain, out var queryContextType)) { return; }

        var idProperty = entityType.GetMembers().Properties["Id"];

        context.Action.Parameter["target"] =
            new(context.Controller.MappedType, ParameterModelFrom.Route, "id")
            {
                IsOptional = false,
                IsInvokeMethodParameter = false,
                RoutePosition = 1,
                AdditionalAttributes = [$"SwaggerSchema(\"Unique value to find {context.Controller.MappedType.Name.Humanize().ToLowerInvariant()} resource\")"],
                Type = idProperty.PropertyType.CSharpFriendlyFullName
            };
    }
}