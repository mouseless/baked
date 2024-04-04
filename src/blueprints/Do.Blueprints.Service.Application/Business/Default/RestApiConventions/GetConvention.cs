using Do.RestApi.Configuration;

namespace Do.Business.Default.RestApiConventions;

public class GetConvention : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (!context.Action.Id.StartsWith("Get")) { return; }

        context.Action.Route = context.Action.Route.Replace(context.Action.Id, context.Action.Id[3..]);
    }
}
