using Baked.Business;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using Humanizer;

namespace Baked.CodingStyle.CommandViaMethodName;

public class UseRootPathAsGroupNameForSingleMethodNonLocatablesConvention(
    Func<TypeModelContext, bool>? _whenContext = default
) : IDomainModelConvention<TypeModelContext>
{
    public void Apply(TypeModelContext context)
    {
        if (_whenContext is not null && !_whenContext(context)) { return; }
        if (!context.Type.TryGetMetadata(out var metadata)) { return; }
        if (metadata.Has<Locatable>()) { return; }
        if (!metadata.TryGet<ApiController>(out var controller)) { return; }
        if (controller.Action.Count != 1) { return; }

        var theOnlyAction = controller.Actions.Single();
        var rootPath = theOnlyAction.RouteParts.First();
        controller.GroupName = rootPath.Pascalize();
    }
}