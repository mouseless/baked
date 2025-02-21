namespace Baked.CodingStyle.UseBuiltInTypes;

public class StringDefaultValueConvention : IApiModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        var parameterType = context.Parameter.TypeModel;
        if (!parameterType.Is<string>()) { return; }

        context.Parameter.DefaultValueRenderer = defaultValue => $"\"{defaultValue}\"";
    }
}