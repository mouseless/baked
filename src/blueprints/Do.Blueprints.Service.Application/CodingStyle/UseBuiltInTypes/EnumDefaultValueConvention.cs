using Do.RestApi.Configuration;

namespace Do.CodingStyle.UseBuiltInTypes;

public class EnumDefaultValueConvention : IApiModelConvention<ParameterModelContext>
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