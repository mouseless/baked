using Baked.Architecture;
using Baked.Business;
using Baked.Orm;
using Baked.RestApi;
using Humanizer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Baked.CodingStyle.Id;

public class IdCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.RemoveTypeAttribute<ValueTypeAttribute>(
                when: c => c.Type.Is<Business.Id>(),
                order: 10
            );
            builder.Conventions.SetPropertyAttribute(
                when: c => c.Property.PropertyType.Is<Business.Id>(),
                attribute: c => new IdAttribute(c.Property.Name.Camelize()),
                order: int.MinValue + 10
            );
        });

        configurator.ConfigureGeneratedAssemblyCollection(generatedAssemblies =>
        {
            configurator.UsingDomainModel(domain =>
            {
                generatedAssemblies.Add(nameof(IdCodingStyleFeature),
                    assembly => assembly
                        .AddReferenceFrom<IdCodingStyleFeature>()
                        .AddCodes(new AutoPersistenceModelConfigurerTemplate(domain)),
                    usings: [.. AutoPersistenceModelConfigurerTemplate.GlobalUsings]
                );
            });
        });

        configurator.ConfigureAutoPersistenceModel(model =>
        {
            configurator.UsingGeneratedContext(context =>
            {
                context.Assemblies[nameof(IdCodingStyleFeature)]
                    .CreateImplementationInstance<IAutoPersistenceModelConfigurer>()
                    ?.Configure(model);
            });
        });

        configurator.ConfigureMvcNewtonsoftJsonOptions(options =>
        {
            options.SerializerSettings.Converters.Add(new IdJsonConverter());
            options.SerializerSettings.Converters.Add(new NullableJsonConverter<Business.Id>(new IdJsonConverter()));
        });

        configurator.ConfigureSwaggerGenOptions(swaggerGenOptions =>
        {
            // Use 'MapType' instead of 'ISchemaFilter' for
            // not render 'Id' as a reference and display properties
            // instead of only '$ref' in schemas
            swaggerGenOptions.MapType<Business.Id>(() => new OpenApiSchema { Type = "string" });
        });
    }
}