using Do.RestApi.Configuration;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Do.CodingStyle.UseNullableTypes;

public class NonOptionalNotNullParametersAreRequiredConvention : IApiModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (context.Parameter.IsOptional) { return; }
        if (context.Parameter.MappedParameter is null) { return; }
        if (!context.Parameter.MappedParameter.Has<NotNullAttribute>()) { return; }

        if (context.Parameter.FromBodyOrForm)
        {
            context.Parameter.AdditionalAttributes.Add($"{typeof(RequiredAttribute).FullName}");
        }
        else
        {
            context.Parameter.AdditionalAttributes.Add($"{typeof(BindRequiredAttribute).FullName}");
        }
    }
}