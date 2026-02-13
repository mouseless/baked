using Baked.Business;
using Baked.CodeGeneration;
using Baked.Domain.Model;
using Baked.Orm;

namespace Baked.CodingStyle.Id;

public class IdMapperTemplate : CodeTemplateBase
{
    public static readonly string[] GlobalUsings =
        [
            "Baked.Business",
            "Baked.CodingStyle.Id",
            "FluentNHibernate",
            "FluentNHibernate.Automapping",
            "FluentNHibernate.Diagnostics",
            "FluentNHibernate.Conventions.Helpers",
            "FluentNHibernate.Mapping",
            "NHibernate.Linq",
        ];

    readonly List<(TypeModel Type, IdAttribute.OrmConfig Orm)> _entities = [];

    public IdMapperTemplate(DomainModel _domain)
    {
        foreach (var entity in _domain.Types.Having<EntityAttribute>())
        {
            var idProperty = entity.GetMembers().FirstPropertyOrDefault<IdAttribute>();
            if (idProperty is null) { continue; }
            if (!idProperty.PropertyType.Is<Business.Id>()) { continue; }

            var idAttribute = idProperty.Get<IdAttribute>();
            var orm = idAttribute.Orm ?? new(typeof(IdGuidUserType)) { IdentifierGenerator = typeof(IdGuidGenerator) };

            _entities.Add((entity, orm));
        }

        AddReferences(_entities.Select(e => e.Type));
    }

    protected override IEnumerable<string> Render() =>
        [IdMapper()];

    string IdMapper() => $$"""
        namespace IdCodingStyleFeature;

        public class IdMapper : IIdMapper
        {
            public void Configure(AutoPersistenceModel model)
            {
            {{ForEach(_entities, e => $$"""
                {{ModelOverride(e.Type, e.Orm)}}
                {{ForeignKeyOverride(e.Type)}}
            """)}}
            }
        }
    """;

    string ModelOverride(TypeModel typeModel, IdAttribute.OrmConfig orm) => $$"""
        model.Override<{{typeModel.CSharpFriendlyFullName}}>(x =>
        {{If(orm.IdentifierGenerator is null, () => $$"""
            x.Id(e => e.{{typeModel.GetIdInfo().PropertyName}}).CustomType<{{orm.UserType.GetCSharpFriendlyFullName()}}>().GeneratedBy.Assigned()
        """, @else: () => $$"""
            x.Id(e => e.{{typeModel.GetIdInfo().PropertyName}}).CustomType<{{orm.UserType.GetCSharpFriendlyFullName()}}>().GeneratedBy.Custom<{{orm.IdentifierGenerator?.GetCSharpFriendlyFullName()}}>()
        """)}}
        );
    """;

    string ForeignKeyOverride(TypeModel typeModel) => $$"""
        {{ForEach(typeModel.GetMembers().Properties.Where(p => p.PropertyType.TryGetMetadata(out var metadata) && metadata.Has<EntityAttribute>()), p => $$"""
            model.Override<{{typeModel.CSharpFriendlyFullName}}> (x => x.References(r => r.{{p.Name}}).Column("{{p.Name}}{{p.PropertyType.GetIdInfo().PropertyName}}"));
        """)}}
    """;
}