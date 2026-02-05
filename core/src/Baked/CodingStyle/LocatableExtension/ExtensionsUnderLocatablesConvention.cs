using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using Humanizer;

namespace Baked.CodingStyle.LocatableExtension;

public class ExtensionsUnderLocatablesConvention : IDomainModelConvention<TypeModelContext>
{
    public void Apply(TypeModelContext context)
    {
        if (!context.Type.TryGetMetadata(out var metadata)) { return; }
        if (!metadata.TryGet<ControllerModelAttribute>(out var controller)) { return; }
        if (!context.Type.TryGetLocatableTypeFromExtension(context.Domain, out var locatableType)) { return; }

        controller.GroupName = locatableType.Name.Pluralize();
    }
}