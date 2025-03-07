using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.CodingStyle.CommandPattern;

public class UseClassNameInsteadOfActionNamesConvention(IEnumerable<string> actionNames)
    : IDomainModelConvention<MethodModelContext>
{
    readonly HashSet<string> _actionNames = [.. actionNames];

    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGetSingle<ActionModelAttribute>(out var action)) { return; }
        if (!_actionNames.Contains(action.Name)) { return; }

        action.RouteParts.RemoveAll(action.Name);
        action.Name = context.Type.Name;
    }
}