using Baked.CodeGeneration;
using Baked.Domain.Model;

namespace Baked.Orm.AutoMap;

public class TypeModelTypeSourceTemplate(DomainModel _domain)
    : CodeTemplateBase
{
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
            {{ForEach(_domain.Types.Having<EntityAttribute>().Where(CheckType), entity => $$"""
            yield return typeof({{entity.CSharpFriendlyFullName}});
            """)}}
        }

        void ITypeSource.LogSource(IDiagnosticLogger logger) { }
    }
    """;

    bool CheckType(TypeModel model)
    {
        Type? result = null;
        model.Apply(t => result = t);
        return result != null;
    }
}