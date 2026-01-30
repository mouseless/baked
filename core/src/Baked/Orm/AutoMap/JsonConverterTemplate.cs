using Baked.Business;
using Baked.CodeGeneration;
using Baked.Domain.Model;
using Humanizer;

namespace Baked.Orm.AutoMap;

public class JsonConverterTemplate(DomainModel _domain)
    : CodeTemplateBase
{
    public static string[] GlobalUsings =
        [
            "Baked.Orm",
            "Baked.RestApi",
            "Baked.Runtime",
            "Microsoft.Extensions.DependencyInjection"
        ];

    protected override IEnumerable<string> Render() => [
        ServiceAdder(),
        .. _domain
            .Types
            .Having<EntityAttribute>()
            .Select(type => type.GetMembers())
            .Select(JsonConverter),
        ContractResolverConfigurer(
        [
            .. _domain
            .Types
            .Having<EntityAttribute>()
            .Select(type => type.GetMembers())
        ])
    ];

    string ServiceAdder() => $$"""
        namespace AutoMapFeature.Locatability;

        public class ServiceAdder : IServiceAdder
        {
            public void AddServices(IServiceCollection services)
            {
            {{ForEach(_domain.Types.Having<EntityAttribute>(), entity => $$"""
                services.AddSingleton<{{entity.Name}}JsonConverter>();
            """)}}
            }
        }
    """;

    string JsonConverter(TypeModelMembers entity) => $$"""
        namespace AutoMapFeature.Locatability;

        public class {{entity.Name}}JsonConverter(IQueryContext<{{entity.CSharpFriendlyFullName}}> _queryContext)
            : {{typeof(EntityJsonConverter<,>).Namespace}}.EntityJsonConverter<{{entity.CSharpFriendlyFullName}}, string>(_queryContext)
        {
            protected override string IdProp => "{{entity.GetIdInfo().PropertyName.Camelize()}}";
            protected override IEnumerable<string> LabelProps => [{{Labels(entity)}}];

            protected override string GetId({{entity.CSharpFriendlyFullName}} entity) =>
                entity.Id.ToString();

            protected override string GetLabel({{entity.CSharpFriendlyFullName}} entity, string labelProp) =>
                labelProp switch
                {
                {{ForEach(entity.Properties.Having<LabelAttribute>(), label => $$"""
                    "{{label.Name.Camelize()}}" => entity.{{label.Name}},
                """, separator: $",{Environment.NewLine}")}}
                    _ => throw new InvalidOperationException($"`{labelProp}` is not a label property for `{{entity.Name}}`")
                };
        }
    """;

    string Labels(TypeModelMembers entity) =>
        ForEach(entity.Properties.Having<LabelAttribute>(),
            label => $$""" "{{label.Name.Camelize()}}" """,
            separator: ", "
        );

    string ContractResolverConfigurer(IEnumerable<TypeModelMembers> entities) => $$"""
        namespace AutoMapFeature.Locatability;

        public class ContractResolverConfigurer : IContractResolverConfigurer
        {
            public void Configure(ExtendedContractResolver contractResolver)
            {
            {{ForEach(entities.SelectMany(e => e.Properties
                .Where(p => p.IsPublic)
                .Where(p => p.PropertyType.TryGetMetadata(out var metadata) && metadata.Has<EntityAttribute>())
                .Select(p => new { Property = p, Entity = e })
            ), relation => $$"""
                contractResolver.SetPropertyConverterType(
                    typeof({{relation.Entity.CSharpFriendlyFullName}}),
                    nameof({{relation.Entity.CSharpFriendlyFullName}}.{{relation.Property.Name}}),
                    typeof({{relation.Property.PropertyType.Name}}JsonConverter)
                );
            """)}}
            }
        }
    """;
}