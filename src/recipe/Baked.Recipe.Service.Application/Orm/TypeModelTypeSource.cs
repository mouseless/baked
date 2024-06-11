using Baked.Domain.Model;
using FluentNHibernate;
using FluentNHibernate.Diagnostics;

namespace Baked.Orm;

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