using Baked.Architecture;
using Baked.Business;
using Baked.Orm;
using Baked.RestApi;
using Humanizer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;
using NHibernate.Id;

namespace Baked.CodingStyle.Id;

public class IdCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureDomainModelBuilder(builder =>
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

        configurator.Domain.ConfigureExportConfigurations(exports =>
        {
            exports.Build("DataAccess", export => export
                .Include<IdAttribute>()
                .AddProperty(id =>
                {
                    var type = id.GetMapping().UserType.Name.Kebaberize();
                    if (type.StartsWith("id-")) { type = type[3..]; }
                    if (type.EndsWith("-user-type")) { type = type[..^10]; }

                    return new(type);
                })
                .AddProperty(id =>
                {
                    var generatorType = id.GetMapping().IdentifierGenerator;
                    var generator = generatorType is not null && generatorType != typeof(Assigned)
                        ? generatorType.Name.Kebaberize()
                        : null;

                    return new(generator);
                })
            );
        });

        configurator.CodeGeneration.ConfigureGeneratedAssemblyCollection(generatedAssemblies =>
        {
            configurator.Domain.UsingDomainModel(domain =>
            {
                generatedAssemblies.Add(nameof(IdCodingStyleFeature),
                    assembly => assembly
                        .AddReferenceFrom<IdCodingStyleFeature>()
                        .AddCodes(new AutoPersistenceModelConfigurerTemplate(domain)),
                    usings: [.. AutoPersistenceModelConfigurerTemplate.GlobalUsings]
                );
            });
        });

        configurator.DataAccess.ConfigureAutoPersistenceModel(model =>
        {
            configurator.CodeGeneration.UsingGeneratedContext(context =>
            {
                context.Assemblies[nameof(IdCodingStyleFeature)]
                    .CreateImplementationInstance<IAutoPersistenceModelConfigurer>()
                    ?.Configure(model);
            });
        });

        configurator.RestApi.ConfigureMvcNewtonsoftJsonOptions(options =>
        {
            options.SerializerSettings.Converters.Add(new IdJsonConverter());
            options.SerializerSettings.Converters.Add(new NullableJsonConverter<Business.Id>(new IdJsonConverter()));
        });

        configurator.RestApi.ConfigureSwaggerGenOptions(swaggerGenOptions =>
        {
            // Use 'MapType' instead of 'ISchemaFilter' for
            // not render 'Id' as a reference and display properties
            // instead of only '$ref' in schemas
            swaggerGenOptions.MapType<Business.Id>(() => new OpenApiSchema { Type = JsonSchemaType.String });
        });
    }
}