using Do.RestApi.Configuration;

namespace Do.CodingStyle.CommandPattern;

public class UseClassNameInsteadOfActionNames(IEnumerable<string> actionNames)
    : IApiModelConvention<ActionModelContext>
{
    readonly HashSet<string> _actionNames = actionNames.ToHashSet();

    public void Apply(ActionModelContext context)
    {
        if (!_actionNames.Contains(context.Action.Name)) { return; }

        context.Action.Name = context.Controller.ClassName;
    }
}