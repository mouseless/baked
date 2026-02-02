namespace Baked.Business;

[AttributeUsage(AttributeTargets.Class)]
public class LocatableAttribute(Type ServiceType)
    : Attribute
{
    public Type ServiceType { get; set; } = ServiceType;
    public bool IsAsync { get; set; } = false;
    public Locate LocateRenderer { get; set; } = default!;
    public Locate? LocateManyRenderer { get; set; }

    public delegate string Locate(string serviceExpression, string idExpression);
    public delegate string LocateMany(string serviceExpression, string idsExpression);
}