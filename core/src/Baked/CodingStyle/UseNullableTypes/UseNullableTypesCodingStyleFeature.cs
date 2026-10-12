using Baked.Architecture;
using Baked.Binding;
using Baked.CodingStyle.UseNullableTypes;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Baked;

public static class UseNullableTypesCodingStyleFeature
{
    public static IFeature<CodingStyleFeature.Configurator> UseNullableTypes(this CodingStyleFeature.Configurator x) => x.Define(new NullabilityInfoContext(), (configurator, nullability) =>
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.SetTypeAttribute(
                when: c =>
                    c.Type.IsAssignableTo(typeof(Nullable<>)) &&
                    c.Type.GenericTypeArguments.FirstOrDefault()?.Model.TryGetMetadata(out var genericArgumentMetadata) == true &&
                    genericArgumentMetadata.Has<Bindable>(),
                attribute: () => new Bindable(),
                order: Order.At.Infra.Min
            );

            conventions.SetParameterAttribute(
                when: c =>
                {
                    var nullable = c.Parameter.ParameterType.IsAssignableTo(typeof(Nullable<>));
                    c.Parameter.Apply(p =>
                    {
                        nullable = nullable || nullability.Create(p).WriteState is NullabilityState.Nullable;
                    });

                    return !nullable;
                },
                attribute: () => new NotNullAttribute(),
                order: Order.At.Infra.Min
            );

            conventions.SetParameterAttribute(
                when: c => !c.Parameter.IsOptional && !c.Parameter.IsNullable,
                attribute: () => new RequiredAttribute(),
                order: Order.At.Infra
            );

            conventions.Add(new RequiredParametersAreRequiredInApiModelConvention(), order: Order.At.Infra);
            conventions.Add(new SetDefaultValueForNullableEnumConvention(), order: Order.At.Infra);
        });

        configurator.RestApi.ConfigureApiModel(api =>
        {
            api.Usings.AddRange([
                "Microsoft.AspNetCore.Mvc.ModelBinding",
                "Newtonsoft.Json",
                "System.ComponentModel.DataAnnotations"
            ]);
        });
    });

    extension(ApiParameter parameter)
    {
        public bool HasRequiredAttributes =>
            parameter.AdditionalAttributes.Contains(nameof(BindRequiredAttribute)) ||
            parameter.AdditionalAttributes.Contains(nameof(RequiredAttribute));

        public void AddRequiredAttributes(bool isValueType)
        {
            if (parameter.FromRoute || parameter.FromServices) { return; }

            if (isValueType)
            {
                parameter.AdditionalAttributes.Add(nameof(BindRequiredAttribute));
            }
            else
            {
                parameter.AdditionalAttributes.Add(nameof(RequiredAttribute));
            }

            parameter.AdditionalAttributes.Add($"{nameof(JsonPropertyAttribute)}(Required = {nameof(Required)}.{Required.Always})");
        }
    }
}