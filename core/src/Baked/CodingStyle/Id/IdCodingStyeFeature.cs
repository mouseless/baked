using Baked.Architecture;
using Baked.Business;
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
                attribute: c => new IdAttribute(c.Property.Name.Camelize()),
                order: int.MinValue + 10
            );
        });

        configurator.ConfigureGeneratedAssemblyCollection(generatedAssemblies =>
        {
            configurator.UsingDomainModel(domain =>
            {
                generatedAssemblies.Add(nameof(IdCodingStyeFeature),
                    assembly =>
                    {
                        var codeTemplate = new IdMapperTemplate(domain);
                        assembly.AddCodes(codeTemplate);
                        assembly.AddReferences(codeTemplate.References);
                        assembly.AddReferenceFrom<IdCodingStyeFeature>();
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
                var idMapperType = context.Assemblies[nameof(IdCodingStyeFeature)].GetExportedTypes().FirstOrDefault(t => t.IsAssignableTo(typeof(IIdMapper)));
                if (idMapperType is not null)
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