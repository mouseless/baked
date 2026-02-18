using Baked.Architecture;
using Baked.Business;
using Baked.RestApi;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Baked.CodingStyle.ValueType;

public class ValueTypeCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Index.Type.Add<ValueTypeAttribute>();

            builder.Conventions.SetTypeAttribute(
                when: c =>
                    c.Type.IsValueType &&
                    !c.Type.IsEnum &&
                    c.Type.Namespace is not null &&
                    !c.Type.Namespace.StartsWith("System") &&
                    c.Type.IsAssignableTo(typeof(IParsable<>)),
                attribute: () => new ValueTypeAttribute()
            );
        });

        configurator.ConfigureGeneratedAssemblyCollection(generatedAssemblies =>
        {
            configurator.UsingDomainModel(domain =>
            {
                generatedAssemblies.Add(nameof(ValueTypeCodingStyleFeature),
                    assembly => assembly
                        .AddReferenceFrom<ValueTypeCodingStyleFeature>()
                        .AddCodes(new ValueTypeTemplate(domain)),
                    usings: [.. ValueTypeTemplate.GlobalUsings]
                );
            });
        });

        configurator.ConfigureAutoPersistenceModel(model =>
        {
            configurator.UsingGeneratedContext(generatedContext =>
            {
                var valueTypes = generatedContext
                    .Assemblies[nameof(ValueTypeCodingStyleFeature)]
                    .CreateRequiredImplementationInstance<IEnumerable<Type>>();

                model.Conventions.Add(new ValueTypeConvention(valueTypes));
            });
        });

        configurator.ConfigureMvcNewtonsoftJsonOptions(options =>
        {
            configurator.UsingGeneratedContext(generatedContext =>
            {
                var valueTypes = generatedContext
                    .Assemblies[nameof(ValueTypeCodingStyleFeature)]
                    .CreateRequiredImplementationInstance<IEnumerable<Type>>();

                foreach (var valueType in valueTypes)
                {
                    var converter =
                        (JsonConverter?)Activator.CreateInstance(typeof(ValueTypeJsonConverter<>).MakeGenericType(valueType)) ??
                        throw new($"Cannot create instance of a value type json converter for {valueType.Name}");

                    var nullableConverter =
                        (JsonConverter?)Activator.CreateInstance(typeof(NullableJsonConverter<>).MakeGenericType(valueType), converter) ??
                        throw new($"Cannot create instance of a nullable json converter for {valueType.Name}");

                    options.SerializerSettings.Converters.Add(converter);
                    options.SerializerSettings.Converters.Add(nullableConverter);
                }
            });
        });

        configurator.ConfigureSwaggerGenOptions(swaggerGenOptions =>
        {
            configurator.UsingGeneratedContext(generatedContext =>
            {
                var valueTypes = generatedContext
                    .Assemblies[nameof(ValueTypeCodingStyleFeature)]
                    .CreateRequiredImplementationInstance<IEnumerable<Type>>();

                foreach (var valueType in valueTypes)
                {
                    swaggerGenOptions.MapType(valueType, () => new OpenApiSchema { Type = "string" });
                }
            });
        });
    }
}