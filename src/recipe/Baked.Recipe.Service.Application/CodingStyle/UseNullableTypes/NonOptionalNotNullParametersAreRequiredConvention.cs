using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using System.Diagnostics.CodeAnalysis;

namespace Baked.CodingStyle.UseNullableTypes;

public class NonOptionalNotNullParametersAreRequiredConvention : IDomainModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (!context.Parameter.TryGet<ParameterModelAttribute>(out var parameter)) { return; }
        if (context.Parameter.IsOptional) { return; }
        if (!context.Parameter.Has<NotNullAttribute>()) { return; }
        if (parameter.FromRoute) { return; }
        if (parameter.FromServices) { return; }

        parameter.AddRequiredAttributes(isValueType: context.Parameter.ParameterType.IsValueType);
    }
}