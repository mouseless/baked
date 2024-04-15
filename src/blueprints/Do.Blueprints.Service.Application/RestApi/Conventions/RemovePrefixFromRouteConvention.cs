using Do.RestApi.Configuration;

namespace Do.RestApi.Conventions;

public class RemovePrefixFromRouteConvention(IEnumerable<string> _prefixes)
    : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        var prefix = _prefixes.FirstOrDefault(prefix => context.Action.Name.StartsWith(prefix));
        if (prefix is null) { return; }

        var newName = context.Action.Name[prefix.Length..];
        context.Action.Route = context.Action.Route.Replace(context.Action.Name, newName);
        context.Action.Name = newName;
    }
}