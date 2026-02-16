using Baked.CodeGeneration;
using Baked.Domain.Model;

namespace Baked.Orm.AutoMap;

public class AutoPersistenceModelConfigurerTemplate : CodeTemplateBase
{
    public static string[] GlobalUsings =
        [
            "Baked.Orm",
            "FluentNHibernate.Automapping",
        ];

    readonly IEnumerable<TypeModelMembers> _entities;

    public AutoPersistenceModelConfigurerTemplate(DomainModel domain)
    {
        _entities = domain
            .Types
            .Having<EntityAttribute>()
            .Where(type => type.HasMembers())
            .Select(type => type.GetMembers());

        AddReferences(_entities);
    }

    protected override IEnumerable<string> Render() =>
        [Configurer()];

    string Configurer() => $$"""
        namespace AutoMapOrmFeature;

        public class AutoPersistenceModelConfigurer : IAutoPersistenceModelConfigurer
        {
            public void Configure(AutoPersistenceModel model)
            {
            {{ForEach(_entities
                .SelectMany(e => e.Properties
                    .Having<UniqueAttribute>()
                    .Select(p => new { Entity = e, Property = p })
                ), context => $$"""
                model.Override<{{context.Entity.CSharpFriendlyFullName}}>(x => x.Map(e => e.{{context.Property.Name}}).Unique());
            """, indentation: 2)}}
            }
        }
    """;
}