using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.CodingStyle.CommandPattern;

public class IncludeClassDocsForActionNamesConvention(IEnumerable<string> actionNames)
    : IDomainModelConvention<MethodModelContext>
{
    readonly HashSet<string> _actionNames = [.. actionNames];

    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGetSingle<ActionModel>(out var action)) { return; }
        if (!_actionNames.Contains(action.Name)) { return; }
        if (context.Type.Documentation is null) { return; }

        var summary = context.Type.Documentation.GetSummary();
        var remarks = context.Type.Documentation.GetRemarks();
        if (summary is null && remarks is null) { return; }

        action.AdditionalAttributes.Add($"SwaggerOperation(Summary = \"{summary.EscapeNewLines()}\", Description = \"{remarks.EscapeNewLines()}\")");
    }
}