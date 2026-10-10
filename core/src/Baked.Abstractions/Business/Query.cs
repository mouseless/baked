namespace Baked.Business;

[AttributeUsage(AttributeTargets.Class)]
public class Query(Type locatableType)
    : Attribute()
{
    public Type LocatableType { get; } = locatableType;
}