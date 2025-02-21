using System.Diagnostics.CodeAnalysis;

namespace Baked.CodingStyle.UseNullableTypes;

public class NonOptionalNotNullParametersAreRequiredConvention : IApiModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (context.Parameter.IsOptional) { return; }
        if (context.Parameter.MappedParameter is null) { return; }
        if (!context.Parameter.MappedParameter.Has<NotNullAttribute>()) { return; }
        if (context.Parameter.FromRoute) { return; }
        if (context.Parameter.FromServices) { return; }

        context.Parameter.AddRequiredAttributes();
    }
}