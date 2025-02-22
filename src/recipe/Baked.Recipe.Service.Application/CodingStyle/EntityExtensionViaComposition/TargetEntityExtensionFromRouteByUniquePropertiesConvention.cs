using Baked.CodingStyle.SingleByUnique;
using Baked.Domain.Configuration;
using Baked.Domain.Model;
using System.Diagnostics.CodeAnalysis;

namespace Baked.CodingStyle.EntityExtensionViaComposition;

public class TargetEntityExtensionFromRouteByUniquePropertiesConvention
    : TargetEntityFromRouteByUniquePropertiesConvention
{
    protected override bool TryGetEntityType(ParameterModelContext context, [NotNullWhen(true)] out TypeModel? entityType, out TypeModel? castTo)
    {
        castTo = context.Parameter.ParameterType;

        return castTo.TryGetEntityTypeFromExtension(context.Domain, out entityType);
    }
}