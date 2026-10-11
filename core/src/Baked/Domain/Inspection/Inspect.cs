using Baked.Domain.Configuration;
using System.Linq.Expressions;

namespace Baked.Domain.Inspection;

public class Inspect
{
    public void TypeAttribute<T>(
        Func<TypeModelMetadataContext, bool>? when = default,
        Expression<Func<T, object?>>? attribute = default
    ) where T : Attribute =>
        Attribute(
            when: when.GeneralizeOrDefault(),
            attribute: attribute
        );

    public void PropertyAttribute<T>(
        Func<PropertyModelContext, bool>? when = default,
        Expression<Func<T, object?>>? attribute = default
    ) where T : Attribute =>
        Attribute(
            when: when.GeneralizeOrDefault(),
            attribute: attribute
        );

    public void MethodAttribute<T>(
        Func<MethodModelContext, bool>? when = default,
        Expression<Func<T, object?>>? attribute = default
    ) where T : Attribute =>
        Attribute(
            when: when.GeneralizeOrDefault(),
            attribute: attribute
        );

    public void ParameterAttribute<T>(
        Func<ParameterModelContext, bool>? when = default,
        Expression<Func<T, object?>>? attribute = default
    ) where T : Attribute =>
        Attribute(
            when: when.GeneralizeOrDefault(),
            attribute: attribute
        );

    public void Attribute<T>(
        Func<DomainModelContext, bool>? when = default,
        Expression<Func<T, object?>>? attribute = default
    ) where T : Attribute
    {
        attribute ??= x => x;

        Inspection.Current = new(typeof(T), c => attribute.Compile().Invoke((T)c), attribute.ToString());
        if (when is not null)
        {
            Inspection.Current.AddFilter(nameof(when), when);
        }
    }
}