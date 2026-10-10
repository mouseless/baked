using Baked.Architecture;
using Baked.Business;
using Baked.Domain.Configuration;
using Baked.RestApi;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;
using Newtonsoft.Json;

namespace Baked.CodingStyle.PrimitiveViaParsable;

public class PrimitiveViaParsableCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureBuilder(builder =>
        {
            builder.Index.Type.Add<Primitive>();
        });

        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.SetTypeAttribute(
                when: c =>
                    c.Type.IsValueType &&
                    !c.Type.IsEnum &&
                    c.Type.Namespace is not null &&
                    !c.Type.Namespace.StartsWith("System") &&
                    c.Type.IsAssignableTo(typeof(IParsable<>)),
                attribute: () => new Primitive(),
                order: Order.At.Infra
            );
        });

        configurator.Buildtime.ConfigureGeneratedAssemblyCollection(generatedAssemblies =>
        {
            configurator.Domain.UsingDomainModel(domain =>
            {
                generatedAssemblies.Add(nameof(PrimitiveViaParsableCodingStyleFeature),
                    assembly => assembly
                        .AddReferenceFrom<PrimitiveViaParsableCodingStyleFeature>()
                        .AddCodes(new PrimitiveTemplate(domain)),
                    usings: [.. PrimitiveTemplate.GlobalUsings]
                );
            });
        });

        configurator.DataAccess.ConfigureAutoPersistenceModel(model =>
        {
            configurator.Buildtime.UsingGeneratedContext(generatedContext =>
            {
                var valueTypes = generatedContext
                    .Assemblies[nameof(PrimitiveViaParsableCodingStyleFeature)]
                    .CreateRequiredImplementationInstance<IEnumerable<Type>>();

                model.Conventions.Add(new PrimitiveConvention(valueTypes));
            });
        });

        configurator.RestApi.ConfigureMvcNewtonsoftJsonOptions(options =>
        {
            configurator.Buildtime.UsingGeneratedContext(generatedContext =>
            {
                var valueTypes = generatedContext
                    .Assemblies[nameof(PrimitiveViaParsableCodingStyleFeature)]
                    .CreateRequiredImplementationInstance<IEnumerable<Type>>();

                foreach (var valueType in valueTypes)
                {
                    var converter =
                        (JsonConverter?)Activator.CreateInstance(typeof(PrimitiveJsonConverter<>).MakeGenericType(valueType)) ??
                        throw new($"Cannot create instance of a value type json converter for {valueType.Name}");

                    var nullableConverter =
                        (JsonConverter?)Activator.CreateInstance(typeof(NullableJsonConverter<>).MakeGenericType(valueType), converter) ??
                        throw new($"Cannot create instance of a nullable json converter for {valueType.Name}");

                    options.SerializerSettings.Converters.Add(converter);
                    options.SerializerSettings.Converters.Add(nullableConverter);
                }
            });
        });

        configurator.RestApi.ConfigureSwaggerGenOptions(swaggerGenOptions =>
        {
            configurator.Buildtime.UsingGeneratedContext(generatedContext =>
            {
                var valueTypes = generatedContext
                    .Assemblies[nameof(PrimitiveViaParsableCodingStyleFeature)]
                    .CreateRequiredImplementationInstance<IEnumerable<Type>>();

                foreach (var valueType in valueTypes)
                {
                    swaggerGenOptions.MapType(valueType, () => new OpenApiSchema { Type = JsonSchemaType.String });
                }
            });
        });
    }
}