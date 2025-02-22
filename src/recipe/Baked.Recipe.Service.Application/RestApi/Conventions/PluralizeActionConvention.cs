using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using Humanizer;

namespace Baked.RestApi.Conventions;

public class PluralizeActionConvention(
    Func<ActionModel, bool>? _when = default
) : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGetSingle<ActionModel>(out var action)) { return; }
        if (_when is not null && !_when(action)) { return; }

        var newName = action.Name.Pluralize();
        action.RouteParts = action.RouteParts.Replace(action.Name, newName);
        action.Name = newName;
    }
}