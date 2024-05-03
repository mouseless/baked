using Do.RestApi.Configuration;
using Humanizer;

namespace Do.CodingStyle.AddRemoveChild;

public class PluralizeActionForDeleteChildConvention : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (context.Action.Method != HttpMethod.Delete) { return; }
        if (context.Action.Name == string.Empty) { return; }
        if (!context.Action.InvokedMethodParameters.Any()) { return; }

        var newName = context.Action.Name.Pluralize();
        context.Action.Route = context.Action.Route.Replace(context.Action.Name, newName);
        context.Action.Name = newName;
    }
}