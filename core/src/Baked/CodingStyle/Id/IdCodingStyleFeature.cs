using Baked.Architecture;
using Baked.Business;
using Baked.Orm;
using FluentNHibernate.Conventions.Helpers;
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
            model.Conventions.Add(ForeignKey.EndsWith("Id"));

            configurator.UsingGeneratedContext(context =>
            {
                context.Assemblies[nameof(IdCodingStyleFeature)]
                    .CreateImplementationInstance<IAutoPersistenceModelConfigurer>()
                    ?.Configure(model);
            });
        });

        configurator.ConfigureAutomapping(automapping =>
        {
            automapping.MemberIsId.Add(m => m.PropertyType == typeof(Business.Id));
        });

        configurator.ConfigureMvcNewtonsoftJsonOptions(options =>
        {
            options.SerializerSettings.Converters.Add(new IdJsonConverter());
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