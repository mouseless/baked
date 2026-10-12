using Baked.Architecture;
using Baked.Business;
using Baked.CodingStyle.TypeBasedId;
using Baked.Domain.Configuration;
using Baked.Orm;
using Baked.RestApi;
using Humanizer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;
using NHibernate.Id;

namespace Baked;

public static class TypeBasedIdCodingStyleFeature
{
    public static IFeature<CodingStyleFeature.Configurator> TypeBasedId(this CodingStyleFeature.Configurator x) => x.Define(configurator =>
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.RemoveTypeAttribute<Primitive>(
                when: c => c.Type.Is<Id>(),
                order: Order.At.Infra + 10
            );
            conventions.SetPropertyAttribute(
                when: c => c.Property.PropertyType.Is<Id>(),
                attribute: c => new IdProperty(c.Property.Name.Camelize()),
                order: Order.At.Global.Min
            );
        });

        configurator.Domain.ConfigureExportConfigurations(exports =>
        {
            exports.Build("DataAccess", export => export
                .Include<IdProperty>()
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
                    if (generator?.EndsWith("-generator") == true) { generator = generator[..^10]; }

                    return new(generator);
                })
            );
        });

        configurator.Buildtime.ConfigureGeneratedAssemblyCollection(generatedAssemblies =>
        {
            configurator.Domain.UsingDomainModel(domain =>
            {
                generatedAssemblies.Add(nameof(TypeBasedIdCodingStyleFeature),
                    assembly => assembly
                        .AddReferenceFrom<CodingStyleFeature.Configurator>()
                        .AddCodes(new AutoPersistenceModelConfigurerTemplate(domain)),
                    usings: [.. AutoPersistenceModelConfigurerTemplate.GlobalUsings]
                );
            });
        });

        configurator.DataAccess.ConfigureAutoPersistenceModel(model =>
        {
            configurator.Buildtime.UsingGeneratedContext(context =>
            {
                context.Assemblies[nameof(TypeBasedIdCodingStyleFeature)]
                    .CreateImplementationInstance<IAutoPersistenceModelConfigurer>()
                    ?.Configure(model);
            });
        });

        configurator.RestApi.ConfigureMvcNewtonsoftJsonOptions(options =>
        {
            options.SerializerSettings.Converters.Add(new IdJsonConverter());
            options.SerializerSettings.Converters.Add(new NullableJsonConverter<Id>(new IdJsonConverter()));
        });

        configurator.RestApi.ConfigureSwaggerGenOptions(swaggerGenOptions =>
        {
            // Use 'MapType' instead of 'ISchemaFilter' for
            // not render 'Id' as a reference and display properties
            // instead of only '$ref' in schemas
            swaggerGenOptions.MapType<Id>(() => new OpenApiSchema { Type = JsonSchemaType.String });
        });
    });

    extension(IdProperty id)
    {
        public void Generated() =>
            id.Mapping = new(typeof(IdGuidUserType)) { IdentifierGenerator = typeof(IdGuidGenerator) };

        public void AutoIncrement() =>
            id.Mapping = new(typeof(IdIntUserType)) { IdentifierGenerator = typeof(IdentityGenerator) };

        public void Assigned() =>
            id.Mapping = new(typeof(IdStringUserType)) { IdentifierGenerator = typeof(Assigned) };

        public void AssignedGuid() =>
            id.Mapping = new(typeof(IdGuidUserType)) { IdentifierGenerator = typeof(Assigned) };

        public IdProperty.MappingOptions GetMapping() =>
            id.Mapping ??
            new(typeof(IdGuidUserType)) { IdentifierGenerator = typeof(IdGuidGenerator) };
    }
}