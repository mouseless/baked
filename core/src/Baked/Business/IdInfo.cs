using Baked.Domain.Model;

namespace Baked.Business;

public record IdInfo(string Type, string PropertyName, string RouteName)
{
    public IdInfo(PropertyModel property)
        : this(property.PropertyType.CSharpFriendlyFullName, property.Name, property.Get<IdAttribute>().RouteName) { }
}