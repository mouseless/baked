using Baked.Domain.Configuration;
using Baked.Orm;
using Baked.RestApi.Model;
using Humanizer;

namespace Baked.CodingStyle.RichEntity;

public class EntityUnderPluralGroupConvention : IDomainModelConvention<TypeModelContext>
{
    public void Apply(TypeModelContext context)
    {
        if (!context.Type.TryGetMetadata(out var metadata)) { return; }
        if (!metadata.Has<EntityAttribute>()) { return; }
        if (!metadata.TryGet<ControllerModelAttribute>(out var controller)) { return; }

        controller.GroupName = controller.GroupName.Pluralize();
    }
}