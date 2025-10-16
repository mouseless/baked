using Baked.Architecture;
using Baked.RestApi.Model;
using FluentNHibernate.Conventions.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Baked.CodingStyle.ObjectAsJson;

public class ObjectAsJsonCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.SetTypeAttribute(new ApiInputAttribute(),
                when: c => c.Type.Is<object>()
            );

            builder.Conventions.Add(new SingleObjectParametersDontUseRequestClassConvention());
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
            swaggerGenOptions.OperationFilter<ObjectResponseOperationFilter>();
        });
    }
}