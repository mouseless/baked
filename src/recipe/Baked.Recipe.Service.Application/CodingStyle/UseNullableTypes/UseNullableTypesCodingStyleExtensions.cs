using Do.CodingStyle;
using Do.CodingStyle.UseNullableTypes;
using Do.RestApi.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Do;

public static class UseNullableTypesCodingStyleExtensions
{
    public static UseNullableTypesCodingStyleFeature UseNullableTypes(this CodingStyleConfigurator _) =>
        new();

    public static void AddRequiredAttributes(this ParameterModel parameter,
        bool? isValueType = default)
    {
        if (parameter.FromRoute || parameter.FromServices) { return; }

        isValueType ??= parameter.TypeModel.IsValueType;

        if (isValueType.Value)
        {
            parameter.AdditionalAttributes.Add($"{nameof(BindRequiredAttribute)}");
        }
        else
        {
            parameter.AdditionalAttributes.Add($"{nameof(RequiredAttribute)}");
        }

        parameter.AdditionalAttributes.Add($"{nameof(JsonPropertyAttribute)}(Required = {nameof(Required)}.{Required.Always})");
    }
}