using Do.Domain.Model;
using FluentNHibernate;
using FluentNHibernate.Diagnostics;

namespace Do.Orm.Default;

public class TypeModelTypeSource(IEnumerable<TypeModel> _types)
    : ITypeSource
{
    string ITypeSource.GetIdentifier() =>
        nameof(TypeModelTypeSource);

    IEnumerable<Type> ITypeSource.GetTypes()
    {
        foreach (var type in _types)
        {
            Type? result = null;
            type.Apply(t => result = t);
            if (result is null) { continue; }

            yield return result;
        }
    }

    void ITypeSource.LogSource(IDiagnosticLogger logger) { }
}