using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.CodingStyle.UseBuiltInTypes;

public class StringDefaultValueConvention : IDomainModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        var parameterType = context.Parameter.ParameterType;
        if (!parameterType.Is<string>()) { return; }
        if (!context.Parameter.TryGetSingle<ParameterModelAttribute>(out var parameter)) { return; }

        parameter.DefaultValueRenderer = defaultValue => $"\"{defaultValue}\"";
    }
}