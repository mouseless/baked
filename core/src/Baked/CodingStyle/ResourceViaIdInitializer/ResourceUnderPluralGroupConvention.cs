using Baked.Business;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using Humanizer;

namespace Baked.CodingStyle.ResourceViaIdInitializer;

public class ResourceUnderPluralGroupConvention : IDomainModelConvention<TypeModelContext>
{
    public void Apply(TypeModelContext context)
    {
        if (!context.Type.TryGetMetadata(out var metadata)) { return; }
        if (!metadata.TryGet<ApiController>(out var controller)) { return; }
        if (!metadata.Has<Locatable>()) { return; }

        controller.GroupName = controller.GroupName.Pluralize();
    }
}