using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.CodingStyle.UseBuiltInTypes;

public class BoolDefaultValueConvention : IDomainModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (!context.Parameter.TryGetSingle<ParameterModel>(out var parameter)) { return; }
        if (!context.Parameter.ParameterType.Is<bool>() && !context.Parameter.ParameterType.Is<bool?>()) { return; }

        parameter.DefaultValueRenderer = defaultValue => $"{defaultValue}".ToLowerInvariant();
    }
}