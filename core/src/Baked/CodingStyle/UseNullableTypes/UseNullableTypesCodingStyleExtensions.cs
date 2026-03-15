using Baked.CodingStyle;
using Baked.CodingStyle.UseNullableTypes;
using Baked.RestApi.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Baked;

public static class UseNullableTypesCodingStyleExtensions
{
    extension(CodingStyleConfigurator _)
    {
        public UseNullableTypesCodingStyleFeature UseNullableTypes() =>
            new();
    }

    extension(ParameterModelAttribute parameter)
    {
        public void AddRequiredAttributes(bool isValueType)
        {
            if (parameter.FromRoute || parameter.FromServices) { return; }

            if (isValueType)
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
}