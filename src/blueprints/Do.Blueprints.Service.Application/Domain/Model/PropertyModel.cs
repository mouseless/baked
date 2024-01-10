using System.Reflection;

namespace Do.Domain.Model;

public record PropertyModel(string Name, bool IsPublic)
{
    public PropertyModel(PropertyInfo propertyInfo)
        : this(propertyInfo.Name, propertyInfo.CanRead)
    { }
}
