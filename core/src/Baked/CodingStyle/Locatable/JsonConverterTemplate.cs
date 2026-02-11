using Baked.Business;
using Baked.CodeGeneration;
using Baked.Domain.Model;
using Humanizer;

namespace Baked.CodingStyle.Locatable;

public class JsonConverterTemplate(DomainModel _domain)
    : CodeTemplateBase
{
    public static string[] GlobalUsings =
        [
            "Baked.Business",
            "Baked.CodingStyle.Locatable",
            "Baked.RestApi",
            "Baked.Runtime",
            "Microsoft.Extensions.DependencyInjection",
            "Newtonsoft.Json"
        ];

    protected override IEnumerable<string> Render() =>
        [
            ServiceAdder(),
            .. _domain
                .Types
                .Having<LocatableAttribute>()
                .Select(type => type.GetMembers())
                .Select(JsonConverter),
            ContractResolverConfigurer(
            [
                .. _domain
                .Types
                .Having<LocatableAttribute>()
                .Select(type => type.GetMembers())
            ])
        ];

    string ServiceAdder() => $$"""
        namespace LocatableCodingStyleFeature;

        public class ServiceAdder : IServiceAdder
        {
            public void AddServices(IServiceCollection services)
            {
            {{ForEach(_domain.Types.Having<LocatableAttribute>(), locatable => $$"""
                services.AddSingleton<{{locatable.Name}}JsonConverter>();
            """, indentation: 2)}}
            }
        }
    """;

    string JsonConverter(TypeModelMembers locatable) => If(locatable.TryGetIdInfo(out var id), () => $$"""
        namespace LocatableCodingStyleFeature;

        public class {{locatable.Name}}JsonConverter(ILocator<{{locatable.CSharpFriendlyFullName}}> _locator, Func<LocatableInitializations> _getLocatableInitializations)
            : {{typeof(LocatableJsonConverter<,>).Namespace}}.LocatableJsonConverter<{{locatable.CSharpFriendlyFullName}}, string>(_locator, _getLocatableInitializations)
        {
            protected override string IdProp => "{{id!.PropertyName.Camelize()}}";
            protected override IEnumerable<string> LabelProps => [{{Labels(locatable)}}];

            protected override string GetId({{locatable.CSharpFriendlyFullName}} locatable) =>
                locatable.{{id!.PropertyName}}.ToString();

            protected override string GetLabel({{locatable.CSharpFriendlyFullName}} locatable, string labelProp) =>
                labelProp switch
                {
                {{ForEach(locatable.Properties.Having<LabelAttribute>(), label => $$"""
                    "{{label.Name.Camelize()}}" => locatable.{{label.Name}},
                """, separator: $",{Environment.NewLine}", indentation: 2)}}
                    _ => throw new InvalidOperationException($"`{labelProp}` is not a label property for `{{locatable.Name}}`")
                };
        }
    """);

    string Labels(TypeModelMembers locatable) =>
        ForEach(locatable.Properties.Having<LabelAttribute>(),
            label => $$""" "{{label.Name.Camelize()}}" """,
            separator: ", "
        );

    string ContractResolverConfigurer(IEnumerable<TypeModelMembers> locatables) => $$"""
        namespace LocatableCodingStyleFeature;

        public class ContractResolverConfigurer : IContractResolverConfigurer
        {
            public Dictionary<Type, string> IdPropertyNames => new()
            {
            {{ForEach(locatables
                .Where(e => e.Properties.Having<IdAttribute>().Any())
                .Select(e => new { Type = e, IdProperty = e.Properties.Having<IdAttribute>().Single() }), context => $$"""
               { typeof({{context.Type.CSharpFriendlyFullName}}), "{{context.IdProperty.Name}}" },
            """, indentation: 2)}}
            };

            public void Configure(ExtendedContractResolver contractResolver)
            {
            {{ForEach(locatables
                .SelectMany(l => l.Properties.Select(p => new { Property = p, Type = l }))
                .Where(c => c.Property.PropertyType.TryGetMetadata(out var metadata) && metadata.Has<LocatableAttribute>()), context => $$"""
                contractResolver.SetProperty(
                    typeof({{context.Type.CSharpFriendlyFullName}}),
                    "{{context.Property.Name}}",
                    options: (property, sp) => property.Converter = sp.GetRequiredService<{{context.Property.PropertyType.Name}}JsonConverter>()
                );
            """, indentation: 2)}}
            }
        }
    """;
}