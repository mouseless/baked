using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace Baked.CodingStyle.ValueType;

public class ValueTypeConvention : IUserTypeConvention
{
    public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria) =>
        criteria.Expect(x =>
            x.Property.PropertyType.SkipNullable().IsCustomValueType()
        );

    public void Apply(IPropertyInstance instance)
    {
        var type = instance.Property.PropertyType.SkipNullable();
        var userType = typeof(ValueTypeUserType<>).MakeGenericType(type);

        instance.CustomType(userType);
    }
}