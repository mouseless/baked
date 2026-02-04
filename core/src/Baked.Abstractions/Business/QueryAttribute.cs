namespace Baked.Business;

[AttributeUsage(AttributeTargets.Class)]
public class QueryAttribute(Type locatableType)
    : Attribute
{
    public Type LocatableType { get; } = locatableType;
}