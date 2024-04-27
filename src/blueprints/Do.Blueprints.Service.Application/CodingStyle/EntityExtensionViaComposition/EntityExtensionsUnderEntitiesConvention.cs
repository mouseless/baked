using Do.Domain.Model;
using Do.Orm;
using Do.RestApi.Configuration;
using Humanizer;

namespace Do.CodingStyle.EntityExtensionViaComposition;

public class EntityExtensionsUnderEntitiesConvention(DomainModel _domain)
    : IApiModelConvention<ControllerModelContext>
{
    public void Apply(ControllerModelContext context)
    {
        var entityExtensionType = context.Controller.TypeModel;
        if (!entityExtensionType.TryGetMetadata(out var entityExtensionMetadata)) { return; }
        if (!entityExtensionMetadata.TryGetSingle<EntityExtensionAttribute>(out var entityExtensionAttribute)) { return; }

        var entityType = _domain.Types[entityExtensionAttribute.EntityType];
        if (!entityType.TryGetMetadata(out var entityMetadata)) { return; }

        if (!entityMetadata.TryGetSingle<EntityAttribute>(out _)) { return; }

        context.Controller.GroupName = entityType.Name.Pluralize();
    }
}