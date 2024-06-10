using Baked.RestApi.Configuration;

namespace Baked.CodingStyle.UseNullableTypes;

public class SetDefaultValueForNullableEnumConvention : IApiModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (!context.Parameter.IsOptional) { return; }
        if (!context.Parameter.TypeModel.IsAssignableTo(typeof(Nullable<>))) { return; }
        if (!context.Parameter.TypeModel.TryGetGenerics(out var generics)) { return; }
        if (!generics.GenericTypeArguments.Any()) { return; }
        if (!generics.GenericTypeArguments[0].Model.IsEnum) { return; }

        var enumType = generics.GenericTypeArguments[0].Model;
        context.Parameter.DefaultValueRenderer = defaultValue =>
        {
            var enumName = string.Empty;

            enumType.Apply(t => enumName = Enum.GetName(t, defaultValue));

            return $"{enumType.CSharpFriendlyFullName}.{enumName}";
        };
    }
}