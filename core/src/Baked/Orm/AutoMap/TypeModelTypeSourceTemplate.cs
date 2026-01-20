using Baked.CodeGeneration;
using Baked.Domain.Model;

namespace Baked.Orm.AutoMap;

public class TypeModelTypeSourceTemplate(DomainModel _domain)
    : CodeTemplateBase
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

    Lazy<IEnumerable<TypeModel>> _entities = new(() => _domain.Types.Having<EntityAttribute>().Where(CheckType));

    protected override IEnumerable<string> Render() =>
        [TypeModelSource()];

    string TypeModelSource() => $$"""
    namespace AutoMapFeature;

    public class TypeModelTypeSource
        : ITypeSource
    {
        string ITypeSource.GetIdentifier() =>
            nameof(TypeModelTypeSource);

        IEnumerable<Type> ITypeSource.GetTypes()
        {
            {{If(!_entities.Value.Any(), () => """
            return Array.Empty<Type>();
            """, @else: () => ForEach(_entities.Value, entity => $$"""
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