using Baked.Architecture;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Baked.CodingStyle.UseNullableTypes;

public class UseNullableTypesCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    NullabilityInfoContext _nullability = new();

    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.SetTypeAttribute(
                when: c =>
                    c.Type.IsAssignableTo(typeof(Nullable<>)) &&
                    c.Type.GenericTypeArguments.FirstOrDefault()?.Model.TryGetMetadata(out var genericArgumentMetadata) == true &&
                    genericArgumentMetadata.Has<ApiInputAttribute>(),
                attribute: () => new ApiInputAttribute(),
                order: Order.At.Defaults.AbsoluteMin // TODO consider using Order.At.Infra.Min
            );

            conventions.SetParameterAttribute(
                when: c =>
                {
                    var nullable = c.Parameter.ParameterType.IsAssignableTo(typeof(Nullable<>));
                    c.Parameter.Apply(p =>
                    {
                        nullable = nullable || _nullability.Create(p).WriteState is NullabilityState.Nullable;
                    });

                    return !nullable;
                },
                attribute: () => new NotNullAttribute(),
                order: Order.At.Defaults.Min
            );

            conventions.SetParameterAttribute(
                when: c => !c.Parameter.IsOptional && !c.Parameter.IsNullable,
                attribute: () => new RequiredAttribute(),
                order: Order.At.Defaults
            );

            conventions.Add(new RequiredParametersAreRequiredInApiModelConvention(), order: Order.At.Defaults);
            conventions.Add(new SetDefaultValueForNullableEnumConvention(), order: Order.At.Defaults);
        });

        configurator.RestApi.ConfigureApiModel(api =>
        {
            api.Usings.AddRange([
                "Microsoft.AspNetCore.Mvc.ModelBinding",
                "Newtonsoft.Json",
                "System.ComponentModel.DataAnnotations"
            ]);
        });
    }
}