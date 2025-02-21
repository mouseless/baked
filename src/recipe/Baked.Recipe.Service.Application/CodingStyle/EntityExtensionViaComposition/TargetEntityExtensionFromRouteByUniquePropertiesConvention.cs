using Baked.CodingStyle.SingleByUnique;
using Baked.Domain.Model;
using System.Diagnostics.CodeAnalysis;

namespace Baked.CodingStyle.EntityExtensionViaComposition;

public class TargetEntityExtensionFromRouteByUniquePropertiesConvention(DomainModel _domain)
    : TargetEntityFromRouteByUniquePropertiesConvention(_domain)
{
    protected override bool TryGetEntityType(ParameterModelContext context, [NotNullWhen(true)] out TypeModel? entityType, out TypeModel? castTo)
    {
        castTo = context.Parameter.TypeModel;

        return castTo.TryGetEntityTypeFromExtension(Domain, out entityType);
    }
}