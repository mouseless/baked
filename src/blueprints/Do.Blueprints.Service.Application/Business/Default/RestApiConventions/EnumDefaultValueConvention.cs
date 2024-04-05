using Do.Domain.Model;
using Do.RestApi.Configuration;

namespace Do.Business.Default.RestApiConventions;

public class EnumDefaultValueConvention : IApiModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        TypeModel? enumType = null;
        if (context.Parameter.TypeModel.IsEnum) { enumType = context.Parameter.TypeModel; }
        if (context.Parameter.TypeModel.IsAssignableTo(typeof(Nullable<>)) &&
            context.Parameter.TypeModel.TryGetGenerics(out var generics))
        {
            enumType = generics.GenericTypeArguments.FirstOrDefault()?.Model;
        }

        if (enumType is null) { return; }

        context.Parameter.DefaultValueRenderer = defaultValue =>
        {
            var enumName = string.Empty;

            enumType.Apply(t => enumName = Enum.GetName(t, defaultValue));

            return $"{enumType.CSharpFriendlyFullName}.{enumName}";
        };
    }
}