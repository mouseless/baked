using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Orm;
using Baked.RestApi.Model;
using Humanizer;

namespace Baked.CodingStyle.LocatableTransient;

public class FindTargetFromLocatorConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGet<ActionModelAttribute>(out var action)) { return; }
        if (context.Method.Has<InitializerAttribute>()) { return; }
        if (!context.Type.TryGetMembers(out var metadata)) { return; }
        // TODO remove after configuring rich transient
        if (!metadata.Has<EntityAttribute>()) { return; }
        if (!metadata.TryGet<LocatableAttribute>(out var locatable)) { return; }
        if (!metadata.TryGetIdInfo(out var idInfo)) { return; }

        var id = action.Parameter[ParameterModelAttribute.TargetParameterName] =
            new(idInfo.PropertyName.Camelize(), idInfo.Type, ParameterModelFrom.Route)
            {
                IsInvokeMethodParameter = false,
                RoutePosition = 1
            };
        id.AdditionalAttributes.Add($"SwaggerSchema(\"Unique value to find {context.Type.Name.Humanize().ToLowerInvariant()} resource\")");

        var locatorServiceParameter = locatable.AddLocatorService(action);
        action.FindTargetStatement = locatable.LocateTargetTemplate(locatorServiceParameter, id);
    }
}
