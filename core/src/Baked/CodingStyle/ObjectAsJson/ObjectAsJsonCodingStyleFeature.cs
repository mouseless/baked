using Baked.Architecture;
using Baked.RestApi.Model;
using FluentNHibernate.Conventions.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;

namespace Baked.CodingStyle.ObjectAsJson;

public class ObjectAsJsonCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.SetTypeAttribute(
                attribute: () => new ApiInputAttribute(),
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

        configurator.RestApi.ConfigureSwaggerGenOptions(swaggerGenOptions =>
        {
            swaggerGenOptions.MapType<object>(() => new OpenApiSchema { Type = JsonSchemaType.Object }); // Makes endpoint content template an object.
            swaggerGenOptions.SchemaFilter<NullTypesAreObjectSchemaFilter>();
            swaggerGenOptions.OperationFilter<ObjectResponseOperationFilter>();
        });
    }
}