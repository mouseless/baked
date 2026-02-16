using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace Baked.CodingStyle.ValueType;

// TODO this is just a prototype using AI
public class ValueTypeConvention : IUserTypeConvention
{
    public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
    {
        criteria.Expect(x =>
        {
            var type = x.Property.PropertyType;
            var underlying = Nullable.GetUnderlyingType(type) ?? type;

            return underlying.IsValueType
                   && !underlying.IsEnum
                   && underlying.Namespace?.StartsWith("System") != true
                   && underlying.GetInterfaces().Any(i =>
                        i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IParsable<>));
        });
    }

    public void Apply(IPropertyInstance instance)
    {
        var type = instance.Property.PropertyType;
        var underlying = Nullable.GetUnderlyingType(type) ?? type;

        var userType = typeof(ValueTypeUserType<>).MakeGenericType(underlying);

        instance.CustomType(userType);
        instance.Length(255);
    }
}