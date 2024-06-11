using Baked.RestApi.Configuration;

namespace Baked.CodingStyle.UseBuiltInTypes;

public class SetDefaultValueForEnumConvention : IApiModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (!context.Parameter.TypeModel.IsEnum) { return; }

        var enumType = context.Parameter.TypeModel;
        context.Parameter.DefaultValueRenderer = defaultValue =>
        {
            var enumName = string.Empty;

            enumType.Apply(t => enumName = Enum.GetName(t, defaultValue));

            return $"{enumType.CSharpFriendlyFullName}.{enumName}";
        };
    }
}