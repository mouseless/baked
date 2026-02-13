using Baked.CodeGeneration;
using Baked.Domain.Model;

namespace Baked.Orm.AutoMap;

public class TypeModelTypeSourceTemplate : CodeTemplateBase
{
    public static string[] GlobalUsings =
        [
            "Baked.Orm",
            "Baked.Runtime",
            "FluentNHibernate",
            "FluentNHibernate.Diagnostics",
            "Microsoft.Extensions.DependencyInjection",
            "NHibernate.Linq"
        ];

    readonly IEnumerable<TypeModel> _entities;

    public TypeModelTypeSourceTemplate(DomainModel domain)
    {
        _entities = domain.Types.Having<EntityAttribute>().Where(CheckType);

        AddReferences(_entities);
    }

    protected override IEnumerable<string> Render() =>
        [TypeModelSource()];

    string TypeModelSource() => $$"""
    namespace AutoMapOrmFeature;

    public class TypeModelTypeSource
        : ITypeSource
    {
        string ITypeSource.GetIdentifier() =>
            nameof(TypeModelTypeSource);

        IEnumerable<Type> ITypeSource.GetTypes()
        {
            {{If(!_entities.Any(), () => """
            return Array.Empty<Type>();
            """, @else: () => ForEach(_entities, entity => $$"""
            yield return typeof({{entity.CSharpFriendlyFullName}});
            """)
            )}}
        }

        void ITypeSource.LogSource(IDiagnosticLogger logger) { }
    }
    """;

    static bool CheckType(TypeModel model)
    {
        Type? result = null;
        model.Apply(t => result = t);

        return result != null;
    }
}