using Baked.Architecture;
using Baked.Business;
using Baked.Orm;
using FluentNHibernate.Conventions.Helpers;
using Humanizer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Baked.CodingStyle.Id;

public class IdCodingStyeFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.SetPropertyAttribute(
                when: c => c.Property.Name == "Id" && c.Property.PropertyType.Is<Business.Id>(),
                attribute: c => new IdAttribute(c.Property.Name.Camelize())
            );
        });

        configurator.ConfigureGeneratedAssemblyCollection(generatedAssemblies =>
        {
            configurator.UsingDomainModel(domain =>
            {
                generatedAssemblies.Add(nameof(IdCodingStyeFeature),
                    assembly =>
                    {
                        assembly.AddReferenceFrom<IdCodingStyeFeature>();

                        foreach (var entity in domain.Types.Having<EntityAttribute>())
                        {
                            var idProperty = entity.GetMembers().FirstPropertyOrDefault<IdAttribute>();
                            if (idProperty is null) { continue; }
                            if (idProperty.Name != "Id") { continue; }
                            if (!idProperty.PropertyType.Is<Business.Id>()) { continue; }

                            var idAttribute = idProperty.Get<IdAttribute>();
                            var orm = idAttribute.Orm ?? new(typeof(IdGuidUserType)) { IdentifierGenerator = typeof(IdGuidGenerator) };

                            assembly.AddCodes(new IdMapperTemplate(entity, orm));

                            entity.Apply(t => assembly.AddReferenceFrom(t));
                        }
                    },
                    usings: [.. IdMapperTemplate.GlobalUsings]
                );
            });
        });

        configurator.ConfigureAutoPersistenceModel(model =>
        {
            model.Conventions.Add(ForeignKey.EndsWith("Id"));

            configurator.UsingGeneratedContext(context =>
            {
                var idMapperTypes = context.Assemblies[nameof(IdCodingStyeFeature)].GetExportedTypes().Where(t => t.IsAssignableTo(typeof(IIdMapper)));
                foreach (var idMapperType in idMapperTypes)
                {
                    var idMapper = (IIdMapper?)Activator.CreateInstance(idMapperType) ?? throw new($"Cannot create instance of {idMapperType}");

                    idMapper.Configure(model);
                }
            });
        });

        configurator.ConfigureAutomapping(automapping =>
        {
            automapping.MemberIsId.Add(m => m.PropertyType == typeof(Business.Id) && m.Name == "Id");
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