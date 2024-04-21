using Do.RestApi.Configuration;
using Humanizer;

namespace Do.RestApi.Conventions;

public class RemovePrefixFromRouteConvention(IEnumerable<string> _prefixes,
    bool _pluralize = false
) : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        var prefix = _prefixes.FirstOrDefault(prefix => context.Action.Name.StartsWith(prefix));
        if (prefix is null) { return; }

        var newName = context.Action.Name[prefix.Length..];
        if (_pluralize)
        {
            newName = newName.Pluralize();
        }

        context.Action.Route = context.Action.Route.Replace(context.Action.Name, newName);
        context.Action.Name = newName;
    }
}