namespace Do.Orm;

[AttributeUsage(AttributeTargets.Class)]
public class EntityAttribute(Type queryType, Type queryContextType)
    : Attribute
{
    public Type QueryType { get; } = queryType;
    public Type QueryContextType { get; } = queryContextType;
}
