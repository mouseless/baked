using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using Humanizer;

namespace Baked.CodingStyle.EntityExtensionViaComposition;

public class EntityExtensionsUnderEntitiesConvention : IDomainModelConvention<TypeModelContext>
{
    public void Apply(TypeModelContext context)
    {
        if (!context.Type.TryGetMetadata(out var metadata)) { return; }
        if (!metadata.TryGetSingle<ControllerModelAttribute>(out var controller)) { return; }
        if (!context.Type.TryGetEntityTypeFromExtension(context.Domain, out var entityType)) { return; }

        controller.GroupName = entityType.Name.Pluralize();
    }
}