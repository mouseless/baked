using Baked.Domain.Model;
using Baked.Testing;

namespace Baked.Test;

public static class DomainModelExtensions
{
    public static DomainModel TheDomainModel(this Stubber giveMe) =>
        giveMe.Spec.GenerateContext.GetDomainModel();

    public static TypeModel TheTypeModel(this Stubber giveMe, Type type) =>
        giveMe.TheDomainModel().Types[type];

    public static AttributeCollection AnAttributeCollection(this Stubber _,
        Attribute? item = default,
        IEnumerable<Attribute>? items = default
    )
    {
        items ??= [];

        var result = new AttributeCollection();

        if (item is not null)
        {
            ((IMutableAttributeCollection)result).Add(item);
        }

        foreach (var current in items)
        {
            ((IMutableAttributeCollection)result).Add(current);
        }

        return result;
    }
}