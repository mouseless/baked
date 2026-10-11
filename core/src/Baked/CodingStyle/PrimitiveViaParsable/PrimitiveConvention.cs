using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace Baked.CodingStyle.PrimitiveViaParsable;

public class PrimitiveConvention(IEnumerable<Type> _valueTypes)
    : IUserTypeConvention
{
    readonly HashSet<Type> _valueTypesSet = [.. _valueTypes];

    public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria) =>
        criteria.Expect(x =>
            _valueTypesSet.Contains(x.Property.PropertyType.SkipNullable())
        );

    public void Apply(IPropertyInstance instance)
    {
        var type = instance.Property.PropertyType.SkipNullable();
        var userType = typeof(PrimitiveUserType<>).MakeGenericType(type);

        instance.CustomType(userType);
    }
}