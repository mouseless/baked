using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Orm;

namespace Baked.CodingStyle.RichEntity;

public class FindTargetUsingQueryContextConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (context.Method.Has<InitializerAttribute>()) { return; }
        if (!context.Type.TryGetMetadata(out var metadata)) { return; }
        if (!metadata.Has<EntityAttribute>()) { return; }
        if (!metadata.TryGet<LocatableAttribute>(out var locator)) { return; }

        var entityType = context.Type;
        if (!entityType.TryGetQueryContextType(context.Domain, out var queryContextType)) { return; }
        if (!entityType.TryGetIdInfo(out var idInfo)) { return; }

        locator.AddLocatorService = (action) => action.AddQueryContextAsService(queryContextType);
        locator.LocateTargetTemplate = (queryContextParameter, parameter) => queryContextParameter.BuildSingleBy(parameter.Name, "Id", fromRoute: true);
    }
}