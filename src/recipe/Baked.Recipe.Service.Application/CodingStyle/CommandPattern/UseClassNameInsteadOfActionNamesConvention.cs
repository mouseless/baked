using Baked.RestApi.Configuration;

namespace Baked.CodingStyle.CommandPattern;

public class UseClassNameInsteadOfActionNamesConvention(IEnumerable<string> actionNames)
    : IApiModelConvention<ActionModelContext>
{
    readonly HashSet<string> _actionNames = actionNames.ToHashSet();

    public void Apply(ActionModelContext context)
    {
        if (!_actionNames.Contains(context.Action.Name)) { return; }

        context.Action.RouteParts.RemoveAll(context.Action.Name);
        context.Action.Name = context.Controller.MappedType.Name;
    }
}