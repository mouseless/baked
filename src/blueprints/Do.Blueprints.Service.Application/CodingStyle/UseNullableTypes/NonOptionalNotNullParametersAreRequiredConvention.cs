using Do.RestApi.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Do.CodingStyle.UseNullableTypes;

public class NonOptionalNotNullParametersAreRequiredConvention : IApiModelConvention<ParameterModelContext>, IApiModelConvention<ApiModelContext>
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

    public void Apply(ApiModelContext context)
    {
        context.Api.Usings.AddRange([
            "Microsoft.AspNetCore.Mvc.ModelBinding",
            "Newtonsoft.Json",
            "System.ComponentModel.DataAnnotations"
        ]);
    }
}