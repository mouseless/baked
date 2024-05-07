using Do.Domain.Model;
using Do.RestApi.Configuration;
using Humanizer;

namespace Do.CodingStyle.EntityExtensionViaComposition;

public class EntityExtensionsUnderEntitiesConvention(DomainModel _domain)
    : IApiModelConvention<ControllerModelContext>
{
    public void Apply(ControllerModelContext context)
    {
        if (!context.Controller.TypeModel.TryGetEntityTypeFromExtension(_domain, out var entityType)) { return; }

        context.Controller.GroupName = entityType.Name.Pluralize();
    }
}