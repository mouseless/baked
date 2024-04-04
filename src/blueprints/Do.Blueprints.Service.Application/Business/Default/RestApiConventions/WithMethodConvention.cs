using Do.Business.Attributes;
using Do.RestApi.Configuration;
using Humanizer;

namespace Do.Business.Default.RestApiConventions;

public class WithMethodConvention : IApiModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (context.Parameter.IsInvokeMethodParameter) { return; }
        if (!context.Parameter.TypeModel.TryGetMetadata(out var metadata) || !metadata.Has<TransientAttribute>()) { return; }
        if (context.Action.Id != "With") { return; }

        context.Parameter.Name = "newTarget";
        context.Parameter.Type = $"Func<{context.Parameter.Type}>";
        context.Action.FindTargetStatement = "newTarget()";
        context.Action.Route = $"{context.Parameter.TypeModel.Name.Pluralize()}/{context.Action.Name}";
    }
}
