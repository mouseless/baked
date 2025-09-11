using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using Humanizer;

namespace Baked.CodingStyle.EntitySubclassViaComposition;

public class EntitySubclassUnderEntitiesConvention
    : IDomainModelConvention<TypeModelContext>
{
    public void Apply(TypeModelContext context)
    {
        if (!context.Type.TryGetMetadata(out var metadata)) { return; }
        if (!metadata.TryGet<ControllerModelAttribute>(out var controller)) { return; }
        if (!context.Type.TryGetEntityTypeFromSubclass(context.Domain, out var entityType)) { return; }

        controller.GroupName = entityType.Name.Pluralize();
    }
}