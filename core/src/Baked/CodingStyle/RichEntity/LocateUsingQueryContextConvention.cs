using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Orm;
using Humanizer;

namespace Baked.CodingStyle.RichEntity;

public class LocateUsingQueryContextConvention : IDomainModelConvention<TypeModelContext>
{
    public void Apply(TypeModelContext context)
    {
        if (!context.Type.TryGetMetadata(out var metadata)) { return; }
        if (!metadata.Has<EntityAttribute>()) { return; }
        if (!metadata.TryGet<LocatableAttribute>(out var locatable)) { return; }

        var entityType = context.Type;
        if (!entityType.TryGetQueryContextType(context.Domain, out var queryContextType)) { return; }
        if (!entityType.TryGetIdInfo(out var idInfo)) { return; }

        queryContextType.Apply(t => locatable.ServiceType = t);
        locatable.LocateSingleMethodName = $"SingleBy{idInfo.PropertyName}";
        locatable.LocateMultipleMethodName = $"By{idInfo.PropertyName.Pluralize()}";
    }
}