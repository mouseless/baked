using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace Baked.CodingStyle.FlagsEnum;

public class FlagsEnumTypeConvention : IUserTypeConvention
{
    public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria) =>
        criteria.Expect(x =>
        {
            var pt = Nullable.GetUnderlyingType(x.Property.PropertyType) ?? x.Property.PropertyType;

            return pt.IsEnum && pt.IsDefined(typeof(FlagsAttribute), false);
        });

    public void Apply(IPropertyInstance instance)
    {
        instance.CustomType(instance.Property.PropertyType);
    }
}