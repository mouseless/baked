using Baked.Business;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using Humanizer;

namespace Baked.CodingStyle.EntityExtensionViaComposition;

public class ExtensionsAreServedUnderEntityRoutesConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGet<ActionModelAttribute>(out var action)) { return; }
        if (context.Method.Has<InitializerAttribute>()) { return; }
        if (!context.Type.TryGetEntityTypeFromExtension(context.Domain, out var entityType)) { return; }

        action.RouteParts = [entityType.Name.Pluralize(), action.Name];
    }
}