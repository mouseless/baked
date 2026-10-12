using Baked.Architecture;
using Baked.Binding;
using Baked.CodingStyle.UseBuiltInTypes;
using Baked.Domain.Configuration;
using Baked.Domain.Model;
using FluentNHibernate.Conventions.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace Baked;

public static class UseBuiltInTypesCodingStyleFeature
{
    public static IFeature<CodingStyleFeature.Configurator> UseBuiltInTypes(this CodingStyleFeature.Configurator x,
        IEnumerable<string>? textPropertySuffixes = default
    ) => x.Define(textPropertySuffixes ?? ["Data", "Description"], (configurator, textPropertySuffixes) =>
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
                    textPropertySuffixes.Any(suffix => p.Property.Name.EndsWith(suffix))
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
    });

    extension(TypeModel type)
    {
        public bool TryGetElementType([NotNullWhen(true)] out TypeModel? elementType)
        {
            elementType = default;

            if (!type.IsAssignableTo(typeof(IEnumerable<>))) { return false; }
            if (!type.TryGetGenerics(out var enumerableGenerics)) { return false; }

            elementType = type.IsArray
                ? enumerableGenerics.ElementType
                : enumerableGenerics.GenericTypeArguments.FirstOrDefault()?.Model;

            return elementType is not null;
        }

        public TypeModel GetElementType()
        {
            if (!type.TryGetElementType(out var result))
            {
                throw DiagnosticCode.RequiresElementType.Exception(
                    $"{type.Name} does not have an element type"
                );
            }

            return result;
        }
    }
}