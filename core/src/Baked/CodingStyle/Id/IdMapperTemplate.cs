using Baked.Business;
using Baked.CodeGeneration;
using Baked.Domain.Model;

namespace Baked.CodingStyle.Id;

public class IdMapperTemplate(TypeModel typeModel, IdAttribute.OrmConfig orm) : CodeTemplateBase
{
    public static string[] GlobalUsings =
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

    protected override IEnumerable<string> Render() =>
        [IdMapper()];

    string IdMapper() => $$"""
        namespace IdCodingStyle;

        public class {{typeModel.Name}}IdMapper : IIdMapper
        {
            public void Configure(AutoPersistenceModel model)
            {
                model.Override<{{typeModel.CSharpFriendlyFullName}}>(x =>
                {{If(orm.IdentifierGenerator is null, () => $$"""
                    x.Id(e => e.Id).CustomType<{{orm.UserType.GetCSharpFriendlyFullName()}}>().GeneratedBy.Assigned()
                """, @else: () => $$"""
                    x.Id(e => e.Id).CustomType<{{orm.UserType.GetCSharpFriendlyFullName()}}>().GeneratedBy.Custom<{{orm.IdentifierGenerator?.GetCSharpFriendlyFullName()}}>()
                """)}}
                );
            }
        }
    """;
}