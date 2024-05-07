using Do.Business;
using Do.Domain.Model;
using Do.RestApi.Configuration;
using Humanizer;

namespace Do.CodingStyle.EntitySubclassViaComposition;

public class EntitySubclassInitializerIsPostResourceConvention(DomainModel _domain)
    : IApiModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (context.Parameter.IsInvokeMethodParameter) { return; }
        if (!context.Parameter.TypeModel.TryGetSubclassName(out var subclassName)) { return; }
        if (!context.Parameter.TypeModel.TryGetEntityTypeFromSubclass(_domain, out var entityType)) { return; }
        if (context.Action.MappedMethod is null) { return; }
        if (!context.Action.MappedMethod.Has<InitializerAttribute>()) { return; }

        context.Parameter.Name = "newTarget";
        context.Parameter.Type = $"Func<{context.Parameter.Type}>";

        context.Action.FindTargetStatement = "newTarget()";
        context.Action.RouteParts = [entityType.Name.Pluralize(), subclassName];
    }
}