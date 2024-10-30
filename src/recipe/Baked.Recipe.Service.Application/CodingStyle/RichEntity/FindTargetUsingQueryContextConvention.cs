using Baked.Business;
using Baked.Domain.Model;
using Baked.Orm;
using Baked.RestApi.Configuration;
using Humanizer;

namespace Baked.CodingStyle.RichEntity;

public class FindTargetUsingQueryContextConvention(DomainModel _domain)
    : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (context.Action.MappedMethod is null) { return; }
        if (context.Action.MappedMethod.Has<InitializerAttribute>()) { return; }
        if (context.Controller.MappedType is null) { return; }
        if (!context.Controller.MappedType.TryGetMetadata(out var metadata)) { return; }
        if (!metadata.Has<EntityAttribute>()) { return; }

        var entityType = context.Controller.MappedType;
        if (!entityType.TryGetQueryContextType(_domain, out var queryContextType)) { return; }

        var queryContextParameter = context.Action.AddQueryContextAsService(queryContextType);

        context.Action.RouteParts = [entityType.Name.Pluralize(), context.Action.Name];
        context.Action.FindTargetStatement = queryContextParameter.BuildSingleBy(context.Action.Parameter["target"].Name, fromRoute: true);
    }
}
