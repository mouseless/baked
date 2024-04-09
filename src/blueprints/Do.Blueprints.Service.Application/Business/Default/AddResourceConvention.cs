using Do.RestApi.Configuration;
using Humanizer;

namespace Do.Business.Default;

public class AddResourceConvention : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (!context.Action.Id.StartsWith("Add")) { return; }

        context.Action.Route = context.Action.Route.Replace(context.Action.Id, context.Action.Id[3..].Pluralize());
    }
}