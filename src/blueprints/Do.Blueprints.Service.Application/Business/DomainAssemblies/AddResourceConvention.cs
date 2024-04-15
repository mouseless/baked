using Do.RestApi.Configuration;
using Humanizer;

namespace Do.Business.DomainAssemblies;

public class AddResourceConvention : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (!context.Action.Name.StartsWith("Add")) { return; }

        var newName = context.Action.Name[3..].Pluralize();
        context.Action.Route = context.Action.Route.Replace(context.Action.Name, newName);
        context.Action.Name = newName;
    }
}