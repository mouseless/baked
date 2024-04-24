using Do.RestApi.Configuration;

namespace Do.CodingStyle.CommandPattern;

public class RemoveTheOnlyActionNameFromRouteForSingleMethodInitializables : IApiModelConvention<ControllerModelContext>
{
    public void Apply(ControllerModelContext context)
    {
        if (!context.Controller.TypeModel.TryGetMetadata(out var metadata)) { return; }
        if (!metadata.Has<PubliclyInitializableAttribute>()) { return; }
        if (context.Controller.Action.Count > 1) { return; }

        var theOnlyAction = context.Controller.Actions.Single();

        theOnlyAction.Route = theOnlyAction.Route.Replace($"/{theOnlyAction.Name}", string.Empty);
        theOnlyAction.Name = string.Empty;
    }
}