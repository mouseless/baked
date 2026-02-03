using Baked.Business;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using Humanizer;

namespace Baked.CodingStyle.LocatableExtension;

public class ExtensionsAreServedUnderLocatableRoutesConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGet<ActionModelAttribute>(out var action)) { return; }
        if (context.Method.Has<InitializerAttribute>()) { return; }
        if (!context.Type.TryGetLocatableTypeFromExtension(context.Domain, out var entityType)) { return; }

        action.RouteParts = [entityType.Name.Pluralize(), action.Name];
    }
}