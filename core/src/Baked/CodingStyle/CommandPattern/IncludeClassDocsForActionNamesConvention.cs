using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.CodingStyle.CommandPattern;

public class IncludeClassDocsForActionNamesConvention(
    Func<MethodModelContext, bool>? _whenContext = default
) : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.Has<CommandMethodAttribute>()) { return; }
        if (!context.Method.TryGet<ActionModelAttribute>(out var action)) { return; }
        if (_whenContext is not null && !_whenContext(context)) { return; }
        if (context.Type.Documentation is null) { return; }

        var summary = context.Type.Documentation.Summary;
        var remarks = context.Type.Documentation.Remarks;
        if (summary is null && remarks is null) { return; }

        action.AdditionalAttributes.Add($"SwaggerOperation(Summary = \"{summary.EscapeNewLines()}\", Description = \"{remarks.EscapeNewLines()}\")");
    }
}