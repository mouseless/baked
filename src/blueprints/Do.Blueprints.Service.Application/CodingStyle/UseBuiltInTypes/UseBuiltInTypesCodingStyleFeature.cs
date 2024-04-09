using System.Runtime.Serialization;
using Do.Architecture;
using Do.Business.Attributes;
using Do.Domain.Model;
using Do.RestApi.Configuration;
using FluentNHibernate.Conventions.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Do.CodingStyle.UseBuiltInTypes;

public class UseBuiltInTypesCodingStyleFeature(IEnumerable<string> _textPropertySuffices)
    : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddType(new ApiInputAttribute(),
                when: type =>
                  type.IsEnum ||
                  type.Is<Uri>() ||
                  type.Is<object>() ||
                  type.IsAssignableTo(typeof(IParsable<>)) ||
                  type.IsAssignableTo(typeof(string))
            );
            builder.Conventions.AddType(new ApiInputAttribute(),
                when: type =>
                    type.IsAssignableTo(typeof(Nullable<>)) &&
                    type.GenericTypeArguments.FirstOrDefault()?.Model.TryGetMetadata(out var genericArgumentMetadata) == true &&
                    genericArgumentMetadata.Has<ApiInputAttribute>()
            );
            builder.Conventions.AddType(new ApiInputAttribute(),
                when: type =>
                    type.IsAssignableTo(typeof(IEnumerable<>)) &&
                    type.IsGenericType && type.TryGetGenerics(out var generics) &&
                    generics.GenericTypeArguments.FirstOrDefault()?.Model.TryGetMetadata(out var genericArgMetadata) == true &&
                    genericArgMetadata.Has<ApiInputAttribute>(),
                order: 20
            );
            builder.Conventions.AddType(new ApiInputAttribute(),
                when: type =>
                    type.IsArray && type.TryGetGenerics(out var generics) &&
                    generics.ElementType?.TryGetMetadata(out var elementMetadata) == true &&
                    elementMetadata.Has<ApiInputAttribute>(),
                order: 20
            );
        });

        configurator.ConfigureAutoPersistenceModel(model =>
        {
            model.Conventions.Add(ConventionBuilder.Property.When(
                x => x.Expect(p =>
                    p.Property.PropertyType == typeof(string) &&
                    _textPropertySuffices.Any(suffix => p.Property.Name.EndsWith(suffix))
                ),
                x => x.CustomSqlType("TEXT")
            ));
        });

        configurator.ConfigureApiModelConventions(conventions =>
        {
            conventions.Add(new EnumDefaultValueConvention());
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

    class EnumDefaultValueConvention : IApiModelConvention<ParameterModelContext>
    {
        public void Apply(ParameterModelContext context)
        {
            TypeModel? enumType = null;
            if (context.Parameter.TypeModel.IsEnum) { enumType = context.Parameter.TypeModel; }
            if (context.Parameter.TypeModel.IsAssignableTo(typeof(Nullable<>)) &&
                context.Parameter.TypeModel.TryGetGenerics(out var generics))
            {
                enumType = generics.GenericTypeArguments.FirstOrDefault()?.Model;
            }

            if (enumType is null) { return; }

            context.Parameter.DefaultValueRenderer = defaultValue =>
            {
                var enumName = string.Empty;

                enumType.Apply(t => enumName = Enum.GetName(t, defaultValue));

                return $"{enumType.CSharpFriendlyFullName}.{enumName}";
            };
        }
    }

    class ConvertEnumToStringSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (!context.Type.IsEnum) { return; }

            schema.Type = "string";
            schema.Format = null;
            schema.Enum.Clear();

            foreach (var enumName in Enum.GetNames(context.Type))
            {
                var memberInfo = context.Type.GetMember(enumName).FirstOrDefault(m => m.DeclaringType == context.Type);
                var enumMemberAttribute = memberInfo?.GetCustomAttributes(typeof(EnumMemberAttribute), false).OfType<EnumMemberAttribute>().FirstOrDefault();

                var label = enumMemberAttribute == null || string.IsNullOrWhiteSpace(enumMemberAttribute.Value)
                    ? enumName
                    : enumMemberAttribute.Value;

                schema.Enum.Add(new OpenApiString(label.ToLowerInvariant()));
            }
        }
    }
}