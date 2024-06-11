using Baked.RestApi.Configuration;

namespace Baked.CodingStyle.UseBuiltInTypes;

public class BoolDefaultValueConvention : IApiModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        var parameterType = context.Parameter.TypeModel;
        if (!parameterType.Is<bool>() && !parameterType.Is<bool?>()) { return; }

        context.Parameter.DefaultValueRenderer = defaultValue => $"{defaultValue}".ToLowerInvariant();
    }
}