using Do.Business;
using Do.Orm;
using Do.RestApi.Configuration;
using Humanizer;

namespace Do.CodingStyle.RichEntity;

public class EntityInitializerIsPostResourceConvention : IApiModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (context.Parameter.IsInvokeMethodParameter) { return; }
        if (!context.Parameter.TypeModel.TryGetMetadata(out var metadata)) { return; }
        if (!metadata.Has<EntityAttribute>()) { return; }
        if (context.Action.MethodModel is null) { return; }
        if (!context.Action.MethodModel.Has<InitializerAttribute>()) { return; }

        context.Parameter.Name = "newTarget";
        context.Parameter.Type = $"Func<{context.Parameter.Type}>";

        context.Action.FindTargetStatement = "newTarget()";
        context.Action.Route = $"{context.Parameter.TypeModel.Name.Pluralize()}";
    }
}