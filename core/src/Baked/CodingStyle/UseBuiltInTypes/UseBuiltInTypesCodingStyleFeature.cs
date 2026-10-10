using Baked.Architecture;
using Baked.Binding;
using Baked.Domain.Configuration;
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
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.SetTypeAttribute(
                attribute: () => new Bindable(),
                when: c =>
                  c.Type.IsEnum ||
                  c.Type.Is<Uri>() ||
                  c.Type.IsAssignableTo(typeof(IParsable<>)) ||
                  c.Type.IsAssignableTo(typeof(string)),
              order: Order.At.Infra.Min
            );
            conventions.SetTypeAttribute(
                attribute: () => new Bindable(),
                when: c =>
                    c.Type.IsAssignableTo(typeof(IEnumerable<>)) &&
                    c.Type.IsGenericType && c.Type.TryGetGenerics(out var generics) &&
                    generics.GenericTypeArguments.FirstOrDefault()?.Model.TryGetMetadata(out var genericArgMetadata) == true &&
                    genericArgMetadata.Has<Bindable>(),
                order: Order.At.Infra + 20
            );
            conventions.SetTypeAttribute(
                attribute: () => new Bindable(),
                when: c =>
                    c.Type.IsArray && c.Type.TryGetGenerics(out var generics) &&
                    generics.ElementType?.TryGetMetadata(out var elementMetadata) == true &&
                    elementMetadata.Has<Bindable>(),
                order: Order.At.Infra + 20
            );

            conventions.Add(new BoolDefaultValueConvention(), order: Order.At.Infra);
            conventions.Add(new SetDefaultValueForEnumConvention(), order: Order.At.Infra);
            conventions.Add(new StringDefaultValueConvention(), order: Order.At.Infra);
        });

        configurator.DataAccess.ConfigureAutoPersistenceModel(model =>
        {
            model.Conventions.Add(ConventionBuilder.Property.When(
                x => x.Expect(p =>
                    p.Property.PropertyType == typeof(string) &&
                    _textPropertySuffixes.Any(suffix => p.Property.Name.EndsWith(suffix))
                ),
                x => x.CustomSqlType("TEXT")
            ));
        });

        configurator.RestApi.ConfigureSwaggerGenOptions(swaggerGenOptions =>
        {
            swaggerGenOptions.SchemaFilter<ConvertEnumToStringSchemaFilter>();
        });

        configurator.RestApi.ConfigureMvcNewtonsoftJsonOptions(options =>
        {
            options.SerializerSettings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));
        });
    }
}