using Baked.RestApi.Configuration;

namespace Baked.CodingStyle.CommandPattern;

public class IncludeClassDocsForActionNamesConvention(IEnumerable<string> actionNames)
    : IApiModelConvention<ActionModelContext>
{
    readonly HashSet<string> _actionNames = actionNames.ToHashSet();

    public void Apply(ActionModelContext context)
    {
        if (!_actionNames.Contains(context.Action.Name)) { return; }
        if (!context.Controller.MappedType.TryGetMembers(out var controllerMembers)) { return; }
        if (controllerMembers.Documentation is null) { return; }

        var summary = controllerMembers.Documentation.GetSummary();
        var remarks = controllerMembers.Documentation.GetRemarks();
        if (summary is null && remarks is null) { return; }

        context.Action.AdditionalAttributes.Add($"SwaggerOperation(Summary = \"{summary.EscapeNewLines()}\", Description = \"{remarks.EscapeNewLines()}\")");
    }
}