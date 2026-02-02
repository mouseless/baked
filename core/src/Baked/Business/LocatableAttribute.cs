using Baked.Domain.Model;

namespace Baked.Business;

[AttributeUsage(AttributeTargets.Class)]
public class LocatableAttribute(Type ServiceType, string LocateMethodName)
    : Attribute
{
    public Type ServiceType { get; set; } = ServiceType;
    public string LocateMethodName { get; set; } = LocateMethodName;
    public bool IsFactory { get; set; } = false;
    public bool IsAsync { get; set; } = false;
    public string? LocateManyMethodName { get; set; }
    public TypeModel? CastTo { get; set; }
}