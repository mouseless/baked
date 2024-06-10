using Do.Business;
using Do.RestApi.Configuration;
using Humanizer;

namespace Do.CodingStyle.CommandPattern;

public class UseRootPathAsGroupNameForSingleMethodNonLocatablesConvention : IApiModelConvention<ControllerModelContext>
{
    public void Apply(ControllerModelContext context)
    {
        if (!context.Controller.MappedType.TryGetMetadata(out var metadata)) { return; }
        if (metadata.Has<LocatableAttribute>()) { return; }
        if (context.Controller.Action.Count != 1) { return; }

        var theOnlyAction = context.Controller.Actions.Single();
        var rootPath = theOnlyAction.RouteParts.First();
        context.Controller.GroupName = rootPath.Pascalize();
    }
}