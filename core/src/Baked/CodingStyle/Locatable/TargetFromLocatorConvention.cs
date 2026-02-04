using Baked.Business;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using Humanizer;

namespace Baked.CodingStyle.Locatable;

public class TargetFromLocatorConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGet<ActionModelAttribute>(out var action)) { return; }
        if (context.Method.Has<InitializerAttribute>()) { return; }
        if (!context.Type.TryGetMembers(out var metadata)) { return; }
        if (!metadata.TryGet<LocatableAttribute>(out var locatable)) { return; }
        if (!metadata.TryGetIdInfo(out var idInfo)) { return; }

        var id = action.Parameter[ParameterModelAttribute.TargetParameterName];
        var throwNotFound = action.Parameter["throwNotFound"] =
            new("throwNotFound", context.Domain.Types[typeof(bool)].CSharpFriendlyFullName, ParameterModelFrom.Query)
            {
                IsHardCoded = true,
                LookupRenderer = _ => "true",
                IsInvokeMethodParameter = false
            };

        var locatorServiceParameter = locatable.AddLocatorAsService(action, context.Type);
        action.FindTargetStatement = locatable.RenderLocate(locatorServiceParameter.Name, id.RenderLookup(id.Name), throwNotFound.RenderLookup(throwNotFound.Name));
        action.RouteParts = [context.Type.Name.Pluralize(), action.Name];
        if (locatable.IsAsync)
        {
            action.MakeAsync();
        }
    }
}