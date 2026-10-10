namespace Baked.Business;

[AttributeUsage(AttributeTargets.Class)]
public class QueryClass(Type locatableType)
    : Attribute()
{
    public Type LocatableType { get; } = locatableType;
}