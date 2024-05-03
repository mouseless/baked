using Do.Business;
using Do.RestApi.Configuration;
using Humanizer;

namespace Do.CodingStyle.CommandPattern;

public class UseRootPathAsGroupNameForSingleMethodNonLocatables : IApiModelConvention<ControllerModelContext>
{
    public void Apply(ControllerModelContext context)
    {
        if (!context.Controller.TypeModel.TryGetMetadata(out var metadata)) { return; }
        if (metadata.Has<LocatableAttribute>()) { return; }
        if (context.Controller.Action.Count != 1) { return; }

        var theOnlyAction = context.Controller.Actions.Single();
        var rootPath = theOnlyAction.Route.Split('/').First();
        context.Controller.GroupName = rootPath.Pascalize();
    }
}