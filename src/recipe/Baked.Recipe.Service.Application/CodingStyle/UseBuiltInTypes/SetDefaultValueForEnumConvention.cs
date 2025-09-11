using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.CodingStyle.UseBuiltInTypes;

public class SetDefaultValueForEnumConvention : IDomainModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (!context.Parameter.ParameterType.IsEnum) { return; }
        if (!context.Parameter.TryGet<ParameterModelAttribute>(out var parameter)) { return; }

        var enumType = context.Parameter.ParameterType;
        parameter.DefaultValueRenderer = defaultValue =>
        {
            var enumName = string.Empty;

            enumType.Apply(t => enumName = Enum.GetName(t, defaultValue));

            return $"{enumType.CSharpFriendlyFullName}.{enumName}";
        };
    }
}