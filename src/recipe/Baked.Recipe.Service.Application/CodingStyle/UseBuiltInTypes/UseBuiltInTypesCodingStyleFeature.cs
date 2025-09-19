using Baked.Architecture;
using Baked.RestApi;
using Baked.RestApi.Model;
using FluentNHibernate.Conventions.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Baked.CodingStyle.UseBuiltInTypes;

public class UseBuiltInTypesCodingStyleFeature(IEnumerable<string> _textPropertySuffixes)
    : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.SetTypeMetadata(new ApiInputAttribute(),
                when: c =>
                  c.Type.IsEnum ||
                  c.Type.Is<Uri>() ||
                  c.Type.IsAssignableTo(typeof(IParsable<>)) ||
                  c.Type.IsAssignableTo(typeof(string)),
              order: RestApiLayer.MinConventionOrder
            );
            builder.Conventions.SetTypeMetadata(new ApiInputAttribute(),
                when: c =>
                    c.Type.IsAssignableTo(typeof(IEnumerable<>)) &&
                    c.Type.IsGenericType && c.Type.TryGetGenerics(out var generics) &&
                    generics.GenericTypeArguments.FirstOrDefault()?.Model.TryGetMetadata(out var genericArgMetadata) == true &&
                    genericArgMetadata.Has<ApiInputAttribute>(),
                order: 20
            );
            builder.Conventions.SetTypeMetadata(new ApiInputAttribute(),
                when: c =>
                    c.Type.IsArray && c.Type.TryGetGenerics(out var generics) &&
                    generics.ElementType?.TryGetMetadata(out var elementMetadata) == true &&
                    elementMetadata.Has<ApiInputAttribute>(),
                order: 20
            );

            builder.Conventions.Add(new BoolDefaultValueConvention());
            builder.Conventions.Add(new SetDefaultValueForEnumConvention());
            builder.Conventions.Add(new StringDefaultValueConvention());
        });

        configurator.ConfigureAutoPersistenceModel(model =>
        {
            model.Conventions.Add(ConventionBuilder.Property.When(
                x => x.Expect(p =>
                    p.Property.PropertyType == typeof(string) &&
                    _textPropertySuffixes.Any(suffix => p.Property.Name.EndsWith(suffix))
                ),
                x => x.CustomSqlType("TEXT")
            ));
        });

        configurator.ConfigureSwaggerGenOptions(swaggerGenOptions =>
        {
            swaggerGenOptions.SchemaFilter<ConvertEnumToStringSchemaFilter>();
        });

        configurator.ConfigureMvcNewtonsoftJsonOptions(options =>
        {
            options.SerializerSettings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));
        });
    }
}