using Do.Architecture;
using Do.Business;
using FluentNHibernate.Conventions.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Do.CodingStyle.ObjectAsJson;

public class ObjectAsJsonCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddTypeMetadata(new ApiInputAttribute(),
                when: c => c.Type.Is<object>()
            );
        });

        configurator.ConfigureApiModelConventions(conventions =>
        {
            conventions.Add(new ObjectParameterFromBodyConvention());
        });

        configurator.ConfigureAutoPersistenceModel(model =>
        {
            model.Conventions.Add(ConventionBuilder.Property.When(
                x => x.Expect(p => p.Property.PropertyType == typeof(object)),
                x => x.CustomType(typeof(ObjectUserType))
            ));
        });

        configurator.ConfigureSwaggerGenOptions(swaggerGenOptions =>
        {
            swaggerGenOptions.MapType<object>(() => new OpenApiSchema { Type = "object" }); // Makes endpoint content template an object.
            swaggerGenOptions.SchemaFilter<NullTypesAreObjectSchemaFilter>();
            swaggerGenOptions.DocumentFilter<ObjectResponseDocumentFilter>();
        });
    }
}