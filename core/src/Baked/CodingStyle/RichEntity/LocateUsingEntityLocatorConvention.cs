using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Orm;

namespace Baked.CodingStyle.RichEntity;

public class LocateUsingEntityLocatorConvention : IDomainModelConvention<TypeModelContext>
{
    public void Apply(TypeModelContext context)
    {
        if (!context.Type.TryGetMetadata(out var metadata)) { return; }
        if (!metadata.Has<EntityAttribute>()) { return; }
        if (!metadata.TryGet<LocatableAttribute>(out var locatable)) { return; }

        var entityType = context.Type;
        if (!entityType.TryGetQueryContextType(context.Domain, out var queryContextType)) { return; }
        if (!entityType.TryGetIdInfo(out var idInfo)) { return; }

        entityType.Apply(t => locatable.ServiceType = typeof(ILocator<>).MakeGenericType(t));
        locatable.LocateSingleMethodName = "Single";
        locatable.LocateMultipleMethodName = "Multiple";
    }
}