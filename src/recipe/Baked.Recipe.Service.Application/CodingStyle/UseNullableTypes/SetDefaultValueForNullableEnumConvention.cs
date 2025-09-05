using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.CodingStyle.UseNullableTypes;

public class SetDefaultValueForNullableEnumConvention : IDomainModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (!context.Parameter.IsOptional) { return; }
        if (!context.Parameter.TryGet<ParameterModelAttribute>(out var parameter)) { return; }
        if (!context.Parameter.ParameterType.IsAssignableTo(typeof(Nullable<>))) { return; }
        if (!context.Parameter.ParameterType.TryGetGenerics(out var generics)) { return; }
        if (!generics.GenericTypeArguments.Any()) { return; }
        if (!generics.GenericTypeArguments[0].Model.IsEnum) { return; }

        var enumType = generics.GenericTypeArguments[0].Model;
        parameter.DefaultValueRenderer = defaultValue =>
        {
            var enumName = string.Empty;

            enumType.Apply(t => enumName = Enum.GetName(t, defaultValue));

            return $"{enumType.CSharpFriendlyFullName}.{enumName}";
        };
    }
}