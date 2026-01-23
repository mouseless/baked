using Baked.Business;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using Humanizer;

namespace Baked.CodingStyle.TransientBinding;

public class TargetFromLocatorConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGet<ActionModelAttribute>(out var action)) { return; }
        if (context.Method.Has<InitializerAttribute>()) { return; }
        if (!context.Type.TryGetMembers(out var metadata)) { return; }
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
        action.FindTargetStatement = locatable.FindTargetTemplate(locatorServiceParameter, id);
        action.RouteParts = [context.Type.Name.Pluralize(), action.Name];
        if (locatable.IsAsync)
        {
            action.MakeAsync();
        }
    }
}